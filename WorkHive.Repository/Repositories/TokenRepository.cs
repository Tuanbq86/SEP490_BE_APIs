﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WorkHive.Data.Models;
using WorkHive.Repositories.IRepositories;

namespace WorkHive.Repositories.Repositories;

public sealed class TokenRepository : ITokenRepository
{
    private readonly IConfiguration _configuration;
    public TokenRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public List<String> DecodeJwtToken(string token)
    {
        var claims = new List<String>();

        var handler = new JwtSecurityTokenHandler(); //use to decode jwt token
        var jwtToken = handler.ReadJwtToken(token); //convert jwt toke to JwtSecurityToken

        //Get Sub being the subject for identification equals userId
        var subInformation = jwtToken.Claims.FirstOrDefault(c => c.Type.Equals(JwtRegisteredClaimNames.Sub))?.Value;
        claims.Add(subInformation!);

        //Get RoleId for user to authorize
        var roleInformation = jwtToken.Claims.FirstOrDefault(c => c.Type.Equals("RoleId"))?.Value;
        claims.Add(roleInformation!);

        return claims;
    }

    public string GenerateJwtToken(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[] {
        new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
        new Claim(JwtRegisteredClaimNames.Email, user.Email),
        new Claim(JwtRegisteredClaimNames.Name, user.Name),
        new Claim("RoleId", user.RoleId.ToString())
    };

        var token = new JwtSecurityToken(_configuration["Jwt:Issuer"]!,
            _configuration["Jwt:Issuer"]!,
            claims,
            expires: DateTime.Now.AddMinutes(120),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string GenerateJwtToken(WorkspaceOwner Owner)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[] {      
        new Claim(JwtRegisteredClaimNames.Sub, Owner.Id.ToString()),
        new Claim(JwtRegisteredClaimNames.Email, Owner.Email),
        new Claim("Phone", Owner.Phone)};

        var token = new JwtSecurityToken(_configuration["Jwt:Issuer"]!,
            _configuration["Jwt:Issuer"]!,
            claims,
            expires: DateTime.Now.AddMinutes(120),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
