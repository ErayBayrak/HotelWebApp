﻿using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HotelProject.CoreLayer.Utilities.Security.JWT
{
    public class CreateToken : ITokenHelper
    {
        public string TokenCreate()
        {
            var bytes = Encoding.UTF8.GetBytes("aspnetcoreapiapi");

            SymmetricSecurityKey key = new SymmetricSecurityKey(bytes);

            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: "http://localhost",
                audience: "http://localhost",
                notBefore:DateTime.Now,
                expires:DateTime.Now.AddMinutes(3),
                signingCredentials:credentials
                );

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
           return handler.WriteToken(token);
        }

        public string TokenCreateAdmin()
        {
            var bytes = Encoding.UTF8.GetBytes("aspnetcoreapiapi");

            SymmetricSecurityKey key = new SymmetricSecurityKey(bytes);

            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role,"Admin"),
                new Claim(ClaimTypes.Role,"Visitor")
            };

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: "http://localhost",
                audience: "http://localhost",
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddMinutes(3),
                signingCredentials: credentials,
                claims:claims
                );

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            return handler.WriteToken(token);
        }
    }
}
