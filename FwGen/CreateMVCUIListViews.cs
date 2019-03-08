using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FwGen
{
    public class CreateMvcUiListViews
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
                    File.WriteAllText(path + type.Name + "Index.cshtml", content, System.Text.Encoding.UTF8);
            }
        }

        private string GenerateClassFilesType(Type type)
        {
            var projectName = Form1.frm.txtProjectName.Text;
            var sb = new StringBuilder();
            // ozellikleri al (Inheritance icin bu calismaz)
            var props = type.GetProperties();


            sb.AppendLine($"@model IGrid<{type.Name}>");
            sb.AppendLine("@{Layout = \"~/Views/Shared/_Layout.cshtml\";}");
            sb.AppendLine($"<style>");
            sb.AppendLine(".table > caption + thead > tr:first-child > td, .table > caption + thead > tr:first-child > th, .table > colgroup + thead > tr:first-child > td, .table > colgroup + thead > tr:first-child > th, .table > thead:first-child > tr:first-child > td, .table > thead:first-child > tr:first-child > th {");
            sb.AppendLine($"border-top: 0;");
            sb.AppendLine($"background: #8bc34a;");
            sb.AppendLine("}");
            sb.AppendLine(".actions {margin-left: 5px;}");
            sb.AppendLine(".w3-table th:first-child, .w3-table td:first-child, .w3-table-all th:first-child, .w3-table-all td:first-child {");
            sb.AppendLine($"padding-left: 5px;");
            sb.AppendLine($"width: 100px;");
            sb.AppendLine("}");
            sb.AppendLine($"</style>");
            sb.AppendLine($"<div class=\"w3-container w3-card-4 w3-text-blue w3-margin \" style=\"background: white;\">");
            sb.AppendLine($"<div class=\"w3-container w3-blue w3-center\">");
            sb.AppendLine($"<div align=\"left\" style=\"float: left;margin-top: 7px;\">");
            sb.AppendLine($"</div>");
            sb.AppendLine($"<div align=\"center\">");
            sb.AppendLine($"<h4>{type.Name} Tanımları(@ViewBag.totalRows)</h4>");
            sb.AppendLine($"</div>");
            sb.AppendLine($"<div align=\"right\" style=\"float: right;margin-top: -32px;\">");
            sb.AppendLine($"    <a href=\"@(Url.Action(\"Create\"))\" title=\"Ekle\" class=\"fa fa-plus btn btn-primary btn-sm\"> </a>");
            sb.AppendLine($"    <a href=\"@(Url.Action(\"DF_BrandIndex\"))\" title=\"Tazele\" class=\"fas fa-sync-alt btn btn-info btn-sm\"> </a>");
            sb.AppendLine($"    <a href=\"@(Url.Action(\"ExportIndex\") + \"?\" + Request.QueryString)\" title=\"Excele Aktar\" class=\"fa fa-file-excel-o btn btn-success btn-sm\"> </a>");
            sb.AppendLine($"</div>");
            sb.AppendLine($"</div>");
            sb.AppendLine($"<div class=\"w3-responsive\">");
            sb.AppendLine($"@(new HtmlGrid<{type.Name}>(Html, grid: Model))");
            sb.AppendLine($"</div>");
            sb.AppendLine($"</div>");
            return fmtClassFile
                .Replace("[ClassName]", type.Name)
                .Replace("[Body]", sb.ToString().Replace("int32", "int")
                    .Replace("int16", "int"));
        }

        const string fmtClassFile = @"[Body]";
    }
}
