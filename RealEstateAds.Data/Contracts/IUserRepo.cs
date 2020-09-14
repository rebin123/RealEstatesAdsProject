using RealEstateAdsEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstateAds.Data.Contracts
{
    public interface IUserRepo
    {
        User AddUser(User user);
        IEnumerable<User> GetUsers();
        User GetUser(int userId);
        IEnumerable<RealEstate> GetAllRealEstate(int userId);
        bool UserExists(int userId);
        RealEstate GetRealEstate(int userId, int realEstateId);
        void DeleteUser(User user);
        bool DeleteRealEstateFromUser(int userId, int realEstateId);
        bool AddRealState(int userId, RealEstate realEstate);
        bool RateUser(int userId, Rating rating);
        IEnumerable<Rating> GetRatings(int userId);
        Rating GetRatingFromUser(int userId, int ratingId);
        bool Save();
    }
}
