using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using BayiPuan.Business.Abstract;
using BayiPuan.Entities.Concrete;
using BayiPuan.MvcWebUi.Filters;
using BayiPuan.MvcWebUi.HtmlHelpers;
using BayiPuan.MvcWebUi.Infrastructure;
using NewGenFramework.Core.Aspects.Postsharp.AuthorizationAspects;
using NewGenFramework.Core.DataAccess;
using NewGenFramework.Core.Utilities.Common;

namespace BayiPuan.MvcWebUi.Controllers
{
  [AuthorizationFilter]
  public class BuyUserController : BaseController
  {
    private readonly IQueryableRepository<Gift> _giftQueryableRepository;
    private readonly IQueryableRepository<Brand> _brandQueryableRepository;
    private readonly IQueryableRepository<Category> _categoryQueryableRepository;
    private readonly IQueryableRepository<User> _userQueryableRepository;
    private readonly IGiftService _giftService;
    private readonly IBuyService _buyService;

    public BuyUserController(IQueryableRepository<Gift> giftQueryableRepository, IQueryableRepository<Brand> brandQueryableRepository, IQueryableRepository<Category> categoryQueryableRepository, IGiftService giftService, IBuyService buyService, IQueryableRepository<User> userQueryableRepository)
    {
      _giftQueryableRepository = giftQueryableRepository;
      _brandQueryableRepository = brandQueryableRepository;
      _categoryQueryableRepository = categoryQueryableRepository;
      _giftService = giftService;
      _buyService = buyService;
      _userQueryableRepository = userQueryableRepository;
    }
    [SecuredOperation(Roles = "SystemAdmin,Admin,User")]
    // GET: BuyUser
    public ActionResult Index(int? id)
    {
      //var dataList = _giftService.GetAll("Category,Brand");
      var dataList = _giftQueryableRepository.Table.Include("Category").Include("Brand").ToList();

      var list = dataList.Where(x => x.IsActive).GroupBy(x => x.BrandId).OrderBy(x => x.Key).ToList();
      var listId = dataList.Where(x => x.IsActive && x.BrandId == id).GroupBy(x => x.BrandId).OrderBy(x => x.Key)
        .ToList();
      if (id == null)
      {
        return View(list);
      }
      return View(listId);
    }
    public ActionResult CategoryIndex(int? id)
    {
      //var dataList = _giftService.GetAll("Category,Brand");
      var dataList = _giftQueryableRepository.Table.Include("Category").Include("Brand").AsNoTracking().ToList();

      var list = dataList.Where(x => x.IsActive).GroupBy(x => x.CategoryId).OrderBy(x => x.Key).ToList();
      var listId = dataList.Where(x => x.IsActive && x.CategoryId == id).GroupBy(x => x.BrandId).OrderBy(x => x.Key)
        .ToList();
      if (id == null)
      {
        return View(list);
      }
      return View(listId);
    }
    [HttpPost]
    [SecuredOperation(Roles = "SystemAdmin,Admin,User")]
    public ActionResult BuyGift(int id)
    {
      var loggedUserId = Convert.ToInt32(GeneralHelpers.GetUserId());
      var userMail = _userQueryableRepository.Table.AsNoTracking().FirstOrDefault(x => x.UserId == loggedUserId);
      var buyingGift = _giftService.GetById(id);
      var remainingPoint = GeneralHelpers.GetRemainingPoint();
      
      var brand = _giftQueryableRepository.Table.Include("Brand").AsNoTracking().FirstOrDefault(x => x.GiftId == id);
      var brandId = _brandQueryableRepository.Table.FirstOrDefault(x => x.BrandId == brand.BrandId);
      if (buyingGift.GiftPoint > remainingPoint)
      {
        ErrorNotification("Çok Üzgünüm:( Yeterli Miktarda BaPu'nuz Yok. Daha Fazla BaPu İçin Puan Kazan Ekranına Gidip Satış Girmeye Ne Dersiniz?");
        return RedirectToAction("Index", "BuyUser");
      }
      _buyService.Add(new Buy
      {
        GiftId = buyingGift.GiftId,
        UserId = Convert.ToInt32(GeneralHelpers.GetUserId()),
        BuyDate = DateTime.Now,
        BuyAmount = 1,
        BrandId = brand.BrandId,
        IsApproved = true,
        BuyState = BuyState.IncelemeBekliyor
      });
      var mailEnable = System.Configuration.ConfigurationManager.AppSettings["MailEnable"];
      if (mailEnable == "true")
      {
        var mail = MailHelper.SendMail(
          $"<strong>{userMail.FirstName + " " + userMail.LastName}</strong> Kullanıcısının <strong>{brandId.BrandName + " " + buyingGift.Description}</strong> için Hediye İsteği var.<br/> Lütfen bayipuan.com üzerinde takip ediniz.",
          $"kerem@hemosoft.com", "Yeni Bir Hediye İsteği Var!", true);
        if (mail)
        {
          SuccessNotification("Mail Gönderildi");
        }
        else
        {
          ErrorNotification("Mail Gönderilemedi!");
        }
      }

      SuccessNotification("Tebrikler! Hediye Talebiniz İletildi. Lütfen Talebinizin Durumunu Hediye Takip Ekranından Kontrol Ediniz");
      return RedirectToAction("Index", "BuyUser");
    }
  }
}