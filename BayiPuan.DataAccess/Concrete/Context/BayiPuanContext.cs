using System.Data.Entity;
using BayiPuan.DataAccess.Concrete.EntityFramework.Mappings;
using BayiPuan.Entities.ComplexTypes;
using BayiPuan.Entities.Concrete;


namespace BayiPuan.DataAccess.Concrete.Context
{
  public class BayiPuanContext : DbContext
  {
    public BayiPuanContext()
    {
      Configuration.LazyLoadingEnabled = false;
      Configuration.ProxyCreationEnabled = false;
      Configuration.AutoDetectChangesEnabled = false;
      Database.SetInitializer<BayiPuanContext>(null/*new CreateDatabaseIfNotExists<BayiPuanContext>()*/);
    }
    public DbSet<MyNew> MyNews { get; set; }
    public DbSet<MyProduct> MyProducts { get; set; }
    public DbSet<Answer> Answers { get; set; }
    public DbSet<KnowledgeTest> KnowledgeTests { get; set; }
    public DbSet<Brand> Brands { get; set; }
    public DbSet<Campaign> Campaigns { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Buy> Buy { get; set; }
    public DbSet<City> Cities { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Language> Languages { get; set; }
    public DbSet<LanguageWord> LanguageWords { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<RelationalPerson> RelationalPersons { get; set; }
    public DbSet<GN_Report> GN_Reports { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Sale> Sales { get; set; }
    public DbSet<Seller> Sellers { get; set; }
    public DbSet<UnitType> UnitTypes { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<UserType> UserTypes { get; set; }
    public DbSet<vwRP_StockCount> StockCounts { get; set; }
    public DbSet<Gift> Gifts { get; set; }
    public DbSet<Score> Scores { get; set; }
    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
      modelBuilder.Configurations.Add(new BrandMap());
      modelBuilder.Configurations.Add(new CampaignMap());
      modelBuilder.Configurations.Add(new CategoryMap());
      modelBuilder.Configurations.Add(new CityMap());
      modelBuilder.Configurations.Add(new CustomerMap());
      modelBuilder.Configurations.Add(new LanguageMap());
      modelBuilder.Configurations.Add(new GN_ReportMap());
      modelBuilder.Configurations.Add(new LanguageWordMap());
      modelBuilder.Configurations.Add(new ProductMap());
      modelBuilder.Configurations.Add(new RelationalPersonMap());
      modelBuilder.Configurations.Add(new GiftMap());
      modelBuilder.Configurations.Add(new RoleMap());
      modelBuilder.Configurations.Add(new SaleMap());
      modelBuilder.Configurations.Add(new SellerMap());
      modelBuilder.Configurations.Add(new UnitTypeMap());
      modelBuilder.Configurations.Add(new UserMap());
      modelBuilder.Configurations.Add(new UserRoleMap());
      modelBuilder.Configurations.Add(new UserTypeMap());
      modelBuilder.Configurations.Add(new vwRP_StockCountMap());
      modelBuilder.Configurations.Add(new ScoreMap());
      modelBuilder.Configurations.Add(new MyNewMap());
      modelBuilder.Configurations.Add(new MyProductMap());
    }
  }
}
