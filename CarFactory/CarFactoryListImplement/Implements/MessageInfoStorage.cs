using CarFactoryBusinessLogic.BindingModels;
using CarFactoryBusinessLogic.Interfaces;
using CarFactoryBusinessLogic.ViewModels;
using CarFactoryListImplement.Models;
using System.Collections.Generic;

namespace CarFactoryListImplement.Implements
{
    public class MessageInfoStorage : IMessageInfoStorage
    {
        private readonly DataListSingleton source;

        public MessageInfoStorage()
        {
            source = DataListSingleton.GetInstance();
        }

        public List<MessageInfoViewModel> GetFullList()
        {
            var result = new List<MessageInfoViewModel>();
            foreach (var msg in source.Messages)
            {
                result.Add(CreateModel(msg));
            }
            return result;
        }

        public List<MessageInfoViewModel> GetFilteredList(MessageInfoBindingModel model)
        {
            int toSkip = model.ToSkip ?? 0;
            int toTake = model.ToTake ?? source.Messages.Count;
            if (model == null)
            {
                return null;
            }
            var result = new List<MessageInfoViewModel>();
            if (model.ToSkip.HasValue && model.ToTake.HasValue && !model.ClientId.HasValue)
            {
                foreach (var msg in source.Messages)
                {
                    if (toSkip > 0) { toSkip--; continue; }
                    if (toTake > 0)
                    {
                        result.Add(CreateModel(msg));
                        toTake--;
                    }
                }
                return result;
            }
            foreach (var msg in source.Messages)
            {
                if ((model.ClientId.HasValue && msg.ClientId == model.ClientId) ||
                    (!model.ClientId.HasValue && msg.DateDelivery.Date == model.DateDelivery.Date))
                {
                    if (toSkip > 0) { toSkip--; continue; }
                    if (toTake > 0)
                    {
                        result.Add(CreateModel(msg));
                        toTake--;
                    }
                }
            }
            return result;
        }

        public void Insert(MessageInfoBindingModel model)
        {
            MessageInfo element = null;
            foreach (var msg in source.Messages)
            {
                if (msg.MessageId == model.MessageId)
                {
                    element = msg;
                    break;
                }
            }
            if (element != null)
            {
                return;
            }
            source.Messages.Add(new MessageInfo
            {
                MessageId = model.MessageId,
                ClientId = model.ClientId,
                SenderName = model.FromMailAddress,
                DateDelivery = model.DateDelivery,
                Subject = model.Subject,
                Body = model.Body
            });

        }

        private MessageInfoViewModel CreateModel(MessageInfo model)
        {
            return new MessageInfoViewModel
            {
                MessageId = model.MessageId,
                SenderName = model.SenderName,
                DateDelivery = model.DateDelivery,
                Subject = model.Subject,
                Body = model.Body
            };
        }
    }
}
