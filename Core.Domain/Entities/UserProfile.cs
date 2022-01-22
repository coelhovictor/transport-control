using Core.Domain.Validators;
using System;
using System.Collections.Generic;

namespace Core.Domain.Entities
{
    public sealed class UserProfile : Entity
    {
        public string Email { get; private set; }
        public DateTime? Birthdate { get; private set; }
        public string Profession { get; private set; }
        public string Location { get; private set; }

        public ICollection<Reason> Reasons { get; set; }
        public ICollection<Tag> Tags { get; set; }
        public ICollection<TransportType> TransportTypes { get; set; }
        public ICollection<Transport> Transports { get; set; }

        public UserProfile()
        {
        }

        public UserProfile(string email, DateTime? birth,
            string profession, string location)
        {
            Id = Guid.NewGuid().ToString();
            ValidateDomain(email, birth, profession, location);
        }

        public UserProfile(string id, string email, DateTime? birth,
            string profession, string location)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(id), "Invalid Id value");
            Id = id;
            ValidateDomain(email, birth, profession, location);
        }

        private void ValidateDomain(string email, DateTime? birth,
            string profession, string location)
        { 
            DomainExceptionValidation.When(string.IsNullOrEmpty(email), "Email is Required");
            DomainExceptionValidation.When(!email.Contains("@"), "Invalid Email value");
            if (!string.IsNullOrEmpty(profession)) DomainExceptionValidation.When(profession.Length > 35, "Profession is too long");
            if (!string.IsNullOrEmpty(location)) DomainExceptionValidation.When(location.Length > 35, "Location is too long");

            Email = email;
            Birthdate = birth;
            Profession = profession;
            Location = location;
            CreatedAt = DateTime.Now;
        }
    }
}
