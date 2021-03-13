using CarFactoryBusinessLogic.BindingModels;
using CarFactoryBusinessLogic.Interfaces;
using CarFactoryBusinessLogic.ViewModels;
using CarFactoryFileImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CarFactoryFileImplement.Implements
{
    public class OrderStorage : IOrderStorage
    {
        private readonly FileDataListSingleton source;

        public OrderStorage()
        {
            source = FileDataListSingleton.GetInstance();
        }

        public void Delete(OrderBindingModel model)
        {
            Order element = source.Orders.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {

                source.Orders.Remove(element);
            }
            else
            {
                throw new Exception("Element not found");
            }
        }

        public OrderViewModel GetElement(OrderBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            var component = source.Orders.FirstOrDefault(rec => rec.Id == model.Id);
            return component != null ? CreateModel(component) : null;
        }

        public List<OrderViewModel> GetFilteredList(OrderBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            return source.Orders.Where(rec =>
                rec.Id == model.Id).OrderBy(res => res.DateCreate).Select(CreateModel).ToList();
        }

        public List<OrderViewModel> GetFullList()
        {
            return source.Orders.Select(CreateModel).ToList();
        }

        public void Insert(OrderBindingModel model)
        {
            int maxId = source.Orders.Count > 0 ? source.Orders.Max(rec => rec.Id) : 0;
            var element = new Order { Id = maxId + 1 };
            source.Orders.Add(CreateModel(model, element));
        }

        public void Update(OrderBindingModel model)
        {
            var element = source.Orders.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Element not found");
            }
            CreateModel(model, element);
        }

        private Order CreateModel(OrderBindingModel model, Order order)
        {
            order.CarId = model.CarId;
            order.Count = model.Count;
            order.Status = model.Status;
            order.Sum = model.Sum;
            order.DateCreate = model.DateCreate;
            order.DateImplement = model.DateImplement;
            return order;
        }

        private OrderViewModel CreateModel(Order order)
        {
            return new OrderViewModel
            {
                Id = order.Id,
                CarId = order.CarId,
                CarName = source.Cars.FirstOrDefault(car => car.Id == order.CarId)?.CarName,
                Count = order.Count,
                Sum = order.Sum,
                Status = order.Status,
                DateCreate = order.DateCreate,
                DateImplement = order?.DateImplement
            };
        }
    }
}
