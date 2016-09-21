using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Quotes.Models
{
    [NotMapped]
    public class UserRoleViewModel : CustomUserRole
    {
        public string UserName { get; set; }
        public string RoleLabel { get; set; }
    }
}