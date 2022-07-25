using System;
using System.Collections.Generic;

#nullable disable

namespace DataModel.Models
{
    public partial class Review
    {
        public int ReviewId { get; set; }
        public string CustomerName { get; set; }
        public string Comment { get; set; }
        public DateTime DateAndTime { get; set; }
        public decimal Rate { get; set; }
        public bool IsAvailable { get; set; }
        public string MotherCode { get; set; }
        public string ProCode { get; set; }
        public string RamCode { get; set; }
        public string Vgacode { get; set; }
        public string Psucode { get; set; }
        public string CaseCode { get; set; }
        public string Ssdcode { get; set; }
        public string Hddcode { get; set; }

        public virtual Case CaseCodeNavigation { get; set; }
        public virtual Hdd HddcodeNavigation { get; set; }
        public virtual Motherboard MotherCodeNavigation { get; set; }
        public virtual Processor ProCodeNavigation { get; set; }
        public virtual PowerSupply PsucodeNavigation { get; set; }
        public virtual Ram RamCodeNavigation { get; set; }
        public virtual Ssd SsdcodeNavigation { get; set; }
        public virtual GraphicsCard VgacodeNavigation { get; set; }
    }
}
