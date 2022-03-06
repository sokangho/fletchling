using Fletchling.Domain.ApiModels.Requests;

namespace Fletchling.Application.Interfaces.Services;

public interface IJwtService
{
    string CreateJwt(CreateJwtRequest request);
}