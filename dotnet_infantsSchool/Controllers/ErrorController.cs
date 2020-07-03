using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace dotnet_infantsSchool.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ErrorController : ControllerBase
    {
        //private readonly ILogger<ErrorController> _logger;
        //ILogger<ErrorController> logger

        public ErrorController()
        {
            //_logger = logger;
        }
        /// <summary>
        /// 自定义错误
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult ErrorDIY()
        {
            throw new Exception("这个地方错问题啦啦啦！！！");
            return Ok();
        }
        /// <summary>
        /// 不能被0整除
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult ErrorDivideZero()
        {
            int a = 10;
            int b = a / 0;
            return Ok();
        }
    }
}