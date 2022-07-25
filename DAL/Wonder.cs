using BLL.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel.Models;
using System.Diagnostics;
using System.Linq.Dynamic.Core;
using System.Globalization;

namespace DAL {
    public class Wonder : IWonder {
        readonly WonderHardwareContext _wonder;

        public Wonder(WonderHardwareContext wonder)
        {
            _wonder = wonder;
        }

        #region GetAllProducts

        public List<ProcessorVM> GetAllProcessors(int userid = 0, string deleteddata = null)
        {
            List<Processor> Processor = new();
            if (deleteddata == null)
            {
                Processor = _wonder.Processors.Where(x => x.IsAvailable == true).ToList();
            }
            else
            {
                Processor = _wonder.Processors.Where(x => x.IsAvailable == false).ToList();
            }

            List<ProcessorVM> PR = new List<ProcessorVM>();
            foreach (var item in Processor)
            {
                ProcessorVM obj = (ProcessorVM)item;
                obj.Image = _wonder.Images.Where(x => x.ProCode == obj.ProCode).Select(x => x.ProductImage).Take(1).ToList();
                obj.WishList = _wonder.WishLists.Any(x => x.ProCode == item.ProCode && x.UserId == userid && x.IsAdded == true);
                obj.ProRate = 0;
                //Total Rate from Reviews
                List<decimal> Rates = _wonder.Reviews.Where(x => x.ProCode == item.ProCode && x.Rate != 0 && x.IsAvailable == true).Select(x => x.Rate).ToList();
                if (Rates.Count() != 0)
                {
                    obj.ProRate = Rates.Sum() / Rates.Count();
                }
                PR.Add(obj);
            }
            return PR;
        }
        public List<MotherboardVM> GetAllMotherboard(int userid = 0, string deleteddata = null)
        {
            List<Motherboard> Motherboard = new();
            if (deleteddata == null)
            {
                Motherboard = _wonder.Motherboards.Where(x => x.IsAvailable == true).ToList();
            }
            else
            {
                Motherboard = _wonder.Motherboards.Where(x => x.IsAvailable == false).ToList();
            }
            List<MotherboardVM> MB = new List<MotherboardVM>();
            foreach (var item in Motherboard)
            {
                MotherboardVM obj = (MotherboardVM)item;
                obj.Image = _wonder.Images.Where(x => x.MotherCode == obj.MotherCode).Select(x => x.ProductImage).Take(1).ToList();
                obj.MotherRate = 0;
                //Total Rate from Reviews
                List<decimal> Rates = _wonder.Reviews.Where(x => x.MotherCode == item.MotherCode && x.IsAvailable == true && x.Rate != 0).Select(x => x.Rate).ToList();
                if (Rates.Count() != 0)
                {
                    obj.MotherRate = Rates.Sum() / Rates.Count();
                }
                MB.Add(obj);
            }
            return MB;
        }
        public List<HddVM> GetAllHDD(int userid = 0, string deleteddata = null)
        {
            List<Hdd> HDD = new();
            if (deleteddata == null)
            {
                HDD = _wonder.Hdds.Where(x => x.IsAvailable == true).ToList();
            }
            else
            {
                HDD = _wonder.Hdds.Where(x => x.IsAvailable == false).ToList();
            }

            List<HddVM> HD = new List<HddVM>();
            foreach (var item in HDD)
            {
                HddVM obj = (HddVM)item;
                obj.WishList = _wonder.WishLists.Any(x => x.Hddcode == item.Hddcode && x.UserId == userid && x.IsAdded == true);
                obj.Image = _wonder.Images.Where(x => x.Hddcode == obj.Hddcode).Select(x => x.ProductImage).Take(1).ToList();
                obj.Hddrate = 0;
                //Total Rate from Reviews
                List<decimal> Rates = _wonder.Reviews.Where(x => x.Hddcode == item.Hddcode && x.IsAvailable == true && x.Rate != 0).Select(x => x.Rate).ToList();
                if (Rates.Count() != 0)
                {
                    obj.Hddrate = Rates.Sum() / Rates.Count();
                }
                HD.Add(obj);
            }
            return HD;
        }
        public List<RamVM> GetAllRAM(int userid = 0, string deleteddata = null)
        {
            List<Ram> Ram = new();
            if (deleteddata == null)
            {
                Ram = _wonder.Rams.Where(x => x.IsAvailable == true).ToList();
            }
            else
            {
                Ram = _wonder.Rams.Where(x => x.IsAvailable == false).ToList();
            }
            List<RamVM> RM = new List<RamVM>();
            foreach (var item in Ram)
            {
                RamVM obj = (RamVM)item;
                obj.WishList = _wonder.WishLists.Any(x => x.RamCode == item.RamCode && x.UserId == userid && x.IsAdded == true);
                obj.Image = _wonder.Images.Where(x => x.RamCode == obj.RamCode).Select(x => x.ProductImage).Take(1).ToList();
                obj.RamRate = 0;
                //Total Rate from Reviews
                List<decimal> Rates = _wonder.Reviews.Where(x => x.RamCode == item.RamCode && x.IsAvailable == true && x.Rate != 0).Select(x => x.Rate).ToList();
                if (Rates.Count() != 0)
                {
                    obj.RamRate = Rates.Sum() / Rates.Count();
                }
                RM.Add(obj);
            }
            return RM;
        }
        public List<SsdVM> GetAllSSD(int userid = 0, string deleteddata = null)
        {
            List<Ssd> Ssd = new();
            if (deleteddata == null)
            {
                Ssd = _wonder.Ssds.Where(x => x.IsAvailable == true).ToList();
            }
            else
            {
                Ssd = _wonder.Ssds.Where(x => x.IsAvailable == false).ToList();
            }
            List<SsdVM> SD = new List<SsdVM>();
            foreach (var item in Ssd)
            {
                SsdVM obj = (SsdVM)item;
                obj.WishList = _wonder.WishLists.Any(x => x.Ssdcode == item.Ssdcode && x.UserId == userid && x.IsAdded == true);
                obj.Image = _wonder.Images.Where(x => x.Ssdcode == obj.Ssdcode).Select(x => x.ProductImage).Take(1).ToList();
                obj.Ssdrate = 0;
                //Total Rate from Reviews
                List<decimal> Rates = _wonder.Reviews.Where(x => x.Ssdcode == item.Ssdcode && x.IsAvailable == true && x.Rate != 0).Select(x => x.Rate).ToList();
                if (Rates.Count() != 0)
                {
                    obj.Ssdrate = Rates.Sum() / Rates.Count();
                }
                SD.Add(obj);
            }
            return SD;
        }
        public List<GraphicsCardVM> GetAllCard(int userid = 0, string deleteddata = null)
        {
            List<GraphicsCard> GraphicsCard = new();
            if (deleteddata == null)
            {
                GraphicsCard = _wonder.GraphicsCards.Where(x => x.IsAvailable == true).ToList();
            }
            else
            {
                GraphicsCard = _wonder.GraphicsCards.Where(x => x.IsAvailable == false).ToList();
            }
            List<GraphicsCardVM> GC = new List<GraphicsCardVM>();
            foreach (var item in GraphicsCard)
            {
                GraphicsCardVM obj = (GraphicsCardVM)item;
                obj.WishList = _wonder.WishLists.Any(x => x.Vgacode == item.Vgacode && x.UserId == userid && x.IsAdded == true);
                obj.Image = _wonder.Images.Where(x => x.Vgacode == obj.Vgacode).Select(x => x.ProductImage).Take(1).ToList();
                obj.Vgarate = 0;
                //Total Rate from Reviews
                List<decimal> Rates = _wonder.Reviews.Where(x => x.Vgacode == item.Vgacode && x.IsAvailable == true && x.Rate != 0).Select(x => x.Rate).ToList();
                if (Rates.Count() != 0)
                {
                    obj.Vgarate = Rates.Sum() / Rates.Count();
                }
                GC.Add(obj);
            }
            return GC;
        }
        public List<CaseVM> GetAllCase(int userid = 0, string deleteddata = null)
        {
            List<Case> Case = new List<Case>();
            if (deleteddata == null)
                Case = _wonder.Cases.Include(b => b.Images).Where(x => x.IsAvailable == true).ToList();
            else
                Case = _wonder.Cases.Include(b => b.Images).Where(x => x.IsAvailable == false).ToList();

            List<CaseVM> CA = new List<CaseVM>();
            foreach (var item in Case)
            {
                CaseVM obj = (CaseVM)item;
                obj.WishList = _wonder.WishLists.Any(x => x.CaseCode == item.CaseCode && x.UserId == userid && x.IsAdded == true);
                obj.Image = _wonder.Images.Where(x => x.CaseCode == obj.CaseCode).Select(x => x.ProductImage).Take(1).ToList();
                obj.CaseRate = 0;
                //Total Rate from Reviews
                List<decimal> Rates = _wonder.Reviews.Where(x => x.CaseCode == item.CaseCode && x.IsAvailable == true && x.Rate != 0).Select(x => x.Rate).ToList();
                if (Rates.Count() != 0)
                {
                    obj.CaseRate = Rates.Sum() / Rates.Count();
                }
                CA.Add(obj);
            }
            return CA;
        }
        public List<PowerSupplyVM> GetAllPowerSuply(int userid = 0, string deleteddata = null)
        {
            List<PowerSupply> PowerSupply = new();
            if (deleteddata == null)
            {
                PowerSupply = _wonder.PowerSupplies.Where(x => x.IsAvailable == true).ToList();
            }
            else
            {
                PowerSupply = _wonder.PowerSupplies.Where(x => x.IsAvailable == false).ToList();
            }
            List<PowerSupplyVM> PS = new List<PowerSupplyVM>();
            foreach (var item in PowerSupply)
            {
                PowerSupplyVM obj = (PowerSupplyVM)item;
                obj.WishList = _wonder.WishLists.Any(x => x.Psucode == item.Psucode && x.UserId == userid && x.IsAdded == true);
                obj.Image = _wonder.Images.Where(x => x.Psucode == obj.Psucode).Select(x => x.ProductImage).Take(1).ToList();
                obj.Psurate = 0;
                //Total Rate from Reviews
                List<decimal> Rates = _wonder.Reviews.Where(x => x.Psucode == item.Psucode && x.IsAvailable == true && x.Rate != 0).Select(x => x.Rate).ToList();
                if (Rates.Count() != 0)
                {
                    obj.Psurate = Rates.Sum() / Rates.Count();
                }
                PS.Add(obj);
            }
            return PS;
        }

        #endregion

        #region StorePage

        #region Processor
        public IEnumerable<BrandVM> GetProcessorBrandNamesAndNumbers()
        {
            IList<BrandVM> brandVMs = _wonder.Brands.ToList().Join(GetAllProcessors(),
                                        brand => brand.BrandId,
                                        processor => processor.ProBrandId,
                                        (brand, processor) => new BrandVM
                                        {
                                            BrandName = brand.BrandName,
                                            BrandNum = _wonder.Processors.Where(brandNum => brandNum.ProBrandId == brand.BrandId).Count()
                                        }

                ).GroupBy(i => i.BrandName).Select(i => i.FirstOrDefault()).ToList();
            return brandVMs;
        }
        public IEnumerable<ProcessorVM> GetProcessorProductsByPrice(IEnumerable<ProcessorVM> processorVMs, int Id,int userid=0)
        {
            IList<ProcessorVM> processors = null;
            if (Id == 1)
            {
                processors = processorVMs.OrderByDescending(PVM => PVM.ProPrice).ToList();
            }
            else if (Id == 2)
            {
                processors = processorVMs.OrderBy(PVM => PVM.ProPrice).ToList();
            }
            else
            {
                processors = processorVMs.ToList();
            }
            return processors;
        }
        public IEnumerable<ProcessorVM> GetProcessorProductsByBrand(string[] BName, int id, int min=100, int max= 50000, int userid = 0)
        {
            IEnumerable<ProcessorVM> Data = from Pro in GetAllProcessors(userid)
                                            join brand in BName
                                            on Pro.BrandName.Trim() equals brand
                                            select Pro;
            if (min == 0 && max == 0)
            {

                return GetProcessorProductsByPrice(Data, id);
            }

            return GetProcessorProductsByPrice(Data, id).Where(pro => pro.ProPrice >= min && pro.ProPrice <= max);
        }
       
        public IEnumerable<ProcessorVM> GetProcessorDependentOnSort(int id, int userid = 0)
        {
            if (id == 0)
            {
                return GetAllProcessors(userid).ToList();
            }
            return GetProcessorProductsByPrice(GetAllProcessors(userid), id, userid);
        }
        public IEnumerable<ProcessorVM> GetProcessorPriceDependentOnBrand( int sort, int min = 100, int max = 50000, int userid = 0)
        {
            IEnumerable<ProcessorVM> processors = null;
            if (min == 0 && max == 0)
            {
                processors = GetProcessorDependentOnSort(sort, userid).ToList();
            }
            else
            {

                processors = GetAllProcessors(userid).Where(processor => processor.ProPrice >= min && processor.ProPrice <= max);
            }
            return GetProcessorProductsByPrice(processors, sort);
        }
        #endregion

