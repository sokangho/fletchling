using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Fletchling.Application.Config;
using Fletchling.Application.Interfaces.Services;
using Fletchling.Domain.ApiModels.Requests;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Fletchling.Application.Services;

public class JwtService : IJwtService
{
    private readonly JwtConfig _jwtConfig;

    public JwtService(IOptions<JwtConfig> jwtConfigOptions)
    {
        _jwtConfig = jwtConfigOptions.Value;
    }
    
    public string CreateJwt(CreateJwtRequest request)
    {
        var signingKey = Encoding.UTF8.GetBytes(_jwtConfig.SigningKey);
        var signingCredentials =
            new SigningCredentials(new SymmetricSecurityKey(signingKey), SecurityAlgorithms.HmacSha256Signature);

        var encryptingKey = Encoding.UTF8.GetBytes(_jwtConfig.EncryptingKey);
        var encryptingCredentials = new EncryptingCredentials(new SymmetricSecurityKey(encryptingKey),
            SecurityAlgorithms.Aes128KW, SecurityAlgorithms.Aes128CbcHmacSha256);

        var claims = BuildClaims(request);

        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Issuer = _jwtConfig.Issuer,
            Audience = _jwtConfig.Audience,
            Expires = DateTime.UtcNow.AddDays(_jwtConfig.TokenValidityInDays),
            SigningCredentials = signingCredentials,
            EncryptingCredentials = encryptingCredentials
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    private List<Claim> BuildClaims(CreateJwtRequest request)
    {
        var claims = new List<Claim>
        {
            new(nameof(request.TwitterUserId), request.TwitterUserId),
            new(nameof(request.AccessToken), request.AccessToken),
            new(nameof(request.RefreshToken), request.RefreshToken)
        };
        return claims;
    }
}