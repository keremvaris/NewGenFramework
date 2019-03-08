using System;
using System.Collections.Specialized;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using NewGenFramework.Core.Aspects.Postsharp.AuthorizationAspects;
using NewGenFramework.Core.DataAccess;
using NewGenFramework.Core.Utilities.Mappings;
using NonFactors.Mvc.Grid;
using BayiPuan.Business.Abstract;
using BayiPuan.Entities.ComplexTypes;
using BayiPuan.Entities.Concrete;
using BayiPuan.MvcWebUi.GenericVM;
using BayiPuan.MvcWebUi.Filters;
using BayiPuan.MvcWebUi.HtmlHelpers;
using BayiPuan.MvcWebUi.Infrastructure;
using BayiPuan.MvcWebUi.Models.ViewModels;
using NewGenFramework.Core.Utilities.Common;

namespace BayiPuan.MvcWebUi.Controllers
{
  [AuthorizationFilter]
  public class BuyController : BaseController
  {
    private readonly IBuyService _buyService;
    private readonly IQueryableRepository<Buy> _queryableRepository;
    private readonly IQueryableRepository<vwRP_StockCount> _totalRowsRepository;
    private readonly IQueryableRepository<Gift> _giftQueryableRepository;
    private readonly IQueryableRepository<User> _userQueryableRepository;

    public BuyController(IBuyService buyService, IQueryableRepository<Buy> queryableRepository, IQueryableRepository<vwRP_StockCount> totalRowsRepository, IQueryableRepository<Gift> giftQueryableRepository, IQueryableRepository<User> userQueryableRepository)
    {
      _buyService = buyService;
      _queryableRepository = queryableRepository;
      _totalRowsRepository = totalRowsRepository;
      _giftQueryableRepository = giftQueryableRepository;
      _userQueryableRepository = userQueryableRepository;
    }
    // GET: List
    [SecuredOperation(Roles = "SystemAdmin,Admin,User")]
    public ActionResult BuyIndex(Int32? page, Int32? rows)
    {
      var getUserId =Convert.ToInt32(GeneralHelpers.GetUserId());
      IGrid<Buy> col = new Grid<Buy>(_queryableRepository.Table.Include("Gift").Include("User").Include("Brand").Where(x=>x.UserId==getUserId).OrderByDescending(x => x.BuyId).Skip((page - 1 ?? 0) * (rows ?? 10)).Take(rows ?? 10).AsNoTracking());
      col.Query = new NameValueCollection(Request.QueryString);
      if (col.Query != null)
      {
        col = new Grid<Buy>(_queryableRepository.Table.Include("Gift").Include("User").Include("Brand").Where(x => x.UserId == getUserId).OrderByDescending(x => x.BuyId).AsNoTracking());
      }
      
      col.Columns.Add(x => x.Brand.BrandName).Titled("Marka");
      col.Columns.Add(x => x.Gift.Description).Titled("Hediye");
      col.Columns.Add(x => x.UserId).RenderedAs(x => x.User.FirstName + " " + x.User.LastName).Titled("Kullanıcı");
      col.Columns.Add(x => x.BuyAmount).Titled("Alınan Miktar");
      col.Columns.Add(x => x.BuyDate).Titled("İstek Tarihi");
      col.Columns.Add(x => x.IsApproved).Titled("Durumu").RenderedAs(x => x.IsApproved == true ? "İstek Yapıldı" : "Puan İadesi Yapıldı");
      col.Columns.Add(x => x.BuyState).Titled("Hediye Durumu").RenderedAs(x => x.BuyState == BuyState.IncelemeBekliyor ? "İnceleme Bekliyor" : x.BuyState == BuyState.OnaylandiGonderimBekliyor ? "Onaylandı, Gönderim Bekleniyor." :x.BuyState==BuyState.HediyeKarsilanamiyor? "Hediye Firmadan Karşılanamıyor" : "Gönderildi.");
      

      col.Pager = new GridPager<Buy>(col);
      col.Processors.Add(col.Pager);
      col.Pager.RowsPerPage = 10;
      col.EmptyText = "Gösterilecek Kayıt Yok :(";
      foreach (IGridColumn column in col.Columns)
      {
        column.IsFilterable = true;
        column.IsSortable = true;
      }
      var total = _totalRowsRepository.Table.Where(x => x.TableName == "Buys").AsNoTracking().Select(x => x.TableRows).First();
      ViewBag.totalRows = Convert.ToInt32(total);
      return View(col);
    }

