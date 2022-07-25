using System;
using System.Collections.Generic;

#nullable disable

namespace DataModel.Models
{
    public partial class Message
    {
        public int MessageId { get; set; }
        public int UserId { get; set; }
        public int? AdminId { get; set; }
        public string MessageText { get; set; }
        public DateTime Time { get; set; }
        public bool AdminOrNot { get; set; }
        public bool Seen { get; set; }

        public virtual User Admin { get; set; }
        public virtual User User { get; set; }
    }
}
