using AutoMapper;
using BayiPuan.Entities.Concrete;

namespace BayiPuan.Business.Mappings.AutoMapper.Profiles
{
  public class BusinessProfiles : Profile
  {
    public BusinessProfiles()
    {
      CreateMap<Brand, Brand>();
      CreateMap<Campaign, Campaign>();
      CreateMap<Category, Category>();
      CreateMap<City, City>();
      CreateMap<Customer, Customer>();
      CreateMap<Language, Language>();
      CreateMap<LanguageWord, LanguageWord>();
      CreateMap<Product, Product>();
      CreateMap<RelationalPerson, RelationalPerson>();
      CreateMap<GN_Report, GN_Report>();
      CreateMap<Role, Role>();
      CreateMap<Sale, Sale>();
      CreateMap<Seller, Seller>();
      CreateMap<UnitType, UnitType>();
      CreateMap<User, User>();
      CreateMap<UserRole, UserRole>();
      CreateMap<UserType, UserType>();
      CreateMap<Gift, Gift>();
      CreateMap<Buy, Buy>();
      CreateMap<Score, Score>();
      CreateMap<Answer, Answer>();
      CreateMap<KnowledgeTest, KnowledgeTest>();
      CreateMap<MyNew, MyNew>();
      CreateMap<MyProduct, MyProduct>();
    }
  }
}
