using Dapper;
using Security.Domain.Aggregates.ProfileMenuAggregate;
using Security.Domain.Exceptions;
using System.Data.SqlClient;
using System.Data;

namespace Security.Repository.Repositories
{
    public class ProfileMenuRepository : IProfileMenuRepository
    {
        readonly string _connectionString = string.Empty;

        public ProfileMenuRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<int> Register(ProfileMenu profileMenu)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                try
                {
                    var parameters = new DynamicParameters();

                    parameters = getParameters(profileMenu);
                    var result = await connection.ExecuteAsync(@"SECURITY.PROFILE_MENU_insert_update", parameters, commandType: CommandType.StoredProcedure);

                    profileMenu.profileId = parameters.Get<int>("@poi_profile_id");

                    return profileMenu.profileId;
                }
                catch (Exception ex)
                {
                    throw new SecurityBaseException(ex.Message);
                }
            }
        }

        public async Task<int> RegisterAsync(ProfileMenu profileMenu)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        await DeleteByProfileAsync(profileMenu.profileId, connection, transaction);
                        var parameters = new DynamicParameters();
                        int result = 0;

                        if (profileMenu.menuIds.Length > 0)
                        {
                            for (int i = 0; i < profileMenu.menuIds.Length; i++)
                            {
                                profileMenu.menuId = profileMenu.menuIds[i];
                                parameters = getParameters(profileMenu);
                                result = await connection.ExecuteAsync(@"SECURITY.PROFILE_MENU_insert_update", parameters, transaction, commandType: CommandType.StoredProcedure);
                            }
                        }
                        
                        transaction.Commit();
                        return result;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw new SecurityBaseException(ex.Message);
                    }
                }
            }
        }

        public async Task<int> DeleteByProfileAsync(int profileId, SqlConnection connection, SqlTransaction transaction) 
        {
            var parameters = new DynamicParameters();

            parameters.Add("@pii_profile_id", profileId, DbType.Int32, ParameterDirection.Input);
            return await connection.ExecuteAsync(@"SECURITY.PROFILE_MENU_delete_by_profile", parameters, transaction, commandType: CommandType.StoredProcedure);
        }

        #region Methods

        public DynamicParameters getParameters(ProfileMenu profileMenu) 
        {
            var parameters = new DynamicParameters();

            parameters.Add("@poi_profile_id", profileMenu.profileId, DbType.Int32, ParameterDirection.InputOutput);
            parameters.Add("@pii_menu_id", profileMenu.menuId, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@pii_register_user_id", profileMenu.registerUserId, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@piv_register_user_fullname", profileMenu.registerUserFullname, DbType.String, ParameterDirection.Input);
            parameters.Add("@pid_register_datetime", profileMenu.registerDatetime, DbType.DateTime, ParameterDirection.Input);
            parameters.Add("@pii_update_user_id", profileMenu.updateUserId, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@piv_update_user_fullname", profileMenu.updateUserFullname, DbType.String, ParameterDirection.Input);
            parameters.Add("@pid_update_datetime", profileMenu.updateDatetime, DbType.DateTime, ParameterDirection.Input);

            return parameters;
        }

        #endregion

    }
}
