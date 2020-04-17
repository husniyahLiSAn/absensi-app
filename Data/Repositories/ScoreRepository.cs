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
    public class ScoreRepository : IScoreRepository
    {
        DynamicParameters parameters = new DynamicParameters();
        public readonly ConnectionStrings _connectionString;

        public ScoreRepository(ConnectionStrings connectionStrings)
        {
            this._connectionString = connectionStrings;
        }

        public int Create(ScoreVM scoreVM)
        {
            parameters.Add("name", scoreVM.Note);
            using (var connection = new SqlConnection(_connectionString.Value))
            {
                var items = connection.Execute("SP_InsertScoring",
                    parameters, commandType: CommandType.StoredProcedure);
                return items;
            }
        }

        public int Delete(int id)
        {
            parameters.Add("Id", id);
            using (var connection = new SqlConnection(_connectionString.Value))
            {
                var items = connection.Execute("SP_DeleteScore",
                    parameters, commandType: CommandType.StoredProcedure);
                return items;
            }
        }

        public async Task<IEnumerable<ScoreVM>> Get()
        {
            using (var connection = new SqlConnection(_connectionString.Value))
            {
                var items = await connection.QueryAsync<ScoreVM>("SP_GetScore");
                return items;
            }
        }

        public async Task<IEnumerable<ScoreVM>> Get(int id)
        {
            parameters.Add("id", id);
            using (var connection = new SqlConnection(_connectionString.Value))
            {
                var items = await connection.QueryAsync<ScoreVM>("SP_GetScoreId",
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
                    result.dataScores = await connection.QueryAsync<ScoreVM>("SP_PagingScoring",
                        parameters, commandType: CommandType.StoredProcedure);
                    result.length = parameters.Get<int>("length");
                    result.filteredLength = parameters.Get<int>("filteredLength");
                    return result;
                }
            }
            catch (Exception) { }
            return result;
        }

        public async Task<IEnumerable<ScoreVM>> Search(string term)
        {
            var items = (IEnumerable<ScoreVM>)null;
            try
            {
                parameters.Add("keyword", term);
                using (var connection = new SqlConnection(_connectionString.Value))
                {
                    items = await connection.QueryAsync<ScoreVM>("SP_SearchScore",
                        parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception) { }
            return items;
        }

        public int Update(ScoreVM scoreVM)
        {
            parameters.Add("id", scoreVM.Id);
            parameters.Add("name", scoreVM.Note);
            using (var connection = new SqlConnection(_connectionString.Value))
            {
                var items = connection.Execute("SP_UpdateScore",
                    parameters, commandType: CommandType.StoredProcedure);
                return items;
            }
        }
    }
}
