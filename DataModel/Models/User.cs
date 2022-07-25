using System;
using System.Collections.Generic;

#nullable disable

namespace DataModel.Models
{
    public partial class User
    {
        public User()
        {
            MessageAdmins = new HashSet<Message>();
            MessageUsers = new HashSet<Message>();
            Sales = new HashSet<Sale>();
        }

        public int UserId { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Phone { get; set; }
        public bool IsAdmin { get; set; }

        public virtual ICollection<Message> MessageAdmins { get; set; }
        public virtual ICollection<Message> MessageUsers { get; set; }
        public virtual ICollection<Sale> Sales { get; set; }
    }
}
