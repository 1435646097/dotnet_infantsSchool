using log4net.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_infantsSchool.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ErrorController:ControllerBase
    {
        private readonly ILogger<ErrorController> _logger;

        public ErrorController(ILogger<ErrorController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public IActionResult ErrorDIY()
        {
            throw new Exception("这个地方错问题啦啦啦！！！");
            return Ok();
        }
        [HttpGet]
        public IActionResult ErrorDivideZero()
        {
            int a = 10;
            int b = a / 0;
            return Ok();
        }
    }
}
