using System;
using System.Collections.Generic;
using System.Text;

namespace CarFactoryBusinessLogic.BindingModels
{
    public class WarehouseAddComponentBindingModel
    {
        public int ComponentId { get; set; }

        public int WarehouseId { get; set; }

        public int Count { get; set; }
    }
}
