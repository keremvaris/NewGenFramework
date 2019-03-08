using FluentValidation;
using NewGenFramework.Business.ValidationRules.FluentValidation;
using NewGenFramework.Entities.Concrete;
using Ninject.Modules;

namespace NewGenFramework.Business.DependencyResolvers.Ninject
{
   public class ValidationModule:NinjectModule
    {
        public override void Load()
        {
Bind<IValidator<Answer>>().To<AnswerValidator>().InSingletonScope();
Bind<IValidator<AnswerType>>().To<AnswerTypeValidator>().InSingletonScope();
Bind<IValidator<Brand>>().To<BrandValidator>().InSingletonScope();
Bind<IValidator<Buy>>().To<BuyValidator>().InSingletonScope();
Bind<IValidator<BuyState>>().To<BuyStateValidator>().InSingletonScope();
Bind<IValidator<Campaign>>().To<CampaignValidator>().InSingletonScope();
Bind<IValidator<Category>>().To<CategoryValidator>().InSingletonScope();
Bind<IValidator<City>>().To<CityValidator>().InSingletonScope();
Bind<IValidator<Customer>>().To<CustomerValidator>().InSingletonScope();
Bind<IValidator<Gift>>().To<GiftValidator>().InSingletonScope();
Bind<IValidator<GN_Report>>().To<GN_ReportValidator>().InSingletonScope();
Bind<IValidator<KnowledgeTest>>().To<KnowledgeTestValidator>().InSingletonScope();
Bind<IValidator<Language>>().To<LanguageValidator>().InSingletonScope();
Bind<IValidator<LanguageWord>>().To<LanguageWordValidator>().InSingletonScope();
Bind<IValidator<MyNew>>().To<MyNewValidator>().InSingletonScope();
Bind<IValidator<MyProduct>>().To<MyProductValidator>().InSingletonScope();
Bind<IValidator<NewsType>>().To<NewsTypeValidator>().InSingletonScope();
Bind<IValidator<Product>>().To<ProductValidator>().InSingletonScope();
Bind<IValidator<RelationalPerson>>().To<RelationalPersonValidator>().InSingletonScope();
Bind<IValidator<Role>>().To<RoleValidator>().InSingletonScope();
Bind<IValidator<Sale>>().To<SaleValidator>().InSingletonScope();
Bind<IValidator<Score>>().To<ScoreValidator>().InSingletonScope();
Bind<IValidator<ScoreType>>().To<ScoreTypeValidator>().InSingletonScope();
Bind<IValidator<Seller>>().To<SellerValidator>().InSingletonScope();
Bind<IValidator<UnitType>>().To<UnitTypeValidator>().InSingletonScope();
Bind<IValidator<User>>().To<UserValidator>().InSingletonScope();
Bind<IValidator<UserRole>>().To<UserRoleValidator>().InSingletonScope();
Bind<IValidator<UserType>>().To<UserTypeValidator>().InSingletonScope();

        }
    }
}
