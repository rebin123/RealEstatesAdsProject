using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstatesAds.API.Models
{
    public class UserDto
    {
        public string UserName { get; set; }
        public double? AverageRating { get; set; }
        public int RealEstates { get; set; }
        public int Comments { get; set; }
    }
}
