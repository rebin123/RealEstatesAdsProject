using RealEstateAds.Data.Contracts;
using RealEstateAdsEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RealEstateAds.Data.Repositories
{
    public class CommentRepo : ICommentRepo
    {
        private readonly RealEstateAdsContext context;
        public CommentRepo(RealEstateAdsContext context)
        {
            this.context = context;
        }
        public bool AddComment(Comment comment)
        {
            if (comment == null) return false;

            //comment.UserId = userId;
            //comment.RealEstateId = realEstateId;
            comment.CreatedOn = DateTime.Now;
            context.Comments.Add(comment);
            return true;
        }

        public IEnumerable<Comment> GetAllCommentsRealEstate(int realEstateId, string skip, string take)
        {
            if (skip == null) skip = "0";
            if (take == null) take = "10";
            return context.Comments.ToList<Comment>()
                .OrderByDescending(o => o.CreatedOn)
                .Skip(int.Parse(skip))
                .Take(int.Parse(take));
        }

        public IEnumerable<Comment> GetCommentsFromUser(string userName, string skip, string take)
        {
            if (skip == null) skip = "0";
            if (take == null) take = "10";
            return context.Comments.ToList<Comment>()
                .Where(x=>x.User.Name==userName)
                .OrderByDescending(o => o.CreatedOn)
                .Skip(int.Parse(skip))
                .Take(int.Parse(take));
        }
        public bool Save() => (context.SaveChanges() >= 0);
    }
}
