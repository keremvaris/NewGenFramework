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
  public class CategoryController : BaseController
  {
    private readonly ICategoryService _categoryService;
    private readonly IQueryableRepository<Category> _queryableRepository;
    private readonly IQueryableRepository<vwRP_StockCount> _totalRowsRepository;
    public CategoryController(ICategoryService categoryService, IQueryableRepository<Category> queryableRepository, IQueryableRepository<vwRP_StockCount> totalRowsRepository)
    {
      _categoryService = categoryService;
      _queryableRepository = queryableRepository;
      _totalRowsRepository = totalRowsRepository;
    }
    // GET: List
    [SecuredOperation(Roles = "SystemAdmin,Admin")]
    public ActionResult CategoryIndex(Int32? page, Int32? rows)
    {
      IGrid<Category> col = new Grid<Category>(_queryableRepository.Table.OrderByDescending(x => x.CategoryId).Skip((page - 1 ?? 0) * (rows ?? 10)).Take(rows ?? 10).AsNoTracking());
      col.Query = new NameValueCollection(Request.QueryString);

      if (col.Query != null)
      {
        col = new Grid<Category>(_queryableRepository.Table.OrderByDescending(x => x.CategoryId).AsNoTracking());
      }
      col.Columns.Add(x => "<a class=' fas fa-edit btn btn-warning btn-sm' title='Güncelle' href='Edit/" + x.CategoryId + "'> </a>" +
                           "<a class='actions fas fa-trash-alt btn btn-danger btn-sm' title='Sil' href='Delete/" + x.CategoryId + "'> </a>")
          .Encoded(false).Titled("işlemler").Filterable(false);
      //Görüntülenecek kolonları buraya yazacaksanız
      col.Columns.Add(x => x.CategoryId).Titled("Kategori No").MultiFilterable(true);
      col.Columns.Add(x => x.CategoryName).Titled("Kategori Name").MultiFilterable(true);

      col.Pager = new GridPager<Category>(col);
      col.Processors.Add(col.Pager);
      col.Pager.RowsPerPage = 10;
      col.EmptyText = "Gösterilecek Kayıt Yok :(";
      foreach (IGridColumn column in col.Columns)
      {
        column.IsFilterable = true;
        column.IsSortable = true;
      }
      var total = _totalRowsRepository.Table.AsNoTracking().Where(x => x.TableName == "Categories").Select(x => x.TableRows).First();
      ViewBag.totalRows = Convert.ToInt32(total);
      return View(col);
    }
    // GET: Create
    [SecuredOperation(Roles = "SystemAdmin,Admin")]
    public ActionResult Create()
    {
      var empty = new CategoryViewModel();
      var data = empty.ToVM();
      return View(data);
    }
    // POST: Brand/Create
    [HttpPost]
    public ActionResult Create(CategoryViewModel category)
    {
      if (!ModelState.IsValid)
      {
        ErrorNotification("Kayıt Eklenemedi!");
        return RedirectToAction("Create");
      }
      _categoryService.Add(new Category
      {
        CategoryName = category.CategoryName

      });
      SuccessNotification("Kayıt Eklendi.");
      return RedirectToAction("CategoryIndex");
    }
    // GET: Edit
    [SecuredOperation(Roles = "SystemAdmin,Admin")]
    public ActionResult Edit(int id)
    {
      var data = AutoMapperHelper.MapToSameViewModel<Category, CategoryViewModel>(_categoryService.GetById(id));
      return View(data.ToVM());
    }
    // POST: Edit
    [HttpPost]
    public ActionResult Edit(Category category)
    {
      try
      {
        // TODO: Add update logic here
        _categoryService.Update(new Category
        {
         CategoryName = category.CategoryName,
          CategoryId = category.CategoryId
        });
        SuccessNotification("Kayıt Güncellendi");
        return RedirectToAction("CategoryIndex");
      }
      catch
      {
        return View();
      }
    }
    // GET: Delete
    [SecuredOperation(Roles = "SystemAdmin,Admin")]
    public ActionResult Delete(int id, Category category)
    {
      var data = AutoMapperHelper.MapToSameViewModel<Category, CategoryViewModel>(_categoryService.GetById(id));
      return View(data.ToVM());
    }
    // POST: Delete
    [HttpPost]
    public ActionResult Delete(int id)
    {
      try
      {
        _categoryService.Delete(_categoryService.GetById(id));
        SuccessNotification("Kayıt Silindi");
        return RedirectToAction("CategoryIndex");
      }
      catch
      {
        return View();
      }
    }
  }
}