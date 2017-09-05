using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Security;
using Extant.Data.Entities;
using Extant.Web.Infrastructure;

namespace Extant.Web.Models
{

    public class ChangePasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [System.ComponentModel.DataAnnotations.Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class LogOnModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterModel
    {
        [Required]
        [Display(Name = "Name")]
        [HelpText("Your full name")]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email address")]
        [HelpText("Your email address; you will use this to login with")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Affiliated Institution")]
        [HelpText("The institution you are affiliated with; this will help us approve your registration")]
        public string Affiliation { get; set; }

        [Required]
        [Display(Name = "Disease area")]
        [HelpText("Your primary Disease Area; if you are involved in more than one Disease Area select the one which you work in most of the time")]
        public int DiseaseAreaId { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 10)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [MembershipPassword]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        

        [RegularExpression("True", ErrorMessage = "Please confirm that you have read and accept the privacy policy.")]
        [Display(Name = "Accept Policies")]
        public bool AcceptTerms { get; set; }

        public IEnumerable<DiseaseArea> DiseaseAreas { get; set; }
    }
}