        #region MotherBoard
        public IEnumerable<BrandVM> GetMotherboardBrandNamesAndNumbers(int userid = 0)
        {
            IList<BrandVM> brandVMs = _wonder.Brands.ToList().Join(GetAllMotherboard(userid),
                                        brand => brand.BrandId,
                                        month => month.MotherBrandId,
                                        (brand, month) => new BrandVM
                                        {
                                            BrandName = brand.BrandName,
                                            BrandNum = _wonder.Motherboards.Where(brandNum => brandNum.MotherBrandId == brand.BrandId).Count()
                                        }

                ).GroupBy(i => i.BrandName).Select(i => i.FirstOrDefault()).ToList();
            return brandVMs;
        }
        public IEnumerable<MotherboardVM> GetMotherboardProductsByPrice(IEnumerable<MotherboardVM> motherboardVMs, int Id, int userid = 0)
        {
            IList<MotherboardVM> motherboards = null;
            if (Id == 1)
            {
                motherboards = motherboardVMs.OrderByDescending(MVM => MVM.MotherPrice).ToList();
            }
            else if (Id == 2)
            {
                motherboards = motherboardVMs.OrderBy(MVM => MVM.MotherPrice).ToList();
            }
            else
            {
                motherboards = motherboardVMs.ToList();
            }
            return motherboards;
        }
        public IEnumerable<MotherboardVM> GetMotherboardProductsByBrand(string[] BName, int id, int min=100, int max=50000, int userid = 0)
        {
            IEnumerable<MotherboardVM> Data = from Pro in GetAllMotherboard(userid)
                                            join brand in BName
                                            on Pro.BrandName.Trim() equals brand
                                            select Pro;
            if (min == 0 && max == 0)
            {

                return GetMotherboardProductsByPrice(Data, id, userid);
            }

            return GetMotherboardProductsByPrice(Data, id, userid).Where(moth => moth.MotherPrice >= min && moth.MotherPrice <= max);
        }
       
        public IEnumerable<MotherboardVM> GetMotherboardDependentOnSort(int id, int userid = 0)
        {
            if (id == 0)
            {
                return GetAllMotherboard(userid).ToList();
            }
            return GetMotherboardProductsByPrice(GetAllMotherboard(userid), id, userid);
        }
        public IEnumerable<MotherboardVM> GetMotherboardPriceDependentOnBrand( int sort,int min=100, int max=50000, int userid = 0)
        {
            IEnumerable<MotherboardVM> motherboards = null;
            if (min == 0 && max == 0)
            {

                motherboards = GetMotherboardDependentOnSort(sort).ToList();
            }
            else
            {

                motherboards = GetAllMotherboard().Where(moth => moth.MotherPrice >= min && moth.MotherPrice <= max);
            }
            return GetMotherboardProductsByPrice(motherboards, sort);
        }

        #endregion

        #region HDD
        public IEnumerable<BrandVM> GetHDDBrandNamesAndNumbers(int userid = 0)
        {
            IList<BrandVM> brandVMs = _wonder.Brands.ToList().Join(GetAllHDD(userid),
                                        brand => brand.BrandId,
                                        hdd => hdd.HddbrandId,
                                        (brand, hdd) => new BrandVM
                                        {
                                            BrandName = brand.BrandName,
                                            BrandNum = _wonder.Hdds.Where(brandNum => brandNum.HddbrandId == brand.BrandId).Count()
                                        }

                ).GroupBy(i => i.BrandName).Select(i => i.FirstOrDefault()).ToList();
            return brandVMs;
        }
        public IEnumerable<HddVM> GetHDDProductsByPrice(IEnumerable<HddVM> hddVMs, int Id, int userid = 0)
        {
            IList<HddVM> hdds = null;
            if (Id == 1)
            {
                hdds = hddVMs.OrderByDescending(PVM => PVM.Hddprice).ToList();
            }
            else if (Id == 2)
            {
                hdds = hddVMs.OrderBy(PVM => PVM.Hddprice).ToList();
            }
            else
            {
                hdds = hddVMs.ToList();
            }
            return hdds;
        }
        public IEnumerable<HddVM> GetHDDProductsByBrand(string[] BName, int id, int min=100, int max=50000,int userid = 0)
        {
            IEnumerable<HddVM> Data = from hdd in GetAllHDD(userid)
                                      join brand in BName
                                      on hdd.BrandName.Trim() equals brand
                                      select hdd;
            if (min == 0 && max == 0)
            {

                return GetHDDProductsByPrice(Data, id, userid);
            }

            return GetHDDProductsByPrice(Data, id).Where(hdd => hdd.Hddprice >= min && hdd.Hddprice <= max);
        }
      
        public IEnumerable<HddVM> GetHDDDependentOnSort(int id,int userid = 0)
        {
            if (id == 0)
            {
                return GetAllHDD(userid).ToList();
            }
            return GetHDDProductsByPrice(GetAllHDD(userid), id, userid);
        }
        public IEnumerable<HddVM> GetHDDPriceDependentOnBrand(int sort, int min = 100, int max = 50000,int userid = 0)
        {
            IEnumerable<HddVM> hdds = null;
            if (min == 0 && max == 0)
            {

                hdds = GetHDDDependentOnSort(sort, userid).ToList();
            }
            else
            {

                hdds = GetAllHDD(userid).Where(hdd => hdd.Hddprice >= min && hdd.Hddprice <= max);
            }
            return GetHDDProductsByPrice(hdds, sort, userid);
        }
        #endregion

        #region RAM
        public IEnumerable<BrandVM> GetRAMBrandNamesAndNumbers(int userid = 0)
        {
            IList<BrandVM> brandVMs = _wonder.Brands.ToList().Join(GetAllRAM(userid),
                                      brand => brand.BrandId,
                                      ram => ram.RamBrandId,
                                      (brand, ram) => new BrandVM
                                      {
                                          BrandName = brand.BrandName,
                                          BrandNum = _wonder.Rams.Where(brandNum => brandNum.RamBrandId == brand.BrandId).Count()
                                      }

              ).GroupBy(i => i.BrandName).Select(i => i.FirstOrDefault()).ToList();
            return brandVMs;
        }
        public IEnumerable<RamVM> GetRAMProductsByPrice(IEnumerable<RamVM> RamVMs, int Id,int userid=0)
        {
            IList<RamVM> Rams = null;
            if (Id == 1)
            {
                Rams = RamVMs.OrderByDescending(PVM => PVM.RamPrice).ToList();
            }
            else if (Id == 2)
            {
                Rams = RamVMs.OrderBy(PVM => PVM.RamPrice).ToList();
            }
            else
            {
                Rams = RamVMs.ToList();
            }
            return Rams;
        }
        public IEnumerable<RamVM> GetRAMProductsByBrand(string[] BName,  int id, int min=100, int max=50000,int userid=0)
        {
            IEnumerable<RamVM> Data = from ram in GetAllRAM(userid)
                                      join brand in BName
                                      on ram.BrandName.Trim() equals brand
                                      select ram;
            if (min == 0 && max == 0)
            {

                return GetRAMProductsByPrice(Data, id, userid);
            }

            return GetRAMProductsByPrice(Data, id, userid).Where(ram => ram.RamPrice >= min && ram.RamPrice <= max);
        }
        public IEnumerable<RamVM> GetRAMDependentOnSort(int id, int userid = 0)
        {
            if (id == 0)
            {
                return GetAllRAM(userid).ToList();
            }
            return GetRAMProductsByPrice(GetAllRAM(userid), id, userid);
        }
        public IEnumerable<RamVM> GetRAMPriceDependentOnBrand( int sort, int min=100, int max=50000, int userid = 0)
        {
            IEnumerable<RamVM> ramVMs = null;
            if (min == 0 && max == 0)
            {

                ramVMs = GetRAMDependentOnSort(sort, userid).ToList();
            }
            else
            {

                ramVMs = GetAllRAM(userid).Where(ram => ram.RamPrice >= min && ram.RamPrice <= max);
            }
            return GetRAMProductsByPrice(ramVMs, sort, userid);
        }
        #endregion

        #region SSD
        public IEnumerable<BrandVM> GetSSDBrandNamesAndNumbers(int userid = 0)
        {
            IEnumerable<BrandVM> brandVMs = _wonder.Brands.ToList().Join(GetAllSSD(userid),
                                       brand => brand.BrandId,
                                       ssd => ssd.SsdbrandId,
                                       (brand, ssd) => new BrandVM
                                       {
                                           BrandName = brand.BrandName,
                                           BrandNum = GetAllSSD(userid).Where(brandNum => brandNum.SsdbrandId == brand.BrandId).Count()
                                       }

               ).GroupBy(i => i.BrandName).Select(i => i.FirstOrDefault()).ToList();
            return brandVMs;
        }
        public IEnumerable<SsdVM> GetSSDProductsByPrice(IEnumerable<SsdVM> ssdVMs, int Id, int userid = 0)
        {
            IList<SsdVM> ssds = null;
            if (Id == 1)
            {
                ssds = ssdVMs.OrderByDescending(ssdvm => ssdvm.Ssdprice).ToList();
            }
            else if (Id == 2)
            {
                ssds = ssdVMs.OrderBy(ssdvm => ssdvm.Ssdprice).ToList();
            }
            else
            {
                ssds = ssdVMs.ToList();
            }
            return ssds;
        }
        public IEnumerable<SsdVM> GetSSDProductsByBrand(string[] BName, int id, int min=100, int max=50000, int userid = 0)
        {
            IEnumerable<SsdVM> Data = from ssd in GetAllSSD(userid)
                                      join brand in BName
                                      on ssd.BrandName.Trim() equals brand
                                      select ssd;
            if (min == 0 && max == 0)
            {

                return GetSSDProductsByPrice(Data, id, userid);
            }

            return GetSSDProductsByPrice(Data, id, userid).Where(ssd => ssd.Ssdprice >= min && ssd.Ssdprice <= max);
        }
        public IEnumerable<SsdVM> GetSSDDependentOnSort(int id, int userid = 0)
        {
            if (id == 0)
            {
                return GetAllSSD(userid).ToList();
            }
            return GetSSDProductsByPrice(GetAllSSD(userid), id, userid);
        }
        public IEnumerable<SsdVM> GetSSDPriceDependentOnBrand( int sort, int min=100, int max=50000, int userid = 0)
        {
            IEnumerable<SsdVM> ssds = null;
            if (min == 0 && max == 0)
            {

                ssds = GetSSDDependentOnSort(sort, userid).ToList();
            }
            else
            {

                ssds = GetAllSSD(userid).Where(ssd => ssd.Ssdprice >= min && ssd.Ssdprice <= max);
            }
            return GetSSDProductsByPrice(ssds, sort, userid);
        }

        #endregion

        #region Graphics Card

        public IEnumerable<BrandVM> GetCardBrandNamesAndNumbers(int userid = 0)
        {
            IEnumerable<BrandVM> brandVMs = _wonder.Brands.ToList().Join(GetAllCard(userid),
                                       brand => brand.BrandId,
                                      Card => Card.VgabrandId,
                                       (brand, ssd) => new BrandVM
                                       {
                                           BrandName = brand.BrandName,
                                           BrandNum = GetAllCard().Where(brandNum => brandNum.VgabrandId == brand.BrandId).Count()
                                       }

               ).GroupBy(i => i.BrandName).Select(i => i.FirstOrDefault()).ToList();
            return brandVMs;
        }

        public IEnumerable<GraphicsCardVM> GetCardProductsByPrice(IEnumerable<GraphicsCardVM> CardVMVMs, int Id, int userid = 0)
        {
            IList<GraphicsCardVM> cards = null;
            if (Id == 1)
            {
                cards = CardVMVMs.OrderByDescending(card => card.Vgaprice).ToList();
            }
            else if (Id == 2)
            {
                cards = CardVMVMs.OrderBy(card => card.Vgaprice).ToList();
            }
            else
            {
                cards = CardVMVMs.ToList();
            }
            return cards;

        }

        public IEnumerable<GraphicsCardVM> GetCardProductsByBrand(string[] BName,  int id, int min=100, int max=50000, int userid = 0)
        {
            IEnumerable<GraphicsCardVM> Data = from card in GetAllCard(userid)
                                               join brand in BName
                                               on card.BrandName.Trim() equals brand
                                               select card;
            if (min == 0 && max == 0)
            {

                return GetCardProductsByPrice(Data, id, userid);
            }

            return GetCardProductsByPrice(Data, id, userid).Where(card => card.Vgaprice >= min && card.Vgaprice <= max);
        }

        public IEnumerable<GraphicsCardVM> GetCardDependentOnSort(int id, int userid=0)
        {
            if (id == 0)
            {
                return GetAllCard(userid).ToList();
            }
            return GetCardProductsByPrice(GetAllCard(userid), id, userid);
        }
        public IEnumerable<GraphicsCardVM> GetCardPriceDependentOnBrand( int sort, int min=100, int max=50000,int userid=0)
        {
            IEnumerable<GraphicsCardVM> card = null;
            if (min == 0 && max == 0)
            {

                card = GetCardDependentOnSort(sort, userid).ToList();
            }
            else
            {

                card = GetAllCard().Where(card => card.Vgaprice >= min && card.Vgaprice <= max);
            }
            return GetCardProductsByPrice(card, sort, userid);
        }
        #endregion

