using DataModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModel
{
    public partial class HddVM
    {
        public string Hddcode { get; set; }
        public string Hddname { get; set; }
        public byte HddbrandId { get; set; }
        public string BrandName { get; set; }
        public int Hddprice { get; set; }
        public short Hddquantity { get; set; }
        public short Hddsize { get; set; }
        public short Hddrpm { get; set; }
        public string Hddtype { get; set; }
        public decimal Hddrate { get; set; }
        public bool? IsAvailable { get; set; }
        public bool WishList { get; set; }
        public List<RateVM> RateCount { get; set; }
        public List<ReviewVM> Reviews { get; set; }
        public IEnumerable<string> Image { get; set; }

        public virtual BrandVM Hddbrand { get; set; }

        //reviews pagination
        public int CurrentPageIndex { get; set; }
        public int PageCount { get; set; }
    }
    public partial class HddVM
    {
        public static explicit operator HddVM(Hdd v)
        {
            return new HddVM()
            {
                Hddcode = v.Hddcode,
                Hddname = v.Hddname,
                HddbrandId = v.HddbrandId,
                BrandName = v.Hddbrand.BrandName,
                Hddprice = v.Hddprice,
                Hddquantity = v.Hddquantity,
                Hddsize = v.Hddsize,
                Hddrpm = v.Hddrpm,
                Hddtype = v.Hddtype
            };
        }

    }
}
