using FluentValidation;
using BayiPuan.Business.ValidationRules.FluentValidation;
using BayiPuan.Entities.Concrete;
using Ninject.Modules;

namespace BayiPuan.Business.DependencyResolvers.Ninject
{
  public class ValidationModule : NinjectModule
  {
    public override void Load()
    {
      Bind<IValidator<Brand>>().To<BrandValidator>().InSingletonScope();
      Bind<IValidator<Buy>>().To<BuyValidator>().InSingletonScope();
      Bind<IValidator<Campaign>>().To<CampaignValidator>().InSingletonScope();
      Bind<IValidator<Category>>().To<CategoryValidator>().InSingletonScope();
      Bind<IValidator<City>>().To<CityValidator>().InSingletonScope();
      Bind<IValidator<Customer>>().To<CustomerValidator>().InSingletonScope();
      Bind<IValidator<Gift>>().To<GiftValidator>().InSingletonScope();
      Bind<IValidator<GN_Report>>().To<GN_ReportValidator>().InSingletonScope();
      Bind<IValidator<Language>>().To<LanguageValidator>().InSingletonScope();
      Bind<IValidator<LanguageWord>>().To<LanguageWordValidator>().InSingletonScope();
      Bind<IValidator<Product>>().To<ProductValidator>().InSingletonScope();
      Bind<IValidator<RelationalPerson>>().To<RelationalPersonValidator>().InSingletonScope();
      Bind<IValidator<Role>>().To<RoleValidator>().InSingletonScope();
      Bind<IValidator<Sale>>().To<SaleValidator>().InSingletonScope();
      Bind<IValidator<Seller>>().To<SellerValidator>().InSingletonScope();
      Bind<IValidator<UnitType>>().To<UnitTypeValidator>().InSingletonScope();
      Bind<IValidator<User>>().To<UserValidator>().InSingletonScope();
      Bind<IValidator<UserRole>>().To<UserRoleValidator>().InSingletonScope();
      Bind<IValidator<UserType>>().To<UserTypeValidator>().InSingletonScope();
      Bind<IValidator<Score>>().To<ScoreValidator>().InSingletonScope();
      Bind<IValidator<Answer>>().To<AnswerValidator>().InSingletonScope();
      Bind<IValidator<KnowledgeTest>>().To<KnowledgeTestValidator>().InSingletonScope();
      Bind<IValidator<MyNew>>().To<MyNewValidator>().InSingletonScope();
      Bind<IValidator<MyProduct>>().To<MyProductValidator>().InSingletonScope();

    }
  }
}