        #region Case
        public IEnumerable<BrandVM> GetCaseBrandNamesAndNumbers(int userid = 0)
        {
            IEnumerable<BrandVM> brandVMs = _wonder.Brands.ToList().Join(GetAllCase(userid),
                                       brand => brand.BrandId,
                                       Case => Case.CaseBrandId,
                                       (brand, cAse) => new BrandVM
                                       {
                                           BrandName = brand.BrandName,
                                           BrandNum = GetAllCase(userid).Where(brandNum => brandNum.CaseBrandId == brand.BrandId).Count()
                                       }

               ).GroupBy(i => i.BrandName).Select(i => i.FirstOrDefault()).ToList();
            return brandVMs;
        }

        public IEnumerable<CaseVM> GetCaseProductsByPrice(IEnumerable<CaseVM> caseVMs, int Id, int userid = 0)
        {
            IList<CaseVM> cases = null;
            if (Id == 1)
            {
                cases = caseVMs.OrderByDescending(casevm => casevm.CasePrice).ToList();
            }
            else if (Id == 2)
            {
                cases = caseVMs.OrderBy(casevm => casevm.CasePrice).ToList();
            }
            else
            {
                cases = caseVMs.ToList();
            }
            return cases;
        }

        public IEnumerable<CaseVM> GetCaseProductsByBrand(string[] BName, int id, int min = 100, int max = 50000, int userid = 0)
        {
            IEnumerable<CaseVM> Data = from casevm in GetAllCase(userid)
                                       join brand in BName
                                       on casevm.BrandName.Trim() equals brand
                                       select casevm;
            if (min == 0 && max == 0)
            {

                return GetCaseProductsByPrice(Data, id, userid);
            }

            return GetCaseProductsByPrice(Data, id, userid).Where(casevm => casevm.CasePrice >= min && casevm.CasePrice <= max);
        }
        public IEnumerable<CaseVM> GetCaseDependentOnSort(int id, int userid = 0)
        {
            if (id == 0)
            {
                return GetAllCase(userid).ToList();
            }
            return GetCaseProductsByPrice(GetAllCase(userid), id, userid);
        }
        public IEnumerable<CaseVM> GetCasePriceDependentOnBrand( int sort,int min=100, int max=50000, int userid = 0)
        {
            IEnumerable<CaseVM> casevm = null;
            if (min == 0 && max == 0)
            {

                casevm = GetCaseDependentOnSort(sort, userid).ToList();
            }
            else
            {

                casevm = GetAllCase(userid).Where(casevm => casevm.CasePrice >= min && casevm.CasePrice <= max);
            }
            return GetCaseProductsByPrice(casevm, sort, userid);
        }
        #endregion

        #region PowerSuply
        public IEnumerable<BrandVM> GetPowerSuplyBrandNamesAndNumbers(int userid = 0)
        {
            IEnumerable<BrandVM> brandVMs = _wonder.Brands.ToList().Join(GetAllPowerSuply(userid),
                                       brand => brand.BrandId,
                                       PowerSupply => PowerSupply.PsubrandId,
                                       (brand, PowerSupply) => new BrandVM
                                       {
                                           BrandName = brand.BrandName,
                                           BrandNum = GetAllPowerSuply(userid).Where(brandNum => brandNum.PsubrandId == brand.BrandId).Count()
                                       }

               ).GroupBy(i => i.BrandName).Select(i => i.FirstOrDefault()).ToList();
            return brandVMs;
        }
        public IEnumerable<PowerSupplyVM> GetPowerSuplyProductsByPrice(IEnumerable<PowerSupplyVM> PowerSupplyVMs, int Id, int userid = 0)
        {
            IList<PowerSupplyVM> PSVM = null;
            if (Id == 1)
            {
                PSVM = PowerSupplyVMs.OrderByDescending(PSvm => PSvm.Psuprice).ToList();
            }
            else if (Id == 2)
            {
                PSVM = PowerSupplyVMs.OrderBy(PSvm => PSvm.Psuprice).ToList();
            }
            else
            {
                PSVM = PowerSupplyVMs.ToList();
            }
            return PSVM;
        }
        public IEnumerable<PowerSupplyVM> GetPowerSuplyProductsByBrand(string[] BName,  int id, int min=100, int max=50000, int userid = 0)
        {
            IEnumerable<PowerSupplyVM> Data = from PS in GetAllPowerSuply(userid)
                                              join brand in BName
                                              on PS.BrandName.Trim() equals brand
                                              select PS;
            if (min == 0 && max == 0)
            {

                return GetPowerSuplyProductsByPrice(Data, id, userid);
            }

            return GetPowerSuplyProductsByPrice(Data, id, userid).Where(PSVM => PSVM.Psuprice >= min && PSVM.Psuprice <= max);
        }
        public IEnumerable<PowerSupplyVM> GetPowerSuplyDependentOnSort(int id,int userid = 0)
        {
            if (id == 0)
            {
                return GetAllPowerSuply(userid).ToList();
            }
            return GetPowerSuplyProductsByPrice(GetAllPowerSuply(userid), id, userid);
        }
        public IEnumerable<PowerSupplyVM> GetPowerSuplyPriceDependentOnBrand( int sort,int min=100, int max=50000, int userid = 0)
        {
            IEnumerable<PowerSupplyVM> PSVMvm = null;
            if (min == 0 && max == 0)
            {

                PSVMvm = GetPowerSuplyDependentOnSort(sort, userid).ToList();
            }
            else
            {

                PSVMvm = GetAllPowerSuply(userid).Where(PSVM => PSVM.Psuprice >= min && PSVM.Psuprice <= max);
            }
            return GetPowerSuplyProductsByPrice(PSVMvm, sort, userid);
        }
        #endregion

        #endregion

        #region TopSelling

        #region Case
        public List<CaseVM> GetTopCases(int userid = 0)
        {
            List<CaseVM> topProducts = new();
            var codes = (from O in _wonder.Sales
                         where O.CaseCode != null
                         group O by O.CaseCode into grp
                         orderby grp.Count() descending
                         select grp.Key.ToString()).Take(4);
            foreach (var code in codes)
            {
                var item = _wonder.Cases.Where(x => x.CaseCode == code).Select(x => x).FirstOrDefault();
                CaseVM obj = (CaseVM)item;
                obj.WishList = _wonder.WishLists.Any(x => x.CaseCode == item.CaseCode && x.UserId == userid && x.IsAdded == true);
                obj.Image = _wonder.Images.Where(x => x.CaseCode == obj.CaseCode).Select(x => x.ProductImage).Take(1).ToList();
                obj.CaseRate = 0;
                //Total Rate from Reviews
                List<decimal> Rates = _wonder.Reviews.Where(x => x.MotherCode == item.CaseCode && x.IsAvailable == true && x.Rate != 0).Select(x => x.Rate).ToList();
                if (Rates.Count() != 0)
                {
                    obj.CaseRate = Rates.Sum() / Rates.Count();
                }
                topProducts.Add(obj);
            }
            return topProducts;
        }
        #endregion

        #region GraphicsCard
        public List<GraphicsCardVM> GetTopVgas(int userid = 0)
        {
            List<GraphicsCardVM> topProducts = new();
            var codes = (from O in _wonder.Sales
                         where O.Vgacode != null
                         group O by O.Vgacode into grp
                         orderby grp.Count() descending
                         select grp.Key.ToString()).Take(4);
            foreach (var code in codes)
            {
                var item = _wonder.GraphicsCards.Where(x => x.Vgacode == code).Select(x => x).FirstOrDefault();
                GraphicsCardVM obj = (GraphicsCardVM)item;
                obj.WishList = _wonder.WishLists.Any(x => x.Vgacode == item.Vgacode && x.UserId == userid && x.IsAdded == true);
                obj.Image = _wonder.Images.Where(x => x.Vgacode == obj.Vgacode).Select(x => x.ProductImage).Take(1).ToList();
                obj.Vgarate = 0;
                //Total Rate from Reviews
                List<decimal> Rates = _wonder.Reviews.Where(x => x.Vgacode == item.Vgacode && x.IsAvailable == true && x.Rate != 0).Select(x => x.Rate).ToList();
                if (Rates.Count() != 0)
                {
                    obj.Vgarate = Rates.Sum() / Rates.Count();
                }
                topProducts.Add(obj);
            }
            return topProducts;
        }
        #endregion

        #region HDD
        public List<HddVM> GetTopHdds(int userid = 0)
        {
            List<HddVM> topProducts = new();
            var codes = (from O in _wonder.Sales
                         where O.Hddcode != null
                         group O by O.Hddcode into grp
                         orderby grp.Count() descending
                         select grp.Key.ToString()).Take(4);
            foreach (var code in codes)
            {
                var item = _wonder.Hdds.Where(x => x.Hddcode == code).Select(x => x).FirstOrDefault();
                HddVM obj = (HddVM)item;
                obj.WishList = _wonder.WishLists.Any(x => x.Hddcode == item.Hddcode && x.UserId == userid && x.IsAdded == true);
                obj.Image = _wonder.Images.Where(x => x.Hddcode == obj.Hddcode).Select(x => x.ProductImage).Take(1).ToList();
                obj.Hddrate = 0;
                //Total Rate from Reviews
                List<decimal> Rates = _wonder.Reviews.Where(x => x.Hddcode == item.Hddcode && x.IsAvailable == true && x.Rate != 0).Select(x => x.Rate).ToList();
                if (Rates.Count() != 0)
                {
                    obj.Hddrate = Rates.Sum() / Rates.Count();
                }
                topProducts.Add(obj);
            }
            return topProducts;
        }
        #endregion

        #region MotherBoard
        public List<MotherboardVM> GetTopMotherboards(int userid = 0)
        {
            List<MotherboardVM> topProducts = new List<MotherboardVM>();
            var codes = (from O in _wonder.Sales
                         where O.MotherCode != null
                         group O by O.MotherCode into grp
                         orderby grp.Count() descending
                         select grp.Key.ToString()).Take(4);
            foreach (var code in codes)
            {
                var item = _wonder.Motherboards.Where(x => x.MotherCode == code).Select(x => x).FirstOrDefault();
                MotherboardVM obj = (MotherboardVM)item;
                obj.WishList = _wonder.WishLists.Any(x => x.MotherCode == item.MotherCode && x.UserId == userid && x.IsAdded == true);
                obj.Image = _wonder.Images.Where(x => x.MotherCode == obj.MotherCode).Select(x => x.ProductImage).Take(1).ToList();
                obj.MotherRate = 0;
                //Total Rate from Reviews
                List<decimal> Rates = _wonder.Reviews.Where(x => x.MotherCode == item.MotherCode && x.IsAvailable == true && x.Rate != 0).Select(x => x.Rate).ToList();
                if (Rates.Count() != 0)
                {
                    obj.MotherRate = Rates.Sum() / Rates.Count();
                }
                topProducts.Add(obj);
            }
            return topProducts;
        }
        #endregion

        #region PowerSupply
        public List<PowerSupplyVM> GetTopPsus(int userid = 0)
        {
            List<PowerSupplyVM> topProducts = new();
            var codes = (from O in _wonder.Sales
                         where O.Psucode != null
                         group O by O.Psucode into grp
                         orderby grp.Count() descending
                         select grp.Key.ToString()).Take(4);
            foreach (var code in codes)
            {
                var item = _wonder.PowerSupplies.Where(x => x.Psucode == code).Select(x => x).FirstOrDefault();
                PowerSupplyVM obj = (PowerSupplyVM)item;
                obj.WishList = _wonder.WishLists.Any(x => x.Psucode == item.Psucode && x.UserId == userid && x.IsAdded == true);
                obj.Image = _wonder.Images.Where(x => x.Psucode == obj.Psucode).Select(x => x.ProductImage).Take(1).ToList();
                obj.Psurate = 0;
                //Total Rate from Reviews
                List<decimal> Rates = _wonder.Reviews.Where(x => x.Psucode == item.Psucode && x.IsAvailable == true && x.Rate != 0).Select(x => x.Rate).ToList();
                if (Rates.Count() != 0)
                {
                    obj.Psurate = Rates.Sum() / Rates.Count();
                }
                topProducts.Add(obj);
            }
            return topProducts;
        }

        #endregion

        #region Processor
        public List<ProcessorVM> GetTopProcessors(int userid = 0)
        {
            List<ProcessorVM> topProducts = new();
            var codes = (from O in _wonder.Sales
                         where O.ProCode != null
                         group O by O.ProCode into grp
                         orderby grp.Count() descending
                         select grp.Key.ToString()).Take(4);
            foreach (var code in codes)
            {
                var item = _wonder.Processors.Where(x => x.ProCode == code).Select(x => x).FirstOrDefault();
                ProcessorVM obj = (ProcessorVM)item;
                obj.WishList = _wonder.WishLists.Any(x => x.ProCode == item.ProCode && x.UserId == userid && x.IsAdded == true);
                obj.Image = _wonder.Images.Where(x => x.ProCode == obj.ProCode).Select(x => x.ProductImage).Take(1).ToList();
                obj.ProRate = 0;
                //Total Rate from Reviews
                List<decimal> Rates = _wonder.Reviews.Where(x => x.ProCode == item.ProCode && x.IsAvailable == true && x.Rate != 0).Select(x => x.Rate).ToList();
                if (Rates.Count() != 0)
                {
                    obj.ProRate = Rates.Sum() / Rates.Count();
                }
                topProducts.Add(obj);
            }
            return topProducts;
        }
        #endregion

