using CarFactoryBusinessLogic.Attributes;
using System;
using System.Runtime.Serialization;

namespace CarFactoryBusinessLogic.ViewModels
{
    [DataContract]
    public class MessageInfoViewModel
    {
        [DataMember]
        [Column(visible: false)]
        public string MessageId { get; set; }

        [Column(title: "Sender", width: 100)]
        [DataMember]
        public string SenderName { get; set; }

        [Column(title: "Delivery date", width: 100)]
        [DataMember]
        public DateTime DateDelivery { get; set; }

        [Column(title: "Subject", width: 100)]
        [DataMember]
        public string Subject { get; set; }

        [Column(title: "Text", gridViewAutoSize: GridViewAutoSize.Fill)]
        [DataMember]
        public string Body { get; set; }
    }
}
