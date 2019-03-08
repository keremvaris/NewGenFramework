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
using NewGenFramework.Core.Aspects.Postsharp.TransactionAspects;
using NewGenFramework.Core.Utilities.Common;

namespace BayiPuan.MvcWebUi.Controllers
{
  [AuthorizationFilter]
  public class SaleController : BaseController
  {
    private readonly ISaleService _saleService;
    private readonly IUserService _userService;
    private readonly IProductService _productService;
    private readonly IQueryableRepository<Sale> _queryableRepository;
    private readonly IQueryableRepository<Product> _productQueryableRepository;
    private readonly IQueryableRepository<vwRP_StockCount> _totalRowsRepository;
    private readonly IQueryableRepository<User> _userQueryableRepository;
    private readonly IScoreService _scoreService;
    public SaleController(ISaleService saleService, IQueryableRepository<Sale> queryableRepository, IQueryableRepository<vwRP_StockCount> totalRowsRepository, IUserService userService, IProductService productService, IQueryableRepository<Product> productQueryableRepository, IQueryableRepository<User> userQueryableRepository, IScoreService scoreService)
    {
      _saleService = saleService;
      _queryableRepository = queryableRepository;
      _totalRowsRepository = totalRowsRepository;
      _userService = userService;
      _productService = productService;
      _productQueryableRepository = productQueryableRepository;
      _userQueryableRepository = userQueryableRepository;
      _scoreService = scoreService;
    }
    // GET: List
    [SecuredOperation(Roles = "SystemAdmin,Admin,User")]
    public ActionResult SaleIndex(Int32? page, Int32? rows)
    {
      var getUserId = _userService.UniqueUserName(User.Identity.Name);
      IGrid<Sale> col = new Grid<Sale>(_queryableRepository.Table.Include("Product").Include("Customer").Include("User").Where(x=>x.UserId==getUserId.UserId && x.IsApproved==false).OrderByDescending(x => x.SaleId).Skip((page - 1 ?? 0) * (rows ?? 10)).Take(rows ?? 10).AsNoTracking());
      col.Query = new NameValueCollection(Request.QueryString);

      if (col.Query != null)
      {
        col = new Grid<Sale>(_queryableRepository.Table.Include("Product").Include("Customer").Include("User").Where(x => x.UserId == getUserId.UserId && x.IsApproved == false).OrderByDescending(x => x.SaleId).AsNoTracking());
      }
      //col.Columns.Add(x => "<a class=' fas fa-edit btn btn-warning btn-sm' title='Güncelle' href='Edit/" + x.SaleId + "'> </a>" +
      //                     "<a class='actions fas fa-trash-alt btn btn-danger btn-sm' title='Sil' href='Delete/" + x.SaleId + "'> </a>")
      //    .Encoded(false).Titled("işlemler").Filterable(false);
      //Görüntülenecek kolonları buraya yazacaksanız
      col.Columns.Add(x => x.InvoiceNo).Titled("Fatura No");
      col.Columns.Add(x => x.InvoiceImage).Titled("Fatura Görseli").Encoded(false).RenderedAs(x => x.InvoiceImage != null ? "<img class=\"aw-zoom\"  src='" + String.Format("data:image/gif;base64,{0}", Convert.ToBase64String(x.InvoiceImage)) + "' width=\"80\" height=\"80\" />" : "<img src='../images/indir.gif' ' width=\"180\" height=\"180\" />");
      col.Columns.Add(x => x.Product.ProductName).Titled("Ürün");
      col.Columns.Add(x => x.AmountOfSales).Titled("Miktar");
      col.Columns.Add(x => x.InvoiceTotal).Titled("Fatura Tutarı");
      col.Columns.Add(x => x.Customer.CustomerName).Titled("Müşteri");

      col.Columns.Add(x => x.InvoiceDate).Titled("Fatura Tarihi");
      col.Columns.Add(x => x.User.UserName).Titled("Satıcı");
      col.Columns.Add(x => x.Reason).Titled("Red Gerekçesi");

      col.Pager = new GridPager<Sale>(col);
      col.Processors.Add(col.Pager);
      col.Pager.RowsPerPage = 10;
      col.EmptyText = "Gösterilecek Kayıt Yok :(";
      
      foreach (IGridColumn column in col.Columns)
      {
        column.IsFilterable = true;
        column.IsSortable = true;
      }
      var total = _totalRowsRepository.Table.AsNoTracking().Where(x => x.TableName == "Sales").Select(x => x.TableRows).First();
      ViewBag.totalRows = Convert.ToInt32(total);
      return View(col);
    }

