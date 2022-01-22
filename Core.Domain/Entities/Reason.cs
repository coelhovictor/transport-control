using Core.Domain.Validators;
using System;
using System.Collections.Generic;

namespace Core.Domain.Entities
{
    public sealed class Reason : Entity
    {
        public string Name { get; private set; }
        public ICollection<Transport> Transports { get; set; }
        public string UserProfileId { get; set; }
        public UserProfile UserProfile { get; set; }

        public Reason()
        {
        }

        public Reason(string name)
        {
            Id = Guid.NewGuid().ToString();
            ValidateDomain(name);
        }

        public Reason(string id, string name)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(id), "Invalid Id value");
            Id = id;
            ValidateDomain(name);
        }

        private void ValidateDomain(string name)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(name), "Name is required");
            DomainExceptionValidation.When(name.Length > 30, "Invalid Name value");

            Name = name;
            CreatedAt = DateTime.Now;
        }
    }
}
