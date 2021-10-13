using HostelAPI.Utilities.DTOs.Register;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HostelAPI.Model;
using HostelAPI.Utilities.DTOs.User;

namespace HostelAPI.Utilities.DTOs.Mapping
{
    public class UserMapping
    {
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
                PhoneNumber = user.PhoneNumber

            };
        }
        public static AppUser GetUser(RegRequest regRequest)
        {
            return new AppUser()
            {
              
                LastName = regRequest.LastName,
                FirstName = regRequest.FirstName,
                UserName = regRequest.UserName,
                Email = regRequest.Email,
                PhoneNumber = regRequest.PhoneNumber
             


            };
        }
    }
}

