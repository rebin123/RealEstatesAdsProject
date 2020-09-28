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
        private readonly IUserRepo userRepo;
        private readonly IMapper _mapper;

        public UsersController(IUserRepo userRepo, IMapper mapper)
        {
            this.userRepo = userRepo;
            _mapper = mapper;
        }

        [HttpGet()]
        public ActionResult<IEnumerable<UserDto>> GetUser()
        {            
            var userFromRepo = userRepo.GetUsers();
            return Ok(_mapper.Map<IEnumerable<UserDto>>(userFromRepo));
        }

        [HttpGet("{userName}", Name = "GetUser")]
        public IActionResult GetUser(string userName)
        {
            var userFromRepo = userRepo.GetUser(userName);
            return Ok(_mapper.Map<UserDto>(userFromRepo));
        }

        [HttpPut("Rate")]
        public IActionResult Rate(UpdateUser user)
        {
            var userFromRepo = userRepo.GetUser(user.UserId);
            if (userFromRepo == null) return NotFound();

            userRepo.RateUser(user.UserId, user.Value);
            userRepo.Save();

            return Ok();
        }
    }
}
