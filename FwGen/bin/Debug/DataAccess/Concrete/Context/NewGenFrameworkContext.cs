using System.Data.Entity;
using NewGenFramework.DataAccess.Concrete.EntityFramework.Mappings;
using NewGenFramework.Entities.Concrete;

namespace NewGenFramework.DataAccess.Concrete.Context
{    
    public class NewGenFrameworkContext:DbContext
    {
        public NewGenFrameworkContext()
        {
               Database.SetInitializer<NewGenFrameworkContext>(null/*new CreateDatabaseIfNotExists<NewGenFrameworkContext>()*/);
        }
public DbSet<Answer> Answers { get; set; }
public DbSet<AnswerType> AnswerTypes { get; set; }
public DbSet<Brand> Brands { get; set; }
public DbSet<Buy> Buies { get; set; }
public DbSet<BuyState> BuyStates { get; set; }
public DbSet<Campaign> Campaigns { get; set; }
public DbSet<Category> Categories { get; set; }
public DbSet<City> Cities { get; set; }
public DbSet<Customer> Customers { get; set; }
public DbSet<Gift> Gifts { get; set; }
public DbSet<GN_Report> GN_Reports { get; set; }
public DbSet<KnowledgeTest> KnowledgeTests { get; set; }
public DbSet<Language> Languages { get; set; }
public DbSet<LanguageWord> LanguageWords { get; set; }
public DbSet<MyNew> MyNews { get; set; }
public DbSet<MyProduct> MyProducts { get; set; }
public DbSet<NewsType> NewsTypes { get; set; }
public DbSet<Product> Products { get; set; }
public DbSet<RelationalPerson> RelationalPersons { get; set; }
public DbSet<Role> Roles { get; set; }
public DbSet<Sale> Sales { get; set; }
public DbSet<Score> Scores { get; set; }
public DbSet<ScoreType> ScoreTypes { get; set; }
public DbSet<Seller> Sellers { get; set; }
public DbSet<UnitType> UnitTypes { get; set; }
public DbSet<User> Users { get; set; }
public DbSet<UserRole> UserRoles { get; set; }
public DbSet<UserType> UserTypes { get; set; }
protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
modelBuilder.Configurations.Add(new AnswerMap());
modelBuilder.Configurations.Add(new AnswerTypeMap());
modelBuilder.Configurations.Add(new BrandMap());
modelBuilder.Configurations.Add(new BuyMap());
modelBuilder.Configurations.Add(new BuyStateMap());
modelBuilder.Configurations.Add(new CampaignMap());
modelBuilder.Configurations.Add(new CategoryMap());
modelBuilder.Configurations.Add(new CityMap());
modelBuilder.Configurations.Add(new CustomerMap());
modelBuilder.Configurations.Add(new GiftMap());
modelBuilder.Configurations.Add(new GN_ReportMap());
modelBuilder.Configurations.Add(new KnowledgeTestMap());
modelBuilder.Configurations.Add(new LanguageMap());
modelBuilder.Configurations.Add(new LanguageWordMap());
modelBuilder.Configurations.Add(new MyNewMap());
modelBuilder.Configurations.Add(new MyProductMap());
modelBuilder.Configurations.Add(new NewsTypeMap());
modelBuilder.Configurations.Add(new ProductMap());
modelBuilder.Configurations.Add(new RelationalPersonMap());
modelBuilder.Configurations.Add(new RoleMap());
modelBuilder.Configurations.Add(new SaleMap());
modelBuilder.Configurations.Add(new ScoreMap());
modelBuilder.Configurations.Add(new ScoreTypeMap());
modelBuilder.Configurations.Add(new SellerMap());
modelBuilder.Configurations.Add(new UnitTypeMap());
modelBuilder.Configurations.Add(new UserMap());
modelBuilder.Configurations.Add(new UserRoleMap());
modelBuilder.Configurations.Add(new UserTypeMap());
}
    }
}
