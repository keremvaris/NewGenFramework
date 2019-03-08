using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FwGen
{
	public class CreateDataAccessAbstractFiles : GeneratorBase
	{


		protected override void GenerateClassFiles(string path)
		{
			foreach (var type in Types)
			{
				var content = GenerateClassFilesType(type);
				if (!type.FullName.Contains("ComplexType"))
					File.WriteAllText(path + "I" + type.Name + "Dal.cs", content, System.Text.Encoding.UTF8);
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
using NewGenFramework.Core.DataAccess;
using [ProjectName].Entities.Concrete;
namespace [ProjectName].DataAccess.Abstract
{
    //Neden IEntityRepository direk kullanmıyoruz da IProductDal diye bir şeyi araya atıyoruz sorusuna 
    //Nesneye özgü metodlar geliştirilebilir.
    public interface I[ClassName]Dal:IEntityRepository<[ClassName]>
    {
        //for Ex:
        //List<[ClassName]Detail> Get[ClassName]Details();
    }
}";
	}
}
