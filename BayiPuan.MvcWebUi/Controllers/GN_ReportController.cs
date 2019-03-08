using System;
using System.Collections.Specialized;
using System.Data.Entity;
using System.EnterpriseServices;
using System.Linq;
using System.Web.Mvc;
using BayiPuan.Business.Abstract;
using BayiPuan.Entities.ComplexTypes;
using BayiPuan.Entities.Concrete;
using BayiPuan.MvcWebUi.Filters;
using BayiPuan.MvcWebUi.GenericVM;
using BayiPuan.MvcWebUi.Infrastructure;
using BayiPuan.MvcWebUi.Models.ViewModels;
using NewGenFramework.Core.Aspects.Postsharp.AuthorizationAspects;
using NewGenFramework.Core.DataAccess;
using NewGenFramework.Core.Utilities.Mappings;
using Ninject.Activation;
using NonFactors.Mvc.Grid;



namespace BayiPuan.MVCWebUI.Controllers
{
  [AuthorizationFilter]
  public class GN_ReportController : BaseController
  {
    private readonly IGN_ReportService _reportService;
    private readonly IQueryableRepository<GN_Report> _queryableRepository;
    private readonly IQueryableRepository<vwRP_StockCount> _totalRowsRepository;


    public GN_ReportController(IGN_ReportService reportService, IQueryableRepository<GN_Report> queryableRepository, IQueryableRepository<vwRP_StockCount> totalRowsRepository)
    {
      _reportService = reportService;
      _queryableRepository = queryableRepository;
      _totalRowsRepository = totalRowsRepository;
    }

    // GET: GN_Report
    public ActionResult GN_ReportIndex(Int32? page, Int32? rows)
    {
      IGrid<GN_Report> col = new Grid<GN_Report>(_queryableRepository.Table.OrderByDescending(x => x.ReportId).Skip((page - 1 ?? 0) * (rows ?? 10)).Take(rows ?? 10).AsNoTracking());
      col.Query = new NameValueCollection(Request.QueryString);

      if (col.Query != null)
      {
        col = new Grid<GN_Report>(_queryableRepository.Table.OrderByDescending(x => x.ReportId).AsNoTracking());
      }
      col.Columns.Add(x => "<a class=' fas fa-edit btn btn-warning btn-sm' title='Güncelle' href='Edit/" + x.ReportId + "'> </a>" +
                           "<a class=' actions fas fa-trash-alt btn btn-danger btn-sm' title='Sil' href='Delete/" + x.ReportId + "'> </a>" +
                           "<a class=' actions fas fa-chart-line btn btn-success btn-sm' title='Rapor Al' href='../ReportViewer/Index?ReportId=" + x.ReportId + "&filterColumns=" + x.ReportFilter + "'> </a>").Css("tdwidth")
          .Encoded(false).Titled("işlemler").Filterable(false);
      col.Columns.Add(x => x.ReportTitle).Titled("Başlık").MultiFilterable(true);
      //col.Columns.Add(x => x.ReportSql).Titled("SQL Kodu");
      //col.Columns.Add(x => x.ReportFilter).Titled("Rapor Filtre");

      col.Pager = new GridPager<GN_Report>(col);
      col.Processors.Add(col.Pager);
      col.Pager.RowsPerPage = 10;
      col.EmptyText = "Gösterilecek Kayıt Yok :(";
      foreach (IGridColumn column in col.Columns)
      {
        column.IsFilterable = true;
        column.IsSortable = true;
      }
      var total = _totalRowsRepository.Table.AsNoTracking().Where(x => x.TableName == "GN_Reports").Select(x => x.TableRows).First();
      ViewBag.totalRows = Convert.ToInt32(total);
      return View(col);
    }

    [SecuredOperation(Roles = "SystemAdmin")]
    public ActionResult Create()
    {
      var empty = new GN_ReportViewModel();

      var data = empty.ToVM();

      return View(data);
    }
    [HttpPost]
    public ActionResult Create(GN_ReportViewModel report)
    {
      if (!ModelState.IsValid)
      {
        ErrorNotification("Marka Eklenemedi!");
        return RedirectToAction("Create");
      }
      _reportService.Add(new GN_Report
      {
        ReportTitle = report.ReportTitle,
        ReportFilter = report.ReportFilter,
        ReportSql = report.ReportSql,
        ReportAuthorization = "SystemAdmin," + report.ReportAuthorization
      });
      SuccessNotification("Kayıt Eklendi.");
      return RedirectToAction("GN_ReportIndex");
    }
    [SecuredOperation(Roles = "SystemAdmin")]
    public ActionResult Edit(int id)
    {
      var data = AutoMapperHelper.MapToSameViewModel<GN_Report, GN_ReportViewModel>(_reportService.GetById(id));
      return View(data.ToVM());
    }

    [HttpPost]
    public ActionResult Edit(GN_Report report)
    {
      try
      {
        // TODO: Add update logic here
        _reportService.Update(new GN_Report
        {
          ReportTitle = report.ReportTitle,
          ReportFilter = report.ReportFilter,
          ReportSql = report.ReportSql,
          ReportAuthorization = report.ReportAuthorization,
          ReportId = report.ReportId
        });
        SuccessNotification("Kayıt Güncellendi");
        return RedirectToAction("GN_ReportIndex");
      }
      catch
      {
        return View();
      }
    }

    [SecuredOperation(Roles = "SystemAdmin")]
    public ActionResult Delete(int id, GN_Report report)
    {
      var data = AutoMapperHelper.MapToSameViewModel<GN_Report, GN_ReportViewModel>(_reportService.GetById(id));
      //var data = Mapper.Map<Product, ProductViewModel>(_productService.GetById(id));
      return View(data.ToVM());
    }

    [HttpPost]
    public ActionResult Delete(int id)
    {
      try
      {
        // TODO: Add delete logic here
        _reportService.Delete(_reportService.GetById(id));
        SuccessNotification("Kayıt Silindi");

        return RedirectToAction("GN_ReportIndex");
      }
      catch
      {
        return View();
      }
    }
  }
}