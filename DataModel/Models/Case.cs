using System;
using System.Collections.Generic;

#nullable disable

namespace DataModel.Models
{
    public partial class Case
    {
        public Case()
        {
            Images = new HashSet<Image>();
            Reviews = new HashSet<Review>();
            Sales = new HashSet<Sale>();
            WishLists = new HashSet<WishList>();
        }

        public string CaseCode { get; set; }
        public string CaseName { get; set; }
        public byte CaseBrandId { get; set; }
        public int CasePrice { get; set; }
        public short CaseQuantity { get; set; }
        public string CaseFactorySize { get; set; }
        public bool? IsAvailable { get; set; }

        public virtual Brand CaseBrand { get; set; }
        public virtual ICollection<Image> Images { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<Sale> Sales { get; set; }
        public virtual ICollection<WishList> WishLists { get; set; }
    }
}
