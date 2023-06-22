namespace Core.Entities.Dtos.Request
{
    public class RequestLoginUser : IDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
