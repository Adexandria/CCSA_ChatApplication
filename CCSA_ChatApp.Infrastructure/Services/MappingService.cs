using CCSA_ChatApp.Domain.DTOs.GroupChatDTOs;
using CCSA_ChatApp.Domain.DTOs.UserDTOs;
using CCSA_ChatApp.Domain.DTOs.UserProfileDTOs;
using CCSA_ChatApp.Domain.Models;
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
            return TypeAdapterConfig<User, UsersDTO>.NewConfig().
                Map(dest => dest.FullName, src => $"{src.FirstName} {src.MiddleName} {src.LastName}").Config;
        }

        public static TypeAdapterConfig UserMappingService()
        {
            return TypeAdapterConfig<User, UserDTO>.NewConfig().
                Map(dest => dest.FirstName, src => src.FirstName)
                .Map(dest => dest.MiddleName, src => src.MiddleName)
                .Map(dest => dest.LastName, src => src.LastName)
                .Map(dest => dest.Email, src => src.Email)
                .Map(dest => dest.GroupChats, src => src.GroupChats)
                .Map(dest => dest.UserProfile.Username, src => src.Profile)
                .Config;
        }

        public static TypeAdapterConfig UserProfileMappingService()
        {
            return TypeAdapterConfig<UserDTO, UserProfileDTO>.NewConfig()
                .Map(dest => dest.Username, src => src.UserProfile.Username)
                .Map(dest => dest.Picture, src => src.UserProfile.Picture)
                .Map(dest=>dest.Country,src=>src.UserProfile.Country)
                .Map(dest => dest.GroupChats, src => src.GroupChats)
                .Config;
        }
        public static TypeAdapterConfig UsersProfileMappingService()
        {
            return TypeAdapterConfig<UserProfileDTO, UserProfilesDTO>.NewConfig()
                .Map(dest => dest.Username, src => src.Username)
                .Map(dest => dest.Country, src => src.Country)
                .Map(dest => dest.GroupChats, src => src.GroupChats)
                .Config;
        }

        public static TypeAdapterConfig GroupMappingService()
        {
            return TypeAdapterConfig<GroupChat, GroupChatsDTO>.NewConfig()
                .Map(d=>d.CreatedBy.FullName, src => $"{src.CreatedBy.FirstName} {src.CreatedBy.MiddleName} {src.CreatedBy.LastName}")
                .Map(d=>d.CreatedBy.Email,s=>s.CreatedBy.Email)
                .Config;
        }
    }
}