    [SecuredOperation(Roles = "SystemAdmin,Admin")]
    public ActionResult ApprovedSaleIndex(Int32? page, Int32? rows)
    {
      IGrid<Sale> col = new Grid<Sale>(_queryableRepository.Table.Include("Product").Include("Customer").Include("User").Where(x=>x.IsApproved==false && x.NotApproved!=true).OrderByDescending(x => x.SaleId).Skip((page - 1 ?? 0) * (rows ?? 10)).Take(rows ?? 10).AsNoTracking());
      col.Query = new NameValueCollection(Request.QueryString);

      if (col.Query != null)
      {
        col = new Grid<Sale>(_queryableRepository.Table.Include("Product").Include("Customer").Include("User").Where(x => x.IsApproved == false && x.NotApproved != true).OrderByDescending(x => x.SaleId).AsNoTracking());
      }
      col.Columns.Add(x => "<a class=' fa fa-thumbs-up btn btn-warning btn-sm' title='Satış Onayla' href='Edit/" + x.SaleId + "'> </a>" +
                           "<a class='actions fa fa-ban btn btn-danger btn-sm' title='Satışı Reddet' href='DeleteEdit/" + x.SaleId + "'> </a>")
          .Encoded(false).Titled("işlemler").Filterable(false);
      //Görüntülenecek kolonları buraya yazacaksanız
      col.Columns.Add(x => x.InvoiceNo).Titled("Fatura No");
      col.Columns.Add(x => x.InvoiceImage).Titled("Fatura Görseli").Encoded(false).RenderedAs(x => x.InvoiceImage != null ? "<img class=\"aw-zoom\"  src='" + String.Format("data:image/gif;base64,{0}", Convert.ToBase64String(x.InvoiceImage)) + "' width=\"180\" height=\"180\" />" : "<img src='../images/indir.gif' ' width=\"180\" height=\"180\" />");
      col.Columns.Add(x => x.Product.ProductName).Titled("Ürün");
      col.Columns.Add(x => x.AmountOfSales).Titled("Miktar");
      col.Columns.Add(x => x.InvoiceTotal).Titled("Fatura Tutarı");
      col.Columns.Add(x => x.Customer.CustomerName).Titled("Müşteri");
      col.Columns.Add(x => x.InvoiceDate).Titled("Fatura Tarihi");
      col.Columns.Add(x => x.User.UserName).Titled("Satıcı");

      col.Pager = new GridPager<Sale>(col);
      col.Processors.Add(col.Pager);
      col.Pager.RowsPerPage = 10;
      col.EmptyText = "Gösterilecek Kayıt Yok :(";
      foreach (IGridColumn column in col.Columns)
      {
        column.IsFilterable = true;
        column.IsSortable = true;
      }
      var total = _totalRowsRepository.Table.Where(x => x.TableName == "Sales").AsNoTracking().Select(x => x.TableRows).First();
      ViewBag.totalRows = Convert.ToInt32(total);
      return View(col);
    }
    // GET: Create
    [SecuredOperation(Roles = "SystemAdmin,Admin,User")]
    public ActionResult Create()
    {
      var empty = new SaleViewModel();
      var data = empty.ToVM();
      return View(data);
    }
    // POST: Brand/Create
    [HttpPost]
    [TransactionScopeAspect]
    public ActionResult Create(SaleViewModel sale)
    {
      var getUserId =Convert.ToInt32(GeneralHelpers.GetUserId()) ;
      if (!ModelState.IsValid)
      {
        ErrorNotification("Satış Eklenemedi!");
        return RedirectToAction("Create");
      }
      //satış yapılınca stok miktarından otomatik olarak düşer onay ya da onaylanmama durumuna göre stoklar güncellenir
      //Eğer satılacak ürün stokta varsa ve kalan stok miktarı satılandan büyükse hata döner işlemi yapmaz
      var product = _productQueryableRepository.Table.AsNoTracking().FirstOrDefault(x => x.ProductId == sale.ProductId);
      var userMail = _userQueryableRepository.Table.AsNoTracking().FirstOrDefault(x => x.UserId ==getUserId);
      if (product == null || product.RemainingStockAmount < sale.AmountOfSales)
      {
        ErrorNotification($"Satış Gerçekleşemedi Sattığınız Ürün Miktarı Satabileceğinizden Fazla Gözüküyor! <br /> Satabileceğiniz Ürün Stoğu Miktarı:<strong> {product.RemainingStockAmount.ToString()}</strong>");
        return RedirectToAction("Create");
      }
      _saleService.Add(new Sale
      {
        UserId = getUserId,
        InvoiceImage = sale.InvoiceImage,
        InvoiceDate = sale.InvoiceDate,
        AddDate = DateTime.Now,
        AmountOfSales = sale.AmountOfSales,
        InvoiceImageExt = ".png",
        InvoiceNo = sale.InvoiceNo,
        ProductId = sale.ProductId,
        CustomerId = sale.CustomerId,
        InvoiceTotal = sale.InvoiceTotal
      });
     
      _productService.Update(new Product
      {
        ProductName = product.ProductName,
        CriticalStockAmount = product.CriticalStockAmount,
        Point = product.Point,
        PointToMoney = product.PointToMoney,
        ProductCode = product.ProductCode,
        ProductShortCode = product.ProductShortCode,
        StockAmount = product.StockAmount,
        UnitTypeId = product.UnitTypeId,
        RemainingStockAmount = product.RemainingStockAmount - sale.AmountOfSales,
        ProductId = sale.ProductId
      });
      var mailEnable = System.Configuration.ConfigurationManager.AppSettings["MailEnable"];
      if (mailEnable == "true")
      {
        var mail = MailHelper.SendMail(
          $"{userMail.FirstName + " " + userMail.LastName} Tarafından {sale.InvoiceNo} Fatura Numarasıyla Yeni Bir Satış Gerçekleştirildi.<br/> Lütfen bayipuan.com üzerinde takip ediniz.",
          $"kerem@hemosoft.com", "Yeni Bir Satış Yapıldı!", true);
        if (mail)
        {
          SuccessNotification("Mail Gönderildi");
        }
        else
        {
          ErrorNotification("Mail Gönderilemedi!");
        }
      }

      SuccessNotification("Kayıt Eklendi. Yönetici Onayından Sonra Puan Kazanacaksınız!");
      return RedirectToAction("SaleIndex");
    }
    // GET: Edit
    [SecuredOperation(Roles = "SystemAdmin,Admin")]
    public ActionResult Edit(int id)
    {
      var data = AutoMapperHelper.MapToSameViewModel<Sale, ApproveSaleViewModel>(_saleService.GetById(id));
      return View(data.ToVM());
    }
    // POST: Edit
    [HttpPost]
    public ActionResult Edit(ApproveSaleViewModel sale)
    {
      var saleDefault = _queryableRepository.Table.Include("Product").Include("Customer").Include("User").FirstOrDefault(x=>x.SaleId==sale.SaleId);
      if (!ModelState.IsValid)
      {
        ErrorNotification("Bir Hata Oluştu");
        return RedirectToAction("DeleteEdit");
      }
      var saleImage = _saleService.GetById(sale.SaleId);
      var userMail = _userQueryableRepository.Table.AsNoTracking().FirstOrDefault(x => x.UserId == sale.UserId);
      try
      {
        if (sale.IsApproved==false)
        {
          ErrorNotification("Satışı Onaylamadınız! Başka Bir İşlem Yapmak İster misiniz?");
          return Redirect(Request.UrlReferrer.ToString());
        }
          // TODO: Add update logic here
          if (saleImage.InvoiceImage != null)
          {
            _saleService.Update(new Sale
            {
              UserId = sale.UserId,
              InvoiceImage = saleImage.InvoiceImage,
              InvoiceDate = sale.InvoiceDate,
              AddDate = saleImage.AddDate,
              AmountOfSales = sale.AmountOfSales,
              InvoiceImageExt = ".png",
              InvoiceNo = sale.InvoiceNo,
              ProductId = sale.ProductId,
              CustomerId = sale.CustomerId,
              SaleId = sale.SaleId,
              IsApproved = sale.IsApproved,
              ApprovedDate = DateTime.Now,
              InvoiceTotal = sale.InvoiceTotal

            });
            _scoreService.Add(new Score
            {
              ScoreTotal = sale.AmountOfSales * saleDefault.Product.Point,
              ScoreType = ScoreType.UrunModulu,
              UserId = sale.UserId,
              ScoreDate = DateTime.Now
            });
            var mailEnable = System.Configuration.ConfigurationManager.AppSettings["MailEnable"];
            if (mailEnable == "true")
            {
              var mail = MailHelper.SendMail(
                $"Tebrikler {sale.InvoiceNo} Fatura Numaralı Satışınız Onaylandı.<br/> Lütfen bayipuan.com üzerinde takip ediniz.",
                $"{userMail.Email}", "Satış Onaylanmadı!", true);
              if (mail)
              {
                SuccessNotification("Mail Gönderildi");
              }
              else
              {
                ErrorNotification("Mail Gönderilemedi!");
              }
            }
            SuccessNotification("Satış Onaylandı :)");
            return RedirectToAction("SaleIndex");
          }
        _saleService.Update(new Sale
        {
          UserId = sale.UserId,
          InvoiceImage = sale.InvoiceImage,
          InvoiceDate = sale.InvoiceDate,
          AddDate = saleImage.AddDate,
          AmountOfSales = sale.AmountOfSales,
          InvoiceImageExt = ".png",
          InvoiceNo = sale.InvoiceNo,
          ProductId = sale.ProductId,
          CustomerId = sale.CustomerId,
          SaleId = sale.SaleId,
          IsApproved = sale.IsApproved,
          ApprovedDate = DateTime.Now,
          InvoiceTotal = sale.InvoiceTotal
        });
        _scoreService.Add(new Score
        {
          ScoreTotal = sale.AmountOfSales * saleDefault.Product.Point,
          ScoreType = ScoreType.UrunModulu,
          UserId = sale.UserId,
          ScoreDate = DateTime.Now
        });
        var mailEnablex = System.Configuration.ConfigurationManager.AppSettings["MailEnable"];
        if (mailEnablex == "true")
        {
          var onayMail =
            MailHelper.SendMail(
              $"Tebrikler {sale.InvoiceNo} Fatura Numaralı Satışınız Onaylandı.<br/> Lütfen bayipuan.com üzerinde takip ediniz.",
              $"{userMail.Email}", "Satış Onaylanmadı!", true);
          if (onayMail)
          {
            SuccessNotification("Mail Gönderildi");
          }
          else
          {
            ErrorNotification("Mail Gönderilemedi!");
          }
        }

        SuccessNotification("Satış Onaylandı :)");
        return RedirectToAction("SaleIndex");
      }
      catch
      {
        return View();
      }
    }
    // GET: Delete
    [SecuredOperation(Roles = "SystemAdmin,Admin")]
    public ActionResult DeleteEdit(int id)
    {
      var data = AutoMapperHelper.MapToSameViewModel<Sale, NotApproveSaleViewModel>(_saleService.GetById(id));
      return View(data.ToVM());
    }
    // POST: Delete
    [HttpPost]
    [TransactionScopeAspect]
    public ActionResult DeleteEdit(NotApproveSaleViewModel sale)
    {
      if (!ModelState.IsValid)
      {
        ErrorNotification("Bir Hata Oluştu");
        return RedirectToAction("DeleteEdit");
      }
      var product = _productQueryableRepository.Table.AsNoTracking().FirstOrDefault(x => x.ProductId == sale.ProductId);
      var saleImage = _saleService.GetById(sale.SaleId);
      var userMail = _userQueryableRepository.Table.AsNoTracking().FirstOrDefault(x => x.UserId == sale.UserId);
      try
      {
        // TODO: Add update logic here
        if (saleImage.InvoiceImage != null)
        {
          _saleService.Update(new Sale
          {
            UserId = sale.UserId,
            InvoiceImage = saleImage.InvoiceImage,
            InvoiceDate = saleImage.InvoiceDate,
            AddDate = saleImage.AddDate,
            AmountOfSales = saleImage.AmountOfSales,
            InvoiceImageExt = ".png",
            InvoiceNo = saleImage.InvoiceNo,
            ProductId = saleImage.ProductId,
            CustomerId = saleImage.CustomerId,
            SaleId = sale.SaleId,
            IsApproved = false,
            ApprovedDate = null,
            NotApproved = true,
            NotApprovedDate = DateTime.Now,
            Reason = sale.Reason,
            InvoiceTotal = sale.InvoiceTotal

          });
          _productService.Update(new Product
          {
            ProductName = product.ProductName,
            
            CriticalStockAmount = product.CriticalStockAmount,
            Point = product.Point,
            PointToMoney = product.PointToMoney,
            ProductCode = product.ProductCode,
            ProductShortCode = product.ProductShortCode,
            StockAmount = product.StockAmount,
            UnitTypeId = product.UnitTypeId,
            RemainingStockAmount = product.RemainingStockAmount + sale.AmountOfSales,
            ProductId = sale.ProductId
          });
          var mailEnable = System.Configuration.ConfigurationManager.AppSettings["MailEnable"];
          if (mailEnable == "true")
          {
            var mail = MailHelper.SendMail(
              $"{sale.InvoiceNo} Fatura Numaralı Satışınız <strong>{sale.Reason}</strong> Gerekçesiyle Onaylanmadı.<br/> Lütfen bayipuan.com üzerinde takip ediniz.",
              $"{userMail.Email}", "Satış Onaylanmadı!", true);
            if (mail)
            {
              SuccessNotification("Mail Gönderildi");
            }
            else
            {
              ErrorNotification("Mail Gönderilemedi!");
            }
          }

          ErrorNotification("Satış Reddedildi! Ürün Stoğu Güncellendi");
          return RedirectToAction("SaleIndex");
        }
        _saleService.Update(new Sale
        {
          UserId = sale.UserId,
          InvoiceImage =  null,
          InvoiceDate = saleImage.InvoiceDate,
          AddDate = saleImage.AddDate,
          AmountOfSales = saleImage.AmountOfSales,
          InvoiceImageExt = ".png",
          InvoiceNo = saleImage.InvoiceNo,
          ProductId = saleImage.ProductId,
          CustomerId = saleImage.CustomerId,
          SaleId = sale.SaleId,
          IsApproved = false,
          ApprovedDate = null,
          NotApproved = true,
          InvoiceTotal = sale.InvoiceTotal,
          NotApprovedDate = DateTime.Now,
          Reason = sale.Reason
        });
        _productService.Update(new Product
        {
          ProductName = product.ProductName,
          CriticalStockAmount = product.CriticalStockAmount,
          Point = product.Point,
          PointToMoney = product.PointToMoney,
          ProductCode = product.ProductCode,
          ProductShortCode = product.ProductShortCode,
          StockAmount = product.StockAmount,
          UnitTypeId = product.UnitTypeId,
          RemainingStockAmount = product.RemainingStockAmount + sale.AmountOfSales,
          ProductId = sale.ProductId
        });
        var mailEnablex = System.Configuration.ConfigurationManager.AppSettings["MailEnable"];
        if (mailEnablex == "true")
        {
          var redmail =
            MailHelper.SendMail(
              $"{sale.InvoiceNo} Fatura Numaralı Satışınız <strong>{sale.Reason}</strong> Gerekçesiyle Onaylanmadı.<br/> Lütfen bayipuan.com üzerinde takip ediniz.",
              $"{userMail.Email}", "Satış Onaylanmadı!", true);
          if (redmail)
          {
            SuccessNotification("Mail Gönderildi");
          }
          else
          {
            ErrorNotification("Mail Gönderilemedi!");
          }
        }

        ErrorNotification("Satış Reddedildi! Ürün Stoğu Güncellendi");
        return RedirectToAction("SaleIndex");
      }
      catch (Exception ex)
      {
        return View(ex.ToString());
      }
    }
  }
}