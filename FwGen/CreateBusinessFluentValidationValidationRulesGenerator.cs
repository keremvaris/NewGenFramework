using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FwGen
{
    public class CreateBusinessFluentValidationValidationRulesGenerator
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
                    File.WriteAllText(path + type.Name + "Validator.cs", content, System.Text.Encoding.UTF8);
            }
        }

        private string GenerateClassFilesType(Type type)
        {
            var sb = new StringBuilder();
            // ozellikleri al (Inheritance icin bu calismaz)
            var props = type.GetProperties();
  
            foreach (var prop in props)
            {
                
                sb.AppendLine($"RuleFor(x => x.{prop.Name}).NotEmpty();");
            }
            var projectName = Form1.frm.txtProjectName.Text;
            return fmtClassFile
                .Replace("[ClassName]", type.Name)
                .Replace("[ClassToTitleCase]", type.Name.Substring(0, 1).ToLower() + type.Name.Substring(1, type.Name.Length - 1))
                .Replace("[ProjectName]", projectName)
                .Replace("[Body]", sb.ToString());
        }


        const string fmtClassFile = @"using FluentValidation;
using [ProjectName].Entities.Concrete;
using [ProjectName].Business.DependencyResolvers.Ninject;
using [ProjectName].Entities.Concrete;
using [ProjectName].Business.Abstract;

namespace [ProjectName].Business.ValidationRules.FluentValidation
{
   public class [ClassName]Validator:AbstractValidator<[ClassName]>
    {
        public [ClassName]Validator()
        {
        //Eğer CustomRule yazmak istenirse service interfacelerini çözer custom rule için gerekli metodlara ulaşmanızı sağlar
        var [ClassToTitleCase]Service = DependencyResolver<I[ClassName]Service>.Resolve();
        //Sadece Boş Olamaz Kontrolü Yapar
            [Body]

        //Custom Rule Kullanımı Aşağıdaki gibidir
         //Custom(rm =>
            //{
            //var useremail = userService.UniqueEmail(rm.Email);
            //    if (rm.Agreement != true /*&& useremail.Email != null*/)
            //    {
            //        return new ValidationFailure(/*you must type the property name here, you must type the error message here */);
        //    }
        //    return null;
        //});
        }
}
}";
    }
}
