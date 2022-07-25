using DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DataModel.Models;
using BLL.ViewModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using cloudscribe.Web.Pagination;
using cloudscribe.Pagination.Models;
using Microsoft.AspNetCore.Http;
using UI.Helper;

namespace UI.Controllers {
    public class HomeController : Controller {
        readonly IWonder _iwonder;

        public HomeController(IWonder iwonder, WonderHardwareContext wonder)
        {
            _iwonder = iwonder;
            _wonder = wonder;
        }
        readonly WonderHardwareContext _wonder;


        //private readonly ISession session;
        //public HomeController(IHttpContextAccessor httpContextAccessor)
        //{
        //    this.session = httpContextAccessor.HttpContext.Session;
        //}

        public IActionResult Index()
        {
            int Uid = HttpContext.Session.GetInt32("UserID").GetValueOrDefault();
            #region NewProducts
            ViewBag.NewMotherBoards = _iwonder.GetAllMotherboard(userid: Uid).OrderByDescending(p => p.MotherCode).Take(4);
            ViewBag.NewProcessors = _iwonder.GetAllProcessors(userid: Uid).OrderByDescending(p => p.ProCode).Take(4);
            ViewBag.NewRam = _iwonder.GetAllRAM(userid: Uid).OrderByDescending(p => p.RamCode).Take(4);
            ViewBag.NewVGA = _iwonder.GetAllCard(userid: Uid).OrderByDescending(p => p.Vgacode).Take(4);
            ViewBag.NewHDD = _iwonder.GetAllHDD(userid: Uid).OrderByDescending(p => p.Hddcode).Take(4);
            ViewBag.NewSSD = _iwonder.GetAllSSD(userid: Uid).OrderByDescending(p => p.Ssdcode).Take(4);
            ViewBag.NewPSU = _iwonder.GetAllPowerSuply(userid: Uid).OrderByDescending(p => p.Psucode).Take(4);
            ViewBag.NewCase = _iwonder.GetAllCase(userid: Uid).OrderByDescending(p => p.CaseCode).Take(4);
            #endregion

            #region TopSelling
            ViewBag.GetTopCases = _iwonder.GetTopCases(userid: Uid);
            ViewBag.GetTopVgas = _iwonder.GetTopVgas(userid: Uid);
            ViewBag.GetTopHdds = _iwonder.GetTopHdds(userid: Uid);
            ViewBag.GetTopMotherboards = _iwonder.GetTopMotherboards(userid: Uid);
            ViewBag.GetTopPsus = _iwonder.GetTopPsus(userid: Uid);
            ViewBag.GetTopProcessors = _iwonder.GetTopProcessors(userid: Uid);
            ViewBag.GetTopRams = _iwonder.GetTopRams(userid: Uid);
            ViewBag.GetTopSsds = _iwonder.GetTopSsds(userid: Uid);
            #endregion

            return View();
        }

        #region  Processor
        [HttpGet]
        public IActionResult Processor(int PageNumber = 1, int PageSize = 3)
        {
            int Uid = HttpContext.Session.GetInt32("UserID").GetValueOrDefault();
            HttpContext.Session.SetString("PageSize", PageSize.ToString());
            HttpContext.Session.SetString("PageNumber", PageNumber.ToString());
            var PNumber = int.Parse(HttpContext.Session.GetString("PageNumber")); // Session for PageNumber
            var SNumber = int.Parse(HttpContext.Session.GetString("PageSize")); // Session for PageSize 
            var Data = Pagination.PagedResult(_iwonder.GetAllProcessors(userid: Uid).ToList(), PNumber, SNumber);
            ViewBag.BrandNamesAndNumbers = _iwonder.GetProcessorBrandNamesAndNumbers(); // Get All Brands
            ViewData["PageSize"] = PageSize;
            return View(Data);
        }
        [HttpGet]
        public JsonResult ProcessorAjax(int PageNumber)
        {
            int Uid = HttpContext.Session.GetInt32("UserID").GetValueOrDefault();
            HttpContext.Session.SetString("PageNumber", PageNumber.ToString());
            var SNumber = int.Parse(HttpContext.Session.GetString("PageSize"));
            var PNumber = int.Parse(HttpContext.Session.GetString("PageNumber"));
            var Sort = HttpContext.Session.GetInt32("SortPro") ?? 0;
            var max = HttpContext.Session.GetInt32("Max") ?? 0;
            var min = HttpContext.Session.GetInt32("Min") ?? 0;
            var brand = HttpContext.Session.GetString("BrandsPro") ?? null;
            string[] brands = null;
            if (brand != null)
            {

                brands = brand.Split(',');

                bool IsTrue = brands.Length > 0 && brands[0] != "";
                if (IsTrue)
                {
                    var processor = Pagination.PagedResult(_iwonder.GetProcessorProductsByBrand(brands, Sort, min, max, Uid).ToList(), PageNumber, SNumber);
                    return Json(processor);
                }
            }

            var result = Pagination.PagedResult(_iwonder.GetProcessorPriceDependentOnBrand(Sort,min, max, Uid).ToList(), PNumber, SNumber);
            return Json(result);
        }
        [HttpGet]
        public JsonResult AscendingProcessorProdoucts(int Id)
        {
            int Uid = HttpContext.Session.GetInt32("UserID").GetValueOrDefault();
            int PageSize = int.Parse(HttpContext.Session.GetString("PageSize"));
            int PageNumber = int.Parse(HttpContext.Session.GetString("PageNumber"));
            var max = HttpContext.Session.GetInt32("Max") ?? 0;
            var min = HttpContext.Session.GetInt32("Min") ?? 0;
            if (PageNumber >= 2)
            {
                PageNumber = 1;
            }
            if (Id != 0)
            {
                HttpContext.Session.SetInt32("SortPro", Id);
                if (HttpContext.Session.GetString("BrandsPro") != null)
                {
                    var brands = HttpContext.Session.GetString("BrandsPro").Split(',');
                    if (brands.Length > 0 && brands[0] != "")
                    {
                        var result = Pagination.PagedResult(_iwonder.GetProcessorProductsByBrand(brands, Id, min, max, Uid).ToList(), PageNumber, PageSize);
                        return Json(result);

                    }
                }
            }
            var Data = Pagination.PagedResult(_iwonder.GetProcessorPriceDependentOnBrand( Id, min, max, Uid).ToList(), PageNumber, PageSize);
            return Json(Data);
        }
        [HttpGet]
        public JsonResult DefaultProcessor(int PageSize = 3)
        {
            int Uid = HttpContext.Session.GetInt32("UserID").GetValueOrDefault();
            HttpContext.Session.SetString("PageSize", PageSize.ToString());

            int PNumber = int.Parse(HttpContext.Session.GetString("PageNumber"));
            int SNumber = int.Parse(HttpContext.Session.GetString("PageSize"));
            var Sort = HttpContext.Session.GetInt32("SortPro") ?? 0;
            var max = HttpContext.Session.GetInt32("Max") ?? 0;
            var min = HttpContext.Session.GetInt32("Min") ?? 0;
            if (PNumber >= 2)
            {
                PNumber = 1;
            }
            if (HttpContext.Session.GetString("BrandsPro") != null)
            {
                var brands = HttpContext.Session.GetString("BrandsPro").Split(',');
                if (brands.Length != 0 && brands[0] != "")
                {
                    var result = Pagination.PagedResult(_iwonder.GetProcessorProductsByBrand(brands, Sort, min, max, Uid).ToList(), PNumber, SNumber);
                    //if (result.Data.Count() <= 0)
                    //{
                    //    result.CurrentPage = 1;
                    //}
                    return Json(result);

                }
            }

            var Processors = Pagination.PagedResult(_iwonder.GetProcessorPriceDependentOnBrand(Sort,min, max, Uid).ToList(), PNumber, PageSize);
            HttpContext.Session.SetString("PageNumber", Processors.CurrentPage.ToString());

            //if (Processors.Data.Count() == 0)
            //{
            //    Processors.CurrentPage = 1;
            //}
            return Json(Processors);
        }
        [HttpPost]
        public JsonResult ProductsOfProcessorBrand(string[] brand)
        {
            int Uid = HttpContext.Session.GetInt32("UserID").GetValueOrDefault();
            int PageSize = int.Parse(HttpContext.Session.GetString("PageSize"));
            int PageNumber = int.Parse(HttpContext.Session.GetString("PageNumber"));
            HttpContext.Session.SetString("BrandsPro", string.Join(",", brand));
            var Sort = HttpContext.Session.GetInt32("SortPro") ?? 0;
            var brands = HttpContext.Session.GetString("BrandsPro").Split(',');
            var max = HttpContext.Session.GetInt32("Max") ?? 0;
            var min = HttpContext.Session.GetInt32("Min") ?? 0;
            if (PageNumber >= 2 )
            {
                PageNumber = 1;
            }
            if (brands.Length <= 0 || brands[0] == "")
            {
                var Data = Pagination.PagedResult(_iwonder.GetProcessorPriceDependentOnBrand(Sort,min, max, Uid).ToList(), PageNumber, PageSize);
                HttpContext.Session.SetString("PageNumber", Data.CurrentPage.ToString());

                if (Data.Data.Count() <= 0)
                {
                    Data.CurrentPage = 1;
                    return Json("No");
                }
                return Json(Data);

            }
            else
            {
                var processor = Pagination.PagedResult(_iwonder.GetProcessorProductsByBrand(brands, Sort, min, max, Uid).ToList(), PageNumber, PageSize);
                HttpContext.Session.SetString("PageNumber", processor.CurrentPage.ToString());

                if (processor.Data.Count() <= 0)
                {
                    processor.CurrentPage = 1;
                    return Json("No");
                }
                return Json(processor);


            }

        }
        [HttpGet]
        public JsonResult GetProcessorPrice(int min, int max)
        {
            int Uid = HttpContext.Session.GetInt32("UserID").GetValueOrDefault();
            int PageSize = int.Parse(HttpContext.Session.GetString("PageSize"));
            int PageNumber = int.Parse(HttpContext.Session.GetString("PageNumber"));
            var IsNull = HttpContext.Session.GetString("BrandsPro") ?? null;
            var Sort = HttpContext.Session.GetInt32("SortPro") ?? 0;
            if (PageNumber >= 2)
            {
                PageNumber = 1;
            }
            HttpContext.Session.SetInt32("Max", max);
            HttpContext.Session.SetInt32("Min", min);
            if (IsNull == null)  
            {

                var processor = Pagination.PagedResult(_iwonder.GetProcessorPriceDependentOnBrand(Sort, min, max, Uid).ToList(), PageNumber, PageSize);
                HttpContext.Session.SetString("PageNumber", processor.CurrentPage.ToString());

                if (processor.Data.Count() <= 0)
                {
                    processor.CurrentPage = 1;
                    return Json("No");
                }
                return Json(processor);

            }
            var brands = HttpContext.Session.GetString("BrandsPro").Split(',');
            if (brands[0] == "") {
                var processor = Pagination.PagedResult(_iwonder.GetProcessorPriceDependentOnBrand(Sort, min, max, Uid).ToList(), PageNumber, PageSize);
                HttpContext.Session.SetString("PageNumber", processor.CurrentPage.ToString());

                if (processor.Data.Count() <= 0)
                {
                    processor.CurrentPage = 1;
                    return Json("No");

                }
                return Json(processor);
            }
            var result = Pagination.PagedResult(_iwonder.GetProcessorProductsByBrand(brands, Sort, min, max, Uid).ToList(), PageNumber, PageSize);
            HttpContext.Session.SetString("PageNumber", result.CurrentPage.ToString());

            if (result.Data.Count() <= 0)
            {
                result.CurrentPage = 1;
                return Json("No");

            }
            return Json(result);


        }
        #endregion

        #region Motherboard

