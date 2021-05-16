using CarFactoryBusinessLogic.BindingModels;
using CarFactoryBusinessLogic.Enums;
using CarFactoryBusinessLogic.HelperModels;
using CarFactoryBusinessLogic.Interfaces;
using CarFactoryBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;

namespace CarFactoryBusinessLogic.BusinessLogics
{
    public class OrderLogic
    {
        private readonly IOrderStorage _orderStorage;
        private readonly IWarehouseStorage _warehouseStorage;
        private readonly ICarStorage _carStorage;
        private readonly IClientStorage _clientStorage;
        private readonly object locker = new object();

        public OrderLogic(IOrderStorage orderStorage, IWarehouseStorage warehouseStorage, ICarStorage carStorage, IClientStorage clientStorage)
        {
            _orderStorage = orderStorage;
            _warehouseStorage = warehouseStorage;
            _carStorage = carStorage;
            _clientStorage = clientStorage;
        }

        public List<OrderViewModel> Read(OrderBindingModel model)
        {
            if (model == null)
            {
                return _orderStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<OrderViewModel> { _orderStorage.GetElement(model) };
            }
            return _orderStorage.GetFilteredList(model);
        }

        public void CreateOrder(CreateOrderBindingModel model)
        {
            _orderStorage.Insert(new OrderBindingModel
            {
                CarId = model.CarId,
                ClientId = model.ClientId,
                Count = model.Count,
                Sum = model.Sum,
                DateCreate = DateTime.Now,
                Status = OrderStatus.Accepted
            });

            MailLogic.MailSendAsync(new MailSendInfo
            {
                MailAddress = _clientStorage.GetElement(new ClientBindingModel
                {
                    Id = model.ClientId
                })?.Email,
                Subject = $"New order",
                Text = $"Order from {DateTime.Now} for the amount {model.Sum:N2} accepted."
            });

        }

        public void TakeOrderInWork(ChangeStatusBindingModel model)
        {
            lock (locker)
            {
                OrderStatus status = OrderStatus.Running;
                var order = _orderStorage.GetElement(new OrderBindingModel
                {
                    Id = model.OrderId
                });
                if (order == null)
                {
                    throw new Exception("Order not found");
                }
                if (order.Status != OrderStatus.Accepted)
                {
                    throw new Exception("Order isn't in the status \"Accepted\"");
                }
                var car = _carStorage.GetElement(new CarBindingModel { Id = order.CarId });
                if (!_warehouseStorage.CheckComponentsCount(order.Count, car.CarComponents))
                {
                    status = OrderStatus.NeedMaterials;
                }
                if (order.ImplementerId.HasValue)
                {
                    throw new Exception("Order already has implementer");
                }
                _orderStorage.Update(new OrderBindingModel
                {
                    Id = order.Id,
                    ClientId = order.ClientId,
                    ImplementerId = model.ImplementerId,
                    CarId = order.CarId,
                    Count = order.Count,
                    Sum = order.Sum,
                    DateCreate = order.DateCreate,
                    Status = status
                });

                MailLogic.MailSendAsync(new MailSendInfo
                {
                    MailAddress = _clientStorage.GetElement(new ClientBindingModel
                    {
                        Id =order.ClientId
                    })?.Email,
                    Subject = $"Order №{order.Id}",
                    Text = $"Order №{order.Id} took in work."
                });

            }
        }

        public void FinishOrder(ChangeStatusBindingModel model)
        {
            var order = _orderStorage.GetElement(new OrderBindingModel { Id = model.OrderId });
            if (order == null)
            {
                throw new Exception("Order not found");
            }
            if (order.Status == OrderStatus.NeedMaterials)
            {
                order.Status = OrderStatus.Running;
            }
            var car = _carStorage.GetElement(new CarBindingModel { Id = order.CarId });
            if (!_warehouseStorage.CheckComponentsCount(order.Count, car.CarComponents))
            {
                return;
            }
            if (order.Status != OrderStatus.Running)
            {
                throw new Exception("Order isn't in the status \"Running\"");
            }
            _orderStorage.Update(new OrderBindingModel
            {
                Id = order.Id,
                CarId = order.CarId,
                ImplementerId = order.ImplementerId,
                Count = order.Count,
                Sum = order.Sum,
                DateCreate = order.DateCreate,
                Status = OrderStatus.Ready
            });

            //send email
            MailLogic.MailSendAsync(new MailSendInfo
            {
                MailAddress = _clientStorage.GetElement(new ClientBindingModel
                {
                    Id = order.ClientId
                })?.Email,
                Subject = $"Finish order",
                Text = $"Order for the amount {order.Sum:N2} finished."
            });
        }

        public void PayOrder(ChangeStatusBindingModel model)
        {
            var order = _orderStorage.GetElement(new OrderBindingModel { Id = model.OrderId });
            if (order == null)
            {
                throw new Exception("Order not found");
            }
            if (order.Status != OrderStatus.Ready)
            {
                throw new Exception("Order isn't in the status \"Ready\"");
            }
            _orderStorage.Update(new OrderBindingModel
            {
                Id = order.Id,
                CarId = order.CarId,
                ImplementerId = order.ImplementerId,
                Count = order.Count,
                Sum = order.Sum,
                DateCreate = order.DateCreate,
                DateImplement = DateTime.Now,
                Status = OrderStatus.Paid
            });

            //send email
            MailLogic.MailSendAsync(new MailSendInfo
            {
                MailAddress = _clientStorage.GetElement(new ClientBindingModel
                {
                    Id = order.ClientId
                })?.Email,
                Subject = $"Paid order",
                Text = $"Order for the amount {order.Sum:N2} paid."
            });
        }
    }
}
