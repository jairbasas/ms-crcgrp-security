using MediatR;
using Security.Application.Queries.Generics;
using Security.Application.Utility;
using Security.Application.Wrappers;
using Security.Domain.Aggregates.UsersAggregate;

namespace Security.Application.Commands.UsersCommand
{
    public class ChangeUserPasswordCommand : IRequest<Response<int>>
    {
        public int userId { get; set; }
        public string password { get; set; }
        public int updateUserId { get; set; }
        public string updateUserFullname { get; set; }
    }

    public class ChangeUserPasswordCommandHandler : IRequestHandler<ChangeUserPasswordCommand, Response<int>>
    {
        readonly IUsersRepository _iUsersRepository;
        readonly IValuesSettings _iValuesSettings;

        public ChangeUserPasswordCommandHandler(IUsersRepository iUsersRepository, IValuesSettings iValuesSettings)
        {
            _iUsersRepository = iUsersRepository;
            _iValuesSettings = iValuesSettings;
        }

        public async Task<Response<int>> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
        {
            Users users = new Users(request.userId, request.password, ResetPasswordOption.Yes, request.updateUserId, request.updateUserFullname, DateTime.Now.Peru(_iValuesSettings.GetTimeZone()));

            var result = await _iUsersRepository.ChangePassword(users);

            return new Response<int>(result);
        }
    }

}
