using CarFactoryBusinessLogic.Attributes;
using System.Runtime.Serialization;

namespace CarFactoryBusinessLogic.ViewModels
{
    [DataContract]
    public class ClientViewModel
    {
        [Column(title: "Number", width: 100)]
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        [Column(title: "Full name", width: 150)]
        public string ClientFIO { get; set; }

        [DataMember]
        [Column(title: "Email", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string Email { get; set; }

        [DataMember]
        [Column(title: "Password", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string Password { get; set; }
    }
}
