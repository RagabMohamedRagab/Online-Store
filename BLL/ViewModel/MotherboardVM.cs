using DataModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModel
{
    public partial class MotherboardVM
    {
        public string MotherCode { get; set; }
        public string MotherName { get; set; }
        public byte MotherBrandId { get; set; }
        public string BrandName { get; set; }
        public int MotherPrice { get; set; }
        public short MotherQuantity { get; set; }
        public string MotherSocket { get; set; }
        public decimal MotherRate { get; set; }
        public bool? IsAvailable { get; set; }
        public bool WishList { get; set; }
        public List<RateVM> RateCount { get; set; }
        public List<ReviewVM> Reviews { get; set; }
        public IEnumerable<string> Image { get; set; }

        public virtual BrandVM MotherBrand { get; set; }

        //reviews pagination
        public int CurrentPageIndex { get; set; }
        public int PageCount { get; set; }
    }
    public partial class MotherboardVM
    {
        public static explicit operator MotherboardVM(Motherboard v)
        {
            return new MotherboardVM()
            {
                MotherCode = v.MotherCode,
                MotherName = v.MotherName,
                MotherBrandId = v.MotherBrandId,
                BrandName = v.MotherBrand.BrandName,
                MotherPrice = v.MotherPrice,
                MotherQuantity = v.MotherQuantity,
                MotherSocket = v.MotherSocket
            };
        }

    }
}