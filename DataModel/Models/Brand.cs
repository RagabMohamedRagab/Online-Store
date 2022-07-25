using System;
using System.Collections.Generic;

#nullable disable

namespace DataModel.Models
{
    public partial class Brand
    {
        public Brand()
        {
            Cases = new HashSet<Case>();
            GraphicsCards = new HashSet<GraphicsCard>();
            Hdds = new HashSet<Hdd>();
            Motherboards = new HashSet<Motherboard>();
            PowerSupplies = new HashSet<PowerSupply>();
            Processors = new HashSet<Processor>();
            Rams = new HashSet<Ram>();
            Ssds = new HashSet<Ssd>();
        }

        public byte BrandId { get; set; }
        public string BrandName { get; set; }

        public virtual ICollection<Case> Cases { get; set; }
        public virtual ICollection<GraphicsCard> GraphicsCards { get; set; }
        public virtual ICollection<Hdd> Hdds { get; set; }
        public virtual ICollection<Motherboard> Motherboards { get; set; }
        public virtual ICollection<PowerSupply> PowerSupplies { get; set; }
        public virtual ICollection<Processor> Processors { get; set; }
        public virtual ICollection<Ram> Rams { get; set; }
        public virtual ICollection<Ssd> Ssds { get; set; }
    }
}
