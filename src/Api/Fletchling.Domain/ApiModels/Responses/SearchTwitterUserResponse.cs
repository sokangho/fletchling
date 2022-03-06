using System.Collections.Generic;

namespace Fletchling.Domain.ApiModels.Responses;

public class SearchTwitterUserResponse
{
    public IEnumerable<TwitterUser> Users { get; set; }
}