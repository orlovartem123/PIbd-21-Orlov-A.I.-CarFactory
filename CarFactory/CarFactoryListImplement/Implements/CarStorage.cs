using CarFactoryBusinessLogic.BindingModels;
using CarFactoryBusinessLogic.Interfaces;
using CarFactoryBusinessLogic.ViewModels;
using CarFactoryListImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CarFactoryListImplement.Implements
{
	public class CarStorage : ICarStorage
	{
		private readonly DataListSingleton source;

		public CarStorage()
		{
			source = DataListSingleton.GetInstance();
		}

		public List<CarViewModel> GetFullList()
		{
			List<CarViewModel> result = new List<CarViewModel>();
			foreach (var component in source.Cars)
			{
				result.Add(CreateModel(component));
			}
			return result;
		}

		public List<CarViewModel> GetFilteredList(CarBindingModel model)
		{
			if (model == null)
			{
				return null;
			}
			List<CarViewModel> result = new List<CarViewModel>();
			foreach (var car in source.Cars)
			{
				if (car.CarName.Contains(model.CarName))
				{
					result.Add(CreateModel(car));
				}
			}
			return result;
		}

		public CarViewModel GetElement(CarBindingModel model)
		{
			if (model == null)
			{
				return null;
			}
			foreach (var car in source.Cars)
			{
				if (car.Id == model.Id || car.CarName ==
				model.CarName)
				{
					return CreateModel(car);
				}
			}
			return null;
		}

		public void Insert(CarBindingModel model)
		{
			Car tempCar = new Car
			{
				Id = 1,
				CarComponents = new
			Dictionary<int, int>()
			};
			foreach (var car in source.Cars)
			{
				if (car.Id >= tempCar.Id)
				{
					tempCar.Id = car.Id + 1;
				}
			}
			source.Cars.Add(CreateModel(model, tempCar));
		}

		public void Update(CarBindingModel model)
		{
			Car tempCar = null;
			foreach (var car in source.Cars)
			{
				if (car.Id == model.Id)
				{
                    tempCar = car;
				}
			}
			if (tempCar == null)
			{
				throw new Exception("Element not found");
			}
			CreateModel(model, tempCar);
		}

		public void Delete(CarBindingModel model)
		{
			for (int i = 0; i < source.Cars.Count; ++i)
			{
				if (source.Cars[i].Id == model.Id)
				{
					source.Cars.RemoveAt(i);
					return;
				}
			}
			throw new Exception("Element not found");
		}

		private Car CreateModel(CarBindingModel model, Car car)
		{
            car.CarName = model.CarName;
            car.Price = model.Price;
			// удаляем убранные
			foreach (var key in car.CarComponents.Keys.ToList())
			{
				if (!model.CarComponents.ContainsKey(key))
				{
                    car.CarComponents.Remove(key);
				}
			}
			// обновляем существуюущие и добавляем новые
			foreach (var component in model.CarComponents)
			{
				if (car.CarComponents.ContainsKey(component.Key))
				{
                    car.CarComponents[component.Key] =
					model.CarComponents[component.Key].Item2;
				}
				else
				{
                    car.CarComponents.Add(component.Key,
					model.CarComponents[component.Key].Item2);
				}
			}
			return car;
		}

		private CarViewModel CreateModel(Car car)
		{
			// требуется дополнительно получить список компонентов для изделия с
			//названиями и их количество
			Dictionary<int, (string, int)> carComponents = new
			Dictionary<int, (string, int)>();
			foreach (var pc in car.CarComponents)
			{
				string componentName = string.Empty;
				foreach (var component in source.Components)
				{
					if (pc.Key == component.Id)
					{
						componentName = component.ComponentName;
						break;
					}
				}
				carComponents.Add(pc.Key, (componentName, pc.Value));
			}
			return new CarViewModel
			{
				Id = car.Id,
				CarName = car.CarName,
				Price = car.Price,
				CarComponents = carComponents
			};
		}
	}
}
