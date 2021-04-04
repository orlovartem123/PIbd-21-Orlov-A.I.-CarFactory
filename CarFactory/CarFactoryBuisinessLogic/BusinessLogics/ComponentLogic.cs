using CarFactoryBusinessLogic.BindingModels;
using CarFactoryBusinessLogic.Interfaces;
using CarFactoryBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;

namespace CarFactoryBusinessLogic.BusinessLogics
{
    public class ComponentLogic
    {
        private readonly IComponentStorage _componentStorage;

        public ComponentLogic(IComponentStorage componentStorage)
        {
            _componentStorage = componentStorage;
        }

        public List<ComponentViewModel> Read(ComponentBindingModel model)
        {
            if (model == null)
            {
                return _componentStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<ComponentViewModel> { _componentStorage.GetElement(model) };
            }
            return _componentStorage.GetFilteredList(model);
        }

        public void CreateOrUpdate(ComponentBindingModel model)
        {
            var element = _componentStorage.GetElement(new ComponentBindingModel
            {
                ComponentName = model.ComponentName
            });
            if (element != null && element.Id != model.Id)
            {
                throw new Exception("There is already a component with the same name");
            }
            if (model.Id.HasValue)
            {
                _componentStorage.Update(model);
            }
            else
            {
                _componentStorage.Insert(model);
            }
        }        public void Delete(ComponentBindingModel model)
        {
            var element = _componentStorage.GetElement(new ComponentBindingModel { Id = model.Id });
            if (element == null)
            {
                throw new Exception("Element not found");
            }
            _componentStorage.Delete(model);
        }
    }
}
