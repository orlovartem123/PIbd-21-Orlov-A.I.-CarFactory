using CarFactoryBusinessLogic.BindingModels;
using CarFactoryBusinessLogic.Interfaces;
using CarFactoryBusinessLogic.ViewModels;
using CarFactoryDatabaseImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarFactoryDatabaseImplement.Implements
{
	public class ComponentStorage : IComponentStorage
	{
		public void Delete(ComponentBindingModel model)
		{
			using (var context = new CarFactoryDbContext())
			{
				Component element = context.Components.FirstOrDefault(rec => rec.Id == model.Id);
				if (element != null)
				{
					context.Components.Remove(element);
					context.SaveChanges();
				}
				else
				{
					throw new Exception("Element not found");
				}
			}
		}

		public ComponentViewModel GetElement(ComponentBindingModel model)
		{
			if (model == null)
			{
				return null;
			}
			using (var context = new CarFactoryDbContext())
			{
				var component = context.Components
				.FirstOrDefault(rec => rec.ComponentName == model.ComponentName || rec.Id == model.Id);
				return component != null ?
				new ComponentViewModel
				{
					Id = component.Id,
					ComponentName = component.ComponentName
				} : null;
			}

		}

		public List<ComponentViewModel> GetFilteredList(ComponentBindingModel model)
		{
			if (model == null)
			{
				return null;
			}
			using (var context = new CarFactoryDbContext())
			{
				return context.Components
				.Where(rec => rec.ComponentName.Contains(model.ComponentName))
				.Select(rec => new ComponentViewModel
				{
					Id = rec.Id,
					ComponentName = rec.ComponentName
				}).ToList();
			}
		}

		public List<ComponentViewModel> GetFullList()
		{
			using (var context = new CarFactoryDbContext())
			{
				return context.Components
				.Select(rec => new ComponentViewModel
				{
					Id = rec.Id,
					ComponentName = rec.ComponentName
				}).ToList();
			}
		}

		public void Insert(ComponentBindingModel model)
		{
			using (var context = new CarFactoryDbContext())
			{
				context.Components.Add(CreateModel(model, new Component()));
				context.SaveChanges();
			}
		}

		public void Update(ComponentBindingModel model)
		{
			using (var context = new CarFactoryDbContext())
			{
				var element = context.Components.FirstOrDefault(rec => rec.Id == model.Id);
				if (element == null)
				{
					throw new Exception("Element not found");
				}
				CreateModel(model, element);
				context.SaveChanges();
			}
		}

		private Component CreateModel(ComponentBindingModel model, Component component)
		{
			component.ComponentName = model.ComponentName;
			return component;
		}
	}
}
