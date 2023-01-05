using MediatR;
using Security.Application.Queries.Generics;
using Security.Application.Utility;
using Security.Application.Wrappers;
using Security.Domain.Aggregates.UsersAggregate;

namespace Security.Application.Commands.UsersCommand
{
    public class UpdateUsersCommand : IRequest<Response<int>>
    {
        public int userId { get; set; }
        public string userName { get; set; }
        public string fatherLastName { get; set; }
        public string motherLastName { get; set; }
        public string documentNumber { get; set; }
        public string email { get; set; }
        public string login { get; set; }
        public string? password { get; set; }
        public int? resetPassword { get; set; }
        public int? state { get; set; }
        public int? updateUserId { get; set; }
        public string updateUserFullname { get; set; }
    }

    public class UpdateUsersCommandHandler : IRequestHandler<UpdateUsersCommand, Response<int>>
    {
        readonly IUsersRepository _iUsersRepository;

        readonly IValuesSettings _iValuesSettings;

        public UpdateUsersCommandHandler(IUsersRepository iUsersRepository, IValuesSettings iValuesSettings)
        {
            _iUsersRepository = iUsersRepository;
            _iValuesSettings = iValuesSettings;
        }

        public async Task<Response<int>> Handle(UpdateUsersCommand request, CancellationToken cancellationToken)
        {
            Users users = new Users(request.userId, request.userName, request.fatherLastName, request.motherLastName, request.documentNumber, request.email, request.login, request.password, request.resetPassword, request.state, request.updateUserId, request.updateUserFullname, DateTime.Now.Peru(_iValuesSettings.GetTimeZone()), request.updateUserId, request.updateUserFullname, DateTime.Now.Peru(_iValuesSettings.GetTimeZone()));

            var result = await _iUsersRepository.Register(users);

            return new Response<int>(result);
        }
    }
}
