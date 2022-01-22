using Core.Domain.Validators;
using System;
using System.Collections.Generic;

namespace Core.Domain.Entities
{
    public sealed class Transport : Entity
    {
        public string Name { get; private set; }
        public decimal? Price { get; private set; }
        public DateTime Date { get; private set; }
        public DateTime? DateArrival { get; private set; }
        public ICollection<TransportTag> TransportTags { get; set; }
        public string TransportTypeId { get; set; }
        public TransportType TransportType { get; set; }
        public string ReasonId { get; set; }
        public Reason Reason { get; set; }
        public string UserProfileId { get; set; }
        public UserProfile UserProfile { get; set; }

        public Transport()
        {
        }

        public Transport(string name, decimal? price, DateTime date, DateTime? dateArrival)
        {
            Id = Guid.NewGuid().ToString();
            ValidateDomain(name, price, date, dateArrival);
        }

        public Transport(string id, string name, decimal? price, DateTime date, DateTime? dateArrival)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(id), "Invalid Id value");
            Id = id;
            ValidateDomain(name, price, date, dateArrival);
        }

        private void ValidateDomain(string name, decimal? price, DateTime date, DateTime? dateArrival)
        {
            if(!string.IsNullOrEmpty(name))
            {
                DomainExceptionValidation.When(name.Length > 30, "Name is too long");
            }
            DomainExceptionValidation.When(price <= 0 || price > 10000, "Invalid Price value");

            Name = name;
            Price = price;
            Date = date;
            DateArrival = dateArrival;
            CreatedAt = DateTime.Now;
        }
    }
}
