using CarFactoryBusinessLogic.Enums;
using System;
using System.ComponentModel;

namespace CarFactoryBusinessLogic.ViewModels
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public int CarId { get; set; }

        [DisplayName("Car")]
        public string CarName { get; set; }

        [DisplayName("Quantity")]
        public int Count { get; set; }

        [DisplayName("Sum")]
        public decimal Sum { get; set; }

        [DisplayName("Status")]
        public OrderStatus Status { get; set; }

        [DisplayName("Creation date")]
        public DateTime DateCreate { get; set; }

        [DisplayName("Complition date")]
        public DateTime? DateImplement { get; set; }
    }
}
