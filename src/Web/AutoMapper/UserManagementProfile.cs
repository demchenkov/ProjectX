using AutoMapper;

using Core.Models.Account;

using Web.ViewModels.Account.Requests;

namespace Web.AutoMapper
{
    public class UserManagementProfile : Profile
    {
        public UserManagementProfile()
        {
            CreateMap<RegisterRequest, RegisterModel>();

        }
    }
}
