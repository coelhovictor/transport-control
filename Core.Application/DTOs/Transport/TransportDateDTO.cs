using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Application.DTOs
{
    public class TransportDateDTO
    {
        public DateTime Date { get; set; }

        public string DateMonth { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        [DataType(DataType.Currency)]
        public decimal TotalPrice { get; set; }

        public IEnumerable<TransportDTOGet> Transports { get; set; }
    }
}
