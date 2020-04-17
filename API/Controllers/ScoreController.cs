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
    public class ScoreController : ControllerBase
    {
        public IScoreService _scoreService;

        public ScoreController(IScoreService scoreService)
        {
            this._scoreService = scoreService;
        }

        // GET: api/Score
        [HttpGet("Paging")]
        public async Task<ActionResult<Paging>> Paging(string keyword, int pageSize, int pageNumber)
        {
            if (keyword == null)
            {
                keyword = "";
            }
            var data = await _scoreService.Paging(keyword, pageSize, pageNumber);
            if (data != null)
            {
                return Ok(data);
            }
            return BadRequest("Failed ");
        }

        [HttpPost]
        [Route("Search")]
        public async Task<ActionResult<ScoreVM>> Search(string term)
        {
            var data = await _scoreService.Search(term);
            if (data != null)
            {
                return Ok(data);
            }
            return BadRequest("Failed ");
        }

        [HttpGet]
        public async Task<IEnumerable<ScoreVM>> Get()
        {
            return await _scoreService.Get();
        }

        // GET: api/Score/5
        [HttpGet("{id}")]
        public async Task<IEnumerable<ScoreVM>> Get(int id)
        {
            return await _scoreService.Get(id);
        }

        // POST: api/Score
        [HttpPost]
        public IActionResult Post(ScoreVM scoreVM)
        {
            var data = _scoreService.Create(scoreVM);
            if (data > 0)
            {
                return Ok(data);
            }
            return BadRequest("Failed");
            //var status = Ok(data);
        }

        // PUT: api/Score/5
        [HttpPut("{id}")]
        public IActionResult Put(ScoreVM scoreVM)
        {
            var data = _scoreService.Update(scoreVM);
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
            var data = _scoreService.Delete(Id);
            if (data > 0)
            {
                return Ok(data);
            }
            return BadRequest("Failed");
        }
    }
}
