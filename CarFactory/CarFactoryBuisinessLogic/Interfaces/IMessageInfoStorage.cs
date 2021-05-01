using CarFactoryBusinessLogic.BindingModels;
using CarFactoryBusinessLogic.ViewModels;
using System.Collections.Generic;

namespace CarFactoryBusinessLogic.Interfaces
{
    public interface IMessageInfoStorage
    {
        List<MessageInfoViewModel> GetFullList();

        List<MessageInfoViewModel> GetFilteredList(MessageInfoBindingModel model);

        void Insert(MessageInfoBindingModel model);
    }
}
