using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using ShoeCollection.Models;
using ShoeCollection.Repositories;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShoeCollection.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoeController : ControllerBase
    {
        private readonly IShoeRepository _shoeRepository;
        private readonly IUserRepository _userRepository;
        public ShoeController(IShoeRepository shoeRepository, IUserRepository userRepository)
        {
            _shoeRepository = shoeRepository;
            _userRepository =userRepository;
        }

        // GET: api/<ShoeController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_shoeRepository.GetAllShoes());
        }

        // GET api/<ShoeController>/5
        [Authorize]
        [HttpGet("myShoes")]
        public IActionResult GetShoesByCurrentUser()
        {
            var shoeUser = GetCurrentUserProfile();
            if (shoeUser == null)
            {
                return NotFound();
            }
            var id = shoeUser.Id;
            return Ok(_shoeRepository.GetShoesByLoggedUser(id));
        }

        [Authorize]
        [HttpGet("myFavorites")]
        public IActionResult GetFavoritesByCurrentUser()
        {
            var favUser = GetCurrentUserProfile();
            if (favUser == null)
            {
                return NotFound();
            }
            var id = favUser.Id;
            return Ok(_shoeRepository.GetFavoritesByLoggedUser(id));
        }

        // POST api/<ShoeController>
        [Authorize]
        [HttpPost]
        public IActionResult Post(Shoe shoe)
        {
            shoe.UserId = GetCurrentUserProfile().Id;
            _shoeRepository.AddShoe(shoe);
            return CreatedAtAction("Get", new { shoe.Id }, shoe);
        }

        [Authorize]
        [HttpPost("Favorite")]
        public IActionResult PostAFav(Favorite favorite)
        {
            favorite.UserId = GetCurrentUserProfile().Id;
            _shoeRepository.AddAFavorite(favorite);
            return CreatedAtAction("Get", new { favorite.Id }, favorite);
            //      return Ok();
        }

        // PUT api/<ShoeController>/5
        [Authorize]
        [HttpPut("myshoes/edit/{id}")]
        public IActionResult Put(int id, Shoe shoe)
        {
            if (id != shoe.Id)
            {
                return BadRequest();
            }
            var userId = GetCurrentUserProfile().Id;
            _shoeRepository.UpdateAShoe(shoe, userId);
            return NoContent();
        }

        [Authorize]
        [HttpGet("myshoes/{id}")]
        public IActionResult Get(int id)
        {
            var userId = GetCurrentUserProfile().Id;
            var shoe = _shoeRepository.GetShoeById(id, userId);
            if (id != shoe.Id)
            {
                return BadRequest();
            }
            return Ok(shoe);
        }


    // DELETE api/<ShoeController>/5
    [HttpDelete("myshoes/{id}")]
    public IActionResult Delete(int id)
    {
        _shoeRepository.DeleteAShoe(id);
        return NoContent();
    }

        [Authorize]
        [HttpDelete("unlikeshoe/{id}")]
    public IActionResult UnlikeAShoe(int id)
    {   
           var userId = GetCurrentUserProfile().Id;
            _shoeRepository.DeleteAFavorite(id, userId);
            return NoContent();
    }


        private User GetCurrentUserProfile()
        {
            var firebaseUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return _userRepository.GetByFirebaseUserId(firebaseUserId);
        }
    }
}
