using Gada.Users.Api.ViewModels;
using Gada.Users.Services;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace Gada.Users.Api.Controllers
{
    [RoutePrefix("api/user")]
    public class UserController : ApiController
    {
        private IApplicationUserManager _userManager;

        public UserController(IApplicationUserManager userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        [Route("test")]
        public async Task<IHttpActionResult> Test()
        {
            return Ok("test");
        }

        [HttpPost]
        [Route("register")]
        public async Task<IHttpActionResult> Register(RegistrationViewModel registration)
        {
            try
            {
                if (registration == null)
                {
                    return BadRequest("An error occured. Please contact support");
                }
                Data.ApplicationUser appUser = new Data.ApplicationUser
                {
                    Email = registration.Email,
                    FirstName = registration.FirstName,
                    LastName = registration.LastName,
                    UserName = registration.Email
                };
                var user = await _userManager.CreateAsync(appUser, registration.Password);
                if (user.Succeeded)
                {
                    _userManager.SendRegistrationEmail(registration.Email);
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<IHttpActionResult> Login(LoginViewModel loginDetails)
        {
            try
            {
                var user = await _userManager.FindAsync(loginDetails.Email, loginDetails.Password);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("confirmEmail/{base64UserId}")]
        public async Task<IHttpActionResult> ConfirmEmail(string base64UserId)
        {
            try
            {
                await _userManager.ConfirmUserEmail(base64UserId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("findByEmail")]
        public async Task<IHttpActionResult> FindByEmail(EmailModel em)
        {
            try
            {
                var user = _userManager.FindByEmailAsync(em.Email).Result;
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("email")]
        public async Task<IHttpActionResult> TestEmail()
        {
            try
            {
                await _userManager.TestEmail();
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}