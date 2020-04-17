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
    public class TeacherRepository : ITeacherRepository
    {
        DynamicParameters parameters = new DynamicParameters();
        public readonly ConnectionStrings _connectionString;

        public TeacherRepository(ConnectionStrings connectionStrings)
        {
            this._connectionString = connectionStrings;
        }

        public int Create(TeacherVM teacherVM)
        {
            parameters.Add("School", teacherVM.SchoolId);
            parameters.Add("Email", teacherVM.Email);
            using (var connection = new SqlConnection(_connectionString.Value))
            {
                var items = connection.Execute("SP_InsertTeacher",
                    parameters, commandType: CommandType.StoredProcedure);
                return items;
            }
        }

        public int AddSchool(SchoolVM schoolVM)
        {
            parameters.Add("Name", schoolVM.SchoolName);
            parameters.Add("Address", schoolVM.Address);
            parameters.Add("Country", schoolVM.CountryId);
            parameters.Add("Email", schoolVM.Email);
            using (var connection = new SqlConnection(_connectionString.Value))
            {
                var items = connection.Execute("SP_InsertTeacherSchool",
                    parameters, commandType: CommandType.StoredProcedure);
                return items;
            }
        }


        public int Delete(TeacherVM teacherVM)
        {
            parameters.Add("schoolId", teacherVM.SchoolId);
            parameters.Add("email", teacherVM.Email);
            using (var connection = new SqlConnection(_connectionString.Value))
            {
                var items = connection.Execute("SP_DeleteTeacher",
                    parameters, commandType: CommandType.StoredProcedure);
                return items;
            }
        }
    }
}
