using CarFactoryBusinessLogic.Enums;
using System;
using CarFactoryBusinessLogic.Attributes;
using System.Runtime.Serialization;

namespace CarFactoryBusinessLogic.ViewModels
{
    /// <summary>
    /// Заказ
    /// </summary>
    [DataContract]
    public class OrderViewModel
    {
        [Column(title: "Number", gridViewAutoSize: GridViewAutoSize.Fill)]
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int ClientId { get; set; }

        [DataMember]
        public int CarId { get; set; }

        [DataMember]
        public int? ImplementerId { get; set; }

        [Column(title: "Client", gridViewAutoSize: GridViewAutoSize.Fill)]
        [DataMember]
        public string ClientFIO { get; set; }

        [Column(title: "Car", gridViewAutoSize: GridViewAutoSize.Fill)]
        [DataMember]
        public string CarName { get; set; }

        [Column(title: "Implementer", gridViewAutoSize: GridViewAutoSize.Fill)]
        [DataMember]
        public string ImplementerFIO { get; set; }

        [Column(title: "Quantity", gridViewAutoSize: GridViewAutoSize.Fill)]
        [DataMember]
        public int Count { get; set; }

        [Column(title: "Sum", gridViewAutoSize: GridViewAutoSize.Fill)]
        [DataMember]
        public decimal Sum { get; set; }

        [Column(title: "Status", gridViewAutoSize: GridViewAutoSize.Fill)]
        [DataMember]
        public OrderStatus Status { get; set; }

        [Column(title: "Creation date", gridViewAutoSize: GridViewAutoSize.Fill)]
        [DataMember]
        public DateTime DateCreate { get; set; }

        [Column(title: "Complition date", gridViewAutoSize: GridViewAutoSize.Fill)]
        [DataMember]
        public DateTime? DateImplement { get; set; }
    }
}



