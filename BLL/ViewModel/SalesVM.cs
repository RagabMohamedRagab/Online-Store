using DataModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModel
{
    public partial class SalesVM
    {
        public int? UserID { get; set; }
        public string ProductCode { get; set; }
        public byte? ProductQuantity { get; set; }
        public int? TotalPrice { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public DateTime? DateAndTime { get; set; }
    }
    public partial class SalesVM
    {
        public static explicit operator SalesVM(Sale v)
        {
            return new SalesVM()
            {
                UserID = v.UserId,
                Address = v.Address,
                DateAndTime = v.DateAndTime,
                ProductQuantity = v.ProductQuantity,
                TotalPrice = v.TotalPrice
            };
        }
    }
}
