using RealEstateAdsEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstateAds.Data.Contracts
{
    public interface IUserRepos
    {
        User GetUserInfo(string userName);
        Rating RateUser(string userName, Rating rating);
        bool Save();
    }
}
