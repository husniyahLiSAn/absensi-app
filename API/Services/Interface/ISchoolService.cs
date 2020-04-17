using Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace API.Services.Interface
{
    public interface ISchoolService
    {
        Task<Paging> Paging(string keyword, int pageSize, int pageNumber);
        Task<IEnumerable<SchoolVM>> Get();
        Task<IEnumerable<SchoolVM>> Get(int id);
        Task<IEnumerable<SchoolVM>> Search(string term);
        int Create(SchoolVM schoolVM);
        int Update(SchoolVM schoolVM);
        int Delete(int id);
    }
}
