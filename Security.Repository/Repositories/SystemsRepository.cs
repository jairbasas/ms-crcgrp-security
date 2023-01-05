using Dapper;
using Security.Domain.Aggregates.SystemsAggregate;
using Security.Domain.Exceptions;
using System.Data.SqlClient;
using System.Data;

namespace Security.Repository.Repositories
{
    public class SystemsRepository : ISystemsRepository
    {
        readonly string _connectionString = string.Empty;

        public SystemsRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<int> Register(Systems systems)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                try
                {
                    var parameters = new DynamicParameters();

                    parameters.Add("@poi_system_id", systems.systemId, DbType.Int32, ParameterDirection.InputOutput);
                    parameters.Add("@piv_system_name", systems.systemName, DbType.String, ParameterDirection.Input);
                    parameters.Add("@pii_state", systems.state, DbType.Int32, ParameterDirection.Input);
                    parameters.Add("@pii_register_user_id", systems.registerUserId, DbType.Int32, ParameterDirection.Input);
                    parameters.Add("@piv_register_user_fullname", systems.registerUserFullname, DbType.String, ParameterDirection.Input);
                    parameters.Add("@pid_register_datetime", systems.registerDatetime, DbType.DateTime, ParameterDirection.Input);
                    parameters.Add("@pii_update_user_id", systems.updateUserId, DbType.Int32, ParameterDirection.Input);
                    parameters.Add("@piv_update_user_fullname", systems.updateUserFullname, DbType.String, ParameterDirection.Input);
                    parameters.Add("@pid_update_datetime", systems.updateDatetime, DbType.DateTime, ParameterDirection.Input);

                    var result = await connection.ExecuteAsync(@"SECURITY.SYSTEMS_insert_update", parameters, commandType: CommandType.StoredProcedure);

                    systems.systemId = parameters.Get<int>("@poi_system_id");

                    return systems.systemId;
                }
                catch (Exception ex)
                {
                    throw new SecurityBaseException(ex.Message);
                }
            }
        }
    }
}
