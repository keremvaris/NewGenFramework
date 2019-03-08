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
  public class BrandController : BaseController
  {
    private readonly IBrandService _brandService;
    private readonly IQueryableRepository<Brand> _queryableRepository;
    private readonly IQueryableRepository<vwRP_StockCount> _totalRowsRepository;
    public BrandController(IBrandService brandService, IQueryableRepository<Brand> queryableRepository, IQueryableRepository<vwRP_StockCount> totalRowsRepository)
    {
      _brandService = brandService;
      _queryableRepository = queryableRepository;
      _totalRowsRepository = totalRowsRepository;
    }
    // GET: List
    [SecuredOperation(Roles = "SystemAdmin,Admin")]
    public ActionResult BrandIndex(Int32? page, Int32? rows)
    {
      

      IGrid<Brand> col = new Grid<Brand>(_queryableRepository.Table.AsNoTracking().OrderByDescending(x => x.BrandId).Skip((page - 1 ?? 0) * (rows ?? 10)).Take(rows ?? 10));
      col.Query = new NameValueCollection(Request.QueryString);

      if (col.Query != null)
      {
        col = new Grid<Brand>(_queryableRepository.Table.OrderByDescending(x => x.BrandId).AsNoTracking());
      }
      col.Columns.Add(x => "<a class=' fas fa-edit btn btn-warning btn-sm' title='Güncelle' href='Edit/" + x.BrandId + "'> </a>" +
                           "<a class='actions fas fa-trash-alt btn btn-danger btn-sm' title='Sil' href='Delete/" + x.BrandId + "'> </a>")
          .Encoded(false).Titled("işlemler").Filterable(false);
      //Görüntülenecek kolonları buraya yazacaksanız

      col.Columns.Add(x => x.BrandId).Titled("Marka No").MultiFilterable(true);
      col.Columns.Add(x => x.BrandName).Titled("Marka Adı").MultiFilterable(true);
      col.Columns.Add(x => x.BrandImage).Titled("Marka Logo").Encoded(false).RenderedAs(x =>x.BrandImage!=null? "<img src='"+String.Format("data:image/gif;base64,{0}", Convert.ToBase64String(x.BrandImage))+ "' width=\"80\" height=\"80\" />": "../images/indir.gif") ;

      col.Pager = new GridPager<Brand>(col);
      col.Processors.Add(col.Pager);
      col.Pager.RowsPerPage = 10;
      col.EmptyText = "Gösterilecek Kayıt Yok :(";
      foreach (IGridColumn column in col.Columns)
      {
        column.IsFilterable = true;
        column.IsSortable = true;
      }
      var total = _totalRowsRepository.Table.AsNoTracking().Where(x => x.TableName == "Brands").Select(x => x.TableRows).First();
      ViewBag.totalRows = Convert.ToInt32(total);
      return View(col);
    }
    // GET: Create
    [SecuredOperation(Roles = "SystemAdmin,Admin")]
    public ActionResult Create()
    {
      var empty = new BrandViewModel();
      var data = empty.ToVM();
      return View(data);
    }
    // POST: Brand/Create
    [HttpPost]
    public ActionResult Create(BrandViewModel brand)
    {
      if (!ModelState.IsValid)
      {
        ErrorNotification("Kayıt Eklenemedi!");
        return RedirectToAction("Create");
      }
      _brandService.Add(new Brand
      {
        BrandName = brand.BrandName,
        BrandImage = brand.BrandImage,
        BrandImageExt = ".png"

      });
      SuccessNotification("Kayıt Eklendi.");
      return RedirectToAction("BrandIndex");
    }
    // GET: Edit
    [SecuredOperation(Roles = "SystemAdmin,Admin")]
    public ActionResult Edit(int id)
    {
      var data = AutoMapperHelper.MapToSameViewModel<Brand, BrandViewModel>(_brandService.GetById(id));
      return View(data.ToVM());
    }
    // POST: Edit
    [HttpPost]
    public ActionResult Edit(BrandViewModel brand)
    {
      try
      {
        // TODO: Add update logic here
        _brandService.Update(new Brand
        {
          BrandName = brand.BrandName,
          BrandImage = brand.BrandImage,
          BrandImageExt = ".png",
          BrandId = brand.BrandId
        });
        SuccessNotification("Kayıt Güncellendi");
        return RedirectToAction("BrandIndex");
      }
      catch
      {
        return View();
      }
    }
    // GET: Delete
    [SecuredOperation(Roles = "SystemAdmin")]
    public ActionResult Delete(int id, Brand brand)
    {
      var data = AutoMapperHelper.MapToSameViewModel<Brand, BrandViewModel>(_brandService.GetById(id));
      return View(data.ToVM());
    }
    // POST: Delete
    [HttpPost]
    public ActionResult Delete(int id)
    {
      try
      {
        _brandService.Delete(_brandService.GetById(id));
        SuccessNotification("Kayıt Silindi");
        return RedirectToAction("BrandIndex");
      }
      catch
      {
        return View();
      }
    }
  }
}