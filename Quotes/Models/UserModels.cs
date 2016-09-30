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
            this.RefCountry = userModel.RefCountry;
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
        public List<string> assignedRoles { get; set; }


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

    public class UserProfileViewModel
    {
        public UserDetailModel User { get; set; } 
        public List<string> Badges { get; set; }
        public List<string> Tags { get; set; }
        public int TotalQuotes { get; set; }
        public int TotalReactions { get; set; }
        public int TotalFollowers { get; set; }
        public int TotalSubs { get; set; }

        public List<CountryModel> CountryList { get; set; }

        public UserProfileViewModel()
        {
            Badges = new List<string>();
            Tags = new List<string>();
        }

    }
}