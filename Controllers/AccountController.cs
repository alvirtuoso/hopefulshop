using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using shop.Repository;
using shop.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace shop.Controllers
{

    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly AppOptions _options;
        private readonly ILogger<AccountController> _logger;

        AccountRepository acctRepo = new AccountRepository();

        //set by dependency injection
        public AccountController(IOptions<AppOptions> optionsAccessor, ILogger<AccountController> logger)
        {
            _options = optionsAccessor.Value;
            _logger = logger;
        }


        // GET: api/account/
        public IActionResult Index()
        {
            IEnumerable<Account> accounts = null;
            try
            {
                accounts = acctRepo.FindAll();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return NotFound(ex.Message);
            }

            return new JsonResult(accounts);
        }

        [HttpPost("add")] // "api/account/add
        public IActionResult Add([FromBody]Account acct)
        {
            Account a = acct;

            try
            {
                a = acctRepo.Add(acct);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return NotFound(ex.Message);
            }

            return new JsonResult(a);
        }

        // GET api/account/aNumber Ex. http://127.0.0.1:5000/api/account/7ccc9a70-b635-4388-b0c5-afb4a3664f33
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            Account acct = null;
            try
            {
                acct = acctRepo.FindByID(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return NotFound(ex.Message);
            }


            return new JsonResult(acct);
        }

        // GET api/account/delete
        [HttpDelete("delete/{id}")]
        public IActionResult DeleteAccount(string id)
        {
            bool r = false;

            try
            {
                r = acctRepo.RemoveById(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return NotFound(ex.Message);
            }

            return new JsonResult(r);
        }

        [HttpPut("update")]
        public IActionResult Update([FromBody]Account acct)
        {
            bool result = false;
            try
            {
                result = acctRepo.UpdateAccount(acct);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return NotFound(ex.Message);
            }
            return new JsonResult(result);
        }

    }
}
