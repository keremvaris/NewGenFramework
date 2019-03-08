using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FwGen
{
	public class CreateDataAccessEfConcreteFiles : GeneratorBase
	{
		protected override void GenerateClassFiles(string path)
		{
			foreach (var type in Types)
			{
				var content = GenerateClassFilesType(type);
				if (!type.FullName.Contains("ComplexType"))
					File.WriteAllText(path + "Ef" + type.Name + "Dal.cs", content, System.Text.Encoding.UTF8);
			}
		}


		private string GenerateClassFilesType(Type type)
		{

			var context = Form1.frm.textBox3.Text;
			var projectName = Form1.frm.txtProjectName.Text;
			return fmtClassFile
					.Replace("[ClassName]", type.Name)
					.Replace("[ContextName]", context)
					.Replace("[ProjectName]", projectName);

		}



		private const string fmtClassFile = @"using NewGenFramework.Core.DataAccess.EntityFramework;
using [ProjectName].DataAccess.Abstract;
using [ProjectName].DataAccess.Concrete.Context;
using [ProjectName].Entities.Concrete;

namespace [ProjectName].DataAccess.Concrete.EntityFramework
{
		 public class Ef[ClassName]Dal:EfEntityRepositoryBase<[ClassName],[ContextName]>,I[ClassName]Dal
		{
		}
}
";
	}
}
