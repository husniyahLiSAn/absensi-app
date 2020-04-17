using Data.ViewModels;
using Microsoft.AspNetCore.Identity;
using System;

namespace Data.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public bool IsDelete { get; set; }
        public DateTimeOffset CreateDate { get; set; }
        public Nullable<DateTimeOffset> UpdateDate { get; set; }
        public Nullable<DateTimeOffset> DeleteDate { get; set; }

        public User() { }

        public User(UserVM userVM)
        {
            FirstName = userVM.FirstName;
            LastName = userVM.LastName;
            UserName = userVM.UserName;
            Password = userVM.Password;
            Email = userVM.Email;

            CreateDate = DateTime.Now;
            IsDelete = false;
        }
    }
}
