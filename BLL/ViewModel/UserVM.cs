using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModel
{
    public partial class UserVM
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public int Telephone { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
        public DateTime? LatestBuyTime { get; set; }
        public int NumberOfTimes { get; set; }
        public int Quantity { get; set; }
    }
}
