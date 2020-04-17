using Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Interface
{
    public interface ITeacherRepository
    {
        int AddSchool(SchoolVM schoolVM);
        int Create(TeacherVM teacherVM);
        int Delete(TeacherVM teacherVM);
    }
}
