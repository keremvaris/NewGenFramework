using NewGenFramework.Business.Abstract;
using NewGenFramework.Core.Utilities.Common;
using Ninject.Modules;

namespace NewGenFramework.Business.DependencyResolvers.Ninject
{
    public class ServiceModule:NinjectModule
    {
        public override void Load()
        {
Bind<IAnswerService().ToConstant(WCFProxy<IAnswerService>.CreateChannel());
Bind<IAnswerTypeService().ToConstant(WCFProxy<IAnswerTypeService>.CreateChannel());
Bind<IBrandService().ToConstant(WCFProxy<IBrandService>.CreateChannel());
Bind<IBuyService().ToConstant(WCFProxy<IBuyService>.CreateChannel());
Bind<IBuyStateService().ToConstant(WCFProxy<IBuyStateService>.CreateChannel());
Bind<ICampaignService().ToConstant(WCFProxy<ICampaignService>.CreateChannel());
Bind<ICategoryService().ToConstant(WCFProxy<ICategoryService>.CreateChannel());
Bind<ICityService().ToConstant(WCFProxy<ICityService>.CreateChannel());
Bind<ICustomerService().ToConstant(WCFProxy<ICustomerService>.CreateChannel());
Bind<IGiftService().ToConstant(WCFProxy<IGiftService>.CreateChannel());
Bind<IGN_ReportService().ToConstant(WCFProxy<IGN_ReportService>.CreateChannel());
Bind<IKnowledgeTestService().ToConstant(WCFProxy<IKnowledgeTestService>.CreateChannel());
Bind<ILanguageService().ToConstant(WCFProxy<ILanguageService>.CreateChannel());
Bind<ILanguageWordService().ToConstant(WCFProxy<ILanguageWordService>.CreateChannel());
Bind<IMyNewService().ToConstant(WCFProxy<IMyNewService>.CreateChannel());
Bind<IMyProductService().ToConstant(WCFProxy<IMyProductService>.CreateChannel());
Bind<INewsTypeService().ToConstant(WCFProxy<INewsTypeService>.CreateChannel());
Bind<IProductService().ToConstant(WCFProxy<IProductService>.CreateChannel());
Bind<IRelationalPersonService().ToConstant(WCFProxy<IRelationalPersonService>.CreateChannel());
Bind<IRoleService().ToConstant(WCFProxy<IRoleService>.CreateChannel());
Bind<ISaleService().ToConstant(WCFProxy<ISaleService>.CreateChannel());
Bind<IScoreService().ToConstant(WCFProxy<IScoreService>.CreateChannel());
Bind<IScoreTypeService().ToConstant(WCFProxy<IScoreTypeService>.CreateChannel());
Bind<ISellerService().ToConstant(WCFProxy<ISellerService>.CreateChannel());
Bind<IUnitTypeService().ToConstant(WCFProxy<IUnitTypeService>.CreateChannel());
Bind<IUserService().ToConstant(WCFProxy<IUserService>.CreateChannel());
Bind<IUserRoleService().ToConstant(WCFProxy<IUserRoleService>.CreateChannel());
Bind<IUserTypeService().ToConstant(WCFProxy<IUserTypeService>.CreateChannel());

        }
    }
}
