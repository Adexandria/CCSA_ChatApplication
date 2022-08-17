using CCSA_ChatApp.Domain.DTOs.UserDTOs;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCSA_ChatApp.Infrastructure.Services
{
    public class MappingService
    {
        public static TypeAdapterConfig UsersMappingService()
        {
            return TypeAdapterConfig<UserDTO, UsersDTO>.NewConfig().
                Map(dest => dest.FullName, src => $"{src.FirstName} {src.MiddleName} {src.LastName}").Config;
        }
    }
}
