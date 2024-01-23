namespace Alumni.Domain.Entities
{
    public class PostGrad
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string PostGradUniversity { get; set; }
        public string PostGradDegree { get; set; }
        public string PostGradField { get; set; }
        public virtual User User { get; set; }
    }
}
