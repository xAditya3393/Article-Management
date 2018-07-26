using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApplication6_Project.Models;

namespace WebApplication6_Project.Areas.Admin.ViewModels
{

    public class RoleCheckBox
    {
        public int Id { get; set; }
        public bool IsChecked { get; set; }
        public string Name { get; set; }

    }



    public class UsersIndex
    {
        public IEnumerable<User> Users { get; set; }
    }

    public class UserNew
    {

        public IList<RoleCheckBox> Roles { get; set; }

        [Required, MaxLength(128)]
        public string UserName { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [Required, MaxLength(256), DataType(DataType.EmailAddress)]
        public string Email { get; set; }

    }


    public class UserEdit
    {

        public IList<RoleCheckBox> Roles { get; set; }

        [Required, MaxLength(128)]
        public string UserName { get; set; }

        [Required, MaxLength(256), DataType(DataType.EmailAddress)]
        public string Email { get; set; }

    }


    public class UserResetPassword
    {

        public string Username { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

    }
    
}