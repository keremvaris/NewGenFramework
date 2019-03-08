using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BayiPuan.Business.Abstract;
using BayiPuan.DataAccess.Concrete.Context;
using BayiPuan.Entities.ComplexTypes;
using BayiPuan.Entities.Concrete;
using BayiPuan.MvcWebUi.Filters;
using BayiPuan.MvcWebUi.HtmlHelpers;
using BayiPuan.MvcWebUi.Infrastructure;
using BayiPuan.MvcWebUi.Models.ViewModels;
using BayiPuan.MvcWebUi.Models.ComplexTypeModels;
using DotNet.Highcharts.Enums;
using DotNet.Highcharts.Helpers;
using DotNet.Highcharts.Options;
using NewGenFramework.Core.DataAccess;

namespace BayiPuan.MvcWebUi.Controllers
{
  [AuthorizationFilter]
  public class UserHomeController : BaseController
  {
    private readonly ISaleService _saleService;
    private readonly IProductService _productService;
    private readonly ICustomerService _customerService;
    private readonly IUserService _userService;
    private readonly IGiftService _giftService;
    private readonly IBuyService _buyService;
    private readonly ICampaignService _campaignService;
    private readonly IQueryableRepository<Sale> _saleQueryableRepository;
    private readonly IQueryableRepository<Product> _productQueryableRepository;
    private readonly IQueryableRepository<Customer> _customerQueryableRepository;
    private readonly IQueryableRepository<Gift> _giftQueryableRepository;
    private readonly IQueryableRepository<Buy> _buyQueryableRepository;
    private readonly IQueryableRepository<User> _userQueryableRepository;
    private readonly IQueryableRepository<Campaign> _campaignQueryableRepository;
    private readonly IQueryableRepository<Score> _scroreQueryableRepository;
    public UserHomeController(ISaleService saleService, IProductService productService, ICustomerService customerService, IUserService userService, IGiftService giftService, IBuyService buyService, ICampaignService campaignService, IQueryableRepository<Sale> saleQueryableRepository, IQueryableRepository<Product> productQueryableRepository, IQueryableRepository<Customer> customerQueryableRepository, IQueryableRepository<Gift> giftQueryableRepository, IQueryableRepository<Buy> buyQueryableRepository, IQueryableRepository<User> userQueryableRepository, IQueryableRepository<Campaign> campaignQueryableRepository, IQueryableRepository<Score> scroreQueryableRepository)
    {
      _saleService = saleService;
      _productService = productService;
      _customerService = customerService;
      _userService = userService;
      _giftService = giftService;
      _buyService = buyService;
      _campaignService = campaignService;
      _productQueryableRepository = productQueryableRepository;
      _customerQueryableRepository = customerQueryableRepository;
      _giftQueryableRepository = giftQueryableRepository;
      _buyQueryableRepository = buyQueryableRepository;
      _userQueryableRepository = userQueryableRepository;
      _campaignQueryableRepository = campaignQueryableRepository;
      _scroreQueryableRepository = scroreQueryableRepository;
      _saleQueryableRepository = saleQueryableRepository;
    }

