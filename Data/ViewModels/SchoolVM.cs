using System;
using System.Collections.Generic;
using System.Text;

namespace Data.ViewModels
{
    public class SchoolVM
    {
        public int SchoolId { get; set; }
        public string SchoolName { get; set; }
        public string Address { get; set; }
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public string Email { get; set; }
        public string TeacherName { get; set; }
    }
}
