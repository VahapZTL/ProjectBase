using Core.Entities.Dtos.Models;

namespace Core.Entities.Dtos.Response
{
    public class ResponseRegisterUser : IDto
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<MenuItems> UserPermissionsList { get; set; }
    }
}