        [HttpGet]
        public IActionResult Motherboard(int pageNumber = 1, int PageSize = 3)
        {
            int Uid = HttpContext.Session.GetInt32("UserID").GetValueOrDefault(); 
            HttpContext.Session.SetString("PageSizeMoh", PageSize.ToString());
            HttpContext.Session.SetString("PageNumberMoh", pageNumber.ToString());
            var PNumber = int.Parse(HttpContext.Session.GetString("PageNumberMoh"));
            var SNumber = int.Parse(HttpContext.Session.GetString("PageSizeMoh"));
            var Data = Pagination.PagedResult(_iwonder.GetAllMotherboard(userid: Uid).ToList(), PNumber, SNumber);
            ViewBag.BrandNamesAndNumbers = _iwonder.GetMotherboardBrandNamesAndNumbers(userid: Uid); 
            ViewData["PageSizeMoh"] = PageSize;
            return View(Data);
        }
        [HttpGet]
        public JsonResult MotherboardAjax(int PageNumber)
        {
            int Uid = HttpContext.Session.GetInt32("UserID").GetValueOrDefault();
            HttpContext.Session.SetString("PageNumberMoh", PageNumber.ToString());
            var SNumber = int.Parse(HttpContext.Session.GetString("PageSizeMoh"));
            var PNumber = int.Parse(HttpContext.Session.GetString("PageNumberMoh"));
            var Sort = HttpContext.Session.GetInt32("SortMoh") ?? 0;
            var max = HttpContext.Session.GetInt32("MaxMoh") ?? 0;
            var min = HttpContext.Session.GetInt32("MinMoh") ?? 0;
            var brand = HttpContext.Session.GetString("BrandsMoh") ?? null;
            string[] brands;
            if (brand != null)
            {
                brands = brand.Split(',');
                if (brands.Length > 0 && brands[0] != "")
                {
                    var motherboards = Pagination.PagedResult(_iwonder.GetMotherboardProductsByBrand(brands, Sort, min, max, Uid).ToList(), PageNumber, SNumber);
                    return Json(motherboards);
                }
            }

            var result = Pagination.PagedResult(_iwonder.GetMotherboardPriceDependentOnBrand(Sort, min, max, Uid).ToList(), PNumber, SNumber);
            return Json(result);
        }
        [HttpGet]
        public JsonResult AscendingMotherboardProdoucts(int Id)
        {
            int Uid = HttpContext.Session.GetInt32("UserID").GetValueOrDefault();
            int PageSize = int.Parse(HttpContext.Session.GetString("PageSizeMoh"));
            int PageNumber = int.Parse(HttpContext.Session.GetString("PageNumberMoh"));
            var max = HttpContext.Session.GetInt32("MaxMoh") ?? 0;
            var min = HttpContext.Session.GetInt32("MinMoh") ?? 0;
            if (PageNumber >= 2)
            {
                PageNumber = 1;
            }
            if (Id != 0)
            {
                HttpContext.Session.SetInt32("SortMoh", Id);
                if (HttpContext.Session.GetString("BrandsMoh") != null)
                {
                    var brands = HttpContext.Session.GetString("BrandsMoh").Split(',');
                    if (brands.Length > 0 && brands[0] != "")
                    {
                        var result = Pagination.PagedResult(_iwonder.GetMotherboardProductsByBrand(brands, Id, min, max, Uid).ToList(), PageNumber, PageSize);
                        return Json(result);
                    }
                }
            }
            var Data = Pagination.PagedResult(_iwonder.GetMotherboardPriceDependentOnBrand(Id, min, max, Uid).ToList(), PageNumber, PageSize);
            return Json(Data);
        }

        [HttpGet]
        public JsonResult DefaultMotherboard(int PageSize = 3)
        {
            int Uid = HttpContext.Session.GetInt32("UserID").GetValueOrDefault();
            HttpContext.Session.SetString("PageSizeMoh", PageSize.ToString());
            int PNumber = int.Parse(HttpContext.Session.GetString("PageNumberMoh"));
            int SNumber = int.Parse(HttpContext.Session.GetString("PageSizeMoh"));
            var Sort = HttpContext.Session.GetInt32("SortMoh") ?? 0;
            var max = HttpContext.Session.GetInt32("MaxMoh") ?? 0;
            var min = HttpContext.Session.GetInt32("MinMoh") ?? 0;
            if (PNumber >= 2)
            {
                PNumber = 1;
            }
            if (HttpContext.Session.GetString("BrandsMoh") != null)
            {
                var brands = HttpContext.Session.GetString("BrandsMoh").Split(',');
                if (brands.Length != 0 && brands[0] != "")
                {
                    var result = Pagination.PagedResult(_iwonder.GetMotherboardProductsByBrand(brands, Sort, min, max, Uid).ToList(), PNumber, SNumber);
                    //if (result.Data.Count() <= 0)
                    //{
                    //    result.CurrentPage = 1;
                    //}
                    return Json(result);

                }
            }

            var Data = Pagination.PagedResult(_iwonder.GetMotherboardPriceDependentOnBrand(Sort, min, max, Uid).ToList(), PNumber, PageSize);
            HttpContext.Session.SetString("PageNumberMoh",Data.CurrentPage.ToString());

            //if (Processors.Data.Count() == 0)
            //{
            //    Processors.CurrentPage = 1;
            //}
            return Json(Data);
        }

        [HttpPost]
        public JsonResult ProductsOfMotherboardBrand(string[] brand)
        {
            int Uid = HttpContext.Session.GetInt32("UserID").GetValueOrDefault();
            int PageSize = int.Parse(HttpContext.Session.GetString("PageSizeMoh"));
            int PageNumber = int.Parse(HttpContext.Session.GetString("PageNumberMoh"));
            HttpContext.Session.SetString("BrandsMoh", string.Join(",", brand));
            var Sort = HttpContext.Session.GetInt32("SortMoh") ?? 0;
            var brands = HttpContext.Session.GetString("BrandsMoh").Split(',');
            var max = HttpContext.Session.GetInt32("MaxMoh") ?? 0;
            var min = HttpContext.Session.GetInt32("MinMoh") ?? 0;
            if (PageNumber >= 2)
            {
                PageNumber = 1;
            }
            if (brands.Length <= 0 || brands[0] == "")
            {
                var Data = Pagination.PagedResult(_iwonder.GetMotherboardPriceDependentOnBrand(Sort, min, max, Uid).ToList(), PageNumber, PageSize);
                HttpContext.Session.SetString("PageNumberMoh", Data.CurrentPage.ToString());

                if (Data.Data.Count() <= 0)
                {
                    Data.CurrentPage = 1;
                         return Json("No");
                }
                return Json(Data);

            }
            else
            {
                var result = Pagination.PagedResult(_iwonder.GetMotherboardProductsByBrand(brands, Sort, min, max, Uid).ToList(), PageNumber, PageSize);
                HttpContext.Session.SetString("PageNumberMoh", result.CurrentPage.ToString());

                if (result.Data.Count() <= 0)
                {
                    result.CurrentPage = 1;
                    return Json("No");

                }
                return Json(result);


            }
        }
        [HttpGet]
        public JsonResult GetMotherboardPrice(int min, int max)
        {
            int Uid = HttpContext.Session.GetInt32("UserID").GetValueOrDefault();
            int PageSize = int.Parse(HttpContext.Session.GetString("PageSizeMoh"));
            int PageNumber = int.Parse(HttpContext.Session.GetString("PageNumberMoh"));
            var IsNull = HttpContext.Session.GetString("BrandsMoh") ?? null;
            var Sort = HttpContext.Session.GetInt32("SortMoh") ?? 0;
            if (PageNumber >= 2)
            {
                PageNumber = 1;
            }
            HttpContext.Session.SetInt32("MaxMoh", max);
            HttpContext.Session.SetInt32("MinMoh", min);
            if (IsNull == null)
            {

                var motherboards = Pagination.PagedResult(_iwonder.GetMotherboardPriceDependentOnBrand(Sort, min, max, Uid).ToList(), PageNumber, PageSize);
                HttpContext.Session.SetString("PageNumberMoh", motherboards.CurrentPage.ToString());

                if (motherboards.Data.Count() <= 0)
                {
                    motherboards.CurrentPage = 1;
                    return Json("No");
                }
                return Json(motherboards);

            }
            var brands = HttpContext.Session.GetString("BrandsMoh").Split(',');
            if (brands[0] == "")
            {
                var MotherDepenOn = Pagination.PagedResult(_iwonder.GetMotherboardPriceDependentOnBrand(Sort, min, max, Uid).ToList(), PageNumber, PageSize);
                HttpContext.Session.SetString("PageNumberMoh", MotherDepenOn.CurrentPage.ToString());

                if (MotherDepenOn.Data.Count() <= 0)
                {
                    MotherDepenOn.CurrentPage = 1;
                    return Json("No");
                }
                return Json(MotherDepenOn);
            }
            var result = Pagination.PagedResult(_iwonder.GetMotherboardProductsByBrand(brands, Sort, min, max, Uid).ToList(), PageNumber, PageSize);
            HttpContext.Session.SetString("PageNumberMoh", result.CurrentPage.ToString());

            if (result.Data.Count() <= 0)
            {
                result.CurrentPage = 1;
                return Json("No");
            }
            return Json(result);
        }
        #endregion

        #region HDD

        [HttpGet]
        public IActionResult HDD(int pageNumber = 1, int PageSize = 3)
        {
            int Uid = HttpContext.Session.GetInt32("UserID").GetValueOrDefault();
            HttpContext.Session.SetString("PageSizehdd", PageSize.ToString());
            HttpContext.Session.SetString("PageNumberhdd", pageNumber.ToString());
            var PNumber = int.Parse(HttpContext.Session.GetString("PageNumberhdd")); 
            var SNumber = int.Parse(HttpContext.Session.GetString("PageSizehdd"));  
            var Data = Pagination.PagedResult(_iwonder.GetAllHDD(userid: Uid).ToList(), PNumber, SNumber);
            ViewBag.BrandNamesAndNumbers = _iwonder.GetHDDBrandNamesAndNumbers(userid: Uid); 
            ViewData["PageSizehdd"] = PageSize;
            return View(Data);
        }
        [HttpGet]
        public JsonResult HDDAjax(int PageNumber)
        {
            int Uid = HttpContext.Session.GetInt32("UserID").GetValueOrDefault();
            HttpContext.Session.SetString("PageNumberhdd", PageNumber.ToString());
            var SNumber = int.Parse(HttpContext.Session.GetString("PageSizehdd"));
            var PNumber = int.Parse(HttpContext.Session.GetString("PageNumberhdd"));
            var Sort = HttpContext.Session.GetInt32("Sorthdd") ?? 0;
            var max = HttpContext.Session.GetInt32("Maxhdd") ?? 0;
            var min = HttpContext.Session.GetInt32("Minhdd") ?? 0;
            var brand = HttpContext.Session.GetString("Brandshdd") ?? null;
            string[] brands;
            if (brand != null)
            {
                brands = brand.Split(',');
                if (brands.Length > 0 && brands[0] != "")
                {
                    var hdds = Pagination.PagedResult(_iwonder.GetHDDProductsByBrand(brands, Sort, min, max, Uid).ToList(), PageNumber, SNumber);
                    return Json(hdds);
                }
            }

            var result = Pagination.PagedResult(_iwonder.GetHDDPriceDependentOnBrand(Sort, min, max, Uid).ToList(), PNumber, SNumber);
            return Json(result);
        }
        [HttpGet]
        public JsonResult AscendingHDDProdoucts(int Id)
        {
            int Uid = HttpContext.Session.GetInt32("UserID").GetValueOrDefault();
            int PageSize = int.Parse(HttpContext.Session.GetString("PageSizehdd"));
            int PageNumber = int.Parse(HttpContext.Session.GetString("PageNumberhdd"));
            var max = HttpContext.Session.GetInt32("Maxhdd") ?? 0;
            var min = HttpContext.Session.GetInt32("Minhdd") ?? 0;
            if (PageNumber >= 2)
            {
                PageNumber = 1;
            }
            if (Id != 0)
            {
                HttpContext.Session.SetInt32("Sorthdd", Id);
                if (HttpContext.Session.GetString("Brandshdd") != null)
                {
                    var brands = HttpContext.Session.GetString("Brandshdd").Split(',');
                    if (brands.Length > 0 && brands[0] != "")
                    {
                        var result = Pagination.PagedResult(_iwonder.GetHDDProductsByBrand(brands, Id, min, max, Uid).ToList(), PageNumber, PageSize);
                        return Json(result);
                    }
                }
            }
            var Data = Pagination.PagedResult(_iwonder.GetHDDPriceDependentOnBrand(Id, min, max, Uid).ToList(), PageNumber, PageSize);
            return Json(Data);
        }

