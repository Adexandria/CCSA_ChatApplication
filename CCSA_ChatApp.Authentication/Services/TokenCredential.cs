using CCSA_ChatApp.Domain.DTOs;
using CCSA_ChatApp.Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CCSA_ChatApp.Authentication.Services
{
    public class TokenCredential : ITokenCredential
    {
        private readonly IConfiguration _config;
        public TokenCredential(IConfiguration config)
        {
            _config = config.GetSection("JWTSettings");
        }

        //Generate token for user using refresh token

        public async Task<RefreshTokenDTO> GenerateToken(User currentUser, string refreshToken)
        {
            //Get existing token
            //check if the date has expired
            string token = await GenerateToken(currentUser);
            return new RefreshTokenDTO { AccessToken = token};           
        }
        
        //Generate token for user

        public async Task<string> GenerateToken(User currentUser)
        {
            List<Claim> claims = await GetUserClaim(currentUser);
            JwtSecurityToken tokenOption = GenerateTokenOptions(claims);
            string token = new JwtSecurityTokenHandler().WriteToken(tokenOption);
            return token;
        }
        

        //Generate refreshToken
        public RefreshToken GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                var refreshToken = new RefreshToken
                {
                    Token = Convert.ToBase64String(randomNumber),
                    ExpiryDate = DateTime.Now.AddHours(1)
                };
                return refreshToken;
            }
        }

       

        private async Task<List<Claim>> GetUserClaim(User currentUser)
        { 
            //get user roles
            /*UserRole role = await _user.GetUserRole(currentUser.Username);*/
            //add user claim
            List<Claim> claims = new()
            {
                 /*new Claim(ClaimTypes.Name, $"{currentUser.Username}"),
                 new Claim(ClaimTypes.Role,$"{role.Name}")*/
            };
            claims.Add(new Claim(ClaimTypes.Name, $"{currentUser.FirstName} {currentUser.LastName}"));
            claims.Add(new Claim(ClaimTypes.NameIdentifier,$"{currentUser.Id}"));
            return claims;
        }

        private SigningCredentials GetSigningCredentials()
        {
            var key = Encoding.UTF8.GetBytes(_config.GetSection("securityKey").Value);
            var secret = new SymmetricSecurityKey(key);
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private JwtSecurityToken GenerateTokenOptions(List<Claim> claims)
        {
            var tokenOptions = new JwtSecurityToken(
            issuer: _config.GetSection("validIssuer").Value,
            claims: claims,
            expires: DateTime.Now.AddMinutes(Convert.ToDouble(_config.GetSection("expiryInMinutes").Value)),
            signingCredentials: GetSigningCredentials());
            return tokenOptions;
        }
        
    }
}
