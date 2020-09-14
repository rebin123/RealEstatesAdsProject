using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstatesAds.API.Models
{
    public class RealEstateDetailsDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Address { get; set; }
        public string Discription { get; set; }
        public int RentingPrice { get; set; }
        public int PurchasePrice { get; set; }
        public bool CanBeRented { get; set; }
        public bool CanBeBought { get; set; }
        public int ConstructionYear { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Type { get; set; }
    }
}
