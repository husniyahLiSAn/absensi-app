using Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace API.Services.Interface
{
    public interface IScoreService
    {
        Task<Paging> Paging(string keyword, int pageSize, int pageNumber);
        Task<IEnumerable<ScoreVM>> Get();
        Task<IEnumerable<ScoreVM>> Get(int id);
        Task<IEnumerable<ScoreVM>> Search(string term);
        int Create(ScoreVM scoreVM);
        int Update(ScoreVM scoreVM);
        int Delete(int id);
    }
}
