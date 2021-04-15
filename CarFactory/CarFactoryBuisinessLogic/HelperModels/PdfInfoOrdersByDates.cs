using CarFactoryBusinessLogic.ViewModels;
using System.Collections.Generic;

namespace CarFactoryBusinessLogic.HelperModels
{
    public class PdfInfoOrdersByDates
    {
        public string FileName { get; set; }

        public string Title { get; set; }

        public List<ReportOrderByDatesViewModel> Orders { get; set; }
    }
}
