using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Fletchling.Api.Models
{
    public class TimelineRequest
    {
        [Required]
        public string UID { get; set; }

        [Required]
        public List<string> Timelines { get; set; }   

        [Required]
        public string GroupName { get; set; }

            
    }
}
