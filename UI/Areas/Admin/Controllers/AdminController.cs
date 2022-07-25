using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using BLL.ViewModel;
using DataModel.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace UI.Controllers
{
    public class AdminController : Controller
    {
        readonly IWonder _iwonder;
        private IWebHostEnvironment _hostingEnv;

        public AdminController(IWonder iwonder, WonderHardwareContext wonder, IWebHostEnvironment hostingEnv)
        {
            _iwonder = iwonder;
            _wonder = wonder;
            _hostingEnv = hostingEnv;
        }
        readonly WonderHardwareContext _wonder;



        public IActionResult Index()
        {
            if ((HttpContext.Session.GetInt32("AdminID").GetValueOrDefault()) == 0)
            {
                return RedirectToAction("Login");
            }

            #region counter
            Dictionary<string, int> Counter = new Dictionary<string, int>();

            Counter.Add("UsersCount", _iwonder.GetUsersData().Count());
            Counter.Add("SalesCount", (int)_wonder.Sales.Sum(x => x.ProductQuantity));
            Counter.Add("Revenue", (int)_wonder.Sales.Sum(x => x.TotalPrice));
            Counter.Add("ProductsCounts", _iwonder.GetAllProcessors().Count() + _iwonder.GetAllCard().Count() + _iwonder.GetAllCase().Count() + _iwonder.GetAllHDD().Count() + _iwonder.GetAllMotherboard().Count() + _iwonder.GetAllPowerSuply().Count() + _iwonder.GetAllRAM().Count() + _iwonder.GetAllSSD().Count());

            ViewBag.Counter = Counter;
            #endregion

            #region big chart

            Dictionary<string, List<int>> productsEarningsMonthly = new Dictionary<string, List<int>>();

            List<int> earningsMonthly1 = new List<int>();
            List<int> earningsMonthly2 = new List<int>();
            List<int> earningsMonthly3 = new List<int>();
            List<int> earningsMonthly4 = new List<int>();
            List<int> earningsMonthly5 = new List<int>();
            List<int> earningsMonthly6 = new List<int>();
            List<int> earningsMonthly7 = new List<int>();
            List<int> earningsMonthly8 = new List<int>();

            for (int i = 1; i <= 12; i++)
            {
                earningsMonthly1.Add((int)_wonder.Sales.Where(x => x.ProCode != null && x.DateAndTime.Value.Year == DateTime.Now.Year && x.DateAndTime.Value.Month == i).Sum(x => x.TotalPrice));
            }
            productsEarningsMonthly.Add("Pro", earningsMonthly1);

            for (int i = 1; i <= 12; i++)
            {
                earningsMonthly2.Add((int)_wonder.Sales.Where(x => x.CaseCode != null && x.DateAndTime.Value.Year == DateTime.Now.Year && x.DateAndTime.Value.Month == i).Sum(x => x.TotalPrice));
            }
            productsEarningsMonthly.Add("Case", earningsMonthly2);

            for (int i = 1; i <= 12; i++)
            {
                earningsMonthly3.Add((int)_wonder.Sales.Where(x => x.Vgacode != null && x.DateAndTime.Value.Year == DateTime.Now.Year && x.DateAndTime.Value.Month == i).Sum(x => x.TotalPrice));
            }
            productsEarningsMonthly.Add("VGA", earningsMonthly3);

            for (int i = 1; i <= 12; i++)
            {
                earningsMonthly4.Add((int)_wonder.Sales.Where(x => x.Hddcode != null && x.DateAndTime.Value.Year == DateTime.Now.Year && x.DateAndTime.Value.Month == i).Sum(x => x.TotalPrice));
            }
            productsEarningsMonthly.Add("HDD", earningsMonthly4);

            for (int i = 1; i <= 12; i++)
            {
                earningsMonthly5.Add((int)_wonder.Sales.Where(x => x.MotherCode != null && x.DateAndTime.Value.Year == DateTime.Now.Year && x.DateAndTime.Value.Month == i).Sum(x => x.TotalPrice));
            }
            productsEarningsMonthly.Add("MB", earningsMonthly5);

            for (int i = 1; i <= 12; i++)
            {
                earningsMonthly6.Add((int)_wonder.Sales.Where(x => x.Psucode != null && x.DateAndTime.Value.Year == DateTime.Now.Year && x.DateAndTime.Value.Month == i).Sum(x => x.TotalPrice));
            }
            productsEarningsMonthly.Add("PSu", earningsMonthly6);

            for (int i = 1; i <= 12; i++)
            {
                earningsMonthly7.Add((int)_wonder.Sales.Where(x => x.RamCode != null && x.DateAndTime.Value.Year == DateTime.Now.Year && x.DateAndTime.Value.Month == i).Sum(x => x.TotalPrice));
            }
            productsEarningsMonthly.Add("RAM", earningsMonthly7);

            for (int i = 1; i <= 12; i++)
            {
                earningsMonthly8.Add((int)_wonder.Sales.Where(x => x.Ssdcode != null && x.DateAndTime.Value.Year == DateTime.Now.Year && x.DateAndTime.Value.Month == i).Sum(x => x.TotalPrice));
            }
            productsEarningsMonthly.Add("SSD", earningsMonthly8);


            ViewBag.productsEarningsMonthly = productsEarningsMonthly;


            #endregion

            #region progress bar
            Dictionary<string, int> SalesSum = new Dictionary<string, int>();

            SalesSum.Add("Cases", (int)_wonder.Sales.Where(x => x.CaseCode != null).Sum(x => x.ProductQuantity));
            SalesSum.Add("VGAs", (int)_wonder.Sales.Where(x => x.Vgacode != null).Sum(x => x.ProductQuantity));
            SalesSum.Add("HDDs", (int)_wonder.Sales.Where(x => x.Hddcode != null).Sum(x => x.ProductQuantity));
            SalesSum.Add("MBs", (int)_wonder.Sales.Where(x => x.MotherCode != null).Sum(x => x.ProductQuantity));
            SalesSum.Add("PSs", (int)_wonder.Sales.Where(x => x.Psucode != null).Sum(x => x.ProductQuantity));
            SalesSum.Add("RAMs", (int)_wonder.Sales.Where(x => x.RamCode != null).Sum(x => x.ProductQuantity));
            SalesSum.Add("Pros", (int)_wonder.Sales.Where(x => x.ProCode != null).Sum(x => x.ProductQuantity));
            SalesSum.Add("SSDs", (int)_wonder.Sales.Where(x => x.Ssdcode != null).Sum(x => x.ProductQuantity));

            ViewBag.SalesSum = SalesSum;
            #endregion

            #region circle chart
            Dictionary<string, int> quantity = new Dictionary<string, int>();

            quantity.Add("Cases", _wonder.Cases.Sum(x => x.CaseQuantity));
            quantity.Add("VGAs", _wonder.GraphicsCards.Sum(x => x.Vgaquantity));
            quantity.Add("HDDs", _wonder.Hdds.Sum(x => x.Hddquantity));
            quantity.Add("MBs", _wonder.Motherboards.Sum(x => x.MotherQuantity));
            quantity.Add("PSs", _wonder.PowerSupplies.Sum(x => x.Psuquantity));
            quantity.Add("RAMs", _wonder.Rams.Sum(x => x.RamQuantity));
            quantity.Add("Pros", _wonder.Processors.Sum(x => x.ProQuantity));
            quantity.Add("SSDs", _wonder.Ssds.Sum(x => x.Ssdquantity));

            ViewBag.ProductsQuantity = quantity;
            #endregion

            #region top users 
            var topuseres = (from o in _wonder.Sales
                             group o by o.UserId into g
                             orderby g.Sum(x => x.ProductQuantity) descending
                             select new { id = g.Key, quantity = g.Sum(x => x.ProductQuantity) })
                            .Take(5).ToList();

            List<UserVM> topuseresData = new List<UserVM>();
            foreach (var item in topuseres)
            {
                UserVM obj = new UserVM();
                obj.ID = (int)item.id;
                obj.Name = _wonder.Users.Where(x => x.UserId == (int)item.id).Select(x => x.FirstName).FirstOrDefault() + " " + _wonder.Users.Where(x => x.UserId == (int)item.id).Select(x => x.LastName).FirstOrDefault();
                obj.Telephone = _wonder.Users.Where(x => x.UserId == (int)item.id).Select(x => x.Phone).FirstOrDefault();
                obj.Quantity = (int)item.quantity;
                topuseresData.Add(obj);
            }

            ViewBag.TopUsersData = topuseresData;
            #endregion
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }
        public ActionResult EnterAccount(UserVM admin)
        {
            if (_wonder.Users.Where(x => x.Phone == admin.Telephone && x.IsAdmin == true).FirstOrDefault() != null)
            {
                if (_wonder.Users.Where(x => x.Phone == admin.Telephone && x.Password == admin.Password).FirstOrDefault() != null)
                {
                    var id = _wonder.Users.Where(x => x.Phone == admin.Telephone).Select(x => x.UserId).FirstOrDefault();
                    var name = _wonder.Users.Where(x => x.UserId == id).Select(x => new { x.FirstName, x.LastName }).FirstOrDefault();
                    HttpContext.Session.SetInt32("AdminID", id);
                    HttpContext.Session.SetString("AdminName", name.FirstName + " " + name.LastName);
                    return Json("return to index");
                }
                else
                {
                    return Json("wrong password");
                }
            }
            else
                return Json("this phone isn't exist");
        }

        public ActionResult LogOut()
        {
            HttpContext.Session.Remove("AdminID");
            return RedirectToAction("Login");
        }
        public ActionResult Users()
        {
            if ((HttpContext.Session.GetInt32("AdminID").GetValueOrDefault()) == 0)
            {
                return RedirectToAction("Login");
            }
            return View();
        }

        public JsonResult UsersData()
        {
            return Json(_iwonder.GetUsersData());
        }

        public ActionResult Admins()
        {
            if ((HttpContext.Session.GetInt32("AdminID").GetValueOrDefault()) == 0)
            {
                return RedirectToAction("Login");
            }
            return View();
        }

        public JsonResult AdminsData()
        {
            return Json(_iwonder.GetAdmins());
        }

        #region Reviews
        public ActionResult ProductReviews(string code)
        {
            string[] arr = new string[2];
            arr[0] = code;
            arr[1] = "";
            if (code.StartsWith("SSD"))
            {
                arr[1] = _wonder.Ssds.Where(x => x.Ssdcode == code).Select(x => x.Ssdname).FirstOrDefault();
            }
            else if (code.StartsWith("R"))
            {
                arr[1] = _wonder.Rams.Where(x => x.RamCode == code).Select(x => x.RamName).FirstOrDefault();
            }
            else if (code.StartsWith("CAS"))
            {
                arr[1] = _wonder.Cases.Where(x => x.CaseCode == code).Select(x => x.CaseName).FirstOrDefault();
            }
            else if (code.StartsWith("V"))
            {
                arr[1] = _wonder.GraphicsCards.Where(x => x.Vgacode == code).Select(x => x.Vganame).FirstOrDefault();
            }
            else if (code.StartsWith("PS"))
            {
                arr[1] = _wonder.PowerSupplies.Where(x => x.Psucode == code).Select(x => x.Psuname).FirstOrDefault();
            }
            else if (code.StartsWith("PR"))
            {
                arr[1] = _wonder.Processors.Where(x => x.ProCode == code).Select(x => x.ProName).FirstOrDefault();
            }
            else if (code.StartsWith("MO"))
            {
                arr[1] = _wonder.Motherboards.Where(x => x.MotherCode == code).Select(x => x.MotherName).FirstOrDefault();
            }
            else if (code.StartsWith("HD"))
            {
                arr[1] = _wonder.Hdds.Where(x => x.Hddcode == code).Select(x => x.Hddname).FirstOrDefault();
            }
            return View(arr);
        }
        public JsonResult ReviewsData(string code)
        {
            return Json(_iwonder.Reviews(code));
        }
        public JsonResult CheckReview(int ID, int Num)
        {
            Review R = _wonder.Reviews.Where(x => x.ReviewId == ID).Select(x => x).FirstOrDefault();
            if (Num == 1)
            {
                R.IsAvailable = true;
                _wonder.Reviews.Update(R);
                _wonder.SaveChanges();
                return Json("Accepted");
            }
            else if (Num == 2)
            {
                R.IsAvailable = false;
                _wonder.Reviews.Update(R);
                _wonder.SaveChanges();
                return Json("Refused");
            }
            else
            {
                return Json("SomeThing Error");
            }
        }
        #endregion

        #region Tables
        #region Brands
        public ActionResult Brand()
        {
            if ((HttpContext.Session.GetInt32("AdminID").GetValueOrDefault()) == 0)
            {
                return RedirectToAction("Login");
            }
            return View();
        }
        public JsonResult BrandData()
        {
            List<BrandVM> B = new();
            List<Brand> obj = _iwonder.GetProductBrand().ToList();
            foreach (var item in obj)
            {
                BrandVM O = (BrandVM)item;
                B.Add(O);
            }
            return Json(B);
        }

        [HttpPost]
        public IActionResult UpdateBrand(Brand item)
        {
            Brand Edit = _wonder.Brands.Where(x => x.BrandId == item.BrandId).FirstOrDefault();
            Edit.BrandName = item.BrandName;
            _wonder.Brands.Update(Edit);
            _wonder.SaveChanges();
            return Json("Update");
        }


        [HttpPost]
        public ActionResult CreateBrand(string newbrand)
        {
            Brand obj = new()
            {
                BrandName = newbrand
            };
            _wonder.Brands.Add(obj);
            _wonder.SaveChanges();
            return Json("Add");
        }

        #endregion

        #region Case
        public ActionResult Case()
        {
            if ((HttpContext.Session.GetInt32("AdminID").GetValueOrDefault()) == 0)
            {
                return RedirectToAction("Login");
            }
            return View();
        }
        public JsonResult CaseData(string data)
        {
            return Json(_iwonder.GetAllCase(deleteddata: data));
        }

        public IActionResult UpdateCase(string Code)
        {
            ViewBag.Brands = _iwonder.GetProductBrand();
            return View(_iwonder.CaseDetails(Code));
        }

        [HttpPost]
        public IActionResult UpdateCase(CaseVM item, IFormFile Photo_1, IFormFile Photo_2, IFormFile Photo_3)
        {
            var path = Path.Combine(_hostingEnv.WebRootPath, "Images");
            Case Edit = _wonder.Cases.Where(x => x.CaseCode == item.CaseCode).FirstOrDefault();
            Edit.CaseName = item.CaseName;
            Edit.CasePrice = item.CasePrice;
            Edit.CaseBrandId = item.CaseBrandId;
            Edit.CaseFactorySize = item.CaseFactorySize;
            Edit.CaseQuantity = item.CaseQuantity;
            _wonder.Cases.Update(Edit);
            _wonder.SaveChanges();
            var filePath = Path.Combine(_hostingEnv.WebRootPath, "Images");
            List<Image> Data = _wonder.Images.Where(b => b.CaseCode == Edit.CaseCode).ToList();
            if (Data.Count != 0)
            {
                if (Photo_1 != null)
                {
                    string FileName = Photo_1.FileName;
                    var fullPath = Path.Combine(filePath, FileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        Photo_1.CopyTo(stream);
                    }
                    for (int i = 0; i < Data.Count; i++)
                    {
                        if (i == 0)
                        {
                            Data[i].ProductImage = FileName;
                            _wonder.SaveChanges();
                        }
                    }

                }
                if (Photo_2 != null)
                {

                    string FileName = Photo_2.FileName;
                    var fullPath = Path.Combine(filePath, FileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        Photo_2.CopyTo(stream);
                    }
                    for (int i = 0; i < Data.Count; i++)
                    {
                        if (i == 1)
                        {
                            Data[i].ProductImage = FileName;
                            _wonder.SaveChanges();
                        }
                    }

                }
                if (Photo_3 != null)
                {
                    string FileName = Photo_3.FileName;
                    var fullPath = Path.Combine(filePath, FileName);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        Photo_3.CopyTo(stream);
                    }
                    for (int i = 0; i < Data.Count; i++)
                    {
                        if (i == 2)
                        {
                            Data[i].ProductImage = FileName;
                            _wonder.SaveChanges();
                        }
                    }

                }
            }
            else
            {
                if (Photo_1 != null)
                {
                    string fileName = Photo_1.FileName;

                    var fileNameWithPath = Path.Combine(filePath, fileName);

                    using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                    {
                        Photo_1.CopyTo(stream);
                    }
                    _wonder.Images.Add(new Image() { ProductImage = fileName, CaseCode = Edit.CaseCode });

                    _wonder.SaveChanges();
                }
                if (Photo_2 != null)
                {
                    string fileName = Photo_2.FileName;

                    var fileNameWithPath = Path.Combine(filePath, fileName);

                    using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                    {
                        Photo_2.CopyTo(stream);
                    }
                    _wonder.Images.Add(new Image() { ProductImage = fileName, CaseCode = Edit.CaseCode });

                    _wonder.SaveChanges();
                }
                if (Photo_3 != null)
                {
                    string fileName = Photo_3.FileName;

                    var fileNameWithPath = Path.Combine(filePath, fileName);

                    using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                    {
                        Photo_3.CopyTo(stream);
                    }
                    _wonder.Images.Add(new Image() { ProductImage = fileName, CaseCode = Edit.CaseCode });

                    _wonder.SaveChanges();
                }

            }
            return Json("CaseUpdatedDone");

        }
        public ActionResult DeleteCase(string Code)
        {
            Case obj = _wonder.Cases.Where(x => x.CaseCode == Code).FirstOrDefault();
            obj.IsAvailable = false;
            _wonder.Cases.Update(obj);
            _wonder.SaveChanges();
            return RedirectToAction("Case");
        }
        public ActionResult AddDeletedCase(string Code)
        {
            Case obj = _wonder.Cases.Where(x => x.CaseCode == Code).FirstOrDefault();
            obj.IsAvailable = true;
            _wonder.Cases.Update(obj);
            _wonder.SaveChanges();
            return RedirectToAction("Case");
        }

        public ActionResult CreateCase()
        {
            ViewBag.Brands = _iwonder.GetProductBrand();
            return View();
        }
        [HttpPost]
        public ActionResult CreateCase(Case newcase, IFormFile Photo_1, IFormFile Photo_2, IFormFile Photo_3)
        {
            Case obj = newcase;
            obj.IsAvailable = true;
            _wonder.Cases.Add(obj);
            _wonder.SaveChanges();
            var filePath = Path.Combine(_hostingEnv.WebRootPath, "Images");
            if (Photo_1 != null)
            {
                string fileName = Photo_1.FileName;

                var fileNameWithPath = Path.Combine(filePath, fileName);

                using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                {
                    Photo_1.CopyTo(stream);
                }
                _wonder.Images.Add(new Image() { ProductImage = fileName, CaseCode = obj.CaseCode });

                _wonder.SaveChanges();
            }
            if (Photo_2 != null)
            {
                string fileName = Photo_2.FileName;

                var fileNameWithPath = Path.Combine(filePath, fileName);

                using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                {
                    Photo_2.CopyTo(stream);
                }
                _wonder.Images.Add(new Image() { ProductImage = fileName, CaseCode = obj.CaseCode });

                _wonder.SaveChanges();
            }
            if (Photo_3 != null)
            {
                string fileName = Photo_3.FileName;

                var fileNameWithPath = Path.Combine(filePath, fileName);

                using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                {
                    Photo_3.CopyTo(stream);
                }
                _wonder.Images.Add(new Image() { ProductImage = fileName, CaseCode = obj.CaseCode });

                _wonder.SaveChanges();
            }

            return Json("CaseAddedDone");
        }

        #endregion Case

        #region GraphicsCard
        public ActionResult GraphicsCard()
        {
            if ((HttpContext.Session.GetInt32("AdminID").GetValueOrDefault()) == 0)
            {
                return RedirectToAction("Login");
            }
            return View();
        }
        public JsonResult GraphicsCardData(string data)
        {
            return Json(_iwonder.GetAllCard(deleteddata: data));
        }

        public IActionResult UpdateGraphicsCard(string Code)
        {
            ViewBag.Brands = _iwonder.GetProductBrand();
            return View(_iwonder.GraphicsCardDetails(Code));
        }

        [HttpPost]
        public IActionResult UpdateGraphicsCard(GraphicsCardVM item, IFormFile Photo_1, IFormFile Photo_2, IFormFile Photo_3)
        {
            GraphicsCard Edit = _wonder.GraphicsCards.Where(x => x.Vgacode == item.Vgacode).FirstOrDefault();
            Edit.Vganame = item.Vganame;
            Edit.Vgacode = item.Vgacode;
            Edit.VgabrandId = item.VgabrandId;
            Edit.Vgaprice = item.Vgaprice;
            Edit.Vgaquantity = item.Vgaquantity;
            Edit.Vram = item.Vram;
            _wonder.GraphicsCards.Update(Edit);
            _wonder.SaveChanges();
            var filePath = Path.Combine(_hostingEnv.WebRootPath, "Images");
            List<Image> Data = _wonder.Images.Where(b => b.Vgacode == Edit.Vgacode).ToList();
            if (Data.Count != 0)
            {
                if (Photo_1 != null)
                {
                    string FileName = Photo_1.FileName;
                    var fullPath = Path.Combine(filePath, FileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        Photo_1.CopyTo(stream);
                    }
                    for (int i = 0; i < Data.Count; i++)
                    {
                        if (i == 0)
                        {
                            Data[i].ProductImage = FileName;
                            _wonder.SaveChanges();
                        }
                    }

                }
                if (Photo_2 != null)
                {

                    string FileName = Photo_2.FileName;
                    var fullPath = Path.Combine(filePath, FileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        Photo_2.CopyTo(stream);
                    }
                    for (int i = 0; i < Data.Count; i++)
                    {
                        if (i == 1)
                        {
                            Data[i].ProductImage = FileName;
                            _wonder.SaveChanges();
                        }
                    }

                }
                if (Photo_3 != null)
                {
                    string FileName = Photo_3.FileName;
                    var fullPath = Path.Combine(filePath, FileName);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        Photo_3.CopyTo(stream);
                    }
                    for (int i = 0; i < Data.Count; i++)
                    {
                        if (i == 2)
                        {
                            Data[i].ProductImage = FileName;
                            _wonder.SaveChanges();
                        }
                    }

                }
            }
            else
            {
                if (Photo_1 != null)
                {
                    string fileName = Photo_1.FileName;

                    var fileNameWithPath = Path.Combine(filePath, fileName);

                    using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                    {
                        Photo_1.CopyTo(stream);
                    }
                    _wonder.Images.Add(new Image() { ProductImage = fileName, Vgacode = Edit.Vgacode });

                    _wonder.SaveChanges();
                }
                if (Photo_2 != null)
                {
                    string fileName = Photo_2.FileName;

                    var fileNameWithPath = Path.Combine(filePath, fileName);

                    using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                    {
                        Photo_2.CopyTo(stream);
                    }
                    _wonder.Images.Add(new Image() { ProductImage = fileName, Vgacode = Edit.Vgacode });

                    _wonder.SaveChanges();
                }
                if (Photo_3 != null)
                {
                    string fileName = Photo_3.FileName;

                    var fileNameWithPath = Path.Combine(filePath, fileName);

                    using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                    {
                        Photo_3.CopyTo(stream);
                    }
                    _wonder.Images.Add(new Image() { ProductImage = fileName, Vgacode = Edit.Vgacode });

                    _wonder.SaveChanges();
                }

            }
            return Json("VgaUpdatedDone");
        }
        public ActionResult DeleteGraphicsCard(string Code)
        {
            GraphicsCard obj = _wonder.GraphicsCards.Where(x => x.Vgacode == Code).FirstOrDefault();
            obj.IsAvailable = false;
            _wonder.GraphicsCards.Update(obj);
            _wonder.SaveChanges();
            return RedirectToAction("GraphicsCard");
        }
        public ActionResult AddDeletedGraphicsCard(string Code)
        {
            GraphicsCard obj = _wonder.GraphicsCards.Where(x => x.Vgacode == Code).FirstOrDefault();
            obj.IsAvailable = true;
            _wonder.GraphicsCards.Update(obj);
            _wonder.SaveChanges();
            return RedirectToAction("GraphicsCard");
        }

        public ActionResult CreateVga()
        {
            ViewBag.Brands = _iwonder.GetProductBrand();
            return View();
        }
        [HttpPost]
        public ActionResult CreateVga(GraphicsCard newvga, IFormFile Photo_1, IFormFile Photo_2, IFormFile Photo_3)
        {
            GraphicsCard obj = newvga;
            obj.IsAvailable = true;
            _wonder.GraphicsCards.Add(obj);
            _wonder.SaveChanges();
            var filePath = Path.Combine(_hostingEnv.WebRootPath, "Images");
            if (Photo_1 != null)
            {
                string fileName = Photo_1.FileName;

                var fileNameWithPath = Path.Combine(filePath, fileName);

                using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                {
                    Photo_1.CopyTo(stream);
                }
                _wonder.Images.Add(new Image() { ProductImage = fileName, Vgacode = obj.Vgacode });

                _wonder.SaveChanges();
            }
            if (Photo_2 != null)
            {
                string fileName = Photo_2.FileName;

                var fileNameWithPath = Path.Combine(filePath, fileName);

                using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                {
                    Photo_2.CopyTo(stream);
                }
                _wonder.Images.Add(new Image() { ProductImage = fileName, Vgacode = obj.Vgacode });

                _wonder.SaveChanges();
            }
            if (Photo_3 != null)
            {
                string fileName = Photo_3.FileName;

                var fileNameWithPath = Path.Combine(filePath, fileName);

                using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                {
                    Photo_3.CopyTo(stream);
                }
                _wonder.Images.Add(new Image() { ProductImage = fileName, Vgacode = obj.Vgacode });

                _wonder.SaveChanges();
            }
            return Json("VgaAddedDone");
        }
        #endregion GraphicsCard

        #region HDD
        public ActionResult Hdd()
        {
            if ((HttpContext.Session.GetInt32("AdminID").GetValueOrDefault()) == 0)
            {
                return RedirectToAction("Login");
            }
            return View();
        }
        public JsonResult HddData(string data)
        {
            return Json(_iwonder.GetAllHDD(deleteddata: data));
        }

        public IActionResult UpdateHdd(string Code)
        {
            ViewBag.Brands = _iwonder.GetProductBrand();
            return View(_iwonder.HddDetails(Code));
        }

        [HttpPost]
        public IActionResult UpdateHdd(HddVM item, IFormFile Photo_1, IFormFile Photo_2, IFormFile Photo_3)
        {
            Hdd Edit = _wonder.Hdds.Where(x => x.Hddcode == item.Hddcode).FirstOrDefault();
            Edit.Hddcode = item.Hddcode;
            Edit.Hddname = item.Hddname;
            Edit.HddbrandId = item.HddbrandId;
            Edit.Hddprice = item.Hddprice;
            Edit.Hddquantity = item.Hddquantity;
            Edit.Hddsize = item.Hddsize;
            Edit.Hddrpm = item.Hddrpm;
            Edit.Hddtype = item.Hddtype;
            _wonder.Hdds.Update(Edit);
            _wonder.SaveChanges();
            var filePath = Path.Combine(_hostingEnv.WebRootPath, "Images");
            List<Image> Data = _wonder.Images.Where(b => b.Hddcode == Edit.Hddcode).ToList();
            if (Data.Count != 0)
            {
                if (Photo_1 != null)
                {
                    string FileName = Photo_1.FileName;
                    var fullPath = Path.Combine(filePath, FileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        Photo_1.CopyTo(stream);
                    }
                    for (int i = 0; i < Data.Count; i++)
                    {
                        if (i == 0)
                        {
                            Data[i].ProductImage = FileName;
                            _wonder.SaveChanges();
                        }
                    }

                }
                if (Photo_2 != null)
                {

                    string FileName = Photo_2.FileName;
                    var fullPath = Path.Combine(filePath, FileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        Photo_2.CopyTo(stream);
                    }
                    for (int i = 0; i < Data.Count; i++)
                    {
                        if (i == 1)
                        {
                            Data[i].ProductImage = FileName;
                            _wonder.SaveChanges();
                        }
                    }

                }
                if (Photo_3 != null)
                {
                    string FileName = Photo_3.FileName;
                    var fullPath = Path.Combine(filePath, FileName);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        Photo_3.CopyTo(stream);
                    }
                    for (int i = 0; i < Data.Count; i++)
                    {
                        if (i == 2)
                        {
                            Data[i].ProductImage = FileName;
                            _wonder.SaveChanges();
                        }
                    }

                }
            }
            else
            {
                if (Photo_1 != null)
                {
                    string fileName = Photo_1.FileName;

                    var fileNameWithPath = Path.Combine(filePath, fileName);

                    using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                    {
                        Photo_1.CopyTo(stream);
                    }
                    _wonder.Images.Add(new Image() { ProductImage = fileName, Hddcode = Edit.Hddcode });

                    _wonder.SaveChanges();
                }
                if (Photo_2 != null)
                {
                    string fileName = Photo_2.FileName;

                    var fileNameWithPath = Path.Combine(filePath, fileName);

                    using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                    {
                        Photo_2.CopyTo(stream);
                    }
                    _wonder.Images.Add(new Image() { ProductImage = fileName, Hddcode = Edit.Hddcode });

                    _wonder.SaveChanges();
                }
                if (Photo_3 != null)
                {
                    string fileName = Photo_3.FileName;

                    var fileNameWithPath = Path.Combine(filePath, fileName);

                    using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                    {
                        Photo_3.CopyTo(stream);
                    }
                    _wonder.Images.Add(new Image() { ProductImage = fileName, Hddcode = Edit.Hddcode });

                    _wonder.SaveChanges();
                }

            }
            return Json("HddUpdatedDone");
        }
        public ActionResult DeleteHdd(string Code)
        {
            Hdd obj = _wonder.Hdds.Where(x => x.Hddcode == Code).FirstOrDefault();
            obj.IsAvailable = false;
            _wonder.Hdds.Update(obj);
            _wonder.SaveChanges();
            return RedirectToAction("Hdd");
        }
        public ActionResult AddDeletedHdd(string Code)
        {
            Hdd obj = _wonder.Hdds.Where(x => x.Hddcode == Code).FirstOrDefault();
            obj.IsAvailable = true;
            _wonder.Hdds.Update(obj);
            _wonder.SaveChanges();
            return RedirectToAction("Hdd");
        }

        public ActionResult CreateHdd()
        {
            ViewBag.Brands = _iwonder.GetProductBrand();
            return View();
        }
        [HttpPost]
        public ActionResult CreateHdd(Hdd newhdd, IFormFile Photo_1, IFormFile Photo_2, IFormFile Photo_3)
        {
            Hdd obj = newhdd;
            obj.IsAvailable = true;
            _wonder.Hdds.Add(obj);
            _wonder.SaveChanges();
            var filePath = Path.Combine(_hostingEnv.WebRootPath, "Images");
            if (Photo_1 != null)
            {
                string fileName = Photo_1.FileName;

                var fileNameWithPath = Path.Combine(filePath, fileName);

                using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                {
                    Photo_1.CopyTo(stream);
                }
                _wonder.Images.Add(new Image() { ProductImage = fileName, Hddcode = obj.Hddcode });

                _wonder.SaveChanges();
            }
            if (Photo_2 != null)
            {
                string fileName = Photo_2.FileName;

                var fileNameWithPath = Path.Combine(filePath, fileName);

                using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                {
                    Photo_2.CopyTo(stream);
                }
                _wonder.Images.Add(new Image() { ProductImage = fileName, Hddcode = obj.Hddcode });

                _wonder.SaveChanges();
            }
            if (Photo_3 != null)
            {
                string fileName = Photo_3.FileName;

                var fileNameWithPath = Path.Combine(filePath, fileName);

                using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                {
                    Photo_3.CopyTo(stream);
                }
                _wonder.Images.Add(new Image() { ProductImage = fileName, Hddcode = obj.Hddcode });

                _wonder.SaveChanges();
            }
            return Json("HddAddedDone");
        }
        #endregion HDD

        #region MotherBoard
        public ActionResult MotherBoard()
        {
            if ((HttpContext.Session.GetInt32("AdminID").GetValueOrDefault()) == 0)
            {
                return RedirectToAction("Login");
            }
            return View();
        }
        public JsonResult MotherBoardData(string data)
        {
            return Json(_iwonder.GetAllMotherboard(deleteddata: data));
        }

        public IActionResult UpdateMotherBoard(string Code)
        {
            ViewBag.Brands = _iwonder.GetProductBrand();
            return View(_iwonder.MotherboardDetails(Code));
        }

        [HttpPost]
        public IActionResult UpdateMotherBoard(MotherboardVM item, IFormFile Photo_1, IFormFile Photo_2, IFormFile Photo_3)
        {
            Motherboard Edit = _wonder.Motherboards.Where(x => x.MotherCode == item.MotherCode).FirstOrDefault();
            Edit.MotherCode = item.MotherCode;
            Edit.MotherName = item.MotherName;
            Edit.MotherBrandId = item.MotherBrandId;
            Edit.MotherPrice = item.MotherPrice;
            Edit.MotherQuantity = item.MotherQuantity;
            Edit.MotherSocket = item.MotherSocket;
            _wonder.Motherboards.Update(Edit);
            _wonder.SaveChanges();
            var filePath = Path.Combine(_hostingEnv.WebRootPath, "Images");
            List<Image> Data = _wonder.Images.Where(b => b.MotherCode == Edit.MotherCode).ToList();
            if (Data.Count != 0)
            {
                if (Photo_1 != null)
                {
                    string FileName = Photo_1.FileName;
                    var fullPath = Path.Combine(filePath, FileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        Photo_1.CopyTo(stream);
                    }
                    for (int i = 0; i < Data.Count; i++)
                    {
                        if (i == 0)
                        {
                            Data[i].ProductImage = FileName;
                            _wonder.SaveChanges();
                        }
                    }

                }
                if (Photo_2 != null)
                {

                    string FileName = Photo_2.FileName;
                    var fullPath = Path.Combine(filePath, FileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        Photo_2.CopyTo(stream);
                    }
                    for (int i = 0; i < Data.Count; i++)
                    {
                        if (i == 1)
                        {
                            Data[i].ProductImage = FileName;
                            _wonder.SaveChanges();
                        }
                    }

                }
                if (Photo_3 != null)
                {
                    string FileName = Photo_3.FileName;
                    var fullPath = Path.Combine(filePath, FileName);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        Photo_3.CopyTo(stream);
                    }
                    for (int i = 0; i < Data.Count; i++)
                    {
                        if (i == 2)
                        {
                            Data[i].ProductImage = FileName;
                            _wonder.SaveChanges();
                        }
                    }

                }
            }
            else
            {
                if (Photo_1 != null)
                {
                    string fileName = Photo_1.FileName;

                    var fileNameWithPath = Path.Combine(filePath, fileName);

                    using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                    {
                        Photo_1.CopyTo(stream);
                    }
                    _wonder.Images.Add(new Image() { ProductImage = fileName, MotherCode = Edit.MotherCode });

                    _wonder.SaveChanges();
                }
                if (Photo_2 != null)
                {
                    string fileName = Photo_2.FileName;

                    var fileNameWithPath = Path.Combine(filePath, fileName);

                    using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                    {
                        Photo_2.CopyTo(stream);
                    }
                    _wonder.Images.Add(new Image() { ProductImage = fileName, MotherCode = Edit.MotherCode });

                    _wonder.SaveChanges();
                }
                if (Photo_3 != null)
                {
                    string fileName = Photo_3.FileName;

                    var fileNameWithPath = Path.Combine(filePath, fileName);

                    using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                    {
                        Photo_3.CopyTo(stream);
                    }
                    _wonder.Images.Add(new Image() { ProductImage = fileName, MotherCode = Edit.MotherCode });

                    _wonder.SaveChanges();
                }

            }
            return Json("MotherBoardUpdatedDone");
        }
        public ActionResult DeleteMotherBoard(string Code)
        {
            Motherboard obj = _wonder.Motherboards.Where(x => x.MotherCode == Code).FirstOrDefault();
            obj.IsAvailable = false;
            _wonder.Motherboards.Update(obj);
            _wonder.SaveChanges();
            return RedirectToAction("MotherBoard");
        }
        public ActionResult AddDeletedMotherBoard(string Code)
        {
            Motherboard obj = _wonder.Motherboards.Where(x => x.MotherCode == Code).FirstOrDefault();
            obj.IsAvailable = true;
            _wonder.Motherboards.Update(obj);
            _wonder.SaveChanges();
            return RedirectToAction("MotherBoard");
        }

        public ActionResult CreateMotherBoard()
        {
            ViewBag.Brands = _iwonder.GetProductBrand();
            return View();
        }
        [HttpPost]
        public ActionResult CreateMotherBoard(Motherboard newmother, IFormFile Photo_1, IFormFile Photo_2, IFormFile Photo_3)
        {
            Motherboard obj = newmother;
            obj.IsAvailable = true;
            _wonder.Motherboards.Add(obj);
            _wonder.SaveChanges();
            var filePath = Path.Combine(_hostingEnv.WebRootPath, "Images");
            if (Photo_1 != null)
            {
                string fileName = Photo_1.FileName;

                var fileNameWithPath = Path.Combine(filePath, fileName);

                using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                {
                    Photo_1.CopyTo(stream);
                }
                _wonder.Images.Add(new Image() { ProductImage = fileName, MotherCode = obj.MotherCode });

                _wonder.SaveChanges();
            }
            if (Photo_2 != null)
            {
                string fileName = Photo_2.FileName;

                var fileNameWithPath = Path.Combine(filePath, fileName);

                using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                {
                    Photo_2.CopyTo(stream);
                }
                _wonder.Images.Add(new Image() { ProductImage = fileName, MotherCode = obj.MotherCode });

                _wonder.SaveChanges();
            }
            if (Photo_3 != null)
            {
                string fileName = Photo_3.FileName;

                var fileNameWithPath = Path.Combine(filePath, fileName);

                using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                {
                    Photo_3.CopyTo(stream);
                }
                _wonder.Images.Add(new Image() { ProductImage = fileName, MotherCode = obj.MotherCode });

                _wonder.SaveChanges();
            }


            return Json("MotherBoardAddedDone");
        }
        #endregion MotherBoard

        #region PSU
        public ActionResult PowerSupply()
        {
            if ((HttpContext.Session.GetInt32("AdminID").GetValueOrDefault()) == 0)
            {
                return RedirectToAction("Login");
            }
            return View();
        }
        public JsonResult PowerSupplyData(string data)
        {
            return Json(_iwonder.GetAllPowerSuply(deleteddata: data));
        }

        public IActionResult UpdatePowerSupply(string Code)
        {
            ViewBag.Brands = _iwonder.GetProductBrand();
            return View(_iwonder.PowerSupplyDetails(Code));
        }

        [HttpPost]
        public IActionResult UpdatePowerSupply(PowerSupplyVM item, IFormFile Photo_1, IFormFile Photo_2, IFormFile Photo_3)
        {
            PowerSupply Edit = _wonder.PowerSupplies.Where(x => x.Psucode == item.Psucode).FirstOrDefault();
            Edit.Psucode = item.Psucode;
            Edit.Psuname = item.Psuname;
            Edit.PsubrandId = item.PsubrandId;
            Edit.Psuprice = item.Psuprice;
            Edit.Psuquantity = item.Psuquantity;
            Edit.Psuwatt = item.Psuwatt;
            Edit.Psucertificate = item.Psucertificate;
            _wonder.PowerSupplies.Update(Edit);
            _wonder.SaveChanges();
            var filePath = Path.Combine(_hostingEnv.WebRootPath, "Images");
            List<Image> Data = _wonder.Images.Where(b => b.Psucode == Edit.Psucode).ToList();
            if (Data.Count != 0)
            {
                if (Photo_1 != null)
                {
                    string FileName = Photo_1.FileName;
                    var fullPath = Path.Combine(filePath, FileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        Photo_1.CopyTo(stream);
                    }
                    for (int i = 0; i < Data.Count; i++)
                    {
                        if (i == 0)
                        {
                            Data[i].ProductImage = FileName;
                            _wonder.SaveChanges();
                        }
                    }

                }
                if (Photo_2 != null)
                {

                    string FileName = Photo_2.FileName;
                    var fullPath = Path.Combine(filePath, FileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        Photo_2.CopyTo(stream);
                    }
                    for (int i = 0; i < Data.Count; i++)
                    {
                        if (i == 1)
                        {
                            Data[i].ProductImage = FileName;
                            _wonder.SaveChanges();
                        }
                    }

                }
                if (Photo_3 != null)
                {
                    string FileName = Photo_3.FileName;
                    var fullPath = Path.Combine(filePath, FileName);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        Photo_3.CopyTo(stream);
                    }
                    for (int i = 0; i < Data.Count; i++)
                    {
                        if (i == 2)
                        {
                            Data[i].ProductImage = FileName;
                            _wonder.SaveChanges();
                        }
                    }

                }
            }
            else
            {
                if (Photo_1 != null)
                {
                    string fileName = Photo_1.FileName;

                    var fileNameWithPath = Path.Combine(filePath, fileName);

                    using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                    {
                        Photo_1.CopyTo(stream);
                    }
                    _wonder.Images.Add(new Image() { ProductImage = fileName, Psucode = Edit.Psucode });

                    _wonder.SaveChanges();
                }
                if (Photo_2 != null)
                {
                    string fileName = Photo_2.FileName;

                    var fileNameWithPath = Path.Combine(filePath, fileName);

                    using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                    {
                        Photo_2.CopyTo(stream);
                    }
                    _wonder.Images.Add(new Image() { ProductImage = fileName, Psucode = Edit.Psucode });

                    _wonder.SaveChanges();
                }
                if (Photo_3 != null)
                {
                    string fileName = Photo_3.FileName;

                    var fileNameWithPath = Path.Combine(filePath, fileName);

                    using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                    {
                        Photo_3.CopyTo(stream);
                    }
                    _wonder.Images.Add(new Image() { ProductImage = fileName, Psucode = Edit.Psucode });

                    _wonder.SaveChanges();
                }

            }





            return Json("PsuUpdatedDone");
        }
        public ActionResult DeletePowerSupply(string Code)
        {
            PowerSupply obj = _wonder.PowerSupplies.Where(x => x.Psucode == Code).FirstOrDefault();
            obj.IsAvailable = false;
            _wonder.PowerSupplies.Update(obj);
            _wonder.SaveChanges();
            return RedirectToAction("PowerSupply");
        }
        public ActionResult AddDeletedPowerSupply(string Code)
        {
            PowerSupply obj = _wonder.PowerSupplies.Where(x => x.Psucode == Code).FirstOrDefault();
            obj.IsAvailable = true;
            _wonder.PowerSupplies.Update(obj);
            _wonder.SaveChanges();
            return RedirectToAction("PowerSupply");
        }

        public ActionResult CreatePowerSupply()
        {
            ViewBag.Brands = _iwonder.GetProductBrand();
            return View();
        }
        [HttpPost]
        public ActionResult CreatePowerSupply(PowerSupply newmother, IFormFile Photo_1, IFormFile Photo_2, IFormFile Photo_3)
        {
            PowerSupply obj = newmother;
            obj.IsAvailable = true;
            _wonder.PowerSupplies.Add(obj);
            _wonder.SaveChanges();
            var filePath = Path.Combine(_hostingEnv.WebRootPath, "Images");
            if (Photo_1 != null)
            {
                string fileName = Photo_1.FileName;

                var fileNameWithPath = Path.Combine(filePath, fileName);

                using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                {
                    Photo_1.CopyTo(stream);
                }
                _wonder.Images.Add(new Image() { ProductImage = fileName, Psucode = obj.Psucode });

                _wonder.SaveChanges();
            }
            if (Photo_2 != null)
            {
                string fileName = Photo_2.FileName;

                var fileNameWithPath = Path.Combine(filePath, fileName);

                using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                {
                    Photo_2.CopyTo(stream);
                }
                _wonder.Images.Add(new Image() { ProductImage = fileName, Psucode = obj.Psucode });

                _wonder.SaveChanges();
            }
            if (Photo_3 != null)
            {
                string fileName = Photo_3.FileName;

                var fileNameWithPath = Path.Combine(filePath, fileName);

                using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                {
                    Photo_3.CopyTo(stream);
                }
                _wonder.Images.Add(new Image() { ProductImage = fileName, Psucode = obj.Psucode });

                _wonder.SaveChanges();
            }


            return Json("PsuAddedDone");
        }
        #endregion PSU

        #region Processor
        public ActionResult Processor()
        {
            if ((HttpContext.Session.GetInt32("AdminID").GetValueOrDefault()) == 0)
            {
                return RedirectToAction("Login");
            }
            return View();
        }
        public JsonResult ProcessorData(string data)
        {
            return Json(_iwonder.GetAllProcessors(deleteddata: data));
        }

        public IActionResult UpdateProcessor(string Code)
        {
            ViewBag.Brands = _iwonder.GetProductBrand();
            return View(_iwonder.ProcessorDetails(Code));
        }

        [HttpPost]
        public IActionResult UpdateProcessor(ProcessorVM item, IFormFile Photo_1, IFormFile Photo_2, IFormFile Photo_3)
        {
            var path = Path.Combine(_hostingEnv.WebRootPath, "Images");
            Processor Edit = _wonder.Processors.Where(x => x.ProCode == item.ProCode).FirstOrDefault();

            Edit.ProCode = item.ProCode;
            Edit.ProName = item.ProName;
            Edit.ProBrandId = item.ProBrandId;
            Edit.ProPrice = item.ProPrice;
            Edit.ProQuantity = item.ProQuantity;
            Edit.ProCores = item.ProCores;
            Edit.ProSocket = item.ProSocket;
            Edit.ProThreads = item.ProThreads;
            Edit.ProBaseFreq = item.ProBaseFreq;
            Edit.ProMaxTurboFreq = item.ProMaxTurboFreq;
            Edit.ProLithography = item.ProLithography;
            _wonder.Processors.Update(Edit);
            _wonder.SaveChanges();

            var filePath = Path.Combine(_hostingEnv.WebRootPath, "Images");
            List<Image> Data = _wonder.Images.Where(b => b.ProCode == Edit.ProCode).ToList();
            if (Data.Count != 0)
            {
                if (Photo_1 != null)
                {
                    string FileName = Photo_1.FileName;
                    var fullPath = Path.Combine(filePath, FileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        Photo_1.CopyTo(stream);
                    }
                    for (int i = 0; i < Data.Count; i++)
                    {
                        if (i == 0)
                        {
                            Data[i].ProductImage = FileName;
                            _wonder.SaveChanges();
                        }
                    }

                }
                if (Photo_2 != null)
                {

                    string FileName = Photo_2.FileName;
                    var fullPath = Path.Combine(filePath, FileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        Photo_2.CopyTo(stream);
                    }
                    for (int i = 0; i < Data.Count; i++)
                    {
                        if (i == 1)
                        {
                            Data[i].ProductImage = FileName;
                            _wonder.SaveChanges();
                        }
                    }

                }
                if (Photo_3 != null)
                {
                    string FileName = Photo_3.FileName;
                    var fullPath = Path.Combine(filePath, FileName);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        Photo_3.CopyTo(stream);
                    }
                    for (int i = 0; i < Data.Count; i++)
                    {
                        if (i == 2)
                        {
                            Data[i].ProductImage = FileName;
                            _wonder.SaveChanges();
                        }
                    }

                }
            }
            else
            {
                if (Photo_1 != null)
                {
                    string fileName = Photo_1.FileName;

                    var fileNameWithPath = Path.Combine(filePath, fileName);

                    using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                    {
                        Photo_1.CopyTo(stream);
                    }
                    _wonder.Images.Add(new Image() { ProductImage = fileName, ProCode = Edit.ProCode });

                    _wonder.SaveChanges();
                }
                if (Photo_2 != null)
                {
                    string fileName = Photo_2.FileName;

                    var fileNameWithPath = Path.Combine(filePath, fileName);

                    using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                    {
                        Photo_2.CopyTo(stream);
                    }
                    _wonder.Images.Add(new Image() { ProductImage = fileName, ProCode = Edit.ProCode });

                    _wonder.SaveChanges();
                }
                if (Photo_3 != null)
                {
                    string fileName = Photo_3.FileName;

                    var fileNameWithPath = Path.Combine(filePath, fileName);

                    using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                    {
                        Photo_3.CopyTo(stream);
                    }
                    _wonder.Images.Add(new Image() { ProductImage = fileName, ProCode = Edit.ProCode });

                    _wonder.SaveChanges();
                }

            }
            return Json("ProUpdatedDone");
        }
        public ActionResult DeleteProcessor(string Code)
        {
            Processor obj = _wonder.Processors.Where(x => x.ProCode == Code).FirstOrDefault();
            obj.IsAvailable = false;
            _wonder.Processors.Update(obj);
            _wonder.SaveChanges();
            return RedirectToAction("Processor");
        }
        public ActionResult AddDeletedProcessor(string Code)
        {
            Processor obj = _wonder.Processors.Where(x => x.ProCode == Code).FirstOrDefault();
            obj.IsAvailable = true;
            _wonder.Processors.Update(obj);
            _wonder.SaveChanges();
            return RedirectToAction("Processor");
        }

        public ActionResult CreateProcessor()
        {
            ViewBag.Brands = _iwonder.GetProductBrand();
            return View();
        }
        [HttpPost]
        public ActionResult CreateProcessor(Processor newpro, IFormFile Photo_1, IFormFile Photo_2, IFormFile Photo_3
        )
        {
            Processor obj = newpro;
            obj.IsAvailable = true;
            _wonder.Processors.Add(obj);
            _wonder.SaveChanges();
            var filePath = Path.Combine(_hostingEnv.WebRootPath, "Images");
            if (Photo_1 != null)
            {
                string fileName = Photo_1.FileName;

                var fileNameWithPath = Path.Combine(filePath, fileName);

                using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                {
                    Photo_1.CopyTo(stream);
                }
                _wonder.Images.Add(new Image() { ProductImage = fileName, ProCode = obj.ProCode });

                _wonder.SaveChanges();
            }
            if (Photo_2 != null)
            {
                string fileName = Photo_2.FileName;

                var fileNameWithPath = Path.Combine(filePath, fileName);

                using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                {
                    Photo_2.CopyTo(stream);
                }
                _wonder.Images.Add(new Image() { ProductImage = fileName, ProCode = obj.ProCode });

                _wonder.SaveChanges();
            }
            if (Photo_3 != null)
            {
                string fileName = Photo_3.FileName;

                var fileNameWithPath = Path.Combine(filePath, fileName);

                using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                {
                    Photo_3.CopyTo(stream);
                }
                _wonder.Images.Add(new Image() { ProductImage = fileName, ProCode = obj.ProCode });

                _wonder.SaveChanges();
            }
            return Json("ProddedDone");
        }
        #endregion Processor

        #region Ram
        public ActionResult Ram()
        {
            if ((HttpContext.Session.GetInt32("AdminID").GetValueOrDefault()) == 0)
            {
                return RedirectToAction("Login");
            }
            return View();
        }
        public JsonResult RamData(string data)
        {
            return Json(_iwonder.GetAllRAM(deleteddata: data));
        }

        public IActionResult UpdateRam(string Code)
        {
            ViewBag.Brands = _iwonder.GetProductBrand();
            return View(_iwonder.RamDetails(Code));
        }

        [HttpPost]
        public IActionResult UpdateRam(RamVM item, IFormFile Photo_1, IFormFile Photo_2, IFormFile Photo_3)
        {
            Ram Edit = _wonder.Rams.Where(x => x.RamCode == item.RamCode).FirstOrDefault();
            Edit.RamCode = item.RamCode;
            Edit.RamName = item.RamName;
            Edit.RamBrandId = item.RamBrandId;
            Edit.RamPrice = item.RamPrice;
            Edit.RamQuantity = item.RamQuantity;
            Edit.RamSize = item.RamSize;
            Edit.RamFrequency = item.RamFrequency;
            Edit.RamType = item.RamType;
            Edit.Ramkits = item.Ramkits;
            _wonder.Rams.Update(Edit);
            _wonder.SaveChanges();
            var filePath = Path.Combine(_hostingEnv.WebRootPath, "Images");
            List<Image> Data = _wonder.Images.Where(b => b.RamCode == Edit.RamCode).ToList();
            if (Data.Count != 0)
            {
                if (Photo_1 != null)
                {
                    string FileName = Photo_1.FileName;
                    var fullPath = Path.Combine(filePath, FileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        Photo_1.CopyTo(stream);
                    }
                    for (int i = 0; i < Data.Count; i++)
                    {
                        if (i == 0)
                        {
                            Data[i].ProductImage = FileName;
                            _wonder.SaveChanges();
                        }
                    }

                }
                if (Photo_2 != null)
                {

                    string FileName = Photo_2.FileName;
                    var fullPath = Path.Combine(filePath, FileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        Photo_2.CopyTo(stream);
                    }
                    for (int i = 0; i < Data.Count; i++)
                    {
                        if (i == 1)
                        {
                            Data[i].ProductImage = FileName;
                            _wonder.SaveChanges();
                        }
                    }

                }
                if (Photo_3 != null)
                {
                    string FileName = Photo_3.FileName;
                    var fullPath = Path.Combine(filePath, FileName);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        Photo_3.CopyTo(stream);
                    }
                    for (int i = 0; i < Data.Count; i++)
                    {
                        if (i == 2)
                        {
                            Data[i].ProductImage = FileName;
                            _wonder.SaveChanges();
                        }
                    }

                }
            }
            else
            {
                if (Photo_1 != null)
                {
                    string fileName = Photo_1.FileName;

                    var fileNameWithPath = Path.Combine(filePath, fileName);

                    using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                    {
                        Photo_1.CopyTo(stream);
                    }
                    _wonder.Images.Add(new Image() { ProductImage = fileName, RamCode = Edit.RamCode });

                    _wonder.SaveChanges();
                }
                if (Photo_2 != null)
                {
                    string fileName = Photo_2.FileName;

                    var fileNameWithPath = Path.Combine(filePath, fileName);

                    using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                    {
                        Photo_2.CopyTo(stream);
                    }
                    _wonder.Images.Add(new Image() { ProductImage = fileName, RamCode = Edit.RamCode });

                    _wonder.SaveChanges();
                }
                if (Photo_3 != null)
                {
                    string fileName = Photo_3.FileName;

                    var fileNameWithPath = Path.Combine(filePath, fileName);

                    using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                    {
                        Photo_3.CopyTo(stream);
                    }
                    _wonder.Images.Add(new Image() { ProductImage = fileName, RamCode = Edit.RamCode });

                    _wonder.SaveChanges();
                }

            }

            return Json("RamUpdatedDone");
        }
        public ActionResult DeleteRam(string Code)
        {
            Ram obj = _wonder.Rams.Where(x => x.RamCode == Code).FirstOrDefault();
            obj.IsAvailable = false;
            _wonder.Rams.Update(obj);
            _wonder.SaveChanges();
            return RedirectToAction("Ram");
        }
        public ActionResult AddDeletedRam(string Code)
        {
            Ram obj = _wonder.Rams.Where(x => x.RamCode == Code).FirstOrDefault();
            obj.IsAvailable = true;
            _wonder.Rams.Update(obj);
            _wonder.SaveChanges();
            return RedirectToAction("Ram");
        }

        public ActionResult CreateRam()
        {
            ViewBag.Brands = _iwonder.GetProductBrand();
            return View();
        }
        [HttpPost]
        public ActionResult CreateRam(Ram newram, IFormFile Photo_1, IFormFile Photo_2, IFormFile Photo_3)
        {
            Ram obj = newram;
            obj.IsAvailable = true;
            _wonder.Rams.Add(obj);
            _wonder.SaveChanges();
            var filePath = Path.Combine(_hostingEnv.WebRootPath, "Images");
            if (Photo_1 != null)
            {
                string fileName = Photo_1.FileName;

                var fileNameWithPath = Path.Combine(filePath, fileName);

                using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                {
                    Photo_1.CopyTo(stream);
                }
                _wonder.Images.Add(new Image() { ProductImage = fileName, RamCode = obj.RamCode });

                _wonder.SaveChanges();
            }
            if (Photo_2 != null)
            {
                string fileName = Photo_2.FileName;

                var fileNameWithPath = Path.Combine(filePath, fileName);

                using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                {
                    Photo_2.CopyTo(stream);
                }
                _wonder.Images.Add(new Image() { ProductImage = fileName, RamCode = obj.RamCode });

                _wonder.SaveChanges();
            }
            if (Photo_3 != null)
            {
                string fileName = Photo_3.FileName;

                var fileNameWithPath = Path.Combine(filePath, fileName);

                using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                {
                    Photo_3.CopyTo(stream);
                }
                _wonder.Images.Add(new Image() { ProductImage = fileName, RamCode = obj.RamCode });

                _wonder.SaveChanges();
            }

            return Json("RamAddedDone");
        }
        #endregion Ram

        #region SSD
        public ActionResult Ssd()
        {
            if ((HttpContext.Session.GetInt32("AdminID").GetValueOrDefault()) == 0)
            {
                return RedirectToAction("Login");
            }
            return View();
        }
        public JsonResult SsdData(string data)
        {
            return Json(_iwonder.GetAllSSD(deleteddata: data));
        }

        public IActionResult UpdateSsd(string Code)
        {
            ViewBag.Brands = _iwonder.GetProductBrand();
            return View(_iwonder.SsdDetails(Code));
        }

        [HttpPost]
        public IActionResult UpdateSsd(SsdVM item, IFormFile Photo_1, IFormFile Photo_2, IFormFile Photo_3)
        {
            Ssd Edit = _wonder.Ssds.Where(x => x.Ssdcode == item.Ssdcode).FirstOrDefault();
            Edit.Ssdcode = item.Ssdcode;
            Edit.Ssdname = item.Ssdname;
            Edit.SsdbrandId = item.SsdbrandId;
            Edit.Ssdprice = item.Ssdprice;
            Edit.Ssdquantity = item.Ssdquantity;
            Edit.Ssdsize = item.Ssdsize;
            Edit.Ssdinterface = item.Ssdinterface;
            _wonder.Ssds.Update(Edit);
            _wonder.SaveChanges();
            var filePath = Path.Combine(_hostingEnv.WebRootPath, "Images");
            List<Image> Data = _wonder.Images.Where(b => b.Ssdcode == Edit.Ssdcode).ToList();
            if (Data.Count != 0)
            {
                if (Photo_1 != null)
                {
                    string FileName = Photo_1.FileName;
                    var fullPath = Path.Combine(filePath, FileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        Photo_1.CopyTo(stream);
                    }
                    for (int i = 0; i < Data.Count; i++)
                    {
                        if (i == 0)
                        {
                            Data[i].ProductImage = FileName;
                            _wonder.SaveChanges();
                        }
                    }

                }
                if (Photo_2 != null)
                {

                    string FileName = Photo_2.FileName;
                    var fullPath = Path.Combine(filePath, FileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        Photo_2.CopyTo(stream);
                    }
                    for (int i = 0; i < Data.Count; i++)
                    {
                        if (i == 1)
                        {
                            Data[i].ProductImage = FileName;
                            _wonder.SaveChanges();
                        }
                    }

                }
                if (Photo_3 != null)
                {
                    string FileName = Photo_3.FileName;
                    var fullPath = Path.Combine(filePath, FileName);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        Photo_3.CopyTo(stream);
                    }
                    for (int i = 0; i < Data.Count; i++)
                    {
                        if (i == 2)
                        {
                            Data[i].ProductImage = FileName;
                            _wonder.SaveChanges();
                        }
                    }

                }
            }
            else
            {
                if (Photo_1 != null)
                {
                    string fileName = Photo_1.FileName;

                    var fileNameWithPath = Path.Combine(filePath, fileName);

                    using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                    {
                        Photo_1.CopyTo(stream);
                    }
                    _wonder.Images.Add(new Image() { ProductImage = fileName, Ssdcode = Edit.Ssdcode });

                    _wonder.SaveChanges();
                }
                if (Photo_2 != null)
                {
                    string fileName = Photo_2.FileName;

                    var fileNameWithPath = Path.Combine(filePath, fileName);

                    using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                    {
                        Photo_2.CopyTo(stream);
                    }
                    _wonder.Images.Add(new Image() { ProductImage = fileName, Ssdcode = Edit.Ssdcode });

                    _wonder.SaveChanges();
                }
                if (Photo_3 != null)
                {
                    string fileName = Photo_3.FileName;

                    var fileNameWithPath = Path.Combine(filePath, fileName);

                    using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                    {
                        Photo_3.CopyTo(stream);
                    }
                    _wonder.Images.Add(new Image() { ProductImage = fileName, Ssdcode = Edit.Ssdcode });

                    _wonder.SaveChanges();
                }

            }
            return Json("SsdUpdatedDone");
        }
        public ActionResult DeleteSsd(string Code)
        {
            Ssd obj = _wonder.Ssds.Where(x => x.Ssdcode == Code).FirstOrDefault();
            obj.IsAvailable = false;
            _wonder.Ssds.Update(obj);
            _wonder.SaveChanges();
            return RedirectToAction("Ssd");
        }
        public ActionResult AddDeletedSsd(string Code)
        {
            Ssd obj = _wonder.Ssds.Where(x => x.Ssdcode == Code).FirstOrDefault();
            obj.IsAvailable = true;
            _wonder.Ssds.Update(obj);
            _wonder.SaveChanges();
            return RedirectToAction("Ssd");
        }

        public ActionResult CreateSsd()
        {
            ViewBag.Brands = _iwonder.GetProductBrand();
            return View();
        }
        [HttpPost]
        public ActionResult CreateSsd(Ssd newssd, IFormFile Photo_1, IFormFile Photo_2, IFormFile Photo_3)
        {
            Ssd obj = newssd;
            obj.IsAvailable = true;
            _wonder.Ssds.Add(obj);
            _wonder.SaveChanges();
            var filePath = Path.Combine(_hostingEnv.WebRootPath, "Images");
            if (Photo_1 != null)
            {
                string fileName = Photo_1.FileName;

                var fileNameWithPath = Path.Combine(filePath, fileName);

                using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                {
                    Photo_1.CopyTo(stream);
                }
                _wonder.Images.Add(new Image() { ProductImage = fileName, Ssdcode = obj.Ssdcode });

                _wonder.SaveChanges();
            }
            if (Photo_2 != null)
            {
                string fileName = Photo_2.FileName;

                var fileNameWithPath = Path.Combine(filePath, fileName);

                using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                {
                    Photo_2.CopyTo(stream);
                }
                _wonder.Images.Add(new Image() { ProductImage = fileName, Ssdcode = obj.Ssdcode });

                _wonder.SaveChanges();
            }
            if (Photo_3 != null)
            {
                string fileName = Photo_3.FileName;

                var fileNameWithPath = Path.Combine(filePath, fileName);

                using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                {
                    Photo_3.CopyTo(stream);
                }
                _wonder.Images.Add(new Image() { ProductImage = fileName, Ssdcode = obj.Ssdcode });

                _wonder.SaveChanges();
            }
            return Json("SsdAddedDone");
        }
        #endregion SSD


        #endregion Tables

        public ActionResult Sales()
        {
            if ((HttpContext.Session.GetInt32("AdminID").GetValueOrDefault()) == 0)
            {
                return RedirectToAction("Login");
            }
            return View();
        }
        public ActionResult GetAdminInfo()
        {
            int adminId = HttpContext.Session.GetInt32("AdminID").GetValueOrDefault();
            string adminName = _wonder.Users.Where(x => x.UserId == adminId).Select(x => x.FirstName).FirstOrDefault() + " " + _wonder.Users.Where(x => x.UserId == adminId).Select(x => x.LastName).FirstOrDefault();
            return Json(adminName);
        }

        #region Sales
        [HttpPost]
        public IActionResult SalesData(string code)
        {
            List<SalesVM> Sales = new List<SalesVM>();
            if (code != null)
            {
                var salesTable = code.Split(',');

                foreach (var item in salesTable)
                {
                    switch (item.ToUpper())
                    {
                        case "CAS":
                            var CAS = _iwonder.GetCases();
                            Sales.AddRange(CAS);
                            break;
                        case "V":
                            var V = _iwonder.GetGraphicsCard();
                            Sales.AddRange(V);
                            break;
                        case "HD":
                            var HD = _iwonder.GetHDD();
                            Sales.AddRange(HD);
                            break;
                        case "MO":
                            var MO = _iwonder.GetMotherboard();
                            Sales.AddRange(MO);
                            break;
                        case "PS":
                            var PS = _iwonder.GetPowerSupplies();
                            Sales.AddRange(PS);
                            break;
                        case "PR":
                            var Pr = _iwonder.GetProcessor();
                            Sales.AddRange(Pr);
                            break;
                        case "R":
                            var R = _iwonder.GetRam();
                            Sales.AddRange(R);
                            break;
                        case "SSD":
                            var SSD = _iwonder.GetSDD();
                            Sales.AddRange(SSD);
                            break;
                    }
                }
                return Json(Sales.OrderBy(s => s.UserID).Distinct());
            }
            return Json(new SalesVM());
        }

        #endregion
        #region chat
        public ActionResult AdminChat()
        {
            int adminId = HttpContext.Session.GetInt32("AdminID").GetValueOrDefault();
            if (adminId == 0)
            {
                return RedirectToAction("Login");
            }
            else
            {
                ViewBag.adminId = adminId;
                ViewBag.adminName = _wonder.Users.Where(x => x.UserId == adminId).Select(x => x.FirstName).FirstOrDefault() + " " + _wonder.Users.Where(x => x.UserId == adminId).Select(x => x.LastName).FirstOrDefault();
                return View();
            }
        }
        public ActionResult GetChats()
        {
            int adminId = HttpContext.Session.GetInt32("AdminID").GetValueOrDefault();

            var usersIDs = _wonder.Messages.Select(x => x.UserId).Distinct().ToList();
            List<ChatVM> LMsgInfo = new List<ChatVM>();

            foreach (var item in usersIDs)
            {
                Message row = _wonder.Messages.OrderByDescending(x => x.MessageId).Where(x => x.UserId == item).FirstOrDefault();

                ChatVM obj = new ChatVM();
                obj.MessageId = row.MessageId;
                obj.UserId = row.UserId;
                obj.UserName = row.User.FirstName + " " + row.User.LastName;
                obj.Time = row.Time.ToShortTimeString();
                obj.MessageText = row.MessageText;
                obj.Seen = row.Seen;
                obj.AdminOrNot = row.AdminOrNot;
                LMsgInfo.Add(obj);
            }
            return Json(LMsgInfo.OrderByDescending(x => x.MessageId));
        }
        public ActionResult GetMessages(int userid)
        {
            var messages = _iwonder.GetAllMessages(userid);
            return Json(messages);
        }
        public ActionResult SeeMessages(int userid)
        {
            List<Message> NotSeenRows = new List<Message>();
            NotSeenRows = _wonder.Messages.Where(x => x.UserId == userid && x.AdminOrNot == false && x.Seen == false).ToList();

            foreach (var item in NotSeenRows)
            {
                item.Seen = true;
                _wonder.Messages.Update(item);
            }
            _wonder.SaveChanges();
            return Json("seen");
        }
        public ActionResult GetMessagesCounter()
        {
            int counter = _wonder.Messages.Where(x => x.Seen == false && x.AdminOrNot == false).Select(x => x.UserId).Distinct().Count();
            return Json(counter);
        }


        #endregion
        #region chatsearch
        public ActionResult Search(string src)
        {
            List<int> usersIDs = _wonder.Messages.Select(x => x.UserId).Distinct().ToList();
            List<ChatVM> userInfo = new List<ChatVM>();
            foreach (var item in usersIDs)
            {
                Message row = _wonder.Messages.OrderByDescending(x => x.MessageId).Where(x => x.UserId == item).FirstOrDefault();
                string name = row.User.FirstName + " " + row.User.LastName;
                if (name.Contains(src) == true)
                {
                    ChatVM obj = new ChatVM();
                    obj.MessageId = row.MessageId;
                    obj.UserId = item;
                    obj.UserName = name;
                    obj.Time = row.Time.ToShortTimeString();
                    obj.MessageText = row.MessageText;
                    obj.Seen = row.Seen;
                    userInfo.Add(obj);
                }
            }
            return Json(userInfo.OrderByDescending(x => x.MessageId));
        }
        #endregion
        #region Images

        [HttpGet]
        public JsonResult Images(string Code)
        {
            if (Code == null)
            {
                return Json(0);
            }
            //var Data=_wonder.Images.Where(ProductCode=>ProductCode.)
            if (Code.ToUpper().StartsWith("CAS"))
            {
                var CaseImages = _wonder.Images.Where(productCode => productCode.CaseCode == Code).Select(img => img.ProductImage);
                return Json(CaseImages);
            }
            else if (Code.ToUpper().StartsWith("V"))
            {
                var VImages = _wonder.Images.Where(productCode => productCode.Vgacode == Code).Select(img => img.ProductImage);
                return Json(VImages);
            }
            else if (Code.ToUpper().StartsWith("HD"))
            {
                var hddmages = _wonder.Images.Where(productCode => productCode.Hddcode == Code).Select(img => img.ProductImage);
                return Json(hddmages);
            }
            else if (Code.ToUpper().StartsWith("MO"))
            {
                var Mohmages = _wonder.Images.Where(productCode => productCode.MotherCode == Code).Select(img => img.ProductImage);
                return Json(Mohmages);
            }
            else if (Code.ToUpper().StartsWith("PS"))
            {
                var PSmages = _wonder.Images.Where(productCode => productCode.Psucode == Code).Select(img => img.ProductImage);
                return Json(PSmages);
            }
            else if (Code.ToUpper().StartsWith("PR"))
            {
                var PRomages = _wonder.Images.Where(productCode => productCode.ProCode == Code).Select(img => img.ProductImage);
                return Json(PRomages);
            }
            else if (Code.ToUpper().StartsWith("R"))
            {
                var RAMmages = _wonder.Images.Where(productCode => productCode.RamCode == Code).Select(img => img.ProductImage);
                return Json(RAMmages);
            }
            else if (Code.ToUpper().StartsWith("SSD"))
            {
                var SSDmages = _wonder.Images.Where(productCode => productCode.Ssdcode == Code).Select(img => img.ProductImage);
                return Json(SSDmages);
            }
            else
            {
                return Json(0);
            }

        }
        #endregion

    }
}








