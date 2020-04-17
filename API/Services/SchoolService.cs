using API.Services.Interface;
using Data.Repositories.Interface;
using Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services
{
    public class SchoolService : ISchoolService
    {
        public ISchoolRepository schoolRepository;

        public SchoolService(ISchoolRepository _schoolRepository)
        {
            this.schoolRepository = _schoolRepository;
        }

        public int Create(SchoolVM schoolVM)
        {
            return schoolRepository.Create(schoolVM);
        }

        public int Delete(int id)
        {
            return schoolRepository.Delete(id);
        }

        public async Task<IEnumerable<SchoolVM>> Get()
        {
            return await schoolRepository.Get();
        }

        public async Task<IEnumerable<SchoolVM>> Get(int id)
        {
            return await schoolRepository.Get(id);
        }

        public async Task<Paging> Paging(string keyword, int pageSize, int pageNumber)
        {
            return await schoolRepository.Paging(keyword, pageSize, pageNumber);
        }

        public async Task<IEnumerable<SchoolVM>> Search(string term)
        {
            return await schoolRepository.Search(term);
        }

        public int Update(SchoolVM schoolVM)
        {
            return schoolRepository.Update(schoolVM);
        }
    }
}
