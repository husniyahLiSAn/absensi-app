using Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Interface
{
    public interface IStudentRepository
    {
        Task<Paging> Paging(int classId, string keyword, int pageSize, int pageNumber);
        Task<IEnumerable<StudentVM>> Get();
        Task<IEnumerable<StudentVM>> Get(int id);
        Task<IEnumerable<StudentVM>> Search(string term);
        int Create(StudentVM studentVM);
        int Update(StudentVM studentVM);
        int Delete(int id);
    }
}