    [SecuredOperation(Roles = "SystemAdmin,Admin")]
    public ActionResult ApprovedBuyIndex(Int32? page, Int32? rows)
    {
      IGrid<Buy> col = new Grid<Buy>(_queryableRepository.Table.Include("Gift").Include("User").Include("Brand").AsNoTracking().Where(x => x.BuyState != BuyState.Gonderildi && x.BuyState!=BuyState.HediyeKarsilanamiyor).OrderByDescending(x => x.BuyId).Skip((page - 1 ?? 0) * (rows ?? 10)).Take(rows ?? 10));
      col.Query = new NameValueCollection(Request.QueryString);

      if (col.Query != null)
      {
        col = new Grid<Buy>(_queryableRepository.Table.Include("Gift").Include("User").Include("Brand").AsNoTracking().Where(x => x.BuyState != BuyState.Gonderildi && x.BuyState != BuyState.HediyeKarsilanamiyor).OrderByDescending(x => x.BuyId));
      }
      col.Columns.Add(x => "<a class=' fas fa-edit btn btn-warning btn-sm' title='Güncelle' href='Edit/" + x.BuyId + "'> </a>")
        //                     "<a class='actions fas fa-trash-alt btn btn-danger btn-sm' title='Sil' href='Delete/" + x.BuyId + "'> </a>")
        .Encoded(false).Titled("işlemler").Filterable(false);
      //Görüntülenecek kolonları buraya yazacaksanız
      //col.Columns.Add(x => x.BuyId).Titled("BuyId").MultiFilterable(true);
      col.Columns.Add(x => x.Brand.BrandName).Titled("Marka");
      col.Columns.Add(x => x.Gift.Description).Titled("Hediye");
      col.Columns.Add(x => x.UserId).RenderedAs(x => x.User.FirstName + " " + x.User.LastName).Titled("Kullanıcı");
      col.Columns.Add(x => x.BuyAmount).Titled("Alınan Miktar");
      col.Columns.Add(x => x.BuyDate).Titled("İstek Tarihi");
      col.Columns.Add(x => x.IsApproved).Titled("Durumu").RenderedAs(x => x.IsApproved == true ? "İstek Yapıldı" : "Puan İadesi Yapıldı");
      col.Columns.Add(x => x.BuyState).Titled("Hediye Durumu").RenderedAs(x => x.BuyState == BuyState.IncelemeBekliyor ? "İnceleme Bekliyor" : x.BuyState == BuyState.OnaylandiGonderimBekliyor ? "Onaylandı, Gönderim Bekleniyor." : x.BuyState == BuyState.HediyeKarsilanamiyor ? "Hediye Firmadan Karşılanamıyor" : "Gönderildi.");
      col.Pager = new GridPager<Buy>(col);
      col.Processors.Add(col.Pager);
      col.Pager.RowsPerPage = 10;
      col.EmptyText = "Gösterilecek Kayıt Yok :(";
      foreach (IGridColumn column in col.Columns)
      {
        column.IsFilterable = true;
        column.IsSortable = true;
      }
      var total = _totalRowsRepository.Table.Where(x => x.TableName == "Buys").AsNoTracking().Select(x => x.TableRows).First();
      ViewBag.totalRows = Convert.ToInt32(total);
      return View(col);
    }
    // GET: Create
    [SecuredOperation(Roles = "SystemAdmin")]
    public ActionResult Create()
    {
      var empty = new BuyViewModel();
      var data = empty.ToVM();
      return View(data);
    }
    // POST: Brand/Create
    [HttpPost]
    public ActionResult Create(BuyViewModel buy)
    {
      if (!ModelState.IsValid)
      {
        ErrorNotification("Kayıt Eklenemedi!");
        return RedirectToAction("Create");
      }
      _buyService.Add(new Buy
      {
        //Alanlar buraya yazılacak
        //Örn:BrandName = brand.BrandName,
        
      });
 
      SuccessNotification("Kayıt Eklendi.");
      return RedirectToAction("BuyIndex");
    }
    // GET: Edit
    [SecuredOperation(Roles = "SystemAdmin,Admin")]
    public ActionResult Edit(int id)
    {
      var data = AutoMapperHelper.MapToSameViewModel<Buy, BuyViewModel>(_buyService.GetById(id));
      return View(data.ToVM());
    }
    // POST: Edit
    [HttpPost]
    public ActionResult Edit(BuyViewModel buy)
    {
      var brand = _giftQueryableRepository.Table.AsNoTracking().FirstOrDefault(x => x.GiftId == buy.GiftId);
      var userMail = _userQueryableRepository.Table.AsNoTracking().FirstOrDefault(x => x.UserId == buy.UserId);
      if (!ModelState.IsValid)
      {
        ErrorNotification("Bir Hata Oluştu");
        return RedirectToAction("BuyIndex");
      }
      try
      {
        if (buy.BuyState == BuyState.IncelemeBekliyor)
        {
          ErrorNotification("Hediye Talebini İnceleme Bekliyor Durumunda Bıraktınız! Başka Bir İşlem Yapmak İster misiniz?");
          return RedirectToAction("BuyIndex");
        }

        if (buy.BuyState == BuyState.HediyeKarsilanamiyor)
        {
          // TODO: Add update logic here
          _buyService.Update(new Buy
          {
            UserId = buy.UserId,
            ApprovedDate = DateTime.Now,
            BuyAmount = buy.BuyAmount,
            BuyDate = buy.BuyDate,
            EditUserId = Convert.ToInt32(GeneralHelpers.GetUserId()),
            GiftId = buy.GiftId,
            IsApproved = false,
            BuyState = buy.BuyState,
            NotApproved = true,
            NotApprovedDate = DateTime.Now,
            BrandId = brand.BrandId,
            Reason = "Hediye İsteği Firmadan Karşılanamıyor.Puanlar iade Edildi.",
            BuyId = buy.BuyId
          });

          var mailEnable = System.Configuration.ConfigurationManager.AppSettings["MailEnable"];
          if (mailEnable=="true")
          {
            var mail = MailHelper.SendMail($"Hediye İsteğiniz Karşılanamıyor, Harcadığınız Puanların İadesi Gerçekleştirildi.<br/>Durum için gerçekten çok üzgünüz. Lütfen bayipuan.com üzerinde takip ediniz.", $"{userMail.Email}", "Hediye İsteğinizle İlgili Üzücü Bir Gelişme Oldu!", true);
            if (mail)
            {
              SuccessNotification("Mail Gönderildi");
            }
            else
            {
              ErrorNotification("Mail Gönderilemedi!");
            }
          }
          
         
          ErrorNotification("Hediye Talebi Karşılanamıyor. Talep Reddedildi!!! <br/> Harcanan Puanların İadesi Gerçekleştirildi.");
          return RedirectToAction("BuyIndex");
        }
        _buyService.Update(new Buy
        {
          UserId = buy.UserId,
          ApprovedDate = DateTime.Now,
          BuyAmount = buy.BuyAmount,
          BuyDate = buy.BuyDate,
          EditUserId = Convert.ToInt32(GeneralHelpers.GetUserId()),
          GiftId = buy.GiftId,
          IsApproved = true,
          BuyState = buy.BuyState,
          BrandId = brand.BrandId,
          BuyId = buy.BuyId
        });
        var mailEnablex = System.Configuration.ConfigurationManager.AppSettings["MailEnable"];
        if (mailEnablex == "true")
        {
          var Onaymail =
            MailHelper.SendMail($"Tebrikler Hediye İsteğiniz Onaylandı. Lütfen bayipuan.com üzerinde takip ediniz.",
              $"{userMail.Email}", "Tebrikler Hediye İsteğiniz Onaylandı!", true);
          if (Onaymail)
          {
            SuccessNotification("Mail Gönderildi");
          }
          else
          {
            ErrorNotification("Mail Gönderilemedi!");
          }
        }

        SuccessNotification("Seçilen Hediye Onaylandı.");
        return RedirectToAction("BuyIndex");
      }
      catch (Exception ex)
      {
        return View(ex.ToString());
      }
    }
    // GET: Delete
    [SecuredOperation(Roles = "SystemAdmin")]
    public ActionResult Delete(int id, Buy buy)
    {
      var data = AutoMapperHelper.MapToSameViewModel<Buy, BuyViewModel>(_buyService.GetById(id));
      return View(data.ToVM());
    }
    // POST: Delete
    [HttpPost]
    public ActionResult Delete(int id)
    {
      try
      {
        _buyService.Delete(_buyService.GetById(id));
        SuccessNotification("Kayıt Silindi");
        return RedirectToAction("BuyIndex");
      }
      catch
      {
        return View();
      }
    }
  }
}