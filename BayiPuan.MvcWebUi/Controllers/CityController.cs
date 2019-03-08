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
  public class CityController : BaseController
  {
    private readonly ICityService _cityService;
    private readonly IQueryableRepository<City> _queryableRepository;
    private readonly IQueryableRepository<vwRP_StockCount> _totalRowsRepository;
    public CityController(ICityService cityService, IQueryableRepository<City> queryableRepository, IQueryableRepository<vwRP_StockCount> totalRowsRepository)
    {
      _cityService = cityService;
      _queryableRepository = queryableRepository;
      _totalRowsRepository = totalRowsRepository;
    }
    
    // GET: List
    [SecuredOperation(Roles = "SystemAdmin,Admin")]
    public ActionResult CityIndex(Int32? page, Int32? rows)
    {
      IGrid<City> col = new Grid<City>(_queryableRepository.Table.OrderByDescending(x => x.CityId).Skip((page - 1 ?? 0) * (rows ?? 10)).Take(rows ?? 10).AsNoTracking());
      col.Query = new NameValueCollection(Request.QueryString);

      if (col.Query != null)
      {
        col = new Grid<City>(_queryableRepository.Table.OrderByDescending(x => x.CityId).AsNoTracking());
      }
      col.Columns.Add(x => "<a class=' fas fa-edit btn btn-warning btn-sm' title='Güncelle' href='Edit/" + x.CityId + "'> </a>" +
                           "<a class='actions fas fa-trash-alt btn btn-danger btn-sm' title='Sil' href='Delete/" + x.CityId + "'> </a>")
          .Encoded(false).Titled("işlemler").Filterable(false);
      //Görüntülenecek kolonları buraya yazacaksanız
      col.Columns.Add(x => x.CityId).Titled("Şehir No");
      col.Columns.Add(x => x.CityName).Titled("Şehir Adı");

      col.Pager = new GridPager<City>(col);
      col.Processors.Add(col.Pager);
      col.Pager.RowsPerPage = 10;
      col.EmptyText = "Gösterilecek Kayıt Yok :(";
      foreach (IGridColumn column in col.Columns)
      {
        column.IsFilterable = true;
        column.IsSortable = true;
      }
      var total = _totalRowsRepository.Table.AsNoTracking().Where(x => x.TableName == "Cities").Select(x => x.TableRows).First();
      ViewBag.totalRows = Convert.ToInt32(total);
      return View(col);
    }
    // GET: Create
    [SecuredOperation(Roles = "SystemAdmin")]
    public ActionResult Create()
    {
      var empty = new CityViewModel();
      var data = empty.ToVM();
      return View(data);
    }
    // POST: Brand/Create
    [HttpPost]
    public ActionResult Create(CityViewModel city)
    {
      if (!ModelState.IsValid)
      {
        ErrorNotification("Kayıt Eklenemedi!");
        return RedirectToAction("Create");
      }
      _cityService.Add(new City
      {
        CityName = city.CityName

      });
      SuccessNotification("Kayıt Eklendi.");
      return RedirectToAction("CityIndex");
    }
    // GET: Edit
    [SecuredOperation(Roles = "SystemAdmin")]
    public ActionResult Edit(int id)
    {
      var data = AutoMapperHelper.MapToSameViewModel<City, CityViewModel>(_cityService.GetById(id));
      return View(data.ToVM());
    }
    // POST: Edit
    [HttpPost]
    public ActionResult Edit(City city)
    {
      try
      {
        // TODO: Add update logic here
        _cityService.Update(new City
        {
          CityName = city.CityName,
          CityId = city.CityId
        });
        SuccessNotification("Kayıt Güncellendi");
        return RedirectToAction("CityIndex");
      }
      catch
      {
        return View();
      }
    }
    // GET: Delete
    [SecuredOperation(Roles = "SystemAdmin")]
    public ActionResult Delete(int id, City city)
    {
      var data = AutoMapperHelper.MapToSameViewModel<City, CityViewModel>(_cityService.GetById(id));
      return View(data.ToVM());
    }
    // POST: Delete
    [HttpPost]
    public ActionResult Delete(int id)
    {
      try
      {
        _cityService.Delete(_cityService.GetById(id));
        SuccessNotification("Kayıt Silindi");
        return RedirectToAction("CityIndex");
      }
      catch
      {
        return View();
      }
    }
  }
}