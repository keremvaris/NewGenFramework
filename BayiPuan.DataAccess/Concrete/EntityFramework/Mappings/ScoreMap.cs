using System.Data.Entity.ModelConfiguration;
using BayiPuan.Entities.Concrete;

namespace BayiPuan.DataAccess.Concrete.EntityFramework.Mappings
{
  public class ScoreMap : EntityTypeConfiguration<Score>
  {
    public ScoreMap()
    {
      ToTable("Scores", "dbo");
      HasKey(x => x.ScoreId);
      Property(x => x.ScoreId).HasColumnName("ScoreId");
      Property(x => x.ScoreTotal).HasColumnName("ScoreTotal");
      Property(x => x.ScoreType).HasColumnName("ScoreType");
      Property(x => x.UserId).HasColumnName("UserId");
      Property(x => x.ScoreDate).HasColumnName("ScoreDate");
    }
  }
}