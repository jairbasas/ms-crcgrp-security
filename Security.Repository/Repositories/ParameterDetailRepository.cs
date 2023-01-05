using Dapper;
using Security.Domain.Aggregates.ParameterDetailAggregate;
using Security.Domain.Exceptions;
using System.Data.SqlClient;
using System.Data;

namespace Security.Repository.Repositories
{
    public class ParameterDetailRepository : IParameterDetailRepository
    {
        readonly string _connectionString = string.Empty;

        public ParameterDetailRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<int> Register(ParameterDetail parameterDetail)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                try
                {
                    var parameters = new DynamicParameters();

                    parameters.Add("@poi_parameter_detail_id", parameterDetail.parameterDetailId, DbType.Int32, ParameterDirection.InputOutput);
                    parameters.Add("@piv_description", parameterDetail.description, DbType.String, ParameterDirection.Input);
                    parameters.Add("@piv_field_value_1", parameterDetail.fieldValue1, DbType.String, ParameterDirection.Input);
                    parameters.Add("@piv_field_description_1", parameterDetail.fieldDescription1, DbType.String, ParameterDirection.Input);
                    parameters.Add("@piv_field_value_2", parameterDetail.fieldValue2, DbType.String, ParameterDirection.Input);
                    parameters.Add("@piv_field_description_2", parameterDetail.fieldDescription2, DbType.String, ParameterDirection.Input);
                    parameters.Add("@piv_field_value_3", parameterDetail.fieldValue3, DbType.String, ParameterDirection.Input);
                    parameters.Add("@piv_field_description_3", parameterDetail.fieldDescription3, DbType.String, ParameterDirection.Input);
                    parameters.Add("@pii_parameter_id", parameterDetail.parameterId, DbType.Int32, ParameterDirection.Input);

                    var result = await connection.ExecuteAsync(@"SECURITY.PARAMETER_DETAIL_insert_update", parameters, commandType: CommandType.StoredProcedure);

                    parameterDetail.parameterDetailId = parameters.Get<int>("@poi_parameter_detail_id");

                    return parameterDetail.parameterDetailId;
                }
                catch (Exception ex)
                {
                    throw new SecurityBaseException(ex.Message);
                }
            }
        }
    }
}
