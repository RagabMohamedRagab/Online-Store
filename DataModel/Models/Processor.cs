using System;
using System.Collections.Generic;

#nullable disable

namespace DataModel.Models
{
    public partial class Processor
    {
        public Processor()
        {
            Images = new HashSet<Image>();
            Reviews = new HashSet<Review>();
            Sales = new HashSet<Sale>();
            WishLists = new HashSet<WishList>();
        }

        public string ProCode { get; set; }
        public string ProName { get; set; }
        public byte ProBrandId { get; set; }
        public int ProPrice { get; set; }
        public short ProQuantity { get; set; }
        public byte ProCores { get; set; }
        public string ProSocket { get; set; }
        public byte ProThreads { get; set; }
        public double ProBaseFreq { get; set; }
        public double ProMaxTurboFreq { get; set; }
        public string ProLithography { get; set; }
        public bool? IsAvailable { get; set; }

        public virtual Brand ProBrand { get; set; }
        public virtual ICollection<Image> Images { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<Sale> Sales { get; set; }
        public virtual ICollection<WishList> WishLists { get; set; }
    }
}
