using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FwGen
{
    public class HibernateMappingGenerator
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
                    File.WriteAllText(path + type.Name + "Map.cs", content, System.Text.Encoding.UTF8);
            }
        }

        private string GenerateClassFilesType(Type type)
        {
            var sb = new StringBuilder();
            // ozellikleri al (Inheritance icin bu calismaz)
            var props = type.GetProperties();
            var idx = 0;
            var str = type.Name;
            if (str[str.Length - 1] == 'y')
            {
                str = str.Replace("y", "ies");
                sb.AppendLine($"Table(\"{str}\");");
            }
            else
            {
                sb.AppendLine($"Table(\"{str}s\");");
            }
            
            sb.AppendLine("LazyLoad();");
            foreach (var prop in props)
            {
                //ilk ozellik anahtar olsun (Key annotation'i olmadigi icin bu bu sekilde
                if (idx == 0)
                    sb.AppendLine($"Id(x => x.{prop.Name}).Column(\"{prop.Name}\");");
                else
                    sb.AppendLine($"Map(x => x.{prop.Name}).Column(\"{prop.Name}\");");
                idx++;
            }
            var projectName = Form1.frm.txtProjectName.Text;
            return fmtClassFile
                .Replace("[ClassName]", type.Name)
                .Replace("[ClassNames]", str)
                .Replace("[ProjectName]", projectName)
                .Replace("[Body]", sb.ToString());
        }

        const string fmtClassFile = @"using FluentNHibernate.Mapping;
using [ProjectName].Entities.Concrete;
 
namespace [ProjectName].DataAccess.Concrete.NHibernate.Mappings
{
  public class [ClassName]Map : ClassMap<[ClassName]>
  {
    public [ClassName]Map()
    {
      [Body]
    }
  }
}";
    }
}