        #region Ram
        public List<RamVM> GetTopRams(int userid = 0)
        {
            List<RamVM> topProducts = new();
            var codes = (from O in _wonder.Sales
                         where O.RamCode != null
                         group O by O.RamCode into grp
                         orderby grp.Count() descending
                         select grp.Key.ToString()).Take(4);
            foreach (var code in codes)
            {
                var item = _wonder.Rams.Where(x => x.RamCode == code).Select(x => x).FirstOrDefault();
                RamVM obj = (RamVM)item;
                obj.WishList = _wonder.WishLists.Any(x => x.RamCode == item.RamCode && x.UserId == userid && x.IsAdded == true);
                obj.Image = _wonder.Images.Where(x => x.RamCode == obj.RamCode).Select(x => x.ProductImage).Take(1).ToList();
                obj.RamRate = 0;
                //Total Rate from Reviews
                List<decimal> Rates = _wonder.Reviews.Where(x => x.RamCode == item.RamCode && x.IsAvailable == true && x.Rate != 0).Select(x => x.Rate).ToList();
                if (Rates.Count() != 0)
                {
                    obj.RamRate = Rates.Sum() / Rates.Count();
                }
                topProducts.Add(obj);
            }
            return topProducts;
        }
        #endregion

        #region SSD
        public List<SsdVM> GetTopSsds(int userid = 0)
        {
            List<SsdVM> topProducts = new();
            var codes = (from O in _wonder.Sales
                         where O.Ssdcode != null
                         group O by O.Ssdcode into grp
                         orderby grp.Count() descending
                         select grp.Key.ToString()).Take(4);
            foreach (var code in codes)
            {
                var item = _wonder.Ssds.Where(x => x.Ssdcode == code).Select(x => x).FirstOrDefault();
                SsdVM obj = (SsdVM)item;
                obj.WishList = _wonder.WishLists.Any(x => x.Ssdcode == item.Ssdcode && x.UserId == userid && x.IsAdded == true);
                obj.Image = _wonder.Images.Where(x => x.Ssdcode == obj.Ssdcode).Select(x => x.ProductImage).Take(1).ToList();
                obj.Ssdrate = 0;
                //Total Rate from Reviews
                List<decimal> Rates = _wonder.Reviews.Where(x => x.Ssdcode == item.Ssdcode && x.IsAvailable == true && x.Rate != 0).Select(x => x.Rate).ToList();
                if (Rates.Count() != 0)
                {
                    obj.Ssdrate = Rates.Sum() / Rates.Count();
                }
                topProducts.Add(obj);
            }
            return topProducts;
        }
        #endregion
        #endregion

        #region ProductDetails

        public CaseVM CaseDetails(string code, int currentPageIndex = 0)
        {
            var product = _wonder.Cases.Where(x => x.CaseCode == code && x.IsAvailable == true).FirstOrDefault();
            CaseVM obj = (CaseVM)product;
            obj.CaseRate = 0;
            obj.Image = _wonder.Images.Where(x => x.CaseCode == obj.CaseCode).Select(x => x.ProductImage).ToList();
            int maxRows = 3;
            List<Review> Data = new();
            if (currentPageIndex == 0)
            {
                Data = _wonder.Reviews.Select(X => X).Where(x => x.CaseCode == code && x.IsAvailable == true).ToList();
            }
            else
            {
                Data = _wonder.Reviews.Select(X => X).Where(x => x.CaseCode == code && x.IsAvailable == true).Skip((currentPageIndex - 1) * maxRows).Take(maxRows).ToList();
            }
            List<ReviewVM> reviews = new List<ReviewVM>();
            if (currentPageIndex == 0)
            {
                double pageCount = (double)(Data.Count() / Convert.ToDecimal(maxRows));
                obj.PageCount = (int)Math.Ceiling(pageCount);
                obj.CurrentPageIndex = 1;
            }
            foreach (var item in Data)
            {
                ReviewVM R = (ReviewVM)item;
                R.ProductCode = item.CaseCode;
                reviews.Add(R);
                obj.CaseRate += item.Rate;
            }
            obj.Reviews = reviews;
            if (Data.Where(x => x.Rate != 0).Count() != 0)
            {
                obj.CaseRate = obj.CaseRate / Data.Where(x => x.Rate != 0).Count();
            }
            if (currentPageIndex == 0)
            {
                var ratecount = from O in Data
                                group O by O.Rate into grp
                                select new { Rate = grp.Key, Count = grp.Count() };
                List<RateVM> ratecount2 = new List<RateVM>();
                foreach (var item in ratecount)
                {
                    RateVM RC = new RateVM();
                    RC.Rate = item.Rate;
                    RC.Count = item.Count;
                    ratecount2.Add(RC);
                }
                obj.RateCount = ratecount2;
            }
            return obj;
        }

        public GraphicsCardVM GraphicsCardDetails(string code, int currentPageIndex = 0)
        {
            var product = _wonder.GraphicsCards.Where(x => x.Vgacode == code && x.IsAvailable == true).FirstOrDefault();
            GraphicsCardVM obj = (GraphicsCardVM)product;
            obj.Vgarate = 0;
            obj.Image = _wonder.Images.Where(x => x.Vgacode == obj.Vgacode).Select(x => x.ProductImage).ToList();
            int maxRows = 3;
            List<Review> Data = new();
            if (currentPageIndex == 0)
            {
                Data = _wonder.Reviews.Select(X => X).Where(x => x.Vgacode == code && x.IsAvailable == true).ToList();
            }
            else
            {
                Data = _wonder.Reviews.Select(X => X).Where(x => x.Vgacode == code && x.IsAvailable == true).Skip((currentPageIndex - 1) * maxRows).Take(maxRows).ToList();
            }
            List<ReviewVM> reviews = new List<ReviewVM>();
            if (currentPageIndex == 0)
            {
                double pageCount = (double)(Data.Count() / Convert.ToDecimal(maxRows));
                obj.PageCount = (int)Math.Ceiling(pageCount);
                obj.CurrentPageIndex = 1;
            }
            foreach (var item in Data)
            {
                ReviewVM R = (ReviewVM)item;
                R.ProductCode = item.Vgacode;
                reviews.Add(R);
                obj.Vgarate += item.Rate;
            }
            obj.Reviews = reviews;
            if (Data.Where(x => x.Rate != 0).Count() != 0)
            {
                obj.Vgarate = obj.Vgarate / Data.Where(x => x.Rate != 0).Count();
            }
            if (currentPageIndex == 0)
            {
                var ratecount = from O in Data
                                group O by O.Rate into grp
                                select new { Rate = grp.Key, Count = grp.Count() };
                List<RateVM> ratecount2 = new List<RateVM>();
                foreach (var item in ratecount)
                {
                    RateVM RC = new RateVM();
                    RC.Rate = item.Rate;
                    RC.Count = item.Count;
                    ratecount2.Add(RC);
                }
                obj.RateCount = ratecount2;
            }
            return obj;
        }

        public HddVM HddDetails(string code, int currentPageIndex = 0)
        {
            var product = _wonder.Hdds.Where(x => x.Hddcode == code && x.IsAvailable == true).FirstOrDefault();
            HddVM obj = (HddVM)product;
            obj.Hddrate = 0;
            obj.Image = _wonder.Images.Where(x => x.Hddcode == obj.Hddcode).Select(x => x.ProductImage).ToList();
            int maxRows = 3;
            List<Review> Data = new();
            if (currentPageIndex == 0)
            {
                Data = _wonder.Reviews.Select(X => X).Where(x => x.Hddcode == code && x.IsAvailable == true).ToList();
            }
            else
            {
                Data = _wonder.Reviews.Select(X => X).Where(x => x.Hddcode == code && x.IsAvailable == true).Skip((currentPageIndex - 1) * maxRows).Take(maxRows).ToList();
            }
            List<ReviewVM> reviews = new List<ReviewVM>();
            if (currentPageIndex == 0)
            {
                double pageCount = (double)(Data.Count() / Convert.ToDecimal(maxRows));
                obj.PageCount = (int)Math.Ceiling(pageCount);
                obj.CurrentPageIndex = 1;
            }
            foreach (var item in Data)
            {
                ReviewVM R = (ReviewVM)item;
                R.ProductCode = item.Hddcode;
                reviews.Add(R);
                obj.Hddrate += item.Rate;
            }
            obj.Reviews = reviews;
            if (Data.Where(x => x.Rate != 0).Count() != 0)
            {
                obj.Hddrate = obj.Hddrate / Data.Where(x => x.Rate != 0).Count();
            }
            if (currentPageIndex == 0)
            {
                var ratecount = from O in Data
                                group O by O.Rate into grp
                                select new { Rate = grp.Key, Count = grp.Count() };
                List<RateVM> ratecount2 = new List<RateVM>();
                foreach (var item in ratecount)
                {
                    RateVM RC = new RateVM();
                    RC.Rate = item.Rate;
                    RC.Count = item.Count;
                    ratecount2.Add(RC);
                }
                obj.RateCount = ratecount2;
            }
            return obj;
        }

        public MotherboardVM MotherboardDetails(string code, int currentPageIndex = 0)
        {
            var product = _wonder.Motherboards.Where(x => x.MotherCode == code && x.IsAvailable == true).FirstOrDefault();
            MotherboardVM obj = (MotherboardVM)product;
            obj.MotherRate = 0;
            obj.Image = _wonder.Images.Where(x => x.MotherCode == obj.MotherCode).Select(x => x.ProductImage).ToList();
            int maxRows = 3;
            List<Review> Data = new();
            if (currentPageIndex == 0)
            {
                Data = _wonder.Reviews.Select(X => X).Where(x => x.MotherCode == code && x.IsAvailable == true).ToList();
            }
            else
            {
                Data = _wonder.Reviews.Select(X => X).Where(x => x.MotherCode == code && x.IsAvailable == true).Skip((currentPageIndex - 1) * maxRows).Take(maxRows).ToList();
            }
            List<ReviewVM> reviews = new List<ReviewVM>();
            if (currentPageIndex == 0)
            {
                double pageCount = (double)(Data.Count() / Convert.ToDecimal(maxRows));
                obj.PageCount = (int)Math.Ceiling(pageCount);
                obj.CurrentPageIndex = 1;
            }
            foreach (var item in Data)
            {
                ReviewVM R = (ReviewVM)item;
                R.ProductCode = item.MotherCode;
                reviews.Add(R);
                obj.MotherRate += item.Rate;
            }
            obj.Reviews = reviews;
            if (Data.Where(x => x.Rate != 0).Count() != 0)
            {
                obj.MotherRate = obj.MotherRate / Data.Where(x => x.Rate != 0).Count();
            }
            if (currentPageIndex == 0)
            {
                var ratecount = from O in Data
                                group O by O.Rate into grp
                                select new { Rate = grp.Key, Count = grp.Count() };
                List<RateVM> ratecount2 = new List<RateVM>();
                foreach (var item in ratecount)
                {
                    RateVM RC = new RateVM();
                    RC.Rate = item.Rate;
                    RC.Count = item.Count;
                    ratecount2.Add(RC);
                }
                obj.RateCount = ratecount2;
            }
            return obj;
        }

        public PowerSupplyVM PowerSupplyDetails(string code, int currentPageIndex = 0)
        {
            var product = _wonder.PowerSupplies.Where(x => x.Psucode == code && x.IsAvailable == true).FirstOrDefault();
            PowerSupplyVM obj = (PowerSupplyVM)product;
            obj.Psurate = 0;
            obj.Image = _wonder.Images.Where(x => x.Psucode == obj.Psucode).Select(x => x.ProductImage).ToList();
            int maxRows = 3;
            List<Review> Data = new();
            if (currentPageIndex == 0)
            {
                Data = _wonder.Reviews.Select(X => X).Where(x => x.Psucode == code && x.IsAvailable == true).ToList();
            }
            else
            {
                Data = _wonder.Reviews.Select(X => X).Where(x => x.Psucode == code && x.IsAvailable == true).Skip((currentPageIndex - 1) * maxRows).Take(maxRows).ToList();
            }
            List<ReviewVM> reviews = new List<ReviewVM>();
            if (currentPageIndex == 0)
            {
                double pageCount = (double)(Data.Count() / Convert.ToDecimal(maxRows));
                obj.PageCount = (int)Math.Ceiling(pageCount);
                obj.CurrentPageIndex = 1;
            }
            foreach (var item in Data)
            {
                ReviewVM R = (ReviewVM)item;
                R.ProductCode = item.Psucode;
                reviews.Add(R);
                obj.Psurate += item.Rate;
            }
            obj.Reviews = reviews;
            if (Data.Where(x => x.Rate != 0).Count() != 0)
            {
                obj.Psurate = obj.Psurate / Data.Where(x => x.Rate != 0).Count();
            }
            if (currentPageIndex == 0)
            {
                var ratecount = from O in Data
                                group O by O.Rate into grp
                                select new { Rate = grp.Key, Count = grp.Count() };
                List<RateVM> ratecount2 = new List<RateVM>();
                foreach (var item in ratecount)
                {
                    RateVM RC = new RateVM();
                    RC.Rate = item.Rate;
                    RC.Count = item.Count;
                    ratecount2.Add(RC);
                }
                obj.RateCount = ratecount2;
            }
            return obj;
        }

