using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using BayiPuan.Entities.Concrete;
using NewGenFramework.Core.Entities;


namespace FwGen
{
    public partial class Form1 : Form
    {
        public static Form1 frm;
        public Form1()
        {
            frm = this;
            InitializeComponent();
        }


        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // get directory
            var directory = VisualStudioProvider.TryGetSolutionDirectoryInfo();
            string[] dirs = Directory.GetDirectories(directory.FullName, "*.Entities", SearchOption.AllDirectories);
            txtProjectName.Text = directory.Name;
            textBox3.Text = directory.Name + "Context";
            foreach (string dir in dirs)
            {
                textBox2.Text = dir + @"\Concrete\";
            }

            DirectoryInfo entityInfo = new DirectoryInfo(textBox2.Text);
            foreach (var item in entityInfo.GetFiles())
            {
                comboBox1.Items.Add(item.Name.Replace(".cs", ""));
            }

            if (directory != null)
            {
                textBox1.Text = directory.FullName;
            }
        }

        public static class VisualStudioProvider
        {
            public static DirectoryInfo TryGetSolutionDirectoryInfo(string currentPath = null)
            {
                var directory = new DirectoryInfo(
                    currentPath ?? Directory.GetCurrentDirectory());
                while (directory != null && !directory.GetFiles("*.sln").Any())
                {
                    directory = directory.Parent;
                }
                return directory;
            }
        }
        public void CreateDataAccessAbstractFiles()
        {
            #region CommentRegion

            //            var path = Application.StartupPath + @"\DataAccess\Abstract\I" + comboBox1.SelectedItem + "Dal.cs";
            //            var text2write =
            //                @"using NewGenFramework.Core.DataAccess;
            //using BayiPuan.Entities.Concrete;
            //namespace BayiPuan.DataAccess.Abstract
            //{
            //    public interface I" + comboBox1.SelectedItem + @"Dal:IEntityRepository<" + comboBox1.SelectedItem + @">
            //    {
            //    }
            //}";
            //            StreamWriter writer = new StreamWriter(path);
            //            writer.Write(text2write);
            //            writer.Close();

            #endregion
            var path = Application.StartupPath + @"\DataAccess\Abstract\";
            if (!path.EndsWith("\\")) path += "\\";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            var gen = new CreateDataAccessAbstractFiles();
            foreach (var type in typeof(Language).Assembly.GetTypes())
            {
                var t = type.GetInterfaces();
                if (t != null && t.Length > 0 && t[0] != typeof(IEntity)) continue;
                gen.Add(type);
            }
            gen.Generate(path);
        }
        public void CreateDataAccessContextFile()
        {
            var path = Application.StartupPath + @"\DataAccess\Concrete\Context\";
            if (!path.EndsWith("\\")) path += "\\";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            var text2write = @"using System.Data.Entity;
using "+txtProjectName.Text+ @".DataAccess.Concrete.EntityFramework.Mappings;
using " + txtProjectName.Text + @".Entities.Concrete;

namespace " + txtProjectName.Text + @".DataAccess.Concrete.Context
{    
    public class " + textBox3.Text + @":DbContext
    {
        public " + textBox3.Text + @"()
        {
               Database.SetInitializer<" + textBox3.Text +
                             @">(null/*new CreateDatabaseIfNotExists<" + textBox3.Text + @">()*/);
        }" + Environment.NewLine;
            DirectoryInfo entityInfo = new DirectoryInfo(textBox2.Text);
            foreach (var item in entityInfo.GetFiles())
            {
                var str = item.Name.Replace(".cs", "");
                if (str[str.Length - 1] == 'y')
                {
                    text2write += @"public DbSet<" + str + "> " + str.Replace("y", "ies") + " { get; set; }" + Environment.NewLine;
                }
                else
                {
                    text2write += @"public DbSet<" + str + "> " + str + "s" + " { get; set; }" + Environment.NewLine;
                }

            }

            text2write += @"protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {" + Environment.NewLine;
            foreach (var item in entityInfo.GetFiles())
            {
                var str = item.Name.Replace(".cs", "");
                text2write += "modelBuilder.Configurations.Add(new " + str + "Map());" + Environment.NewLine;
            }
            text2write += @"}
    }
}
";

            File.WriteAllText(path + textBox3.Text + ".cs", text2write, System.Text.Encoding.UTF8);

        }
        public void CreateDataAccessNhSqlServerHelperClass()
        {
            var path = Application.StartupPath + @"\DataAccess\Concrete\NHibernate\Helpers\";
            if (!path.EndsWith("\\")) path += "\\";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            var text2write =
                @"using System.Reflection;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NewGenFramework.Core.DataAccess.NHibernate;
using NHibernate;
using NHibernate.Tool.hbm2ddl;

namespace " + txtProjectName.Text + @".DataAccess.Concrete.NHibernate.Helpers
{
    public class SqlServerHelper : NHibernateHelper
    {
        protected override ISessionFactory InitializeFactory()
        {
            return Fluently.Configure().Database(MsSqlConfiguration.MsSql2012
                .ConnectionString(c => c.FromConnectionStringWithKey(" + textBox3.Text + @")))
                .Mappings(t => t.FluentMappings.AddFromAssembly(Assembly.GetExecutingAssembly()))
                //.ExposeConfiguration(cfg => new SchemaExport(cfg).Create(true, true))
                .ExposeConfiguration(cfg => new SchemaUpdate(cfg).Execute(true, true))
                .BuildSessionFactory();
        }
    }
}
";
            File.WriteAllText(path + "SqlServerHelper.cs", text2write, System.Text.Encoding.UTF8);

        }
        public void CreateDataAccessNhibernateMapFiles()
        {
            var path = Application.StartupPath + @"\DataAccess\Concrete\NHibernate\Mappings\";
            var gen = new HibernateMappingGenerator();
            foreach (var type in typeof(Language).Assembly.GetTypes())
            {
                var t = type.GetInterfaces();
                if (t != null && t.Length > 0 && t[0] != typeof(IEntity)) continue;
                gen.Add(type);
            }

            gen.Generate(path);
        }
        public void CreateDataAccessEntityFrameworkMapFiles()
        {
            var path = Application.StartupPath + @"\DataAccess\Concrete\EntityFramework\Mappings\";
            var gen = new EntityFrameworkMappingGenerator();
            foreach (var type in typeof(Language).Assembly.GetTypes())
            {
                var t = type.GetInterfaces();
                if (t != null && t.Length > 0 && t[0] != typeof(IEntity)) continue;
                gen.Add(type);
            }

            gen.Generate(path);
        }
        public void CreateDataAccessNhConcreteFiles()
        {
            var path = Application.StartupPath + @"\DataAccess\Concrete\NHibernate\";
            var gen = new CreateDataAccessNhConcreteFiles();
            foreach (var type in typeof(Language).Assembly.GetTypes())
            {
                var t = type.GetInterfaces();
                if (t != null && t.Length > 0 && t[0] != typeof(IEntity)) continue;
                gen.Add(type);
            }

            gen.Generate(path);
        }
        public void CreateDataAccessEfConcreteFiles()
        {
            var path = Application.StartupPath + @"\DataAccess\Concrete\EntityFramework\";
            var gen = new CreateDataAccessEfConcreteFiles();
            foreach (var type in typeof(Language).Assembly.GetTypes())
            {
                var t = type.GetInterfaces();
                if (t != null && t.Length > 0 && t[0] != typeof(IEntity)) continue;
                gen.Add(type);
            }

            gen.Generate(path);
        }
        public void CreateBusinessAbstractFiles()
        {
            var path = Application.StartupPath + @"\Business\Abstract\";
            var gen = new CreateBusinessAbstractFiles();
            foreach (var type in typeof(Language).Assembly.GetTypes())
            {
                var t = type.GetInterfaces();
                if (t != null && t.Length > 0 && t[0] != typeof(IEntity)) continue;
                gen.Add(type);
            }

            gen.Generate(path);
        }
        public void CreateBusinessManagerFiles()
        {
            var path = Application.StartupPath + @"\Business\Concrete\Managers\";
            var gen = new CreateBusinessManagerFiles();
            foreach (var type in typeof(Language).Assembly.GetTypes())
            {
                var t = type.GetInterfaces();
                if (t != null && t.Length > 0 && t[0] != typeof(IEntity)) continue;
                gen.Add(type);
            }

            gen.Generate(path);
        }
        public void CreateBusinessFluentValidationValidationRulesGenerator()
        {
            var path = Application.StartupPath + @"\Business\ValidationRules\FluentValidation\";
            var gen = new CreateBusinessFluentValidationValidationRulesGenerator();
            foreach (var type in typeof(Language).Assembly.GetTypes())
            {
                var t = type.GetInterfaces();
                if (t != null && t.Length > 0 && t[0] != typeof(IEntity)) continue;
                gen.Add(type);
            }

            gen.Generate(path);
        }
        public void CreateBusinessMappingsAutoMapperProfileFile()
        {
            var path = Application.StartupPath + @"\Business\Mappings\AutoMapper\Profiles\";
            if (!path.EndsWith("\\")) path += "\\";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            var text2write = @"using AutoMapper;
using " + txtProjectName.Text + @".Entities.Concrete;

namespace " + txtProjectName.Text + @".Business.Mappings.AutoMapper.Profiles
{
    public class BusinessProfiles:Profile
    {
        public BusinessProfiles()
        {" + Environment.NewLine;
            DirectoryInfo entityInfo = new DirectoryInfo(textBox2.Text);
            foreach (var item in entityInfo.GetFiles())
            {
                var str = item.Name.Replace(".cs", "");
                text2write += @"CreateMap<" + str + ", " + str + ">();" + Environment.NewLine;
            }
            text2write += @"
        }
    }
}
";

            File.WriteAllText(path + "BusinessProfiles.cs", text2write, System.Text.Encoding.UTF8);
        }
        public void CreateBusinessModuleFile()
        {
            var path = Application.StartupPath + @"\Business\DependencyResolvers\Ninject\";
            if (!path.EndsWith("\\")) path += "\\";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            var text2write = @"using NewGenFramework.Core.DataAccess.NHibernate;
using " + txtProjectName.Text + @".DataAccess.Abstract;
using " + txtProjectName.Text + @".DataAccess.Concrete.NHibernate;
using " + txtProjectName.Text + @".DataAccess.Concrete.NHibernate.Helpers;
using Ninject.Modules;

namespace " + txtProjectName.Text + @".Business.DependencyResolvers.Ninject
{
    public class BusinessModule:NinjectModule
    {
        public override void Load()
        {" + Environment.NewLine;
            DirectoryInfo entityInfo = new DirectoryInfo(textBox2.Text);
            foreach (var item in entityInfo.GetFiles())
            {
                var str = item.Name.Replace(".cs", "");


                text2write += @"Bind<I" + str + "Service>().To<" + str + "Manager>().InSingletonScope();" + Environment.NewLine +
            "Bind<I" + str + "Dal>().To<Ef" + str + "Dal>().InSingletonScope();" + Environment.NewLine + Environment.NewLine;
            }
            text2write += @"
            Bind(typeof(IQueryableRepository<>)).To(typeof(EFQueryableRepository<>));
          Bind<DbContext>().To<" + txtProjectName.Text + @"Context>();
        }
    }
}
";

            File.WriteAllText(path + "BusinessModule.cs", text2write, System.Text.Encoding.UTF8);
        }
        public void CreateAutoMapperModuleFile()
        {
            var path = Application.StartupPath + @"\Business\DependencyResolvers\Ninject\";
            if (!path.EndsWith("\\")) path += "\\";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            var text2write = @"using AutoMapper;
using Ninject.Modules;

namespace " + txtProjectName.Text + @".Business.DependencyResolvers.Ninject
{
   public  class AutoMapperModule:NinjectModule
    {
        public override void Load()
        {
            Bind<IMapper>().ToConstant(CreateConfiguration().CreateMapper()).InSingletonScope();
        }

        private MapperConfiguration CreateConfiguration()
        {
            var config=new MapperConfiguration(cfg =>
            {
                cfg.AddProfiles(GetType().Assembly);
            });
            return config;
        }
    }
}
";

            File.WriteAllText(path + "AutoMapperModule.cs", text2write, System.Text.Encoding.UTF8);
        }
        public void CreateDependencyResolverFile()
        {
            var path = Application.StartupPath + @"\Business\DependencyResolvers\Ninject\";
            if (!path.EndsWith("\\")) path += "\\";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            var text2write = @"using Ninject;

namespace " + txtProjectName.Text + @".Business.DependencyResolvers.Ninject
{
    public class DependencyResolver<T>
    {
        public static T Resolve()
        {
            IKernel kernel = new StandardKernel(new ResolveModule(),new AutoMapperModule());
           
            return kernel.Get<T>();
        }
    }
}
";

            File.WriteAllText(path + "DependencyResolver.cs", text2write, System.Text.Encoding.UTF8);
        }
        public void CreateInstanceFactoryFile()
        {
            var path = Application.StartupPath + @"\Business\DependencyResolvers\Ninject\";
            if (!path.EndsWith("\\")) path += "\\";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            var text2write = @"using Ninject;

namespace " + txtProjectName.Text + @".Business.DependencyResolvers.Ninject
{
    public class InstanceFactory
    {
        public static T GetInstance<T>()
        {
            var kernel=new StandardKernel(new BusinessModule(),new AutoMapperModule());
            return kernel.Get<T>();
        }
    }
}
";


        }
        public void CreateNinjectValidatoryFactoryFile()
        {
            var path = Application.StartupPath + @"\Business\DependencyResolvers\Ninject\";
            if (!path.EndsWith("\\")) path += "\\";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            var text2write = @"using System;
using FluentValidation;
using Ninject;

namespace " + txtProjectName.Text + @".Business.DependencyResolvers.Ninject
{
   public class NinjectValidatoryFactory: ValidatorFactoryBase
    {
        private readonly IKernel _kernel;

        public NinjectValidatoryFactory()
        {
            _kernel = new StandardKernel(new ValidationModule());
        }

        public override IValidator CreateInstance(Type validatorType)
        {
            //var bindings = (List<IBinding>)_kernel.GetBindings(validatorType);

            //if (bindings.Count > 0)
            //    return (IValidator)_kernel.Get(validatorType);

            //return null;

            return (validatorType == null) ? null : (IValidator)_kernel.TryGet(validatorType);
        }
    }
}
";

        }
        public void CreateValidationModule()
        {
            var path = Application.StartupPath + @"\Business\DependencyResolvers\Ninject\";
            if (!path.EndsWith("\\")) path += "\\";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            var text2write = @"using FluentValidation;
using " + txtProjectName.Text + @".Business.ValidationRules.FluentValidation;
using " + txtProjectName.Text + @".Entities.Concrete;
using Ninject.Modules;

namespace " + txtProjectName.Text + @".Business.DependencyResolvers.Ninject
{
   public class ValidationModule:NinjectModule
    {
        public override void Load()
        {" + Environment.NewLine;
            DirectoryInfo entityInfo = new DirectoryInfo(textBox2.Text);
            foreach (var item in entityInfo.GetFiles())
            {
                var str = item.Name.Replace(".cs", "");


                text2write += $"Bind<IValidator<{str}>>().To<{str}Validator>().InSingletonScope();" +
                              Environment.NewLine;

            }
            text2write += @"
        }
    }
}
";

            File.WriteAllText(path + "ValidationModule.cs", text2write, System.Text.Encoding.UTF8);
        }
        public void CreateServiceModule()
        {
            var path = Application.StartupPath + @"\Business\DependencyResolvers\Ninject\";
            if (!path.EndsWith("\\")) path += "\\";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            var text2write = @"using " + txtProjectName.Text + @".Business.Abstract;
using NewGenFramework.Core.Utilities.Common;
using Ninject.Modules;

namespace " + txtProjectName.Text + @".Business.DependencyResolvers.Ninject
{
    public class ServiceModule:NinjectModule
    {
        public override void Load()
        {" + Environment.NewLine;
            DirectoryInfo entityInfo = new DirectoryInfo(textBox2.Text);
            foreach (var item in entityInfo.GetFiles())
            {
                var str = item.Name.Replace(".cs", "");


                text2write += @"Bind<I" + str + "Service().ToConstant(WCFProxy<I" + str + "Service>.CreateChannel());" +
                              Environment.NewLine;

            }
            text2write += @"
        }
    }
}
";

            File.WriteAllText(path + "ServiceModule.cs", text2write, System.Text.Encoding.UTF8);
        }
        public void CreateResolveModuleFile()
        {
            var path = Application.StartupPath + @"\Business\DependencyResolvers\Ninject\";
            if (!path.EndsWith("\\")) path += "\\";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            var text2write = @"using Ninject;
using Ninject.Modules;

namespace " + txtProjectName.Text + @".Business.DependencyResolvers.Ninject
{
    public class ResolveModule : NinjectModule
    {
        public override void Load()
        {
            Kernel.Load(new BusinessModule());
            //var soaSetting = ConfigurationManager.AppSettings[/*SOA webconfig Key*/];

            //var soa = !string.IsNullOrEmpty(soaSetting) && soaSetting.ToBoolean();

            //if (soa)
            //{
            //    Kernel.Load(new ServiceModule());
            //}
            //else
            //{
            //    Kernel.Load(new BusinessModule());
            //}
        }
    }
}
";

            File.WriteAllText(path + "ResolveModule.cs", text2write, System.Text.Encoding.UTF8);
        }
        private void CreateMVCListViewModel()
        {
            var path = Application.StartupPath + @"\MVCUI\Models\ListViewModels";
            var gen = new CreateMvcListViewModel();
            foreach (var type in typeof(Language).Assembly.GetTypes())
            {
                var t = type.GetInterfaces();
                if (t != null && t.Length > 0 && t[0] != typeof(IEntity)) continue;
                gen.Add(type);
            }

            gen.Generate(path);
        }
        private void CreateMVCViewModel()
        {
            var path = Application.StartupPath + @"\MVCUI\Models\ViewModels";
            var gen = new CreateMvcViewModel();
            foreach (var type in typeof(Language).Assembly.GetTypes())
            {
                var t = type.GetInterfaces();
                if (t != null && t.Length > 0 && t[0] != typeof(IEntity)) continue;
                gen.Add(type);
            }

            gen.Generate(path);
        }
        private void CreateController()
        {
            var path = Application.StartupPath + @"\MVCUI\Controllers";
            var gen = new CreateController();
            foreach (var type in typeof(Language).Assembly.GetTypes())
            {
                var t = type.GetInterfaces();
                if (t != null && t.Length > 0 && t[0] != typeof(IEntity)) continue;
                gen.Add(type);
            }

            gen.Generate(path);
        }

