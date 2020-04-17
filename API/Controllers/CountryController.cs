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
    //[Authorize(AuthenticationSchemes = "Bearer ")]
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        public ICountryService _countryService;

        public CountryController(ICountryService countryService)
        {
            this._countryService = countryService;
        }

        // GET: api/Country
        [HttpGet("Paging")]
        public async Task<ActionResult<Paging>> Paging(string keyword, int pageSize, int pageNumber)
        {
            if (keyword == null)
            {
                keyword = "";
            }
            var data = await _countryService.Paging(keyword, pageSize, pageNumber);
            if (data != null)
            {
                return Ok(data);
            }
            return BadRequest("Failed ");
        }

        [HttpPost]
        [Route("Search")]
        public async Task<ActionResult<CountryVM>> Search(string term)
        {
            var data = await _countryService.Search(term);
            if (data != null)
            {
                return Ok(data);
            }
            return BadRequest("Failed ");
        }

        [HttpGet]
        public async Task<IEnumerable<CountryVM>> Get()
        {
            return await _countryService.Get();
        }

        // GET: api/Country/5
        [HttpGet("{id}")]
        public async Task<IEnumerable<CountryVM>> Get(int id)
        {
            return await _countryService.Get(id);
        }

        // POST: api/Country
        [HttpPost]
        public IActionResult Post(CountryVM countryVM)
        {
            var data = _countryService.Create(countryVM);
            if (data > 0)
            {
                return Ok(data);
            }
            return BadRequest("Failed");
            //var status = Ok(data);
        }

        // PUT: api/Country/5
        [HttpPut("{id}")]
        public IActionResult Put(CountryVM countryVM)
        {
            var data = _countryService.Update(countryVM);
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
            var data = _countryService.Delete(Id);
            if (data > 0)
            {
                return Ok(data);
            }
            return BadRequest("Failed");
        }
    }
}
