using API.Services.Interface;
using Data.Repositories.Interface;
using Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services
{
    public class TokenService : ITokenService
    {
        private ITokenRepository _tokenRepository;
        public TokenService() { }

        public TokenService(ITokenRepository tokenRepository)
        {
            _tokenRepository = tokenRepository;
        }

        public TokenVM Get(string email)
        {
            return _tokenRepository.Get(email);
        }

        public int Insert(TokenVM tokenVM)
        {
            return _tokenRepository.Insert(tokenVM);
        }

        public int Update(TokenVM tokenVM)
        {
            return _tokenRepository.Update(tokenVM);
        }
    }
}
