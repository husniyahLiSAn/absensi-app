using Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.ViewModels
{
    public class Paging
    {
        public IEnumerable<ClassVM> dataClasses { get; set; }
        public IEnumerable<CountryVM> dataCountries { get; set; }
        public IEnumerable<SchoolVM> dataSchools { get; set; }
        public IEnumerable<StudentVM> dataStudents { get; set; }
        public IEnumerable<ScoreVM> dataScores { get; set; }
        public IEnumerable<TeacherVM> dataTeachers { get; set; }

        public IEnumerable<PresenceVM> dataPresences { get; set; }
        public int length { get; set; }
        public int filteredLength { get; set; }

    }
}
