using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstatesAds.API.Models
{
    public class CreateCommentDto
    {
        [Required]
        public int RealEstateId { get; set; }
        [Required]
        public string Content { get; set; }
    }
}
