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
    public class CarStorage : ICarStorage
    {
        public void Delete(CarBindingModel model)
        {
            using (var context = new CarFactoryDbContext())
            {
                Car element = context.Cars.FirstOrDefault(rec => rec.Id == model.Id);
                if (element != null)
                {
                    context.Cars.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Element not found");
                }
            }
        }

        public CarViewModel GetElement(CarBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new CarFactoryDbContext())
            {
                var car = context.Cars
                .Include(rec => rec.CarComponents)
               .ThenInclude(rec => rec.Component)
               .FirstOrDefault(rec => rec.CarName == model.CarName || rec.Id == model.Id);
                return car != null ?
                new CarViewModel
                {
                    Id = car.Id,
                    CarName = car.CarName,
                    Price = car.Price,
                    CarComponents = car.CarComponents.ToDictionary(recPC => recPC.ComponentId, recPC => (recPC.Component?.ComponentName, recPC.Count))
                } : null;
            }
        }

        public List<CarViewModel> GetFilteredList(CarBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new CarFactoryDbContext())
            {
                return context.Cars
                .Include(rec => rec.CarComponents)
                .ThenInclude(rec => rec.Component)
                .Where(rec => rec.CarName.Contains(model.CarName))
                .ToList()
                .Select(rec => new CarViewModel
                {
                    Id = rec.Id,
                    CarName = rec.CarName,
                    Price = rec.Price,
                    CarComponents = rec.CarComponents
                .ToDictionary(recCC => recCC.ComponentId, recCC => (recCC.Component?.ComponentName, recCC.Count))
                }).ToList();
            }

        }

        public List<CarViewModel> GetFullList()
        {
            using (var context = new CarFactoryDbContext())
            {
                return context.Cars
                .Include(rec => rec.CarComponents)
                .ThenInclude(rec => rec.Component).ToList()
                .Select(rec => new CarViewModel
                {
                    Id = rec.Id,
                    CarName = rec.CarName,
                    Price = rec.Price,
                    CarComponents = rec.CarComponents
                .ToDictionary(recCC => recCC.ComponentId, recCC =>
                (recCC.Component?.ComponentName, recCC.Count))
                }).ToList();
            }
        }

        public void Insert(CarBindingModel model)
        {
            var value = model.Id;
            using (var context = new CarFactoryDbContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var car = CreateModel(model, new Car());
                        context.Cars.Add(car);
                        context.SaveChanges();
                        car = CreateModel(model, car, context);
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

        public void Update(CarBindingModel model)
        {
            using (var context = new CarFactoryDbContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var element = context.Cars.FirstOrDefault(rec => rec.Id == model.Id);
                        if (element == null)
                        {
                            throw new Exception("Element not found");
                        }
                        CreateModel(model, element, context);
                        context.SaveChanges();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        private Car CreateModel(CarBindingModel model, Car car)
        {
            car.CarName = model.CarName;
            car.Price = model.Price;
            return car;
        }

        private Car CreateModel(CarBindingModel model, Car car, CarFactoryDbContext context)
        {
            car.CarName = model.CarName;
            car.Price = model.Price;
            if (model.Id.HasValue)
            {
                var carComponents = context.CarComponents.Where(rec => rec.CarId == model.Id.Value).ToList();
                context.CarComponents.RemoveRange(carComponents.Where(rec => !model.CarComponents.ContainsKey(rec.ComponentId)).ToList());
                context.SaveChanges();
                foreach (var updateComponent in carComponents)
                {
                    updateComponent.Count = model.CarComponents[updateComponent.ComponentId].Item2;
                    model.CarComponents.Remove(updateComponent.ComponentId);
                }
                context.SaveChanges();
            }
            foreach (var pc in model.CarComponents)
            {
                context.CarComponents.Add(new CarComponent
                {
                    CarId = car.Id,
                    ComponentId = pc.Key,
                    Count = pc.Value.Item2
                });

            }
            context.SaveChanges();
            return car;
        }
    }
}
