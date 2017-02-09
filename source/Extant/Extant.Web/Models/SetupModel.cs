using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Security;
namespace Extant.Web.Models
{
    public class SetupModel

    {
        // Admin user details
        [DisplayName("Adminstrator User Name")]
        [Required]
        public string AdminUserName { get; set; } = "Administrator";

        [DisplayName("Adminstrator Email Address")]
        [EmailAddress]
        [Required]
        public string AdminEmail { get; set; }

        [DisplayName("Adminstrator Password")]
        [MembershipPassword]
        [Required]
        public string AdminPassword { get; set; }

        [DisplayName("Confirm Password")]
        [Compare("AdminPassword", ErrorMessage = "Both password and pasword confirmation must match")]
        [Required]
        public string AdminRepeatPassword { get; set; }


        // System config values
        [DisplayName("Organisation Name")]
        [Required]
        public string OrganisationName { get; set; }

        [DisplayName("Catalogue Name")]
        [Required]
        public string CatalogueName { get; set; }

        [DisplayName("Support Email Address")]
        [EmailAddress]
        [Required]
        public string SupportEmail { get; set; }

        // Mailer Settings
        [DisplayName("Email Server Address")]
        [Required]
        public string MailServerIPAddress { get; set; }

        [DisplayName("Mail User Name")]
        public string MailUsername { get; set; }

        [DisplayName("Mail User Password")]
        public string MailPassword { get; set; }

        [DisplayName("'From' Email Address")]
        [EmailAddress]
        [Required]
        public string FromEmail { get; set; }

        public SetupModel()
        {
            AdminUserName = "Administrator";
            
        }
    }
}