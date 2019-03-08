using System;
using System.Collections.Specialized;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using NewGenFramework.Core.Aspects.Postsharp.AuthorizationAspects;
using NewGenFramework.Core.DataAccess;
using NonFactors.Mvc.Grid;
using BayiPuan.Business.Abstract;
using BayiPuan.Entities.ComplexTypes;
using BayiPuan.Entities.Concrete;
using BayiPuan.MvcWebUi.Filters;
using BayiPuan.MvcWebUi.GenericVM;
using BayiPuan.MvcWebUi.Infrastructure;
using BayiPuan.MvcWebUi.Models.ViewModels;

namespace BayiPuan.MVCWebUI.Controllers
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
    [SecuredOperation(Roles = "SystemAdmin")]
    // GET: List
        [SecuredOperation(Roles = "SystemAdmin")]
        public ActionResult LanguageWordIndex(Int32? page, Int32? rows)
        {
            IGrid<LanguageWord> col = new Grid<LanguageWord>(_queryableRepository.Table.OrderByDescending(x => x.LanguageId).Skip((page - 1 ?? 0) * (rows ?? 10)).Take(rows ?? 10).AsNoTracking());
            col.Query = new NameValueCollection(Request.QueryString);

            if (col.Query != null)
            {
                col = new Grid<LanguageWord>(_queryableRepository.Table.OrderByDescending(x => x.LanguageId).AsNoTracking());
            }
            col.Columns.Add(x => "<a class=' fas fa-edit btn btn-warning btn-sm' title='Güncelle' href='Edit/" + x.LanguageId + "'> </a>" +
                                 "<a class='actions fas fa-trash-alt btn btn-danger btn-sm' title='Sil' href='Delete/" + x.LanguageId + "'> </a>")
                .Encoded(false).Titled("işlemler").Filterable(false);
            //Görüntülenecek kolonları buraya yazacaksanız
            col.Columns.Add(x => x.LanguageId).Titled("LanguageId").MultiFilterable(true);            

            col.Pager = new GridPager<LanguageWord>(col);
            col.Processors.Add(col.Pager);
            col.Pager.RowsPerPage = 10;
            col.EmptyText = "Gösterilecek Kayıt Yok :(";
            foreach (IGridColumn column in col.Columns)
            {
                column.IsFilterable = true;
                column.IsSortable = true;
            }
            var total = _totalRowsRepository.Table.AsNoTracking().Where(x => x.TableName == "LanguageWords").Select(x => x.TableRows).First();
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
                //Alanlar buraya yazılacak
                //Örn:BrandName = brand.BrandName,
                
            });
            SuccessNotification("Kayıt Eklendi.");
            return RedirectToAction("LanguageWordIndex");
        }
    }
}