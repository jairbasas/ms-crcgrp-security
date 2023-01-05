using MediatR;
using Security.Application.Queries.Interfaces;
using Security.Application.Queries.ViewModels;
using Security.Application.Wrappers;

namespace Security.Application.Commands.AuthenticationCommand
{
    public class CreateAuthenticationCommand : IRequest<Response<UserAuthenticationViewModel>>
    {
        public string? login { get; set; }
        public string? password { get; set; }
    }

    public class CreateAuthenticationCommandHandler : IRequestHandler<CreateAuthenticationCommand, Response<UserAuthenticationViewModel>>
    {
        readonly IUserAuthenticationQuery _iUserAuthenticationQuery;

        public CreateAuthenticationCommandHandler(IUserAuthenticationQuery iUserAuthenticationQuery)
        {
            _iUserAuthenticationQuery = iUserAuthenticationQuery;
        }

        public async Task<Response<UserAuthenticationViewModel>> Handle(CreateAuthenticationCommand request, CancellationToken cancellationToken)
        {
            return await _iUserAuthenticationQuery.AuthenticationUserToken(new UserAuthenticationRequest()
            {
                login = request.login,
                password = request.password
            });
        }
    }
}
