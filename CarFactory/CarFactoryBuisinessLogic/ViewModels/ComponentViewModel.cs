using CarFactoryBusinessLogic.Attributes;

namespace CarFactoryBusinessLogic.ViewModels
{
    public class ComponentViewModel
    {
        [Column(title: "Number", width: 100)]
        public int Id { get; set; }

        [Column(title: "Component name", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string ComponentName { get; set; }
    }
}
