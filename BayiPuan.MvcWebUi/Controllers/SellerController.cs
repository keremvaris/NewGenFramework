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
using BayiPuan.MvcWebUi.Infrastructure;

using BayiPuan.MvcWebUi.Models.ViewModels;

namespace BayiPuan.MvcWebUi.Controllers
{
  [AuthorizationFilter]
  public class SellerController : BaseController
  {
    private readonly ISellerService _sellerService;
    private readonly IQueryableRepository<Seller> _queryableRepository;
    private readonly IQueryableRepository<vwRP_StockCount> _totalRowsRepository;
    public SellerController(ISellerService sellerService, IQueryableRepository<Seller> queryableRepository, IQueryableRepository<vwRP_StockCount> totalRowsRepository)
    {
      _sellerService = sellerService;
      _queryableRepository = queryableRepository;
      _totalRowsRepository = totalRowsRepository;
    }
    // GET: List
    [SecuredOperation(Roles = "SystemAdmin,Admin")]
    public ActionResult SellerIndex(Int32? page, Int32? rows)
    {
      IGrid<Seller> col = new Grid<Seller>(_queryableRepository.Table.Include("City").Include("UserType").OrderByDescending(x => x.SellerId).Skip((page - 1 ?? 0) * (rows ?? 10)).Take(rows ?? 10).AsNoTracking());
      col.Query = new NameValueCollection(Request.QueryString);

      if (col.Query != null)
      {
        col = new Grid<Seller>(_queryableRepository.Table.Include("City").Include("UserType").OrderByDescending(x => x.SellerId).AsNoTracking());
      }
      col.Columns.Add(x => "<a class=' fas fa-edit btn btn-warning btn-sm' title='Güncelle' href='Edit/" + x.SellerId + "'> </a>" +
                           "<a class='actions fas fa-trash-alt btn btn-danger btn-sm' title='Sil' href='Delete/" + x.SellerId + "'> </a>")
          .Encoded(false).Titled("işlemler").Filterable(false);
      //Görüntülenecek kolonları buraya yazacaksanız
      col.Columns.Add(x => x.UserType.UserTypeName).Titled("Bayi Tipi").MultiFilterable(true);
      col.Columns.Add(x => x.SellerName).Titled("Bayi Adı").MultiFilterable(true);
      col.Columns.Add(x => x.SellerCode).Titled("Bayi Kodu").MultiFilterable(true);
      col.Columns.Add(x => x.City.CityName).Titled("Şehir");
      
      


      col.Pager = new GridPager<Seller>(col);
      col.Processors.Add(col.Pager);
      col.Pager.RowsPerPage = 10;
      col.EmptyText = "Gösterilecek Kayıt Yok :(";
      foreach (IGridColumn column in col.Columns)
      {
        column.IsFilterable = true;
        column.IsSortable = true;
      }
      var total = _totalRowsRepository.Table.Where(x => x.TableName == "Sellers").AsNoTracking().Select(x => x.TableRows).First();
      ViewBag.totalRows = Convert.ToInt32(total);
      return View(col);
    }
    // GET: Create
    [SecuredOperation(Roles = "SystemAdmin,Admin")]
    public ActionResult Create()
    {
      var empty = new SellerViewModel();
      var data = empty.ToVM();
      return View(data);
    }
    // POST: Brand/Create
    [HttpPost]
    public ActionResult Create(SellerViewModel seller)
    {
      if (!ModelState.IsValid)
      {
        ErrorNotification("Kayıt Eklenemedi!");
        return RedirectToAction("Create");
      }
      _sellerService.Add(new Seller
      {
        CityId = seller.CityId,
        SellerCode = seller.SellerCode,
        SellerName = seller.SellerName,
        UserTypeId = seller.UserTypeId
        


      });
      SuccessNotification("Kayıt Eklendi.");
      return RedirectToAction("SellerIndex");
    }
    // GET: Edit
    [SecuredOperation(Roles = "SystemAdmin,Admin")]
    public ActionResult Edit(int id)
    {
      var data = AutoMapperHelper.MapToSameViewModel<Seller, SellerViewModel>(_sellerService.GetById(id));
      return View(data.ToVM());
    }
    // POST: Edit
    [HttpPost]
    public ActionResult Edit(Seller seller)
    {
      try
      {
        // TODO: Add update logic here
        _sellerService.Update(new Seller
        {
          CityId = seller.CityId,
          SellerCode = seller.SellerCode,
          SellerName = seller.SellerName,
          UserTypeId = seller.UserTypeId,
          SellerId = seller.SellerId
        });
        SuccessNotification("Kayıt Güncellendi");
        return RedirectToAction("SellerIndex");
      }
      catch
      {
        return View();
      }
    }
    // GET: Delete
    [SecuredOperation(Roles = "SystemAdmin")]
    public ActionResult Delete(int id, Seller seller)
    {
      var data = AutoMapperHelper.MapToSameViewModel<Seller, SellerViewModel>(_sellerService.GetById(id));
      return View(data.ToVM());
    }
    // POST: Delete
    [HttpPost]
    public ActionResult Delete(int id)
    {
      try
      {
        _sellerService.Delete(_sellerService.GetById(id));
        SuccessNotification("Kayıt Silindi");
        return RedirectToAction("SellerIndex");
      }
      catch
      {
        return View();
      }
    }
  }
}