        public ProcessorVM ProcessorDetails(string code, int currentPageIndex = 0)
        {
            var product = _wonder.Processors.Where(x => x.ProCode == code && x.IsAvailable == true).FirstOrDefault();
            ProcessorVM obj = (ProcessorVM)product;
            obj.ProRate = 0;
            obj.Image = _wonder.Images.Where(x => x.ProCode == obj.ProCode).Select(x => x.ProductImage).ToList();
            int maxRows = 3;
            List<Review> Data = new();
            if (currentPageIndex == 0)
            {
                Data = _wonder.Reviews.Select(X => X).Where(x => x.ProCode == code && x.IsAvailable == true).ToList();
            }
            else
            {
                Data = _wonder.Reviews.Select(X => X).Where(x => x.ProCode == code && x.IsAvailable == true).Skip((currentPageIndex - 1) * maxRows).Take(maxRows).ToList();
            }
            List<ReviewVM> reviews = new List<ReviewVM>();
            if (currentPageIndex == 0)
            {
                double pageCount = (double)(Data.Count() / Convert.ToDecimal(maxRows));
                obj.PageCount = (int)Math.Ceiling(pageCount);
                obj.CurrentPageIndex = 1;
            }
            foreach (var item in Data)
            {
                ReviewVM R = (ReviewVM)item;
                R.ProductCode = item.ProCode;
                reviews.Add(R);
                obj.ProRate += item.Rate;
            }
            obj.Reviews = reviews;
            if (Data.Where(x => x.Rate != 0).Count() != 0)
            {
                obj.ProRate = obj.ProRate / Data.Where(x => x.Rate != 0).Count();
            }
            if (currentPageIndex == 0)
            {
                var ratecount = from O in Data
                                group O by O.Rate into grp
                                select new { Rate = grp.Key, Count = grp.Count() };
                List<RateVM> ratecount2 = new List<RateVM>();
                foreach (var item in ratecount)
                {
                    RateVM RC = new RateVM();
                    RC.Rate = item.Rate;
                    RC.Count = item.Count;
                    ratecount2.Add(RC);
                }
                obj.RateCount = ratecount2;
            }
            return obj;
        }

        public RamVM RamDetails(string code, int currentPageIndex = 0)
        {
            var product = _wonder.Rams.Where(x => x.RamCode == code && x.IsAvailable == true).FirstOrDefault();
            RamVM obj = (RamVM)product;
            obj.RamRate = 0;
            obj.Image = _wonder.Images.Where(x => x.RamCode == obj.RamCode).Select(x => x.ProductImage).ToList();
            int maxRows = 3;
            List<Review> Data = new();
            if (currentPageIndex == 0)
            {
                Data = _wonder.Reviews.Select(X => X).Where(x => x.RamCode == code && x.IsAvailable == true).ToList();
            }
            else
            {
                Data = _wonder.Reviews.Select(X => X).Where(x => x.RamCode == code && x.IsAvailable == true).Skip((currentPageIndex - 1) * maxRows).Take(maxRows).ToList();
            }
            List<ReviewVM> reviews = new List<ReviewVM>();
            if (currentPageIndex == 0)
            {
                double pageCount = (double)(Data.Count() / Convert.ToDecimal(maxRows));
                obj.PageCount = (int)Math.Ceiling(pageCount);
                obj.CurrentPageIndex = 1;
            }
            foreach (var item in Data)
            {
                ReviewVM R = (ReviewVM)item;
                R.ProductCode = item.RamCode;
                reviews.Add(R);
                obj.RamRate += item.Rate;
            }
            obj.Reviews = reviews;
            if (Data.Where(x => x.Rate != 0).Count() != 0)
            {
                obj.RamRate = obj.RamRate / Data.Where(x => x.Rate != 0).Count();
            }
            if (currentPageIndex == 0)
            {
                var ratecount = from O in Data
                                group O by O.Rate into grp
                                select new { Rate = grp.Key, Count = grp.Count() };
                List<RateVM> ratecount2 = new List<RateVM>();
                foreach (var item in ratecount)
                {
                    RateVM RC = new RateVM();
                    RC.Rate = item.Rate;
                    RC.Count = item.Count;
                    ratecount2.Add(RC);
                }
                obj.RateCount = ratecount2;
            }
            return obj;
        }

        public SsdVM SsdDetails(string code, int currentPageIndex = 0)
        {
            var product = _wonder.Ssds.Where(x => x.Ssdcode == code && x.IsAvailable == true).FirstOrDefault();
            SsdVM obj = (SsdVM)product;
            obj.Ssdrate = 0;
            obj.Image = _wonder.Images.Where(x => x.Ssdcode == obj.Ssdcode).Select(x => x.ProductImage).ToList();
            int maxRows = 3;
            List<Review> Data = new();
            if (currentPageIndex == 0)
            {
                Data = _wonder.Reviews.Select(X => X).Where(x => x.Ssdcode == code && x.IsAvailable == true).ToList();
            }
            else
            {
                Data = _wonder.Reviews.Select(X => X).Where(x => x.Ssdcode == code && x.IsAvailable == true).Skip((currentPageIndex - 1) * maxRows).Take(maxRows).ToList();
            }
            List<ReviewVM> reviews = new List<ReviewVM>();
            if (currentPageIndex == 0)
            {
                double pageCount = (double)(Data.Count() / Convert.ToDecimal(maxRows));
                obj.PageCount = (int)Math.Ceiling(pageCount);
                obj.CurrentPageIndex = 1;
            }
            foreach (var item in Data)
            {
                ReviewVM R = (ReviewVM)item;
                R.ProductCode = item.Ssdcode;
                reviews.Add(R);
                obj.Ssdrate += item.Rate;
            }
            obj.Reviews = reviews;
            if (Data.Where(x => x.Rate != 0).Count() != 0)
            {
                obj.Ssdrate = obj.Ssdrate / Data.Where(x => x.Rate != 0).Count();
            }
            if (currentPageIndex == 0)
            {
                var ratecount = from O in Data
                                group O by O.Rate into grp
                                select new { Rate = grp.Key, Count = grp.Count() };
                List<RateVM> ratecount2 = new List<RateVM>();
                foreach (var item in ratecount)
                {
                    RateVM RC = new RateVM();
                    RC.Rate = item.Rate;
                    RC.Count = item.Count;
                    ratecount2.Add(RC);
                }
                obj.RateCount = ratecount2;
            }
            return obj;
        }

        #endregion

        #region CheckOut

        public string CheckOrderCreateAcc(UserVM UserData, SalesVM[] OrderData)
        {
            User Uobj = new User();
            if (_wonder.Users.Where(x => x.Phone == UserData.Telephone && x.IsAdmin == false).FirstOrDefault() == null)
            {
                Uobj.FirstName = UserData.FName;
                Uobj.LastName = UserData.LName;
                Uobj.Password = UserData.Password;
                Uobj.Phone = UserData.Telephone;
                _wonder.Users.Add(Uobj);
                _wonder.SaveChanges();
                var userid = _wonder.Users.Where(x => x.Phone == UserData.Telephone).Select(x => x.UserId).FirstOrDefault();

                foreach (var item in OrderData)
                {
                    Sale Sobj = new Sale();

                    Sobj.UserId = userid;
                    Sobj.Address = item.City + " , " + item.Address;
                    if (item.ProductCode.StartsWith("SSD"))
                    {
                        Sobj.Ssdcode = item.ProductCode;
                        var obj = _wonder.Ssds.Where(x => x.Ssdcode == item.ProductCode).FirstOrDefault();
                        obj.Ssdquantity = (short)(obj.Ssdquantity - item.ProductQuantity);
                        _wonder.Ssds.Update(obj);
                    }
                    else if (item.ProductCode.StartsWith("R"))
                    {
                        Sobj.RamCode = item.ProductCode;
                        var obj = _wonder.Rams.Where(x => x.RamCode == item.ProductCode).FirstOrDefault();
                        obj.RamQuantity = (short)(obj.RamQuantity - item.ProductQuantity);
                        _wonder.Rams.Update(obj);
                    }
                    else if (item.ProductCode.StartsWith("CAS"))
                    {
                        Sobj.CaseCode = item.ProductCode;
                        var obj = _wonder.Cases.Where(x => x.CaseCode == item.ProductCode).FirstOrDefault();
                        obj.CaseQuantity = (short)(obj.CaseQuantity - item.ProductQuantity);
                        _wonder.Cases.Update(obj);
                    }
                    else if (item.ProductCode.StartsWith("V"))
                    {
                        Sobj.Vgacode = item.ProductCode;
                        var obj = _wonder.GraphicsCards.Where(x => x.Vgacode == item.ProductCode).FirstOrDefault();
                        obj.Vgaquantity = (short)(obj.Vgaquantity - item.ProductQuantity);
                        _wonder.GraphicsCards.Update(obj);
                    }
                    else if (item.ProductCode.StartsWith("PS"))
                    {
                        Sobj.Psucode = item.ProductCode;
                        var obj = _wonder.PowerSupplies.Where(x => x.Psucode == item.ProductCode).FirstOrDefault();
                        obj.Psuquantity = (short)(obj.Psuquantity - item.ProductQuantity);
                        _wonder.PowerSupplies.Update(obj);
                    }
                    else if (item.ProductCode.StartsWith("PR"))
                    {
                        Sobj.ProCode = item.ProductCode;
                        var obj = _wonder.Processors.Where(x => x.ProCode == item.ProductCode).FirstOrDefault();
                        obj.ProQuantity = (short)(obj.ProQuantity - item.ProductQuantity);
                        _wonder.Processors.Update(obj);
                    }
                    else if (item.ProductCode.StartsWith("MO"))
                    {
                        Sobj.MotherCode = item.ProductCode;
                        var obj = _wonder.Motherboards.Where(x => x.MotherCode == item.ProductCode).FirstOrDefault();
                        obj.MotherQuantity = (short)(obj.MotherQuantity - item.ProductQuantity);
                        _wonder.Motherboards.Update(obj);
                    }
                    else if (item.ProductCode.StartsWith("HD"))
                    {
                        Sobj.Hddcode = item.ProductCode;
                        var obj = _wonder.Hdds.Where(x => x.Hddcode == item.ProductCode).FirstOrDefault();
                        obj.Hddquantity = (short)(obj.Hddquantity - item.ProductQuantity);
                        _wonder.Hdds.Update(obj);
                    }

                    Sobj.ProductQuantity = item.ProductQuantity;
                    Sobj.TotalPrice = item.TotalPrice;
                    Sobj.DateAndTime = DateTime.Now;
                    _wonder.Sales.Add(Sobj);
                    _wonder.SaveChanges();
                }
                //Account has been created &Order checked successfully.
                return "success";
            }
            else
            {
                return "This phone is already exist";
            }
        }

        //public String CheckOrderSignIn(UserVM UserData, SalesVM[] OrderData)
        //{
        //    //xxxxxxxx
        //    User Uobj = new User();

        //    var resultMsg = "";

        //    var userid = _wonder.Users.Where(x => x.Phone == UserData.Telephone && x.IsAdmin == false).Select(x => x.UserId).FirstOrDefault();
        //    var X = _wonder.Users.Where(x => x.Password == UserData.Password && x.Phone == UserData.Telephone && x.IsAdmin == false).Select(x => x.UserId).FirstOrDefault();
        //    if (X != 0)
        //    {
        //        foreach (var item in OrderData)
        //        {
        //            Sale Sobj = new Sale();

        //            Sobj.UserId = userid;

