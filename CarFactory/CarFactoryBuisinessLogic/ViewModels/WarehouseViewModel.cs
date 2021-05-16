using System;
using System.Collections.Generic;
using System.ComponentModel;
using CarFactoryBusinessLogic.Attributes;

namespace CarFactoryBusinessLogic.ViewModels
{
    public class WarehouseViewModel
    {
        public int Id { get; set; }

        [DisplayName("Name")]
        [Column(title: "Name", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string WarehouseName { get; set; }

        [DisplayName("Managers full name")]
        [Column(title: "Managers full name", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string ManagerFullName { get; set; }

        [DisplayName("Creation date")]
        [Column(title: "Creation date", width: 100, format: "D")]
        public DateTime DateCreate { get; set; }

        public Dictionary<int, (string, int)> WarehouseComponents { get; set; }
    }
}
