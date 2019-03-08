using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FwGen
{
	public class EntityFrameworkMappingGenerator : GeneratorBase
	{protected override void GenerateClassFiles(string path)
		{
			foreach (var type in Types)
			{
				var content = GenerateClassFilesType(type);
				if (!type.FullName.Contains("ComplexType"))
					File.WriteAllText(path + type.Name + "Map.cs", content, System.Text.Encoding.UTF8);
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
			if (str[str.Length - 1] == 'y')
			{
				str = str.Replace("y", "ies");
				sb.AppendLine($"ToTable(@\"{str}\",\"dbo\");");
			}
			else
			{
				sb.AppendLine($"ToTable(\"{str}s\",\"dbo\");");
			}

			// sb.AppendLine("LazyLoad();");
			foreach (var prop in props)
			{
				//ilk ozellik anahtar olsun (Key annotation'i olmadigi icin bu bu sekilde
				if (idx == 0)
					sb.AppendLine($"HasKey(x => x.{prop.Name});");

				sb.AppendLine($"Property(x => x.{prop.Name}).HasColumnName(\"{prop.Name}\");");
				idx++;
			}

			return fmtClassFile
					.Replace("[ClassName]", type.Name)
					.Replace("[ClassNames]", str)
					.Replace("[ProjectName]", projectName)
					.Replace("[Body]", sb.ToString());
		}

		const string fmtClassFile = @"using System.Data.Entity.ModelConfiguration;
using [ProjectName].Entities.Concrete;

namespace [ProjectName].DataAccess.Concrete.EntityFramework.Mappings
{
  public class [ClassName]Map : EntityTypeConfiguration<[ClassName]>
  {
    public [ClassName]Map()
    {
      [Body]
    }
  }
}";
	}
}
