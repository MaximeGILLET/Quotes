using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Quotes.Models
{
    [NotMapped]
    public sealed class UserDetailModel : ApplicationUser
    {


        public UserDetailModel()
        {

        }
        public UserDetailModel(ApplicationUser userModel)
        {

            this.UserName = userModel.UserName;
            this.Email = userModel.Email;
            this.EmailConfirmed = userModel.EmailConfirmed;
            this.RegistrationDate = userModel.RegistrationDate;
            this.PhoneNumber = userModel.PhoneNumber;
            this.PhoneNumberConfirmed = userModel.PhoneNumberConfirmed;
            this.TwoFactorEnabled = userModel.TwoFactorEnabled;
            this.LockoutEnabled = userModel.LockoutEnabled;
            this.LockoutEndDateUtc = userModel.LockoutEndDateUtc;
            this.AccessFailedCount = userModel.AccessFailedCount;
            this.PasswordHash = userModel.PasswordHash;
            this.SecurityStamp = userModel.SecurityStamp;
            this.Id = userModel.Id;


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

    public class LastRegisterUserViewModel
    {
        public string Username { get; set; }
        public DateTime RegisterDate { get; set; }
        public string Label { get; set; }

    }

    public class TopUserViewModel
    {
        public string Username { get; set; }
        public int Points { get; set; }

    }

    public class TopUserClusterViewModel
    {
        public List<TopUserViewModel> AllTimeUsers { get; set; }
        public List<TopUserViewModel> MonthUsers { get; set; }
        public List<TopUserViewModel> WeekUsers { get; set; }

    }
}