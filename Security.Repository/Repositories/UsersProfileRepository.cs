using Dapper;
using Security.Domain.Aggregates.UsersProfileAggregate;
using Security.Domain.Exceptions;
using System.Data.SqlClient;
using System.Data;

namespace Security.Repository.Repositories
{
    public class UsersProfileRepository : IUsersProfileRepository
    {
        readonly string _connectionString = string.Empty;

        public UsersProfileRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<int> Register(UsersProfile usersProfile)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                try
                {
                    var parameters = new DynamicParameters();

                    parameters.Add("@poi_user_id", usersProfile.userId, DbType.Int32, ParameterDirection.Input);
                    parameters.Add("@pii_profile_id", usersProfile.profileId, DbType.Int32, ParameterDirection.Input);
                    parameters.Add("@pii_register_user_id", usersProfile.registerUserId, DbType.Int32, ParameterDirection.Input);
                    parameters.Add("@piv_register_user_fullname", usersProfile.registerUserFullname, DbType.String, ParameterDirection.Input);
                    parameters.Add("@pid_register_datetime", usersProfile.registerDatetime, DbType.DateTime, ParameterDirection.Input);
                    parameters.Add("@pii_update_user_id", usersProfile.updateUserId, DbType.Int32, ParameterDirection.Input);
                    parameters.Add("@piv_update_user_fullname", usersProfile.updateUserFullname, DbType.String, ParameterDirection.Input);
                    parameters.Add("@pid_update_datetime", usersProfile.updateDatetime, DbType.DateTime, ParameterDirection.Input);

                    var result = await connection.ExecuteAsync(@"SECURITY.USERS_PROFILE_insert_update", parameters, commandType: CommandType.StoredProcedure);

                    usersProfile.userId = parameters.Get<int>("@poi_user_id");

                    return usersProfile.userId;
                }
                catch (Exception ex)
                {
                    throw new SecurityBaseException(ex.Message);
                }
            }
        }
    }
}
