using CarFactoryBusinessLogic.BindingModels;
using CarFactoryBusinessLogic.Interfaces;
using CarFactoryBusinessLogic.ViewModels;
using CarFactoryDatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CarFactoryDatabaseImplement.Implements
{
    public class WarehouseStorage : IWarehouseStorage
    {
        public bool CheckComponentsCount(int count, Dictionary<int, (string, int)> components)
        {
            using (var context = new CarFactoryDbContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        foreach (var component in components)
                        {
                            int requiredCount = component.Value.Item2 * count;
                            foreach (var warehouse in context.Warehouses.Include(rec => rec.WarehouseComponents))
                            {
                                int? availableCount = warehouse.WarehouseComponents.FirstOrDefault(rec => rec.ComponentId == component.Key)?.Count;
                                if (availableCount == null) { continue; }
                                requiredCount -= (int)availableCount;
                                warehouse.WarehouseComponents.FirstOrDefault(rec => rec.ComponentId == component.Key).Count = (requiredCount < 0) ? (int)availableCount - ((int)availableCount + requiredCount) : 0;
                            }
                            if (requiredCount > 0)
                            {
                                throw new Exception("Not enough components in warehouses");
                            }
                        }
                        context.SaveChanges();
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
            return true;
        }

        public void Delete(WarehouseBindingModel model)
        {
            using (var context = new CarFactoryDbContext())
            {
                Warehouse element = context.Warehouses.FirstOrDefault(rec => rec.Id == model.Id);
                if (element != null)
                {
                    context.Warehouses.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Warehouse not found");
                }
            }
        }

        public WarehouseViewModel GetElement(WarehouseBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new CarFactoryDbContext())
            {
                var warehouse = context.Warehouses
                .Include(rec => rec.WarehouseComponents)
               .ThenInclude(rec => rec.Component)
               .FirstOrDefault(rec => rec.WarehouseName == model.WarehouseName || rec.Id == model.Id);
                return warehouse != null ?
                CreateModel(warehouse) : null;
            }
        }

        public List<WarehouseViewModel> GetFilteredList(WarehouseBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new CarFactoryDbContext())
            {
                return context.Warehouses
                .Include(rec => rec.WarehouseComponents)
                .ThenInclude(rec => rec.Component)
                .Where(rec => rec.WarehouseName.Contains(model.WarehouseName))
                .ToList()
                .Select(CreateModel).ToList();
            }
        }

        public List<WarehouseViewModel> GetFullList()
        {
            using (var context = new CarFactoryDbContext())
            {
                return context.Warehouses
                .Include(rec => rec.WarehouseComponents)
                .ThenInclude(rec => rec.Component).ToList()
                .Select(CreateModel).ToList();
            }
        }

        public void Insert(WarehouseBindingModel model)
        {
            using (var context = new CarFactoryDbContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var warehouse = CreateModel(model, new Warehouse());
                        context.Warehouses.Add(warehouse);
                        context.SaveChanges();
                        warehouse = CreateModel(model, warehouse, context);
                        context.SaveChanges();
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public void Update(WarehouseBindingModel model)
        {
            using (var context = new CarFactoryDbContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var element = context.Warehouses.FirstOrDefault(rec => rec.Id == model.Id);
                        if (element == null)
                        {
                            throw new Exception("Warehouse not found");
                        }
                        CreateModel(model, element, context);
                        context.SaveChanges();
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public WarehouseViewModel CreateModel(Warehouse warehouse)
        {
            return new WarehouseViewModel
            {
                Id = warehouse.Id,
                WarehouseName = warehouse.WarehouseName,
                ManagerFullName = warehouse.ManagerFullName,
                DateCreate = warehouse.DateCreate,
                WarehouseComponents = warehouse.WarehouseComponents.ToDictionary(recCC => recCC.ComponentId, recCC =>
                  (recCC.Component?.ComponentName, recCC.Count))
            };
        }

        private Warehouse CreateModel(WarehouseBindingModel model, Warehouse warehouse)
        {
            warehouse.WarehouseName = model.WarehouseName;
            warehouse.ManagerFullName = model.ManagerFullName;
            warehouse.DateCreate = model.DateCreate;
            return warehouse;
        }

        private Warehouse CreateModel(WarehouseBindingModel model, Warehouse warehouse, CarFactoryDbContext context)
        {
            warehouse.WarehouseName = model.WarehouseName;
            warehouse.ManagerFullName = model.ManagerFullName;
            warehouse.DateCreate = model.DateCreate;
            if (model.Id.HasValue)
            {
                var warehouseComponents = context.WarehouseComponents.Where(rec => rec.WarehouseId == model.Id.Value).ToList();
                context.WarehouseComponents.RemoveRange(warehouseComponents.Where(rec => !model.WarehouseComponents.ContainsKey(rec.ComponentId)).ToList());
                context.SaveChanges();
                foreach (var updateComponent in warehouseComponents)
                {
                    updateComponent.Count = model.WarehouseComponents[updateComponent.ComponentId].Item2;
                    model.WarehouseComponents.Remove(updateComponent.ComponentId);
                }
                context.SaveChanges();
            }
            foreach (var wc in model.WarehouseComponents)
            {
                context.WarehouseComponents.Add(new WarehouseComponent
                {
                    WarehouseId = warehouse.Id,
                    ComponentId = wc.Key,
                    Count = wc.Value.Item2
                });

            }
            context.SaveChanges();
            return warehouse;
        }
    }
}
