using Dapper;
using Data.Repositories.Interface;
using Data.ViewModels;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class SchoolRepository : ISchoolRepository
    {
        DynamicParameters parameters = new DynamicParameters();
        public readonly ConnectionStrings _connectionString;

        public SchoolRepository(ConnectionStrings connectionStrings)
        {
            this._connectionString = connectionStrings;
        }

        public int Create(SchoolVM schoolVM)
        {
            parameters.Add("Name", schoolVM.SchoolName);
            parameters.Add("Address", schoolVM.Address);
            parameters.Add("Country", schoolVM.CountryId);
            using (var connection = new SqlConnection(_connectionString.Value))
            {
                var items = connection.Execute("SP_InsertSchool",
                    parameters, commandType: CommandType.StoredProcedure);
                return items;
            }
        }

        public int Delete(int id)
        {
            parameters.Add("@Id", id);
            using (var connection = new SqlConnection(_connectionString.Value))
            {
                var items = connection.Execute("SP_DeleteSchool",
                    parameters, commandType: CommandType.StoredProcedure);
                return items;
            }
        }

        public async Task<IEnumerable<SchoolVM>> Get()
        {
            using (var connection = new SqlConnection(_connectionString.Value))
            {
                var items = await connection.QueryAsync<SchoolVM>("SP_GetSchools");
                return items;
            }
        }

        public async Task<IEnumerable<SchoolVM>> Get(int id)
        {
            parameters.Add("Id", id);
            using (var connection = new SqlConnection(_connectionString.Value))
            {
                var items = await connection.QueryAsync<SchoolVM>("SP_GetSchoolId",
                    parameters, commandType: CommandType.StoredProcedure);
                return items;
            }
        }

        public async Task<Paging> Paging(string keyword, int pageSize, int pageNumber)
        {
            var result = new Paging();
            try
            {
                parameters.Add("keyword", keyword);
                parameters.Add("pageSize", pageSize);
                parameters.Add("pageNumber", pageNumber);
                parameters.Add("length", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parameters.Add("filteredLength", dbType: DbType.Int32, direction: ParameterDirection.Output);
                using (var connection = new SqlConnection(_connectionString.Value))
                {
                    result.dataSchools = await connection.QueryAsync<SchoolVM>("SP_PagingSchool",
                        parameters, commandType: CommandType.StoredProcedure);
                    result.length = parameters.Get<int>("length");
                    result.filteredLength = parameters.Get<int>("filteredLength");
                    return result;
                }
            }
            catch (Exception) { }
            return result;
        }

        public async Task<IEnumerable<SchoolVM>> Search(string term)
        {
            var items = (IEnumerable<SchoolVM>)null;
            try
            {
                parameters.Add("keyword", term);
                using (var connection = new SqlConnection(_connectionString.Value))
                {
                    items = await connection.QueryAsync<SchoolVM>("SP_SearchSchool",
                        parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception) { }
            return items;
        }

        public int Update(SchoolVM schoolVM)
        {
            parameters.Add("id", schoolVM.SchoolId);
            parameters.Add("name", schoolVM.SchoolName);
            parameters.Add("address", schoolVM.Address);
            using (var connection = new SqlConnection(_connectionString.Value))
            {
                var items = connection.Execute("SP_UpdateSchool",
                    parameters, commandType: CommandType.StoredProcedure);
                return items;
            }
        }
    }
}
