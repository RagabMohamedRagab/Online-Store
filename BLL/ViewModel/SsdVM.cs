using DataModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModel
{
    public partial class SsdVM
    {
        public string Ssdcode { get; set; }
        public string Ssdname { get; set; }
        public byte SsdbrandId { get; set; }
        public string BrandName { get; set; }
        public int Ssdprice { get; set; }
        public short Ssdquantity { get; set; }
        public short Ssdsize { get; set; }
        public string Ssdinterface { get; set; }
        public decimal Ssdrate { get; set; }
        public bool? IsAvailable { get; set; }
        public bool WishList { get; set; }
        public List<RateVM> RateCount { get; set; }
        public List<ReviewVM> Reviews { get; set; }
        public IEnumerable<string> Image { get; set; }

        public virtual BrandVM Ssdbrand { get; set; }

        //reviews pagination
        public int CurrentPageIndex { get; set; }
        public int PageCount { get; set; }
    }
    public partial class SsdVM
    {
        public static explicit operator SsdVM(Ssd v)
        {
            return new SsdVM()
            {
                Ssdcode = v.Ssdcode,
                Ssdname = v.Ssdname,
                SsdbrandId = v.SsdbrandId,
                BrandName = v.Ssdbrand.BrandName,
                Ssdprice = v.Ssdprice,
                Ssdquantity = v.Ssdquantity,
                Ssdsize = v.Ssdsize,
                Ssdinterface = v.Ssdinterface
            };
        }

    }
}
