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
    public class RoleController : BaseController
    {
        private readonly IRoleService _roleService;
        private readonly IQueryableRepository<Role> _queryableRepository;
        private readonly IQueryableRepository<vwRP_StockCount> _totalRowsRepository;
        public RoleController(IRoleService roleService, IQueryableRepository<Role> queryableRepository, IQueryableRepository<vwRP_StockCount> totalRowsRepository)
        {
            _roleService = roleService;
            _queryableRepository = queryableRepository;
            _totalRowsRepository = totalRowsRepository;
        }
    [SecuredOperation(Roles = "SystemAdmin")]
    // GET: List
        [SecuredOperation(Roles = "SystemAdmin")]
        public ActionResult RoleIndex(Int32? page, Int32? rows)
        {
            IGrid<Role> col = new Grid<Role>(_queryableRepository.Table.OrderByDescending(x => x.RoleId).Skip((page - 1 ?? 0) * (rows ?? 10)).Take(rows ?? 10).AsNoTracking());
            col.Query = new NameValueCollection(Request.QueryString);

            if (col.Query != null)
            {
                col = new Grid<Role>(_queryableRepository.Table.OrderByDescending(x => x.RoleId).AsNoTracking());
            }
            col.Columns.Add(x => "<a class=' fas fa-edit btn btn-warning btn-sm' title='Güncelle' href='Edit/" + x.RoleId + "'> </a>" +
                                 "<a class='actions fas fa-trash-alt btn btn-danger btn-sm' title='Sil' href='Delete/" + x.RoleId + "'> </a>")
                .Encoded(false).Titled("işlemler").Filterable(false);
            //Görüntülenecek kolonları buraya yazacaksanız
            col.Columns.Add(x => x.RoleId).Titled("RoleId").MultiFilterable(true);
      col.Columns.Add(x => x.RoleName).Titled("RoleName").MultiFilterable(true);

      col.Pager = new GridPager<Role>(col);
            col.Processors.Add(col.Pager);
            col.Pager.RowsPerPage = 10;
            col.EmptyText = "Gösterilecek Kayıt Yok :(";
            foreach (IGridColumn column in col.Columns)
            {
                column.IsFilterable = true;
                column.IsSortable = true;
            }
            var total = _totalRowsRepository.Table.AsNoTracking().Where(x => x.TableName == "Roles").Select(x => x.TableRows).First();
            ViewBag.totalRows = Convert.ToInt32(total);
            return View(col);
        }
         // GET: Create
        [SecuredOperation(Roles = "SystemAdmin")]
        public ActionResult Create()
        {
            var empty = new RoleViewModel();
            var data = empty.ToVM();
            return View(data);
        }
        // POST: Brand/Create
        [HttpPost]
        public ActionResult Create(RoleViewModel role)
        {
            if (!ModelState.IsValid)
            {
                ErrorNotification("Kayıt Eklenemedi!");
                return RedirectToAction("Create");
            }
            _roleService.Add(new Role
            {
              //Alanlar buraya yazılacak
              //Örn:BrandName = brand.BrandName,
              RoleName = role.RoleName
              
            });
            SuccessNotification("Kayıt Eklendi.");
            return RedirectToAction("RoleIndex");
        }
        // GET: Edit
        [SecuredOperation(Roles = "SystemAdmin")]
        public ActionResult Edit(int id)
        {
            var data = AutoMapperHelper.MapToSameViewModel<Role, RoleViewModel>(_roleService.GetById(id));
            return View(data.ToVM());
        }
        // POST: Edit
        [HttpPost]
        public ActionResult Edit( Role role)
        {
            try
            {
                // TODO: Add update logic here
                _roleService.Update(new  Role
                {
                 //Alanlar buraya yazılacak Id alanı en altta olacak unutmayın!!!
                 //Örn:BrandName = brand.BrandName,
                 ////BrandId = brand.BrandId
                  RoleName = role.RoleName,
                  RoleId = role.RoleId
                });
                SuccessNotification("Kayıt Güncellendi");
                return RedirectToAction("RoleIndex");
            }
            catch
            {
                return View();
            }
        }
         // GET: Delete
        [SecuredOperation(Roles = "SystemAdmin")]
        public ActionResult Delete(int id, Role role)
        {
            var data = AutoMapperHelper.MapToSameViewModel<Role, RoleViewModel>(_roleService.GetById(id));
            return View(data.ToVM());
        }
        // POST: Delete
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {            
                _roleService.Delete(_roleService.GetById(id));
                SuccessNotification("Kayıt Silindi");
                return RedirectToAction("RoleIndex");
            }
            catch
            {
                return View();
            }
        }
    }
}