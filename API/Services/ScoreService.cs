using API.Services.Interface;
using Data.Repositories.Interface;
using Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services
{
    public class ScoreService : IScoreService
    {
        public IScoreRepository scoreRepository;

        public ScoreService(IScoreRepository _scoreRepository)
        {
            this.scoreRepository = _scoreRepository;
        }

        public int Create(ScoreVM scoreVM)
        {
            return scoreRepository.Create(scoreVM);
        }

        public int Delete(int id)
        {
            return scoreRepository.Delete(id);
        }

        public async Task<IEnumerable<ScoreVM>> Get()
        {
            return await scoreRepository.Get();
        }

        public async Task<IEnumerable<ScoreVM>> Get(int id)
        {
            return await scoreRepository.Get(id);
        }

        public async Task<Paging> Paging(string keyword, int pageSize, int pageNumber)
        {
            return await scoreRepository.Paging(keyword, pageSize, pageNumber);
        }

        public async Task<IEnumerable<ScoreVM>> Search(string term)
        {
            return await scoreRepository.Search(term);
        }

        public int Update(ScoreVM scoreVM)
        {
            return scoreRepository.Update(scoreVM);
        }
    }
}
