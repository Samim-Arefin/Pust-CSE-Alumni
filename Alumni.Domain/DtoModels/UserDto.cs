using Alumni.Domain.Enums;

namespace Alumni.Domain.DtoModels
{
    public class UserDto
    {
        public string Id { get; set; } 
        public string UserName { get; set; }
        public string Email { get; set; }
        public string UserRole { get; set; }
        public BatchEnum Batch { get; set; }
        public string Session {  get; set; }
        public StatusEnum Status { get; set; }
    }
}
