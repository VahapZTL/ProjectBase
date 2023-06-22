namespace Core.Entities.Dtos.Models
{
    public class MenuItems : IDto
    {
        public string MenuName { get; set; }
        public string MenuIconClass { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public long Priorty { get; set; }
        public IList<SubMenuItems> SubMenus { get; set; }
    }
}
