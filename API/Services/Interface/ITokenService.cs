using Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services.Interface
{
    public interface ITokenService
    {
        public TokenVM Get(string email);
        public int Insert(TokenVM tokenVM);
        public int Update(TokenVM tokenVM);
    }
}