    // GET: UserHome
    public ActionResult Index(int id)
    {
      if(User.Identity.IsAuthenticated == false || GeneralHelpers.GetUserId()!=id.ToString())
      {
        return RedirectToAction("SignOut", "Account");
      }
      if (GeneralHelpers.GetUserId()==null || GeneralHelpers.GetUserId()=="")
      {
       return RedirectToAction("SignIn", "Account");
      }
      var product = _productQueryableRepository.Table.AsNoTracking().ToList();
      var sale = _saleQueryableRepository.Table.AsNoTracking().ToList();
      var gift = _giftQueryableRepository.Table.AsNoTracking().ToList();
      var buyGift = _buyQueryableRepository.Table.AsNoTracking().ToList();
      var user = _userQueryableRepository.Table.AsNoTracking().ToList();
      List<Campaign> campaign = _campaignQueryableRepository.Table.AsNoTracking().ToList();
      var score = _scroreQueryableRepository.Table.AsNoTracking().ToList();
      var order = 0;
      var vm = new ViewModel
      {
        totalWon = (from s in score
                    
                    select new
                    {
                      s.UserId,
                      s.ScoreTotal
                    }).Where(p => p.UserId == id)
              .GroupBy(w => w.UserId)
              .Select(y => new ProductPoint
              {
                UserId = y.Key,
                SumPoint = y.Sum(x => x.ScoreTotal),
                //SumPointToMoney = y.Sum(x => x.AmountOfSales * x.Point * x.PointToMoney)
              }).ToList(),

        spentPoint = (from b in buyGift
                      join g in gift on b.GiftId equals g.GiftId
                      select new
                      {
                        b.UserId,
                        b.IsApproved,
                        g.GiftPoint
                      }).Where(x => x.UserId == id && x.IsApproved == true)
              .GroupBy(w => w.UserId)
              .Select(y => new SpentPoint()
              {
                UserId = y.Key,
                SpendPoint = y.Sum(x => x.GiftPoint)
              }).ToList(),

        saleRankings = (from s in sale
                        join u in user on s.UserId equals u.UserId
                        select new
                        {
                          u.UserId,
                          u.FirstName,
                          u.LastName,
                          s.AmountOfSales,
                          s.IsApproved
                        }).Where(x => x.IsApproved == true).GroupBy(x => new { x.UserId, x.FirstName, x.LastName })
          .Select(y => new SalesRanking()
          {
            UserId = y.Key.UserId,
            SaleOrder = order + 1,
            FirstName = y.Key.FirstName,
            LastName = y.Key.LastName,
            SumSale = y.Sum(x => x.AmountOfSales)
          }).AsEnumerable().OrderByDescending(x => x.SumSale)
      };

      //puan hesaplama Yöntemi
      //Yapılan satış miktarı * Puan
      //a.	Toplam kazanılan puan

      //b.	Harcanan puan
      //c.	Kalan puan 
      decimal remaining = vm.totalWon.Sum(t => t.SumPoint) - vm.spentPoint.Sum(s => s.SpendPoint);
      decimal remainingTomoney = vm.totalWon.Sum(t => t.SumPointToMoney) - vm.spentPoint.Sum(s => s.SumPointToMoney);

      vm.remainingScore = remaining;
      vm.remainingScoreToMoney = remainingTomoney;

      //d.	Kampanya bilgileri
      vm.CampaignView = campaign;
      //e.	Duyurular
      //f.	Satış sıralamaları Chart
      var table = new DataTable();
      var dt = @"select Top 10 FirstName +' '+ LastName UserName,sum(AmountOfSales) Count from Users u  
inner join Sales s on  s.UserId=u.UserId 
Where s.IsApproved=1
group by FirstName,LastName order by Count desc";
      using (var dbx = new BayiPuanContext())
      {
        var cmd = dbx.Database.Connection.CreateCommand();
        var sql = dt;
        cmd.CommandText = sql;
        cmd.Connection.Open();
        table.Load(cmd.ExecuteReader());
        DataSet ds = new DataSet();
        ds.Tables.Add(table);
      }
      DotNet.Highcharts.Highcharts chart = new DotNet.Highcharts.Highcharts("chart")
        .InitChart(new Chart
        {
          DefaultSeriesType = ChartTypes.Column,
          
        })
        .SetTitle(new Title
        {
          Text = "En İyi İlk 10"
        })
        .SetXAxis(new XAxis
        {
          //Categories = new[] { "Ocak", "Şubat", "Mart", "Nisan", "Mayıs", "Haziran", "Temmuz", "Ağustos", "Eylül", "Ekim", "Kasım", "Aralık" }
          Categories = table.AsEnumerable().Select(r => r.Field<string>("UserName").ToString()).ToArray()
        })
        .SetYAxis(new YAxis
        {
          Title = new YAxisTitle { Text = "Satılan Ürün Miktarı" },
          PlotLines = new[]
          {
            new YAxisPlotLines
            {
              Value = 0,
              Width = 1,
              Color = ColorTranslator.FromHtml("#808080")
            }
          }
        })
        .SetSeries(
          new Series[] {
            new Series
            {
              Name = "Toplam Ürün Satış Sayıları", Data = new Data(
                table.AsEnumerable().Select(r=>r.Field<int>("Count")).Cast<object>().ToArray()
              )
            }
          }
        );
      ViewData["chart"] = chart.ToHtmlString();
      return View(vm);
    }
  }
}