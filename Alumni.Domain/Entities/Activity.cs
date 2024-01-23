namespace Alumni.Domain.Entities
{
    public class Activity
    {
        public int Id { get; set; }
        public DateTime PublishedDate { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public ICollection<Photo> Photos { get; set; }
    }
}
