using Alumni.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace Alumni.Domain.Entities
{
    public class User : IdentityUser
    {
        public string? FullName { get; set; }
        public string? NickName { get; set; }
        public GenderEnum? Gender { get; set; }
        public string? PhotoPath { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? GithubUrl { get; set; }
        public string? LinkedInUrl { get; set; }
        public string? FacebookUrl { get; set; }
        public string? CurrentAddress { get; set; }
        public string? PermanantAddress { get; set; }
        public string? Session { get; set; }
        public string? Roll { get; set; }
        public BatchEnum? Batch { get; set; }
        public bool? IsCurrentStudent { get; set; }
        public bool? IsRenowned { get; set; }
        public bool? IsUnemployeed { get; set; }
        public StatusEnum Status { get; set; }
        public Freelance? Freelance { get; set; }
        public virtual ICollection<ContactNumber> ContactNumbers { get; set; }
        public virtual ICollection<PostGrad> PostGrads { get; set; }
        public virtual ICollection<Business> Businesses { get; set; }
        public virtual ICollection<GovtJob> GovtJobs { get; set; }
        public virtual ICollection<PrivateJob> PrivateJobs { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
    }
}
