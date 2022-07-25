using DataModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModel
{
    public partial class ProcessorVM
    {
        public string ProCode { get; set; }
        public string ProName { get; set; }
        public byte ProBrandId { get; set; }
        public string BrandName { get; set; }
        public int ProPrice { get; set; }
        public short ProQuantity { get; set; }
        public byte ProCores { get; set; }
        public string ProSocket { get; set; }
        public byte ProThreads { get; set; }
        public double ProBaseFreq { get; set; }
        public double ProMaxTurboFreq { get; set; }
        public string ProLithography { get; set; }
        public decimal ProRate { get; set; }
        public bool? IsAvailable { get; set; }
        public bool WishList { get; set; }
        public List<RateVM> RateCount { get; set; }
        public List<ReviewVM> Reviews { get; set; }
        public IEnumerable<string> Image { get; set; }


        public virtual BrandVM ProBrand { get; set; }
        //reviews pagination
        public int CurrentPageIndex { get; set; }
        public int PageCount { get; set; }
    }
    public partial class ProcessorVM {
        public static explicit operator ProcessorVM(Processor v)
        {
            return new ProcessorVM()
            {
                ProCode = v.ProCode,
                ProName = v.ProName,
                ProBrandId = v.ProBrandId,
                BrandName = v.ProBrand.BrandName,
                ProPrice = v.ProPrice,
                ProQuantity = v.ProQuantity,
                ProCores =   v.ProCores,
                ProSocket =  v.ProSocket,
                ProThreads = v.ProThreads,
                ProBaseFreq =v.ProBaseFreq,
                ProMaxTurboFreq = v.ProMaxTurboFreq,
                ProLithography =  v.ProLithography
            };
        }
        
    }
   
}
