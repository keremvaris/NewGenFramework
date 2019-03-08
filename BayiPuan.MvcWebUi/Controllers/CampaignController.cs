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
  public class CampaignController : BaseController
  {
    private readonly ICampaignService _campaignService;
    private readonly IQueryableRepository<Campaign> _queryableRepository;
    private readonly IQueryableRepository<vwRP_StockCount> _totalRowsRepository;
    public CampaignController(ICampaignService campaignService, IQueryableRepository<Campaign> queryableRepository, IQueryableRepository<vwRP_StockCount> totalRowsRepository)
    {
      _campaignService = campaignService;
      _queryableRepository = queryableRepository;
      _totalRowsRepository = totalRowsRepository;
    }
    // GET: List
    [SecuredOperation(Roles = "SystemAdmin,Admin")]
    public ActionResult CampaignIndex(Int32? page, Int32? rows)
    {
      //TODO: Kampanya için eklenecek alanlar var !!!
      IGrid<Campaign> col = new Grid<Campaign>(_queryableRepository.Table.OrderByDescending(x => x.CampaignId).Skip((page - 1 ?? 0) * (rows ?? 10)).Take(rows ?? 10).AsNoTracking());
      col.Query = new NameValueCollection(Request.QueryString);

      if (col.Query != null)
      {
        col = new Grid<Campaign>(_queryableRepository.Table.OrderByDescending(x => x.CampaignId).AsNoTracking());
      }
      col.Columns.Add(x => "<a class=' fas fa-edit btn btn-warning btn-sm' title='Güncelle' href='Edit/" + x.CampaignId + "'> </a>" +
                           "<a class='actions fas fa-trash-alt btn btn-danger btn-sm' title='Sil' href='Delete/" + x.CampaignId + "'> </a>")
          .Encoded(false).Titled("işlemler").Filterable(false);
      //Görüntülenecek kolonları buraya yazacaksanız
      col.Columns.Add(x => x.CampaignId).Titled("Kampanya No").MultiFilterable(true);
      col.Columns.Add(x => x.CampaignName).Titled("Kampanya Adı").MultiFilterable(true);

      col.Pager = new GridPager<Campaign>(col);
      col.Processors.Add(col.Pager);
      col.Pager.RowsPerPage = 10;
      col.EmptyText = "Gösterilecek Kayıt Yok :(";
      foreach (IGridColumn column in col.Columns)
      {
        column.IsFilterable = true;
        column.IsSortable = true;
      }
      var total = _totalRowsRepository.Table.AsNoTracking().Where(x => x.TableName == "Campaigns").Select(x => x.TableRows).First();
      ViewBag.totalRows = Convert.ToInt32(total);
      return View(col);
    }
    // GET: Create
    [SecuredOperation(Roles = "SystemAdmin,Admin")]
    public ActionResult Create()
    {
      var empty = new CampaignViewModel();
      var data = empty.ToVM();
      return View(data);
    }
    // POST: Brand/Create
    [HttpPost]
    public ActionResult Create(CampaignViewModel campaign)
    {
      if (!ModelState.IsValid)
      {
        ErrorNotification("Kayıt Eklenemedi!");
        return RedirectToAction("Create");
      }
      _campaignService.Add(new Campaign
      {
        CampaignName = campaign.CampaignName,
        CampaignImage = campaign.CampaignImage,
        CampaignImageExt = ".png"

      });
      SuccessNotification("Kayıt Eklendi.");
      return RedirectToAction("CampaignIndex");
    }
    // GET: Edit
    [SecuredOperation(Roles = "SystemAdmin,Admin")]
    public ActionResult Edit(int id)
    {
      var data = AutoMapperHelper.MapToSameViewModel<Campaign, CampaignViewModel>(_campaignService.GetById(id));
      return View(data.ToVM());
    }
    // POST: Edit
    [HttpPost]
    public ActionResult Edit(CampaignViewModel campaign)
    {
      try
      {
        // TODO: Add update logic here
        _campaignService.Update(new Campaign
        {
          CampaignName = campaign.CampaignName,
          CampaignImage = campaign.CampaignImage,
          CampaignImageExt = ".png",
          CampaignId = campaign.CampaignId
        });
        SuccessNotification("Kayıt Güncellendi");
        return RedirectToAction("CampaignIndex");
      }
      catch
      {
        return View();
      }
    }
    // GET: Delete
    [SecuredOperation(Roles = "SystemAdmin")]
    public ActionResult Delete(int id, Campaign campaign)
    {
      var data = AutoMapperHelper.MapToSameViewModel<Campaign, CampaignViewModel>(_campaignService.GetById(id));
      return View(data.ToVM());
    }
    // POST: Delete
    [HttpPost]
    public ActionResult Delete(int id)
    {
      try
      {
        _campaignService.Delete(_campaignService.GetById(id));
        SuccessNotification("Kayıt Silindi");
        return RedirectToAction("CampaignIndex");
      }
      catch
      {
        return View();
      }
    }
  }
}