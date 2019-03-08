using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using BayiPuan.Business.Abstract;
using BayiPuan.DataAccess.Concrete.Context;
using BayiPuan.Entities.Concrete;
using BayiPuan.MvcWebUi.Filters;
using BayiPuan.MvcWebUi.GenericVM;
using BayiPuan.MvcWebUi.Infrastructure;
using NewGenFramework.Core.DataAccess;
using Newtonsoft.Json;
using Ninject.Activation;

namespace BayiPuan.MVCWebUI.Controllers
{
    [AuthorizationFilter]
    public class ReportViewerController : BaseController
    {
        private readonly IGN_ReportService _reportService;
        private readonly IQueryableRepository<GN_Report> _queryableRepository;
        private readonly IQueryableRepository<UserRole> _roleRepository;
        private readonly IUserService _userService;
        public ReportViewerController(IGN_ReportService reportService, IQueryableRepository<GN_Report> queryableRepository, IUserService userService, IQueryableRepository<UserRole> roleRepository)
        {
            _reportService = reportService;
            _queryableRepository = queryableRepository;
            _userService = userService;
            _roleRepository = roleRepository;
        }
        // GET: ReportViewer
        public ActionResult Index(int reportId, string filterColumns = null)
        {
            var table = new DataTable();
            var reportKey = _queryableRepository.Table.AsNoTracking().FirstOrDefault(x => x.ReportId == reportId);
            var getUserId = _userService.UniqueUserName(User.Identity.Name);
            var roleControl = _userService.GetUserRoles(getUserId).Select(u => u.RoleName).ToArray();
           
            
            foreach (var role in roleControl)
            {
                var accessControl = reportKey.ReportAuthorization.Split(',');
                for (int i = 0; i < accessControl.Length; i++)
                {
                    if (accessControl[i]==role)
                    {
                        if (!string.IsNullOrEmpty(filterColumns))
                        {
                            foreach (var filterType in filterColumns.Split('|'))
                            {
                                if (filterType.Contains("KeyString"))
                                {
                                    var keyst = filterType.Replace("KeyString:", "");
                                    TempData["html"] += (keyst + " <input value=\"\" class=\"w3-input\" id=\"" + filterType.Replace("KeyString:", "") + "\" name=\"" + filterType.Replace("KeyString:", "") + "\" type=\"text \" ><br/>");
                                }
                                else if (filterType.Contains("KeyInt"))
                                {
                                    var keyint = filterType.Replace("KeyInt:", "");
                                    TempData["html"] += (keyint + "<input value=\"\" class=\"w3-input\" id=\"" + filterType.Replace("KeyInt:", "") + "\" name=\"" + filterType.Replace("KeyInt:", "") + "\" type=\"text \" ><br/>");
                                }
                                else if (filterType.Contains("KeyBetween"))
                                {
                                    TempData["html"] += ("Başlangıç:<input type=\"datetime-local\" id=\"" + filterType.Replace("KeyBetween:", "") + "sdt" + "\" class=\"w3-input\" name=\"" + filterType.Replace("KeyBetween:", "") + "sdt" + "\"  style=\"width:30%\" />Bitiş:<input type=\"datetime-local\" id=\"" + filterType.Replace("KeyBetween:", "") + "edt" + "\" class=\"w3-input\" name=\"" + filterType.Replace("KeyBetween:", "") + "edt" + "\"  style=\"width:30%\" /><br/>");
                                }
                                else if (filterType.Contains("KeyLookup"))
                                {
                                    var lookup = filterType.Replace("KeyLookup:", "");
                                    var Id = "";
                                    var Text = "";
                                    var ActualTableName = "";
                                    Text = lookup.Split(',')[0].ToString();
                                    Id = lookup.Split(',')[1].ToString();
                                    ActualTableName = lookup.Split(',')[2].ToString();
                                    Id = Id.Split('.')[1].ToString();
                                    var result = Tools.Select<ListItem>($"select {Text} [Text], {Id} [Id] from {ActualTableName}");
                                    TempData["html"] += (Text + $"<select id=\"{Id}\" name=\"{Id}\" class=\"w3-input\" required>");
                                    TempData["html"] += ($"<option value=\"\">Seçiniz</option>");
                                    foreach (var optionItem in result)
                                    {
                                        TempData["html"] += ($"<option value=\"{optionItem.Id}\">{optionItem.Text}</option>");
                                    }
                                    TempData["html"] += ("<select><br/>");
                                }
                                else if (filterType.Contains("KeyMaster"))
                                {
                                    var lookup = filterType.Replace("KeyMaster:", "");
                                    var Id = "";
                                    var Text = "";
                                    var ActualTableName = "";
                                    var MasterKey = "";
                                    foreach (var lookupItem in lookup.Split(','))
                                    {
                                        Text = lookup.Split(',')[0].ToString();
                                        Id = lookup.Split(',')[1].ToString();
                                        ActualTableName = lookup.Split(',')[2].ToString();
                                        MasterKey = lookup.Split(',')[3].ToString();
                                    }
                                    TempData["html"] += (Text + $"<select required class=\"w3-input\" dataid=\"{Id}\" dataentity=\"{ActualTableName}\" dataname=\"{Text}\" dataparent=\"{MasterKey}\" id=\"{Id.Split('.')[1].ToString()}\" name=\"{Id.Split('.')[1].ToString()}\"  tabindex=\"-1\" aria-hidden=\"true\">");
                                    TempData["html"] += ("<select><br/>");
                                }
                            }
                            return View(table);
                        }
                        else
                        {
                            using (var dbx = new BayiPuanContext())
                            {
                                ViewData["ReportTitle"] = reportKey?.ReportTitle;
                                var cmd = dbx.Database.Connection.CreateCommand();
                                var sql = reportKey?.ReportSql;
                                cmd.CommandText = sql;
                                cmd.Connection.Open();
                                table.Load(cmd.ExecuteReader());
                                return View(table);
                            }
                        }
                    }
                }
            }
            ErrorNotification("Bu Raporu Görüntülemek İçin Yetkiniz Yok!!!");
           return RedirectToAction("GN_ReportIndex", "GN_Report");
        }
        [HttpPost]
        public ActionResult FilterIndex(FormCollection form, int reportId, string filterColumns = null)
        {
            var table = new DataTable();
            var reportKey = _queryableRepository.Table.AsNoTracking().FirstOrDefault(x => x.ReportId == reportId);
            var sb = new StringBuilder(reportKey?.ReportSql);
            var keys = Request.Form.AllKeys;
            if (filterColumns != null)
            {
                sb.Append(" WHERE 1=1 AND ");
                foreach (var filterType in filterColumns.Split('|'))
                {
                    if (filterType.Contains("KeyString"))
                    {
                        var keyString = filterType.Replace("KeyString:", "");
                        var requestFormString = form[keyString];
                        if (requestFormString != "")
                        {
                            sb.Append(keyString);
                            sb.Append(" = '" + requestFormString.Replace(",", "") + "'");
                            sb.Append(" AND ");
                        }
                    }
                    else if (filterType.Contains("KeyInt"))
                    {
                        var intString = filterType.Replace("KeyInt:", "");
                        var requestFormInt = form[intString];
                        if (requestFormInt != "")
                        {
                            sb.Append(intString);
                            sb.Append(" = " + requestFormInt.Replace(",", "") + "");
                            sb.Append(" AND ");
                        }
                    }
                    else if (filterType.Contains("KeyBetween"))
                    {
                        var betweenString = filterType.Replace("KeyBetween:", "");

                        var requestFormBetweensdt = form[betweenString + "sdt"];
                        var requestFormBetweenedt = form[betweenString + "edt"];
                        if (requestFormBetweensdt != "" && requestFormBetweenedt != "")
                        {
                            sb.Append(betweenString);
                            var sdt = requestFormBetweensdt.Replace(",", "");
                            var edt = requestFormBetweenedt.Replace(",", "");
                            sb.Append(" BETWEEN '" + sdt.Replace("T", " ") + "' AND '" + edt.Replace("T", " ") + "' ");
                            sb.Append(" AND ");
                        }
                    }
                    else if (filterType.Contains("KeyLookup"))
                    {
                        var LookupString = filterType.Replace("KeyLookup:", "");
                        var requestFormInt = "";
                        requestFormInt = LookupString.Split(',')[1].ToString();
                        if (requestFormInt != "")
                        {
                            if (sb.ToString().Contains("JOIN") == true || sb.ToString().Contains("join"))
                            {
                                sb.Append(requestFormInt);
                                requestFormInt = requestFormInt.Split('.')[1].ToString();
                                requestFormInt = form[requestFormInt];
                                sb.Append(" = " + requestFormInt.Replace(",", "") + "");
                                sb.Append(" AND ");
                            }
                            else
                            {
                                requestFormInt = requestFormInt.Split('.')[1].ToString();
                                sb.Append(requestFormInt);
                                requestFormInt = form[requestFormInt];
                                sb.Append(" = " + requestFormInt.Replace(",", "") + "");
                                sb.Append(" AND ");
                            }
                        }

                    }
                    else if (filterType.Contains("KeyMaster"))
                    {
                        var LookupString = filterType.Replace("KeyMaster:", "");
                        var requestFormInt = "";
                        requestFormInt = LookupString.Split(',')[1].ToString();

                        if (requestFormInt != "" || requestFormInt != null)
                        {
                            if (sb.ToString().Contains("JOIN") == true || sb.ToString().Contains("join"))
                            {
                                sb.Append(requestFormInt);
                                requestFormInt = requestFormInt.Split('.')[1].ToString();
                                requestFormInt = form[requestFormInt];
                                sb.Append(" = " + requestFormInt.Replace(",", "") + "");
                                sb.Append(" AND ");
                            }
                            else
                            {
                                requestFormInt = requestFormInt.Split('.')[1].ToString();
                                sb.Append(requestFormInt);
                                requestFormInt = form[requestFormInt];
                                sb.Append(" = " + requestFormInt.Replace(",", "") + "");
                                sb.Append(" AND ");
                            }
                        }

                    }
                    else if (filterType.Contains("KeyExtra"))
                    {
                        var keyExtra = filterType.Replace("KeyExtra:", "");
                        sb.Append(keyExtra);
                        sb.Append(" AND ");
                    }
                    if (filterType.Contains("KeyAfterWhere"))
                    {
                        var cuteAnd = sb.ToString().Substring(0, sb.Length - 4);
                        sb = new StringBuilder(cuteAnd.ToString());
                        var keyAfterWhere = filterType.Replace("KeyAfterWhere:", "");
                        sb.Append(keyAfterWhere);
                        using (var dbx = new BayiPuanContext())
                        {
                            ViewData["ReportTitle"] = reportKey?.ReportTitle;
                            var cmd = dbx.Database.Connection.CreateCommand();
                            var sql = sb.ToString();
                            cmd.CommandText = sql.ToString();
                            cmd.Connection.Open();
                            table.Load(cmd.ExecuteReader());
                            return View(table);
                        }
                    }

                }
                using (var dbx = new BayiPuanContext())
                {
                    ViewData["ReportTitle"] = reportKey?.ReportTitle;
                    var cmd = dbx.Database.Connection.CreateCommand();
                    var sql = sb.ToString().Substring(0, sb.Length - 4);
                    cmd.CommandText = sql.ToString();
                    cmd.Connection.Open();
                    table.Load(cmd.ExecuteReader());
                    return View(table);
                }
            }
            else
            {
                using (var dbx = new BayiPuanContext())
                {
                    ViewData["ReportTitle"] = reportKey?.ReportTitle;
                    var cmd = dbx.Database.Connection.CreateCommand();
                    var sql = reportKey?.ReportSql;
                    cmd.CommandText = sql;
                    cmd.Connection.Open();
                    table.Load(cmd.ExecuteReader());
                    return View(table);
                }
            }

        }

    }
}