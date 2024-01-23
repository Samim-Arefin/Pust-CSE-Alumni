namespace Alumni.Domain.Entities
{
    public class Photo
    {
        public int Id { get; set; }
        public string PhotoPath { get; set; }
        public string? Caption { get; set; }
        public int? EventId { get; set; }
        public int? ActivityId { get; set; }
        public int? NoticeId { get; set; }
        public DateTime? UploadDate { get; set; }
        public Event Event { get; set; }
        public Activity Activity { get; set; }
        public Notice Notice { get; set; }
    }
}
