using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Services.Interface;
using Data.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer ")]
    [Route("api/[controller]")]
    [ApiController]
    public class PresenceController : ControllerBase
    {
        public IPresenceService _presenceService;

        public PresenceController(IPresenceService presenceService)
        {
            this._presenceService = presenceService;
        }

        [HttpGet]
        public async Task<IEnumerable<PresenceVM>> Get(DateTime startDate, DateTime finishDate)
        {
            return await _presenceService.Get(startDate, finishDate);
        }

        [HttpPost]
        public IActionResult Post(PresenceVM presenceVM)
        {
            var data = _presenceService.Create(presenceVM);
            if (data > 0)
            {
                return Ok(data);
            }
            return BadRequest("Failed");
        }
    }
}