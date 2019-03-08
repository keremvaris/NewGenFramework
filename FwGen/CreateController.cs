using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;

namespace FwGen
{
    public class CreateController
    {
        List<Type> types = new List<Type>();
        public void Add<T>()
        {
            Add(typeof(T));
        }

        public void Add(Type t)
        {
            if (!types.Contains(t))
                types.Add(t);

        }

        public void Generate(string path)
        {
            if (!path.EndsWith("\\")) path += "\\";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            GenerateClassFiles(path);
        }

        private void GenerateClassFiles(string path)
        {
            foreach (var type in types)
            {
                var content = GenerateClassFilesType(type);
                if (!type.FullName.Contains("ComplexType"))
                    File.WriteAllText(path + type.Name + "Controller.cs", content, System.Text.Encoding.UTF8);
            }
        }

        private string GenerateClassFilesType(Type type)
        {
            var projectName = Form1.frm.txtProjectName.Text;

            return fmtClassFile
                .Replace("[ClassName]", type.Name)
                .Replace("[ClassToTitleCase]", type.Name.Substring(0,1).ToLower()+type.Name.Substring(1,type.Name.Length-1))
                .Replace("[ProjectName]", projectName);

        }

    

        private const string fmtClassFile = @"using System;
using System.Collections.Specialized;
using System.Linq;
using System.Web.Mvc;
using NewGenFramework.Core.Aspects.Postsharp.AuthorizationAspects;
using NewGenFramework.Core.DataAccess;
using NewGenFramework.Core.Utilities.Mappings;
using NonFactors.Mvc.Grid;
using [ProjectName].Business.Abstract;
using [ProjectName].Entities.ComplexTypes;
using [ProjectName].Entities.Concrete;
using [ProjectName].MvcWebUi.GenericVM;
using [ProjectName].MvcWebUi.Filters;
using [ProjectName].MvcWebUi.Infrastructure;
using [ProjectName].MvcWebUi.Models.ViewModels;

namespace [ProjectName].MvcWebUi.Controllers
{
    [AuthorizationFilter]
    public class [ClassName]Controller : BaseController
    {
        private readonly I[ClassName]Service _[ClassToTitleCase]Service;
        private readonly IQueryableRepository<[ClassName]> _queryableRepository;
        private readonly IQueryableRepository<vwRP_StockCount> _totalRowsRepository;
        public [ClassName]Controller(I[ClassName]Service [ClassToTitleCase]Service, IQueryableRepository<[ClassName]> queryableRepository, IQueryableRepository<vwRP_StockCount> totalRowsRepository)
        {
            _[ClassToTitleCase]Service = [ClassToTitleCase]Service;
            _queryableRepository = queryableRepository;
            _totalRowsRepository = totalRowsRepository;
        }
        // GET: List
        [SecuredOperation(Roles = ""SystemAdmin"")]
        public ActionResult [ClassName]Index(Int32? page, Int32? rows)
        {
            IGrid<[ClassName]> col = new Grid<[ClassName]>(_queryableRepository.Table.OrderByDescending(x => x.[ClassName]Id).Skip((page - 1 ?? 0) * (rows ?? 10)).Take(rows ?? 10));
            col.Query = new NameValueCollection(Request.QueryString);

            if (col.Query != null)
            {
                col = new Grid<[ClassName]>(_queryableRepository.Table.OrderByDescending(x => x.[ClassName]Id));
            }
            col.Columns.Add(x => ""<a class=' fas fa-edit btn btn-warning btn-sm' title='Güncelle' href='Edit/"" + x.[ClassName]Id + ""'> </a>"" +
                                 ""<a class='actions fas fa-trash-alt btn btn-danger btn-sm' title='Sil' href='Delete/"" + x.[ClassName]Id + ""'> </a>"")
                .Encoded(false).Titled(""işlemler"").Filterable(false);
            //Görüntülenecek kolonları buraya yazacaksanız
            col.Columns.Add(x => x.[ClassName]Id).Titled(""[ClassName]Id"").MultiFilterable(true);            

            col.Pager = new GridPager<[ClassName]>(col);
            col.Processors.Add(col.Pager);
            col.Pager.RowsPerPage = 10;
            col.EmptyText = ""Gösterilecek Kayıt Yok :("";
            foreach (IGridColumn column in col.Columns)
            {
                column.IsFilterable = true;
                column.IsSortable = true;
            }
            var total = _totalRowsRepository.Table.Where(x => x.TableName == ""[ClassName]s"").Select(x => x.TableRows).First();
            ViewBag.totalRows = Convert.ToInt32(total);
            return View(col);
        }
         // GET: Create
        [SecuredOperation(Roles = ""SystemAdmin"")]
        public ActionResult Create()
        {
            var empty = new [ClassName]ViewModel();
            var data = empty.ToVM();
            return View(data);
        }
        // POST: Brand/Create
        [HttpPost]
        public ActionResult Create([ClassName]ViewModel [ClassToTitleCase])
        {
            if (!ModelState.IsValid)
            {
                ErrorNotification(""Kayıt Eklenemedi!"");
                return RedirectToAction(""Create"");
            }
            _[ClassToTitleCase]Service.Add(new [ClassName]
            {
                //TODO:Alanlar buraya yazılacak
                //Örn:BrandName = brand.BrandName,
                
            });
            SuccessNotification(""Kayıt Eklendi."");
            return RedirectToAction(""[ClassName]Index"");
        }
        // GET: Edit
        [SecuredOperation(Roles = ""SystemAdmin"")]
        public ActionResult Edit(int id)
        {
            var data = AutoMapperHelper.MapToSameViewModel<[ClassName], [ClassName]ViewModel>(_[ClassToTitleCase]Service.GetById(id));
            return View(data.ToVM());
        }
        // POST: Edit
        [HttpPost]
        public ActionResult Edit( [ClassName] [ClassToTitleCase])
        {
            try
            {                
                _[ClassToTitleCase]Service.Update(new  [ClassName]
                {
                 //TODO:Alanlar buraya yazılacak Id alanı en altta olacak unutmayın!!!
                 //Örn:BrandName = brand.BrandName,
                 //BrandId = brand.BrandId
                  [ClassName]Id = [ClassToTitleCase].[ClassName]Id
                });
                SuccessNotification(""Kayıt Güncellendi"");
                return RedirectToAction(""[ClassName]Index"");
            }
            catch
            {
                return View();
            }
        }
         // GET: Delete
        [SecuredOperation(Roles = ""SystemAdmin"")]
        public ActionResult Delete(int id, [ClassName] [ClassToTitleCase])
        {
            var data = AutoMapperHelper.MapToSameViewModel<[ClassName], [ClassName]ViewModel>(_[ClassToTitleCase]Service.GetById(id));
            return View(data.ToVM());
        }
        // POST: Delete
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {            
                _[ClassToTitleCase]Service.Delete(_[ClassToTitleCase]Service.GetById(id));
                SuccessNotification(""Kayıt Silindi"");
                return RedirectToAction(""[ClassName]Index"");
            }
            catch
            {
                return View();
            }
        }
    }
}";
    }
}
