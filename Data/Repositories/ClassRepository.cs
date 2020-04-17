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
    public class ClassRepository : IClassRepository
    {
        DynamicParameters parameters = new DynamicParameters();
        public readonly ConnectionStrings _connectionString;

        public ClassRepository(ConnectionStrings connectionStrings)
        {
            this._connectionString = connectionStrings;
        }

        public int Create(ClassVM classVM)
        {
            parameters.Add("Name", classVM.ClassName);
            parameters.Add("Grade", classVM.Grade);
            parameters.Add("Email", classVM.Email);
            parameters.Add("School", classVM.SchoolId);
            using (var connection = new SqlConnection(_connectionString.Value))
            {
                var items = connection.Execute("SP_InsertClass",
                    parameters, commandType: CommandType.StoredProcedure);
                return items;
            }
        }

        public int Delete(int id)
        {
            parameters.Add("@Id", id);
            using (var connection = new SqlConnection(_connectionString.Value))
            {
                var items = connection.Execute("SP_DeleteClass",
                    parameters, commandType: CommandType.StoredProcedure);
                return items;
            }
        }

        public async Task<IEnumerable<ClassVM>> Get()
        {
            using (var connection = new SqlConnection(_connectionString.Value))
            {
                var items = await connection.QueryAsync<ClassVM>("SP_GetClass");
                return items;
            }
        }

        public async Task<IEnumerable<ClassVM>> Get(int id)
        {
            parameters.Add("Id", id);
            using (var connection = new SqlConnection(_connectionString.Value))
            {
                var items = await connection.QueryAsync<ClassVM>("SP_GetClassId",
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
                    result.dataClasses = await connection.QueryAsync<ClassVM>("SP_PagingClassroom",
                        parameters, commandType: CommandType.StoredProcedure);
                    result.length = parameters.Get<int>("length");
                    result.filteredLength = parameters.Get<int>("filteredLength");
                    return result;
                }
            }
            catch (Exception) { }
            return result;
        }

        public async Task<Paging> PagingbyTeacher(string email, string keyword, int pageSize, int pageNumber)
        {
            var result = new Paging();
            try
            {
                parameters.Add("email", email);
                parameters.Add("keyword", keyword);
                parameters.Add("pageSize", pageSize);
                parameters.Add("pageNumber", pageNumber);
                parameters.Add("length", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parameters.Add("filteredLength", dbType: DbType.Int32, direction: ParameterDirection.Output);
                using (var connection = new SqlConnection(_connectionString.Value))
                {
                    result.dataClasses = await connection.QueryAsync<ClassVM>("SP_PagingClassTeacher",
                        parameters, commandType: CommandType.StoredProcedure);
                    result.length = parameters.Get<int>("length");
                    result.filteredLength = parameters.Get<int>("filteredLength");
                    return result;
                }
            }
            catch (Exception) { }
            return result;
        }

        public async Task<IEnumerable<ClassVM>> Search(ClassVM classVM)
        {
            var items = (IEnumerable<ClassVM>)null;
            try
            {
                parameters.Add("keyword", classVM.ClassName);
                parameters.Add("schoolId", classVM.SchoolId);
                using (var connection = new SqlConnection(_connectionString.Value))
                {
                    items = await connection.QueryAsync<ClassVM>("SP_SearchClass",
                        parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception) { }
            return items;
        }

        public int Update(ClassVM classVM)
        {
            parameters.Add("id", classVM.ClassId);
            parameters.Add("name", classVM.ClassName);
            parameters.Add("grade", classVM.Grade);
            parameters.Add("email", classVM.Email);
            using (var connection = new SqlConnection(_connectionString.Value))
            {
                var items = connection.Execute("SP_UpdateClass",
                    parameters, commandType: CommandType.StoredProcedure);
                return items;
            }
        }
    }
}
