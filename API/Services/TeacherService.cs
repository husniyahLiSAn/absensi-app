using API.Services.Interface;
using Data.Repositories.Interface;
using Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services
{
    public class TeacherService : ITeacherService
    {
        public ITeacherRepository teacherRepository;

        public TeacherService(ITeacherRepository _teacherRepository)
        {
            this.teacherRepository = _teacherRepository;
        }

        public int AddSchool(SchoolVM schoolVM)
        {
            return teacherRepository.AddSchool(schoolVM);
        }

        public int Create(TeacherVM teacherVM)
        {
            return teacherRepository.Create(teacherVM);
        }

        public int Delete(TeacherVM teacherVM)
        {
            return teacherRepository.Delete(teacherVM);
        }
    }
}
