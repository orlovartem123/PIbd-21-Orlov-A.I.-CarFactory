using System.Collections.Generic;
using System.ComponentModel;

namespace CarFactoryBusinessLogic.ViewModels
{
    public class CarViewModel
    {
        public int Id { get; set; }

        [DisplayName("Car name")]
        public string CarName { get; set; }

        [DisplayName("Price")]
        public decimal Price { get; set; }

        public Dictionary<int, (string, int)> CarComponents { get; set; }
    }
}
