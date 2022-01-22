using System;

namespace Core.Domain.Entities
{
    public abstract class Entity
    {
        public string Id { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
