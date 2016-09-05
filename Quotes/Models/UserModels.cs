using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Quotes.Models
{
    [NotMapped]
    public class UserDetailModel : ApplicationUser
    {

        public UserDetailModel()
        {

        }
        public UserDetailModel(ApplicationUser userModel)
        {

            this.UserName = userModel.UserName;
            this.Email = userModel.Email;
            this.EmailConfirmed = userModel.EmailConfirmed;
            this.PhoneNumber = userModel.PhoneNumber;
            this.PhoneNumberConfirmed = userModel.PhoneNumberConfirmed;
            this.TwoFactorEnabled = userModel.TwoFactorEnabled;
            this.LockoutEnabled = userModel.LockoutEnabled;
            this.LockoutEndDateUtc = userModel.LockoutEndDateUtc;
            this.AccessFailedCount = userModel.AccessFailedCount;
        }

        //Profiles of the user
        public List<CustomUserRole> assignedRoles { get; set; }
        //Static list of profiles
        public IEnumerable<CustomRole> roleList { get; set; }

        public List<int> selectedRoles
        {
            get { return assignedRoles.Select(x => x.RoleId).ToList(); }
        }

    }
    /*
    public class ProfileModel
    {
        public int PrfId { get; set; }
        public string PrfLabel { get; set; }

    }*/
}