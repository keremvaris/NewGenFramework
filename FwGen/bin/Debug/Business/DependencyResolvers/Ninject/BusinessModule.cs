using NewGenFramework.Core.DataAccess.NHibernate;
using NewGenFramework.DataAccess.Abstract;
using NewGenFramework.DataAccess.Concrete.NHibernate;
using NewGenFramework.DataAccess.Concrete.NHibernate.Helpers;
using Ninject.Modules;

namespace NewGenFramework.Business.DependencyResolvers.Ninject
{
    public class BusinessModule:NinjectModule
    {
        public override void Load()
        {
Bind<IAnswerService>().To<AnswerManager>().InSingletonScope();
Bind<IAnswerDal>().To<EfAnswerDal>().InSingletonScope();

Bind<IAnswerTypeService>().To<AnswerTypeManager>().InSingletonScope();
Bind<IAnswerTypeDal>().To<EfAnswerTypeDal>().InSingletonScope();

Bind<IBrandService>().To<BrandManager>().InSingletonScope();
Bind<IBrandDal>().To<EfBrandDal>().InSingletonScope();

Bind<IBuyService>().To<BuyManager>().InSingletonScope();
Bind<IBuyDal>().To<EfBuyDal>().InSingletonScope();

Bind<IBuyStateService>().To<BuyStateManager>().InSingletonScope();
Bind<IBuyStateDal>().To<EfBuyStateDal>().InSingletonScope();

Bind<ICampaignService>().To<CampaignManager>().InSingletonScope();
Bind<ICampaignDal>().To<EfCampaignDal>().InSingletonScope();

Bind<ICategoryService>().To<CategoryManager>().InSingletonScope();
Bind<ICategoryDal>().To<EfCategoryDal>().InSingletonScope();

Bind<ICityService>().To<CityManager>().InSingletonScope();
Bind<ICityDal>().To<EfCityDal>().InSingletonScope();

Bind<ICustomerService>().To<CustomerManager>().InSingletonScope();
Bind<ICustomerDal>().To<EfCustomerDal>().InSingletonScope();

Bind<IGiftService>().To<GiftManager>().InSingletonScope();
Bind<IGiftDal>().To<EfGiftDal>().InSingletonScope();

Bind<IGN_ReportService>().To<GN_ReportManager>().InSingletonScope();
Bind<IGN_ReportDal>().To<EfGN_ReportDal>().InSingletonScope();

Bind<IKnowledgeTestService>().To<KnowledgeTestManager>().InSingletonScope();
Bind<IKnowledgeTestDal>().To<EfKnowledgeTestDal>().InSingletonScope();

Bind<ILanguageService>().To<LanguageManager>().InSingletonScope();
Bind<ILanguageDal>().To<EfLanguageDal>().InSingletonScope();

Bind<ILanguageWordService>().To<LanguageWordManager>().InSingletonScope();
Bind<ILanguageWordDal>().To<EfLanguageWordDal>().InSingletonScope();

Bind<IMyNewService>().To<MyNewManager>().InSingletonScope();
Bind<IMyNewDal>().To<EfMyNewDal>().InSingletonScope();

Bind<IMyProductService>().To<MyProductManager>().InSingletonScope();
Bind<IMyProductDal>().To<EfMyProductDal>().InSingletonScope();

Bind<INewsTypeService>().To<NewsTypeManager>().InSingletonScope();
Bind<INewsTypeDal>().To<EfNewsTypeDal>().InSingletonScope();

Bind<IProductService>().To<ProductManager>().InSingletonScope();
Bind<IProductDal>().To<EfProductDal>().InSingletonScope();

Bind<IRelationalPersonService>().To<RelationalPersonManager>().InSingletonScope();
Bind<IRelationalPersonDal>().To<EfRelationalPersonDal>().InSingletonScope();

Bind<IRoleService>().To<RoleManager>().InSingletonScope();
Bind<IRoleDal>().To<EfRoleDal>().InSingletonScope();

Bind<ISaleService>().To<SaleManager>().InSingletonScope();
Bind<ISaleDal>().To<EfSaleDal>().InSingletonScope();

Bind<IScoreService>().To<ScoreManager>().InSingletonScope();
Bind<IScoreDal>().To<EfScoreDal>().InSingletonScope();

Bind<IScoreTypeService>().To<ScoreTypeManager>().InSingletonScope();
Bind<IScoreTypeDal>().To<EfScoreTypeDal>().InSingletonScope();

Bind<ISellerService>().To<SellerManager>().InSingletonScope();
Bind<ISellerDal>().To<EfSellerDal>().InSingletonScope();

Bind<IUnitTypeService>().To<UnitTypeManager>().InSingletonScope();
Bind<IUnitTypeDal>().To<EfUnitTypeDal>().InSingletonScope();

Bind<IUserService>().To<UserManager>().InSingletonScope();
Bind<IUserDal>().To<EfUserDal>().InSingletonScope();

Bind<IUserRoleService>().To<UserRoleManager>().InSingletonScope();
Bind<IUserRoleDal>().To<EfUserRoleDal>().InSingletonScope();

Bind<IUserTypeService>().To<UserTypeManager>().InSingletonScope();
Bind<IUserTypeDal>().To<EfUserTypeDal>().InSingletonScope();


            Bind(typeof(IQueryableRepository<>)).To(typeof(EFQueryableRepository<>));
          Bind<DbContext>().To<NewGenFrameworkContext>();
        }
    }
}
