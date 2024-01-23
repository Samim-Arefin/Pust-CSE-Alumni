namespace Alumni.Domain.Entities
{
    public class ContactNumber
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string PhoneNumber { get; set; }
        public virtual User User { get; set; }
    }
}
