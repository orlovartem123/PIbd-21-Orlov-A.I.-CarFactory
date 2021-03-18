using CarFactoryBusinessLogic.BindingModels;
using CarFactoryBusinessLogic.Interfaces;
using CarFactoryBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarFactoryBusinessLogic.BusinessLogics
{
	public class CarLogic
	{
		private readonly ICarStorage _carStorage;

		public CarLogic(ICarStorage carStorage)
		{
			_carStorage = carStorage;
		}

		public List<CarViewModel> Read(CarBindingModel model)
		{
			if (model == null)
			{
				return _carStorage.GetFullList();
			}
			if (model.Id.HasValue)
			{
				return new List<CarViewModel> { _carStorage.GetElement(model) };
			}
			return _carStorage.GetFilteredList(model);
		}

		public void CreateOrUpdate(CarBindingModel model)
		{
            //var currCarView = _carStorage.GetElement(model);
            //if (currCarView == null)
            //{
            //	_carStorage.Insert(new CarBindingModel
            //	{
            //		Id = model.Id,
            //		CarName = model.CarName,
            //		Price = model.Price,
            //		CarComponents = model.CarComponents
            //	});
            //	return;
            //}
            //foreach (var carView in _carStorage.GetFullList())
            //{
            //	if (carView.Id == currCarView.Id)
            //	{
            //		carView.CarName = model.CarName;
            //		carView.CarComponents = model.CarComponents;
            //		carView.Price = model.Price;
            //		return;
            //	}
            //}
            var element = _carStorage.GetElement(new CarBindingModel
            {
                CarName = model.CarName
            });
            if (element != null && element.Id != model.Id)
            {
                throw new Exception("There is already a car with the same name");
            }
            if (model.Id.HasValue)
            {
                _carStorage.Update(model);
            }
            else
            {
                _carStorage.Insert(model);
            }
        }

        public void Delete(CarBindingModel model)
        {
            var element = _carStorage.GetElement(new CarBindingModel { Id = model.Id });
            if (element == null)
            {
                throw new Exception("Element not found");
            }
            _carStorage.Delete(model);
        }
    }
}
