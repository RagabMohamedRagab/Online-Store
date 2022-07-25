using BLL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel.Models;

namespace DAL
{
    public interface IWonder
    {
        #region GetAllProducts
        List<ProcessorVM> GetAllProcessors(int userid = 0, string deleteddata = null);
        List<MotherboardVM> GetAllMotherboard(int userid = 0, string deleteddata = null);
        List<HddVM> GetAllHDD(int userid = 0, string deleteddata = null);
        List<RamVM> GetAllRAM(int userid = 0, string deleteddata = null);
        List<SsdVM> GetAllSSD(int userid = 0, string deleteddata = null);
        List<GraphicsCardVM> GetAllCard(int userid = 0, string deleteddata = null);
        List<CaseVM> GetAllCase(int userid = 0, string deleteddata = null);
        List<PowerSupplyVM> GetAllPowerSuply(int userid = 0, string deleteddata = null);

        #endregion

        #region StorePage
        #region Processor

        IEnumerable<BrandVM> GetProcessorBrandNamesAndNumbers();

        IEnumerable<ProcessorVM> GetProcessorProductsByPrice(IEnumerable<ProcessorVM> processorVMs, int Id, int userid = 0);

        IEnumerable<ProcessorVM> GetProcessorProductsByBrand(string[] BName,  int id, int min=100, int max= 50000, int userid = 0);

        IEnumerable<ProcessorVM> GetProcessorDependentOnSort(int id, int userid = 0);

        IEnumerable<ProcessorVM> GetProcessorPriceDependentOnBrand( int sort, int min = 100, int max = 50000, int userid = 0);

    
        #endregion

        #region Motherboard
        IEnumerable<BrandVM> GetMotherboardBrandNamesAndNumbers(int userid = 0);
        IEnumerable<MotherboardVM> GetMotherboardProductsByPrice(IEnumerable<MotherboardVM> motherboardVM, int Id, int userid = 0);
        IEnumerable<MotherboardVM> GetMotherboardProductsByBrand(string[] BName,  int id, int min=100, int max=50000, int userid = 0);
        IEnumerable<MotherboardVM> GetMotherboardDependentOnSort(int id, int userid = 0);
        IEnumerable<MotherboardVM> GetMotherboardPriceDependentOnBrand( int sort, int min=100, int max=50000, int userid = 0);


        #endregion

        #region HDD
        IEnumerable<BrandVM> GetHDDBrandNamesAndNumbers(int userid = 0);
        IEnumerable<HddVM> GetHDDProductsByPrice(IEnumerable<HddVM> hddVM, int Id, int userid = 0);
        IEnumerable<HddVM> GetHDDProductsByBrand(string[] BName, int id, int min = 100, int max = 50000, int userid = 0);
        IEnumerable<HddVM> GetHDDDependentOnSort(int id, int userid = 0);
        IEnumerable<HddVM> GetHDDPriceDependentOnBrand( int sort, int min=100, int max=50000, int userid = 0);
        #endregion

        #region RAM


        IEnumerable<BrandVM> GetRAMBrandNamesAndNumbers(int userid = 0);

        IEnumerable<RamVM> GetRAMProductsByPrice(IEnumerable<RamVM> RamVMs, int Id, int userid = 0);

        IEnumerable<RamVM> GetRAMProductsByBrand(string[] BName,  int id, int min=100, int max=50000, int userid = 0);

       
      
        IEnumerable<RamVM> GetRAMDependentOnSort(int id,int userid = 0);

        IEnumerable<RamVM> GetRAMPriceDependentOnBrand( int sort, int min=100, int max=500000, int userid = 0);

        #endregion

        #region Graphics Card
        IEnumerable<BrandVM> GetCardBrandNamesAndNumbers(int userid = 0);
        IEnumerable<GraphicsCardVM> GetCardProductsByPrice(IEnumerable<GraphicsCardVM> hddVM, int Id, int userid = 0);
        IEnumerable<GraphicsCardVM> GetCardProductsByBrand(string[] BName, int id, int min=100, int max=50000, int userid = 0);
        IEnumerable<GraphicsCardVM> GetCardDependentOnSort(int id, int userid = 0);
        IEnumerable<GraphicsCardVM> GetCardPriceDependentOnBrand( int sort,int min=100, int max=50000, int userid = 0);
        #endregion

        #region SSd
        IEnumerable<BrandVM> GetSSDBrandNamesAndNumbers(int userid = 0);

        IEnumerable<SsdVM> GetSSDProductsByPrice(IEnumerable<SsdVM> hddVM, int Id, int userid = 0);

        IEnumerable<SsdVM> GetSSDProductsByBrand(string[] BName,  int id, int min=100, int max=50000,int userid = 0);

