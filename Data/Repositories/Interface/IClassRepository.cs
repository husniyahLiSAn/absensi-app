using Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Interface
{
    public interface IClassRepository
    {
        Task<Paging> Paging(string keyword, int pageSize, int pageNumber);
        Task<Paging> PagingbyTeacher(string email, string keyword, int pageSize, int pageNumber);
        Task<IEnumerable<ClassVM>> Get();
        Task<IEnumerable<ClassVM>> Get(int id);
        Task<IEnumerable<ClassVM>> Search(ClassVM classVM);
        int Create(ClassVM classVM);
        int Update(ClassVM classVM);
        int Delete(int id);
    }
}
