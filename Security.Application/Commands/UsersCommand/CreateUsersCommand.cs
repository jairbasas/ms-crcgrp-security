using MediatR;
using Newtonsoft.Json;
using Security.Application.Queries.Generics;
using Security.Application.Queries.Interfaces;
using Security.Application.Queries.ViewModels;
using Security.Application.Utility;
using Security.Application.Wrappers;
using Security.Domain.Aggregates.UsersAggregate;
using Security.Domain.Exceptions;
using Security.Services.Services.Body;
using Security.Services.Services.Interfaces;
using Security.Services.Services.Response;

namespace Security.Application.Commands.UsersCommand
{
    public class CreateUsersCommand : IRequest<Response<int>>
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
        public int? companyId { get; set; }
        public int? registerUserId { get; set; }
        public string registerUserFullname { get; set; }
        
    }
    public class CreateUsersCommandHandler : IRequestHandler<CreateUsersCommand, Response<int>>
    {
        readonly IUsersRepository _iUsersRepository;
        readonly IValuesSettings _iValuesSettings;
        readonly IUsersQuery _iUsersQuery;
        readonly IEmployeeService _iEmployeeService;

        public CreateUsersCommandHandler(IUsersRepository iUsersRepository, IValuesSettings iValuesSettings, IUsersQuery iUsersQuery, IEmployeeService iEmployeeService)
        {
            _iUsersRepository = iUsersRepository;
            _iValuesSettings = iValuesSettings;
            _iUsersQuery = iUsersQuery;
            _iEmployeeService = iEmployeeService;
        }

        public async Task<Response<int>> Handle(CreateUsersCommand request, CancellationToken cancellationToken)
        {

            var response = await _iUsersQuery.GetBySearch(new UsersRequest());
            var userFound = response.data;
            if (userFound.Where(x => x.email == request.email && x.userId != request.userId).Count() > 0)
                throw new SecurityBaseException($"El email: {request.email}, ya se encuentra registrado");
            else if (userFound.Where(x => x.documentNumber == request.documentNumber && x.userId != request.userId).Count() > 0)
                throw new SecurityBaseException($"El número de documento: {request.documentNumber}, ya se encuentra registrado");
            else
            {

                Users users = new Users(request.userId, request.userName, request.fatherLastName, request.motherLastName, request.documentNumber, request.email, request.login, request.password, request.resetPassword, request.state, request.registerUserId, request.registerUserFullname, DateTime.Now.Peru(_iValuesSettings.GetTimeZone()), request.registerUserId, request.registerUserFullname, DateTime.Now.Peru(_iValuesSettings.GetTimeZone()));

                var result = await _iUsersRepository.Register(users);

                if (request.companyId > 0)
                {
                    CreateCompanyUsers createCompanyUsers = new CreateCompanyUsers 
                    {
                        companyUserId = 0,
                        userId = result,
                        companyId = request.companyId,
                        state = StateEntity.Active,
                        registerUserId = request.registerUserId,
                        registerUserFullname= request.registerUserFullname,
                    };

                    var createResponse = await _iEmployeeService.CreateCompanyUsers<CreateCompanyUsersResponse>(JsonConvert.SerializeObject(createCompanyUsers));

                    if(!createResponse.succeeded) throw new SecurityBaseException(createResponse.message);
                }

                return new Response<int>(result);
            }            
        }
    }
}
