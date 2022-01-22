using Core.Domain.Validators;
using System;
using System.Collections.Generic;

namespace Core.Domain.Entities
{
    public sealed class Tag : Entity
    {
        public string Name { get; private set; }
        public string Color { get; private set; }
        public ICollection<TransportTag> TransportTags { get; set; }
        public string UserProfileId { get; set; }
        public UserProfile UserProfile { get; set; }

        public Tag()
        {
        }

        public Tag(string name, string color)
        {
            Id = Guid.NewGuid().ToString();
            ValidateDomain(name, color);
        }

        public Tag(string id, string name, string color)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(id), "Invalid Id value");
            Id = id;
            ValidateDomain(name, color);
        }

        private void ValidateDomain(string name, string color)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(name), "Name is required");
            DomainExceptionValidation.When(name.Length > 30, "Name is too long");
            DomainExceptionValidation.When(string.IsNullOrEmpty(color), "Color is required");
            DomainExceptionValidation.When(
                color.Length != 7 ||
                !color.StartsWith("#") ||
                color.Split("#").Length > 2,
                "Invalid Color value, must be like '#FFFFFF'");
            Name = name;
            Color = color;
            CreatedAt = DateTime.Now;
        }
    }
}
