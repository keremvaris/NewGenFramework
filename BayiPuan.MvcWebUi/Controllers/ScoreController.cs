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

namespace BayiPuan.MvcWebUi.Controllers
{
  [AuthorizationFilter]
  public class ScoreController : BaseController
  {
    private readonly IScoreService _scoreService;
    private readonly IQueryableRepository<Score> _queryableRepository;
    private readonly IQueryableRepository<vwRP_StockCount> _totalRowsRepository;
    public ScoreController(IScoreService scoreService, IQueryableRepository<Score> queryableRepository, IQueryableRepository<vwRP_StockCount> totalRowsRepository)
    {
      _scoreService = scoreService;
      _queryableRepository = queryableRepository;
      _totalRowsRepository = totalRowsRepository;
    }
    // GET: List
    [SecuredOperation(Roles = "SystemAdmin")]
    public ActionResult ScoreIndex(Int32? page, Int32? rows)
    {
      var userId = Convert.ToInt32(GeneralHelpers.GetUserId());
      IGrid<Score> col = new Grid<Score>(_queryableRepository.Table.Include("User").OrderByDescending(x => x.ScoreId).Skip((page - 1 ?? 0) * (rows ?? 10)).Take(rows ?? 10));
      col.Query = new NameValueCollection(Request.QueryString);

      if (col.Query != null)
      {
        col = new Grid<Score>(_queryableRepository.Table.Include("User").OrderByDescending(x => x.ScoreId));
      }
      col.Columns.Add(x => "<a class=' fas fa-edit btn btn-warning btn-sm' title='Güncelle' href='Edit/" + x.ScoreId + "'> </a>" +
                           "<a class='actions fas fa-trash-alt btn btn-danger btn-sm' title='Sil' href='Delete/" + x.ScoreId + "'> </a>")
          .Encoded(false).Titled("işlemler").Filterable(false);
      //Görüntülenecek kolonları buraya yazacaksanız
      col.Columns.Add(x => x.ScoreId).Titled("ScoreId").MultiFilterable(true);
      col.Columns.Add(x => x.User.UserName).Titled("Kullanıcı");
      col.Columns.Add(x => x.ScoreTotal).RenderedAs(x => x.ScoreTotal).Titled("Kazanılan Puan");
      col.Columns.Add(x => x.ScoreType).Titled("Puan Kazanma Tipi");
      col.Columns.Add(x => x.ScoreDate).Titled("Puan Tarihi");
      col.Pager = new GridPager<Score>(col);
      col.Processors.Add(col.Pager);
      col.Pager.RowsPerPage = 10;
      col.EmptyText = "Gösterilecek Kayıt Yok :(";
      foreach (IGridColumn column in col.Columns)
      {
        column.IsFilterable = true;
        column.IsSortable = true;
      }
      var total = _totalRowsRepository.Table.Where(x => x.TableName == "Scores").Select(x => x.TableRows).First();
      ViewBag.totalRows = Convert.ToInt32(total);
      return View(col);
    }
    [SecuredOperation(Roles = "SystemAdmin,Admin,User")]
    public ActionResult UserScoreIndex(Int32? page, Int32? rows)
    {
      var userId = Convert.ToInt32(GeneralHelpers.GetUserId());
      IGrid<Score> col = new Grid<Score>(_queryableRepository.Table.Include("User").Where(x => x.UserId == userId).OrderByDescending(x => x.ScoreId).Skip((page - 1 ?? 0) * (rows ?? 10)).Take(rows ?? 10));
      col.Query = new NameValueCollection(Request.QueryString);

      if (col.Query != null)
      {
        col = new Grid<Score>(_queryableRepository.Table.Include("User").Where(x => x.UserId == userId).OrderByDescending(x => x.ScoreId));
      }
      //col.Columns.Add(x => "<a class=' fas fa-edit btn btn-warning btn-sm' title='Güncelle' href='Edit/" + x.ScoreId + "'> </a>" +
      //                     "<a class='actions fas fa-trash-alt btn btn-danger btn-sm' title='Sil' href='Delete/" + x.ScoreId + "'> </a>")
      //  .Encoded(false).Titled("işlemler").Filterable(false);
      //Görüntülenecek kolonları buraya yazacaksanız
      col.Columns.Add(x => x.ScoreId).Titled("ScoreId").MultiFilterable(true);
      col.Columns.Add(x => x.User.UserName).Titled("Kullanıcı");
      col.Columns.Add(x => x.ScoreTotal).RenderedAs(x => x.ScoreTotal).Titled("Kazanılan Puan");
      col.Columns.Add(x => x.ScoreType).Titled("Puan Kazanma Tipi");
      col.Columns.Add(x => x.ScoreDate).Titled("Puan Tarihi");
      col.Pager = new GridPager<Score>(col);
      col.Processors.Add(col.Pager);
      col.Pager.RowsPerPage = 10;
      col.EmptyText = "Gösterilecek Kayıt Yok :(";
      foreach (IGridColumn column in col.Columns)
      {
        column.IsFilterable = true;
        column.IsSortable = true;
      }
      var total = _totalRowsRepository.Table.Where(x => x.TableName == "Scores").Select(x => x.TableRows).First();
      ViewBag.totalRows = Convert.ToInt32(total);
      return View(col);
    }
    // GET: Create
    [SecuredOperation(Roles = "SystemAdmin")]
    public ActionResult Create()
    {
      var empty = new ScoreViewModel();
      var data = empty.ToVM();
      return View(data);
    }
    // POST: Brand/Create
    [HttpPost]
    public ActionResult Create(ScoreViewModel score)
    {
      if (!ModelState.IsValid)
      {
        ErrorNotification("Kayıt Eklenemedi!");
        return RedirectToAction("Create");
      }
      _scoreService.Add(new Score
      {
        UserId = score.UserId,
        ScoreDate = DateTime.Now,
        ScoreTotal = score.ScoreTotal,
        ScoreType = ScoreType.YoneticiPuanArtır
      });
      SuccessNotification("Kayıt Eklendi.");
      return RedirectToAction("ScoreIndex");
    }
    // GET: Edit
    [SecuredOperation(Roles = "SystemAdmin")]
    public ActionResult Edit(int id)
    {
      var data = AutoMapperHelper.MapToSameViewModel<Score, ScoreViewModel>(_scoreService.GetById(id));
      return View(data.ToVM());
    }
    // POST: Edit
    [HttpPost]
    public ActionResult Edit(Score score)
    {
      try
      {
        _scoreService.Update(new Score
        {
          UserId = score.UserId,
          ScoreDate = DateTime.Now,
          ScoreTotal = score.ScoreTotal,
          ScoreType = ScoreType.YoneticiPuanArtır,
          ScoreId = score.ScoreId
        });
        SuccessNotification("Kayıt Güncellendi");
        return RedirectToAction("ScoreIndex");
      }
      catch
      {
        return View();
      }
    }
    // GET: Delete
    [SecuredOperation(Roles = "SystemAdmin")]
    public ActionResult Delete(int id, Score score)
    {
      var data = AutoMapperHelper.MapToSameViewModel<Score, ScoreViewModel>(_scoreService.GetById(id));
      return View(data.ToVM());
    }
    // POST: Delete
    [HttpPost]
    public ActionResult Delete(int id)
    {
      try
      {
        _scoreService.Delete(_scoreService.GetById(id));
        SuccessNotification("Kayıt Silindi");
        return RedirectToAction("ScoreIndex");
      }
      catch
      {
        return View();
      }
    }
  }
}