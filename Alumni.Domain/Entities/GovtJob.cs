namespace Alumni.Domain.Entities
{
    public class GovtJob
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Organization { get; set; }
        public string Designation { get; set; }
        public User User { get; set; }
    }
}
