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
    public class KnowledgeTestController : BaseController
    {
        private readonly IKnowledgeTestService _knowledgeTestService;
        private readonly IQueryableRepository<KnowledgeTest> _queryableRepository;
        private readonly IQueryableRepository<vwRP_StockCount> _totalRowsRepository;
        public KnowledgeTestController(IKnowledgeTestService knowledgeTestService, IQueryableRepository<KnowledgeTest> queryableRepository, IQueryableRepository<vwRP_StockCount> totalRowsRepository)
        {
            _knowledgeTestService = knowledgeTestService;
            _queryableRepository = queryableRepository;
            _totalRowsRepository = totalRowsRepository;
        }
        // GET: List
        [SecuredOperation(Roles = "SystemAdmin")]
        public ActionResult KnowledgeTestIndex(Int32? page, Int32? rows)
        {
            IGrid<KnowledgeTest> col = new Grid<KnowledgeTest>(_queryableRepository.Table.OrderByDescending(x => x.KnowledgeTestId).Skip((page - 1 ?? 0) * (rows ?? 10)).Take(rows ?? 10));
            col.Query = new NameValueCollection(Request.QueryString);

            if (col.Query != null)
            {
                col = new Grid<KnowledgeTest>(_queryableRepository.Table.OrderByDescending(x => x.KnowledgeTestId));
            }
            col.Columns.Add(x => "<a class=' fas fa-edit btn btn-warning btn-sm' title='Güncelle' href='Edit/" + x.KnowledgeTestId + "'> </a>" +
                                 "<a class='actions fas fa-trash-alt btn btn-danger btn-sm' title='Sil' href='Delete/" + x.KnowledgeTestId + "'> </a>")
                .Encoded(false).Titled("işlemler").Filterable(false);
            //Görüntülenecek kolonları buraya yazacaksanız
            col.Columns.Add(x => x.KnowledgeTestId).Titled("KnowledgeTestId").MultiFilterable(true);            

            col.Pager = new GridPager<KnowledgeTest>(col);
            col.Processors.Add(col.Pager);
            col.Pager.RowsPerPage = 10;
            col.EmptyText = "Gösterilecek Kayıt Yok :(";
            foreach (IGridColumn column in col.Columns)
            {
                column.IsFilterable = true;
                column.IsSortable = true;
            }
            var total = _totalRowsRepository.Table.Where(x => x.TableName == "KnowledgeTests").Select(x => x.TableRows).First();
            ViewBag.totalRows = Convert.ToInt32(total);
            return View(col);
        }
         // GET: Create
        [SecuredOperation(Roles = "SystemAdmin")]
        public ActionResult Create()
        {
            var empty = new KnowledgeTestViewModel();
            var data = empty.ToVM();
            return View(data);
        }
        // POST: Brand/Create
        [HttpPost]
        public ActionResult Create(KnowledgeTestViewModel knowledgeTest)
        {
            if (!ModelState.IsValid)
            {
                ErrorNotification("Kayıt Eklenemedi!");
                return RedirectToAction("Create");
            }
            _knowledgeTestService.Add(new KnowledgeTest
            {
                //TODO:Alanlar buraya yazılacak
                //Örn:BrandName = brand.BrandName,
                
            });
            SuccessNotification("Kayıt Eklendi.");
            return RedirectToAction("KnowledgeTestIndex");
        }
        // GET: Edit
        [SecuredOperation(Roles = "SystemAdmin")]
        public ActionResult Edit(int id)
        {
            var data = AutoMapperHelper.MapToSameViewModel<KnowledgeTest, KnowledgeTestViewModel>(_knowledgeTestService.GetById(id));
            return View(data.ToVM());
        }
        // POST: Edit
        [HttpPost]
        public ActionResult Edit( KnowledgeTest knowledgeTest)
        {
            try
            {                
                _knowledgeTestService.Update(new  KnowledgeTest
                {
                 //TODO:Alanlar buraya yazılacak Id alanı en altta olacak unutmayın!!!
                 //Örn:BrandName = brand.BrandName,
                 //BrandId = brand.BrandId
                  KnowledgeTestId = knowledgeTest.KnowledgeTestId
                });
                SuccessNotification("Kayıt Güncellendi");
                return RedirectToAction("KnowledgeTestIndex");
            }
            catch
            {
                return View();
            }
        }
         // GET: Delete
        [SecuredOperation(Roles = "SystemAdmin")]
        public ActionResult Delete(int id, KnowledgeTest knowledgeTest)
        {
            var data = AutoMapperHelper.MapToSameViewModel<KnowledgeTest, KnowledgeTestViewModel>(_knowledgeTestService.GetById(id));
            return View(data.ToVM());
        }
        // POST: Delete
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {            
                _knowledgeTestService.Delete(_knowledgeTestService.GetById(id));
                SuccessNotification("Kayıt Silindi");
                return RedirectToAction("KnowledgeTestIndex");
            }
            catch
            {
                return View();
            }
        }
    }
}