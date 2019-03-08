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
  public class ProductController : BaseController
  {
    private readonly IProductService _productService;
    private readonly IQueryableRepository<Product> _queryableRepository;
    private readonly IQueryableRepository<vwRP_StockCount> _totalRowsRepository;
    public ProductController(IProductService productService, IQueryableRepository<Product> queryableRepository, IQueryableRepository<vwRP_StockCount> totalRowsRepository)
    {
      _productService = productService;
      _queryableRepository = queryableRepository;
      _totalRowsRepository = totalRowsRepository;
    }

    // GET: List
    [SecuredOperation(Roles = "SystemAdmin,Admin")]
    public ActionResult ProductIndex(Int32? page, Int32? rows)
    {
      IGrid<Product> col = new Grid<Product>(_queryableRepository.Table.Include("UnitType").OrderByDescending(x => x.ProductId).Skip((page - 1 ?? 0) * (rows ?? 10)).Take(rows ?? 10).AsNoTracking());
      col.Query = new NameValueCollection(Request.QueryString);

      if (col.Query != null)
      {
        col = new Grid<Product>(_queryableRepository.Table.Include("UnitType").OrderByDescending(x => x.ProductId).AsNoTracking());
      }
      col.Columns.Add(x => "<a class=' fas fa-edit btn btn-warning btn-sm' title='Güncelle' href='Edit/" + x.ProductId + "'> </a>" +
                           "<a class='actions fas fa-trash-alt btn btn-danger btn-sm' title='Sil' href='Delete/" + x.ProductId + "'> </a>")
          .Encoded(false).Titled("işlemler").Filterable(false);
      //Görüntülenecek kolonları buraya yazacaksanız
      col.Columns.Add(x => x.ProductId).Titled("Ürün No").MultiFilterable(true);
      col.Columns.Add(x => x.ProductName).Titled("Ürün Adı");
      col.Columns.Add(x => x.ProductCode).Titled("Ürün Kodu");
      col.Columns.Add(x => x.ProductShortCode).Titled("Ürün Kısa Kodu");
      col.Columns.Add(x => x.UnitType.UnitTypeName).Titled("Ürün Birimi");
      col.Columns.Add(x => x.StockAmount).Titled("Eklenen Stok Miktarı");
      col.Columns.Add(x => x.RemainingStockAmount).Titled("Kalan Stok Miktarı");
      col.Columns.Add(x => x.Point).Titled("Ürünün Puan Karşılığı");
      col.Columns.Add(x => x.PointToMoney).Titled("Puanın TL Karşılığı");
      col.Columns.Add(x => x.CriticalStockAmount).Titled("Kritik Stok Seviyesi");

      col.Pager = new GridPager<Product>(col);
      col.Processors.Add(col.Pager);
      col.Pager.RowsPerPage = 10;
      col.EmptyText = "Gösterilecek Kayıt Yok :(";
      foreach (IGridColumn column in col.Columns)
      {
        column.IsFilterable = true;
        column.IsSortable = true;
      }
      var total = _totalRowsRepository.Table.AsNoTracking().Where(x => x.TableName == "Products").Select(x => x.TableRows).First();
      ViewBag.totalRows = Convert.ToInt32(total);
      return View(col);
    }
    // GET: Create
    [SecuredOperation(Roles = "SystemAdmin,Admin")]
    public ActionResult Create()
    {
      var empty = new ProductViewModel();
      var data = empty.ToVM();
      return View(data);
    }
    // POST: Brand/Create
    [HttpPost]
    public ActionResult Create(ProductViewModel product)
    {
      if (!ModelState.IsValid)
      {
        ErrorNotification("Kayıt Eklenemedi!");
        return RedirectToAction("Create");
      }
      _productService.Add(new Product
      {
        
        Point = product.Point,
        ProductCode = product.ProductCode,
        RemainingStockAmount = product.RemainingStockAmount,
        ProductName = product.ProductName,
        UnitTypeId = product.UnitTypeId,
        PointToMoney = product.PointToMoney,
        StockAmount = product.StockAmount,
        CriticalStockAmount = product.CriticalStockAmount,
        ProductShortCode = product.ProductShortCode

      });
      SuccessNotification("Kayıt Eklendi.");
      return RedirectToAction("ProductIndex");
    }
    // GET: Edit
    [SecuredOperation(Roles = "SystemAdmin,Admin")]
    public ActionResult Edit(int id)
    {
      var data = AutoMapperHelper.MapToSameViewModel<Product, ProductViewModel>(_productService.GetById(id));
      return View(data.ToVM());
    }
    // POST: Edit
    [HttpPost]
    public ActionResult Edit(Product product)
    {
      try
      {
        // TODO: Add update logic here
        _productService.Update(new Product
        {
          
          Point = product.Point,
          ProductCode = product.ProductCode,
          RemainingStockAmount = product.RemainingStockAmount,
          ProductName = product.ProductName,
          UnitTypeId = product.UnitTypeId,
          PointToMoney = product.PointToMoney,
          StockAmount = product.StockAmount,
          CriticalStockAmount = product.CriticalStockAmount,
          ProductShortCode = product.ProductShortCode,
          ProductId = product.ProductId
        });
        SuccessNotification("Kayıt Güncellendi");
        return RedirectToAction("ProductIndex");
      }
      catch
      {
        return View();
      }
    }
    // GET: Delete
    [SecuredOperation(Roles = "SystemAdmin")]
    public ActionResult Delete(int id, Product product)
    {
      var data = AutoMapperHelper.MapToSameViewModel<Product, ProductViewModel>(_productService.GetById(id));
      return View(data.ToVM());
    }
    // POST: Delete
    [HttpPost]
    public ActionResult Delete(int id)
    {
      try
      {
        _productService.Delete(_productService.GetById(id));
        SuccessNotification("Kayıt Silindi");
        return RedirectToAction("ProductIndex");
      }
      catch
      {
        return View();
      }
    }
  }
}