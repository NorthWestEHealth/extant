using System;
using System.Linq;
using System.Web.Mvc;
using Extant.Web.Models;
using Extant.Web.Infrastructure;
using System.Web.Configuration;
using System.Configuration;
using System.Web.Security;
using Extant.Web.Helpers;
using Extant.Data;

namespace Extant.Web.Controllers
{
    [LocalAccessOnly]
    public class SetupController : Controller
    {
        
        
        // GET: Setup
        public ActionResult Index()
        {
            return View(new SetupModel());
        }

        [HttpPost]
        public ActionResult Complete(SetupModel model)
        {

            if (!ModelState.IsValid) return View("Index", model);

            // extract app settings and check that setup has not been run previously.
            Configuration config = WebConfigurationManager.OpenWebConfiguration("~"); // open application web.config

            var appsettings = config.AppSettings.Settings;

            if (appsettings.AllKeys.Contains("SetupDate")) throw new InvalidOperationException("Extant Setup must only be run once per instance.");

            // add admin user
            MembershipCreateStatus status;
            MembershipUser admin = Membership.CreateUser(model.AdminUserName, model.AdminPassword, model.AdminEmail, null, null, true, out status);
            if (!(status == MembershipCreateStatus.Success)) throw new MembershipCreateUserException(status);

            Roles.CreateRole(Constants.AdministratorRole);
            Roles.AddUserToRole(admin.Email, Constants.AdministratorRole);

            Roles.CreateRole(Constants.HubLeadRole);

            // merge settings into web.config
            appsettings.Merge("OrganisationName", model.OrganisationName);
            appsettings.Merge("CatalogueName", model.CatalogueName);
            appsettings.Merge("SupportEmail", model.SupportEmail);

            // asdd mail settings to smtp seciton so SmtpClient can pick them up automatically.
            var mailsettings = config.GetSection("system.net/mailSettings/smtp") as System.Net.Configuration.SmtpSection;

            mailsettings.From = model.FromEmail;
            mailsettings.Network.Host = model.MailServerIPAddress;
            if (!String.IsNullOrEmpty(model.MailUsername)) mailsettings.Network.UserName = model.MailUsername;
            if (!String.IsNullOrEmpty(model.MailPassword)) mailsettings.Network.Password = model.MailPassword;

            appsettings.Add("SetupDate", DateTime.Now.ToShortDateString());

            config.Save();

            return View();
        }
    }
}