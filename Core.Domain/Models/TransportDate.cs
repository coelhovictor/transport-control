using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Domain.Models
{
    public sealed class TransportDate
    {
        public DateTime Date { get; set; }
        public string DateMonth { get; private set; }
        public decimal TotalPrice { get; private set; }
        public IEnumerable<Transport> Transports { get; private set; }

        public TransportDate(DateTime date, string dateMonth, IEnumerable<Transport> transports)
        {
            Date = date;
            DateMonth = dateMonth;
            Transports = transports;
            TotalPrice = transports.Sum(x => x.Price.HasValue ? x.Price.Value : 0);
        }
    }
}
