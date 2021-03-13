using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace CarFactoryBusinessLogic.ViewModels
{
    public class ComponentViewModel
    {
        public int Id { get; set; }

        [DisplayName("Component name")]
        public string ComponentName { get; set; }
    }
}
