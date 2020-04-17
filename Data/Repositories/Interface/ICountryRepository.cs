using Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Interface
{
    public interface ICountryRepository
    {
        Task<Paging> Paging(string keyword, int pageSize, int pageNumber);
        Task<IEnumerable<CountryVM>> Get();
        Task<IEnumerable<CountryVM>> Get(int id);
        Task<IEnumerable<CountryVM>> Search(string term);
        int Create(CountryVM countryVM);
        int Update(CountryVM countryVM);
        int Delete(int id);
    }
}
