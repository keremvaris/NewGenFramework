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
  public class GiftController : BaseController
  {
    private readonly IGiftService _giftService;
    private readonly IQueryableRepository<Gift> _queryableRepository;
    private readonly IQueryableRepository<vwRP_StockCount> _totalRowsRepository;
    public GiftController(IGiftService giftService, IQueryableRepository<Gift> queryableRepository, IQueryableRepository<vwRP_StockCount> totalRowsRepository)
    {
      _giftService = giftService;
      _queryableRepository = queryableRepository;
      _totalRowsRepository = totalRowsRepository;
    }
  
    // GET: List
    [SecuredOperation(Roles = "SystemAdmin,Admin")]
    public ActionResult GiftIndex(Int32? page, Int32? rows)
    {
      IGrid<Gift> col = new Grid<Gift>(_queryableRepository.Table.Include("Category").Include("Brand").OrderByDescending(x => x.GiftId).Skip((page - 1 ?? 0) * (rows ?? 10)).Take(rows ?? 10).AsNoTracking());
      col.Query = new NameValueCollection(Request.QueryString);

      if (col.Query != null)
      {
        col = new Grid<Gift>(_queryableRepository.Table.Include("Category").Include("Brand").OrderByDescending(x => x.GiftId).AsNoTracking());
      }
      col.Columns.Add(x => "<a class=' fas fa-edit btn btn-warning btn-sm' title='Güncelle' href='Edit/" + x.GiftId + "'> </a>" +
                           "<a class='actions fas fa-trash-alt btn btn-danger btn-sm' title='Sil' href='Delete/" + x.GiftId + "'> </a>")
          .Encoded(false).Titled("işlemler").Filterable(false);
      //Görüntülenecek kolonları buraya yazacaksanız
      //col.Columns.Add(x => x.GiftId).Titled("GiftId").MultiFilterable(true);
      col.Columns.Add(x => x.Category.CategoryName).Titled("Kategori");
      col.Columns.Add(x => x.Brand.BrandName).Titled("Marka");
      col.Columns.Add(x => x.Description).Titled("Tanımı");
      col.Columns.Add(x => x.GiftPoint).Titled("Puan");
      col.Columns.Add(x => x.Usage).Titled("Kullanım Şekli");
      col.Columns.Add(x => x.Coverage).Titled("Kapsamı");
      col.Columns.Add(x => x.ValidityPeriod).Titled("Geçerlilik Süresi");
      col.Columns.Add(x => x.Indivisible).Titled("Bölünebilir mi?");
      col.Columns.Add(x => x.Combining).Titled("Birleştirilebilir mi?");
      col.Columns.Add(x => x.TermOfUse).Titled("Kullanım Şartı");
      col.Columns.Add(x => x.Cancellation).Titled("İptal Edilebilir mi?");

      col.Pager = new GridPager<Gift>(col);
      col.Processors.Add(col.Pager);
      col.Pager.RowsPerPage = 10;
      col.EmptyText = "Gösterilecek Kayıt Yok :(";
      foreach (IGridColumn column in col.Columns)
      {
        column.IsFilterable = true;
        column.IsSortable = true;
      }
      var total = _totalRowsRepository.Table.AsNoTracking().Where(x => x.TableName == "Gifts").Select(x => x.TableRows).First();
      ViewBag.totalRows = Convert.ToInt32(total);
      return View(col);
    }
    // GET: Create
    [SecuredOperation(Roles = "SystemAdmin,Admin")]
    public ActionResult Create(Gift gift)
    {
      var empty = new GiftViewModel()
      {
        Cancellation = "Hediye Çeki alındıktan sonra İPTAL edilemez.",
        Combining = "Hediye Çekleri Birleştirilemez.",
        Coverage = "Tüm ürünlerde geçerlidir.",
        Indivisible = "Tek seferde kullanılır. Farklı alışverişlerde kullanılamaz.",
        TermOfUse = "Toplam alışveriş tutarı Hediye Çeki değerinin üzerinde olmalıdır.",
        ValidityPeriod = "Satın aldığınız tarihten itibaren 3 ay içinde kullanabilirsiniz.",
        Usage = "Sadece XXXX mağazalarında (Outletler Hariç) geçerlidir.",
    };
      var data = empty.ToVM();
      return View(data);
    }
    // POST: Brand/Create
    [HttpPost]
    [ValidateInput(false)]
    public ActionResult Create(GiftViewModel gift)
    {
      if (!ModelState.IsValid)
      {
        ErrorNotification("Kayıt Eklenemedi!");
        return RedirectToAction("Create");
      }
      _giftService.Add(new Gift
      {
        CategoryId = gift.CategoryId,
        BrandId = gift.BrandId,
        Cancellation = gift.Cancellation,
        Combining = gift.Combining,
        Coverage = gift.Coverage,
        Description = gift.Description,
        GiftPoint = gift.GiftPoint,
        Indivisible = gift.Indivisible,
        TermOfUse = gift.TermOfUse,
        Usage = gift.Usage,
        ValidityPeriod = gift.ValidityPeriod,
        IsActive = gift.IsActive,
        Detail = gift.Detail


      });
      SuccessNotification("Kayıt Eklendi.");
      return RedirectToAction("GiftIndex");
    }
    // GET: Edit
    [SecuredOperation(Roles = "SystemAdmin,Admin")]
    public ActionResult Edit(int id)
    {
      var data = AutoMapperHelper.MapToSameViewModel<Gift, GiftViewModel>(_giftService.GetById(id));
      return View(data.ToVM());
    }
    // POST: Edit
    [HttpPost]
    [ValidateInput(false)]
    public ActionResult Edit(Gift gift)
    {
      try
      {
        // TODO: Add update logic here
        _giftService.Update(new Gift
        {
          CategoryId = gift.CategoryId,
          BrandId = gift.BrandId,
          Cancellation = gift.Cancellation,
          Combining = gift.Combining,
          Coverage = gift.Coverage,
          Description = gift.Description,
          GiftPoint = gift.GiftPoint,
          Indivisible = gift.Indivisible,
          TermOfUse = gift.TermOfUse,
          Usage = gift.Usage,
          ValidityPeriod = gift.ValidityPeriod,
          IsActive = gift.IsActive,
          Detail = gift.Detail,
          GiftId = gift.GiftId
        });
        SuccessNotification("Kayıt Güncellendi");
        return RedirectToAction("GiftIndex");
      }
      catch
      {
        return View();
      }
    }
    // GET: Delete
    [SecuredOperation(Roles = "SystemAdmin")]
    public ActionResult Delete(int id, Gift gift)
    {
      var data = AutoMapperHelper.MapToSameViewModel<Gift, GiftViewModel>(_giftService.GetById(id));
      return View(data.ToVM());
    }
    // POST: Delete
    [HttpPost]
    public ActionResult Delete(int id)
    {
      try
      {
        _giftService.Delete(_giftService.GetById(id));
        SuccessNotification("Kayıt Silindi");
        return RedirectToAction("GiftIndex");
      }
      catch
      {
        return View();
      }
    }
  }
}