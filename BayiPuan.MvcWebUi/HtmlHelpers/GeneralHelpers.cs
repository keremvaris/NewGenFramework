using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BayiPuan.Business.Abstract;
using BayiPuan.Business.DependencyResolvers.Ninject;
using BayiPuan.DataAccess.Concrete.Context;
using BayiPuan.Entities.ComplexTypes;
using BayiPuan.Entities.Concrete;
using BayiPuan.MvcWebUi.GenericVM;

namespace BayiPuan.MvcWebUi.HtmlHelpers
{
  [OutputCache(VaryByParam = "none", Duration = 3600)]
  public class GeneralHelpers
  {
    public static string GetUserId()
    {
      if (HttpContext.Current.User.Identity.IsAuthenticated == true)
      {
        BayiPuanContext db = new BayiPuanContext();
        var user = db.Users.AsNoTracking().FirstOrDefault(x => x.UserName == HttpContext.Current.User.Identity.Name);
        return user.UserId.ToString();
      }

      return null;
    }
    public static string GetUserFacility()
    {
      if (HttpContext.Current.User.Identity.IsAuthenticated == true)
      {
        BayiPuanContext db = new BayiPuanContext();
        var user = db.Users.AsNoTracking().FirstOrDefault(x => x.UserName == HttpContext.Current.User.Identity.Name);
       var facility= db.Sellers.AsNoTracking().FirstOrDefault(x => x.SellerId == user.SellerId);
        return facility.SellerName;
      }

      return null;
    }
    public static decimal GetRemainingPoint()
    {
      var product = DependencyResolver<IProductService>.Resolve();
      var sale = DependencyResolver<ISaleService>.Resolve();
      var buyGift = DependencyResolver<IBuyService>.Resolve();
      var gift = DependencyResolver<IGiftService>.Resolve();
      var score = DependencyResolver<IScoreService>.Resolve();
      var totalPoint = (from s in score.GetAll()
                        select new
                        {
                          s.UserId,
                          s.ScoreTotal
                        }).Where(p => p.UserId == Convert.ToInt32(GetUserId()))
        .GroupBy(w => w.UserId)
        .Select(y => new ProductPoint
        {
          UserId = y.Key,
          SumPoint = y.Sum(x => x.ScoreTotal),
          //SumPointToMoney = y.Sum(x => x.AmountOfSales * x.Point * x.PointToMoney)
        }).ToList();

      var totalSpent = (from b in buyGift.GetAll()
                        join g in gift.GetAll() on b.GiftId equals g.GiftId
                        select new
                        {
                          b.UserId,
                          b.IsApproved,
                          g.GiftPoint
                        }).Where(b => b.UserId == Convert.ToInt32(GetUserId()) && b.IsApproved == true)
        .GroupBy(w => w.UserId)
        .Select(y => new SpentPoint
        {
          UserId = y.Key,
          SpendPoint = y.Sum(x => x.GiftPoint)
        }).ToList();

      decimal remainingPoint = totalPoint.Sum(p=>p.SumPoint) - totalSpent.Sum(s=>s.SpendPoint);
      return remainingPoint;
    }
  }
}