using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using shop.Repository;
using shop.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace shop.Controllers
{
    [Route("api/[controller]")]
    public class LogClientController : Controller
    {
        LogClientRepository lcRepo = new LogClientRepository();
        private readonly AppOptions _options;
        private readonly ILogger<LogClientController> _logger;

        //set by dependency injection
        public LogClientController(IOptions<AppOptions> optionsAccessor, ILogger<LogClientController> logger)
        {
            _options = optionsAccessor.Value;
            _logger = logger;
        }

        // GET: api/<controller>/
        public IActionResult Index()
        {
            IEnumerable<LogClient> logs = null;
            try
            {
                logs = lcRepo.FindAll();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return NotFound(ex.Message);
            }
            return new JsonResult(logs);

        }

        [HttpPost("add")] // "api/logclient/add
        public IActionResult Add([FromBody]LogClient lc)
        {
            lc.Id = DbContext.DbHelper.NewID();
            try
            {
                lc = lcRepo.Add(lc);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return NotFound(ex.Message);
            }

            return new JsonResult(lc);
        }

        // GET api/logclient/delete/<id>
        [HttpDelete("delete/{id}")]
        public IActionResult DeleteById(string id)
        {

            try
            {
                lcRepo.Remove(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return NotFound(ex.Message);
            }
            return new JsonResult("deleted");
        }
    }
}
