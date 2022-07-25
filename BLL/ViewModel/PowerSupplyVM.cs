using DataModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModel
{
    public partial class PowerSupplyVM
    {
        public string Psucode { get; set; }
        public string Psuname { get; set; }
        public byte PsubrandId { get; set; }
        public string BrandName { get; set; }
        public int Psuprice { get; set; }
        public short Psuquantity { get; set; }
        public short Psuwatt { get; set; }
        public string Psucertificate { get; set; }
        public decimal Psurate { get; set; }
        public bool? IsAvailable { get; set; }
        public bool WishList { get; set; }
        public List<RateVM> RateCount { get; set; }
        public List<ReviewVM> Reviews { get; set; }
        public IEnumerable<string> Image { get; set; }

        public virtual BrandVM Psubrand { get; set; }

        //reviews pagination
        public int CurrentPageIndex { get; set; }
        public int PageCount { get; set; }
    }
    public partial class PowerSupplyVM
    {
        public static explicit operator PowerSupplyVM(PowerSupply v)
        {
            return new PowerSupplyVM()
            {
                Psucode = v.Psucode,
                Psuname = v.Psuname,
                PsubrandId = v.PsubrandId,
                BrandName = v.Psubrand.BrandName,
                Psuprice = v.Psuprice,
                Psuquantity = v.Psuquantity,
                Psuwatt = v.Psuwatt,
                Psucertificate = v.Psucertificate
            };
        }
    }
}
