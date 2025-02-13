using ChatApp.Application.CQRS.Auth.Commands.Models;
using ChatApp.Application.CQRS.Auth.Commands.Validator;
using ChatApp.Application.Services;
using ChatApp.Application.Utilities;
using ChatApp.Application.Utilities.Class;
using ChatApp.Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace ChatApp.Application.CQRS.Commands.Auth
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserRequest, ApiResponse<LoginUserResponse>>
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenService _tokenService;
        public LoginUserCommandHandler(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, ITokenService tokenService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _tokenService = tokenService;
        }

        public async Task<ApiResponse<LoginUserResponse>> Handle(LoginUserRequest request, CancellationToken cancellationToken)
        {
            var signInResult = await _signInManager.PasswordSignInAsync(request.UserName, request.Password, false, false);
            if (signInResult.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(request.UserName);
                if (user == null)
                {
                    return ResponseHandler.Failure<LoginUserResponse>("User not found.");
                }

                var token = _tokenService.GenerateToken(user.Id, user.UserName);
                var response = new LoginUserResponse { Token = token };
                return ResponseHandler.Success(response, "User logged in successfully.");
            }
            else
            {
                return ResponseHandler.Failure<LoginUserResponse>("Invalid login attempt.");
            }
        }
    }
}
