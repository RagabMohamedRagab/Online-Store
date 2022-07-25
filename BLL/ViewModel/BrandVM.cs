using DataModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModel
{
   public partial class BrandVM
    {
        public byte BrandId { get; set; }
        public int BrandNum { get; set; }
        public string BrandName { get; set; }
    }
    public partial class BrandVM
    {
        public static explicit operator BrandVM(Brand v)
        {
            return new BrandVM()
            {
                BrandId = v.BrandId,
                BrandName = v.BrandName,
            };
        }
    }
}
