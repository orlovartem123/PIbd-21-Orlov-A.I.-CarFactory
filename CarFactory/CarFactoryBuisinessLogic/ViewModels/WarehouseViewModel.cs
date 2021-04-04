using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace CarFactoryBusinessLogic.ViewModels
{
    public class WarehouseViewModel
    {
        public int Id { get; set; }

        [DisplayName("Name")]
        public string WarehouseName { get; set; }

        [DisplayName("Managers full name")]
        public string ManagerFullName { get; set; }

        [DisplayName("Creation date")]
        public DateTime DateCreate { get; set; }

        public Dictionary<int, (string, int)> WarehouseComponents { get; set; }
    }
}
