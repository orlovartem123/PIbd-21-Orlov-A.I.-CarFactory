using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace CarFactoryBusinessLogic.ViewModels
{
    [DataContract]
    public class CarViewModel
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        [DisplayName("Car name")]
        public string CarName { get; set; }

        [DataMember]
        [DisplayName("Price")]
        public decimal Price { get; set; }

        [DataMember]
        public Dictionary<int, (string, int)> CarComponents { get; set; }
    }
}
