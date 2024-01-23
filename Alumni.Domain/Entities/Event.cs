namespace Alumni.Domain.Entities
{
    public class Event
    {
        public int Id { get; set; }
        public DateTime PublishedDate { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public List<Photo> Photos { get; set; }
    }
}
