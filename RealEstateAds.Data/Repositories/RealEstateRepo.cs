using Microsoft.EntityFrameworkCore;
using RealEstateAds.Data.Contracts;
using RealEstateAdsEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RealEstateAds.Data.Repositories
{
    public class RealEstateRepo : IRealEstateRepo
    {
        private readonly RealEstateAdsContext context;
        public RealEstateRepo(RealEstateAdsContext context)
        {
            this.context = context;
        }
        public IEnumerable<RealEstate> GetRealEstates(string skip, string take)
        {
            if (skip == null) skip = "0";
            if (take == null) take = "10";
            return context.RealEstates.ToList<RealEstate>()
                .OrderByDescending(o => o.CreatedOn)
                .Skip(int.Parse(skip))
                .Take(int.Parse(take));
        }

        //få med contact och kommentarer
        public RealEstate GetRealEstate(int id) 
        {
            //return context.RealEstates
            //    .Include(c => c.Comments)
            //    .Include(c => c.Contact); 
            return context.RealEstates.FirstOrDefault(a => a.Id == id); 
        }
        public bool Save() => (context.SaveChanges() >= 0);

        public bool AddRealEstate(RealEstate realEstate)
        {
            if (realEstate == null) return false;
            //realEstate.UserId = userId;
            context.RealEstates.Add(realEstate);
            return true;
        }
    }
}
