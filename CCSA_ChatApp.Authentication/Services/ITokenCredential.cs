﻿using CCSA_ChatApp.Domain.DTOs;
using CCSA_ChatApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCSA_ChatApp.Authentication.Services
{
    public interface ITokenCredential
    {
        Task<RefreshTokenDTO> GenerateToken(User currentUser,string refreshToken);
        Task<string> GenerateToken(User currentUser);
        RefreshToken GenerateRefreshToken();
    }
}