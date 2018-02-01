using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using shop.Repository;
using shop.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace shop.Controllers
{
    [Route("api/[controller]")]
    public class DnsController : Controller
    {
        DnsRepository dnsRepo = new DnsRepository();
        private readonly AppOptions _options;
        private readonly ILogger<DnsController> _logger;

        //set by dependency injection
        public DnsController(IOptions<AppOptions> optionsAccessor, ILogger<DnsController> logger)
        {
            _options = optionsAccessor.Value;
            _logger = logger;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            IEnumerable<Dns> dnsList = null;
            try
            {
                dnsList = dnsRepo.FindAll();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return NotFound(ex.Message);
            }

            return new JsonResult(dnsList);
        }

        [HttpGet("findbyid/{id}")]
        public IActionResult FindDnsById(string id)
        {
            Dns n = null;
            try
            {
                dnsRepo.FindByID(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return NotFound(ex.Message);
            }

            return new JsonResult(n);
        }

        [HttpGet("findbyemail/{email}")]
        public IActionResult FindDnsByEmail(string email)
        {
            Dns n = null;
            try
            {
                n = dnsRepo.FindByEmail(email);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return NotFound(ex.Message);
            }

            return new JsonResult(n);
        }

        [HttpGet("findbyamazonid/{id}")]
        public IActionResult FindDnsByAmazonId(string id){
            Dns n = null;
                try
            {
                n = dnsRepo.FindByAmazonId(id);
                if(null == n){
                    n = new Dns() { AmazonId = Helpers.Constants.AMAZON_DEFAULT_ID };   
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return NotFound(ex.Message);
            }

            return new JsonResult(n);
        }

		[HttpGet("findbyurlsubname/{name}")]
		public IActionResult FindDnsByUrlSubname(string name)
		{
			Dns n = null;
			try
			{
                n = dnsRepo.FindBySubUrlname(name);
				if (null == n)
				{
                    n = new Dns() { AmazonId = Helpers.Constants.AMAZON_DEFAULT_ID };
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message);
				return NotFound(ex.Message);
			}

			return new JsonResult(n);
		}

        [HttpPut("add")] // "api/dns/add
        public IActionResult Add([FromBody]Dns dns)
        {
            bool d = false;
            try
            {
                d = dnsRepo.AddDns(dns);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
				return NotFound(ex.Message);
            }

            return new JsonResult(d);
        }

        [HttpPost("update")]
        public IActionResult Update([FromBody]Dns dns)
        {
            bool result = false;
            try
            {
                result = dnsRepo.UpdateDns(dns);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return NotFound(ex.Message);
            }
            return new JsonResult(result);
        }

        // GET api/dns/delete/<id>
        [HttpDelete("delete/{email}")]
        public IActionResult DeleteDnsByEmail(string email)
        {
            int result = 0;
            try
            {
                result = dnsRepo.RemoveByEmail(email);
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
