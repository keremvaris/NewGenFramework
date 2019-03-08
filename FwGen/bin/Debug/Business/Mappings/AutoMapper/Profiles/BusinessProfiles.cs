using AutoMapper;
using NewGenFramework.Entities.Concrete;

namespace NewGenFramework.Business.Mappings.AutoMapper.Profiles
{
    public class BusinessProfiles:Profile
    {
        public BusinessProfiles()
        {
CreateMap<Answer, Answer>();
CreateMap<AnswerType, AnswerType>();
CreateMap<Brand, Brand>();
CreateMap<Buy, Buy>();
CreateMap<BuyState, BuyState>();
CreateMap<Campaign, Campaign>();
CreateMap<Category, Category>();
CreateMap<City, City>();
CreateMap<Customer, Customer>();
CreateMap<Gift, Gift>();
CreateMap<GN_Report, GN_Report>();
CreateMap<KnowledgeTest, KnowledgeTest>();
CreateMap<Language, Language>();
CreateMap<LanguageWord, LanguageWord>();
CreateMap<MyNew, MyNew>();
CreateMap<MyProduct, MyProduct>();
CreateMap<NewsType, NewsType>();
CreateMap<Product, Product>();
CreateMap<RelationalPerson, RelationalPerson>();
CreateMap<Role, Role>();
CreateMap<Sale, Sale>();
CreateMap<Score, Score>();
CreateMap<ScoreType, ScoreType>();
CreateMap<Seller, Seller>();
CreateMap<UnitType, UnitType>();
CreateMap<User, User>();
CreateMap<UserRole, UserRole>();
CreateMap<UserType, UserType>();

        }
    }
}
