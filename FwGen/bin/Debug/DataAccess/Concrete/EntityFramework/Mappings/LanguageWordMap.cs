using System.Data.Entity.ModelConfiguration;
using NewGenFramework.Entities.Concrete;

namespace NewGenFramework.DataAccess.Concrete.EntityFramework.Mappings
{
  public class LanguageWordMap : EntityTypeConfiguration<LanguageWord>
  {
    public LanguageWordMap()
    {
      ToTable("LanguageWords","dbo");
HasKey(x => x.Id);
Property(x => x.Id).HasColumnName("Id");
Property(x => x.LanguageId).HasColumnName("LanguageId");
Property(x => x.Code).HasColumnName("Code");
Property(x => x.Value).HasColumnName("Value");

    }
  }
}