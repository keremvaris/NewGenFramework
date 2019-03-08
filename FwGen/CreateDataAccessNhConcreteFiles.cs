using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FwGen
{
	public class CreateDataAccessNhConcreteFiles : GeneratorBase
	{
		protected override void GenerateClassFiles(string path)
		{
			foreach (var type in Types)
			{
				var content = GenerateClassFilesType(type);
				if (!type.FullName.Contains("ComplexType"))
					File.WriteAllText(path + "Nh" + type.Name + "Dal.cs", content, System.Text.Encoding.UTF8);
			}
		}

		private string GenerateClassFilesType(Type type)
		{
			var projectName = Form1.frm.txtProjectName.Text;

			return fmtClassFile
					.Replace("[ClassName]", type.Name)
					.Replace("[ProjectName]", projectName);

		}



		private const string fmtClassFile = @"
using NewGenFramework.Core.DataAccess.NHibernate;
using [ProjectName].DataAccess.Abstract;
using [ProjectName].Entities.Concrete;
using NHibernate.Linq;

namespace [ProjectName].DataAccess.Concrete.NHibernate
{
		public class Nh[ClassName]Dal:NHEntityRepositoryBase<[ClassName]>,I[ClassName]Dal
		{
				public Nh[ClassName]Dal(NHibernateHelper hibernateHelper) : base(hibernateHelper)
				{
				}
		}
}
";
	}
}
