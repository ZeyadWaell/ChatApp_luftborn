using ChatApp.Application.CQRS.Auth.Commands.Response;
using MediatR;


namespace ChatApp.Application.CQRS.Auth.Commands.Models
{
    public class RegisterUserRequest : IRequest<RegisterUserResponse>
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public string NikName { get; set; }
    }
}
