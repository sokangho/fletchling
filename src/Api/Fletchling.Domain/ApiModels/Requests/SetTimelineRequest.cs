using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Fletchling.Domain.ApiModels.Requests
{
    public class SetTimelineRequest
    {
        [Required]
        public string UID { get; set; }
        
        [Required]
        public List<string> Timelines { get; set; }
        
        [Required]
        public string GroupName { get; set; }
    }
}