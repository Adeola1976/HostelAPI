using HostelAPI.Database.Context;
using HostelAPI.Model;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostelAPI.Database.Seeding
{
    public class Seeder
    {
        public async static Task Seed(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager, HDBContext hbdContext)
        {
            await hbdContext.Database.EnsureCreatedAsync();

            if (!hbdContext.AppUser.Any())
            {
                List<string> roles = new List<string> { "Admin", "Regular" };
                foreach (var role in roles)
                {
                    await roleManager.CreateAsync(new IdentityRole { Name = role });
                }
            }

            AppUser user = new AppUser
            {
                FirstName = "adeola",
                LastName = "victor",
                Email = "victoradeola618@yahoo.com",
                PhoneNumber = "08108419461",
                UserName = "adeboi"
            };



            var result = await userManager.CreateAsync(user, "Lampard@008");


            if (result.Succeeded)
            {
               
                AppUser AUserModel = await userManager.FindByEmailAsync(user.Email);
                AUserModel.EmailConfirmed = true;
                await userManager.AddToRoleAsync(user, "Admin");
            }




        }
    }
}

