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
  public class UserRoleController : BaseController
  {
    private readonly IUserRoleService _userRoleService;
    private readonly IQueryableRepository<UserRole> _queryableRepository;
    private readonly IQueryableRepository<vwRP_StockCount> _totalRowsRepository;
    public UserRoleController(IUserRoleService userRoleService, IQueryableRepository<UserRole> queryableRepository, IQueryableRepository<vwRP_StockCount> totalRowsRepository)
    {
      _userRoleService = userRoleService;
      _queryableRepository = queryableRepository;
      _totalRowsRepository = totalRowsRepository;
    }
    [SecuredOperation(Roles = "SystemAdmin")]
    public ActionResult UserRoleIndex(Int32? page, Int32? rows)
    {
      IGrid<UserRole> col = new Grid<UserRole>(_queryableRepository.Table.Include("User").Include("Role").OrderByDescending(x => x.RoleId).Skip((page - 1 ?? 0) * (rows ?? 10)).Take(rows ?? 10).AsNoTracking());
      col.Query = new NameValueCollection(Request.QueryString);

      if (col.Query != null)
      {
        col = new Grid<UserRole>(_queryableRepository.Table.Include("User").Include("Role").OrderByDescending(x => x.RoleId).AsNoTracking());
      }
      col.Columns.Add(x => "<a class=' fas fa-edit btn btn-warning btn-sm' title='Güncelle' href='Edit/" + x.RoleId + "'> </a>" +
                           "<a class='actions fas fa-trash-alt btn btn-danger btn-sm' title='Sil' href='Delete/" + x.RoleId + "'> </a>")
        .Encoded(false).Titled("işlemler").Filterable(false);
      //Görüntülenecek kolonları buraya yazacaksanız
      col.Columns.Add(x => x.User.UserName).Titled("Kullanıcı");
      col.Columns.Add(x => x.Role.RoleName).Titled("Rol");

      col.Pager = new GridPager<UserRole>(col);
      col.Processors.Add(col.Pager);
      col.Pager.RowsPerPage = 10;
      col.EmptyText = "Gösterilecek Kayıt Yok :(";
      foreach (IGridColumn column in col.Columns)
      {
        column.IsFilterable = true;
        column.IsSortable = true;
      }
      var total = _totalRowsRepository.Table.AsNoTracking().Where(x => x.TableName == "UserRoles").Select(x => x.TableRows).First();
      ViewBag.totalRows = Convert.ToInt32(total);
      return View(col);
    }

    [SecuredOperation(Roles = "SystemAdmin")]
    public ActionResult Create()
    {
      var empty = new UserRoleViewModel();

      var data = empty.ToVM();

      return View(data);
    }

    // POST: GN_Firm/Create
    [HttpPost]
    public ActionResult Create(UserRoleViewModel userrole)
    {
      if (!ModelState.IsValid)
      {
        ErrorNotification("Marka Eklenemedi!");
        return RedirectToAction("Create");
      }
      _userRoleService.Add(new UserRole
      {
        UserId = userrole.UserId,
        RoleId = userrole.RoleId

      });
      SuccessNotification("Kayıt Eklendi.");
      return RedirectToAction("Create");

    }
    // GET: Brand/Edit/5
    [SecuredOperation(Roles = "SystemAdmin")]
    public ActionResult Edit(int id)
    {
      var data = AutoMapperHelper.MapToSameViewModel<UserRole, UserRoleViewModel>(_userRoleService.GetById(id));
      return View(data.ToVM());
    }

    // POST: Brand/Edit/5
    [HttpPost]
    public ActionResult Edit(UserRoleViewModel userrole)
    {
      try
      {
        // TODO: Add update logic here
        _userRoleService.Update(new UserRole
        {
          UserId = userrole.UserId,
          RoleId = userrole.RoleId,
          UserRoleId = userrole.UserRoleId
        });
        SuccessNotification("Kayıt Güncellendi");
        return RedirectToAction("UserRoleIndex");
      }
      catch
      {
        return View();
      }
    }

    // GET: Brand/Delete/5
    [SecuredOperation(Roles = "SystemAdmin")]
    public ActionResult Delete(int id, UserRoleViewModel userrole)
    {
      var data = AutoMapperHelper.MapToSameViewModel<UserRole, UserRoleViewModel>(_userRoleService.GetById(id));
      //var data = Mapper.Map<Product, ProductViewModel>(_productService.GetById(id));
      return View(data.ToVM());
    }

    // POST: Brand/Delete/5
    [HttpPost]
    public ActionResult Delete(int id)
    {
      try
      {
        // TODO: Add delete logic here
        _userRoleService.Delete(_userRoleService.GetById(id));
        SuccessNotification("Kayıt Silindi");

        return RedirectToAction("UserRoleIndex");
      }
      catch
      {
        return View();
      }
    }
  }
}