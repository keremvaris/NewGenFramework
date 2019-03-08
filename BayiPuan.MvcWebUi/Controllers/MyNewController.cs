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
    public class MyNewController : BaseController
    {
        private readonly IMyNewService _myNewService;
        private readonly IQueryableRepository<MyNew> _queryableRepository;
        private readonly IQueryableRepository<vwRP_StockCount> _totalRowsRepository;
        public MyNewController(IMyNewService myNewService, IQueryableRepository<MyNew> queryableRepository, IQueryableRepository<vwRP_StockCount> totalRowsRepository)
        {
            _myNewService = myNewService;
            _queryableRepository = queryableRepository;
            _totalRowsRepository = totalRowsRepository;
        }
        // GET: List
        [SecuredOperation(Roles = "SystemAdmin")]
        public ActionResult MyNewIndex(Int32? page, Int32? rows)
        {
            IGrid<MyNew> col = new Grid<MyNew>(_queryableRepository.Table.OrderByDescending(x => x.NewsId).Skip((page - 1 ?? 0) * (rows ?? 10)).Take(rows ?? 10));
            col.Query = new NameValueCollection(Request.QueryString);

            if (col.Query != null)
            {
                col = new Grid<MyNew>(_queryableRepository.Table.OrderByDescending(x => x.NewsId));
            }
            col.Columns.Add(x => "<a class=' fas fa-edit btn btn-warning btn-sm' title='Güncelle' href='Edit/" + x.NewsId + "'> </a>" +
                                 "<a class='actions fas fa-trash-alt btn btn-danger btn-sm' title='Sil' href='Delete/" + x.NewsId + "'> </a>")
                .Encoded(false).Titled("işlemler").Filterable(false);
            //Görüntülenecek kolonları buraya yazacaksanız
            col.Columns.Add(x => x.NewsId).Titled("MyNewId").MultiFilterable(true);
          col.Columns.Add(x => x.NewsName).Titled("Haber Başlığı");
          col.Columns.Add(x => x.NewsType).Titled("Haber Kategorisi");
            col.Pager = new GridPager<MyNew>(col);
            col.Processors.Add(col.Pager);
            col.Pager.RowsPerPage = 10;
            col.EmptyText = "Gösterilecek Kayıt Yok :(";
            foreach (IGridColumn column in col.Columns)
            {
                column.IsFilterable = true;
                column.IsSortable = true;
            }
            var total = _totalRowsRepository.Table.Where(x => x.TableName == "MyNews").Select(x => x.TableRows).First();
            ViewBag.totalRows = Convert.ToInt32(total);
            return View(col);
        }
      [SecuredOperation(Roles = "SystemAdmin,Admin,User")]
      public ActionResult UserMyNewIndex(Int32? page, Int32? rows)
      {
        IGrid<MyNew> col = new Grid<MyNew>(_queryableRepository.Table.OrderByDescending(x => x.NewsId).Skip((page - 1 ?? 0) * (rows ?? 10)).Take(rows ?? 10));
        col.Query = new NameValueCollection(Request.QueryString);

        if (col.Query != null)
        {
          col = new Grid<MyNew>(_queryableRepository.Table.OrderByDescending(x => x.NewsId));
        }
        col.Columns.Add(x => "<a class=' fas fa-edit btn btn-warning btn-sm' title='Güncelle' href='/MyNew/Detail/" + x.NewsId + "'> </a>" )
          .Encoded(false).Titled("işlemler").Filterable(false);
        //Görüntülenecek kolonları buraya yazacaksanız
        col.Columns.Add(x => x.NewsName).Titled("Haber Başlığı");
        col.Pager = new GridPager<MyNew>(col);
        col.Processors.Add(col.Pager);
        col.Pager.RowsPerPage = 10;
        col.EmptyText = "Gösterilecek Kayıt Yok :(";
        foreach (IGridColumn column in col.Columns)
        {
          column.IsFilterable = true;
          column.IsSortable = true;
        }
        var total = _totalRowsRepository.Table.Where(x => x.TableName == "MyNews").Select(x => x.TableRows).First();
        ViewBag.totalRows = Convert.ToInt32(total);
        return View(col);
      }

      public ActionResult Detail(int id)
      {
        var newsDetail = _queryableRepository.Table.FirstOrDefault(x => x.NewsId == id);
        return View(newsDetail);
      }
    // GET: Create
    [SecuredOperation(Roles = "SystemAdmin")]
        public ActionResult Create()
        {
            var empty = new MyNewViewModel();
            var data = empty.ToVM();
            return View(data);
        }
        // POST: Brand/Create
        [HttpPost]
        public ActionResult Create(MyNewViewModel myNew)
        {
            if (!ModelState.IsValid)
            {
                ErrorNotification("Kayıt Eklenemedi!");
                return RedirectToAction("Create");
            }
            _myNewService.Add(new MyNew
            {
             NewsName = myNew.NewsName,
              NewsImage = myNew.NewsImage,
              NewsImageExt = ".png",
              Description = myNew.Description,
              NewsType = myNew.NewsType,
              IsActive = myNew.IsActive
                
            });
            SuccessNotification("Kayıt Eklendi.");
            return RedirectToAction("MyNewIndex");
        }
        // GET: Edit
        [SecuredOperation(Roles = "SystemAdmin")]
        public ActionResult Edit(int id)
        {
            var data = AutoMapperHelper.MapToSameViewModel<MyNew, MyNewViewModel>(_myNewService.GetById(id));
            return View(data.ToVM());
        }
        // POST: Edit
        [HttpPost]
        public ActionResult Edit(MyNewViewModel myNew)
        {
            try
            {                
                _myNewService.Update(new  MyNew
                {
                  NewsName = myNew.NewsName,
                  NewsImage = myNew.NewsImage,
                  NewsImageExt = ".png",
                  Description = myNew.Description,
                  NewsType = myNew.NewsType,
                  IsActive = myNew.IsActive,
                  NewsId = myNew.NewsId
                });
                SuccessNotification("Kayıt Güncellendi");
                return RedirectToAction("MyNewIndex");
            }
            catch
            {
                return View();
            }
        }
         // GET: Delete
        [SecuredOperation(Roles = "SystemAdmin")]
        public ActionResult Delete(int id, MyNewViewModel myNew)
        {
            var data = AutoMapperHelper.MapToSameViewModel<MyNew, MyNewViewModel>(_myNewService.GetById(id));
            return View(data.ToVM());
        }
        // POST: Delete
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {            
                _myNewService.Delete(_myNewService.GetById(id));
                SuccessNotification("Kayıt Silindi");
                return RedirectToAction("MyNewIndex");
            }
            catch
            {
                return View();
            }
        }
    }
}