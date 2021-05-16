using CarFactoryBusinessLogic.Attributes;

namespace CarFactoryBusinessLogic.ViewModels
{
    public class ImplementerViewModel
    {
        [Column(title: "Number", width: 100)]
        public int Id { get; set; }

        [Column(title: "Car", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string ImplementerFIO { get; set; }

        [Column(title: "Time to work", width: 100)]
        public int WorkingTime { get; set; }

        [Column(title: "Time to pause", width: 100)]
        public int PauseTime { get; set; }
    }
}
