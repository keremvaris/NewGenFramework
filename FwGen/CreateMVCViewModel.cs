using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Reflection;
using System.Text;

namespace FwGen
{
    public class CreateMvcViewModel
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
                    File.WriteAllText(path + type.Name + "ViewModel.cs", content, System.Text.Encoding.UTF8);
            }
        }

        private string GenerateClassFilesType(Type type)
        {
            var projectName = Form1.frm.txtProjectName.Text;
            var sb = new StringBuilder();
            // ozellikleri al (Inheritance icin bu calismaz)
            var props = type.GetProperties();

            var idx = 0;
            var str = type.Name;
            if (str.Length > str.Length - 1 && str[str.Length - 1] == 'y')
            {
                sb.AppendLine($"[Table(\"{type.Name}ies\")]");
            }
            else
            {
                sb.AppendLine($"[Table(\"{type.Name}s\")]");
            }

            sb.AppendLine($"[DisplayColumn(\"{type.Name}Name\")]");
            sb.AppendLine($"[DisplayName(\"{type.Name}\")]");
            sb.AppendLine($"public class {type.Name}ViewModel");
            sb.AppendLine("{");
            foreach (var prop in props)
            {
                if (idx == 0)
                {
                    sb.AppendLine("[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]");
                    sb.AppendLine($"[Display(Name = \"{prop.Name} Id\", AutoGenerateField = false)]");
                    sb.Append($"public virtual {prop.PropertyType.Name.ToLowerInvariant()} {prop.Name}").Append("{ get; set; }").AppendLine("");
                }
                else if (prop.Name.Contains("Id"))
                {
                    sb.AppendLine($"[Display(Name = \"{prop.Name}\"), Required()]");
                    sb.Append($"public virtual {prop.PropertyType.Name.ToLowerInvariant()} {prop.Name}").Append("{ get; set; }").AppendLine("");
                    sb.AppendLine($"[ForeignKey(\"{prop.Name}\")]");
                    sb.Append($"public virtual {prop.Name.Substring(0, prop.Name.Length - 2)}ViewModel {prop.Name.Substring(0, prop.Name.Length - 2)}").Append("{ get; set; }").AppendLine("");
                }
                else if (prop.PropertyType.Name.ToLowerInvariant() == "byte[]")
                {
                    sb.AppendLine($"[ScaffoldColumn(false)]");
                    sb.AppendLine($"[Display(Name = \"Yükle\")]");
                    sb.AppendLine("[DataType(DataType.Upload)]");
                    sb.Append($"public virtual {prop.PropertyType.Name.ToLowerInvariant()} {prop.Name}").Append("{ get; set; }").AppendLine("");
                }
                else if (prop.Name.Contains("ImageExt"))
                {
                    sb.AppendLine($"[ScaffoldColumn(false)]");
                    sb.AppendLine($"[Display(Name = \"Dosya Uzantısı\")]");
                    sb.AppendLine("[MaxLength(5)]");
                    sb.Append($"public virtual {prop.PropertyType.Name.ToLowerInvariant()} {prop.Name}").Append("{ get; set; }").AppendLine("");
                }
                else
                {
                    sb.AppendLine($"[Display(Name = \"{prop.Name}\"), Required()]");
                    sb.Append($"public virtual {prop.PropertyType.Name.ToLowerInvariant()} {prop.Name}").Append("{ get; set; }").AppendLine("");
                }
                idx++;
            }

            return fmtClassFile
                .Replace("[ClassName]", type.Name)
                .Replace("[ProjectName]", projectName)
                .Replace("[Body]", sb.ToString().Replace("int32", "int")
                    
                    .Replace("int16", "int"));
        }

        const string fmtClassFile = @"using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace [ProjectName].MvcWebUi.Models.ViewModels
{               
[Body]
     }
}";
    }
}
