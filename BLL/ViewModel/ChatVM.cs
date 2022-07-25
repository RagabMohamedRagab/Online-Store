using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModel
{
    public class ChatVM
    {
        public int MessageId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int? AdminId { get; set; }
        public string AdminName { get; set; }
        public string MessageText { get; set; }
        public string Time { get; set; }
        public string Date { get; set; }
        public bool AdminOrNot { get; set; }
        public bool Seen { get; set; }

    }
}
