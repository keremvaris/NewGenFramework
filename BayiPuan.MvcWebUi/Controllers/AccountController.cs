using System;
using System.Collections.Specialized;
using System.Data.Entity;
using System.Linq;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;
using BayiPuan.Business.Abstract;
using BayiPuan.Entities.ComplexTypes;
using BayiPuan.Entities.Concrete;
using BayiPuan.MvcWebUi.Filters;
using BayiPuan.MvcWebUi.GenericVM;
using BayiPuan.MvcWebUi.HtmlHelpers;
using BayiPuan.MvcWebUi.Infrastructure;
using BayiPuan.MvcWebUi.Models.ViewModels;
using NewGenFramework.Core.Aspects.Postsharp.AuthorizationAspects;
using NewGenFramework.Core.CrossCuttingConcerns.Security.Web;
using NewGenFramework.Core.DataAccess;
using NewGenFramework.Core.Utilities.Mappings;
using NonFactors.Mvc.Grid;


namespace BayiPuan.MvcWebUi.Controllers
{
  public class AccountController : BaseController
  {
    //TODO: constructor injection hell bundan kurtulmak için IServiceFactory yazıp oradan çekeceğim ninject de Type Factor Facility Nasıl yapılıyor araştıracağım. kullanımı _serviceFactory.CreateUserService().GetAll(); şeklinde olabilir.

    private readonly IUserService _userService;
    private readonly IUserTypeService _userTypeService;
    private readonly IQueryableRepository<User> _queryableRepository;
    private readonly IQueryableRepository<Seller> _sellerQueryableRepository;
    private readonly IQueryableRepository<vwRP_StockCount> _totalRowsRepository;

    //private readonly IServiceFactory _serviceFactory;

    public AccountController(IUserService userService, IUserTypeService userTypeService, IQueryableRepository<User> queryableRepository, IQueryableRepository<vwRP_StockCount> totalRowsRepository, IQueryableRepository<Seller> sellerQueryableRepository
      /* , IServiceFactory serviceFactory*/)
    {
      _userService = userService;
      _userTypeService = userTypeService;
      _queryableRepository = queryableRepository;
      _totalRowsRepository = totalRowsRepository;
      _sellerQueryableRepository = sellerQueryableRepository;
      //_serviceFactory = serviceFactory;
    }

    private int pageSize = 10;

    public int PageSize { get => pageSize; set => pageSize = value; }

    [SecuredOperation(Roles = "SystemAdmin,Admin")]
    [AuthorizationFilter]
    public ViewResult Index(Int32? page, Int32? rows)
    {
      IGrid<User> col = new Grid<User>(_queryableRepository.Table.Include("Seller").Where(x => x.State == false).OrderByDescending(x => x.UserId).Skip((page - 1 ?? 0) * (rows ?? 10)).Take(rows ?? 10).AsNoTracking());
      col.Query = new NameValueCollection(Request.QueryString);

      if (col.Query != null)
      {
        col = new Grid<User>(_queryableRepository.Table.Include("Seller").Where(x => x.State == false).OrderByDescending(x => x.UserId).AsNoTracking());
      }
      col.Columns.Add(x => "<a class=' fas fa-edit btn btn-warning btn-sm' title='Kullanıcıyı Onayla' href='UpdateActivation/" + x.UserId + "'> </a>" +
                           "<a class='actions fas fa-trash-alt btn btn-danger btn-sm' title='Başvurutyu Reddet' href='Delete/" + x.UserId + "'> </a>")
        .Encoded(false).Titled("işlemler").Filterable(false);
      col.Columns.Add(x => x.FirstName).Titled("Adı").MultiFilterable(true);
      col.Columns.Add(x => x.LastName).Titled("Soyadı");
      col.Columns.Add(x => x.MobilePhone).Titled("Cep No");
      col.Columns.Add(x => x.Email).Titled("Eposta");
      col.Columns.Add(x => x.State).Titled("Aktif mi?");
      col.Columns.Add(x => x.Seller.SellerName).Titled("Çalıştığı Firma");

      col.Pager = new GridPager<User>(col);
      col.Processors.Add(col.Pager);
      col.Pager.RowsPerPage = 10;
      col.EmptyText = "Gösterilecek Kayıt Yok :(";
      foreach (IGridColumn column in col.Columns)
      {
        column.IsFilterable = true;
        column.IsSortable = true;
      }
      var total = _totalRowsRepository.Table.Include("Seller").Where(x => x.TableName == "Users").Select(x => x.TableRows).First();
      ViewBag.totalRows = Convert.ToInt32(total);
      return View(col);
    }
    [SecuredOperation(Roles = "SystemAdmin,Admin")]
    [AuthorizationFilter]
    public ActionResult UserIndex(Int32? page, Int32? rows)
    {
      IGrid<User> col = new Grid<User>(_queryableRepository.Table.OrderByDescending(x => x.UserId).Skip((page - 1 ?? 0) * (rows ?? 10)).Take(rows ?? 10).AsNoTracking());
      col.Query = new NameValueCollection(Request.QueryString);

      if (col.Query != null)
      {
        col = new Grid<User>(_queryableRepository.Table.Include("Seller").OrderByDescending(x => x.UserId).AsNoTracking());
      }
      col.Columns.Add(x => "<a class=' fas fa-edit btn btn-warning btn-sm' title='Güncelle' href='Edit/" + x.UserId + "'> </a>" +
                           "<a class='actions fas fa-trash-alt btn btn-danger btn-sm' title='Sil' href='Delete/" + x.UserId + "'> </a>")
          .Encoded(false).Titled("işlemler").Filterable(false);
      col.Columns.Add(x => x.FirstName).Titled("Adı").MultiFilterable(true);
      col.Columns.Add(x => x.LastName).Titled("Soyadı");
      col.Columns.Add(x => x.MobilePhone).Titled("Cep No");
      col.Columns.Add(x => x.Email).Titled("Eposta");
      col.Columns.Add(x => x.State).Titled("Aktif mi?");
      col.Columns.Add(x => x.Seller.SellerName).Titled("Çalıştığı Firma");

      col.Pager = new GridPager<User>(col);
      col.Processors.Add(col.Pager);
      col.Pager.RowsPerPage = 10;
      col.EmptyText = "Gösterilecek Kayıt Yok :(";
      foreach (IGridColumn column in col.Columns)
      {
        column.IsFilterable = true;
        column.IsSortable = true;
      }
      var total = _totalRowsRepository.Table.Where(x => x.TableName == "Users").Select(x => x.TableRows).First();
      ViewBag.totalRows = Convert.ToInt32(total);
      return View(col);
    }
    [SecuredOperation(Roles = "SystemAdmin,Admin")]
    [AuthorizationFilter]
    public ActionResult Edit(int id)
    {
      var data = AutoMapperHelper.MapToSameViewModel<User, UserViewModel>(_userService.GetById(id));
      return View(data.ToVM());
    }

