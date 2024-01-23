namespace Alumni.Domain.Entities
{
    public class Notice
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime PublishedDate { get; set; }
        public List<Photo> Photos { get; set; }
    }
}
