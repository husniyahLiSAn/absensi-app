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
    public class CountryRepository : ICountryRepository
    {
        DynamicParameters parameters = new DynamicParameters();
        public readonly ConnectionStrings _connectionString;

        public CountryRepository(ConnectionStrings connectionStrings)
        {
            this._connectionString = connectionStrings;
        }

        public int Create(CountryVM countryVM)
        {
            parameters.Add("name", countryVM.Name);
            using (var connection = new SqlConnection(_connectionString.Value))
            {
                var items = connection.Execute("SP_InsertCountry",
                    parameters, commandType: CommandType.StoredProcedure);
                return items;
            }
        }

        public int Delete(int id)
        {
            parameters.Add("@Id", id);
            using (var connection = new SqlConnection(_connectionString.Value))
            {
                var items = connection.Execute("SP_DeleteCountry",
                    parameters, commandType: CommandType.StoredProcedure);
                return items;
            }
        }

        public async Task<IEnumerable<CountryVM>> Get()
        {
            using (var connection = new SqlConnection(_connectionString.Value))
            {
                var items = await connection.QueryAsync<CountryVM>("SP_GetCountry");
                return items;
            }
        }

        public async Task<IEnumerable<CountryVM>> Get(int id)
        {
            parameters.Add("id", id);
            using (var connection = new SqlConnection(_connectionString.Value))
            {
                var items = await connection.QueryAsync<CountryVM>("SP_GetCountryId",
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
                    result.dataCountries = await connection.QueryAsync<CountryVM>("SP_PagingCountry",
                        parameters, commandType: CommandType.StoredProcedure);
                    result.length = parameters.Get<int>("length");
                    result.filteredLength = parameters.Get<int>("filteredLength");
                    return result;
                }
            }
            catch (Exception) { }
            return result;
        }

        public Task<IEnumerable<CountryVM>> Search(string term)
        {
            throw new NotImplementedException();
        }

        public int Update(CountryVM countryVM)
        {
            parameters.Add("id", countryVM.Id);
            parameters.Add("name", countryVM.Name);
            using (var connection = new SqlConnection(_connectionString.Value))
            {
                var items = connection.Execute("SP_UpdateCountry",
                    parameters, commandType: CommandType.StoredProcedure);
                return items;
            }
        }
    }
}
