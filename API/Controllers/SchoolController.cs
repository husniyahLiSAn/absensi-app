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
    public class SchoolController : ControllerBase
    {
        public ISchoolService _schoolService;

        public SchoolController(ISchoolService schoolService)
        {
            this._schoolService = schoolService;
        }

        // GET: api/School
        [HttpGet("Paging")]
        public async Task<ActionResult<Paging>> Paging(string keyword, int pageSize, int pageNumber)
        {
            if (keyword == null)
            {
                keyword = "";
            }
            var data = await _schoolService.Paging(keyword, pageSize, pageNumber);
            if (data != null)
            {
                return Ok(data);
            }
            return BadRequest("Failed ");
        }

        [HttpPost("{term}")]
        [Route("Search/{term}")]
        public async Task<ActionResult<SchoolVM>> Search(string term)
        {
            var data = await _schoolService.Search(term);
            if (data != null)
            {
                return Ok(data);
            }
            return BadRequest("Failed ");
        }

        [HttpGet]
        public async Task<IEnumerable<SchoolVM>> Get()
        {
            return await _schoolService.Get();
        }

        // GET: api/School/5
        [HttpGet("{id}")]
        public async Task<IEnumerable<SchoolVM>> Get(int id)
        {
            return await _schoolService.Get(id);
        }

        // POST: api/School
        [HttpPost]
        public IActionResult Post(SchoolVM schoolVM)
        {
            var data = _schoolService.Create(schoolVM);
            if (data > 0)
            {
                return Ok(data);
            }
            return BadRequest("Failed");
            //var status = Ok(data);
        }

        // PUT: api/School/5
        [HttpPut("{id}")]
        public IActionResult Put(SchoolVM schoolVM)
        {
            var data = _schoolService.Update(schoolVM);
            if (data > 0)
            {
                return Ok(data);
            }
            return BadRequest("Failed");
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int Id)
        {
            var data = _schoolService.Delete(Id);
            if (data > 0)
            {
                return Ok(data);
            }
            return BadRequest("Failed");
        }
    }
}
