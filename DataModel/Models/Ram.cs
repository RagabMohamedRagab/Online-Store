using System;
using System.Collections.Generic;

#nullable disable

namespace DataModel.Models
{
    public partial class Ram
    {
        public Ram()
        {
            Images = new HashSet<Image>();
            Reviews = new HashSet<Review>();
            Sales = new HashSet<Sale>();
            WishLists = new HashSet<WishList>();
        }

        public string RamCode { get; set; }
        public string RamName { get; set; }
        public byte RamBrandId { get; set; }
        public int RamPrice { get; set; }
        public short RamQuantity { get; set; }
        public byte RamSize { get; set; }
        public short RamFrequency { get; set; }
        public string RamType { get; set; }
        public byte Ramkits { get; set; }
        public bool? IsAvailable { get; set; }

        public virtual Brand RamBrand { get; set; }
        public virtual ICollection<Image> Images { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<Sale> Sales { get; set; }
        public virtual ICollection<WishList> WishLists { get; set; }
    }
}
