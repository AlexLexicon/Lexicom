﻿using Lexicom.Jwt.Options;
using Lexicom.Jwt.Validators;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Lexicom.Authority;
public abstract class BearerTokenProvider
{
    /// <exception cref="ArgumentNullException"/>
    protected virtual Task<BearerToken> CreateBearerTokenAsync(IEnumerable<Claim> claims, TimeSpan expiresTimeSpan, JwtOptions jwtOptions)
    {
        ArgumentNullException.ThrowIfNull(claims);
        ArgumentNullException.ThrowIfNull(jwtOptions);
        JwtOptionsValidator.ThrowIfNull(jwtOptions.SymmetricSecurityKey);

        //add the jti claim to the top of the claims
        //https://datatracker.ietf.org/doc/html/rfc7519#section-4.1.7
        Guid jti = Guid.NewGuid();
        var jtiClaim = new Claim(JwtRegisteredClaimNames.Jti, jti.ToString().ToLowerInvariant());

        List<Claim> claimsList = claims.ToList();
        claimsList.Insert(0, jtiClaim);
        claims = claimsList;

        byte[] symmetricSecurityKeyBytes = Encoding.ASCII.GetBytes(jwtOptions.SymmetricSecurityKey);

        var subject = new ClaimsIdentity(claims);
        DateTimeOffset expiresDateTimeOffset = DateTimeOffset.UtcNow.Add(expiresTimeSpan);
        var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(symmetricSecurityKeyBytes), SecurityAlgorithms.HmacSha256Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = subject,
            Expires = expiresDateTimeOffset.UtcDateTime,
            SigningCredentials = signingCredentials,
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        SecurityToken securityToken = tokenHandler.CreateToken(tokenDescriptor);
        string bearerTokenValue = tokenHandler.WriteToken(securityToken);

        return Task.FromResult(new BearerToken(jti, expiresDateTimeOffset, bearerTokenValue));
    }

    /// <exception cref="ArgumentNullException"/>
    protected virtual Task<bool> IsBearerTokenValidAsync(string bearerToken, bool validateLifetime, JwtOptions jwtOptions)
    {
        ArgumentNullException.ThrowIfNull(bearerToken);
        ArgumentNullException.ThrowIfNull(jwtOptions);
        JwtOptionsValidator.ThrowIfNull(jwtOptions.SymmetricSecurityKey);

        var tokenHandler = new JwtSecurityTokenHandler();

        byte[] symmetricSecurityKeyBytes = Encoding.ASCII.GetBytes(jwtOptions.SymmetricSecurityKey);

        var symmetricSecurityKey = new SymmetricSecurityKey(symmetricSecurityKeyBytes);

        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = symmetricSecurityKey,
            ValidateIssuer = false,
            ValidateAudience = false,
            RequireExpirationTime = true,
            ValidateLifetime = validateLifetime,
            ClockSkew = jwtOptions.ClockSkew,
        };

        SecurityToken? validatedToken = null;
        try
        {
            tokenHandler.ValidateToken(bearerToken, tokenValidationParameters, out validatedToken);
        }
        catch
        {
            //the token is invalid and 'validatedToken' will remain null 
        }

        bool isValid = validatedToken is not null;

        return Task.FromResult(isValid);
    }
}
