using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CORE.Exceptions;
using INFRASTRUCTURE.Identity.Enums;
using INFRASTRUCTURE.Identity.Models;
using INFRASTRUCTURE.Identity.Services.Abstractions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
    
namespace INFRASTRUCTURE.Identity.Services.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IReadOnlyList<ApplicationUser>> GetAllAsync(CancellationToken cancellationToken = default)
            => await _userManager.Users.ToListAsync(cancellationToken);
        
        public async Task<ApplicationUser> GetUserByEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user is null)
                throw new ResourceNotFoundException("user can not be found");

            return user;
        }
        
        
        public async Task<IReadOnlyList<string>> GetRolesForUserAsync(ApplicationUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            return roles.ToList();
        }
        

        public async Task<bool> DeleteUserAsync(ApplicationUser user)
        {
            var result = await _userManager.DeleteAsync(user);
            return result.Succeeded;
        }
        
        
        public async Task<string> CreateAsync(ApplicationUser user, string password,
            CancellationToken cancellationToken = default)
        {
            await _userManager.CreateAsync(user, password);
            await _userManager.AddToRoleAsync(user,Roles.Basic.ToString());
            return user.Id;
        }

        public async Task<bool> SignInAsync(ApplicationUser user, string password)
        {
            var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
            return result.Succeeded;
        }

        
    }
}