        private void CreateMvcUiListViews()
        {
            var path = Application.StartupPath + @"\MVCUI\Views";
            var gen = new CreateMvcUiListViews();
            var typess = typeof(Language).Assembly.GetTypes();
            foreach (var type in typess)
            {
                var t = type.GetInterfaces();
                if (t != null && t.Length > 0 && t[0] != typeof(IEntity)) continue;
                gen.Add(type);
            }

            gen.Generate(path);
        }

        private void CreatePageInfoFile()
        {
            var path = Application.StartupPath + @"\MVCUI\Models\";
            if (!path.EndsWith("\\")) path += "\\";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            var text2write = @"namespace " + txtProjectName.Text + @".MvcWebUi.Models
{
    public class PagingInfo
    {
        public int CurrentPage { get; set; }
        public int TotalPageCount { get; set; }
        public string BaseUrl { get; set; }
    }
}
";

            File.WriteAllText(path + "PageInfo.cs", text2write, System.Text.Encoding.UTF8);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (chkNh.Checked)
            {
                //NHibernateGenerators
                CreateDataAccessNhibernateMapFiles();
                CreateDataAccessNhConcreteFiles();
                CreateDataAccessNhSqlServerHelperClass();
            }
            else if (chkEf.Checked)
            {
                //EntityFrameworkGenerators
                CreateDataAccessEfConcreteFiles();
                CreateDataAccessEntityFrameworkMapFiles();
            }
            else
            {
                MessageBox.Show("Hiç Bir Data Erişim Alt Yapı Seçmediniz!");
                return;
            }
            //CommonFilesGenerators
            CreateDataAccessAbstractFiles();
            CreateDataAccessContextFile();
            CreateBusinessAbstractFiles();
            CreateBusinessManagerFiles();
            CreateBusinessFluentValidationValidationRulesGenerator();
            CreateBusinessMappingsAutoMapperProfileFile();
            CreateBusinessModuleFile();
            CreateAutoMapperModuleFile();
            CreateDependencyResolverFile();
            CreateInstanceFactoryFile();
            CreateNinjectValidatoryFactoryFile();
            CreateResolveModuleFile();
            CreateValidationModule();
            CreateServiceModule();
            CreateMVCListViewModel();
            CreatePageInfoFile();
            CreateMVCViewModel();
            CreateMvcUiListViews();
            CreateController();
            MessageBox.Show("Dosyalar Yaratıldı.\r\nDataAccess,Business ve MVC Katmanları Yaratıldı.");
            string myPath = Application.StartupPath;
            System.Diagnostics.Process prc = new System.Diagnostics.Process();
            prc.StartInfo.FileName = myPath;
            prc.Start();
        }


    }
}
