using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using INFRASTRUCTURE.Data;
using INFRASTRUCTURE.Identity.Constants;
using INFRASTRUCTURE.Identity.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace INFRASTRUCTURE.Identity
{
    public  class IdentitySeed
    {
        public static async Task SeedAsync(AppDbContext ctx,UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            await MigrateAsync(ctx);
            await SeedRolesAsync(roleManager);
            await SeedBasicUsersAsync(userManager);
            await SeedAdminAsync(userManager);
        }
        
        private static async Task MigrateAsync(AppDbContext ctx)
        {
            try
            {
                await ctx.Database.MigrateAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
        private static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            if (!await roleManager.Roles.AnyAsync())
            {
                await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
                await roleManager.CreateAsync(new IdentityRole(Roles.Basic.ToString()));
            }
        }

        private static async Task SeedBasicUsersAsync(UserManager<ApplicationUser> userManager)
        {
            var basicUsers = new List<ApplicationUser>
            {
                new ApplicationUser
                {
                    UserName = "Basic.1@gmail.com",
                    Email = "Basic.1@gmail.com"
                },
                new ApplicationUser
                {
                    UserName = "Basic.2@gmail.com",
                    Email ="Basic.2@gmail.com",
                },
                new ApplicationUser
                {
                    UserName = "Basic.3@gmail.com",
                    Email = "Basic.3@gmail.com",
                },
                new ApplicationUser
                {
                    UserName = "Basic.4@gmail.com",
                    Email = "Basic.4@gmail.com",
                },
                new ApplicationUser
                {
                    UserName = "Basic.5@gmail.com",
                    Email = "Basic.5@gmail.com",
                },
                new ApplicationUser
                {
                    UserName = "Basic.6@gmail.com",
                    Email = "Basic.6@gmail.com",
                },
            };
            
            
            if (!await userManager.Users.AnyAsync())
            {
                foreach (var user in basicUsers)
                {
                    await userManager.CreateAsync(user, AuthConstants.DefaultPassword);
                    await userManager.AddToRoleAsync(user, Roles.Basic.ToString());
                }
            }
            
        }
        
        private static async Task SeedAdminAsync(UserManager<ApplicationUser> userManager)
        {
            var admin = new ApplicationUser
            {
                UserName = "Admin.1@gmail.com",
                Email = "Admin.1@gmail.com",
            };
            
            if (await userManager.Users.AllAsync(u => u.Id != admin.Id))
            {
                var user = await userManager.FindByEmailAsync(admin.Email);
                
                if (user == null)
                {
                    await userManager.CreateAsync(admin, AuthConstants.DefaultPassword);
                    await userManager.AddToRoleAsync(admin, Roles.Admin.ToString());
                    await userManager.AddToRoleAsync(admin, Roles.Basic.ToString());
                }

            }
        }
    }
}