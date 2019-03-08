using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;

namespace FwGen
{
    public class CreateBusinessManagerFiles:GeneratorBase
    {
        protected override void GenerateClassFiles(string path)
        {
            foreach (var type in Types)
            {
                var content = GenerateClassFilesType(type);
                if (!type.FullName.Contains("ComplexType"))
                    File.WriteAllText(path + type.Name + "Manager.cs", content, System.Text.Encoding.UTF8);
            }
        }

        private string GenerateClassFilesType(Type type)
        {
            var projectName = Form1.frm.txtProjectName.Text;

            return fmtClassFile
                .Replace("[ClassName]", type.Name)
                .Replace("[ProjectName]", projectName)
                .Replace("[ClassToTitleCase]", type.Name.Substring(0, 1).ToLower() + type.Name.Substring(1, type.Name.Length - 1));

        }

        private const string fmtClassFile = @"
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using [ProjectName].Business.Abstract;
using NewGenFramework.Core.Aspects.Postsharp.AuthorizationAspects;
using NewGenFramework.Core.Aspects.Postsharp.CacheAspects;
using NewGenFramework.Core.Aspects.Postsharp.TransactionAspects;
using NewGenFramework.Core.CrossCuttingConcerns.Caching.Microsoft;
using [ProjectName].DataAccess.Abstract;
using [ProjectName].Entities.Concrete;
using NewGenFramework.Core.CrossCuttingConcerns.Transaction;

namespace [ProjectName].Business.Concrete.Managers
{
    public class [ClassName]Manager : ManagerBase, I[ClassName]Service
    {
        private I[ClassName]Dal _[ClassToTitleCase]Dal;

        public [ClassName]Manager(I[ClassName]Dal [ClassToTitleCase]Dal)
        {
            _[ClassToTitleCase]Dal = [ClassToTitleCase]Dal;
        }
        
         // [LogAspect(typeof(DatabaseLogger))]
        [CacheAspect(typeof(MemoryCacheManager))]
        // [PerformanceCounterAspect(1)]      
        public List<[ClassName]> GetAll()
        {
            return _[ClassToTitleCase]Dal.GetList();
        }

        public [ClassName] GetById(int [ClassToTitleCase]Id)
        {
            return _[ClassToTitleCase]Dal.Get(u => u.[ClassName]Id == [ClassToTitleCase]Id);
        }      

        //[FluentValidationAspect(typeof([ClassName]Validator))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public [ClassName] Add([ClassName] [ClassToTitleCase])
        {
            return _[ClassToTitleCase]Dal.Add([ClassToTitleCase]);
        }
        //[FluentValidationAspect(typeof([ClassName]Validator))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Update([ClassName] [ClassToTitleCase])
        {
              _[ClassToTitleCase]Dal.Update([ClassToTitleCase]);
        }

        public void Delete([ClassName] [ClassToTitleCase])
        {
            _[ClassToTitleCase]Dal.Delete([ClassToTitleCase]);
        }    

        public List<[ClassName]> GetBy[ClassName](int [ClassToTitleCase]Id)
        {
            return _[ClassToTitleCase]Dal.GetList(filter: t => t.[ClassName]Id == [ClassToTitleCase]Id).ToList();
        }
    }
}
";
    }
}
