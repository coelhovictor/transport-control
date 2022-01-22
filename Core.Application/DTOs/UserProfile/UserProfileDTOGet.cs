using Core.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Core.Application.DTOs
{
    public class UserProfileDTOGet
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public DateTime? Birthdate { get; set; }

        public string Profession { get; set; }

        public string Location { get; set; }

        public ICollection<Reason> Reasons { get; set; }

        public ICollection<Tag> Tags { get; set; }

        public ICollection<TransportType> TransportTypes { get; set; }

        public ICollection<Transport> Transports { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
