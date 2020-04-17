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
    public class StudentRepository : IStudentRepository
    {
        DynamicParameters parameters = new DynamicParameters();
        public readonly ConnectionStrings _connectionString;

        public StudentRepository(ConnectionStrings connectionStrings)
        {
            this._connectionString = connectionStrings;
        }

        public int Create(StudentVM studentVM)
        {
            parameters.Add("FirstName", studentVM.FirstName);
            parameters.Add("LastName", studentVM.LastName);
            parameters.Add("ClassId", studentVM.ClassId);
            using (var connection = new SqlConnection(_connectionString.Value))
            {
                var items = connection.Execute("SP_InsertStudent",
                    parameters, commandType: CommandType.StoredProcedure);
                return items;
            }
        }

        public int Delete(int id)
        {
            parameters.Add("Id", id);
            using (var connection = new SqlConnection(_connectionString.Value))
            {
                var items = connection.Execute("SP_DeleteStudent",
                    parameters, commandType: CommandType.StoredProcedure);
                return items;
            }
        }

        public async Task<IEnumerable<StudentVM>> Get()
        {
            using (var connection = new SqlConnection(_connectionString.Value))
            {
                var items = await connection.QueryAsync<StudentVM>("SP_GetStudent");
                return items;
            }
        }

        public async Task<IEnumerable<StudentVM>> Get(int id)
        {
            parameters.Add("Id", id);
            using (var connection = new SqlConnection(_connectionString.Value))
            {
                var items = await connection.QueryAsync<StudentVM>("SP_GetStudentId",
                    parameters, commandType: CommandType.StoredProcedure);
                return items;
            }
        }

        public async Task<Paging> Paging(int classId, string keyword, int pageSize, int pageNumber)
        {
            var result = new Paging();
            try
            {
                parameters.Add("classId", classId);
                parameters.Add("keyword", keyword);
                parameters.Add("pageSize", pageSize);
                parameters.Add("pageNumber", pageNumber);
                parameters.Add("length", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parameters.Add("filteredLength", dbType: DbType.Int32, direction: ParameterDirection.Output);
                using (var connection = new SqlConnection(_connectionString.Value))
                {
                    result.dataStudents = await connection.QueryAsync<StudentVM>("SP_PagingStudent",
                        parameters, commandType: CommandType.StoredProcedure);
                    result.length = parameters.Get<int>("length");
                    result.filteredLength = parameters.Get<int>("filteredLength");
                    return result;
                }
            }
            catch (Exception) { }
            return result;
        }

        public async Task<IEnumerable<StudentVM>> Search(string term)
        {
            var items = (IEnumerable<StudentVM>)null;
            try
            {
                parameters.Add("keyword", term);
                using (var connection = new SqlConnection(_connectionString.Value))
                {
                    items = await connection.QueryAsync<StudentVM>("SP_SearchStudent",
                        parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception) { }
            return items;
        }

        public int Update(StudentVM studentVM)
        {
            parameters.Add("id", studentVM.Id);
            parameters.Add("firstName", studentVM.FirstName);
            parameters.Add("lastName", studentVM.LastName);
            using (var connection = new SqlConnection(_connectionString.Value))
            {
                var items = connection.Execute("SP_UpdateStudent",
                    parameters, commandType: CommandType.StoredProcedure);
                return items;
            }
        }
    }
}
