using System;
using System.Collections.Generic;

#nullable disable

namespace DataModel.Models
{
    public partial class Ssd
    {
        public Ssd()
        {
            Images = new HashSet<Image>();
            Reviews = new HashSet<Review>();
            Sales = new HashSet<Sale>();
            WishLists = new HashSet<WishList>();
        }

        public string Ssdcode { get; set; }
        public string Ssdname { get; set; }
        public byte SsdbrandId { get; set; }
        public int Ssdprice { get; set; }
        public short Ssdquantity { get; set; }
        public short Ssdsize { get; set; }
        public string Ssdinterface { get; set; }
        public bool? IsAvailable { get; set; }

        public virtual Brand Ssdbrand { get; set; }
        public virtual ICollection<Image> Images { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<Sale> Sales { get; set; }
        public virtual ICollection<WishList> WishLists { get; set; }
    }
}
