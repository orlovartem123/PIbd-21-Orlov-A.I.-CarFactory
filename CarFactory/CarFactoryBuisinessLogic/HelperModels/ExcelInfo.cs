using CarFactoryBusinessLogic.ViewModels;
using System.Collections.Generic;

namespace CarFactoryBusinessLogic.HelperModels
{
    public class ExcelInfo
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public List<ReportCarComponentViewModel> Cars { get; set; }
    }
}
