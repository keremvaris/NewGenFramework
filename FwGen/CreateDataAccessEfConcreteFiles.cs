using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FwGen
{
    public class CreateDataAccessEfConcreteFiles
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
                if(!type.FullName.Contains("ComplexType"))
                File.WriteAllText(path +"Ef"+ type.Name + "Dal.cs", content, System.Text.Encoding.UTF8);
            }
        }

      
        private string GenerateClassFilesType(Type type)
        {

            var context = Form1.frm.textBox3.Text;
            var projectName = Form1.frm.txtProjectName.Text;
            return fmtClassFile
                .Replace("[ClassName]", type.Name)
                .Replace("[ContextName]",context)
                .Replace("[ProjectName]",projectName);
            
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
