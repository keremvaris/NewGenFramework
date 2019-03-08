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
using BayiPuan.MvcWebUi.GenericVM;
using BayiPuan.MvcWebUi.Filters;
using BayiPuan.MvcWebUi.Infrastructure;
using BayiPuan.MvcWebUi.Models.ViewModels;

namespace BayiPuan.MvcWebUi.Controllers
{
  [AuthorizationFilter]
  public class CustomerController : BaseController
  {
    private readonly ICustomerService _customerService;
    private readonly IQueryableRepository<Customer> _queryableRepository;
    private readonly IQueryableRepository<vwRP_StockCount> _totalRowsRepository;
    private readonly IUserService _userService;
    public CustomerController(ICustomerService customerService, IQueryableRepository<Customer> queryableRepository, IQueryableRepository<vwRP_StockCount> totalRowsRepository, IUserService userService)
    {
      _customerService = customerService;
      _queryableRepository = queryableRepository;
      _totalRowsRepository = totalRowsRepository;
      _userService = userService;
      
    }
    
    // GET: List
    [SecuredOperation(Roles = "SystemAdmin,Admin,User")]
    public ActionResult CustomerIndex(Int32? page, Int32? rows)
    {
      var getUserId = _userService.UniqueUserName(User.Identity.Name);
      IGrid<Customer> col;
      col = new Grid<Customer>(_queryableRepository.Table.Where(x => x.AddingUserId == getUserId.UserId)
        .OrderByDescending(x => x.CustomerId).Skip((page - 1 ?? 0) * (rows ?? 10)).Take(rows ?? 10).AsNoTracking())
      {
        Query = new NameValueCollection(Request.QueryString)
      };

      if (col.Query != null)
      {
        col = new Grid<Customer>(_queryableRepository.Table.Where(x => x.AddingUserId == getUserId.UserId).OrderByDescending(x => x.CustomerId).AsNoTracking());
      }
      col.Columns.Add(x => "<a class=' fas fa-edit btn btn-warning btn-sm' title='Güncelle' href='Edit/" + x.CustomerId + "'> </a>" +
                           "<a class='actions fas fa-trash-alt btn btn-danger btn-sm' title='Sil' href='Delete/" + x.CustomerId + "'> </a>")
          .Encoded(false).Titled("işlemler").Filterable(false);
      //Görüntülenecek kolonları buraya yazacaksanız
      //col.Columns.Add(x => x.CustomerId).Titled("CustomerId").MultiFilterable(true);
      col.Columns.Add(x => x.CustomerName).Titled("Müşteri Adı").MultiFilterable(true);
      col.Columns.Add(x => x.TaxAdministration).Titled("Vergi Dairesi");
      col.Columns.Add(x => x.TaxNo).Titled("Vergi No");
      col.Columns.Add(x => x.RelationalPersonName).Titled("Yetkili Kişi Adı");
      col.Columns.Add(x => x.RelationalPersonSurname).Titled("Yetkili Kişi Soyadı");
      col.Columns.Add(x => x.MobilePhone).Titled("İletişim Telefonu");


      col.Pager = new GridPager<Customer>(col);
      col.Processors.Add(col.Pager);
      col.Pager.RowsPerPage = 10;
      col.EmptyText = "Gösterilecek Kayıt Yok :(";
      foreach (IGridColumn column in col.Columns)
      {
        column.IsFilterable = true;
        column.IsSortable = true;
      }
      var total = _totalRowsRepository.Table.AsNoTracking().Where(x => x.TableName == "Customers").Select(x => x.TableRows).First();
      ViewBag.totalRows = Convert.ToInt32(total);
      return View(col);
    }
    // GET: Create
    [SecuredOperation(Roles = "SystemAdmin,Admin,User")]
    public ActionResult Create()
    {
      var empty = new CustomerViewModel();
      var data = empty.ToVM();
      return View(data);
    }
    // POST: Brand/Create
    [HttpPost]
    public ActionResult Create(CustomerViewModel customer)
    {
      var getUserId = _userService.UniqueUserName(User.Identity.Name);
      if (!ModelState.IsValid)
      {
        ErrorNotification("Kayıt Eklenemedi!");
        return RedirectToAction("Create");
      }
      _customerService.Add(new Customer
      {
        RelationalPersonName = customer.RelationalPersonName,
        MobilePhone = customer.MobilePhone,
        RelationalPersonSurname = customer.RelationalPersonSurname,
        CustomerName = customer.CustomerName,
        TaxNo = customer.TaxNo,
        TaxAdministration = customer.TaxAdministration,
        DateAdded = DateTime.Now,
        State = true,
        AddingUserId = getUserId.UserId
        
       
      });
      SuccessNotification("Kayıt Eklendi.");
      return RedirectToAction("CustomerIndex");
    }
    // GET: Edit
    [SecuredOperation(Roles = "SystemAdmin,Admin,User")]
    public ActionResult Edit(int id)
    {
      var data = AutoMapperHelper.MapToSameViewModel<Customer, CustomerViewModel>(_customerService.GetById(id));
      return View(data.ToVM());
    }
    // POST: Edit
    [HttpPost]
    public ActionResult Edit(Customer customer)
    {
      var getUserId = _userService.UniqueUserName(User.Identity.Name);
      try
      {
        // TODO: Add update logic here
        _customerService.Update(new Customer
        {
          RelationalPersonName = customer.RelationalPersonName,
          MobilePhone = customer.MobilePhone,
          RelationalPersonSurname = customer.RelationalPersonSurname,
          CustomerName = customer.CustomerName,
          TaxNo = customer.TaxNo,
          TaxAdministration = customer.TaxAdministration,
          DateAdded = DateTime.Now,
          State = true,
          AddingUserId = getUserId.UserId,
          CustomerId = customer.CustomerId
        });
        SuccessNotification("Kayıt Güncellendi");
        return RedirectToAction("CustomerIndex");
      }
      catch
      {
        return View();
      }
    }
    // GET: Delete
    [SecuredOperation(Roles = "SystemAdmin,Admin")]
    public ActionResult Delete(int id, Customer customer)
    {
      var data = AutoMapperHelper.MapToSameViewModel<Customer, CustomerViewModel>(_customerService.GetById(id));
      return View(data.ToVM());
    }
    // POST: Delete
    [HttpPost]
    public ActionResult Delete(int id)
    {
      try
      {
        _customerService.Delete(_customerService.GetById(id));
        SuccessNotification("Kayıt Silindi");
        return RedirectToAction("CustomerIndex");
      }
      catch
      {
        return View();
      }
    }
  }
}