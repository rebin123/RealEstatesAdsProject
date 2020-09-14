using RealEstateAdsEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstateAds.Data.Contracts
{
    public interface IRealEstateRepo
    {
        IEnumerable<RealEstate> GetRealEstates(string skip, string take);
        RealEstate GetRealEstate(int Id);
        bool AddRealEstate(RealEstate realEstate);
        bool Save();
    }
}
