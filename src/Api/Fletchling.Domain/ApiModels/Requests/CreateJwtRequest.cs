using System.ComponentModel.DataAnnotations;

namespace Fletchling.Domain.ApiModels.Requests;

public class CreateJwtRequest
{
    [Required]
    public string TwitterUserId { get; set; }
    
    [Required]
    public string AccessToken { get; set; }
    
    [Required]
    public string RefreshToken { get; set; }
}