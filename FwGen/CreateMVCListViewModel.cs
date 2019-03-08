using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;

namespace FwGen
{
    public class CreateMvcListViewModel
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
                    File.WriteAllText(path + type.Name + "ListViewModel.cs", content, System.Text.Encoding.UTF8);
            }
        }

        private string GenerateClassFilesType(Type type)
        {
            var projectName = Form1.frm.txtProjectName.Text;
            var str = type.Name;
            if (str.Length > str.Length - 1 && str[str.Length - 1] == 'y')
            {
              str=   str.Replace("y", "ies");
            }
            
            return fmtClassFile
                .Replace("[ClassName]", type.Name)
                .Replace("[ClassNames]", str)
                .Replace("[ProjectName]", projectName);
        }

        private const string fmtClassFile = @"using System.Collections.Generic;
using [ProjectName].Entities.Concrete;

namespace [ProjectName].MvcWebUi.Models.ListViewModels
{
    public class [ClassName]ListViewModel
    {
        public List<[ClassName]> [ClassNames]s { get; set; }        
    }
}";
    }
}
