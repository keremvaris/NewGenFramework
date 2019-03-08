using System.Data.Entity.ModelConfiguration;
using BayiPuan.Entities.Concrete;

namespace BayiPuan.DataAccess.Concrete.EntityFramework.Mappings
{
  public class LanguageMap : EntityTypeConfiguration<Language>
  {
    public LanguageMap()
    {
      ToTable("Languages","dbo");
HasKey(x => x.LanguageId);
Property(x => x.LanguageId).HasColumnName("LanguageId");
Property(x => x.Name).HasColumnName("Name");
Property(x => x.Code).HasColumnName("Code");

    }
  }
}