namespace Core.Entities.Dtos.Request
{
    public class RequestGetUserRole : IDto
    {
        public long UserId { get; set; }
        public string Email { get; set; }
    }
}
