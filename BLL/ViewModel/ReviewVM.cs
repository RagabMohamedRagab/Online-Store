using DataModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModel
{
    public partial class ReviewVM
    {
        public int ReviewId { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public bool IsAvailable { get; set; }
        public string CustomerName { get; set; }
        public string Comment { get; set; }
        public decimal Rate { get; set; }
        public DateTime DateAndTime { get; set; }

    }
    public partial class ReviewVM
    {
        public static explicit operator ReviewVM(Review v)
        {
            return new ReviewVM()
            {
                ReviewId = v.ReviewId,
                IsAvailable = v.IsAvailable,
                CustomerName = v.CustomerName,
                Comment = v.Comment,
                Rate = v.Rate,
                DateAndTime = v.DateAndTime
            };
        }
    }
}
