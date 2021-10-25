using HostelAPI.Utilities.DTOs.Register;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HostelAPI.Model;
using HostelAPI.Utilities.DTOs.User;
using AutoMapper;
using HostelAPI.Utilities.DTOs.Rooms;

namespace HostelAPI.Utilities.DTOs.Mapping
{
    public class UserMapping : Profile
    {
        public UserMapping()
        {
            CreateMap<RoomType, RoomTypeRequestDto>().ReverseMap();
            CreateMap<RoomTypeResponseDto, RoomType>().ReverseMap();
            CreateMap<Room, RoomRequestDto>().ReverseMap();
            CreateMap<RoomResponseDto, Room>().ReverseMap();
        }
        public static UserResponse GetUserResponse(AppUser user)
        {
            return new UserResponse()
            {
                Id = user.Id,
                Lastname = user.LastName,
                Firstname = user.FirstName,
                ImageUrl = user.ImageUrl,
                ImagePublicId = user.ImagePublicId,
                Email = user.Email,
                IsActive = user.IsActive,
              
            

            };
        }
        public static AppUser GetUser(RegRequest regRequest)
        {
            return new AppUser()
            {
              
                LastName = regRequest.LastName,
                FirstName = regRequest.FirstName,
                Email = regRequest.Email,
             
             


            };
        }
    }
}

