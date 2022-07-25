using System;
using System.Collections.Generic;

#nullable disable

namespace DataModel.Models
{
    public partial class Motherboard
    {
        public Motherboard()
        {
            Images = new HashSet<Image>();
            Reviews = new HashSet<Review>();
            Sales = new HashSet<Sale>();
            WishLists = new HashSet<WishList>();
        }

        public string MotherCode { get; set; }
        public string MotherName { get; set; }
        public byte MotherBrandId { get; set; }
        public int MotherPrice { get; set; }
        public short MotherQuantity { get; set; }
        public string MotherSocket { get; set; }
        public bool? IsAvailable { get; set; }

        public virtual Brand MotherBrand { get; set; }
        public virtual ICollection<Image> Images { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<Sale> Sales { get; set; }
        public virtual ICollection<WishList> WishLists { get; set; }
    }
}
