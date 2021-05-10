using System;

namespace CarFactoryBusinessLogic.ViewModels
{
    public class ReportOrderByDatesViewModel
    {
        public DateTime DateCreate { get; set; }

        public int OrdersCount { get; set; }

        public decimal TotalSum { get; set; }
    }
}