        IEnumerable<SsdVM> GetSSDDependentOnSort(int id, int userid = 0);
        IEnumerable<SsdVM> GetSSDPriceDependentOnBrand( int sort, int min=100, int max=50000, int userid = 0);
        #endregion


        #region Case


        IEnumerable<BrandVM> GetCaseBrandNamesAndNumbers(int userid = 0);

        IEnumerable<CaseVM> GetCaseProductsByPrice(IEnumerable<CaseVM> hddVM, int Id, int userid = 0);

        IEnumerable<CaseVM> GetCaseProductsByBrand(string[] BName,int id, int min=100, int max=50000, int userid = 0);
        IEnumerable<CaseVM> GetCaseDependentOnSort(int id, int userid = 0);
        IEnumerable<CaseVM> GetCasePriceDependentOnBrand( int sort, int min=100, int max=50000, int userid = 0);
        #endregion

        #region PowerSuply
        IEnumerable<BrandVM> GetPowerSuplyBrandNamesAndNumbers(int userid = 0);
        IEnumerable<PowerSupplyVM> GetPowerSuplyProductsByPrice(IEnumerable<PowerSupplyVM> PSVM, int Id, int userid = 0);
        IEnumerable<PowerSupplyVM> GetPowerSuplyProductsByBrand(string[] BName,  int id, int min=100, int max=50000, int userid = 0);
        IEnumerable<PowerSupplyVM> GetPowerSuplyDependentOnSort(int id,int userid = 0);
        IEnumerable<PowerSupplyVM> GetPowerSuplyPriceDependentOnBrand( int sort, int min=100, int max=50000, int userid = 0);
        #endregion

        #endregion


        #region Product Details

        CaseVM CaseDetails(string code, int currentPageIndex = 0);

        GraphicsCardVM GraphicsCardDetails(string code, int currentPageIndex = 0);

        HddVM HddDetails(string code, int currentPageIndex = 0);

        MotherboardVM MotherboardDetails(string code, int currentPageIndex = 0);

        PowerSupplyVM PowerSupplyDetails(string code, int currentPageIndex = 0);

        ProcessorVM ProcessorDetails(string code, int currentPageIndex = 0);

        RamVM RamDetails(string code, int currentPageIndex = 0);

        SsdVM SsdDetails(string code, int currentPageIndex = 0);

        #endregion

        #region TopSelling

        public List<CaseVM> GetTopCases(int userid = 0);
        public List<GraphicsCardVM> GetTopVgas(int userid = 0);
        public List<HddVM> GetTopHdds(int userid = 0);
        public List<MotherboardVM> GetTopMotherboards(int userid = 0);
        public List<PowerSupplyVM> GetTopPsus(int userid = 0);
        public List<ProcessorVM> GetTopProcessors(int userid = 0);
        public List<RamVM> GetTopRams(int userid = 0);
        public List<SsdVM> GetTopSsds(int userid = 0);


        #endregion

        #region Check Order
        string CheckOrderCreateAcc(UserVM UserData, SalesVM[] OrderData);

        //string CheckOrderSignIn(UserVM UserData, SalesVM[] OrderData);

        string CheckOrder(SalesVM[] OrderData, int userid = 0);

        #endregion

        #region WishList

        List<WishListVM> GetWishList(int userid);
        string DeletefromWL(string ProductCode, int userid);
        string CheckfromWL(string ProductCode, int userid);
        string AddToWL(string ProductCode, int userid);

        #endregion

        #region Review
        public ReviewVM AddReview(ReviewVM review);
        public List<ReviewVM> Reviews(string code);
        #endregion

        #region Search
        public List<Search> SearchProduct(string src);
        public List<Search> SearchMotherBoard(string src);
        public List<Search> SearchProcessor(string src);
        public List<Search> SearchRam(string src);
        public List<Search> SearchSSD(string src);
        public List<Search> SearchHDD(string src);
        public List<Search> SearchCase(string src);
        public List<Search> SearchPowerSupply(string src);
        public List<Search> SearchVGA(string src);
        public List<Search> SearchFunction(string src, int num);
        #endregion

        public List<Brand> GetProductBrand();

        #region Admin Project

        public List<UserVM> GetUsersData();

        public List<UserVM> GetAdmins();

        public List<SalesVM> GetProcessor();
        public List<SalesVM> GetMotherboard();
        public List<SalesVM> GetSDD();
        public List<SalesVM> GetHDD();
        public List<SalesVM> GetCases();
        public List<SalesVM> GetPowerSupplies();
        public List<SalesVM> GetGraphicsCard();
        public List<SalesVM> GetRam();

        #endregion

        public List<ChatVM> GetAllMessages(int userid);
        
    }
}