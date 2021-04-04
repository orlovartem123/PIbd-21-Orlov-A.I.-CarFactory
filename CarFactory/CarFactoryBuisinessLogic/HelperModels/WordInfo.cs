using CarFactoryBusinessLogic.ViewModels;
using System.Collections.Generic;

namespace CarFactoryBusinessLogic.HelperModels
{
    public class WordInfo
    {
        public string FileName { get; set; }

        public string Title { get; set; }

        public List<CarViewModel> Cars { get; set; }
    }
}
