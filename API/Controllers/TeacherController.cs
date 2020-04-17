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
    public class TeacherController : ControllerBase
    {
        public ITeacherService _teacherService;

        public TeacherController(ITeacherService teacherService)
        {
            this._teacherService = teacherService;
        }

        [HttpPost]
        public IActionResult Post(TeacherVM teacherVM)
        {
            var data = _teacherService.Create(teacherVM);
            if (data > 0)
            {
                return Ok(data);
            }
            return BadRequest("Failed");
            //var status = Ok(data);
        }

        [HttpPost]
        [Route("AddSchool")]
        public IActionResult AddSchool(SchoolVM schoolVM)
        {
            var data = _teacherService.AddSchool(schoolVM);
            if (data > 0)
            {
                return Ok(data);
            }
            return BadRequest("Failed");
        }

        [HttpDelete]
        public IActionResult Delete(TeacherVM teacherVM)
        {
            var data = _teacherService.Delete(teacherVM);
            if (data > 0)
            {
                return Ok(data);
            }
            return BadRequest("Failed");
        }
    }
}