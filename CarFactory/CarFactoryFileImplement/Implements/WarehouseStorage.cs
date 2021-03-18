using CarFactoryBusinessLogic.BindingModels;
using CarFactoryBusinessLogic.Interfaces;
using CarFactoryBusinessLogic.ViewModels;
using CarFactoryFileImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarFactoryFileImplement.Implements
{
    public class WarehouseStorage : IWarehouseStorage
    {
        private readonly FileDataListSingleton source;

        public WarehouseStorage()
        {
            source = FileDataListSingleton.GetInstance();
        }

        public List<WarehouseViewModel> GetFullList()
        {
            return source.Warehouses.Select(CreateModel).ToList();
        }

        public List<WarehouseViewModel> GetFilteredList(WarehouseBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            return source.Warehouses
                .Where(rec => rec.WarehouseName.Contains(model.WarehouseName))
                .Select(CreateModel)
                .ToList();
        }

        public WarehouseViewModel GetElement(WarehouseBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            var warehouse = source.Warehouses.FirstOrDefault(rec => rec.WarehouseName == model.WarehouseName || rec.Id == model.Id);
            return warehouse != null ? CreateModel(warehouse) : null;
        }

        public void Insert(WarehouseBindingModel model)
        {
            int maxId = source.Warehouses.Count > 0 ? source.Warehouses.Max(rec => rec.Id) : 0;
            var warehouse = new Warehouse
            {
                Id = maxId + 1,
                WarehouseComponents = new Dictionary<int, int>(),
                DateCreate = DateTime.Now
            };
            source.Warehouses.Add(CreateModel(model, warehouse));
        }

        public void Update(WarehouseBindingModel model)
        {
            var warehouse = source.Warehouses.FirstOrDefault(rec => rec.Id == model.Id);
            if (warehouse == null)
            {
                throw new Exception("Element not found");
            }
            CreateModel(model, warehouse);
        }

        public void Delete(WarehouseBindingModel model)
        {
            var warehouse = source.Warehouses.FirstOrDefault(rec => rec.Id == model.Id);
            if (warehouse == null)
            {
                throw new Exception("Element not found");
            }
            source.Warehouses.Remove(warehouse);
        }

        private Warehouse CreateModel(WarehouseBindingModel model, Warehouse warehouse)
        {
            warehouse.WarehouseName = model.WarehouseName;
            warehouse.ManagerFullName = model.ManagerFullName;
            foreach (var key in warehouse.WarehouseComponents.Keys.ToList())
            {
                if (!model.WarehouseComponents.ContainsKey(key))
                {
                    warehouse.WarehouseComponents.Remove(key);
                }
            }
            foreach (var component in model.WarehouseComponents)
            {
                if (warehouse.WarehouseComponents.ContainsKey(component.Key))
                {
                    warehouse.WarehouseComponents[component.Key] = model.WarehouseComponents[component.Key].Item2;
                }
                else
                {
                    warehouse.WarehouseComponents.Add(component.Key, model.WarehouseComponents[component.Key].Item2);
                }
            }
            return warehouse;
        }

        private WarehouseViewModel CreateModel(Warehouse warehouse)
        {
            Dictionary<int, (string, int)> warehouseComponents = new Dictionary<int, (string, int)>();
            foreach (var warehouseComponent in warehouse.WarehouseComponents)
            {
                string componentName = string.Empty;
                foreach (var component in source.Components)
                {
                    if (warehouseComponent.Key == component.Id)
                    {
                        componentName = component.ComponentName;
                        break;
                    }
                }
                warehouseComponents.Add(warehouseComponent.Key, (componentName, warehouseComponent.Value));
            }
            return new WarehouseViewModel
            {
                Id = warehouse.Id,
                WarehouseName = warehouse.WarehouseName,
                ManagerFullName = warehouse.ManagerFullName,
                DateCreate = warehouse.DateCreate,
                WarehouseComponents = warehouseComponents
            };
        }

        public bool CheckComponentsCount(int count, Dictionary<int, (string, int)> components)
        {
            foreach (var component in components)
            {
                int compCount = source.Warehouses.Where(wh => wh.WarehouseComponents.ContainsKey(component.Key))
                    .Sum(wh => wh.WarehouseComponents[component.Key]);
                if (compCount < (component.Value.Item2 * count))
                {
                    return false;
                }
            }
            foreach (var component in components)
            {
                int requiredCount = component.Value.Item2 * count;
                while (requiredCount > 0)
                {
                    var warehouse = source.Warehouses
                        .FirstOrDefault(wh => wh.WarehouseComponents.ContainsKey(component.Key)
                        && wh.WarehouseComponents[component.Key] > 0);
                    int availableCount = warehouse.WarehouseComponents[component.Key];
                    requiredCount -= availableCount;
                    warehouse.WarehouseComponents[component.Key] = (requiredCount < 0) ? availableCount - (availableCount + requiredCount) : 0;
                }
            }
            return true;
        }
    }
}
