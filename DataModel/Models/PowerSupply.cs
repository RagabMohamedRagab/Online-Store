using System;
using System.Collections.Generic;

#nullable disable

namespace DataModel.Models
{
    public partial class PowerSupply
    {
        public PowerSupply()
        {
            Images = new HashSet<Image>();
            Reviews = new HashSet<Review>();
            Sales = new HashSet<Sale>();
            WishLists = new HashSet<WishList>();
        }

        public string Psucode { get; set; }
        public string Psuname { get; set; }
        public byte PsubrandId { get; set; }
        public int Psuprice { get; set; }
        public short Psuquantity { get; set; }
        public short Psuwatt { get; set; }
        public string Psucertificate { get; set; }
        public bool? IsAvailable { get; set; }

        public virtual Brand Psubrand { get; set; }
        public virtual ICollection<Image> Images { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<Sale> Sales { get; set; }
        public virtual ICollection<WishList> WishLists { get; set; }
    }
}
