using Core.Domain.Validators;
using System;
using System.Collections.Generic;

namespace Core.Domain.Entities
{
    public sealed class TransportType : Entity
    {
        public string Name { get; private set; }
        public decimal? Price { get; private set; }
        public string Color { get; private set; }
        public ICollection<Transport> Transports { get; set; }
        public string UserProfileId { get; set; }
        public UserProfile UserProfile { get; set; }

        public TransportType()
        {
        }

        public TransportType(string name, decimal? price, string color)
        {
            Id = Guid.NewGuid().ToString();
            ValidateDomain(name, price, color);
        }

        public TransportType(string id, string name, decimal? price, string color)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(id), "Invalid Id value");
            Id = id;
            ValidateDomain(name, price, color);
        }

        private void ValidateDomain(string name, decimal? price, string color)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(name), "Name is required");
            DomainExceptionValidation.When(name.Length > 30, "Name is too long");
            DomainExceptionValidation.When(string.IsNullOrEmpty(color), "Color is required");
            DomainExceptionValidation.When(price <= 0 || price > 10000, "Invalid Price value");
            DomainExceptionValidation.When(
                color.Length != 7 ||
                !color.StartsWith("#") ||
                color.Split("#").Length > 2, 
                "Invalid Color value, must be like '#FFFFFF'");

            Name = name;
            Price = price;
            Color = color;
            CreatedAt = DateTime.Now;
        }
    }
}