        [HttpGet]
        public JsonResult DefaultHDD(int PageSize = 3)
        {
            int Uid = HttpContext.Session.GetInt32("UserID").GetValueOrDefault();
            HttpContext.Session.SetString("PageSizehdd", PageSize.ToString());
            int PNumber = int.Parse(HttpContext.Session.GetString("PageNumberhdd"));
            int SNumber = int.Parse(HttpContext.Session.GetString("PageSizehdd"));
            var Sort = HttpContext.Session.GetInt32("Sorthdd") ?? 0;
            var max = HttpContext.Session.GetInt32("Maxhdd") ?? 0;
            var min = HttpContext.Session.GetInt32("Minhdd") ?? 0;
            if (PNumber >= 2)
            {
                PNumber = 1;
            }
            if (HttpContext.Session.GetString("Brandshdd") != null)
            {
                var brands = HttpContext.Session.GetString("Brandshdd").Split(',');
                if (brands.Length != 0 && brands[0] != "")
                {
                    var result = Pagination.PagedResult(_iwonder.GetHDDProductsByBrand(brands, Sort, min, max, Uid).ToList(), PNumber, SNumber);
                    //if (result.Data.Count() <= 0)
                    //{
                    //    result.CurrentPage = 1;
                    //}
                    return Json(result);

                }
            }

            var Data = Pagination.PagedResult(_iwonder.GetHDDPriceDependentOnBrand(Sort, min, max, Uid).ToList(), PNumber, PageSize);
            HttpContext.Session.SetString("PageNumberMoh", Data.CurrentPage.ToString());

            //if (Processors.Data.Count() == 0)
            //{
            //    Processors.CurrentPage = 1;
            //}
            return Json(Data);
        }

        [HttpPost]
        public JsonResult ProductsOfHDDBrand(string[] brand)
        {
            int Uid = HttpContext.Session.GetInt32("UserID").GetValueOrDefault();
            int PageSize = int.Parse(HttpContext.Session.GetString("PageSizehdd"));
            int PageNumber = int.Parse(HttpContext.Session.GetString("PageNumberhdd"));
            HttpContext.Session.SetString("Brandshdd", string.Join(",", brand));
            var Sort = HttpContext.Session.GetInt32("Sorthdd") ?? 0;
            var brands = HttpContext.Session.GetString("Brandshdd").Split(',');
            var max = HttpContext.Session.GetInt32("Maxhdd") ?? 0;
            var min = HttpContext.Session.GetInt32("Minhdd") ?? 0;
            if (PageNumber >= 2)
            {
                PageNumber = 1;
            }
            if (brands.Length <= 0 || brands[0] == "")
            {
                var Data = Pagination.PagedResult(_iwonder.GetHDDPriceDependentOnBrand(Sort, min, max, Uid).ToList(), PageNumber, PageSize);
                HttpContext.Session.SetString("PageNumberhdd", Data.CurrentPage.ToString());

                if (Data.Data.Count() <= 0)
                {
                    Data.CurrentPage = 1;
                    return Json("No");
                }
                return Json(Data);

            }
            else
            {
                var result = Pagination.PagedResult(_iwonder.GetHDDProductsByBrand(brands, Sort, min, max, Uid).ToList(), PageNumber, PageSize);
                HttpContext.Session.SetString("PageNumberhdd", result.CurrentPage.ToString());

                if (result.Data.Count() <= 0)
                {
                    result.CurrentPage = 1;
                    return Json("No");
                }
                return Json(result);


            }
        }
        [HttpGet]
        public JsonResult GetHDDPrice(int min, int max)
        {
            int Uid = HttpContext.Session.GetInt32("UserID").GetValueOrDefault();
            int PageSize = int.Parse(HttpContext.Session.GetString("PageSizehdd"));
            int PageNumber = int.Parse(HttpContext.Session.GetString("PageNumberhdd"));
            var IsNull = HttpContext.Session.GetString("Brandshdd") ?? null;
            var Sort = HttpContext.Session.GetInt32("Sorthdd") ?? 0;
            if (PageNumber >= 2)
            {
                PageNumber = 1;
            }
            HttpContext.Session.SetInt32("Maxhdd", max);
            HttpContext.Session.SetInt32("Minhdd", min);
            if (IsNull == null)
            {

                var Data = Pagination.PagedResult(_iwonder.GetHDDPriceDependentOnBrand(Sort, min, max, Uid).ToList(), PageNumber, PageSize);
                HttpContext.Session.SetString("PageNumberhdd", Data.CurrentPage.ToString());

                if (Data.Data.Count() <= 0)
                {
                    Data.CurrentPage = 1;
                    return Json("No");
                }
                return Json(Data);

            }
            var brands = HttpContext.Session.GetString("Brandshdd").Split(',');
            if (brands[0] == "")
            {
                var HddDepenOn = Pagination.PagedResult(_iwonder.GetHDDPriceDependentOnBrand(Sort, min, max, Uid).ToList(), PageNumber, PageSize);
                HttpContext.Session.SetString("PageNumberhdd", HddDepenOn.CurrentPage.ToString());

                if (HddDepenOn.Data.Count() <= 0)
                {
                    HddDepenOn.CurrentPage = 1;
                    return Json("No");
                }
                return Json(HddDepenOn);
            }
            var result = Pagination.PagedResult(_iwonder.GetHDDProductsByBrand(brands, Sort, min, max, Uid).ToList(), PageNumber, PageSize);
            HttpContext.Session.SetString("PageNumberhdd", result.CurrentPage.ToString());

            if (result.Data.Count() <= 0)
            {
                result.CurrentPage = 1;
                return Json("No");
            }
            return Json(result);
        }
        #endregion

        #region RAM

        [HttpGet]
        public IActionResult RAM(int pageNumber = 1, int PageSize = 3)
        {
            int Uid = HttpContext.Session.GetInt32("UserID").GetValueOrDefault();
            HttpContext.Session.SetString("PageSizeram", PageSize.ToString());
            HttpContext.Session.SetString("PageNumberram", pageNumber.ToString());
            var PNumber = int.Parse(HttpContext.Session.GetString("PageNumberram")); 
            var SNumber = int.Parse(HttpContext.Session.GetString("PageSizeram"));
            var Data = Pagination.PagedResult(_iwonder.GetAllRAM(userid: Uid).ToList(), PNumber, SNumber);
            ViewBag.BrandNamesAndNumbers = _iwonder.GetRAMBrandNamesAndNumbers(userid: Uid);
            ViewData["PageSizeram"] = PageSize;
            return View(Data);
        }
        [HttpGet]
        public JsonResult RAMAjax(int PageNumber)
        {

            int Uid = HttpContext.Session.GetInt32("UserID").GetValueOrDefault();
            HttpContext.Session.SetString("PageNumberram", PageNumber.ToString());
            var SNumber = int.Parse(HttpContext.Session.GetString("PageSizeram"));
            var PNumber = int.Parse(HttpContext.Session.GetString("PageNumberram"));
            var Sort = HttpContext.Session.GetInt32("Sortram") ?? 0;
            var max = HttpContext.Session.GetInt32("Maxram") ?? 0;
            var min = HttpContext.Session.GetInt32("Minram") ?? 0;
            var brand = HttpContext.Session.GetString("Brandsram") ?? null;
            string[] brands;
            if (brand != null)
            {
                brands = brand.Split(',');
                if (brands.Length > 0 && brands[0] != "")
                {
                    var rams = Pagination.PagedResult(_iwonder.GetRAMProductsByBrand(brands, Sort, min, max, Uid).ToList(), PageNumber, SNumber);
                    return Json(rams);
                }
            }

            var result = Pagination.PagedResult(_iwonder.GetRAMPriceDependentOnBrand(Sort, min, max, Uid).ToList(), PNumber, SNumber);
            return Json(result);
        }

        [HttpGet]
        public JsonResult AscendingRAMProdoucts(int Id)
        {
            int Uid = HttpContext.Session.GetInt32("UserID").GetValueOrDefault();
            int PageSize = int.Parse(HttpContext.Session.GetString("PageSizeram"));
            int PageNumber = int.Parse(HttpContext.Session.GetString("PageNumberram"));
            var max = HttpContext.Session.GetInt32("Maxram") ?? 0;
            var min = HttpContext.Session.GetInt32("Minram") ?? 0;
            if (PageNumber >= 2)
            {
                PageNumber = 1;
            }
            if (Id != 0)
            {
                HttpContext.Session.SetInt32("Sortram", Id);
                if (HttpContext.Session.GetString("Brandsram") != null)
                {
                    var brands = HttpContext.Session.GetString("Brandsram").Split(',');
                    if (brands.Length > 0 && brands[0] != "")
                    {
                        var result = Pagination.PagedResult(_iwonder.GetRAMProductsByBrand(brands, Id, min, max, Uid).ToList(), PageNumber, PageSize);
                        return Json(result);
                    }
                }
            }
            var Data = Pagination.PagedResult(_iwonder.GetRAMPriceDependentOnBrand(Id, min, max, Uid).ToList(), PageNumber, PageSize);
            return Json(Data);
        }

        [HttpGet]
        public JsonResult DefaultRAM(int PageSize = 3)
        {

            int Uid = HttpContext.Session.GetInt32("UserID").GetValueOrDefault();
            HttpContext.Session.SetString("PageSizeram", PageSize.ToString());
            int PNumber = int.Parse(HttpContext.Session.GetString("PageNumberram"));
            int SNumber = int.Parse(HttpContext.Session.GetString("PageSizeram"));
            var Sort = HttpContext.Session.GetInt32("Sortram") ?? 0;
            var max = HttpContext.Session.GetInt32("Maxram") ?? 0;
            var min = HttpContext.Session.GetInt32("Minram") ?? 0;
            if (PNumber >= 2)
            {
                PNumber = 1;
            }
            if (HttpContext.Session.GetString("Brandsram") != null)
            {
                var brands = HttpContext.Session.GetString("Brandsram").Split(',');
                if (brands.Length != 0 && brands[0] != "")
                {
                    var result = Pagination.PagedResult(_iwonder.GetRAMProductsByBrand(brands, Sort, min, max, Uid).ToList(), PNumber, SNumber);
                    //if (result.Data.Count() <= 0)
                    //{
                    //    result.CurrentPage = 1;
                    //}
                    return Json(result);

                }
            }

            var Data = Pagination.PagedResult(_iwonder.GetRAMPriceDependentOnBrand(Sort, min, max, Uid).ToList(), PNumber, PageSize);
            HttpContext.Session.SetString("PageNumberram", Data.CurrentPage.ToString());

            //if (Processors.Data.Count() == 0)
            //{
            //    Processors.CurrentPage = 1;
            //}
            return Json(Data);
        }

