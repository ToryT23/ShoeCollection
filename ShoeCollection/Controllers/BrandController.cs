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


    public class BrandController : ControllerBase
    {
        private readonly IBrandRepository _brandRepository;

        public BrandController(IBrandRepository brandRepository)
        {
            _brandRepository=brandRepository;
        }

        // GET: api/<BrandController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_brandRepository.GetAllBrands());
        }

        // GET api/<BrandController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var brand = _brandRepository.GetBrandById(id);
            if(brand == null)
            {
                return NotFound();
            }
            return Ok(brand);
        }

        // POST api/<BrandController>
        [HttpPost]
        public IActionResult Post(Brand brand)
        {

            _brandRepository.AddABrand(brand);
            return CreatedAtAction("Get", new { id = brand.Id }, brand);
        }

        // PUT api/<BrandController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, Brand brand)
        {
            if(id != brand.Id)
            {
                return BadRequest();
            }
            _brandRepository.UpdateBrand(brand);
            return NoContent();
        }

        // DELETE api/<BrandController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
         _brandRepository.DeleteABrand(id);
            return NoContent();
        }
    }
}
