using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RealEstateAdsEntities
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedOn { get; set; }

        [ForeignKey("RealEstateId")]
        public RealEstate RealEstate { get; set; }
        public int? RealEstateId { get; set; }

        [ForeignKey("UserId")]
        public User Creator { get; set; }
        public int? UserId { get; set; }
    }
}
