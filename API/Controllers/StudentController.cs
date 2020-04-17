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
    public class StudentController : ControllerBase
    {
        public IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            this._studentService = studentService;
        }

        // GET: api/Student
        [HttpGet("Paging")]
        public async Task<ActionResult<Paging>> Paging(int classId, string keyword, int pageSize, int pageNumber)
        {
            if (keyword == null)
            {
                keyword = "";
            }
            var data = await _studentService.Paging(classId, keyword, pageSize, pageNumber);
            if (data != null)
            {
                return Ok(data);
            }
            return BadRequest("Failed ");
        }

        [HttpPost]
        [Route("Search")]
        public async Task<ActionResult<StudentVM>> Search(string term)
        {
            var data = await _studentService.Search(term);
            if (data != null)
            {
                return Ok(data);
            }
            return BadRequest("Failed ");
        }

        [HttpGet]
        public async Task<IEnumerable<StudentVM>> Get()
        {
            return await _studentService.Get();
        }

        // GET: api/Student/5
        [HttpGet("{id}")]
        public async Task<IEnumerable<StudentVM>> Get(int id)
        {
            return await _studentService.Get(id);
        }

        // POST: api/Student
        [HttpPost]
        public IActionResult Post(StudentVM studentVM)
        {
            var data = _studentService.Create(studentVM);
            if (data > 0)
            {
                return Ok(data);
            }
            return BadRequest("Failed");
            //var status = Ok(data);
        }

        // PUT: api/Student/5
        [HttpPut("{id}")]
        public IActionResult Put(StudentVM studentVM)
        {
            var data = _studentService.Update(studentVM);
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
            var data = _studentService.Delete(Id);
            if (data > 0)
            {
                return Ok(data);
            }
            return BadRequest("Failed");
        }
    }
}