    // POST: GN_Firm/Edit/5
    [HttpPost]
    public ActionResult Edit(User user)
    {
      var uPwd = _userService.GetById(user.UserId);
      try
      {
        // TODO: Add update logic here
        _userService.Update(new User
        {
          Agreement = user.Agreement,
          BirthDate = user.BirthDate,
          Email = user.Email,
          FirstName = user.FirstName,
          LastName = user.LastName,
          MobilePhone = user.MobilePhone,
          State = user.State,
          UserName = user.UserName,
          UserTypeId = user.UserTypeId,
          UserId = user.UserId,
          Password = uPwd.Password,
          SellerId = user.SellerId,
          Contact = user.Contact
        });
        SuccessNotification("Kayıt Güncellendi");
        return RedirectToAction("UserIndex");
      }
      catch
      {
        return View();
      }
    }
    public ActionResult SignIn()
    {
      return View();
    }
    [HttpPost]

    public ActionResult SignIn(string userName, string password)
    {
      //string encoded = Crypto.SHA256(password);
      var user = _userService.GetByUserNameAndPassword(userName, password);

      if (user != null && user.Agreement == true)
      {
        Session["FullName"] = Session["FullName"] == null ? user.FirstName + " " + user.LastName : "";
        if (TempData != null)
        {
          TempData["userName"] = userName;
          TempData["password"] = password;
          return RedirectToAction("Login");
        }
        return RedirectToAction("Login");
      }
      TempData["mesaj"] = "Hatalı Kullanıcı Adı veya Parola!!!" + "<br/>ya da<br/>" + "Onaylanmamış Kullanıcı Girişi!!!";
      return View("SignIn");
    }

    public ActionResult Login(string userName, string password)
    {
      var user = _userService.GetByUserNameAndPassword(TempData["userName"].ToString(),
          TempData["password"].ToString());
      if (user != null && user.Agreement == true)
      {
        AuthenticationHelper.CreateAuthCookie(
            new Guid(),
            user.UserName,
            user.Email,
            DateTime.Now.AddDays(1),
            _userService.GetUserRoles(user).Select(u => u.RoleName).ToArray(),
            false,
            user.FirstName,
            user.LastName);
        return RedirectToAction("Index", "UserHome", new { id = user.UserId });
      }
      return Redirect("NotAuthorization");
    }

    public ActionResult SignOut()
    {
      Session.Clear();
      FormsAuthentication.SignOut();
      Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
      Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
      Response.Cache.SetNoStore();
      Session.Abandon();
      return RedirectToAction("SignIn", "Account", null);
    }

