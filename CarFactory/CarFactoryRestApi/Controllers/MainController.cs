using CarFactoryBusinessLogic.BindingModels;
using CarFactoryBusinessLogic.BusinessLogics;
using CarFactoryBusinessLogic.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace CarFactoryRestApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MainController : ControllerBase
    {
        private readonly OrderLogic _order;

        private readonly CarLogic _car;

        private readonly OrderLogic _main;

        public MainController(OrderLogic order, CarLogic car, OrderLogic main)
        {
            _order = order;
            _car = car;
            _main = main;
        }

        [HttpGet]
        public List<CarViewModel> GetCarList() => _car.Read(null)?.ToList();

        [HttpGet]
        public CarViewModel GetCar(int carId) => _car.Read(new CarBindingModel { Id = carId })?[0];

        [HttpGet]
        public List<OrderViewModel> GetOrders(int clientId) => _order.Read(new OrderBindingModel { ClientId = clientId });

        [HttpPost]
        public void CreateOrder(CreateOrderBindingModel model) => _main.CreateOrder(model);
    }
}
