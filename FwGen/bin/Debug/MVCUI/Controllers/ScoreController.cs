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
            IGrid<Score> col = new Grid<Score>(_queryableRepository.Table.OrderByDescending(x => x.ScoreId).Skip((page - 1 ?? 0) * (rows ?? 10)).Take(rows ?? 10));
            col.Query = new NameValueCollection(Request.QueryString);

            if (col.Query != null)
            {
                col = new Grid<Score>(_queryableRepository.Table.OrderByDescending(x => x.ScoreId));
            }
            col.Columns.Add(x => "<a class=' fas fa-edit btn btn-warning btn-sm' title='Güncelle' href='Edit/" + x.ScoreId + "'> </a>" +
                                 "<a class='actions fas fa-trash-alt btn btn-danger btn-sm' title='Sil' href='Delete/" + x.ScoreId + "'> </a>")
                .Encoded(false).Titled("işlemler").Filterable(false);
            //Görüntülenecek kolonları buraya yazacaksanız
            col.Columns.Add(x => x.ScoreId).Titled("ScoreId").MultiFilterable(true);            

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
                //TODO:Alanlar buraya yazılacak
                //Örn:BrandName = brand.BrandName,
                
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
        public ActionResult Edit( Score score)
        {
            try
            {                
                _scoreService.Update(new  Score
                {
                 //TODO:Alanlar buraya yazılacak Id alanı en altta olacak unutmayın!!!
                 //Örn:BrandName = brand.BrandName,
                 //BrandId = brand.BrandId
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