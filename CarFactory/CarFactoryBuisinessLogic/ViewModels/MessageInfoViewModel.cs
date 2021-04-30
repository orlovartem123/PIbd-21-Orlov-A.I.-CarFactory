using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace CarFactoryBusinessLogic.ViewModels
{
    [DataContract]
    public class MessageInfoViewModel
    {
        [DataMember]
        public string MessageId { get; set; }

        [DisplayName("Sender")]
        [DataMember]
        public string SenderName { get; set; }

        [DisplayName("Delivery date")]
        [DataMember]
        public DateTime DateDelivery { get; set; }

        [DisplayName("Subject")]
        [DataMember]
        public string Subject { get; set; }

        [DisplayName("Text")]
        [DataMember]
        public string Body { get; set; }

    }
}