    public ActionResult NewUser(UserViewModel user)
    {
      var userType = _userTypeService.GetAll();
      var sellerList = _sellerQueryableRepository.Table.ToList();
      ViewData["UserTypeList"] = userType.Select(u => new SelectListItem { Value = u.UserTypeId.ToString(), Text = u.UserTypeName.ToString() });
      ViewData["SellerList"] = sellerList.Select(s => new SelectListItem { Value = s.SellerId.ToString(), Text = s.SellerName.ToString() }); ;
      return View();
    }

    [HttpPost]
    public ActionResult NewUser(UserViewModel user, string username, string email, int usertypeid)
    {
      var userType = _userTypeService.GetAll();
      ViewData["UserTypeList"] = userType.Select(u => new SelectListItem { Value = u.UserTypeId.ToString(), Text = u.UserTypeName.ToString() });
      var sellerList = _sellerQueryableRepository.Table.ToList();
      var seller = sellerList.Select(s => new SelectListItem { Value = s.SellerId.ToString(), Text = s.SellerName.ToString() });
      ViewData["SellerList"] = seller;
      if (!ModelState.IsValid)
      {
        ErrorNotification("Kayıt Başarısız!");
        return View();
      }
      var addUser = _userService.Add(
               new User
               {
                 UserName = user.UserName,
                 BirthDate = user.BirthDate,
                 Contact = user.Contact,
                 SellerId = user.SellerId,
                 Email = user.Email,
                 FirstName = user.FirstName,
                 LastName = user.LastName,
                 MobilePhone = user.MobilePhone,
                 Password = Crypto.SHA256("12345"),
                 State = false,
                 UserTypeId = usertypeid,
                 Agreement = user.Agreement
               });
      TempData["mesaj"] = "Kayıt Başarılı! Yönetici Tarafından Onayladığında Tarafınıza Bilgi Verilecektir.";
      return View("SignIn");
    }
    [SecuredOperation(Roles = "SystemAdmin")]
    [AuthorizationFilter]
    public string Add()
    {
      _userService.Add(new User
      {
        BirthDate = DateTime.Now,
        Email = "webyonett@gmail.com",
        FirstName = "Mahmut",
        LastName = "Deneme",
        Password = Crypto.SHA256("12345"),
        UserName = "Mahmut",
        MobilePhone = "1234567",
        UserTypeId = 1,

        State = false,
        UserImage = null,
        UserId = 1

      });
      return "Done!";
    }
    [SecuredOperation(Roles = "SystemAdmin")]
    [AuthorizationFilter]
    public ActionResult UpdateActivation(int id)
    {
      var model = _userService.GetById(id);
      model.State = true;
      _userService.Update(model);
      //TODO: email gönderim işi yapılacak

      //if (model.State == true)
      //{
      //    string body = $"Merhaba {model.FirstName + " " + model.LastName};<br><br>Hesabınızı aktifleştirilmiştir. Kullanıcı Adınız: {model.UserName}";
      //    var mailResult = MailHelper.SendMail(body, model.Email, "Hesap Aktifleştirme");
      //    if (mailResult == true)
      //        SuccessNotification("Kullanıcı Onaylandı ve Aktivasyon Maili Gönderildi");
      //    else
      //        ErrorNotification("Mail Gönderilemedi");
      //}
      SuccessNotification("Kullanıcı Onaylandı.");
      return RedirectToAction("Index", "Account");
    }
    [SecuredOperation(Roles = "SystemAdmin")]
    [AuthorizationFilter]
    public ActionResult Delete(int id)
    {
      _userService.Delete(_userService.GetById(id));
      SuccessNotification("Kullanıcı Başvuru Talebi Reddedildi!");
      return RedirectToAction("Index", "Account");
    }
    [AuthorizationFilter]
    public ActionResult ChangePassword()
    {
      return View();
    }
    [HttpPost]
    public ActionResult ChangePassword(string userName, string password)
    {
      var user = _userService.GetById(Convert.ToInt32(GeneralHelpers.GetUserId()));
      _userService.Update(new User
      {
        Agreement = user.Agreement,
        BirthDate = user.BirthDate,
        Email = user.Email,
        FirstName = user.FirstName,
        LastName = user.LastName,
        MobilePhone = user.MobilePhone,
        State = user.State,
        UserImage = user.UserImage,
        UserName = user.UserName,
        UserTypeId = user.UserTypeId,
        Password = Crypto.SHA256(password),
        SellerId = user.SellerId,
        UserId = user.UserId
      });
      SuccessNotification("Parolanız Değiştirildi! Lütfen Çıkış Yapıp Tekrar Giriniz");
      return RedirectToAction("SignOut", "Account");
    }
  }
}