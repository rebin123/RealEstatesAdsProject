using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RealEstateAdsEntities
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(100)]
        public string Email { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(100)]
        public string Password { get; set; }
        public double? AverageRating { get; set; }
        public List<Comment> Comments { get; set; } = new List<Comment>();
    }
}
