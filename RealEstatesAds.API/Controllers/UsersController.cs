using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstateAds.Data.Contracts;
using RealEstateAdsEntities;
using RealEstatesAds.API.Models;
using RealEstatesAds.API.Models.UserModel;

namespace RealEstatesAds.API.Controllers
{
    [Route("api/Users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepos userRepo;
        private readonly IMapper _mapper;

        public UsersController(IUserRepos userRepo, IMapper mapper)
        {
            this.userRepo = userRepo;
            _mapper = mapper;
        }

        [HttpGet("{realEstateId}", Name = "GetRealEstate")]
        [Route("api/Users/{userName}")]
        public IActionResult GetUser(string userName)
        {
            var userFromRepo = userRepo.GetUserInfo(userName);
            return Ok(_mapper.Map<IEnumerable<UserDto>>(userFromRepo));
        }

        [HttpPut("{id}")]
        [Route("api/Users/Rate")]
        public IActionResult Rate(int id, [FromBody] UpdateUser user)
        {
            // map model to entity and set id
            var updateUser = _mapper.Map<User>(user);
            updateUser.Id = id;

            //try
            //{
            //    // update user 
            //    userRepo.Update(user, user.);
            //    return Ok();
            //}
            //catch (AppException ex)
            //{
            //    // return error message if there was an exception
            //    return BadRequest(new { message = ex.Message });
            //}

            return NoContent();
        }
    }
}
