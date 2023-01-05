using Dapper;
using Security.Domain.Aggregates.MenuAggregate;
using Security.Domain.Exceptions;
using System.Data.SqlClient;
using System.Data;

namespace Security.Repository.Repositories
{
    public class MenuRepository : IMenuRepository
    {
        readonly string _connectionString = string.Empty;

        public MenuRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<int> Register(Menu menu)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                try
                {
                    var parameters = new DynamicParameters();

                    parameters.Add("@poi_menu_id", menu.menuId, DbType.Int32, ParameterDirection.InputOutput);
                    parameters.Add("@piv_menu_name", menu.menuName, DbType.String, ParameterDirection.Input);
                    parameters.Add("@pii_level", menu.level, DbType.Int32, ParameterDirection.Input);
                    parameters.Add("@piv_url", menu.url, DbType.String, ParameterDirection.Input);
                    parameters.Add("@piv_icon", menu.icon, DbType.String, ParameterDirection.Input);
                    parameters.Add("@pii_order", menu.order, DbType.Int32, ParameterDirection.Input);
                    parameters.Add("@pii_menu_parent_id", menu.menuParentId, DbType.Int32, ParameterDirection.Input);
                    parameters.Add("@pii_state", menu.state, DbType.Int32, ParameterDirection.Input);
                    parameters.Add("@pii_register_user_id", menu.registerUserId, DbType.Int32, ParameterDirection.Input);
                    parameters.Add("@piv_register_user_fullname", menu.registerUserFullname, DbType.String, ParameterDirection.Input);
                    parameters.Add("@pid_register_datetime", menu.registerDatetime, DbType.DateTime, ParameterDirection.Input);
                    parameters.Add("@pii_update_user_id", menu.updateUserId, DbType.Int32, ParameterDirection.Input);
                    parameters.Add("@piv_update_user_fullname", menu.updateUserFullname, DbType.String, ParameterDirection.Input);
                    parameters.Add("@pid_update_datetime", menu.updateDatetime, DbType.DateTime, ParameterDirection.Input);

                    var result = await connection.ExecuteAsync(@"SECURITY.MENU_insert_update", parameters, commandType: CommandType.StoredProcedure);

                    menu.menuId = parameters.Get<int>("@poi_menu_id");

                    return menu.menuId;
                }
                catch (Exception ex)
                {
                    throw new SecurityBaseException(ex.Message);
                }
            }
        }
    }
}
