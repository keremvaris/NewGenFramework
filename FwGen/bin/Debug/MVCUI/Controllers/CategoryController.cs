using System;
using System.Collections.Specialized;
using System.Linq;
using System.Web.Mvc;
using NewGenFramework.Core.Aspects.Postsharp.AuthorizationAspects;
using NewGenFramework.Core.DataAccess;
using NewGenFramework.Core.Utilities.Mappings;
using NonFactors.Mvc.Grid;
using NewGenFramework.Business.Abstract;
using NewGenFramework.Entities.ComplexTypes;
using NewGenFramework.Entities.Concrete;
using NewGenFramework.MvcWebUi.GenericVM;
using NewGenFramework.MvcWebUi.Filters;
using NewGenFramework.MvcWebUi.Infrastructure;
using NewGenFramework.MvcWebUi.Models.ViewModels;

namespace NewGenFramework.MvcWebUi.Controllers
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
        [SecuredOperation(Roles = "SystemAdmin")]
        public ActionResult CategoryIndex(Int32? page, Int32? rows)
        {
            IGrid<Category> col = new Grid<Category>(_queryableRepository.Table.OrderByDescending(x => x.CategoryId).Skip((page - 1 ?? 0) * (rows ?? 10)).Take(rows ?? 10));
            col.Query = new NameValueCollection(Request.QueryString);

            if (col.Query != null)
            {
                col = new Grid<Category>(_queryableRepository.Table.OrderByDescending(x => x.CategoryId));
            }
            col.Columns.Add(x => "<a class=' fas fa-edit btn btn-warning btn-sm' title='Güncelle' href='Edit/" + x.CategoryId + "'> </a>" +
                                 "<a class='actions fas fa-trash-alt btn btn-danger btn-sm' title='Sil' href='Delete/" + x.CategoryId + "'> </a>")
                .Encoded(false).Titled("işlemler").Filterable(false);
            //Görüntülenecek kolonları buraya yazacaksanız
            col.Columns.Add(x => x.CategoryId).Titled("CategoryId").MultiFilterable(true);            

            col.Pager = new GridPager<Category>(col);
            col.Processors.Add(col.Pager);
            col.Pager.RowsPerPage = 10;
            col.EmptyText = "Gösterilecek Kayıt Yok :(";
            foreach (IGridColumn column in col.Columns)
            {
                column.IsFilterable = true;
                column.IsSortable = true;
            }
            var total = _totalRowsRepository.Table.Where(x => x.TableName == "Categorys").Select(x => x.TableRows).First();
            ViewBag.totalRows = Convert.ToInt32(total);
            return View(col);
        }
         // GET: Create
        [SecuredOperation(Roles = "SystemAdmin")]
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
                //TODO:Alanlar buraya yazılacak
                //Örn:BrandName = brand.BrandName,
                
            });
            SuccessNotification("Kayıt Eklendi.");
            return RedirectToAction("CategoryIndex");
        }
        // GET: Edit
        [SecuredOperation(Roles = "SystemAdmin")]
        public ActionResult Edit(int id)
        {
            var data = AutoMapperHelper.MapToSameViewModel<Category, CategoryViewModel>(_categoryService.GetById(id));
            return View(data.ToVM());
        }
        // POST: Edit
        [HttpPost]
        public ActionResult Edit( Category category)
        {
            try
            {                
                _categoryService.Update(new  Category
                {
                 //TODO:Alanlar buraya yazılacak Id alanı en altta olacak unutmayın!!!
                 //Örn:BrandName = brand.BrandName,
                 //BrandId = brand.BrandId
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
        [SecuredOperation(Roles = "SystemAdmin")]
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