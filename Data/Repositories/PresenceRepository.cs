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
    public class PresenceRepository : IPresenceRepository
    {
        DynamicParameters parameters = new DynamicParameters();
        public readonly ConnectionStrings _connectionString;

        public PresenceRepository(ConnectionStrings connectionStrings)
        {
            this._connectionString = connectionStrings;
        }

        public int Create(PresenceVM presenceVM)
        {
            parameters.Add("Class", presenceVM.ClassId);
            parameters.Add("StudentId", presenceVM.StudentId);
            parameters.Add("Scoring", presenceVM.ScoreId);
            parameters.Add("PresenceDate", presenceVM.PresenceDate);
            using (var connection = new SqlConnection(_connectionString.Value))
            {
                var items = connection.Execute("SP_InsertPresence",
                    parameters, commandType: CommandType.StoredProcedure);
                return items;
            }
        }

        public Task<IEnumerable<PresenceVM>> Get(DateTime startDate, DateTime finishDate)
        {
            throw new NotImplementedException();
        }
    }
}
