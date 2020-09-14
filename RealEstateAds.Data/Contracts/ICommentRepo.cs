using RealEstateAdsEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstateAds.Data.Contracts
{
    public interface ICommentRepo
    {
        IEnumerable<Comment> GetAllCommentsRealEstate(int realEstateId, string skip, string take);
        IEnumerable<Comment> GetCommentsFromUser(string userName, string skip, string take);
        bool AddComment(Comment comment);
        bool Save();
    }
}
