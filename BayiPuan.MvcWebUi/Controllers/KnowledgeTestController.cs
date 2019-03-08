using System;
using System.Collections.Generic;
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
using BayiPuan.MvcWebUi.HtmlHelpers;
using BayiPuan.MvcWebUi.Infrastructure;
using BayiPuan.MvcWebUi.Models.ViewModels;

namespace BayiPuan.MvcWebUi.Controllers
{
  [AuthorizationFilter]
  public class KnowledgeTestController : BaseController
  {
    private readonly IKnowledgeTestService _knowledgeTestService;
    private readonly IAnswerService _answerService;
    private readonly IScoreService _scoreService;
    private readonly IQueryableRepository<Answer> _answerQueryableRepository;
    private readonly IQueryableRepository<KnowledgeTest> _queryableRepository;
    private readonly IQueryableRepository<vwRP_StockCount> _totalRowsRepository;
    public KnowledgeTestController(IKnowledgeTestService knowledgeTestService, IQueryableRepository<KnowledgeTest> queryableRepository, IQueryableRepository<vwRP_StockCount> totalRowsRepository, IQueryableRepository<Answer> answerQueryableRepository, IAnswerService answerService, IScoreService scoreService)
    {
      _knowledgeTestService = knowledgeTestService;
      _queryableRepository = queryableRepository;
      _totalRowsRepository = totalRowsRepository;
      _answerQueryableRepository = answerQueryableRepository;
      _answerService = answerService;
      _scoreService = scoreService;
    }
    // GET: List
    [SecuredOperation(Roles = "SystemAdmin")]
    public ActionResult KnowledgeTestIndex(Int32? page, Int32? rows)
    {
      IGrid<KnowledgeTest> col;
      col = new Grid<KnowledgeTest>(_queryableRepository.Table.OrderByDescending(x => x.KnowledgeTestId)
        .Skip((page - 1 ?? 0) * (rows ?? 10)).Take(rows ?? 10)) {Query = new NameValueCollection(Request.QueryString)};

      if (col.Query != null)
      {
        col = new Grid<KnowledgeTest>(_queryableRepository.Table.OrderByDescending(x => x.KnowledgeTestId));
      }
      col.Columns.Add(x => "<a class=' fas fa-edit btn btn-warning btn-sm' title='Güncelle' href='Edit/" + x.KnowledgeTestId + "'> </a>" +
                           "<a class='actions fas fa-trash-alt btn btn-danger btn-sm' title='Sil' href='Delete/" + x.KnowledgeTestId + "'> </a>")
          .Encoded(false).Titled("işlemler").Filterable(false);
      //Görüntülenecek kolonları buraya yazacaksanız
      col.Columns.Add(x => x.KnowledgeTestId).Titled("KnowledgeTestId").MultiFilterable(true);
      col.Columns.Add(x => x.Question).Titled("Soru");
      col.Columns.Add(x => x.ValidAnswerType).Titled("Doğru Cevap");
      col.Columns.Add(x => x.Point).Titled("Soru Puanı");
      col.Pager = new GridPager<KnowledgeTest>(col);
      col.Processors.Add(col.Pager);
      col.Pager.RowsPerPage = 10;
      col.EmptyText = "Gösterilecek Kayıt Yok :(";
      foreach (IGridColumn column in col.Columns)
      {
        column.IsFilterable = true;
        column.IsSortable = true;
      }
      var total = _totalRowsRepository.Table.Where(x => x.TableName == "KnowledgeTests").Select(x => x.TableRows).First();
      ViewBag.totalRows = Convert.ToInt32(total);
      return View(col);
    }
    [SecuredOperation(Roles = "SystemAdmin,Admin,User")]
    public ActionResult Question()
    {
      var date = DateTime.Now.ToShortDateString();
      var userId = Convert.ToInt32(GeneralHelpers.GetUserId());
      var quiz = _knowledgeTestService.GetAll().Where(x => (Convert.ToDateTime(x.KnowledgeDate.ToShortDateString()) == Convert.ToDateTime(date))).ToList();
      var answer = _answerQueryableRepository.Table.Where(x => x.UserId == userId).ToList();
      List<KnowledgeTest> quizList = new List<KnowledgeTest>();
      if (answer.Count == 0)
      {
        return View(quiz);
      }
      foreach (var quizItem in quiz)
      {
        foreach (var answerItem in answer)
        {
          if (answer.Count!=quiz.Count)
          {
            if (quizItem.KnowledgeTestId != answerItem.KnowledgeTestId)
            {
              quizList.Add(_knowledgeTestService.GetById(answerItem.KnowledgeTestId));
            }
          }
        }
      }
      return View(quizList);
    }
    [HttpPost]
    public ActionResult Question(FormCollection form, int questionid)
    {
      var formId = _queryableRepository.Table.FirstOrDefault(x => x.KnowledgeTestId == questionid);
      var keys = Request.Form.AllKeys;
      var reqForm = form["item.Question"];
      var date = DateTime.Now.ToShortDateString();
      var quiz = _knowledgeTestService.GetAll().Where(x => (Convert.ToDateTime(x.KnowledgeDate.ToShortDateString()) == Convert.ToDateTime(date)));
      var quizList = quiz.ToList();
      if (reqForm == formId.ValidAnswerType)
      {
        _answerService.Add(new Answer
        {
          UserId = Convert.ToInt32(GeneralHelpers.GetUserId()),
          KnowledgeTestId = questionid,
          AnswerDate = DateTime.Now,
          ValidAnswer = true
        });
        _scoreService.Add(new Score
        {
          UserId = Convert.ToInt32(GeneralHelpers.GetUserId()),
          ScoreTotal = formId.Point,
          ScoreType = ScoreType.BilgiModulu,
        });
        SuccessNotification("Tebrikler! Cevabınız Doğru.");
      }
      else
      {
        _answerService.Add(new Answer
        {
          UserId = Convert.ToInt32(GeneralHelpers.GetUserId()),
          KnowledgeTestId = questionid,
          AnswerDate = DateTime.Now,
          ValidAnswer = false
        });
        ErrorNotification("Üzgünüm Doğru Cevap " + formId.ValidAnswerType + " Olmalıydı :(");
      }

      return RedirectToAction("Question");
    }
    // GET: Create
    [SecuredOperation(Roles = "SystemAdmin")]
    public ActionResult Create()
    {
      var empty = new KnowledgeTestViewModel();
      var data = empty.ToVM();
      return View(data);
    }
    // POST: Brand/Create
    [HttpPost]
    public ActionResult Create(KnowledgeTestViewModel knowledgeTest)
    {
      if (!ModelState.IsValid)
      {
        ErrorNotification("Kayıt Eklenemedi!");
        return RedirectToAction("Create");
      }
      _knowledgeTestService.Add(new KnowledgeTest
      {
        Question = knowledgeTest.Question,
        Answer1 = knowledgeTest.Answer1,
        Answer2 = knowledgeTest.Answer2,
        Answer3 = knowledgeTest.Answer3,
        Answer4 = knowledgeTest.Answer4,
        KnowledgeDate = DateTime.Now,
        Point = knowledgeTest.Point,
        IsActive = knowledgeTest.IsActive,
        ValidAnswerType = knowledgeTest.ValidAnswerType
      });
      SuccessNotification("Kayıt Eklendi.");
      return RedirectToAction("KnowledgeTestIndex");
    }
    // GET: Edit
    [SecuredOperation(Roles = "SystemAdmin")]
    public ActionResult Edit(int id)
    {
      var data = AutoMapperHelper.MapToSameViewModel<KnowledgeTest, KnowledgeTestViewModel>(_knowledgeTestService.GetById(id));
      return View(data.ToVM());
    }
    // POST: Edit
    [HttpPost]
    public ActionResult Edit(KnowledgeTest knowledgeTest)
    {
      try
      {
        _knowledgeTestService.Update(new KnowledgeTest
        {
          Question = knowledgeTest.Question,
          Answer1 = knowledgeTest.Answer1,
          Answer2 = knowledgeTest.Answer2,
          Answer3 = knowledgeTest.Answer3,
          Answer4 = knowledgeTest.Answer4,
          KnowledgeDate = DateTime.Now,
          Point = knowledgeTest.Point,
          IsActive = knowledgeTest.IsActive,
          ValidAnswerType = knowledgeTest.ValidAnswerType,
          KnowledgeTestId = knowledgeTest.KnowledgeTestId
        });
        SuccessNotification("Kayıt Güncellendi");
        return RedirectToAction("KnowledgeTestIndex");
      }
      catch
      {
        return View();
      }
    }
    // GET: Delete
    [SecuredOperation(Roles = "SystemAdmin")]
    public ActionResult Delete(int id, KnowledgeTest knowledgeTest)
    {
      var data = AutoMapperHelper.MapToSameViewModel<KnowledgeTest, KnowledgeTestViewModel>(_knowledgeTestService.GetById(id));
      return View(data.ToVM());
    }
    // POST: Delete
    [HttpPost]
    public ActionResult Delete(int id)
    {
      try
      {
        _knowledgeTestService.Delete(_knowledgeTestService.GetById(id));
        SuccessNotification("Kayıt Silindi");
        return RedirectToAction("KnowledgeTestIndex");
      }
      catch
      {
        return View();
      }
    }
  }
}