using IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.Entitys;
using Model.Helper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dotnet_infantsSchool.Controllers
{
    [ApiController]
    [Route("api/CostType")]
    [Authorize("actionAuthrization")]
    public class CostTypeController : ControllerBase
    {
        private readonly ICostTypeServices _costTypeServices;

        public CostTypeController(ICostTypeServices costTypeServices)
        {
            _costTypeServices = costTypeServices;
        }

        [HttpGet]
        public async Task<ActionResult<MessageModel<IEnumerable<CostType>>>> GetCostType()
        {
            CostType list = await _costTypeServices.GetEntityByIdAsync(1);
            return Ok(list);
        }
    }
}