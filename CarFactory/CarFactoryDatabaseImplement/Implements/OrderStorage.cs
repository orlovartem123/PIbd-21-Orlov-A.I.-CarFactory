using CarFactoryBusinessLogic.BindingModels;
using CarFactoryBusinessLogic.Interfaces;
using CarFactoryBusinessLogic.ViewModels;
using CarFactoryDatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarFactoryDatabaseImplement.Implements
{
	public class OrderStorage : IOrderStorage
	{
		public void Delete(OrderBindingModel model)
		{
			using (var context = new CarFactoryDbContext())
			{
				Order element = context.Orders.FirstOrDefault(rec => rec.Id == model.Id);
				if (element != null)
				{
					context.Orders.Remove(element);
					context.SaveChanges();
				}
				else
				{
					throw new Exception("Element not found");
				}
			}
		}

		public OrderViewModel GetElement(OrderBindingModel model)
		{
			if (model == null)
			{
				return null;
			}
			using (var context = new CarFactoryDbContext())
			{
				var order = context.Orders
				.FirstOrDefault(rec => rec.Id == model.Id || rec.Id == model.Id);
				return order != null ?
				new OrderViewModel
				{
					Id = order.Id,
					CarId = order.CarId,
					CarName = context.Cars.FirstOrDefault(car => car.Id == order.CarId)?.CarName,
					Count = order.Count,
					Sum = order.Sum,
					Status = order.Status,
					DateCreate = order.DateCreate,
					DateImplement = order?.DateImplement
				} : null;
			}
		}

		public List<OrderViewModel> GetFilteredList(OrderBindingModel model)
		{
			if (model == null)
			{
				return null;
			}
			using (var context = new CarFactoryDbContext())
			{
				return context.Orders
				.Where(rec => rec.Id == model.Id)
				.Select(rec => new OrderViewModel
				{
					Id = rec.Id,
					CarId = rec.CarId,
					CarName = context.Cars.FirstOrDefault(car => car.Id == rec.CarId).CarName,
					Count = rec.Count,
					Sum = rec.Sum,
					Status = rec.Status,
					DateCreate = rec.DateCreate,
					DateImplement = rec.DateImplement
				}).ToList();
			}
		}

		public List<OrderViewModel> GetFullList()
		{
			using (var context = new CarFactoryDbContext())
			{
				return context.Orders
				.Select(rec => new OrderViewModel
				{
					Id = rec.Id,
					CarId = rec.CarId,
					CarName = context.Cars.FirstOrDefault(car => car.Id == rec.CarId).CarName,
					Count = rec.Count,
					Sum = rec.Sum,
					Status = rec.Status,
					DateCreate = rec.DateCreate,
					DateImplement = rec.DateImplement
				}).ToList();
			}
		}

		public void Insert(OrderBindingModel model)
		{
			using (var context = new CarFactoryDbContext())
			{
				context.Orders.Add(CreateModel(model, new Order()));
				context.SaveChanges();
			}
		}

		public void Update(OrderBindingModel model)
		{
			using (var context = new CarFactoryDbContext())
			{
				var element = context.Orders.FirstOrDefault(rec => rec.Id == model.Id);
				if (element == null)
				{
					throw new Exception("Element not found");
				}
				CreateModel(model, element);
				context.SaveChanges();
			}
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
			using (var context = new CarFactoryDbContext())
			{
				return new OrderViewModel
				{
					Id = order.Id,
					CarId = order.CarId,
					CarName = context.Cars.FirstOrDefault(car => car.Id == order.CarId)?.CarName,
					Count = order.Count,
					Sum = order.Sum,
					Status = order.Status,
					DateCreate = order.DateCreate,
					DateImplement = order?.DateImplement
				};
			}
		}
	}
}
