using Core.Entities.Dtos.Models;
using Core.Utilities.Security.Jwt;

namespace Core.Entities.Dtos.Response
{
    public class ResponseLoginUser : IDto
    {
        public long Id { get; set; }
        public long UserTypeId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public AccessToken AccessToken { get; set; }
        public List<AuthorizationModel> Authorization { get; set; }
    }
}
