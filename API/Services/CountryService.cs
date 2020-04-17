using API.Services.Interface;
using Data.Repositories.Interface;
using Data.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Services
{
    public class CountryService : ICountryService
    {
        public ICountryRepository countryRepository;

        public CountryService(ICountryRepository _countryRepository)
        {
            this.countryRepository = _countryRepository;
        }

        public int Create(CountryVM countryVM)
        {
            return countryRepository.Create(countryVM);
        }

        public int Delete(int id)
        {
            return countryRepository.Delete(id);
        }

        public async Task<IEnumerable<CountryVM>> Get()
        {
            return await countryRepository.Get();
        }

        public async Task<IEnumerable<CountryVM>> Get(int id)
        {
            return await countryRepository.Get(id);
        }

        public async Task<Paging> Paging(string keyword, int pageSize, int pageNumber)
        {
            return await countryRepository.Paging(keyword, pageSize, pageNumber);
        }

        public async Task<IEnumerable<CountryVM>> Search(string term)
        {
            return await countryRepository.Search(term);
        }

        public int Update(CountryVM countryVM)
        {
            return countryRepository.Update(countryVM);
        }
    }
}