        [HttpPost]
        public JsonResult ProductsOfRAMBrand(string[] brand)
        {
            int Uid = HttpContext.Session.GetInt32("UserID").GetValueOrDefault();
            int PageSize = int.Parse(HttpContext.Session.GetString("PageSizeram"));
            int PageNumber = int.Parse(HttpContext.Session.GetString("PageNumberram"));
            HttpContext.Session.SetString("Brandsram", string.Join(",", brand));
            var Sort = HttpContext.Session.GetInt32("Sortram") ?? 0;
            var brands = HttpContext.Session.GetString("Brandsram").Split(',');
            var max = HttpContext.Session.GetInt32("Maxram") ?? 0;
            var min = HttpContext.Session.GetInt32("Minram") ?? 0;
            if (PageNumber >= 2)
            {
                PageNumber = 1;
            }
            if (brands.Length <= 0 || brands[0] == "")
            {
                var Data = Pagination.PagedResult(_iwonder.GetRAMPriceDependentOnBrand(Sort, min, max, Uid).ToList(), PageNumber, PageSize);
                HttpContext.Session.SetString("PageNumberram", Data.CurrentPage.ToString());

                if (Data.Data.Count() <= 0)
                {
                    Data.CurrentPage = 1;
                    return Json("No");
                }
                return Json(Data);

            }
            else
            {
                var result = Pagination.PagedResult(_iwonder.GetRAMProductsByBrand(brands, Sort, min, max, Uid).ToList(), PageNumber, PageSize);
                HttpContext.Session.SetString("PageNumberram", result.CurrentPage.ToString());

                if (result.Data.Count() <= 0)
                {
                    result.CurrentPage = 1;
                    return Json("No");
                }
                return Json(result);


            }
        }
        [HttpGet]
        public JsonResult GetRAMPrice(int min, int max)
        {
            int Uid = HttpContext.Session.GetInt32("UserID").GetValueOrDefault();
            int PageSize = int.Parse(HttpContext.Session.GetString("PageSizeram"));
            int PageNumber = int.Parse(HttpContext.Session.GetString("PageNumberram"));
            var IsNull = HttpContext.Session.GetString("Brandsram") ?? null;
            var Sort = HttpContext.Session.GetInt32("Sortram") ?? 0;
            if (PageNumber >= 2)
            {
                PageNumber = 1;
            }
            HttpContext.Session.SetInt32("Maxram", max);
            HttpContext.Session.SetInt32("Minram", min);
            if (IsNull == null)
            {

                var Data = Pagination.PagedResult(_iwonder.GetRAMPriceDependentOnBrand(Sort, min, max, Uid).ToList(), PageNumber, PageSize);
                HttpContext.Session.SetString("PageNumberram", Data.CurrentPage.ToString());

                if (Data.Data.Count() <= 0)
                {
                    Data.CurrentPage = 1;
                    return Json("No");
                }
                return Json(Data);

            }
            var brands = HttpContext.Session.GetString("Brandsram").Split(',');
            if (brands[0] == "")
            {
                var RAMDepenOn = Pagination.PagedResult(_iwonder.GetRAMPriceDependentOnBrand(Sort, min, max, Uid).ToList(), PageNumber, PageSize);
                HttpContext.Session.SetString("PageNumberram", RAMDepenOn.CurrentPage.ToString());

                if (RAMDepenOn.Data.Count() <= 0)
                {
                    RAMDepenOn.CurrentPage = 1;
                    return Json("No");
                }
                return Json(RAMDepenOn);
            }
            var result = Pagination.PagedResult(_iwonder.GetRAMProductsByBrand(brands, Sort, min, max, Uid).ToList(), PageNumber, PageSize);
            HttpContext.Session.SetString("PageNumberram", result.CurrentPage.ToString());

            if (result.Data.Count() <= 0)
            {
                result.CurrentPage = 1;
                return Json("No");
            }
            return Json(result);
        }
        #endregion

        #region Graphics Card

        [HttpGet]
        public IActionResult GraphicsCard(int pageNumber = 1, int PageSize = 3)
        {
            int Uid = HttpContext.Session.GetInt32("UserID").GetValueOrDefault();
            HttpContext.Session.SetString("PageSizecar", PageSize.ToString());
            HttpContext.Session.SetString("PageNumbercar", pageNumber.ToString());
            var PNumber = int.Parse(HttpContext.Session.GetString("PageNumbercar")); 
            var SNumber = int.Parse(HttpContext.Session.GetString("PageSizecar"));  
            var Data = Pagination.PagedResult(_iwonder.GetAllCard(userid: Uid).ToList(), PNumber, SNumber);
            ViewBag.BrandNamesAndNumbers = _iwonder.GetCardBrandNamesAndNumbers(userid: Uid);
            ViewData["PageSizecar"] = PageSize;
            return View(Data);
        }
        [HttpGet]
        public JsonResult CardAjax(int PageNumber)
        {

            int Uid = HttpContext.Session.GetInt32("UserID").GetValueOrDefault();
            HttpContext.Session.SetString("PageNumbercar", PageNumber.ToString());
            var SNumber = int.Parse(HttpContext.Session.GetString("PageSizecar"));
            var PNumber = int.Parse(HttpContext.Session.GetString("PageNumbercar"));
            var Sort = HttpContext.Session.GetInt32("Sortcar") ?? 0;
            var max = HttpContext.Session.GetInt32("Maxcar") ?? 0;
            var min = HttpContext.Session.GetInt32("Mincar") ?? 0;
            var brand = HttpContext.Session.GetString("Brandscar") ?? null;
            string[] brands;
            if (brand != null)
            {
                brands = brand.Split(',');
                if (brands.Length > 0 && brands[0] != "")
                {
                    var cards = Pagination.PagedResult(_iwonder.GetCardProductsByBrand(brands, Sort, min, max, Uid).ToList(), PageNumber, SNumber);
                    return Json(cards);
                }
            }

            var result = Pagination.PagedResult(_iwonder.GetCardPriceDependentOnBrand(Sort, min, max, Uid).ToList(), PNumber, SNumber);
            return Json(result);
        }

        [HttpGet]
        public JsonResult AscendingCardProdoucts(int Id)
        {
            int Uid = HttpContext.Session.GetInt32("UserID").GetValueOrDefault();
            int PageSize = int.Parse(HttpContext.Session.GetString("PageSizecar"));
            int PageNumber = int.Parse(HttpContext.Session.GetString("PageNumbercar"));
            var max = HttpContext.Session.GetInt32("Maxcar") ?? 0;
            var min = HttpContext.Session.GetInt32("Mincar") ?? 0;
            if (PageNumber >= 2)
            {
                PageNumber = 1;
            }
            if (Id != 0)
            {
                HttpContext.Session.SetInt32("Sortcar", Id);
                if (HttpContext.Session.GetString("Brandscar") != null)
                {
                    var brands = HttpContext.Session.GetString("Brandscar").Split(',');
                    if (brands.Length > 0 && brands[0] != "")
                    {
                        var result = Pagination.PagedResult(_iwonder.GetCardProductsByBrand(brands, Id, min, max, Uid).ToList(), PageNumber, PageSize);
                        return Json(result);
                    }
                }
            }
            var Data = Pagination.PagedResult(_iwonder.GetCardPriceDependentOnBrand(Id, min, max, Uid).ToList(), PageNumber, PageSize);
            return Json(Data);
        }

        [HttpGet]
        public JsonResult DefaultCard(int PageSize = 3)
        {
            int Uid = HttpContext.Session.GetInt32("UserID").GetValueOrDefault();
            HttpContext.Session.SetString("PageSizecar", PageSize.ToString());
            int PNumber = int.Parse(HttpContext.Session.GetString("PageNumbercar"));
            int SNumber = int.Parse(HttpContext.Session.GetString("PageSizecar"));
            var Sort = HttpContext.Session.GetInt32("Sortcar") ?? 0;
            var max = HttpContext.Session.GetInt32("Maxcar") ?? 0;
            var min = HttpContext.Session.GetInt32("Mincar") ?? 0;
            if (PNumber >= 2)
            {
                PNumber = 1;
            }
            if (HttpContext.Session.GetString("Brandscar") != null)
            {
                var brands = HttpContext.Session.GetString("Brandscar").Split(',');
                if (brands.Length != 0 && brands[0] != "")
                {
                    var result = Pagination.PagedResult(_iwonder.GetCardProductsByBrand(brands, Sort, min, max, Uid).ToList(), PNumber, SNumber);
                    //if (result.Data.Count() <= 0)
                    //{
                    //    result.CurrentPage = 1;
                    //}
                    return Json(result);

                }
            }

            var Data = Pagination.PagedResult(_iwonder.GetCardPriceDependentOnBrand(Sort, min, max, Uid).ToList(), PNumber, PageSize);
            HttpContext.Session.SetString("PageNumbercar", Data.CurrentPage.ToString());

            //if (Processors.Data.Count() == 0)
            //{
            //    Processors.CurrentPage = 1;
            //}
            return Json(Data);
        }

        [HttpPost]
        public JsonResult ProductsOfCardBrand(string[] brand)
        {
            int Uid = HttpContext.Session.GetInt32("UserID").GetValueOrDefault();
            int PageSize = int.Parse(HttpContext.Session.GetString("PageSizecar"));
            int PageNumber = int.Parse(HttpContext.Session.GetString("PageNumbercar"));
            HttpContext.Session.SetString("Brandscar", string.Join(",", brand));
            var Sort = HttpContext.Session.GetInt32("Sortcar") ?? 0;
            var brands = HttpContext.Session.GetString("Brandscar").Split(',');
            var max = HttpContext.Session.GetInt32("Maxcar") ?? 0;
            var min = HttpContext.Session.GetInt32("Mincar") ?? 0;
            if (PageNumber >= 2)
            {
                PageNumber = 1;
            }
            if (brands.Length <= 0 || brands[0] == "")
            {
                var Data = Pagination.PagedResult(_iwonder.GetCardPriceDependentOnBrand(Sort, min, max, Uid).ToList(), PageNumber, PageSize);
                HttpContext.Session.SetString("PageNumbercar", Data.CurrentPage.ToString());

                if (Data.Data.Count() <= 0)
                {
                    Data.CurrentPage = 1;
                    return Json("No");
                }
                return Json(Data);

            }
            else
            {
                var result = Pagination.PagedResult(_iwonder.GetCardProductsByBrand(brands, Sort, min, max, Uid).ToList(), PageNumber, PageSize);
                HttpContext.Session.SetString("PageNumbercar", result.CurrentPage.ToString());

                if (result.Data.Count() <= 0)
                {
                    result.CurrentPage = 1;
                    return Json("No");
                }
                return Json(result);


            }
        }
        [HttpGet]
        public JsonResult GetCardPrice(int min, int max)
        {
            int Uid = HttpContext.Session.GetInt32("UserID").GetValueOrDefault();
            int PageSize = int.Parse(HttpContext.Session.GetString("PageSizecar"));
            int PageNumber = int.Parse(HttpContext.Session.GetString("PageNumbercar"));
            var IsNull = HttpContext.Session.GetString("Brandscar") ?? null;
            var Sort = HttpContext.Session.GetInt32("Sortcar") ?? 0;
            if (PageNumber >= 2)
            {
                PageNumber = 1;
            }
            HttpContext.Session.SetInt32("Maxcar", max);
            HttpContext.Session.SetInt32("Mincar", min);
            if (IsNull == null)
            {

                var Data = Pagination.PagedResult(_iwonder.GetCardPriceDependentOnBrand(Sort, min, max, Uid).ToList(), PageNumber, PageSize);
                HttpContext.Session.SetString("PageNumbercar", Data.CurrentPage.ToString());

                if (Data.Data.Count() <= 0)
                {
                    Data.CurrentPage = 1;
                    return Json("No");
                }
                return Json(Data);

            }
            var brands = HttpContext.Session.GetString("Brandscar").Split(',');
            if (brands[0] == "")
            {
                var CardDepenOn = Pagination.PagedResult(_iwonder.GetCardPriceDependentOnBrand(Sort, min, max, Uid).ToList(), PageNumber, PageSize);
                HttpContext.Session.SetString("PageNumbercar", CardDepenOn.CurrentPage.ToString());

                if (CardDepenOn.Data.Count() <= 0)
                {
                    CardDepenOn.CurrentPage = 1;
                    return Json("No");
                }
                return Json(CardDepenOn);
            }
            var result = Pagination.PagedResult(_iwonder.GetCardProductsByBrand(brands, Sort, min, max, Uid).ToList(), PageNumber, PageSize);
            HttpContext.Session.SetString("PageNumbercar", result.CurrentPage.ToString());

            if (result.Data.Count() <= 0)
            {
                result.CurrentPage = 1;
                return Json("No");
            }
            return Json(result);
        }
        #endregion

