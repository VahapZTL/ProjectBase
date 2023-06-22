namespace Core.Entities.Dtos.Models
{
    public class AuthorizationModel : IDto
    {
        public string MenuName { get; set; }
        public IList<MenuItems> UserPermissions { get; set; }
    }
}
