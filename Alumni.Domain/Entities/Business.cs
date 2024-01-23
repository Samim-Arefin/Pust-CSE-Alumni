using Alumni.Domain.Enums;

namespace Alumni.Domain.Entities
{
    public class Business
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public BusinessTypeEnum BusinessType { get; set; }
        public User User { get; set; }
    }
}
