using Dapper;
using Data.Models;
using Data.Repositories.Interface;
using Data.ViewModels;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Data.Repositories
{
    public class TokenRepository : ITokenRepository
    {
        DynamicParameters parameters = new DynamicParameters();
        public readonly ConnectionStrings connectionString;

        public TokenRepository(ConnectionStrings _connectionStrings)
        {
            this.connectionString = _connectionStrings;
        }

        public TokenVM Get(string Email)
        {
            var data = new TokenVM();
            try
            {
                parameters.Add("Email", Email);
                const string procedure = "SP_GetToken";
                using (var connection = new SqlConnection(connectionString.Value))
                {
                    data = connection.QuerySingleOrDefault<TokenVM>(procedure, parameters, commandType: CommandType.StoredProcedure);
                    return data;
                }
            }
            catch (Exception) { }
            return data;
        }

        public int Insert(TokenVM tokenVM)
        {
            parameters.Add("Email", tokenVM.Email);
            parameters.Add("AccessToken", tokenVM.AccessToken);
            parameters.Add("ExpirationAccessToken", tokenVM.ExpireToken);
            parameters.Add("RefreshToken", tokenVM.RefreshToken);
            parameters.Add("ExpirationRefreshToken", tokenVM.ExpireRefreshToken);
            const string procedure = "SP_InsertToken";
            using (var connection = new SqlConnection(connectionString.Value))
            {
                var data = connection.Execute(procedure, parameters, commandType: CommandType.StoredProcedure);
                return data;
            }
        }

        public int Update(TokenVM tokenVM)
        {
            parameters.Add("Email", tokenVM.Email);
            parameters.Add("AccessToken", tokenVM.AccessToken);
            parameters.Add("ExpirationAccessToken", tokenVM.ExpireToken);
            parameters.Add("RefreshToken", tokenVM.RefreshToken);
            parameters.Add("ExpirationRefreshToken", tokenVM.ExpireRefreshToken);
            const string procedure = "SP_UpdateToken";
            using (var connection = new SqlConnection(connectionString.Value))
            {
                var data = connection.Execute(procedure, parameters, commandType: CommandType.StoredProcedure);
                return data;
            }
        }
    }
}
