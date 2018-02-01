using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using shop.Repository;
using shop.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using shop.Helpers;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace shop.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly AppOptions _options;
        private readonly ILogger<UserController> _logger;

        UserRepository userRepo = new UserRepository();

        //set by dependency injection
        public UserController(IOptions<AppOptions> optionsAccessor, ILogger<UserController> logger)
        {
            _options = optionsAccessor.Value;
            _logger = logger;
        }

        // GET: api/user/
        public IActionResult Index()
        {
            IEnumerable<User> users = null;
            try
            {
                users = userRepo.FindAll();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return NotFound(ex.Message);
            }

            return new JsonResult(users);
        }

        [HttpPost("add")] // "api/user/add
        public IActionResult Add([FromBody]User user)
        {
            User u = user;

            try
            {
                u = userRepo.Add(user);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return NotFound(ex.Message);
            }

            return new JsonResult(u);
        }

        // GET api/user/aNumber Ex. http://127.0.0.1:5000/api/user/[1234-user-id...w323]
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            User user = null;
            try
            {
                user = userRepo.FindByID(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return NotFound(ex.Message);
            }


            return new JsonResult(user);
        }

        // GET api/user/delete
        [HttpDelete("delete/{id}")]
        public IActionResult DeleteUser(string id)
        {
            bool result = false;
            try
            {
                result = userRepo.RemoveById(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return NotFound(ex.Message);
            }
            return new JsonResult(result);
        }

        /// <summary>
        /// Update the specified user. api/user/update
        /// </summary>
        /// <returns>The update.</returns>
        /// <param name="user">User.</param>
		[HttpPost("update")]
        public IActionResult Update([FromBody]User user)
        {
            bool result = false;
            try
            {
                result = userRepo.UpdateUser(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return NotFound(ex.Message);
            }
            return new JsonResult(result);
        }

        /// <summary>
        /// Finds the by email. api/user/findbyemail/[email]
        /// </summary>
        /// <returns>The by email.</returns>
        /// <param name="email">Email.</param>
        [HttpGet("findbyemail/{email}")]
        public IActionResult FindByEmail(string email)
        {
            shop.Models.User u = null;
            try
            {
                u = userRepo.FindByEmail(email);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return NotFound(ex.Message);
            }
            return new JsonResult(u);
        }

        /// <summary>
        /// Ises the existing user. api/user/isexisting/[email]
        /// </summary>
        /// <returns>The existing user.</returns>
        /// <param name="email">Email.</param>
		[HttpGet("isexisting/{email}")]
        public IActionResult IsExistingUser(string email)
        {
            bool result = false;
            try
            {
                result = userRepo.IsExisting(email);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return NotFound(ex.Message);
            }
            return new JsonResult(result);
        }

        /// <summary>
        /// Sends the mail. api/User/send
        /// </summary>
        /// <returns>The mail.</returns>
        [HttpPost("send")]
        public IActionResult SendMail([FromBody] ContactUs contact)
        {
            try
            {
                Emailer mailer = new Emailer();
                mailer.SendMail(contact, _options.SPARKPOST_API_KEY);

            }
            catch (Exception ex)
            {
                _logger.LogError("****** AverageStars ******* " + ex.Message, new ContactUs[] { contact });
                return NotFound(ex.Message);
            }

            return Ok();
        }
    }
}
