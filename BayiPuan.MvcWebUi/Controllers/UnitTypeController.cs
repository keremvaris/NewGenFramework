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
  public class UnitTypeController : BaseController
  {
    private readonly IUnitTypeService _unitTypeService;
    private readonly IQueryableRepository<UnitType> _queryableRepository;
    private readonly IQueryableRepository<vwRP_StockCount> _totalRowsRepository;
    public UnitTypeController(IUnitTypeService unitTypeService, IQueryableRepository<UnitType> queryableRepository, IQueryableRepository<vwRP_StockCount> totalRowsRepository)
    {
      _unitTypeService = unitTypeService;
      _queryableRepository = queryableRepository;
      _totalRowsRepository = totalRowsRepository;
    }
    [SecuredOperation(Roles = "SystemAdmin,Admin")]
    public ActionResult UnitTypeIndex(Int32? page, Int32? rows)
    {
      IGrid<UnitType> col = new Grid<UnitType>(_queryableRepository.Table.OrderByDescending(x => x.UnitTypeId).Skip((page - 1 ?? 0) * (rows ?? 10)).Take(rows ?? 10).AsNoTracking());
      col.Query = new NameValueCollection(Request.QueryString);

      if (col.Query != null)
      {
        col = new Grid<UnitType>(_queryableRepository.Table.OrderByDescending(x => x.UnitTypeId).AsNoTracking());
      }
      col.Columns.Add(x => "<a class=' fas fa-edit btn btn-warning btn-sm' title='Güncelle' href='Edit/" + x.UnitTypeId + "'> </a>" +
                           "<a class='actions fas fa-trash-alt btn btn-danger btn-sm' title='Sil' href='Delete/" + x.UnitTypeId + "'> </a>")
          .Encoded(false).Titled("işlemler").Filterable(false);
      //Görüntülenecek kolonları buraya yazacaksanız
      col.Columns.Add(x => x.UnitTypeId).Titled("Birim No");
      col.Columns.Add(x => x.UnitTypeName).Titled("Birim Adı");

      col.Pager = new GridPager<UnitType>(col);
      col.Processors.Add(col.Pager);
      col.Pager.RowsPerPage = 10;
      col.EmptyText = "Gösterilecek Kayıt Yok :(";
      foreach (IGridColumn column in col.Columns)
      {
        column.IsFilterable = true;
        column.IsSortable = true;
      }
      var total = _totalRowsRepository.Table.Where(x => x.TableName == "UnitTypes").AsNoTracking().Select(x => x.TableRows).First();
      ViewBag.totalRows = Convert.ToInt32(total);
      return View(col);
    }
    // GET: Create
    [SecuredOperation(Roles = "SystemAdmin,Admin")]
    public ActionResult Create()
    {
      var empty = new UnitTypeViewModel();
      var data = empty.ToVM();
      return View(data);
    }
    // POST: Brand/Create
    [HttpPost]
    public ActionResult Create(UnitTypeViewModel unitType)
    {
      if (!ModelState.IsValid)
      {
        ErrorNotification("Kayıt Eklenemedi!");
        return RedirectToAction("Create");
      }
      _unitTypeService.Add(new UnitType
      {
        UnitTypeName = unitType.UnitTypeName

      });
      SuccessNotification("Kayıt Eklendi.");
      return RedirectToAction("UnitTypeIndex");
    }
    // GET: Edit
    [SecuredOperation(Roles = "SystemAdmin,Admin")]
    public ActionResult Edit(int id)
    {
      var data = AutoMapperHelper.MapToSameViewModel<UnitType, UnitTypeViewModel>(_unitTypeService.GetById(id));
      return View(data.ToVM());
    }
    // POST: Edit
    [HttpPost]
    public ActionResult Edit(UnitType unitType)
    {
      try
      {
        // TODO: Add update logic here
        _unitTypeService.Update(new UnitType
        {
          UnitTypeName = unitType.UnitTypeName,
          UnitTypeId = unitType.UnitTypeId
        });
        SuccessNotification("Kayıt Güncellendi");
        return RedirectToAction("UnitTypeIndex");
      }
      catch
      {
        return View();
      }
    }
    // GET: Delete
    [SecuredOperation(Roles = "SystemAdmin")]
    public ActionResult Delete(int id, UnitType unitType)
    {
      var data = AutoMapperHelper.MapToSameViewModel<UnitType, UnitTypeViewModel>(_unitTypeService.GetById(id));
      return View(data.ToVM());
    }
    // POST: Delete
    [HttpPost]
    public ActionResult Delete(int id)
    {
      try
      {
        _unitTypeService.Delete(_unitTypeService.GetById(id));
        SuccessNotification("Kayıt Silindi");
        return RedirectToAction("UnitTypeIndex");
      }
      catch
      {
        return View();
      }
    }
  }
}