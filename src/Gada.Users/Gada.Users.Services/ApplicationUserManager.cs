using Gada.Users.Data;
using Microsoft.AspNet.Identity;
using SendGrid;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace Gada.Users.Services
{
    public class ApplicationUserManager : IApplicationUserManager
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserStore<ApplicationUser> _store;

        public ApplicationUserManager(IUserStore<ApplicationUser> store)
            : this(store, null) { }

        public ApplicationUserManager(IUserStore<ApplicationUser> store, IUserTokenProvider<ApplicationUser, string> userTokenProvider)
        {
            _userManager = new UserManager<ApplicationUser>(store);
            _store = store;

            _userManager.UserValidator = new UserValidator<ApplicationUser>(_userManager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            _userManager.PasswordValidator = new PasswordValidator()
            {
                RequiredLength = 8,
                RequireNonLetterOrDigit = false,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = false
            };

            if (userTokenProvider != null)
            {
                _userManager.UserTokenProvider = userTokenProvider;
            }
        }

        public Task<ApplicationUser> FindAsync(string username, string password)
        {
            return _userManager.FindAsync(username, password);
        }

        public Task<ApplicationUser> FindByNameAsync(string username)
        {
            return _userManager.FindByNameAsync(username);
        }

        public Task<ApplicationUser> FindByEmailAsync(string email)
        {
            return _userManager.FindByEmailAsync(email);
        }

        public Task<ApplicationUser> FindByIdAsync(string userId)
        {
            return _userManager.FindByIdAsync(userId);
        }

        public Task<IdentityResult> CreateAsync(ApplicationUser user, string password)
        {
            return _userManager.CreateAsync(user, password);
        }

        public void SendRegistrationEmail(string emailAddress)
        {
            try
            {
                var user = FindByEmailAsync(emailAddress).Result;
                if (user != null)
                {
                    byte[] userIdBytes = ASCIIEncoding.ASCII.GetBytes(user.Id);
                    string subject = "Gada Account Registration Confirmation";
                    string body = "Hello " + user.FirstName + "!" +
                        "<p>Thank you for registering on Gada. To confirm your email address please click the link below:</p><a href='" +
                        ConfigurationManager.AppSettings["EmailConfirmationLink"] + Convert.ToBase64String(userIdBytes) + "'>" +
                        ConfigurationManager.AppSettings["EmailConfirmationLink"] + Convert.ToBase64String(userIdBytes) + "</a></p>" +
                        "<p>Kind Regards,</p><p>The Gada Team</p>";
                    var message = new MailMessage("Registration@Gada.org.uk", emailAddress, subject, body);
                    message.IsBodyHtml = true;
                   
                    var smtpClient = new SmtpClient("smtp.sendgrid.net", 587);
                    var un = ConfigurationManager.AppSettings["sgUsername"];
                    var pw = ConfigurationManager.AppSettings["sgPassword"];

                    var credentials = new NetworkCredential(un, pw);
                    smtpClient.Credentials = credentials;

                    smtpClient.Send(message);

//                    var email = new SendGridMessage();
//                    string subject = "Gada Account Registration Confirmation";
//                    byte[] userIdBytes = ASCIIEncoding.ASCII.GetBytes(user.Id);
//                    string body = "Hello " + user.FirstName + "!" +
//                        "<p>Thank you for registering on Gada. To confirm your email address please click the link below:</p><a href='" +
//                        ConfigurationManager.AppSettings["EmailConfirmationLink"] + Convert.ToBase64String(userIdBytes) + "'>" +
//                        ConfigurationManager.AppSettings["EmailConfirmationLink"] + Convert.ToBase64String(userIdBytes) + "</a></p>" +
//                        "<p>Kind Regards,</p><p>The Gada Team</p>";

//                    email.From = new MailAddress("Registration@Gada.org.uk");
//                    List<string> recipients = new List<string>
//                    {
//                        emailAddress
//                    };

//                    email.AddTo(recipients);
//                    email.Subject = subject;
//                    email.Html = body;
//                    email.Text = @"
//            Hello " + user.FirstName + @"!\r\n\r\n
//            Thank you for registering on Gada. To confirm your email address please go to the the link below:\r\n\r\n"
//                          + ConfigurationManager.AppSettings["EmailConfirmationLink"] + Convert.ToBase64String(userIdBytes)
//                          + "\r\n\r\nKind Regards,\r\n\r\nThe Gada Team";

//                    email.EnableClickTracking(true);

//                    var un = ConfigurationManager.AppSettings["sgUsername"];
//                    var pw = ConfigurationManager.AppSettings["sgPassword"];

//                    var credentials = new NetworkCredential(un, pw);

//                    var transportWeb = new Web(credentials);

                    //var transportWeb = new Web("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");

                    //transportWeb.DeliverAsync(email);

                }
                else
                {
                }
            }
            catch (Exception ex)
            {
            }
        }

        public async Task ConfirmUserEmail(string base64UserId)
        {
            try
            {
                byte[] userIdBytes = Convert.FromBase64String(base64UserId);
                string userId = Encoding.UTF8.GetString(userIdBytes);
                var user = FindByIdAsync(userId).Result;
                user.EmailConfirmed = true;
                var result = await _userManager.UpdateAsync(user);
            }
            catch (Exception ex)
            {
                
            }
        }

        public void Dispose()
        {
            _userManager.Dispose();
        }

        public async Task TestEmail()
        {
            string subject = "Gada Test email";
            string body = "Test!" +
                "<p>This is a test email";
            var message = new MailMessage("Test@Gada.org.uk", "rob.allsopp@hotmail.co.uk", subject, body);
            message.IsBodyHtml = true;

            var smtpClient = new SmtpClient("smtp.sendgrid.net", 587);
            var un = ConfigurationManager.AppSettings["sgUsername"];
            var pw = ConfigurationManager.AppSettings["sgPassword"];

            var credentials = new NetworkCredential(un, pw);
            smtpClient.Credentials = credentials;

            smtpClient.Send(message);
        }
    }
}