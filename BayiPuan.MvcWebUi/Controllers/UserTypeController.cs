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
using BayiPuan.MvcWebUi.Filters;
using BayiPuan.MvcWebUi.GenericVM;
using BayiPuan.MvcWebUi.Infrastructure;
using BayiPuan.MvcWebUi.Models.ViewModels;


namespace BayiPuan.MvcWebUi.Controllers
{
    [AuthorizationFilter]
    public class UserTypeController : BaseController
    {
        private readonly IUserTypeService _userTypeService;
        private readonly IQueryableRepository<UserType> _queryableRepository;
        private readonly IQueryableRepository<vwRP_StockCount> _totalRowsRepository;
        public UserTypeController(IUserTypeService userTypeService, IQueryableRepository<UserType> queryableRepository, IQueryableRepository<vwRP_StockCount> totalRowsRepository)
        {
            _userTypeService = userTypeService;
            _queryableRepository = queryableRepository;
            _totalRowsRepository = totalRowsRepository;
        }
  
    // GET: List
        [SecuredOperation(Roles = "SystemAdmin,Admin")]
        public ActionResult UserTypeIndex(Int32? page, Int32? rows)
        {
            IGrid<UserType> col = new Grid<UserType>(_queryableRepository.Table.OrderByDescending(x => x.UserTypeId).Skip((page - 1 ?? 0) * (rows ?? 10)).Take(rows ?? 10).AsNoTracking());
            col.Query = new NameValueCollection(Request.QueryString);

            if (col.Query != null)
            {
                col = new Grid<UserType>(_queryableRepository.Table.OrderByDescending(x => x.UserTypeId).AsNoTracking());
            }
            col.Columns.Add(x => "<a class=' fas fa-edit btn btn-warning btn-sm' title='Güncelle' href='Edit/" + x.UserTypeId + "'> </a>" +
                                 "<a class='actions fas fa-trash-alt btn btn-danger btn-sm' title='Sil' href='Delete/" + x.UserTypeId + "'> </a>")
                .Encoded(false).Titled("işlemler").Filterable(false);
            //Görüntülenecek kolonları buraya yazacaksanız
            col.Columns.Add(x => x.UserTypeId).Titled("UserTypeId").MultiFilterable(true);            

            col.Pager = new GridPager<UserType>(col);
            col.Processors.Add(col.Pager);
            col.Pager.RowsPerPage = 10;
            col.EmptyText = "Gösterilecek Kayıt Yok :(";
            foreach (IGridColumn column in col.Columns)
            {
                column.IsFilterable = true;
                column.IsSortable = true;
            }
            var total = _totalRowsRepository.Table.AsNoTracking().Where(x => x.TableName == "UserTypes").Select(x => x.TableRows).First();
            ViewBag.totalRows = Convert.ToInt32(total);
            return View(col);
        }
         // GET: Create
        [SecuredOperation(Roles = "SystemAdmin,Admin")]
        public ActionResult Create()
        {
            var empty = new UserTypeViewModel();
            var data = empty.ToVM();
            return View(data);
        }
        // POST: Brand/Create
        [HttpPost]
        public ActionResult Create(UserTypeViewModel userType)
        {
            if (!ModelState.IsValid)
            {
                ErrorNotification("Kayıt Eklenemedi!");
                return RedirectToAction("Create");
            }
            _userTypeService.Add(new UserType
            {
                //Alanlar buraya yazılacak
                //Örn:BrandName = brand.BrandName,
                
            });
            SuccessNotification("Kayıt Eklendi.");
            return RedirectToAction("UserTypeIndex");
        }
        // GET: Edit
        [SecuredOperation(Roles = "SystemAdmin,Admin")]
        public ActionResult Edit(int id)
        {
            var data = AutoMapperHelper.MapToSameViewModel<UserType, UserTypeViewModel>(_userTypeService.GetById(id));
            return View(data.ToVM());
        }
        // POST: Edit
        [HttpPost]
        public ActionResult Edit( UserType userType)
        {
            try
            {
                // TODO: Add update logic here
                _userTypeService.Update(new  UserType
                {
                 //Alanlar buraya yazılacak Id alanı en altta olacak unutmayın!!!
                 //Örn:BrandName = brand.BrandName,
                // //BrandId = brand.BrandId
                });
                SuccessNotification("Kayıt Güncellendi");
                return RedirectToAction("UserTypeIndex");
            }
            catch
            {
                return View();
            }
        }
         // GET: Delete
        [SecuredOperation(Roles = "SystemAdmin")]
        public ActionResult Delete(int id, UserType userType)
        {
            var data = AutoMapperHelper.MapToSameViewModel<UserType, UserTypeViewModel>(_userTypeService.GetById(id));
            return View(data.ToVM());
        }
        // POST: Delete
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {            
                _userTypeService.Delete(_userTypeService.GetById(id));
                SuccessNotification("Kayıt Silindi");
                return RedirectToAction("UserTypeIndex");
            }
            catch
            {
                return View();
            }
        }
    }
}