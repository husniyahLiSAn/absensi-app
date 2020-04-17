using API.Services.Interface;
using Data.Repositories.Interface;
using Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services
{
    public class StudentService : IStudentService
    {
        public IStudentRepository studentRepository;

        public StudentService(IStudentRepository _studentRepository)
        {
            this.studentRepository = _studentRepository;
        }

        public int Create(StudentVM studentVM)
        {
            return studentRepository.Create(studentVM);
        }

        public int Delete(int id)
        {
            return studentRepository.Delete(id);
        }

        public async Task<IEnumerable<StudentVM>> Get()
        {
            return await studentRepository.Get();
        }

        public async Task<IEnumerable<StudentVM>> Get(int id)
        {
            return await studentRepository.Get(id);
        }

        public async Task<Paging> Paging(int classId, string keyword, int pageSize, int pageNumber)
        {
            return await studentRepository.Paging(classId, keyword, pageSize, pageNumber);
        }

        public async Task<IEnumerable<StudentVM>> Search(string term)
        {
            return await studentRepository.Search(term);
        }

        public int Update(StudentVM studentVM)
        {
            return studentRepository.Update(studentVM);
        }
    }
}
