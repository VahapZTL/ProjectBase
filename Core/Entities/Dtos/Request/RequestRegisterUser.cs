namespace Core.Entities.Dtos.Request
{
    public class RequestRegisterUser : IDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long UserTypeId { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
    }
}