        //            if (item.ProductCode.StartsWith("SSD"))
        //            {
        //                Sobj.Ssdcode = item.ProductCode;
        //                var obj = _wonder.Ssds.Where(x => x.Ssdcode == item.ProductCode).FirstOrDefault();
        //                obj.Ssdquantity = (short)(obj.Ssdquantity - item.ProductQuantity);
        //                _wonder.Ssds.Update(obj);
        //            }
        //            else if (item.ProductCode.StartsWith("R"))
        //            {
        //                Sobj.RamCode = item.ProductCode;
        //                var obj = _wonder.Rams.Where(x => x.RamCode == item.ProductCode).FirstOrDefault();
        //                obj.RamQuantity = (short)(obj.RamQuantity - item.ProductQuantity);
        //                _wonder.Rams.Update(obj);
        //            }
        //            else if (item.ProductCode.StartsWith("CAS"))
        //            {
        //                Sobj.CaseCode = item.ProductCode;
        //                var obj = _wonder.Cases.Where(x => x.CaseCode == item.ProductCode).FirstOrDefault();
        //                obj.CaseQuantity = (short)(obj.CaseQuantity - item.ProductQuantity);
        //                _wonder.Cases.Update(obj);
        //            }
        //            else if (item.ProductCode.StartsWith("V"))
        //            {
        //                Sobj.Vgacode = item.ProductCode;
        //                var obj = _wonder.GraphicsCards.Where(x => x.Vgacode == item.ProductCode).FirstOrDefault();
        //                obj.Vgaquantity = (short)(obj.Vgaquantity - item.ProductQuantity);
        //                _wonder.GraphicsCards.Update(obj);
        //            }
        //            else if (item.ProductCode.StartsWith("PS"))
        //            {
        //                Sobj.Psucode = item.ProductCode;
        //                var obj = _wonder.PowerSupplies.Where(x => x.Psucode == item.ProductCode).FirstOrDefault();
        //                obj.Psuquantity = (short)(obj.Psuquantity - item.ProductQuantity);
        //                _wonder.PowerSupplies.Update(obj);
        //            }
        //            else if (item.ProductCode.StartsWith("PR"))
        //            {
        //                Sobj.ProCode = item.ProductCode;
        //                var obj = _wonder.Processors.Where(x => x.ProCode == item.ProductCode).FirstOrDefault();
        //                obj.ProQuantity = (short)(obj.ProQuantity - item.ProductQuantity);
        //                _wonder.Processors.Update(obj);
        //            }
        //            else if (item.ProductCode.StartsWith("MO"))
        //            {
        //                Sobj.MotherCode = item.ProductCode;
        //                var obj = _wonder.Motherboards.Where(x => x.MotherCode == item.ProductCode).FirstOrDefault();
        //                obj.MotherQuantity = (short)(obj.MotherQuantity - item.ProductQuantity);
        //                _wonder.Motherboards.Update(obj);
        //            }
        //            else if (item.ProductCode.StartsWith("HD"))
        //            {
        //                Sobj.Hddcode = item.ProductCode;
        //                var obj = _wonder.Hdds.Where(x => x.Hddcode == item.ProductCode).FirstOrDefault();
        //                obj.Hddquantity = (short)(obj.Hddquantity - item.ProductQuantity);
        //                _wonder.Hdds.Update(obj);
        //            }
        //            Sobj.ProductQuantity = item.ProductQuantity;
        //            Sobj.TotalPrice = item.TotalPrice;
        //            if (item.City != null && item.Address != null)
        //            {
        //                Sobj.Address = item.City + " , " + item.Address;
        //                Sobj.DateAndTime = DateTime.Now;
        //                _wonder.Sales.Add(Sobj);
        //                _wonder.SaveChanges();
        //                //Order checked successfully at the address that user entered.
        //                resultMsg = "success checked new address";
        //            }
        //            else
        //            {
        //                Sobj.Address = _wonder.Sales.Where(x => x.UserId == userid).OrderByDescending(x => x.DateAndTime).Take(1).Select(x => x.Address).FirstOrDefault();
        //                Sobj.DateAndTime = DateTime.Now;
        //                _wonder.Sales.Add(Sobj);
        //                _wonder.SaveChanges();
        //                //Order checked successfully at the same address checked before.
        //                resultMsg = "success checked old address";
        //            }
        //        }

        //        return resultMsg;
        //    }
        //    else if (userid == 0)
        //    {
        //        return "failed phone";
        //    }
        //    else if (X == 0)
        //    {
        //        return "failed password";
        //    }
        //    else
        //    {
        //        //Phone or password isn't correct or both !!
        //        return "failed phone&pass";
        //    }
        //}

        public String CheckOrder(SalesVM[] OrderData, int userid = 0)
        {
            //already signed in
            var resultMsg = "";

            foreach (var item in OrderData)
            {
                Sale Sobj = new Sale();
                if (item.UserID == 0 || item.UserID == null)
                {
                    Sobj.UserId = userid;
                }
                else
                {
                    Sobj.UserId = item.UserID;
                }
                if (item.ProductCode.StartsWith("SSD"))
                {
                    Sobj.Ssdcode = item.ProductCode;
                    var obj = _wonder.Ssds.Where(x => x.Ssdcode == item.ProductCode).FirstOrDefault();
                    obj.Ssdquantity = (short)(obj.Ssdquantity - item.ProductQuantity);
                    _wonder.Ssds.Update(obj);
                }
                else if (item.ProductCode.StartsWith("R"))
                {
                    Sobj.RamCode = item.ProductCode;
                    var obj = _wonder.Rams.Where(x => x.RamCode == item.ProductCode).FirstOrDefault();
                    obj.RamQuantity = (short)(obj.RamQuantity - item.ProductQuantity);
                    _wonder.Rams.Update(obj);
                }
                else if (item.ProductCode.StartsWith("CAS"))
                {
                    Sobj.CaseCode = item.ProductCode;
                    var obj = _wonder.Cases.Where(x => x.CaseCode == item.ProductCode).FirstOrDefault();
                    obj.CaseQuantity = (short)(obj.CaseQuantity - item.ProductQuantity);
                    _wonder.Cases.Update(obj);
                }
                else if (item.ProductCode.StartsWith("V"))
                {
                    Sobj.Vgacode = item.ProductCode;
                    var obj = _wonder.GraphicsCards.Where(x => x.Vgacode == item.ProductCode).FirstOrDefault();
                    obj.Vgaquantity = (short)(obj.Vgaquantity - item.ProductQuantity);
                    _wonder.GraphicsCards.Update(obj);
                }
                else if (item.ProductCode.StartsWith("PS"))
                {
                    Sobj.Psucode = item.ProductCode;
                    var obj = _wonder.PowerSupplies.Where(x => x.Psucode == item.ProductCode).FirstOrDefault();
                    obj.Psuquantity = (short)(obj.Psuquantity - item.ProductQuantity);
                    _wonder.PowerSupplies.Update(obj);
                }
                else if (item.ProductCode.StartsWith("PR"))
                {
                    Sobj.ProCode = item.ProductCode;
                    var obj = _wonder.Processors.Where(x => x.ProCode == item.ProductCode).FirstOrDefault();
                    obj.ProQuantity = (short)(obj.ProQuantity - item.ProductQuantity);
                    _wonder.Processors.Update(obj);
                }
                else if (item.ProductCode.StartsWith("MO"))
                {
                    Sobj.MotherCode = item.ProductCode;
                    var obj = _wonder.Motherboards.Where(x => x.MotherCode == item.ProductCode).FirstOrDefault();
                    obj.MotherQuantity = (short)(obj.MotherQuantity - item.ProductQuantity);
                    _wonder.Motherboards.Update(obj);
                }
                else if (item.ProductCode.StartsWith("HD"))
                {
                    Sobj.Hddcode = item.ProductCode;
                    var obj = _wonder.Hdds.Where(x => x.Hddcode == item.ProductCode).FirstOrDefault();
                    obj.Hddquantity = (short)(obj.Hddquantity - item.ProductQuantity);
                    _wonder.Hdds.Update(obj);
                }
                Sobj.ProductQuantity = item.ProductQuantity;
                Sobj.TotalPrice = item.TotalPrice;
                if (item.City != null && item.Address != null)
                {
                    //add address for the first time Or add another address
                    Sobj.Address = item.City + " , " + item.Address;
                    Sobj.DateAndTime = DateTime.Now;
                    _wonder.Sales.Add(Sobj);
                    _wonder.SaveChanges();
                    //Order checked successfully at the address that user entered.
                    resultMsg = "success checked new address";
                }
                else
                {
                    //get the oldest address from sales table
                    Sobj.Address = _wonder.Sales.Where(x => x.UserId == Sobj.UserId).OrderByDescending(x => x.SalesId).Select(x => x.Address).FirstOrDefault();
                    Sobj.DateAndTime = DateTime.Now;
                    _wonder.Sales.Add(Sobj);
                    _wonder.SaveChanges();
                    //Order checked successfully at the same address checked before.
                    resultMsg = "success checked old address";
                }
            }
            return resultMsg;
        }

        #endregion

