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

        public ReportLogic(ICarStorage carStorage, IComponentStorage
       componentStorage, IOrderStorage orderStorage)
        {
            _carStorage = carStorage;
            _componentStorage = componentStorage;
            _orderStorage = orderStorage;
        }        public List<ReportCarComponentViewModel> GetCarComponent()
        {
            var components = _componentStorage.GetFullList();
            var cars = _carStorage.GetFullList();
            var list = new List<ReportCarComponentViewModel>();
            foreach (var component in components)
            {
                var record = new ReportCarComponentViewModel
                {
                    ComponentName = component.ComponentName,
                    Cars = new List<Tuple<string, int>>(),
                    TotalCount = 0
                };
                foreach (var car in cars)
                {
                    if (car.CarComponents.ContainsKey(component.Id))
                    {
                        record.Cars.Add(new Tuple<string, int>(car.CarName,
                       car.CarComponents[component.Id].Item2));
                        record.TotalCount +=
                       car.CarComponents[component.Id].Item2;
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
        }        public void SaveComponentsToWordFile(ReportBindingModel model)
        {
            SaveToWord.CreateDoc(new WordInfo
            {
                FileName = model.FileName,
                Title = "Cars list",
                Cars = _carStorage.GetFullList()
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
                CarComponents = GetCarComponent()
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
    }
}
