using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web.Mvc;
using BayiPuan.DataAccess.Concrete.Context;
using BayiPuan.MvcWebUi.Infrastructure;
using BayiPuan.Entities.Concrete;
using DotNet.Highcharts.Enums;
using DotNet.Highcharts.Helpers;
using DotNet.Highcharts.Options;


namespace BayiPuan.MvcWebUi.Controllers
{
  public class ChartDemoController : BaseController
  {
    public class ChartDemo
    {
      public double Count { get; set; }
      public string UserName { get; set; }
    }
    // GET: ChartDemo
    public ActionResult Index()
    {
      var table = new DataTable();
      var dt = @"select FirstName +' '+ LastName UserName,sum(AmountOfSales) Count from Users u  
inner join Sales s on  s.UserId=u.UserId 
Where s.IsApproved=1
group by FirstName,LastName order by Count desc";
      using (var dbx = new BayiPuanContext())
      {
        var cmd = dbx.Database.Connection.CreateCommand();
        var sql = dt;
        cmd.CommandText = sql;
        cmd.Connection.Open();
        table.Load(cmd.ExecuteReader());
        DataSet ds = new DataSet();
        ds.Tables.Add(table);

        DotNet.Highcharts.Highcharts chart = new DotNet.Highcharts.Highcharts("chart")
          .InitChart(new Chart
          {
            DefaultSeriesType = ChartTypes.Column

          })

          .SetTitle(new Title
          {
            Text = "Satış Raporu"
          })
          .SetXAxis(new XAxis
          {
            //Categories = new[] { "Ocak", "Şubat", "Mart", "Nisan", "Mayıs", "Haziran", "Temmuz", "Ağustos", "Eylül", "Ekim", "Kasım", "Aralık" }
            Categories = table.AsEnumerable().Select(r => r.Field<string>("UserName").ToString()).ToArray()

          })
          .SetYAxis(new YAxis
          {
            Title = new YAxisTitle { Text = "Satılan Ürün Miktarı" },
            PlotLines = new[]
            {
              new YAxisPlotLines
              {
                Value = 0,
                Width = 1,
                Color = ColorTranslator.FromHtml("#808080")

              }
            }
          })
          .SetSeries(
            new Series[] {
              new Series
              {
                Name = "Satılan", Data = new Data(
                  table.AsEnumerable().Select(r=>r.Field<int>("Count")).Cast<object>().ToArray()
                )
              }
            }
          );
        ViewData["chart"]= chart.ToHtmlString();
        return View();
      }

      
    }
  }
}