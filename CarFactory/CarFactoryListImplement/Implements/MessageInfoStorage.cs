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
                result.Add(new MessageInfoViewModel
                {
                    MessageId = msg.MessageId,
                    SenderName = msg.SenderName,
                    DateDelivery = msg.DateDelivery,
                    Subject = msg.Subject,
                    Body = msg.Body
                });
            }
            return result;
        }

        public List<MessageInfoViewModel> GetFilteredList(MessageInfoBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            var result = new List<MessageInfoViewModel>();
            foreach (var msg in source.Messages)
            {
                if ((model.ClientId.HasValue && msg.ClientId == model.ClientId) ||
                    (!model.ClientId.HasValue && msg.DateDelivery.Date == model.DateDelivery.Date))
                {
                    result.Add(new MessageInfoViewModel
                    {
                        MessageId = msg.MessageId,
                        SenderName = msg.SenderName,
                        DateDelivery = msg.DateDelivery,
                        Subject = msg.Subject,
                        Body = msg.Body
                    });
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
    }
}
