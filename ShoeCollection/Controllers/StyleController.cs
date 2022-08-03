using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using System;
using System.Security.Claims;
using ShoeCollection.Models;
using ShoeCollection.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShoeCollection.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StyleController : ControllerBase
    {
        private readonly IStyleRepository _styleRepository;

        public StyleController(IStyleRepository styleRepository)
        {
            _styleRepository=styleRepository;
        }



        // GET: api/<StyleController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_styleRepository.GetAllStyles());
        }

        // GET api/<StyleController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var style = _styleRepository.GetStyleById(id);
            if(style == null)
            {
                return NotFound();
            }
            return Ok(style);
        }

        // POST api/<StyleController>
        [HttpPost]
        public IActionResult Post(Style style)
        {
            _styleRepository.AddAStyle(style);
            return CreatedAtAction("Get", new { id = style.Id }, style);
        }

        // PUT api/<StyleController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, Style style)
        {
            if (id != style.Id)
            {
                return BadRequest();
            }
            _styleRepository.UpdateStyle(style);
                return NoContent(); 
        }

        // DELETE api/<StyleController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _styleRepository.DeleteStyle(id);
            return NoContent();
        }
    }
}
