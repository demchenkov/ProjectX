using System;
using System.Threading.Tasks;
using Core.Models.Account;

namespace Core.Interfaces.Services
{
    public interface IUserService
    {
        Task RegisterUser(RegisterModel model);
        Task ChangeEmail(Guid userId, string email);
        Task ConfirmEmail(Guid userId, string code);
    }
}