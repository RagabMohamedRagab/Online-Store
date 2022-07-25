using DataModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModel
{
    public partial class GraphicsCardVM
    {
        public string Vgacode { get; set; }
        public string Vganame { get; set; }
        public byte VgabrandId { get; set; }
        public string BrandName { get; set; }
        public int Vgaprice { get; set; }
        public short Vgaquantity { get; set; }
        public byte Vram { get; set; }
        public decimal Vgarate { get; set; }
        public bool? IsAvailable { get; set; }
        public bool WishList { get; set; }
        public List<RateVM> RateCount { get; set; }
        public List<ReviewVM> Reviews { get; set; }
        public IEnumerable<string> Image { get; set; }

        public virtual BrandVM Vgabrand { get; set; }

        //reviews pagination
        public int CurrentPageIndex { get; set; }
        public int PageCount { get; set; }

    }
    public partial class GraphicsCardVM
    {
        public static explicit operator GraphicsCardVM(GraphicsCard v)
        {
            return new GraphicsCardVM()
            {
                Vgacode = v.Vgacode,
                Vganame = v.Vganame,
                VgabrandId = v.VgabrandId,
                BrandName = v.Vgabrand.BrandName,
                Vgaprice = v.Vgaprice,
                Vgaquantity = v.Vgaquantity,
                Vram = v.Vram
            };
        }
    }
}
