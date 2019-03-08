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
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IQueryableRepository<User> _queryableRepository;
        private readonly IQueryableRepository<vwRP_StockCount> _totalRowsRepository;
        public UserController(IUserService userService, IQueryableRepository<User> queryableRepository, IQueryableRepository<vwRP_StockCount> totalRowsRepository)
        {
            _userService = userService;
            _queryableRepository = queryableRepository;
            _totalRowsRepository = totalRowsRepository;
        }
    // GET: List
        [SecuredOperation(Roles = "SystemAdmin")]
        public ActionResult UserIndex(Int32? page, Int32? rows)
        {
            IGrid<User> col = new Grid<User>(_queryableRepository.Table.OrderByDescending(x => x.UserId).Skip((page - 1 ?? 0) * (rows ?? 10)).Take(rows ?? 10).AsNoTracking());
            col.Query = new NameValueCollection(Request.QueryString);

            if (col.Query != null)
            {
                col = new Grid<User>(_queryableRepository.Table.OrderByDescending(x => x.UserId).AsNoTracking());
            }
            col.Columns.Add(x => "<a class=' fas fa-edit btn btn-warning btn-sm' title='Güncelle' href='Edit/" + x.UserId + "'> </a>" +
                                 "<a class='actions fas fa-trash-alt btn btn-danger btn-sm' title='Sil' href='Delete/" + x.UserId + "'> </a>")
                .Encoded(false).Titled("işlemler").Filterable(false);
            //Görüntülenecek kolonları buraya yazacaksanız
            col.Columns.Add(x => x.UserId).Titled("UserId").MultiFilterable(true);            

            col.Pager = new GridPager<User>(col);
            col.Processors.Add(col.Pager);
            col.Pager.RowsPerPage = 10;
            col.EmptyText = "Gösterilecek Kayıt Yok :(";
            foreach (IGridColumn column in col.Columns)
            {
                column.IsFilterable = true;
                column.IsSortable = true;
            }
            var total = _totalRowsRepository.Table.AsNoTracking().Where(x => x.TableName == "Users").Select(x => x.TableRows).First();
            ViewBag.totalRows = Convert.ToInt32(total);
            return View(col);
        }
         // GET: Create
        [SecuredOperation(Roles = "SystemAdmin")]
        public ActionResult Create()
        {
            var empty = new UserViewModel();
            var data = empty.ToVM();
            return View(data);
        }
        // POST: Brand/Create
        [HttpPost]
        public ActionResult Create(UserViewModel user)
        {
            if (!ModelState.IsValid)
            {
                ErrorNotification("Kayıt Eklenemedi!");
                return RedirectToAction("Create");
            }
            _userService.Add(new User
            {
                //Alanlar buraya yazılacak
                //Örn:BrandName = brand.BrandName,
                
            });
            SuccessNotification("Kayıt Eklendi.");
            return RedirectToAction("UserIndex");
        }
        // GET: Edit
        [SecuredOperation(Roles = "SystemAdmin")]
        public ActionResult Edit(int id)
        {
            var data = AutoMapperHelper.MapToSameViewModel<User, UserViewModel>(_userService.GetById(id));
            return View(data.ToVM());
        }
        // POST: Edit
        [HttpPost]
        public ActionResult Edit( User user)
        {
            try
            {
                // TODO: Add update logic here
                _userService.Update(new  User
                {
                 //Alanlar buraya yazılacak Id alanı en altta olacak unutmayın!!!
                 //Örn:BrandName = brand.BrandName,
                 ////BrandId = brand.BrandId
                });
                SuccessNotification("Kayıt Güncellendi");
                return RedirectToAction("UserIndex");
            }
            catch
            {
                return View();
            }
        }
         // GET: Delete
        [SecuredOperation(Roles = "SystemAdmin")]
        public ActionResult Delete(int id, User user)
        {
            var data = AutoMapperHelper.MapToSameViewModel<User, UserViewModel>(_userService.GetById(id));
            return View(data.ToVM());
        }
        // POST: Delete
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {            
                _userService.Delete(_userService.GetById(id));
                SuccessNotification("Kayıt Silindi");
                return RedirectToAction("UserIndex");
            }
            catch
            {
                return View();
            }
        }
    }
}