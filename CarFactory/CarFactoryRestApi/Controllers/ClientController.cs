using CarFactoryBusinessLogic.BindingModels;
using CarFactoryBusinessLogic.BusinessLogics;
using CarFactoryBusinessLogic.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace CarFactoryRestApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]

    public class ClientController : ControllerBase
    {
        private readonly ClientLogic _logic;

        private readonly MailLogic _logicM;

        private readonly int _passwordMaxLength = 50;

        private readonly int _passwordMinLength = 10;

        public ClientController(ClientLogic logic, MailLogic logicM)
        {
            _logic = logic;
            _logicM = logicM;
        }

        [HttpGet]
        public ClientViewModel Login(string login, string password) => _logic.Read(new ClientBindingModel { Email = login, Password = password })?[0];

        [HttpPost]
        public void Register(ClientBindingModel model) => _logic.CreateOrUpdate(model);

        [HttpGet]
        public List<MessageInfoViewModel> GetMessages(int clientId) => _logicM.Read(new MessageInfoBindingModel { ClientId = clientId });

        [HttpPost]
        public void UpdateData(ClientBindingModel model)
        {
            CheckData(model);
            _logic.CreateOrUpdate(model);
        }

        private void CheckData(ClientBindingModel model)
        {
            if (!Regex.IsMatch(model.Email, @"regular expression"))
            {
                throw new Exception("Email must be specified as login");
            }
            if (model.Password.Length > _passwordMaxLength || model.Password.Length < _passwordMinLength || !Regex.IsMatch(model.Password,
           @"^((\w+\d+\W+)|(\w+\W+\d+)|(\d+\w+\W+)|(\d+\W+\w+)|(\W+\w+\d+)|(\W+\d+\w+))[\w\d\W]*$"))
            {
                throw new Exception($"Password with length from {_passwordMinLength} to { _passwordMaxLength } must consist of numbers, letters and non-alphabetic characters");
            }
        }
    }
}
