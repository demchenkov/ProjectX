using System;
using System.Linq;
using System.Threading.Tasks;
using Core.Exceptions.Account;
using Core.Interfaces.Services;
using Core.Models.Account;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<IdentityUser> _userManager;

        public UserService(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }


        public async Task RegisterUser(RegisterModel model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user != null)
                throw new UserAlreadyExistException();

            user = new IdentityUser(model.UserName)
            {
                Email = model.Email,
                EmailConfirmed = false
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                throw new CannotCreateUserException(string.Join("\n", result.Errors.Select(x => x.Description)));
        }

        public Task ChangeEmail(Guid userId, string email)
        {
            throw new NotImplementedException();
        }

        public Task ConfirmEmail(Guid userId, string code)
        {
            throw new NotImplementedException();
        }
    }
}