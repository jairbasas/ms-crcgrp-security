using Dapper;
using Security.Domain.Aggregates.ProfileAggregate;
using Security.Domain.Exceptions;
using System.Data.SqlClient;
using System.Data;

namespace Security.Repository.Repositories
{
    public class ProfileRepository : IProfileRepository
    {
        readonly string _connectionString = string.Empty;

        public ProfileRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<int> Register(Profile profile)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                try
                {
                    var parameters = new DynamicParameters();

                    parameters.Add("@poi_profile_id", profile.profileId, DbType.Int32, ParameterDirection.InputOutput);
                    parameters.Add("@piv_profile_name", profile.profileName, DbType.String, ParameterDirection.Input);
                    parameters.Add("@pii_state", profile.state, DbType.Int32, ParameterDirection.Input);
                    parameters.Add("@pii_system_id", profile.systemId, DbType.Int32, ParameterDirection.Input);
                    parameters.Add("@pii_register_user_id", profile.registerUserId, DbType.Int32, ParameterDirection.Input);
                    parameters.Add("@piv_register_user_fullname", profile.registerUserFullname, DbType.String, ParameterDirection.Input);
                    parameters.Add("@pid_register_datetime", profile.registerDatetime, DbType.DateTime, ParameterDirection.Input);
                    parameters.Add("@pii_update_user_id", profile.updateUserId, DbType.Int32, ParameterDirection.Input);
                    parameters.Add("@piv_update_user_fullname", profile.updateUserFullname, DbType.String, ParameterDirection.Input);
                    parameters.Add("@pid_update_datetime", profile.updateDatetime, DbType.DateTime, ParameterDirection.Input);

                    var result = await connection.ExecuteAsync(@"SECURITY.PROFILE_insert_update", parameters, commandType: CommandType.StoredProcedure);

                    profile.profileId = parameters.Get<int>("@poi_profile_id");

                    return profile.profileId;
                }
                catch (Exception ex)
                {
                    throw new SecurityBaseException(ex.Message);
                }
            }
        }
    }
}
