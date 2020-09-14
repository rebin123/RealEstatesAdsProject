using RealEstateAds.Data.Contracts;
using RealEstateAdsEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RealEstateAds.Data.Repositories
{
    public class UserRepos : IUserRepos
    {
        private readonly RealEstateAdsContext context;
        public UserRepos(RealEstateAdsContext context)
        {
            this.context = context;
        }
        public User GetUserInfo(string userName)
        {
            if (userName == null) return null;
            return context.Users.FirstOrDefault(a => a.Name == userName);
        }

        public Rating RateUser(string userName, Rating rating)
        {
            var user = GetUserInfo(userName);
            if (user == null) return null;
            rating.UserId = user.Id;
            context.Ratings.Add(rating);
            return rating;
        }
        public bool Save() => (context.SaveChanges() >= 0);

    }
}
