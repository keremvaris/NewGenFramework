using System;
using System.Collections.Specialized;
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
  public class MyProductController : BaseController
  {
    private readonly IMyProductService _myProductService;
    private readonly IQueryableRepository<MyProduct> _queryableRepository;
    private readonly IQueryableRepository<vwRP_StockCount> _totalRowsRepository;
    public MyProductController(IMyProductService myProductService, IQueryableRepository<MyProduct> queryableRepository, IQueryableRepository<vwRP_StockCount> totalRowsRepository)
    {
      _myProductService = myProductService;
      _queryableRepository = queryableRepository;
      _totalRowsRepository = totalRowsRepository;
    }
    // GET: List
    [SecuredOperation(Roles = "SystemAdmin")]
    public ActionResult MyProductIndex(Int32? page, Int32? rows)
    {
      IGrid<MyProduct> col = new Grid<MyProduct>(_queryableRepository.Table.OrderByDescending(x => x.MyProductId).Skip((page - 1 ?? 0) * (rows ?? 10)).Take(rows ?? 10));
      col.Query = new NameValueCollection(Request.QueryString);

      if (col.Query != null)
      {
        col = new Grid<MyProduct>(_queryableRepository.Table.OrderByDescending(x => x.MyProductId));
      }
      col.Columns.Add(x => "<a class=' fas fa-edit btn btn-warning btn-sm' title='Güncelle' href='Edit/" + x.MyProductId + "'> </a>" +
                           "<a class='actions fas fa-trash-alt btn btn-danger btn-sm' title='Sil' href='Delete/" + x.MyProductId + "'> </a>")
          .Encoded(false).Titled("işlemler").Filterable(false);
      //Görüntülenecek kolonları buraya yazacaksanız

      col.Columns.Add(x => x.ProductName).Titled("Ürün Adı");
      col.Columns.Add(x => x.IsActive).Titled("Aktif mi?");

      col.Pager = new GridPager<MyProduct>(col);
      col.Processors.Add(col.Pager);
      col.Pager.RowsPerPage = 10;
      col.EmptyText = "Gösterilecek Kayıt Yok :(";
      foreach (IGridColumn column in col.Columns)
      {
        column.IsFilterable = true;
        column.IsSortable = true;
      }
      var total = _totalRowsRepository.Table.Where(x => x.TableName == "MyProducts").Select(x => x.TableRows).First();
      ViewBag.totalRows = Convert.ToInt32(total);
      return View(col);
    }
    [SecuredOperation(Roles = "SystemAdmin,Admin,User")]
    public ActionResult UserMyProductIndex(Int32? page, Int32? rows)
    {
      IGrid<MyProduct> col = new Grid<MyProduct>(_queryableRepository.Table.OrderByDescending(x => x.MyProductId).Skip((page - 1 ?? 0) * (rows ?? 10)).Take(rows ?? 10));
      col.Query = new NameValueCollection(Request.QueryString);

      if (col.Query != null)
      {
        col = new Grid<MyProduct>(_queryableRepository.Table.OrderByDescending(x => x.MyProductId));
      }
      col.Columns.Add(x => "<a class=' fas fa-edit btn btn-warning btn-sm' title='Güncelle' href='/MyProduct/Detail/" + x.MyProductId + "'> </a>")
        .Encoded(false).Titled("işlemler").Filterable(false);
      //Görüntülenecek kolonları buraya yazacaksanız
      col.Columns.Add(x => x.ProductName).Titled("Ürün Adı");
      col.Columns.Add(x => x.IsActive).Titled("Aktif mi?");

      col.Pager = new GridPager<MyProduct>(col);
      col.Processors.Add(col.Pager);
      col.Pager.RowsPerPage = 10;
      col.EmptyText = "Gösterilecek Kayıt Yok :(";
      foreach (IGridColumn column in col.Columns)
      {
        column.IsFilterable = true;
        column.IsSortable = true;
      }
      var total = _totalRowsRepository.Table.Where(x => x.TableName == "MyProducts").Select(x => x.TableRows).First();
      ViewBag.totalRows = Convert.ToInt32(total);
      return View(col);
    }

    public ActionResult Detail(int id)
    {
      var productDetail = _queryableRepository.Table.FirstOrDefault(x => x.MyProductId == id);
      return View(productDetail);
    }
    // GET: Create
    [SecuredOperation(Roles = "SystemAdmin")]
    public ActionResult Create()
    {
      var empty = new MyProductViewModel();
      var data = empty.ToVM();
      return View(data);
    }
    // POST: Brand/Create
    [HttpPost]
    public ActionResult Create(MyProductViewModel myProduct)
    {
      if (!ModelState.IsValid)
      {
        ErrorNotification("Kayıt Eklenemedi!");
        return RedirectToAction("Create");
      }
      _myProductService.Add(new MyProduct
      {
        ProductName = myProduct.ProductName,
        MyProductImage = myProduct.MyProductImage,
        MyProductImageExt = ".png",
        Description = myProduct.Description,
        IsActive = myProduct.IsActive

      });
      SuccessNotification("Kayıt Eklendi.");
      return RedirectToAction("MyProductIndex");
    }
    // GET: Edit
    [SecuredOperation(Roles = "SystemAdmin")]
    public ActionResult Edit(int id)
    {
      var data = AutoMapperHelper.MapToSameViewModel<MyProduct, MyProductViewModel>(_myProductService.GetById(id));
      return View(data.ToVM());
    }
    // POST: Edit
    [HttpPost]
    public ActionResult Edit(MyProduct myProduct)
    {
      try
      {
        _myProductService.Update(new MyProduct
        {
          //TODO:Alanlar buraya yazılacak Id alanı en altta olacak unutmayın!!!
          ProductName = myProduct.ProductName,
          MyProductImage = myProduct.MyProductImage,
          MyProductImageExt = ".png",
          Description = myProduct.Description,
          IsActive = myProduct.IsActive,
          MyProductId = myProduct.MyProductId
        });
        SuccessNotification("Kayıt Güncellendi");
        return RedirectToAction("MyProductIndex");
      }
      catch
      {
        return View();
      }
    }
    // GET: Delete
    [SecuredOperation(Roles = "SystemAdmin")]
    public ActionResult Delete(int id, MyProduct myProduct)
    {
      var data = AutoMapperHelper.MapToSameViewModel<MyProduct, MyProductViewModel>(_myProductService.GetById(id));
      return View(data.ToVM());
    }
    // POST: Delete
    [HttpPost]
    public ActionResult Delete(int id)
    {
      try
      {
        _myProductService.Delete(_myProductService.GetById(id));
        SuccessNotification("Kayıt Silindi");
        return RedirectToAction("MyProductIndex");
      }
      catch
      {
        return View();
      }
    }
  }
}