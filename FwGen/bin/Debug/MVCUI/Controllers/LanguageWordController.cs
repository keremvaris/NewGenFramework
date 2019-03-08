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
    public class LanguageWordController : BaseController
    {
        private readonly ILanguageWordService _languageWordService;
        private readonly IQueryableRepository<LanguageWord> _queryableRepository;
        private readonly IQueryableRepository<vwRP_StockCount> _totalRowsRepository;
        public LanguageWordController(ILanguageWordService languageWordService, IQueryableRepository<LanguageWord> queryableRepository, IQueryableRepository<vwRP_StockCount> totalRowsRepository)
        {
            _languageWordService = languageWordService;
            _queryableRepository = queryableRepository;
            _totalRowsRepository = totalRowsRepository;
        }
        // GET: List
        [SecuredOperation(Roles = "SystemAdmin")]
        public ActionResult LanguageWordIndex(Int32? page, Int32? rows)
        {
            IGrid<LanguageWord> col = new Grid<LanguageWord>(_queryableRepository.Table.OrderByDescending(x => x.LanguageWordId).Skip((page - 1 ?? 0) * (rows ?? 10)).Take(rows ?? 10));
            col.Query = new NameValueCollection(Request.QueryString);

            if (col.Query != null)
            {
                col = new Grid<LanguageWord>(_queryableRepository.Table.OrderByDescending(x => x.LanguageWordId));
            }
            col.Columns.Add(x => "<a class=' fas fa-edit btn btn-warning btn-sm' title='Güncelle' href='Edit/" + x.LanguageWordId + "'> </a>" +
                                 "<a class='actions fas fa-trash-alt btn btn-danger btn-sm' title='Sil' href='Delete/" + x.LanguageWordId + "'> </a>")
                .Encoded(false).Titled("işlemler").Filterable(false);
            //Görüntülenecek kolonları buraya yazacaksanız
            col.Columns.Add(x => x.LanguageWordId).Titled("LanguageWordId").MultiFilterable(true);            

            col.Pager = new GridPager<LanguageWord>(col);
            col.Processors.Add(col.Pager);
            col.Pager.RowsPerPage = 10;
            col.EmptyText = "Gösterilecek Kayıt Yok :(";
            foreach (IGridColumn column in col.Columns)
            {
                column.IsFilterable = true;
                column.IsSortable = true;
            }
            var total = _totalRowsRepository.Table.Where(x => x.TableName == "LanguageWords").Select(x => x.TableRows).First();
            ViewBag.totalRows = Convert.ToInt32(total);
            return View(col);
        }
         // GET: Create
        [SecuredOperation(Roles = "SystemAdmin")]
        public ActionResult Create()
        {
            var empty = new LanguageWordViewModel();
            var data = empty.ToVM();
            return View(data);
        }
        // POST: Brand/Create
        [HttpPost]
        public ActionResult Create(LanguageWordViewModel languageWord)
        {
            if (!ModelState.IsValid)
            {
                ErrorNotification("Kayıt Eklenemedi!");
                return RedirectToAction("Create");
            }
            _languageWordService.Add(new LanguageWord
            {
                //TODO:Alanlar buraya yazılacak
                //Örn:BrandName = brand.BrandName,
                
            });
            SuccessNotification("Kayıt Eklendi.");
            return RedirectToAction("LanguageWordIndex");
        }
        // GET: Edit
        [SecuredOperation(Roles = "SystemAdmin")]
        public ActionResult Edit(int id)
        {
            var data = AutoMapperHelper.MapToSameViewModel<LanguageWord, LanguageWordViewModel>(_languageWordService.GetById(id));
            return View(data.ToVM());
        }
        // POST: Edit
        [HttpPost]
        public ActionResult Edit( LanguageWord languageWord)
        {
            try
            {                
                _languageWordService.Update(new  LanguageWord
                {
                 //TODO:Alanlar buraya yazılacak Id alanı en altta olacak unutmayın!!!
                 //Örn:BrandName = brand.BrandName,
                 //BrandId = brand.BrandId
                  LanguageWordId = languageWord.LanguageWordId
                });
                SuccessNotification("Kayıt Güncellendi");
                return RedirectToAction("LanguageWordIndex");
            }
            catch
            {
                return View();
            }
        }
         // GET: Delete
        [SecuredOperation(Roles = "SystemAdmin")]
        public ActionResult Delete(int id, LanguageWord languageWord)
        {
            var data = AutoMapperHelper.MapToSameViewModel<LanguageWord, LanguageWordViewModel>(_languageWordService.GetById(id));
            return View(data.ToVM());
        }
        // POST: Delete
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {            
                _languageWordService.Delete(_languageWordService.GetById(id));
                SuccessNotification("Kayıt Silindi");
                return RedirectToAction("LanguageWordIndex");
            }
            catch
            {
                return View();
            }
        }
    }
}