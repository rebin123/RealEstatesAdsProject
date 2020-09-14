using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RealEstateAdsEntities
{
    public class RealEstate
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(100)]
        [Required]
        public string Title { get; set; }
        [Required]
        [MaxLength(100)]
        public string Address { get; set; }
        [Required]
        [MaxLength(100)]
        public string Discription { get; set; }

        public int RentingPrice { get; set; }
        public int PurchasePrice { get; set; }

        public bool CanBeRented { get; set; }
        public bool CanBeBought { get; set; }

        [Required]
        public int ConstructionYear { get; set; }
        [Required]
        public DateTime CreatedOn { get; set; }
        [Required]
        public string Type { get; set; }

        [Required]
        public string Contact { get; set; }


        [ForeignKey("UserId")]
        public User User { get; set; }
        public int UserId { get; set; }
        public List<Comment> Comments { get; set; } = new List<Comment>();

    }
}