        #region SSD
        [HttpGet]
        public IActionResult SSD(int pageNumber = 1, int PageSize = 3)
        {
            int Uid = HttpContext.Session.GetInt32("UserID").GetValueOrDefault();
            HttpContext.Session.SetString("PageSizess", PageSize.ToString());
            HttpContext.Session.SetString("PageNumberss", pageNumber.ToString());
            var PNumber = int.Parse(HttpContext.Session.GetString("PageNumberss")); 
            var SNumber = int.Parse(HttpContext.Session.GetString("PageSizess")); 
            var Data = Pagination.PagedResult(_iwonder.GetAllSSD(userid: Uid).ToList(), PNumber, SNumber);
            ViewBag.BrandNamesAndNumbers = _iwonder.GetSSDBrandNamesAndNumbers(userid: Uid); 
            ViewData["PageSizess"] = PageSize;
            return View(Data);
        }
        [HttpGet]
        public JsonResult SSDAjax(int PageNumber)
        {

            int Uid = HttpContext.Session.GetInt32("UserID").GetValueOrDefault();
            HttpContext.Session.SetString("PageNumberss", PageNumber.ToString());
            var SNumber = int.Parse(HttpContext.Session.GetString("PageSizess"));
            var PNumber = int.Parse(HttpContext.Session.GetString("PageNumberss"));
            var Sort = HttpContext.Session.GetInt32("Sortss") ?? 0;
            var max = HttpContext.Session.GetInt32("Maxss") ?? 0;
            var min = HttpContext.Session.GetInt32("Minss") ?? 0;
            var brand = HttpContext.Session.GetString("Brandsss") ?? null;
            string[] brands;
            if (brand != null)
            {
                brands = brand.Split(',');
                if (brands.Length > 0 && brands[0] != "")
                {
                    var ssds = Pagination.PagedResult(_iwonder.GetSSDProductsByBrand(brands, Sort, min, max, Uid).ToList(), PageNumber, SNumber);
                    return Json(ssds);
                }
            }

            var result = Pagination.PagedResult(_iwonder.GetSSDPriceDependentOnBrand(Sort, min, max, Uid).ToList(), PNumber, SNumber);
            return Json(result);
        }

        [HttpGet]
        public JsonResult AscendingSSDProdoucts(int Id)
        {
            int Uid = HttpContext.Session.GetInt32("UserID").GetValueOrDefault();
            int PageSize = int.Parse(HttpContext.Session.GetString("PageSizess"));
            int PageNumber = int.Parse(HttpContext.Session.GetString("PageNumberss"));
            var max = HttpContext.Session.GetInt32("Maxss") ?? 0;
            var min = HttpContext.Session.GetInt32("Minss") ?? 0;
            if (PageNumber >= 2)
            {
                PageNumber = 1;
            }
            if (Id != 0)
            {
                HttpContext.Session.SetInt32("Sortss", Id);
                if (HttpContext.Session.GetString("Brandsss") != null)
                {
                    var brands = HttpContext.Session.GetString("Brandsss").Split(',');
                    if (brands.Length > 0 && brands[0] != "")
                    {
                        var result = Pagination.PagedResult(_iwonder.GetSSDProductsByBrand(brands, Id, min, max, Uid).ToList(), PageNumber, PageSize);
                        return Json(result);
                    }
                }
            }
            var Data = Pagination.PagedResult(_iwonder.GetSSDPriceDependentOnBrand(Id, min, max, Uid).ToList(), PageNumber, PageSize);
            return Json(Data);
        }

        [HttpGet]
        public JsonResult DefaultSSD(int PageSize = 3)
        {
            int Uid = HttpContext.Session.GetInt32("UserID").GetValueOrDefault();
            HttpContext.Session.SetString("PageSizess", PageSize.ToString());
            int PNumber = int.Parse(HttpContext.Session.GetString("PageNumberss"));
            int SNumber = int.Parse(HttpContext.Session.GetString("PageSizess"));
            var Sort = HttpContext.Session.GetInt32("Sortss") ?? 0;
            var max = HttpContext.Session.GetInt32("Maxss") ?? 0;
            var min = HttpContext.Session.GetInt32("Minss") ?? 0;
            if (PNumber >= 2)
            {
                PNumber = 1;
            }
            if (HttpContext.Session.GetString("Brandsss") != null)
            {
                var brands = HttpContext.Session.GetString("Brandsss").Split(',');
                if (brands.Length != 0 && brands[0] != "")
                {
                    var result = Pagination.PagedResult(_iwonder.GetSSDProductsByBrand(brands, Sort, min, max, Uid).ToList(), PNumber, SNumber);
                    //if (result.Data.Count() <= 0)
                    //{
                    //    result.CurrentPage = 1;
                    //}
                    return Json(result);

                }
            }

            var Data = Pagination.PagedResult(_iwonder.GetSSDPriceDependentOnBrand(Sort, min, max, Uid).ToList(), PNumber, PageSize);
            HttpContext.Session.SetString("PageNumberss", Data.CurrentPage.ToString());

            //if (Processors.Data.Count() == 0)
            //{
            //    Processors.CurrentPage = 1;
            //}
            return Json(Data);
        }

        [HttpPost]
        public JsonResult ProductsOfSSDBrand(string[] brand)
        {

            int Uid = HttpContext.Session.GetInt32("UserID").GetValueOrDefault();
            int PageSize = int.Parse(HttpContext.Session.GetString("PageSizess"));
            int PageNumber = int.Parse(HttpContext.Session.GetString("PageNumberss"));
            HttpContext.Session.SetString("Brandsss", string.Join(",", brand));
            var Sort = HttpContext.Session.GetInt32("Sortss") ?? 0;
            var brands = HttpContext.Session.GetString("Brandsss").Split(',');
            var max = HttpContext.Session.GetInt32("Maxss") ?? 0;
            var min = HttpContext.Session.GetInt32("Minss") ?? 0;
            if (PageNumber >= 2)
            {
                PageNumber = 1;
            }
            if (brands.Length <= 0 || brands[0] == "")
            {
                var Data = Pagination.PagedResult(_iwonder.GetSSDPriceDependentOnBrand(Sort, min, max, Uid).ToList(), PageNumber, PageSize);
                HttpContext.Session.SetString("PageNumberss", Data.CurrentPage.ToString());

                if (Data.Data.Count() <= 0)
                {
                    Data.CurrentPage = 1;
                    return Json("No");
                }
                return Json(Data);

            }
            else
            {
                var result = Pagination.PagedResult(_iwonder.GetSSDProductsByBrand(brands, Sort, min, max, Uid).ToList(), PageNumber, PageSize);
                HttpContext.Session.SetString("PageNumberss", result.CurrentPage.ToString());

                if (result.Data.Count() <= 0)
                {
                    result.CurrentPage = 1;
                    return Json("No");

                }
                return Json(result);


            }
        }
        [HttpGet]
        public JsonResult GetSSDPrice(int min, int max)
        {
            int Uid = HttpContext.Session.GetInt32("UserID").GetValueOrDefault();
            int PageSize = int.Parse(HttpContext.Session.GetString("PageSizess"));
            int PageNumber = int.Parse(HttpContext.Session.GetString("PageNumberss"));
            var IsNull = HttpContext.Session.GetString("Brandsss") ?? null;
            var Sort = HttpContext.Session.GetInt32("Sortss") ?? 0;
            if (PageNumber >= 2)
            {
                PageNumber = 1;
            }
            HttpContext.Session.SetInt32("Maxss", max);
            HttpContext.Session.SetInt32("Minss", min);
            if (IsNull == null)
            {

                var Data = Pagination.PagedResult(_iwonder.GetSSDPriceDependentOnBrand(Sort, min, max, Uid).ToList(), PageNumber, PageSize);
                HttpContext.Session.SetString("PageNumberss", Data.CurrentPage.ToString());

                if (Data.Data.Count() <= 0)
                {
                    Data.CurrentPage = 1;
                    return Json("No");
                }
                return Json(Data);

            }
            var brands = HttpContext.Session.GetString("Brandsss").Split(',');
            if (brands[0] == "")
            {
                var SSDDepenOn = Pagination.PagedResult(_iwonder.GetSSDPriceDependentOnBrand(Sort, min, max, Uid).ToList(), PageNumber, PageSize);
                HttpContext.Session.SetString("PageNumberss", SSDDepenOn.CurrentPage.ToString());

                if (SSDDepenOn.Data.Count() <= 0)
                {
                    SSDDepenOn.CurrentPage = 1;
                    return Json("No");
                }
                return Json(SSDDepenOn);
            }
            var result = Pagination.PagedResult(_iwonder.GetSSDProductsByBrand(brands, Sort, min, max, Uid).ToList(), PageNumber, PageSize);
            HttpContext.Session.SetString("PageNumberss", result.CurrentPage.ToString());

            if (result.Data.Count() <= 0)
            {
                result.CurrentPage = 1;
                return Json("No");
            }
            return Json(result);
        }
        #endregion

        #region Case

        [HttpGet]
        public IActionResult Case(int pageNumber = 1, int PageSize = 3)
        {
            int Uid = HttpContext.Session.GetInt32("UserID").GetValueOrDefault();
            HttpContext.Session.SetString("PageSizecase", PageSize.ToString());
            HttpContext.Session.SetString("PageNumbercase", pageNumber.ToString());
            var PNumber = int.Parse(HttpContext.Session.GetString("PageNumbercase")); // Session for PageNumber
            var SNumber = int.Parse(HttpContext.Session.GetString("PageSizecase")); // Session for PageSize 
            var Data = Pagination.PagedResult(_iwonder.GetAllCase(userid: Uid).ToList(), PNumber, SNumber);
            ViewBag.BrandNamesAndNumbers = _iwonder.GetCaseBrandNamesAndNumbers(userid: Uid); // Get All Brands
            ViewData["PageSizecase"] = PageSize;
            return View(Data);
        }
        [HttpGet]
        public JsonResult CaseAjax(int PageNumber)
        {

            int Uid = HttpContext.Session.GetInt32("UserID").GetValueOrDefault();
            HttpContext.Session.SetString("PageNumbercase", PageNumber.ToString());
            var SNumber = int.Parse(HttpContext.Session.GetString("PageSizecase"));
            var PNumber = int.Parse(HttpContext.Session.GetString("PageNumbercase"));
            var Sort = HttpContext.Session.GetInt32("Sortcase") ?? 0;
            var max = HttpContext.Session.GetInt32("Maxcase") ?? 0;
            var min = HttpContext.Session.GetInt32("Mincase") ?? 0;
            var brand = HttpContext.Session.GetString("Brandscase") ?? null;
            string[] brands;
            if (brand != null)
            {
                brands = brand.Split(',');
                if (brands.Length > 0 && brands[0] != "")
                {
                    var ssds = Pagination.PagedResult(_iwonder.GetCaseProductsByBrand(brands, Sort, min, max, Uid).ToList(), PageNumber, SNumber);
                    return Json(ssds);
                }
            }

            var result = Pagination.PagedResult(_iwonder.GetCasePriceDependentOnBrand(Sort, min, max, Uid).ToList(), PNumber, SNumber);
            return Json(result);
        }


        [HttpGet]
        public JsonResult AscendingCaseProdoucts(int Id)
        {
            int Uid = HttpContext.Session.GetInt32("UserID").GetValueOrDefault();
            int PageSize = int.Parse(HttpContext.Session.GetString("PageSizecase"));
            int PageNumber = int.Parse(HttpContext.Session.GetString("PageNumbercase"));
            var max = HttpContext.Session.GetInt32("Maxcase") ?? 0;
            var min = HttpContext.Session.GetInt32("Mincase") ?? 0;
            if (PageNumber >= 2)
            {
                PageNumber = 1;
            }
            if (Id != 0)
            {
                HttpContext.Session.SetInt32("Sortcase", Id);
                if (HttpContext.Session.GetString("Brandscase") != null)
                {
                    var brands = HttpContext.Session.GetString("Brandscase").Split(',');
                    if (brands.Length > 0 && brands[0] != "")
                    {
                        var result = Pagination.PagedResult(_iwonder.GetCaseProductsByBrand(brands, Id, min, max, Uid).ToList(), PageNumber, PageSize);
                        return Json(result);
                    }
                }
            }
            var Data = Pagination.PagedResult(_iwonder.GetCasePriceDependentOnBrand(Id, min, max, Uid).ToList(), PageNumber, PageSize);
            return Json(Data);
        }

