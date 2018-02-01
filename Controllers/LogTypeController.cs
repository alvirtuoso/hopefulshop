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
    public class LogTypeController : Controller
    {
        LogTypeRepository ltRepo = new LogTypeRepository();
        private readonly AppOptions _options;
        private readonly ILogger<LogTypeController> _logger;

        //set by dependency injection
        public LogTypeController(IOptions<AppOptions> optionsAccessor, ILogger<LogTypeController> logger)
        {
            _options = optionsAccessor.Value;
            _logger = logger;
        }

        // GET: api/<controller>/
        public IActionResult Index()
        {
            IEnumerable<LogType> logs = null;
            try
            {
                logs = ltRepo.FindAll();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
#if DEBUG

                return new JsonResult(ex.Message);
#endif
            }
            return new JsonResult(logs);

        }

        [HttpPut("add")] // "api/logclient/add
        public IActionResult Add([FromBody]LogType ltype)
        {
            ltype.Id = DbContext.DbHelper.NewID();

            try
            {
                ltype = ltRepo.Add(ltype);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
#if DEBUG

                return new JsonResult(ex.Message);
#endif
            }

            return new JsonResult(ltype);
        }

        // GET api/logtype/delete/<id>
        [HttpDelete("delete/{id}")]
        public IActionResult DeleteById(string id)
        {

            try
            {
                ltRepo.Remove(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
#if DEBUG

                return new JsonResult(ex.Message);
#endif
            }
            return new JsonResult("deleted");
        }
    }
}
