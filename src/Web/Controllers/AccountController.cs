using Core.Interfaces.Providers;
using Core.Interfaces.Services;

using Microsoft.AspNetCore.Mvc;

using System.Threading.Tasks;
using AutoMapper;
using Core.Models.Account;
using Web.ViewModels.Account.Requests;
using Web.ViewModels.Account.Responses;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ITokenProvider _tokenProvider;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public AccountController(ITokenProvider tokenProvider, IUserService userService, IMapper mapper)
        {
            _tokenProvider = tokenProvider;
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<LoginResponse> Register([FromBody]RegisterRequest request)
        {
            var registerModel = _mapper.Map<RegisterModel>(request);
            await _userService.RegisterUser(registerModel);
            var token = await _tokenProvider.Authenticate(request.UserName, request.Password);

            return new LoginResponse() { AccessToken = token };
        }

        [HttpPost("login")]
        public async Task<LoginResponse> Login(LoginRequest request)
        {
            var token = await _tokenProvider.Authenticate(request.UserName, request.Password);

            return new LoginResponse() {AccessToken = token};
        }
    }
}