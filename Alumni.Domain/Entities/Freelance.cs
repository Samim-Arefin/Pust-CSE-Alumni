namespace Alumni.Domain.Entities
{
    public class Freelance
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public bool IsActive { get; set; }
        public string WorkingFields { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public User User { get; set; }
    }
}
