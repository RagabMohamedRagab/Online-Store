using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModel
{
    public class WishListVM
    {
        public int UserID { get; set; }
        public string ProductCode { get; set; }
        public string Image { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }

        public string IsAvailable { get; set; }
        public int ProductPrice { get; set; }
    }
}
