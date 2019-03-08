using System;
using System.Collections.Specialized;
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
    public class RelationalPersonController : BaseController
    {
        private readonly IRelationalPersonService _relationalPersonService;
        private readonly IQueryableRepository<RelationalPerson> _queryableRepository;
        private readonly IQueryableRepository<vwRP_StockCount> _totalRowsRepository;
        public RelationalPersonController(IRelationalPersonService relationalPersonService, IQueryableRepository<RelationalPerson> queryableRepository, IQueryableRepository<vwRP_StockCount> totalRowsRepository)
        {
            _relationalPersonService = relationalPersonService;
            _queryableRepository = queryableRepository;
            _totalRowsRepository = totalRowsRepository;
        }
    [SecuredOperation(Roles = "SystemAdmin")]
    // GET: List
        [SecuredOperation(Roles = "SystemAdmin")]
        public ActionResult RelationalPersonIndex(Int32? page, Int32? rows)
        {
            IGrid<RelationalPerson> col = new Grid<RelationalPerson>(_queryableRepository.Table.OrderByDescending(x => x.RelationalPersonId).Skip((page - 1 ?? 0) * (rows ?? 10)).Take(rows ?? 10));
            col.Query = new NameValueCollection(Request.QueryString);

            if (col.Query != null)
            {
                col = new Grid<RelationalPerson>(_queryableRepository.Table.OrderByDescending(x => x.RelationalPersonId));
            }
            col.Columns.Add(x => "<a class=' fas fa-edit btn btn-warning btn-sm' title='Güncelle' href='Edit/" + x.RelationalPersonId + "'> </a>" +
                                 "<a class='actions fas fa-trash-alt btn btn-danger btn-sm' title='Sil' href='Delete/" + x.RelationalPersonId + "'> </a>")
                .Encoded(false).Titled("işlemler").Filterable(false);
            //Görüntülenecek kolonları buraya yazacaksanız
            col.Columns.Add(x => x.RelationalPersonId).Titled("RelationalPersonId").MultiFilterable(true);            

            col.Pager = new GridPager<RelationalPerson>(col);
            col.Processors.Add(col.Pager);
            col.Pager.RowsPerPage = 10;
            col.EmptyText = "Gösterilecek Kayıt Yok :(";
            foreach (IGridColumn column in col.Columns)
            {
                column.IsFilterable = true;
                column.IsSortable = true;
            }
            var total = _totalRowsRepository.Table.Where(x => x.TableName == "RelationalPersons").Select(x => x.TableRows).First();
            ViewBag.totalRows = Convert.ToInt32(total);
            return View(col);
        }
         // GET: Create
        [SecuredOperation(Roles = "SystemAdmin")]
        public ActionResult Create()
        {
            var empty = new RelationalPersonViewModel();
            var data = empty.ToVM();
            return View(data);
        }
        // POST: Brand/Create
        [HttpPost]
        public ActionResult Create(RelationalPersonViewModel relationalPerson)
        {
            if (!ModelState.IsValid)
            {
                ErrorNotification("Kayıt Eklenemedi!");
                return RedirectToAction("Create");
            }
            _relationalPersonService.Add(new RelationalPerson
            {
                //Alanlar buraya yazılacak
                //Örn:BrandName = brand.BrandName,
                
            });
            SuccessNotification("Kayıt Eklendi.");
            return RedirectToAction("RelationalPersonIndex");
        }
        // GET: Edit
        [SecuredOperation(Roles = "SystemAdmin")]
        public ActionResult Edit(int id)
        {
            var data = AutoMapperHelper.MapToSameViewModel<RelationalPerson, RelationalPersonViewModel>(_relationalPersonService.GetById(id));
            return View(data.ToVM());
        }
        // POST: Edit
        [HttpPost]
        public ActionResult Edit( RelationalPerson relationalPerson)
        {
            try
            {
                // TODO: Add update logic here
                _relationalPersonService.Update(new  RelationalPerson
                {
                 //Alanlar buraya yazılacak Id alanı en altta olacak unutmayın!!!
                 //Örn:BrandName = brand.BrandName,
                 //BrandId = brand.BrandId
                });
                SuccessNotification("Kayıt Güncellendi");
                return RedirectToAction("RelationalPersonIndex");
            }
            catch
            {
                return View();
            }
        }
         // GET: Delete
        [SecuredOperation(Roles = "SystemAdmin")]
        public ActionResult Delete(int id, RelationalPerson relationalPerson)
        {
            var data = AutoMapperHelper.MapToSameViewModel<RelationalPerson, RelationalPersonViewModel>(_relationalPersonService.GetById(id));
            return View(data.ToVM());
        }
        // POST: Delete
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {            
                _relationalPersonService.Delete(_relationalPersonService.GetById(id));
                SuccessNotification("Kayıt Silindi");
                return RedirectToAction("RelationalPersonIndex");
            }
            catch
            {
                return View();
            }
        }
    }
}