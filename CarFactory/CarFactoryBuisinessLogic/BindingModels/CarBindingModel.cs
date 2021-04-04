using System.Collections.Generic;

namespace CarFactoryBusinessLogic.BindingModels
{
    public class CarBindingModel
    {
        public int? Id { get; set; }
        public string CarName { get; set; }
        public decimal Price { get; set; }
        public Dictionary<int, (string, int)> CarComponents { get; set; }
    }
}
