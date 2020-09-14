using RealEstateAds.Data.Contracts;
using RealEstateAdsEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RealEstateAds.Data.Repositories
{
    public class UserRepo : IUserRepo
    {
        private readonly RealEstateAdsContext context;
        public UserRepo(RealEstateAdsContext context)
        {
            this.context = context;
        }
        public RealEstate AddRealState(RealEstate realEstate)
        {
            context.RealEstates.Add(realEstate);
            return realEstate;
        }
        public User AddUser(User user)
        {
            if (user == null) return null;
            context.Users.Add(user);
            return user;
        }
        public User GetUser(int userId)
        {
            var user = context.Users.FirstOrDefault(a => a.Id == userId);
            if (user == null) return null;

            int? value = CalcAvgRate(userId);
            user.AverageRating = value;
            return user;
        }
        private int? CalcAvgRate(int userId)
        {
            var ratingList = GetRatings(userId);
            if (ratingList.Count() == 0) return null;

            int x = 0;
            foreach (var rate in ratingList)
            {
                x += rate.Rate;
            }
            return x / ratingList.Count();
        }
        public IEnumerable<Rating> GetRatings(int userId)
        {
            return context.Ratings.Where(r => r.User.Id == userId).ToList();
        }
        public Rating GetRatingFromUser(int userId, int ratingId)
        {
            return context.Ratings.Where(c => c.User.Id == userId && c.Id == ratingId).FirstOrDefault();
        }
        public IEnumerable<User> GetUsers()
        {
            var users = context.Users.ToList<User>();
            foreach (var user in users)
            {
                user.AverageRating = CalcAvgRate(user.Id);
            }
            return users;
        }
        public void DeleteUser(User user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            context.Users.Remove(user);
        }
        public bool UserExists(int userId) => context.Users.Any(a => a.Id == userId);
        public bool Save() => (context.SaveChanges() >= 0);
        public IEnumerable<RealEstate> GetAllRealEstate(int userId)
        {
            return context.RealEstates.Where(c => c.User.Id == userId).OrderBy(c => c.CreatedOn).ToList();
        }
        public RealEstate GetRealEstate(int userId, int realEstateId)
        {
            return context.RealEstates.Where(c => c.User.Id == userId && c.Id == realEstateId).FirstOrDefault();
        }
        public bool AddRealState(int userId, RealEstate realEstate)
        {
            var user = GetUser(userId);
            if (user == null) return false;
            if (realEstate == null) return false;

            realEstate.UserId = user.Id;
            realEstate.CreatedOn = DateTime.Now;
            bool caneBeBought = realEstate.PurchasePrice > 0;
            bool caneBeRented = realEstate.RentingPrice > 0;

            realEstate.CanBeBought = caneBeBought;
            realEstate.CanBeRented = caneBeRented;
            context.RealEstates.Add(realEstate);
            return true;
        }
        public bool DeleteRealEstateFromUser(int userId, int realEstateId)
        {
            var realEstate = GetRealEstate(userId, realEstateId);
            if (realEstate == null) return false;
            context.RealEstates.Remove(realEstate);
            return true;
        }
        public bool RateUser(int userId, Rating rating)
        {
            var user = GetUser(userId);
            if (user == null) return false;
            rating.UserId = userId;
            context.Ratings.Add(rating);
            return true;
        }
    }
}
