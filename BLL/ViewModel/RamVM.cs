using DataModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModel
{
    public partial class RamVM
    {
        public string RamCode { get; set; }
        public string RamName { get; set; }
        public byte RamBrandId { get; set; }
        public string BrandName { get; set; }
        public int RamPrice { get; set; }
        public short RamQuantity { get; set; }
        public byte RamSize { get; set; }
        public short RamFrequency { get; set; }
        public string RamType { get; set; }
        public byte Ramkits { get; set; }
        public decimal RamRate { get; set; }
        public bool? IsAvailable { get; set; }
        public bool WishList { get; set; }
        public List<RateVM> RateCount { get; set; }
        public List<ReviewVM> Reviews { get; set; }
        public IEnumerable<string> Image { get; set; }

        public virtual BrandVM RamBrand { get; set; }

        //reviews pagination
        public int CurrentPageIndex { get; set; }
        public int PageCount { get; set; }
    }
    public partial class RamVM
    {
        public static explicit operator RamVM(Ram v)
        {
            return new RamVM()
            {
                RamCode = v.RamCode,
                RamName = v.RamName,
                RamBrandId = v.RamBrandId,
                BrandName = v.RamBrand.BrandName,
                RamPrice = v.RamPrice,
                RamQuantity = v.RamQuantity,
                RamSize = v.RamSize,
                RamFrequency = v.RamFrequency,
                RamType = v.RamType,
                Ramkits = v.Ramkits
            };
        }

    }
}
