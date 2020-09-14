using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstatesAds.API.Models
{
    public class RealEstateDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int RentingPrice { get; set; }
        public int PurchasePrice { get; set; }
        public bool CanBeRented { get; set; }
        public bool CanBeBought { get; set; }

    }
}
