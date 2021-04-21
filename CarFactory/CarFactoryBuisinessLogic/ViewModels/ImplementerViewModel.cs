using System.ComponentModel;

namespace CarFactoryBusinessLogic.ViewModels
{
    public class ImplementerViewModel
    {
        public int Id { get; set; }

        [DisplayName("Implementer name")]
        public string ImplementerFIO { get; set; }

        [DisplayName("Time to order")]
        public int WorkingTime { get; set; }

        [DisplayName("Time to pause")]
        public int PauseTime { get; set; }
    }
}
