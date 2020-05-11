using IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;

namespace dotnet_infantsSchool.Controller
{
    [Route("api/CostType")]
    public class CostTypeController : ControllerBase
    {
        private readonly ICostTypeServices _costTypeServices;

        public CostTypeController(ICostTypeServices costTypeServices)
        {
            _costTypeServices = costTypeServices;
        }

        public async Task<ActionResult<IEnumerable<CostType>>> GetCostType()
        {
            CostType list = await _costTypeServices.GetEntityByIdAsync(1);
            return Ok(list);
        }
    }
}