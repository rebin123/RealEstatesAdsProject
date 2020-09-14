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
    [Route("api/Comments")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentRepo commentRepo;
        private readonly IMapper _mapper;

        public CommentsController(ICommentRepo commentRepo, IMapper mapper)
        {
            this.commentRepo = commentRepo;
            _mapper = mapper;
        }

        [HttpGet()]
        [HttpHead]
        [Route("api/Comments/{realEstateId}")]
        public ActionResult<IEnumerable<CommentDto>> GetComments(int realEstateId,
                            [FromQuery] string skip = "", [FromQuery] string take = "10")
        {
            var commentsFromRepo = commentRepo.GetAllCommentsRealEstate(realEstateId ,skip, take);
            return Ok(_mapper.Map<IEnumerable<CommentDto>>(commentsFromRepo));
        }

        [HttpGet("{userName}", Name = "GetCommentFromUser")]
        [Route("api/Comments/ByUser/{userName}")]
        public ActionResult<IEnumerable<CommentDto>> GetCommentFromUser(string userName,
                            [FromQuery] string skip = "", [FromQuery] string take = "10")
        {
            var commentsFromRepo = commentRepo.GetCommentsFromUser(userName,skip, take);

            return Ok(_mapper.Map<IEnumerable<CommentDto>>(commentsFromRepo));
        }

        [HttpPost]
        public ActionResult<CommentDto> CreateComment(CreateCommentDto comment)
        {
            var commentEntity = _mapper.Map<Comment>(comment);
            commentRepo.AddComment(commentEntity);
            commentRepo.Save();

            var commentToReturn = _mapper.Map<CommentDto>(commentEntity);
            return CreatedAtRoute("GetRealEstate",
                new { userName = commentToReturn.UserName }, commentToReturn);
        }
    }
}
