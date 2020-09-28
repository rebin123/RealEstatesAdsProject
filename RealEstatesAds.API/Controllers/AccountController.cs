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

namespace RealEstatesAds.API.Controllers
{
    [Route("api/account/register")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserRepo userRepo;
        private readonly IMapper _mapper;

        public AccountController(IUserRepo userRepo, IMapper mapper)
        {
            this.userRepo = userRepo;
            this._mapper = mapper;
        }

        [HttpPost()]
        public IActionResult CreateUser(CreateUserDto createUser)
        {
            if (!ModelState.IsValid) return BadRequest();

            var userEntity = _mapper.Map<User>(createUser);
            userRepo.AddUser(userEntity);
            userRepo.Save();

            return Ok();
        }
    }
}