        [HttpGet]
        public JsonResult DefaultCase(int PageSize = 3)
        {
            int Uid = HttpContext.Session.GetInt32("UserID").GetValueOrDefault();
            HttpContext.Session.SetString("PageSizecase", PageSize.ToString());
            int PNumber = int.Parse(HttpContext.Session.GetString("PageNumbercase"));
            int SNumber = int.Parse(HttpContext.Session.GetString("PageSizecase"));
            var Sort = HttpContext.Session.GetInt32("Sortcase") ?? 0;
            var max = HttpContext.Session.GetInt32("Maxcase") ?? 0;
            var min = HttpContext.Session.GetInt32("Mincase") ?? 0;
            if (PNumber >= 2)
            {
                PNumber = 1;
            }
            if (HttpContext.Session.GetString("Brandscase") != null)
            {
                var brands = HttpContext.Session.GetString("Brandscase").Split(',');
                if (brands.Length != 0 && brands[0] != "")
                {
                    var result = Pagination.PagedResult(_iwonder.GetCaseProductsByBrand(brands, Sort, min, max, Uid).ToList(), PNumber, SNumber);
                    //if (result.Data.Count() <= 0)
                    //{
                    //    result.CurrentPage = 1;
                    //}
                    return Json(result);

                }
            }

            var Data = Pagination.PagedResult(_iwonder.GetCasePriceDependentOnBrand(Sort, min, max, Uid).ToList(), PNumber, PageSize);
            HttpContext.Session.SetString("PageNumbercase", Data.CurrentPage.ToString());

            //if (Processors.Data.Count() == 0)
            //{
            //    Processors.CurrentPage = 1;
            //}
            return Json(Data);
        }

        [HttpPost]
        public JsonResult ProductsOfCaseBrand(string[] brand)
        {
            int Uid = HttpContext.Session.GetInt32("UserID").GetValueOrDefault();
            int PageSize = int.Parse(HttpContext.Session.GetString("PageSizecase"));
            int PageNumber = int.Parse(HttpContext.Session.GetString("PageNumbercase"));
            HttpContext.Session.SetString("Brandscase", string.Join(",", brand));
            var Sort = HttpContext.Session.GetInt32("Sortcase") ?? 0;
            var brands = HttpContext.Session.GetString("Brandscase").Split(',');
            var max = HttpContext.Session.GetInt32("Maxcase") ?? 0;
            var min = HttpContext.Session.GetInt32("Mincase") ?? 0;
            if (PageNumber >= 2)
            {
                PageNumber = 1;
            }
            if (brands.Length <= 0 || brands[0] == "")
            {
                var Data = Pagination.PagedResult(_iwonder.GetCasePriceDependentOnBrand(Sort, min, max, Uid).ToList(), PageNumber, PageSize);
                HttpContext.Session.SetString("PageNumbercase", Data.CurrentPage.ToString());

                if (Data.Data.Count() <= 0)
                {
                    Data.CurrentPage = 1;
                    return Json("No");
                }
                return Json(Data);

            }
            else
            {
                var result = Pagination.PagedResult(_iwonder.GetCaseProductsByBrand(brands, Sort, min, max, Uid).ToList(), PageNumber, PageSize);
                HttpContext.Session.SetString("PageNumbercase", result.CurrentPage.ToString());

                if (result.Data.Count() <= 0)
                {
                    result.CurrentPage = 1;
                    return Json("No");
                }
                return Json(result);


            }
        }
        [HttpGet]
        public JsonResult GetCasePrice(int min, int max)
        {
            int Uid = HttpContext.Session.GetInt32("UserID").GetValueOrDefault();
            int PageSize = int.Parse(HttpContext.Session.GetString("PageSizecase"));
            int PageNumber = int.Parse(HttpContext.Session.GetString("PageNumbercase"));
            var IsNull = HttpContext.Session.GetString("Brandscase") ?? null;
            var Sort = HttpContext.Session.GetInt32("Sortcase") ?? 0;
            if (PageNumber >= 2)
            {
                PageNumber = 1;
            }
            HttpContext.Session.SetInt32("Maxcase", max);
            HttpContext.Session.SetInt32("Mincase", min);
            if (IsNull == null)
            {

                var Data = Pagination.PagedResult(_iwonder.GetCasePriceDependentOnBrand(Sort, min, max, Uid).ToList(), PageNumber, PageSize);
                HttpContext.Session.SetString("PageNumbercase", Data.CurrentPage.ToString());

                if (Data.Data.Count() <= 0)
                {
                    Data.CurrentPage = 1;
                    return Json("No");
                }
                return Json(Data);

            }
            var brands = HttpContext.Session.GetString("Brandscase").Split(',');
            if (brands[0] == "")
            {
                var SSDDepenOn = Pagination.PagedResult(_iwonder.GetCasePriceDependentOnBrand(Sort, min, max, Uid).ToList(), PageNumber, PageSize);
                HttpContext.Session.SetString("PageNumbercase", SSDDepenOn.CurrentPage.ToString());

                if (SSDDepenOn.Data.Count() <= 0)
                {
                    SSDDepenOn.CurrentPage = 1;
                    return Json("No");
                }
                return Json(SSDDepenOn);
            }
            var result = Pagination.PagedResult(_iwonder.GetCaseProductsByBrand(brands, Sort, min, max, Uid).ToList(), PageNumber, PageSize);
            HttpContext.Session.SetString("PageNumbercase", result.CurrentPage.ToString());

            if (result.Data.Count() <= 0)
            {
                result.CurrentPage = 1;
                return Json("No");
            }
            return Json(result);
        }
        #endregion Case

        #region PowerSuply

        [HttpGet]
        public IActionResult PowerSuply(int pageNumber = 1, int PageSize = 3)
        {
            int Uid = HttpContext.Session.GetInt32("UserID").GetValueOrDefault();
            HttpContext.Session.SetString("PageSizeps", PageSize.ToString());
            HttpContext.Session.SetString("PageNumberps", pageNumber.ToString());
            var PNumber = int.Parse(HttpContext.Session.GetString("PageNumberps")); 
            var SNumber = int.Parse(HttpContext.Session.GetString("PageSizeps")); 
            var Data = Pagination.PagedResult(_iwonder.GetAllPowerSuply(userid: Uid).ToList(), PNumber, SNumber);
            ViewBag.BrandNamesAndNumbers = _iwonder.GetPowerSuplyBrandNamesAndNumbers(userid: Uid); 
            ViewData["PageSizeps"] = PageSize;
            return View(Data);
        }


        public JsonResult PowerSuplyAjax(int PageNumber)
        {

            int Uid = HttpContext.Session.GetInt32("UserID").GetValueOrDefault();
            HttpContext.Session.SetString("PageNumberps", PageNumber.ToString());
            var SNumber = int.Parse(HttpContext.Session.GetString("PageSizeps"));
            var PNumber = int.Parse(HttpContext.Session.GetString("PageNumberps"));
            var Sort = HttpContext.Session.GetInt32("Sortps") ?? 0;
            var max = HttpContext.Session.GetInt32("Maxps") ?? 0;
            var min = HttpContext.Session.GetInt32("Minps") ?? 0;
            var brand = HttpContext.Session.GetString("Brandsps") ?? null;
            string[] brands;
            if (brand != null)
            {
                brands = brand.Split(',');
                if (brands.Length > 0 && brands[0] != "")
                {
                    var PSS = Pagination.PagedResult(_iwonder.GetPowerSuplyProductsByBrand(brands, Sort, min, max, Uid).ToList(), PageNumber, SNumber);
                    return Json(PSS);
                }
            }

            var result = Pagination.PagedResult(_iwonder.GetPowerSuplyPriceDependentOnBrand(Sort, min, max, Uid).ToList(), PNumber, SNumber);
            return Json(result);
        }

        [HttpGet]
        public JsonResult AscendingPowerSuplyProdoucts(int Id)
        {
            int Uid = HttpContext.Session.GetInt32("UserID").GetValueOrDefault();
            int PageSize = int.Parse(HttpContext.Session.GetString("PageSizeps"));
            int PageNumber = int.Parse(HttpContext.Session.GetString("PageNumberps"));
            var max = HttpContext.Session.GetInt32("Maxps") ?? 0;
            var min = HttpContext.Session.GetInt32("Minps") ?? 0;
            if (PageNumber >= 2)
            {
                PageNumber = 1;
            }
            if (Id != 0)
            {
                HttpContext.Session.SetInt32("Sortps", Id);
                if (HttpContext.Session.GetString("Brandsps") != null)
                {
                    var brands = HttpContext.Session.GetString("Brandsps").Split(',');
                    if (brands.Length > 0 && brands[0] != "")
                    {
                        var result = Pagination.PagedResult(_iwonder.GetPowerSuplyProductsByBrand(brands, Id, min, max, Uid).ToList(), PageNumber, PageSize);
                        return Json(result);
                    }
                }
            }
            var Data = Pagination.PagedResult(_iwonder.GetPowerSuplyPriceDependentOnBrand(Id, min, max, Uid).ToList(), PageNumber, PageSize);
            return Json(Data);
        }

        [HttpGet]
        public JsonResult DefaultPowerSuply(int PageSize = 3)
        {
            int Uid = HttpContext.Session.GetInt32("UserID").GetValueOrDefault();
            HttpContext.Session.SetString("PageSizeps", PageSize.ToString());
            int PNumber = int.Parse(HttpContext.Session.GetString("PageNumberps"));
            int SNumber = int.Parse(HttpContext.Session.GetString("PageSizeps"));
            var Sort = HttpContext.Session.GetInt32("Sortps") ?? 0;
            var max = HttpContext.Session.GetInt32("Maxps") ?? 0;
            var min = HttpContext.Session.GetInt32("Minps") ?? 0;
            if (PNumber >= 2)
            {
                PNumber = 1;
            }
            if (HttpContext.Session.GetString("Brandsps") != null)
            {
                var brands = HttpContext.Session.GetString("Brandsps").Split(',');
                if (brands.Length != 0 && brands[0] != "")
                {
                    var result = Pagination.PagedResult(_iwonder.GetPowerSuplyProductsByBrand(brands, Sort, min, max, Uid).ToList(), PNumber, SNumber);
                    //if (result.Data.Count() <= 0)
                    //{
                    //    result.CurrentPage = 1;
                    //}
                    return Json(result);

                }
            }

            var Data = Pagination.PagedResult(_iwonder.GetPowerSuplyPriceDependentOnBrand(Sort, min, max, Uid).ToList(), PNumber, PageSize);
            HttpContext.Session.SetString("PageNumberps", Data.CurrentPage.ToString());

            //if (Processors.Data.Count() == 0)
            //{
            //    Processors.CurrentPage = 1;
            //}
            return Json(Data);
        }

        [HttpPost]
        public JsonResult ProductsOfPowerSuplyBrand(string[] brand)
        {
            int Uid = HttpContext.Session.GetInt32("UserID").GetValueOrDefault();
            int PageSize = int.Parse(HttpContext.Session.GetString("PageSizeps"));
            int PageNumber = int.Parse(HttpContext.Session.GetString("PageNumberps"));
            HttpContext.Session.SetString("Brandsps", string.Join(",", brand));
            var Sort = HttpContext.Session.GetInt32("Sortps") ?? 0;
            var brands = HttpContext.Session.GetString("Brandsps").Split(',');
            var max = HttpContext.Session.GetInt32("Maxps") ?? 0;
            var min = HttpContext.Session.GetInt32("Minps") ?? 0;
            if (PageNumber >= 2)
            {
                PageNumber = 1;
            }
            if (brands.Length <= 0 || brands[0] == "")
            {
                var Data = Pagination.PagedResult(_iwonder.GetPowerSuplyPriceDependentOnBrand(Sort, min, max, Uid).ToList(), PageNumber, PageSize);
                HttpContext.Session.SetString("PageNumberps", Data.CurrentPage.ToString());

                if (Data.Data.Count() <= 0)
                {
                    Data.CurrentPage = 1;
                    return Json("No");
                }
                return Json(Data);

            }
            else
            {
                var result = Pagination.PagedResult(_iwonder.GetPowerSuplyProductsByBrand(brands, Sort, min, max, Uid).ToList(), PageNumber, PageSize);
                HttpContext.Session.SetString("PageNumberps", result.CurrentPage.ToString());

                if (result.Data.Count() <= 0)
                {
                    result.CurrentPage = 1;
                    return Json("No");
                }
                return Json(result);


            }
        }

