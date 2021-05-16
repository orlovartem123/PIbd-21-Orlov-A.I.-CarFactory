using CarFactoryBusinessLogic.Attributes;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CarFactoryBusinessLogic.ViewModels
{
    [DataContract]
    public class CarViewModel
    {
        [Column(title: "Number", width: 100)]
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        [Column(title: "Car name", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string CarName { get; set; }

        [DataMember]
        [Column(title: "Price", width: 100)]
        public decimal Price { get; set; }

        [DataMember]
        [Column(visible: false)]
        public Dictionary<int, (string, int)> CarComponents { get; set; }
    }
}
