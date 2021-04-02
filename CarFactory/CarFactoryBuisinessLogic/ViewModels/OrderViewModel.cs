using CarFactoryBusinessLogic.Enums;
using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace CarFactoryBusinessLogic.ViewModels
{
    [DataContract]
    public class OrderViewModel
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int ClientId { get; set; }

        [DataMember]
        public int CarId { get; set; }

        [DataMember]
        [DisplayName("Client")]
        public string ClientFIO { get; set; }

        [DataMember]
        [DisplayName("Car")]
        public string CarName { get; set; }

        [DataMember]
        [DisplayName("Quantity")]
        public int Count { get; set; }

        [DataMember]
        [DisplayName("Sum")]
        public decimal Sum { get; set; }

        [DataMember]
        [DisplayName("Status")]
        public OrderStatus Status { get; set; }

        [DataMember]
        [DisplayName("Creation date")]
        public DateTime DateCreate { get; set; }

        [DataMember]
        [DisplayName("Complition date")]
        public DateTime? DateImplement { get; set; }
    }
}