        [HttpGet]
        public JsonResult GetPowerSuplyPrice(int min, int max)
        {
            int Uid = HttpContext.Session.GetInt32("UserID").GetValueOrDefault();
            int PageSize = int.Parse(HttpContext.Session.GetString("PageSizeps"));
            int PageNumber = int.Parse(HttpContext.Session.GetString("PageNumberps"));
            var IsNull = HttpContext.Session.GetString("Brandsps") ?? null;
            var Sort = HttpContext.Session.GetInt32("Sortps") ?? 0;
            if (PageNumber >= 2)
            {
                PageNumber = 1;
            }
            HttpContext.Session.SetInt32("Maxps", max);
            HttpContext.Session.SetInt32("Minps", min);
            if (IsNull == null)
            {

                var Data = Pagination.PagedResult(_iwonder.GetPowerSuplyPriceDependentOnBrand(Sort, min, max, Uid).ToList(), PageNumber, PageSize);
                HttpContext.Session.SetString("PageNumberps", Data.CurrentPage.ToString());

                if (Data.Data.Count() <= 0)
                {
                    Data.CurrentPage = 1;
                    return Json("No");
                }
                return Json(Data);

            }
            var brands = HttpContext.Session.GetString("Brandsps").Split(',');
            if (brands[0] == "")
            {
                var PsDepenOn = Pagination.PagedResult(_iwonder.GetPowerSuplyPriceDependentOnBrand(Sort, min, max, Uid).ToList(), PageNumber, PageSize);
                HttpContext.Session.SetString("PageNumberps", PsDepenOn.CurrentPage.ToString());

                if (PsDepenOn.Data.Count() <= 0)
                {
                    PsDepenOn.CurrentPage = 1;
                    return Json("No");
                }
                return Json(PsDepenOn);
            }
            var result = Pagination.PagedResult(_iwonder.GetPowerSuplyProductsByBrand(brands, Sort, min, max, Uid).ToList(), PageNumber, PageSize);
            HttpContext.Session.SetString("PageNumbersps", result.CurrentPage.ToString());

            if (result.Data.Count() <= 0)
            {
                result.CurrentPage = 1;
                return Json("No");
            }
            return Json(result);
        }
        #endregion

        #region Cart
        public IActionResult Cart()
        {
            return View();
        }
        public IActionResult GetAvailableProducts()
        {
            Dictionary<string, WishListVM> list = new Dictionary<string, WishListVM>();
            var Casesobj = _wonder.Cases;
            foreach (var item in Casesobj)
            {
                WishListVM obj = new WishListVM();
                obj.ProductCode = item.CaseCode;
                obj.ProductName = item.CaseName;
                obj.Quantity = item.CaseQuantity;
                obj.ProductPrice = item.CasePrice;
                obj.IsAvailable = (bool)item.IsAvailable ? "Available" : "Not Available";
                obj.Image = _wonder.Images.Where(x => x.CaseCode == obj.ProductCode).Select(x => x.ProductImage).FirstOrDefault();
                list.Add(item.CaseCode, obj);
            }
            var Vgasobj = _wonder.GraphicsCards;
            foreach (var item in Vgasobj)
            {
                WishListVM obj = new WishListVM();
                obj.ProductCode = item.Vgacode;
                obj.ProductName = item.Vganame;
                obj.Quantity = item.Vgaquantity;
                obj.ProductPrice = item.Vgaprice;
                obj.IsAvailable = (bool)item.IsAvailable ? "Available" : "Not Available";
                obj.Image = _wonder.Images.Where(x => x.Vgacode == obj.ProductCode).Select(x => x.ProductImage).FirstOrDefault();
                list.Add(item.Vgacode, obj);
            }
            var HDDsobj = _wonder.Hdds;
            foreach (var item in HDDsobj)
            {
                WishListVM obj = new WishListVM();
                obj.ProductCode = item.Hddcode;
                obj.ProductName = item.Hddname;
                obj.Quantity = item.Hddquantity;
                obj.ProductPrice = item.Hddprice;
                obj.IsAvailable = (bool)item.IsAvailable ? "Available" : "Not Available";
                obj.Image = _wonder.Images.Where(x => x.Hddcode == obj.ProductCode).Select(x => x.ProductImage).FirstOrDefault();
                list.Add(item.Hddcode, obj);
            }
            var MBsobj = _wonder.Motherboards;
            foreach (var item in MBsobj)
            {
                WishListVM obj = new WishListVM();
                obj.ProductCode = item.MotherCode;
                obj.ProductName = item.MotherName;
                obj.Quantity = item.MotherQuantity;
                obj.ProductPrice = item.MotherPrice;
                obj.IsAvailable = (bool)item.IsAvailable ? "Available" : "Not Available";
                obj.Image = _wonder.Images.Where(x => x.MotherCode == obj.ProductCode).Select(x => x.ProductImage).FirstOrDefault();
                list.Add(item.MotherCode, obj);
            }
            var PSUsobj = _wonder.PowerSupplies;
            foreach (var item in PSUsobj)
            {
                WishListVM obj = new WishListVM();
                obj.ProductCode = item.Psucode;
                obj.ProductName = item.Psuname;
                obj.Quantity = item.Psuquantity;
                obj.ProductPrice = item.Psuprice;
                obj.IsAvailable = (bool)item.IsAvailable ? "Available" : "Not Available";
                obj.Image = _wonder.Images.Where(x => x.Psucode == obj.ProductCode).Select(x => x.ProductImage).FirstOrDefault();
                list.Add(item.Psucode, obj);
            }
            var Prosobj = _wonder.Processors;
            foreach (var item in Prosobj)
            {
                WishListVM obj = new WishListVM();
                obj.ProductCode = item.ProCode;
                obj.ProductName = item.ProName;
                obj.Quantity = item.ProQuantity;
                obj.ProductPrice = item.ProPrice;
                obj.IsAvailable = (bool)item.IsAvailable ? "Available" : "Not Available";
                obj.Image = _wonder.Images.Where(x => x.ProCode == obj.ProductCode).Select(x => x.ProductImage).FirstOrDefault();
                list.Add(item.ProCode, obj);
            }
            var RAMsobj = _wonder.Rams;
            foreach (var item in RAMsobj)
            {
                WishListVM obj = new WishListVM();
                obj.ProductCode = item.RamCode;
                obj.ProductName = item.RamName;
                obj.Quantity = item.RamQuantity;
                obj.ProductPrice = item.RamPrice;
                obj.IsAvailable = (bool)item.IsAvailable ? "Available" : "Not Available";
                obj.Image = _wonder.Images.Where(x => x.RamCode == obj.ProductCode).Select(x => x.ProductImage).FirstOrDefault();
                list.Add(item.RamCode, obj);
            }
            var SSDsobj = _wonder.Ssds.Where(x => x.IsAvailable == true);
            foreach (var item in SSDsobj)
            {
                WishListVM obj = new WishListVM();
                obj.ProductCode = item.Ssdcode;
                obj.ProductName = item.Ssdname;
                obj.Quantity = item.Ssdquantity;
                obj.ProductPrice = item.Ssdprice;
                obj.IsAvailable = (bool)item.IsAvailable ? "Available" : "Not Available";
                obj.Image = _wonder.Images.Where(x => x.Ssdcode == obj.ProductCode).Select(x => x.ProductImage).FirstOrDefault();
                list.Add(item.Ssdcode, obj);
            }
            return Json(list);
        }

        #endregion

        #region WishList
        public IActionResult WishList()
        {
            var userid = HttpContext.Session.GetInt32("UserID").GetValueOrDefault();
            if (userid != 0)
            {
                return View("WishList", _iwonder.GetWishList(userid));
            }
            else
            {
                return RedirectToAction("Login_Register", new { Wishlist = "WishList" });
            }
        }
        public ActionResult WishListCounter()
        {
            var userid = HttpContext.Session.GetInt32("UserID").GetValueOrDefault();
            var wishlistCounter = _iwonder.GetWishList(userid).Where(x => x.IsAvailable != "Not Available").Count();

            return Json(wishlistCounter);
        }
        public IActionResult AddToWL(string ProductCode)
        {
            var userid = HttpContext.Session.GetInt32("UserID").GetValueOrDefault();
            if (userid != 0)
            {
                return Json(_iwonder.AddToWL(ProductCode, userid));
            }
            else
            {
                return Json("LoginRegisterPopup");

            }
        }
        public IActionResult CheckWishList(string ProductCode)
        {
            var userid = HttpContext.Session.GetInt32("UserID").GetValueOrDefault();
            if (userid != 0)
            {
                return Json(_iwonder.CheckfromWL(ProductCode, userid));
            }
            else
            {
                return Json("no user");
            }
        }
        public IActionResult DeleteFromWL(string ProductCode)
        {
            var userid = HttpContext.Session.GetInt32("UserID").GetValueOrDefault();
            return Json(_iwonder.DeletefromWL(ProductCode, userid));
        }
        #endregion

        #region Checkout
        [HttpGet]
        public IActionResult CheckOut()
        {
            var userid = HttpContext.Session.GetInt32("UserID");
            ViewBag.UserPhone = _wonder.Users.Where(x => x.UserId == userid).Select(x => x.Phone).FirstOrDefault();
            ViewBag.UserAddress = _wonder.Sales.Where(x => x.UserId == userid).OrderByDescending(x => x.SalesId).Select(x => x.Address).FirstOrDefault();
            return View();
        }

