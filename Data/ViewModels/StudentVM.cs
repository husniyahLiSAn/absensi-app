using System;
using System.Collections.Generic;
using System.Text;

namespace Data.ViewModels
{
    public class StudentVM
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public int ClassId { get; set; }
        public string ClassName { get; set; }
        public string Grade { get; set; }
        public int SchoolId { get; set; }
        public string SchoolName { get; set; }
    }
}
