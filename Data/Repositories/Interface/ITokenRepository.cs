using Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories.Interface
{
    public interface ITokenRepository
    {
        public TokenVM Get(string email);
        public int Insert(TokenVM tokenVM);
        public int Update(TokenVM tokenVM);
    }
}