        public IActionResult CheckAccount(UserVM UserData)
        {
            var userid = _wonder.Users.Where(x => x.Phone == UserData.Telephone && x.IsAdmin == false).Select(x => x.UserId).FirstOrDefault();
            var pass = _wonder.Users.Where(x => x.Password == UserData.Password && x.Phone == UserData.Telephone && x.IsAdmin == false).Select(x => x.UserId).FirstOrDefault();
            if (userid == 0)
            {
                return Json("failed phone");
            }
            else if (pass == 0)
            {
                return Json("failed password");
            }
            else if (userid != 0 && pass != 0)
            {
                //اعمل ليست ابعت فيها الاسم والتليفون و الأدرس 
                IDictionary<string, string> userInfo = new Dictionary<string, string>();
                var address = _wonder.Sales.Where(x => x.UserId == userid).OrderByDescending(x => x.SalesId).Select(x => x.Address).FirstOrDefault();
                if (address == null)
                {
                    var userName = _wonder.Users.Where(x => x.Phone == UserData.Telephone && x.IsAdmin == false).Select(x => x.FirstName).FirstOrDefault() + " " + _wonder.Users.Where(x => x.Phone == UserData.Telephone && x.IsAdmin == false).Select(x => x.LastName).FirstOrDefault();
                    userInfo.Add("Name", userName);
                    userInfo.Add("Phone", Convert.ToString(_wonder.Users.Where(x => x.Phone == UserData.Telephone && x.IsAdmin == false).Select(x => x.Phone).FirstOrDefault()));
                    userInfo.Add("Id", Convert.ToString(userid));
                    HttpContext.Session.SetInt32("UserID", userid);
                    HttpContext.Session.SetString("UserName", userName);
                    return Json(userInfo);
                }
                else
                {
                    var userName = _wonder.Users.Where(x => x.Phone == UserData.Telephone && x.IsAdmin == false).Select(x => x.FirstName).FirstOrDefault() + " " + _wonder.Users.Where(x => x.Phone == UserData.Telephone && x.IsAdmin == false).Select(x => x.LastName).FirstOrDefault();
                    userInfo.Add("Address", address);
                    userInfo.Add("Name", userName);
                    userInfo.Add("Phone", Convert.ToString(_wonder.Users.Where(x => x.Phone == UserData.Telephone && x.IsAdmin == false).Select(x => x.Phone).FirstOrDefault()));
                    userInfo.Add("Id", Convert.ToString(userid));
                    HttpContext.Session.SetInt32("UserID", userid);
                    HttpContext.Session.SetString("UserName", userName);
                    return Json(userInfo);
                }

            }
            else
            {
                //Phone or password isn't correct or both !!
                return Json("failed phone&pass");
            }
        }
        [HttpPost]
        public JsonResult CheckOut(UserVM UserData, SalesVM[] OrderData)
        {
            string check= "";
            if (UserData.FName == null && UserData.LName == null && UserData.ID == 0 && UserData.IsAdmin == false && UserData.LatestBuyTime == null && UserData.NumberOfTimes == 0 && UserData.Password == null && UserData.Telephone == 0)
            {
                //already signed in (add address for the first time / update address or not)
                int userid = (int)HttpContext.Session.GetInt32("UserID");
                check = _iwonder.CheckOrder(OrderData,userid);
            }
            else
            {
                if (UserData.FName != null)
                {
                    //create Acc + checkout
                    check = _iwonder.CheckOrderCreateAcc(UserData, OrderData);
                }
                //else
                //{
                //    //xxxxxx
                //    //sign in + checkout
                //    check = _iwonder.CheckOrderSignIn(UserData, OrderData);
                //}
                if (check == "success" || check == "success checked new address" || check == "success checked old address")
                {
                    var userid = _wonder.Users.Where(x => x.Phone == UserData.Telephone).Select(x => x.UserId).FirstOrDefault();
                    var name = _wonder.Users.Where(x => x.UserId == userid).Select(x => new { x.FirstName, x.LastName }).FirstOrDefault();
                    HttpContext.Session.SetInt32("UserID", userid);
                    HttpContext.Session.SetString("UserName", name.FirstName + " " + name.LastName);
                }

            }

            return Json(check);
        }
        #endregion

        #region Login & Logout & Register
        public ActionResult Login_Register(string Wishlist)
        {
            ViewBag.Wishlist = Wishlist;
            return View();
        }
        public ActionResult LogOut(int? UserID)
        {
            HttpContext.Session.Remove("UserID");
            HttpContext.Session.Remove("UserName");
            return RedirectToAction("Index");
        }

        public ActionResult Login(UserVM user, string WishList)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            if (_wonder.Users.Where(x => x.Phone == user.Telephone && x.IsAdmin == false).FirstOrDefault() != null)
            {
                if (_wonder.Users.Where(x => x.Phone == user.Telephone && x.Password == user.Password && x.IsAdmin == false).FirstOrDefault() != null)
                {
                    var id = _wonder.Users.Where(x => x.Phone == user.Telephone && x.IsAdmin == false).Select(x => x.UserId).FirstOrDefault();
                    var name = _wonder.Users.Where(x => x.UserId == id).Select(x => new { x.FirstName, x.LastName }).FirstOrDefault();
                    HttpContext.Session.SetInt32("UserID", id);
                    HttpContext.Session.SetString("UserName", name.FirstName + " " + name.LastName);
                    //Session.Timeout = 15;
                    data.Add("page", WishList);
                    data.Add("Id", id.ToString());
                    data.Add("Name", name.FirstName + " " + name.LastName);
                }
                else
                {
                    data.Add("page", "wrong password");
                }
            }
            else
                data.Add("page", "this phone isn't exist");

            return Json(data);
        }

        public ActionResult Register(UserVM user, string WishList)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            if (_wonder.Users.Select(x => x.Phone).Contains(user.Telephone))
            {
                data.Add("page", "this phone is already exist");
            }
            else
            {
                User Uobj = new User();
                Uobj.FirstName = user.FName;
                Uobj.LastName = user.LName;
                Uobj.Phone = user.Telephone;
                Uobj.Password = user.Password;
                _wonder.Users.Add(Uobj);
                _wonder.SaveChanges();
                var id = _wonder.Users.Where(x => x.Phone == user.Telephone).Select(x => x.UserId).FirstOrDefault();
                var name = _wonder.Users.Where(x => x.UserId == id).Select(x => new { x.FirstName, x.LastName }).FirstOrDefault();
                HttpContext.Session.SetInt32("UserID", id);
                HttpContext.Session.SetString("UserName", name.FirstName + " " + name.LastName);
                data.Add("Id", id.ToString());
                data.Add("page", WishList);
                data.Add("Name", name.ToString());
            }
            return Json(data);
        }
        #endregion

        #region ProductDetails

        public IActionResult CaseDetails(string code, int currentPageIndex, int NextOrPreviousPage)
        {
            int Uid = HttpContext.Session.GetInt32("UserID").GetValueOrDefault();
            ViewBag.Case = _iwonder.GetAllCase(Uid).Where(x => x.CaseCode != code).Take(4);
            if (currentPageIndex == 0 && NextOrPreviousPage == 0)
            {
                return View(_iwonder.CaseDetails(code));
            }
            else if (currentPageIndex == 0)
            {
                return Json(_iwonder.CaseDetails(code, NextOrPreviousPage));
            }
            else
            {
                return Json(_iwonder.CaseDetails(code, currentPageIndex));
            }
        }

        public IActionResult GraphicsCardDetails(string code, int currentPageIndex, int NextOrPreviousPage)
        {
            int Uid = HttpContext.Session.GetInt32("UserID").GetValueOrDefault();
            ViewBag.GraphicsCard = _iwonder.GetAllCard(Uid).Where(x => x.Vgacode != code).Take(4);
            if (currentPageIndex == 0 && NextOrPreviousPage == 0)
            {
                return View(_iwonder.GraphicsCardDetails(code));
            }
            else if (currentPageIndex == 0)
            {
                return Json(_iwonder.GraphicsCardDetails(code, NextOrPreviousPage));
            }
            else
            {
                return Json(_iwonder.GraphicsCardDetails(code, currentPageIndex));
            }
        }

        public IActionResult HddDetails(string code, int currentPageIndex, int NextOrPreviousPage)
        {
            int Uid = HttpContext.Session.GetInt32("UserID").GetValueOrDefault();
            ViewBag.Hdd = _iwonder.GetAllHDD(Uid).Where(x => x.Hddcode != code).Take(4);
            if (currentPageIndex == 0 && NextOrPreviousPage == 0)
            {
                return View(_iwonder.HddDetails(code));
            }
            else if (currentPageIndex == 0)
            {
                return Json(_iwonder.HddDetails(code, NextOrPreviousPage));
            }
            else
            {
                return Json(_iwonder.HddDetails(code, currentPageIndex));
            }
        }

        public IActionResult MotherboardDetails(string code, int currentPageIndex, int NextOrPreviousPage)
        {
            int Uid = HttpContext.Session.GetInt32("UserID").GetValueOrDefault();
            ViewBag.Motherboard = _iwonder.GetAllMotherboard(Uid).Where(x => x.MotherCode != code).Take(4);
            if (currentPageIndex == 0 && NextOrPreviousPage == 0)
            {
                return View(_iwonder.MotherboardDetails(code));
            }
            else if (currentPageIndex == 0)
            {
                return Json(_iwonder.MotherboardDetails(code, NextOrPreviousPage));
            }
            else
            {
                return Json(_iwonder.MotherboardDetails(code, currentPageIndex));
            }
        }

        public IActionResult PowerSupplyDetails(string code, int currentPageIndex, int NextOrPreviousPage)
        {
            int Uid = HttpContext.Session.GetInt32("UserID").GetValueOrDefault();
            ViewBag.PowerSupply = _iwonder.GetAllPowerSuply(Uid).Where(x => x.Psucode != code).Take(4);
            if (currentPageIndex == 0 && NextOrPreviousPage == 0)
            {
                return View(_iwonder.PowerSupplyDetails(code));
            }
            else if (currentPageIndex == 0)
            {
                return Json(_iwonder.PowerSupplyDetails(code, NextOrPreviousPage));
            }
            else
            {
                return Json(_iwonder.PowerSupplyDetails(code, currentPageIndex));
            }
        }

        public IActionResult ProcessorDetails(string code, int currentPageIndex, int NextOrPreviousPage)
        {
            int Uid = HttpContext.Session.GetInt32("UserID").GetValueOrDefault();
            ViewBag.Processor = _iwonder.GetAllProcessors(Uid).Where(x => x.ProCode != code).Take(4);
            if (currentPageIndex == 0 && NextOrPreviousPage == 0)
            {
                return View(_iwonder.ProcessorDetails(code));
            }
            else if (currentPageIndex == 0)
            {
                return Json(_iwonder.ProcessorDetails(code, NextOrPreviousPage));
            }
            else
            {
                return Json(_iwonder.ProcessorDetails(code, currentPageIndex));
            }
        }

        public IActionResult RamDetails(string code, int currentPageIndex, int NextOrPreviousPage)
        {
            int Uid = HttpContext.Session.GetInt32("UserID").GetValueOrDefault();
            ViewBag.Ram = _iwonder.GetAllRAM(Uid).Where(x => x.RamCode != code).Take(4);
            if (currentPageIndex == 0 && NextOrPreviousPage == 0)
            {
                return View(_iwonder.RamDetails(code));
            }
            else if (currentPageIndex == 0)
            {
                return Json(_iwonder.RamDetails(code, NextOrPreviousPage));
            }
            else
            {
                return Json(_iwonder.RamDetails(code, currentPageIndex));
            }
        }

        public IActionResult SsdDetails(string code, int currentPageIndex, int NextOrPreviousPage)
        {
            int Uid = HttpContext.Session.GetInt32("UserID").GetValueOrDefault();
            ViewBag.Ssd = _iwonder.GetAllSSD(Uid).Where(x => x.Ssdcode != code).Take(4);
            if (currentPageIndex == 0 && NextOrPreviousPage == 0)
            {
                return View(_iwonder.SsdDetails(code));
            }
            else if (currentPageIndex == 0)
            {
                return Json(_iwonder.SsdDetails(code, NextOrPreviousPage));
            }
            else
            {
                return Json(_iwonder.SsdDetails(code, currentPageIndex));
            }
        }

        #endregion

        public IActionResult CategoryInfo(string categoryName)
        {
            ViewBag.categoryname = categoryName;
            return View();
        }

        #region Search

        public IActionResult SearchPage(string src, int num)
        {
            ViewBag.searchWord = src;

            return View(_iwonder.SearchFunction(src, num));
        }
        public IActionResult Search(string src, int num, string txt)
        {
            return Json(_iwonder.SearchFunction(src, num));
        }

        #endregion

        public IActionResult Review(ReviewVM review)
        {
            ReviewVM result = _iwonder.AddReview(review);
            /////////////////////////////////
            return Json(result);
        }

        #region chat
        public IActionResult Chat()
        {
            var userid = HttpContext.Session.GetInt32("UserID").GetValueOrDefault();
            if (userid != 0)
            {
                var messages = _iwonder.GetAllMessages(userid);
                ViewBag.userid = userid;
                return View(messages);
            }
            else
            {
                return RedirectToAction("Login_Register", new { Wishlist = "Chat" });
            }

        }
        public ActionResult GetMessagesCounter(int userid)
        {
            int counter = _wonder.Messages.Where(x => x.Seen == false && x.AdminOrNot == true && x.UserId == userid).Select(x => x.UserId).Count();
            return Json(counter);
        }
        public ActionResult SeeMessages(int userid)
        {
            List<Message> NotSeenRows = new List<Message>();

            NotSeenRows = _wonder.Messages.Where(x => x.UserId == userid && x.AdminOrNot == true && x.Seen == false).ToList();

            foreach (var item in NotSeenRows)
            {
                item.Seen = true;
                _wonder.Messages.Update(item);
            }
            _wonder.SaveChanges();
            return Json("seen");
        }

        #endregion

    }
}