        #region WishList
        public List<WishListVM> GetWishList(int userid)
        {
            List<WishListVM> WishList = new List<WishListVM>();
            var MothersCodes = _wonder.WishLists.Where(x => (x.UserId == userid) && (x.IsAdded == true) && (x.MotherCode != null)).Select(x => x).ToList();
            foreach (var item in MothersCodes)
            {
                WishListVM obj = new WishListVM();
                obj.ProductName = item.MotherCodeNavigation.MotherName;
                obj.ProductPrice = item.MotherCodeNavigation.MotherPrice;
                obj.Quantity = item.MotherCodeNavigation.MotherQuantity;
                obj.ProductCode = item.MotherCode;
                obj.Image = _wonder.Images.Where(x => x.MotherCode == obj.ProductCode).Select(x => x.ProductImage).FirstOrDefault();
                if (_wonder.Motherboards.Where(x => x.MotherCode == obj.ProductCode).Select(x => x.IsAvailable).FirstOrDefault() == false)
                {
                    obj.IsAvailable = "Not Available";
                }
                else if (_wonder.Motherboards.Where(x => x.MotherCode == obj.ProductCode).Select(x => x.MotherQuantity).FirstOrDefault() == 0)
                {
                    obj.IsAvailable = "Out Of Stock";
                }
                obj.UserID = item.UserId;
                WishList.Add(obj);
            }

            var CasesCodes = _wonder.WishLists.Where(x => (x.UserId == userid) && (x.IsAdded == true) && (x.CaseCode != null)).Select(x => x).ToList();
            foreach (var item in CasesCodes)
            {
                WishListVM obj = new WishListVM();
                obj.ProductName = item.CaseCodeNavigation.CaseName;
                obj.ProductPrice = item.CaseCodeNavigation.CasePrice;
                obj.Quantity = item.CaseCodeNavigation.CaseQuantity;
                obj.ProductCode = item.CaseCode;
                obj.Image = _wonder.Images.Where(x => x.CaseCode == obj.ProductCode).Select(x => x.ProductImage).FirstOrDefault();
                if (_wonder.Cases.Where(x => x.CaseCode == obj.ProductCode).Select(x => x.IsAvailable).FirstOrDefault() == false)
                {
                    obj.IsAvailable = "Not Available";
                }
                else if (_wonder.Cases.Where(x => x.CaseCode == obj.ProductCode).Select(x => x.CaseQuantity).FirstOrDefault() == 0)
                {
                    obj.IsAvailable = "Out Of Stock";
                }
                obj.UserID = item.UserId;
                WishList.Add(obj);
            }
            var VGAsCodes = _wonder.WishLists.Where(x => (x.UserId == userid) && (x.IsAdded == true) && (x.Vgacode != null)).Select(x => x).ToList();
            foreach (var item in VGAsCodes)
            {
                WishListVM obj = new WishListVM();
                obj.ProductName = item.VgacodeNavigation.Vganame;
                obj.ProductPrice = item.VgacodeNavigation.Vgaprice;
                obj.Quantity = item.VgacodeNavigation.Vgaquantity;
                obj.ProductCode = item.Vgacode;
                obj.Image = _wonder.Images.Where(x => x.Vgacode == obj.ProductCode).Select(x => x.ProductImage).FirstOrDefault();
                if (_wonder.GraphicsCards.Where(x => x.Vgacode == obj.ProductCode).Select(x => x.IsAvailable).FirstOrDefault() == false)
                {
                    obj.IsAvailable = "Not Available";
                }
                else if (_wonder.GraphicsCards.Where(x => x.Vgacode == obj.ProductCode).Select(x => x.Vgaquantity).FirstOrDefault() == 0)
                {
                    obj.IsAvailable = "Out Of Stock";
                }
                obj.UserID = item.UserId;
                WishList.Add(obj);
            }

            var HDDsCodes = _wonder.WishLists.Where(x => (x.UserId == userid) && (x.IsAdded == true) && (x.Hddcode != null)).Select(x => x).ToList();
            foreach (var item in HDDsCodes)
            {
                WishListVM obj = new WishListVM();
                obj.ProductName = item.HddcodeNavigation.Hddname;
                obj.ProductPrice = item.HddcodeNavigation.Hddprice;
                obj.Quantity = item.HddcodeNavigation.Hddprice;
                obj.ProductCode = item.Hddcode;
                obj.Image = _wonder.Images.Where(x => x.Hddcode == obj.ProductCode).Select(x => x.ProductImage).FirstOrDefault();
                if (_wonder.Hdds.Where(x => x.Hddcode == obj.ProductCode).Select(x => x.IsAvailable).FirstOrDefault() == false)
                {
                    obj.IsAvailable = "Not Available";
                }
                else if (_wonder.Hdds.Where(x => x.Hddcode == obj.ProductCode).Select(x => x.Hddquantity).FirstOrDefault() == 0)
                {
                    obj.IsAvailable = "Out Of Stock";
                }
                obj.UserID = item.UserId;
                WishList.Add(obj);
            }

            var PSUsCodes = _wonder.WishLists.Where(x => (x.UserId == userid) && (x.IsAdded == true) && (x.Psucode != null)).Select(x => x).ToList();
            foreach (var item in PSUsCodes)
            {
                WishListVM obj = new WishListVM();
                obj.ProductName = item.PsucodeNavigation.Psuname;
                obj.ProductPrice = item.PsucodeNavigation.Psuprice;
                obj.Quantity = item.PsucodeNavigation.Psuquantity;
                obj.ProductCode = item.Psucode;
                obj.Image = _wonder.Images.Where(x => x.Psucode == obj.ProductCode).Select(x => x.ProductImage).FirstOrDefault();
                if (_wonder.PowerSupplies.Where(x => x.Psucode == obj.ProductCode).Select(x => x.IsAvailable).FirstOrDefault() == false)
                {
                    obj.IsAvailable = "Not Available";
                }
                else if (_wonder.PowerSupplies.Where(x => x.Psucode == obj.ProductCode).Select(x => x.Psuquantity).FirstOrDefault() == 0)
                {
                    obj.IsAvailable = "Out Of Stock";
                }
                obj.UserID = item.UserId;
                WishList.Add(obj);
            }

            var ProsCodes = _wonder.WishLists.Where(x => (x.UserId == userid) && (x.IsAdded == true) && (x.ProCode != null)).Select(x => x).ToList();
            foreach (var item in ProsCodes)
            {
                WishListVM obj = new WishListVM();
                obj.ProductName = item.ProCodeNavigation.ProName;
                obj.ProductPrice = item.ProCodeNavigation.ProPrice;
                obj.Quantity = item.ProCodeNavigation.ProQuantity;
                obj.ProductCode = item.ProCode;
                obj.Image = _wonder.Images.Where(x => x.ProCode == obj.ProductCode).Select(x => x.ProductImage).FirstOrDefault();
                if (_wonder.Processors.Where(x => x.ProCode == obj.ProductCode).Select(x => x.IsAvailable).FirstOrDefault() == false)
                {
                    obj.IsAvailable = "Not Available";
                }
                else if (_wonder.Processors.Where(x => x.ProCode == obj.ProductCode).Select(x => x.ProQuantity).FirstOrDefault() == 0)
                {
                    obj.IsAvailable = "Out Of Stock";
                }
                obj.UserID = item.UserId;
                WishList.Add(obj);
            }

            var RAMsCodes = _wonder.WishLists.Where(x => (x.UserId == userid) && (x.IsAdded == true) && (x.RamCode != null)).Select(x => x).ToList();
            foreach (var item in RAMsCodes)
            {
                WishListVM obj = new WishListVM();
                obj.ProductName = item.RamCodeNavigation.RamName;
                obj.ProductPrice = item.RamCodeNavigation.RamPrice;
                obj.Quantity = item.RamCodeNavigation.RamQuantity;
                obj.ProductCode = item.RamCode;
                obj.Image = _wonder.Images.Where(x => x.RamCode == obj.ProductCode).Select(x => x.ProductImage).FirstOrDefault();
                if (_wonder.Rams.Where(x => x.RamCode == obj.ProductCode).Select(x => x.IsAvailable).FirstOrDefault() == false)
                {
                    obj.IsAvailable = "Not Available";
                }
                else if (_wonder.Rams.Where(x => x.RamCode == obj.ProductCode).Select(x => x.RamQuantity).FirstOrDefault() == 0)
                {
                    obj.IsAvailable = "Out Of Stock";
                }
                obj.UserID = item.UserId;
                WishList.Add(obj);
            }

            var SSDsCodes = _wonder.WishLists.Where(x => (x.UserId == userid) && (x.IsAdded == true) && (x.Ssdcode != null)).Select(x => x).ToList();
            foreach (var item in SSDsCodes)
            {
                WishListVM obj = new WishListVM();
                obj.ProductName = item.SsdcodeNavigation.Ssdname;
                obj.ProductPrice = item.SsdcodeNavigation.Ssdprice;
                obj.Quantity = item.SsdcodeNavigation.Ssdquantity;
                obj.ProductCode = item.Ssdcode;
                obj.Image = _wonder.Images.Where(x => x.Ssdcode == obj.ProductCode).Select(x => x.ProductImage).FirstOrDefault();
                if (_wonder.Ssds.Where(x => x.Ssdcode == obj.ProductCode).Select(x => x.IsAvailable).FirstOrDefault() == false)
                {
                    obj.IsAvailable = "Not Available";
                }
                else if (_wonder.Ssds.Where(x => x.Ssdcode == obj.ProductCode).Select(x => x.Ssdquantity).FirstOrDefault() == 0)
                {
                    obj.IsAvailable = "Out Of Stock";
                }
                obj.UserID = item.UserId;
                WishList.Add(obj);
            }
            return WishList;
        }
        public string DeletefromWL(string ProductCode, int userid)
        {
            WishList productRow = _wonder.WishLists.Where(x => x.UserId == userid && (x.Ssdcode == ProductCode || x.RamCode == ProductCode || x.CaseCode == ProductCode || x.Vgacode == ProductCode || x.Psucode == ProductCode || x.ProCode == ProductCode || x.MotherCode == ProductCode || x.Hddcode == ProductCode) && x.IsAdded == true).FirstOrDefault();

            if ((ProductCode != null) && productRow != null)
            {
                productRow.IsAdded = false;
                _wonder.SaveChanges();
                return "Deleted Done";
            }
            else
            {
                return "Error";
            }
        }
        public string CheckfromWL(string ProductCode, int userid)
        {
            WishList item = _wonder.WishLists.Where(x => x.UserId == userid && (x.Ssdcode == ProductCode || x.RamCode == ProductCode || x.CaseCode == ProductCode || x.Vgacode == ProductCode || x.Psucode == ProductCode || x.ProCode == ProductCode || x.MotherCode == ProductCode || x.Hddcode == ProductCode)).FirstOrDefault();
            if ((ProductCode != null) && (item != null))
            {
                if (item.IsAdded == true)
                {
                    return "this product is choosed";
                }
                else
                {
                    return "product not choosed";
                }
            }
            else
            {
                return "product isn't exist";
            }


        }
        public string AddToWL(string ProductCode, int userid)
        {
            WishList productRow = _wonder.WishLists.Where(x => x.UserId == userid && (x.Ssdcode == ProductCode || x.RamCode == ProductCode || x.CaseCode == ProductCode || x.Vgacode == ProductCode || x.Psucode == ProductCode || x.ProCode == ProductCode || x.MotherCode == ProductCode || x.Hddcode == ProductCode) && x.IsAdded == false).FirstOrDefault();

            //هاجي هنا لو القلب مش ملون 
            if ((ProductCode != null) && (_wonder.WishLists.Where(x => x.UserId == userid && (x.Ssdcode == ProductCode || x.RamCode == ProductCode || x.CaseCode == ProductCode || x.Vgacode == ProductCode || x.Psucode == ProductCode || x.ProCode == ProductCode || x.MotherCode == ProductCode || x.Hddcode == ProductCode)).FirstOrDefault() == null))
            {
                //في حالة ان البرودكت مش موجود ف الداتابيز خالص 
                WishList Row = new WishList();
                if (ProductCode.StartsWith("SSD"))
                {
                    Row.Ssdcode = ProductCode;
                }
                else if (ProductCode.StartsWith("R"))
                {
                    Row.RamCode = ProductCode;
                }
                else if (ProductCode.StartsWith("CAS"))
                {
                    Row.CaseCode = ProductCode;
                }
                else if (ProductCode.StartsWith("V"))
                {
                    Row.Vgacode = ProductCode;
                }
                else if (ProductCode.StartsWith("PS"))
                {
                    Row.Psucode = ProductCode;
                }
                else if (ProductCode.StartsWith("PR"))
                {
                    Row.ProCode = ProductCode;
                }
                else if (ProductCode.StartsWith("MO"))
                {
                    Row.MotherCode = ProductCode;
                }
                else if (ProductCode.StartsWith("HD"))
                {
                    Row.Hddcode = ProductCode;
                }
                Row.UserId = userid;
                Row.IsAdded = true;
                _wonder.WishLists.Add(Row);
                if (_wonder.SaveChanges() != 0)
                {
                    return "Saved Done";
                }
                else
                {
                    return "Error";
                }
            }
            else if ((ProductCode != null) && productRow != null)
            {
                //في حالة ان البرودكت موجود ف الداتابيز بس الأديد ب فولس 

                productRow.IsAdded = true;
                _wonder.SaveChanges();
                return "Saved Done";
            }
            return "something wrong";

        }
        #endregion

        #region Search

        public List<Search> SearchProduct(string src)
        {
            List<Search> obj = new List<Search>();
            var MotherBoard = _wonder.Motherboards.Select(i => new { ProductCode = i.MotherCode, ProductName = i.MotherName }).Where(x => x.ProductName.Contains(src)).ToList();
            foreach (var item in MotherBoard)
            {
                Search s = new Search();
                s.ProductName = item.ProductName;
                s.ProductCode = item.ProductCode;
                s.Image = _wonder.Images.Where(x => x.MotherCode == s.ProductCode).Select(x => x.ProductImage).FirstOrDefault();
                obj.Add(s);
            }
            var Processor = _wonder.Processors.Select(i => new { ProductCode = i.ProCode, ProductName = i.ProName }).Where(x => x.ProductName.Contains(src)).ToList();
            foreach (var item in Processor)
            {
                Search s = new Search();
                s.ProductName = item.ProductName;
                s.ProductCode = item.ProductCode;
                s.Image = _wonder.Images.Where(x => x.ProCode == s.ProductCode).Select(x => x.ProductImage).FirstOrDefault();
                obj.Add(s);
            }
            var HDD = _wonder.Hdds.Select(i => new { ProductCode = i.Hddcode, ProductName = i.Hddname }).Where(x => x.ProductName.Contains(src)).ToList();
            foreach (var item in HDD)
            {
                Search s = new Search();
                s.ProductName = item.ProductName;
                s.ProductCode = item.ProductCode;
                s.Image = _wonder.Images.Where(x => x.Hddcode == s.ProductCode).Select(x => x.ProductImage).FirstOrDefault();
                obj.Add(s);
            }
            var Ram = _wonder.Rams.Select(i => new { ProductCode = i.RamCode, ProductName = i.RamName }).Where(x => x.ProductName.Contains(src)).ToList();
            foreach (var item in Ram)
            {
                Search s = new Search();
                s.ProductName = item.ProductName;
                s.ProductCode = item.ProductCode;
                s.Image = _wonder.Images.Where(x => x.RamCode == s.ProductCode).Select(x => x.ProductImage).FirstOrDefault();
                obj.Add(s);
            }
            var VGA = _wonder.GraphicsCards.Select(i => new { ProductCode = i.Vgacode, ProductName = i.Vganame }).Where(x => x.ProductName.Contains(src)).ToList();
            foreach (var item in VGA)
            {
                Search s = new Search();
                s.ProductName = item.ProductName;
                s.ProductCode = item.ProductCode;
                s.Image = _wonder.Images.Where(x => x.Vgacode == s.ProductCode).Select(x => x.ProductImage).FirstOrDefault();
                obj.Add(s);
            }
            var SSD = _wonder.Ssds.Select(i => new { ProductCode = i.Ssdcode, ProductName = i.Ssdname }).Where(x => x.ProductName.Contains(src)).ToList();
            foreach (var item in SSD)
            {
                Search s = new Search();
                s.ProductName = item.ProductName;
                s.ProductCode = item.ProductCode;
                s.Image = _wonder.Images.Where(x => x.Ssdcode == s.ProductCode).Select(x => x.ProductImage).FirstOrDefault();
                obj.Add(s);
            }
            var PSU = _wonder.PowerSupplies.Select(i => new { ProductCode = i.Psucode, ProductName = i.Psuname }).Where(x => x.ProductName.Contains(src)).ToList();
            foreach (var item in PSU)
            {
                Search s = new Search();
                s.ProductName = item.ProductName;
                s.ProductCode = item.ProductCode;
                s.Image = _wonder.Images.Where(x => x.Psucode == s.ProductCode).Select(x => x.ProductImage).FirstOrDefault();
                obj.Add(s);
            }
            var Case = _wonder.Cases.Select(i => new { ProductCode = i.CaseCode, ProductName = i.CaseName }).Where(x => x.ProductName.Contains(src)).ToList();
            foreach (var item in Case)
            {
                Search s = new Search();
                s.ProductName = item.ProductName;
                s.ProductCode = item.ProductCode;
                s.Image = _wonder.Images.Where(x => x.CaseCode == s.ProductCode).Select(x => x.ProductImage).FirstOrDefault();
                obj.Add(s);
            }
            return obj;
        }

        public List<Search> SearchMotherBoard(string src)
        {
            List<Search> obj = new List<Search>();
            var MotherBoard = _wonder.Motherboards.Select(i => new { ProductCode = i.MotherCode, ProductName = i.MotherName }).Where(x => x.ProductName.Contains(src)).ToList();
            foreach (var item in MotherBoard)
            {
                Search s = new Search();
                s.ProductName = item.ProductName;
                s.ProductCode = item.ProductCode;
                s.Image = _wonder.Images.Where(x => x.MotherCode == s.ProductCode).Select(x => x.ProductImage).FirstOrDefault();
                obj.Add(s);
            }
            return obj;
        }

        public List<Search> SearchProcessor(string src)
        {
            List<Search> obj = new List<Search>();
            var Processor = _wonder.Processors.Select(i => new { ProductCode = i.ProCode, ProductName = i.ProName }).Where(x => x.ProductName.Contains(src)).ToList();
            foreach (var item in Processor)
            {
                Search s = new Search();
                s.ProductName = item.ProductName;
                s.ProductCode = item.ProductCode;
                s.Image = _wonder.Images.Where(x => x.ProCode == s.ProductCode).Select(x => x.ProductImage).FirstOrDefault();
                obj.Add(s);
            }
            return obj;
        }

        public List<Search> SearchRam(string src)
        {
            List<Search> obj = new List<Search>();
            var Ram = _wonder.Rams.Select(i => new { ProductCode = i.RamCode, ProductName = i.RamName }).Where(x => x.ProductName.Contains(src)).ToList();
            foreach (var item in Ram)
            {
                Search s = new Search();
                s.ProductName = item.ProductName;
                s.ProductCode = item.ProductCode;
                s.Image = _wonder.Images.Where(x => x.RamCode == s.ProductCode).Select(x => x.ProductImage).FirstOrDefault();
                obj.Add(s);
            }
            return obj;
        }

