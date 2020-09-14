using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstatesAds.API.Models
{
    public class CreateRealEstateDto
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Discription { get; set; }
        [Required]
        public int RentingPrice { get; set; }
        [Required]
        public int PurchasePrice { get; set; }
        [Required]
        public string Contact { get; set; }
        [Required]
        public int ConstructionYear { get; set; }
        [Required]
        public string Type { get; set; }
    }
}
