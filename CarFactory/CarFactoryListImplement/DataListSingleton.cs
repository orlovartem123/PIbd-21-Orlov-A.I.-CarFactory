using CarFactoryListImplement.Models;
using System.Collections.Generic;

namespace CarFactoryListImplement
{
    public class DataListSingleton
    {
        private static DataListSingleton instance;
        public List<Component> Components { get; set; }
        public List<Order> Orders { get; set; }
        public List<Car> Cars { get; set; }
        public List<Client> Clients { get; set; }
        public List<Implementer> Implementers { get; set; }
        public List<Warehouse> Warehouses { get; set; }

        private DataListSingleton()
        {
            Components = new List<Component>();
            Orders = new List<Order>();
            Cars = new List<Car>();
            Clients = new List<Client>();
            Implementers = new List<Implementer>();
            Warehouses = new List<Warehouse>();
        }

        public static DataListSingleton GetInstance()
        {
            if (instance == null)
            {
                instance = new DataListSingleton();
            }
            return instance;
        }
    }
}
