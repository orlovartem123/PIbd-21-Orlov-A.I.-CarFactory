using CarFactoryBusinessLogic.Enums;
using CarFactoryFileImplement.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace CarFactoryFileImplement
{
    public class FileDataListSingleton
    {
        private static FileDataListSingleton instance;
        private readonly string ComponentFileName = "Component.xml";
        private readonly string OrderFileName = "Order.xml";
        private readonly string CarFileName = "Car.xml";
        private readonly string ClientFileName = "Client.xml";
        private readonly string ImplementerFileName = "Implementer.xml";
        private readonly string WarehouseFileName = "Warehouse.xml";
        public List<Component> Components { get; set; }
        public List<Order> Orders { get; set; }
        public List<Car> Cars { get; set; }
        public List<Client> Clients { get; set; }
        public List<Implementer> Implementers { get; set; }
        public List<Warehouse> Warehouses { get; set; }

        private FileDataListSingleton()
        {
            Components = LoadComponents();
            Orders = LoadOrders();
            Cars = LoadCars();
            Clients = LoadClients();
            Implementers = LoadImplementers();
            Warehouses = LoadWarehouses();
        }

        public static FileDataListSingleton GetInstance()
        {
            if (instance == null)
            {
                instance = new FileDataListSingleton();
            }
            return instance;
        }

        ~FileDataListSingleton()
        {
            SaveComponents();
            SaveOrders();
            SaveCars();
            SaveClients();
            SaveImplementers();
            SaveWarehouses();
        }

        private List<Warehouse> LoadWarehouses()
        {
            var list = new List<Warehouse>();
            if (File.Exists(WarehouseFileName))
            {
                XDocument xDocument = XDocument.Load(WarehouseFileName);
                var xElements = xDocument.Root.Elements("Warehouse").ToList();
                foreach (var elem in xElements)
                {
                    var Components = new Dictionary<int, int>();
                    foreach (var component in
                   elem.Element("WarehouseComponents").Elements("WarehouseComponent").ToList())
                    {
                        Components.Add(Convert.ToInt32(component.Element("Key").Value),
                       Convert.ToInt32(component.Element("Value").Value));
                    }
                    list.Add(new Warehouse
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id")?.Value),
                        WarehouseName = elem.Element("WarehouseName")?.Value,
                        ManagerFullName = elem.Element("ManagerFullName")?.Value,
                        DateCreate = Convert.ToDateTime(elem.Element("DateCreate")?.Value),
                        WarehouseComponents = Components
                    });
                }
            }
            return list;
        }

        private List<Component> LoadComponents()
        {
            var list = new List<Component>();
            if (File.Exists(ComponentFileName))
            {
                XDocument xDocument = XDocument.Load(ComponentFileName);
                var xElements = xDocument.Root.Elements("Component").ToList();
                foreach (var elem in xElements)
                {
                    list.Add(new Component
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        ComponentName = elem.Element("ComponentName").Value
                    });
                }
            }
            return list;
        }

        private List<Order> LoadOrders()
        {
            var list = new List<Order>();
            if (File.Exists(OrderFileName))
            {
                XDocument xDocument = XDocument.Load(OrderFileName);
                var xElements = xDocument.Root.Elements("Order").ToList();
                foreach (var elem in xElements)
                {
                    var status = OrderStatus.Accepted;
                    DateTime? dateImplement = null;
                    switch (elem.Element("Status")?.Value)
                    {
                        case "Paid":
                            status = OrderStatus.Paid;
                            dateImplement = Convert.ToDateTime(elem.Element("DateImplement")?.Value);
                            break;
                        case "Running":
                            status = OrderStatus.Running;
                            break;
                        case "Ready":
                            status = OrderStatus.Ready;
                            break;
                    }

                    list.Add(new Order
                    {
                        Id = Convert.ToInt32(elem.Element("Id")?.Value),
                        CarId = Convert.ToInt32(elem.Element("CarId")?.Value),
                        ClientId = Convert.ToInt32(elem.Element("ClientId")?.Value),
                        ImplementerId = Convert.ToInt32(elem.Element("ImplementerId")?.Value),
                        Count = Convert.ToInt32(elem.Element("Count")?.Value),
                        Sum = Convert.ToInt32(elem.Element("Sum")?.Value),
                        DateCreate = Convert.ToDateTime(elem.Element("DateCreate")?.Value),
                        DateImplement = dateImplement,
                        Status = status
                    });
                }
            }
            return list;
        }

        private List<Car> LoadCars()
        {
            var list = new List<Car>();
            if (File.Exists(CarFileName))
            {
                XDocument xDocument = XDocument.Load(CarFileName);
                var xElements = xDocument.Root.Elements("Car").ToList();
                foreach (var elem in xElements)
                {
                    var prodComp = new Dictionary<int, int>();
                    foreach (var component in
                   elem.Element("CarComponents").Elements("CarComponent").ToList())
                    {
                        prodComp.Add(Convert.ToInt32(component.Element("Key").Value),
                       Convert.ToInt32(component.Element("Value").Value));
                    }
                    list.Add(new Car
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        CarName = elem.Element("CarName").Value,
                        Price = Convert.ToDecimal(elem.Element("Price").Value),
                        CarComponents = prodComp
                    });
                }
            }
            return list;
        }

        private List<Client> LoadClients()
        {
            var list = new List<Client>();
            if (File.Exists(ClientFileName))
            {
                XDocument xDocument = XDocument.Load(ClientFileName);
                var xElements = xDocument.Root.Elements("Clients").ToList();
                foreach (var elem in xElements)
                {
                    list.Add(new Client
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        ClientFIO = elem.Element("ClientFIO").Value,
                        Email=elem.Element("Email").Value,
                        Password=elem.Element("Password").Value
                    });
                }
            }
            return list;
        }

        private List<Implementer> LoadImplementers()
        {
            var list = new List<Implementer>();
            if (File.Exists(ImplementerFileName))
            {
                XDocument xDocument = XDocument.Load(ImplementerFileName);
                var xElements = xDocument.Root.Elements("Implementers").ToList();
                foreach (var elem in xElements)
                {
                    list.Add(new Implementer
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        ImplementerFIO = elem.Element("ClientFIO").Value,
                        WorkingTime = Convert.ToInt32(elem.Element("WorkingTime").Value),
                        PauseTime = Convert.ToInt32(elem.Element("PauseTime").Value)
                    });
                }
            }
            return list;
        }

        private void SaveComponents()
        {
            if (Components != null)
            {
                var xElement = new XElement("Components");
                foreach (var component in Components)
                {
                    xElement.Add(new XElement("Component",
                    new XAttribute("Id", component.Id),
                    new XElement("ComponentName", component.ComponentName)));
                }
                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(ComponentFileName);
            }
        }

        private void SaveOrders()
        {
            // прописать логику
            if (Orders != null)
            {
                var xElement = new XElement("Orders");
                foreach (var order in Orders)
                {
                    xElement.Add(new XElement("Order",
                    new XAttribute("Id", order.Id),
                    new XElement("CarId", order.CarId),
                    new XElement("ClientId", order.ClientId),
                    new XElement("ImplementerId", order.ImplementerId),
                    new XElement("Count", order.Count),
                    new XElement("DateCreate", order.DateCreate),
                    new XElement("DateImplement", order.DateImplement),
                    new XElement("Status", order.Status),
                    new XElement("Sum", order.Sum)));
                }
                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(OrderFileName);
            }
        }

        private void SaveCars()
        {
            if (Cars != null)
            {
                var xElement = new XElement("Cars");
                foreach (var car in Cars)
                {
                    var compElement = new XElement("CarComponents");
                    foreach (var component in car.CarComponents)
                    {
                        compElement.Add(new XElement("CarComponent",
                        new XElement("Key", component.Key),
                        new XElement("Value", component.Value)));
                    }
                    xElement.Add(new XElement("Car",
                     new XAttribute("Id", car.Id),
                     new XElement("CarName", car.CarName),
                     new XElement("Price", car.Price),
                     compElement));
                }
                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(CarFileName);
            }
        }

        private void SaveClients()
        {
            if (Clients != null)
            {
                var xElement = new XElement("Clients");
                foreach (var client in Clients)
                {
                    xElement.Add(new XElement("Client",
                    new XAttribute("Id", client.Id),
                    new XElement("ClientFIO", client.ClientFIO),
                    new XElement("Email", client.Email),
                    new XElement("Password", client.Password)));
                }
                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(ClientFileName);
            }
        }

        private void SaveImplementers()
        {
            if (Implementers != null)
            {
                var xElement = new XElement("Implementers");
                foreach (var implementer in Implementers)
                {
                    xElement.Add(new XElement("Implementer",
                    new XAttribute("Id", implementer.Id),
                    new XElement("ImplementerFIO", implementer.ImplementerFIO),
                    new XElement("WorkingTime", implementer.WorkingTime),
                    new XElement("PauseTime", implementer.PauseTime)));
                }
                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(ImplementerFileName);
            }
        }
        
        private void SaveWarehouses()
        {
            if (Warehouses != null)
            {
                var xElement = new XElement("Warehouses");
                foreach (var warehouse in Warehouses)
                {
                    var compElement = new XElement("WarehouseComponents");
                    foreach (var component in warehouse.WarehouseComponents)
                    {
                        compElement.Add(new XElement("WarehouseComponent",
                        new XElement("Key", component.Key),
                        new XElement("Value", component.Value)));
                    }
                    xElement.Add(new XElement("Warehouse",
                     new XAttribute("Id", warehouse.Id),
                     new XElement("WarehouseName", warehouse.WarehouseName),
                     new XElement("ManagerFullName", warehouse.ManagerFullName),
                     new XElement("DateCreate", warehouse.DateCreate),
                     compElement));
                }
                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(WarehouseFileName);
            }
        }
    }
}
