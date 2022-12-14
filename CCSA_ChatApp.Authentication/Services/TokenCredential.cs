using CCSA_ChatApp.Db;
using CCSA_ChatApp.Domain.DTOs;
using CCSA_ChatApp.Domain.DTOs.UserDTOs;
using CCSA_ChatApp.Domain.Models;
using CCSA_ChatApp.Infrastructure.Services;
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
        private readonly IAuth _authRepository;
        public TokenCredential(IConfiguration config, IAuth authRepository)
        {
            _config = config.GetSection("JWTSettings");
            _authRepository = authRepository;
        }

        //Generate token for user using refresh token

        public async Task<RefreshTokenDTO> GenerateToken(User currentUser, string refreshToken)
        {
            var currentToken = await _authRepository.GetExistingToken(currentUser.UserId,refreshToken);
            if (currentToken is not null && currentToken.ExpiryDate > DateTime.Now)
            {
                string accessToken = await GenerateToken(currentUser);
                return new RefreshTokenDTO { AccessToken = accessToken};
            }
            return default;     
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
        public async Task<RefreshToken> GenerateRefreshToken(User currentUser)
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            var refreshToken = new RefreshToken
            {
                TokenId = Guid.NewGuid(),
                Token = Convert.ToBase64String(randomNumber),
                ExpiryDate = DateTime.Now.AddHours(1),
                User = currentUser
            };
            await _authRepository.SaveRefreshToken(refreshToken);
            return refreshToken;
        }

        public string GeneratePasswordResetToken(Guid userId)
        {
            var passwordSecretKey = _config.GetSection("PasswordSecretKey").Value + $" {userId} {DateTime.Now.AddMinutes(5)}";
            var encodedKey = Encoding.ASCII.GetBytes(passwordSecretKey);
            var token = Convert.ToBase64String(encodedKey);
            return token;
        }

        public bool DecodePasswordResetToken(string token, Guid userId) 
        {
            var encodeKey = Convert.FromBase64String(token);
            var decodedKey = Encoding.ASCII.GetString(encodeKey);
            var decodedKeys = decodedKey.Split(' ');
            var date = DateTime.Parse($"{decodedKeys[3]} {decodedKeys[4]}");
            if (date > DateTime.Now && decodedKey.Contains(userId.ToString()))
            {
                return true;
            }
            return false;
        }


        private async Task<List<Claim>> GetUserClaim(User currentUser)
        {
            //get user roles
            IEnumerable <UserRoleDTO> roles = _authRepository.GetUserRole(currentUser.UserId);
            //add user claim
            List<Claim> claims = new();
            foreach (var item in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, item.Role));
            }
            claims.Add(new Claim(ClaimTypes.Name, $"{currentUser.FirstName} {currentUser.MiddleName} {currentUser.LastName}"));
            claims.Add(new Claim(ClaimTypes.Name, currentUser.Profile.Username));
            claims.Add(new Claim(ClaimTypes.Email, currentUser.Email));
            claims.Add(new Claim(ClaimTypes.NameIdentifier,$"{currentUser.UserId}"));
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
