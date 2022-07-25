using DataModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModel
{
    public partial class CaseVM
    {
        public string CaseCode { get; set; }
        public string CaseName { get; set; }
        public byte CaseBrandId { get; set; }
        public string BrandName { get; set; }
        public int CasePrice { get; set; }
        public short CaseQuantity { get; set; }
        public string CaseFactorySize { get; set; }
        public decimal CaseRate { get; set; }
        public bool? IsAvailable { get; set; }
        public bool WishList { get; set; }

        public List<RateVM> RateCount { get; set; }
        public List<ReviewVM> Reviews { get; set; }
        public IEnumerable<string> Image { get; set; }
        public virtual BrandVM CaseBrand { get; set; }

        //reviews pagination
        public int CurrentPageIndex { get; set; }
        public int PageCount { get; set; }

    }
    public partial class CaseVM
    {
        public static explicit operator CaseVM(Case v)
        {
            return new CaseVM()
            {
                CaseCode = v.CaseCode,
                CaseName = v.CaseName,
                CaseBrandId = v.CaseBrandId,
                BrandName = v.CaseBrand.BrandName,
                CasePrice = v.CasePrice,
                CaseQuantity = v.CaseQuantity,
                CaseFactorySize = v.CaseFactorySize,
                IsAvailable = v.IsAvailable
            };
        }
    }
}
