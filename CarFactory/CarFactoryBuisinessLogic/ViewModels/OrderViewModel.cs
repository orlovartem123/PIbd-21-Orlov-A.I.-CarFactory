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

        [Column(title: "Implementer", width: 150)]
        [DataMember]
        public string ImplementerFIO { get; set; }

        [Column(title: "Quantity", width: 100)]
        [DataMember]
        public int Count { get; set; }

        [Column(title: "Sum", width: 50)]
        [DataMember]
        public decimal Sum { get; set; }

        [Column(title: "Status", width: 100)]
        [DataMember]
        public OrderStatus Status { get; set; }

        [Column(title: "Creation date", width: 100, format: "R")]
        [DataMember]
        public DateTime DateCreate { get; set; }

        [Column(title: "Complition date", gridViewAutoSize: GridViewAutoSize.Fill, format: "R")]
        [DataMember]
        public DateTime? DateImplement { get; set; }
    }
}



