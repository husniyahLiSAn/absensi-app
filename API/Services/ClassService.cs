using Data.Repositories.Interface;
using API.Services.Interface;
using Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services
{
    public class ClassService : IClassService
    {
        public IClassRepository classRepository;

        public ClassService(IClassRepository _classRepository)
        {
            this.classRepository = _classRepository;
        }

        public int Create(ClassVM classVM)
        {
            return classRepository.Create(classVM);
        }

        public int Delete(int id)
        {
            return classRepository.Delete(id);
        }

        public async Task<IEnumerable<ClassVM>> Get()
        {
            return await classRepository.Get();
        }

        public async Task<IEnumerable<ClassVM>> Get(int id)
        {
            return await classRepository.Get(id);
        }

        public async Task<Paging> Paging(string keyword, int pageSize, int pageNumber)
        {
            return await classRepository.Paging(keyword, pageSize, pageNumber);
        }

        public async Task<Paging> PagingbyTeacher(string email, string keyword, int pageSize, int pageNumber)
        {
            return await classRepository.PagingbyTeacher(email, keyword, pageSize, pageNumber);
        }

        public async Task<IEnumerable<ClassVM>> Search(ClassVM classVM)
        {
            return await classRepository.Search(classVM);
        }

        public int Update(ClassVM classVM)
        {
            return classRepository.Update(classVM);
        }
    }
}
