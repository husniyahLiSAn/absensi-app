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
    public class ClassController : ControllerBase
    {
        public IClassService _classService;

        public ClassController(IClassService classService)
        {
            this._classService = classService;
        }

        // GET: api/Class
        [HttpGet("Paging")]
        public async Task<ActionResult<Paging>> Paging(string keyword, int pageSize, int pageNumber)
        {
            if (keyword == null)
            {
                keyword = "";
            }
            var data = await _classService.Paging(keyword, pageSize, pageNumber);
            if (data != null)
            {
                return Ok(data);
            }
            return BadRequest("Failed ");
        }

        [HttpGet("PagingbyTeacher")]
        public async Task<ActionResult<Paging>> PagingbyTeacher(string email, string keyword, int pageSize, int pageNumber)
        {
            if (keyword == null)
            {
                keyword = "";
            }
            var data = await _classService.PagingbyTeacher(email, keyword, pageSize, pageNumber);
            if (data != null)
            {
                return Ok(data);
            }
            return BadRequest("Failed ");
        }

        [HttpPost]
        [Route("Search")]
        public async Task<ActionResult<ClassVM>> Search(ClassVM classVM)
        {
            var data = await _classService.Search(classVM);
            if (data != null)
            {
                return Ok(data);
            }
            return BadRequest("Failed ");
        }

        [HttpGet]
        public async Task<IEnumerable<ClassVM>> Get()
        {
            return await _classService.Get();
        }

        [HttpGet("{id}")]
        public async Task<IEnumerable<ClassVM>> Get(int id)
        {
            return await _classService.Get(id);
        }

        [HttpPost]
        public IActionResult Post(ClassVM classVM)
        {
            var data = _classService.Create(classVM);
            if (data > 0)
            {
                return Ok(data);
            }
            return BadRequest("Failed");
            //var status = Ok(data);
        }

        [HttpPut("{id}")]
        public IActionResult Put(ClassVM classVM)
        {
            var data = _classService.Update(classVM);
            if (data > 0)
            {
                return Ok(data);
            }
            return BadRequest("Failed");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int Id)
        {
            var data = _classService.Delete(Id);
            if (data > 0)
            {
                return Ok(data);
            }
            return BadRequest("Failed");
        }
    }
}