using System;
using System.ComponentModel.DataAnnotations;

namespace Core.WebUI.ViewModels.Transports
{
    public class TimelineViewModel
    {
        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }
    }
}
