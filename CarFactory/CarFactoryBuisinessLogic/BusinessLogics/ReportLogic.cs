using CarFactoryBusinessLogic.BindingModels;
using CarFactoryBusinessLogic.HelperModels;
using CarFactoryBusinessLogic.Interfaces;
using CarFactoryBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CarFactoryBusinessLogic.BusinessLogics
{
    public class ReportLogic
    {
        private readonly IComponentStorage _componentStorage;
        private readonly ICarStorage _carStorage;
        private readonly IOrderStorage _orderStorage;
        private readonly IWarehouseStorage _warehouseStorage;

        public ReportLogic(ICarStorage carStorage, IComponentStorage
       componentStorage, IOrderStorage orderStorage, IWarehouseStorage warehouseStorage)
        {
            _carStorage = carStorage;
            _componentStorage = componentStorage;
            _orderStorage = orderStorage;
            _warehouseStorage = warehouseStorage;
        }        public List<ReportCarComponentViewModel> GetCarComponent()
        {
            var components = _componentStorage.GetFullList();
            var cars = _carStorage.GetFullList();
            var list = new List<ReportCarComponentViewModel>();
            foreach (var car in cars)
            {
                var record = new ReportCarComponentViewModel
                {
                    CarName = car.CarName,
                    Components = new List<Tuple<string, int>>(),
                    TotalCount = 0
                };
                foreach (var component in components)
                {
                    if (car.CarComponents.ContainsKey(component.Id))
                    {
                        record.Components.Add(new Tuple<string, int>(component.ComponentName,
                       car.CarComponents[component.Id].Item2));
                        record.TotalCount +=
                       car.CarComponents[component.Id].Item2;
                    }
                }
                list.Add(record);
            }
            return list;
        }

        public List<ReportWarehouseComponentViewModel> GetWarehouseComponent()
        {
            var components = _componentStorage.GetFullList();
            var warehouses = _warehouseStorage.GetFullList();
            var list = new List<ReportWarehouseComponentViewModel>();
            foreach (var warehouse in warehouses)
            {
                var record = new ReportWarehouseComponentViewModel
                {
                    WarehouseName = warehouse.WarehouseName,
                    Components = new List<Tuple<string, int>>(),
                    TotalCount = 0
                };
                foreach (var component in components)
                {
                    if (warehouse.WarehouseComponents.ContainsKey(component.Id))
                    {
                        record.Components.Add(new Tuple<string, int>(component.ComponentName,
                       warehouse.WarehouseComponents[component.Id].Item2));
                        record.TotalCount +=
                       warehouse.WarehouseComponents[component.Id].Item2;
                    }
                }
                list.Add(record);
            }
            return list;
        }
        /// <summary>
        /// Получение списка заказов за определенный период
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<ReportOrdersViewModel> GetOrders(ReportBindingModel model)
        {
            return _orderStorage.GetFilteredList(new OrderBindingModel
            {
                DateFrom = model.DateFrom,
                DateTo = model.DateTo
            })
            .Select(x => new ReportOrdersViewModel
            {
                DateCreate = x.DateCreate,
                CarName = x.CarName,
                Count = x.Count,
                Sum = x.Sum,
                Status = x.Status
            })
           .ToList();
        }        public List<ReportOrderByDatesViewModel> GetOrdersByDates()
        {
            return _orderStorage.GetFullList()
            .GroupBy(rec => rec.DateCreate.ToShortDateString())
            .Select(group => new ReportOrderByDatesViewModel
            {
                DateCreate = group.FirstOrDefault().DateCreate,
                OrdersCount = group.Count(),
                TotalSum = group.Sum(rec => rec.Sum)
            }).ToList();
        }        public void SaveComponentsToWordFile(ReportBindingModel model)
        {
            SaveToWord.CreateDoc(new WordInfo
            {
                FileName = model.FileName,
                Title = "Cars list",
                Cars = _carStorage.GetFullList()
            });
        }

        public void SaveWarehousesToWordFile(ReportBindingModel model)
        {
            SaveToWord.CreateDocWarehouses(new WordInfo
            {
                FileName = model.FileName,
                Title = "Warehouses list",
                Warehouses = _warehouseStorage.GetFullList()
            });
        }

        public void SaveWarehouseComponentToExcelFile(ReportBindingModel model)
        {
            SaveToExcel.CreateDoc(new ExcelInfo
            {
                FileName = model.FileName,
                Title = "Warehouses list",
                Warehouses = GetWarehouseComponent()
            });
        }
        /// <summary>
        /// Сохранение компонент с указаеним продуктов в файл-Excel
        /// </summary>
        /// <param name="model"></param>
        public void SaveCarComponentToExcelFile(ReportBindingModel model)
        {
            SaveToExcel.CreateDoc(new ExcelInfo
            {
                FileName = model.FileName,
                Title = "Components list",
                Cars = GetCarComponent()
            });
        }

        /// <summary>
        /// Сохранение заказов в файл-Pdf
        /// </summary>
        /// <param name="model"></param>
        [Obsolete]
        public void SaveOrdersToPdfFile(ReportBindingModel model)
        {
            SaveToPdf.CreateDoc(new PdfInfo
            {
                FileName = model.FileName,
                Title = "Orders list",
                DateFrom = model.DateFrom.Value,
                DateTo = model.DateTo.Value,
                Orders = GetOrders(model)
            });
        }

        [Obsolete]
        public void SaveOrdersByDatesToPdfFile(ReportBindingModel model)
        {
            SaveToPdf.CreateDocOrdersByDates(new PdfInfoOrdersByDates
            {
                FileName = model.FileName,
                Title = "Orders by dates list",
                Orders = GetOrdersByDates()
            });
        }
    }
}