        public List<Search> SearchSSD(string src)
        {
            List<Search> obj = new List<Search>();
            var SSD = _wonder.Ssds.Select(i => new { ProductCode = i.Ssdcode, ProductName = i.Ssdname }).Where(x => x.ProductName.Contains(src)).ToList();
            foreach (var item in SSD)
            {
                Search s = new Search();
                s.ProductName = item.ProductName;
                s.ProductCode = item.ProductCode;
                s.Image = _wonder.Images.Where(x => x.Ssdcode == s.ProductCode).Select(x => x.ProductImage).FirstOrDefault();
                obj.Add(s);
            }
            return obj;
        }

        public List<Search> SearchHDD(string src)
        {
            List<Search> obj = new List<Search>();
            var HDD = _wonder.Hdds.Select(i => new { ProductCode = i.Hddcode, ProductName = i.Hddname }).Where(x => x.ProductName.Contains(src)).ToList();
            foreach (var item in HDD)
            {
                Search s = new Search();
                s.ProductName = item.ProductName;
                s.ProductCode = item.ProductCode;
                s.Image = _wonder.Images.Where(x => x.Hddcode == s.ProductCode).Select(x => x.ProductImage).FirstOrDefault();
                obj.Add(s);
            }
            return obj;
        }

        public List<Search> SearchCase(string src)
        {
            List<Search> obj = new List<Search>();
            var Case = _wonder.Cases.Select(i => new { ProductCode = i.CaseCode, ProductName = i.CaseName }).Where(x => x.ProductName.Contains(src)).ToList();
            foreach (var item in Case)
            {
                Search s = new Search();
                s.ProductName = item.ProductName;
                s.ProductCode = item.ProductCode;
                s.Image = _wonder.Images.Where(x => x.CaseCode == s.ProductCode).Select(x => x.ProductImage).FirstOrDefault();
                obj.Add(s);
            }
            return obj;
        }

        public List<Search> SearchPowerSupply(string src)
        {
            List<Search> obj = new List<Search>();
            var PSU = _wonder.PowerSupplies.Select(i => new { ProductCode = i.Psucode, ProductName = i.Psuname }).Where(x => x.ProductName.Contains(src)).ToList();
            foreach (var item in PSU)
            {
                Search s = new Search();
                s.ProductName = item.ProductName;
                s.ProductCode = item.ProductCode;
                s.Image = _wonder.Images.Where(x => x.Psucode == s.ProductCode).Select(x => x.ProductImage).FirstOrDefault();
                obj.Add(s);
            }
            return obj;
        }

        public List<Search> SearchVGA(string src)
        {
            List<Search> obj = new List<Search>();
            var VGA = _wonder.GraphicsCards.Select(i => new { ProductCode = i.Vgacode, ProductName = i.Vganame }).Where(x => x.ProductName.Contains(src)).ToList();
            foreach (var item in VGA)
            {
                Search s = new Search();
                s.ProductName = item.ProductName;
                s.ProductCode = item.ProductCode;
                s.Image = _wonder.Images.Where(x => x.Vgacode == s.ProductCode).Select(x => x.ProductImage).FirstOrDefault();
                obj.Add(s);
            }
            return obj;
        }
        public List<Search> SearchFunction(string src, int num)
        {
            List<Search> x = new List<Search>();
            if (num == 0)
            {
                x = SearchProduct(src);
            }
            else if (num == 1)
            {
                x = SearchMotherBoard(src);
            }
            else if (num == 2)
            {
                x = SearchProcessor(src);
            }
            else if (num == 3)
            {
                x = SearchRam(src);
            }
            else if (num == 4)
            {
                x = SearchSSD(src);
            }
            else if (num == 5)
            {
                x = SearchHDD(src);
            }
            else if (num == 6)
            {
                x = SearchCase(src);
            }
            else if (num == 7)
            {
                x = SearchPowerSupply(src);
            }
            else if (num == 8)
            {
                x = SearchVGA(src);
            }
            return x;
        }

        #endregion

        #region Review

        public ReviewVM AddReview(ReviewVM review)
        {
            Review obj = new Review();
            if (review.ProductCode.StartsWith("SSD"))
            {
                obj.Ssdcode = review.ProductCode;
            }
            else if (review.ProductCode.StartsWith("R"))
            {
                obj.RamCode = review.ProductCode;
            }
            else if (review.ProductCode.StartsWith("CAS"))
            {
                obj.CaseCode = review.ProductCode;
            }
            else if (review.ProductCode.StartsWith("V"))
            {
                obj.Vgacode = review.ProductCode;
            }
            else if (review.ProductCode.StartsWith("PS"))
            {
                obj.Psucode = review.ProductCode;
            }
            else if (review.ProductCode.StartsWith("PR"))
            {
                obj.ProCode = review.ProductCode;
            }
            else if (review.ProductCode.StartsWith("MO"))
            {
                obj.MotherCode = review.ProductCode;
            }
            else if (review.ProductCode.StartsWith("HD"))
            {
                obj.Hddcode = review.ProductCode;
            }
            if (review.Comment == null)
            {
                obj.Comment = "";
                review.Comment = "";
            }
            else
            {
                obj.Comment = review.Comment;
            }
            obj.CustomerName = review.CustomerName;
            obj.Rate = review.Rate;
            review.DateAndTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd'T'HH:mm:ss", CultureInfo.InvariantCulture));
            obj.DateAndTime = DateTime.Now;
            obj.IsAvailable = true;
            _wonder.Reviews.Add(obj);
            _wonder.SaveChanges();

            return review;
        }

        public List<ReviewVM> Reviews(string code)
        {
            List<Review> Data = new List<Review>();
            List<ReviewVM> reviews = new List<ReviewVM>();
            if (code.StartsWith("SSD"))
            {
                Data = _wonder.Reviews.Select(X => X).Where(x => x.Ssdcode == code).ToList();
            }
            else if (code.StartsWith("R"))
            {
                Data = _wonder.Reviews.Select(X => X).Where(x => x.RamCode == code).ToList();
            }
            else if (code.StartsWith("CAS"))
            {
                Data = _wonder.Reviews.Select(X => X).Where(x => x.CaseCode == code).ToList();
            }
            else if (code.StartsWith("V"))
            {
                Data = _wonder.Reviews.Select(X => X).Where(x => x.Vgacode == code).ToList();
            }
            else if (code.StartsWith("PS"))
            {
                Data = _wonder.Reviews.Select(X => X).Where(x => x.Psucode == code).ToList();
            }
            else if (code.StartsWith("PR"))
            {
                Data = _wonder.Reviews.Select(X => X).Where(x => x.ProCode == code).ToList();
            }
            else if (code.StartsWith("MO"))
            {
                Data = _wonder.Reviews.Select(X => X).Where(x => x.MotherCode == code).ToList();
            }
            else if (code.StartsWith("HD"))
            {
                Data = _wonder.Reviews.Select(X => X).Where(x => x.Hddcode == code).ToList();
            }
            foreach (var item in Data)
            {
                ReviewVM R = (ReviewVM)item;
                R.ProductCode = code;
                reviews.Add(R);
            }
            return reviews;
        }

        #endregion

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////

        #region Getting Brands

        public List<Brand> GetProductBrand()
        {
            var brand = _wonder.Brands.ToList();
            return brand;
        }
        #endregion

        #region Admin Project

        #region Users

        public List<UserVM> GetUsersData()
        {
            List<UserVM> obj = new List<UserVM>();
            var data = _wonder.Users.Where(x => x.IsAdmin == false).Select(x => new { x.UserId, x.FirstName, x.LastName, x.Phone, x.Password }).ToList();
            foreach (var item in data)
            {
                UserVM O = new UserVM();
                O.ID = item.UserId;
                O.Name = item.FirstName + " " + item.LastName;
                O.Telephone = item.Phone;
                O.Password = item.Password;
                O.LatestBuyTime = _wonder.Sales.Where(x => x.UserId == item.UserId).OrderByDescending(x => x.DateAndTime).Take(1).Select(x => x.DateAndTime).FirstOrDefault();
                O.NumberOfTimes = _wonder.Sales.Where(x => x.UserId == item.UserId).GroupBy(x => x.UserId).Select(g => g.Count()).FirstOrDefault();
                obj.Add(O);
            }
            return obj;
        }
        #endregion

        #region Admins

        public List<UserVM> GetAdmins()
        {
            List<UserVM> data = new List<UserVM>();
            var obj = _wonder.Users.Where(x => x.IsAdmin == true).Select(x => new { FName = x.FirstName, LName = x.LastName, Telephone = x.Phone }).ToList();
            foreach (var item in obj)
            {
                UserVM O = new UserVM();
                O.Name = item.FName + " " + item.LName;
                O.Telephone = item.Telephone;
                data.Add(O);
            }
            return data;
        }

        #endregion

        #region Sales
        public List<SalesVM> GetProcessor()
        {
            List<SalesVM> sales = new List<SalesVM>();
            var processor = _wonder.Sales.Where(pro => pro.ProCode != null);
            foreach (var item in processor)
            {
                SalesVM obj = (SalesVM)item;
                obj.ProductCode = item.ProCodeNavigation.ProName;
                sales.Add(obj);
            }
            return sales;
        }
        public List<SalesVM> GetMotherboard()
        {
            List<SalesVM> sales = new List<SalesVM>();
            var Mother = _wonder.Sales.Where(moth => moth.MotherCode != null);
            foreach (var item in Mother)
            {
                SalesVM obj = (SalesVM)item;
                obj.ProductCode = item.MotherCodeNavigation.MotherName;
                sales.Add(obj);
            }
            return sales;
        }
        public List<SalesVM> GetSDD()
        {
            List<SalesVM> sales = new List<SalesVM>();
            var ssd = _wonder.Sales.Where(ssd => ssd.Ssdcode != null);
            foreach (var item in ssd)
            {
                SalesVM obj = (SalesVM)item;
                obj.ProductCode = item.SsdcodeNavigation.Ssdname;
                sales.Add(obj);
            }
            return sales;
        }
        public List<SalesVM> GetHDD()
        {
            List<SalesVM> sales = new List<SalesVM>();
            var ssd = _wonder.Sales.Where(hdd => hdd.Hddcode != null);
            foreach (var item in ssd)
            {
                SalesVM obj = (SalesVM)item;
                obj.ProductCode = item.HddcodeNavigation.Hddname;
                sales.Add(obj);
            }
            return sales;
        }
        public List<SalesVM> GetCases()
        {
            List<SalesVM> sales = new List<SalesVM>();
            var Case = _wonder.Sales.Where(cas => cas.CaseCode != null);
            foreach (var item in Case)
            {
                SalesVM obj = (SalesVM)item;
                obj.ProductCode = item.CaseCodeNavigation.CaseName;
                sales.Add(obj);
            }
            return sales;
        }
        public List<SalesVM> GetPowerSupplies()
        {
            List<SalesVM> sales = new List<SalesVM>();
            var PowerSuply = _wonder.Sales.Where(suply => suply.Psucode != null);
            foreach (var item in PowerSuply)
            {
                SalesVM obj = (SalesVM)item;
                obj.ProductCode = item.PsucodeNavigation.Psuname;
                sales.Add(obj);
            }
            return sales;
        }
        public List<SalesVM> GetGraphicsCard()
        {
            List<SalesVM> sales = new List<SalesVM>();
            var GraphicCard = _wonder.Sales.Where(GC => GC.Vgacode != null);
            foreach (var item in GraphicCard)
            {
                SalesVM obj = (SalesVM)item;
                obj.ProductCode = item.VgacodeNavigation.Vganame;
                sales.Add(obj);
            }
            return sales;
        }
        public List<SalesVM> GetRam()
        {
            List<SalesVM> sales = new List<SalesVM>();
            var ram = _wonder.Sales.Where(r => r.RamCode != null);
            foreach (var item in ram)
            {
                SalesVM obj = (SalesVM)item;
                obj.ProductCode = item.RamCodeNavigation.RamName;
                sales.Add(obj);
            }
            return sales;
        }

        #endregion

        #endregion

        #region Chat
        public List<ChatVM> GetAllMessages(int userid)
        {
            var messages = _wonder.Messages.Where(x => x.UserId == userid).ToList();
            List<ChatVM> messagesList = new List<ChatVM>();
            foreach (var item in messages)
            {
                ChatVM obj = new ChatVM();
                obj.UserId = item.UserId;
                obj.UserName = item.User.FirstName + " " + item.User.LastName;
                obj.AdminId = item.AdminId;
                if (item.AdminId == null)
                {
                    obj.AdminName = "";
                }
                else
                {
                    obj.AdminName = item.Admin.FirstName + " " + item.Admin.LastName;
                }
                obj.MessageText = item.MessageText;
                obj.Time = item.Time.ToShortTimeString();
                obj.Date = item.Time.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture); //نقلب التاريخ من اليمين للشمال 
                obj.AdminOrNot = item.AdminOrNot;
                messagesList.Add(obj);
            }
            return messagesList;
        }

        #endregion
    }
}