using MediatR;
using Security.Application.Queries.Generics;
using Security.Application.Utility;
using Security.Application.Wrappers;
using Security.Domain.Aggregates.UsersAggregate;

namespace Security.Application.Commands.UsersCommand
{
    public class ChangeUserStateCommand : IRequest<Response<int>>
    {
        public int userId { get; set; }
        public int state { get; set; }
        public int updateUserId { get; set; }
        public string updateUserFullname { get; set; }
    }

    public class ChangeUserStateCommandHandler : IRequestHandler<ChangeUserStateCommand, Response<int>>
    {
        readonly IUsersRepository _iUsersRepository;
        readonly IValuesSettings _iValuesSettings;

        public ChangeUserStateCommandHandler(IUsersRepository iUsersRepository, IValuesSettings iValuesSettings)
        {
            _iUsersRepository = iUsersRepository;
            _iValuesSettings = iValuesSettings;
        }
        public async Task<Response<int>> Handle(ChangeUserStateCommand request, CancellationToken cancellationToken)
        {
            Users users = new Users(request.userId, request.state, request.updateUserId, request.updateUserFullname, DateTime.Now.Peru(_iValuesSettings.GetTimeZone()));

            var result = await _iUsersRepository.ChangeState(users);
            return new Response<int>(result);
        }
    }
}
