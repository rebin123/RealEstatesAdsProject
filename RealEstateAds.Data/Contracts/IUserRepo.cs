using RealEstateAdsEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstateAds.Data.Contracts
{
    public interface IUserRepo
    {

        #region User 
        User AddUser(User user);
        User GetUser(string userName);
        User GetUser(int userId);
        bool UserExists(int userId);
        IEnumerable<User> GetUsers();
        void DeleteUser(User user);
        string AuthenticateUser(string name, string password);
        bool ValidUser(string name, string password);
        #endregion

        #region RealEstae
        IEnumerable<RealEstate> GetAllRealEstate(int userId);
        IEnumerable<RealEstate> GetRealEstates(string skip, string take);
        RealEstate GetRealEstateFromUser(int userId, int realEstateId);
        RealEstate GetRealEstate(int realEstateId);
        bool DeleteRealEstateFromUser(int userId, int realEstateId);
        RealEstate AddRealEstate(int userId, RealEstate realEstate);
        #endregion

        #region Comment
        Comment AddComment(string username, Comment comment);
        IEnumerable<Comment> GetCommentsFromUser(string userName, string skip, string take);
        IEnumerable<Comment> GetCommentsRealEstate(int realEstateId, string skip, string take);
        #endregion

        bool RateUser(int userId, string value);
        IEnumerable<Rating> GetRatings(int userId);
        //Rating GetRatingFromUser(int userId, int ratingId);

        bool Save();
    }
}
