using Dapper;
using Security.Application.Queries.ViewModels.Base;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlClient;
using System.Data;
using System.Reflection;

namespace Security.Application.Queries.Generics
{
    public interface IGenericQuery
    {
        Task<IEnumerable<T>> ExecuteDirect<T>(string procedure);
        Task<IEnumerable<T>> ExecuteDirect<T>(string procedure, DynamicParameters parameters);
        Task<dynamic> Search(string procedure, string parametersXml);
        Task<IEnumerable<dynamic>> Search(string procedure, string parametersXml, Pagination pagination);
        Task<IEnumerable<dynamic>> FindAll(string procedure, string parametersXml, Pagination pagination);
        Task<IEnumerable<T>> FindAll<T>(string procedure, string parametersXml, Pagination pagination);
    }

    public class GenericQuery : IGenericQuery
    {
        readonly string _connectionString;

        public GenericQuery(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public async Task<IEnumerable<T>> ExecuteDirect<T>(string procedure)
        {
            IEnumerable<T> result;
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                result = await connection.QueryAsync<T>(procedure, commandType: CommandType.StoredProcedure);
            }
            return result;
        }

        public async Task<IEnumerable<T>> ExecuteDirect<T>(string procedure, DynamicParameters parameters)
        {
            IEnumerable<T> result;
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                result = await connection.QueryAsync<T>(procedure, parameters, commandType: CommandType.StoredProcedure);
            }
            return result;
        }

        public async Task<dynamic> Search(string procedure, string parametersXml)
        {
            dynamic result;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@pit_parametrosXML", parametersXml, DbType.String);
                parameters.Add("@piv_orderBy", "", DbType.String);

                result = await connection.QueryFirstOrDefaultAsync(procedure, parameters, commandType: CommandType.StoredProcedure);
            }
            return result;
        }

        public async Task<IEnumerable<dynamic>> Search(string procedure, string parametersXml, Pagination pagination)
        {
            IEnumerable<dynamic> result;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@pit_parametrosXML", parametersXml, DbType.String);
                parameters.Add("@piv_orderBy", pagination.sort, DbType.String);

                result = await connection.QueryAsync(procedure, parameters, commandType: CommandType.StoredProcedure);
            }
            return result;
        }

        public async Task<IEnumerable<dynamic>> FindAll(string procedure, string parametersXml, Pagination pagination)
        {
            IEnumerable<dynamic> result;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@pit_parametrosXML", parametersXml, DbType.String);
                parameters.Add("@pii_paginaActual", pagination.pageIndex, DbType.Int32);
                parameters.Add("@pii_cantidadMostrar", pagination.pageSize, DbType.Int32);
                parameters.Add("@piv_orderBy", pagination.sort, DbType.String);
                parameters.Add("@poi_totalRegistros", dbType: DbType.Int32, direction: ParameterDirection.Output);

                result = await connection.QueryAsync(procedure, parameters, commandType: CommandType.StoredProcedure);

                pagination.total = parameters.Get<int>("@poi_totalRegistros");


            }
            return result;
        }
        public async Task<IEnumerable<T>> FindAll<T>(string procedure, string parametersXml, Pagination pagination)
        {
            SetupMapper<T>();
            IEnumerable<T> result;
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@pit_parametrosXML", parametersXml, DbType.String);
                parameters.Add("@pii_paginaActual", pagination.pageIndex, DbType.Int32);
                parameters.Add("@pii_cantidadMostrar", pagination.pageSize, DbType.Int32);
                parameters.Add("@piv_orderBy", GetOrderByExpression<T>(pagination), DbType.String);
                parameters.Add("@poi_totalRegistros", dbType: DbType.Int32, direction: ParameterDirection.Output);

                result = await connection.QueryAsync<T>(procedure, parameters, commandType: CommandType.StoredProcedure);

                pagination.total = parameters.Get<int>("@poi_totalRegistros");
            }
            return result;
        }

        #region Private methods
        private void SetupMapper<T>()
        {
            SqlMapper.SetTypeMap(typeof(T),
                new CustomPropertyTypeMap(typeof(T),
                    (type, columnName) => type.GetProperties().FirstOrDefault(prop =>
                        prop.GetCustomAttributes(false).OfType<ColumnAttribute>()
                            .Any(attr => attr.Name == columnName))));
        }

        private string GetOrderByExpression<T>(Pagination pagination)
        {
            if (pagination.sort == null)
            {
                return string.Empty;
            }

            var sortColumn = GetColumnAttribute<T>(pagination.sort);
            return sortColumn != null ? $"{sortColumn} {pagination.sort ?? "ASC"}" : string.Empty;
        }

        private string GetColumnAttribute<T>(string propertyName)
        {
            propertyName = char.ToUpper(propertyName[0]) + propertyName.Substring(1);
            MemberInfo property = typeof(T).GetProperty(propertyName);

            if (property == null)
            {
                return string.Empty;
            }

            var displayAttributed = property.GetCustomAttribute(typeof(ColumnAttribute)) as ColumnAttribute;

            return displayAttributed?.Name;
        }

        #endregion
    }
}
