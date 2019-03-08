using System.Data.Entity.ModelConfiguration;
using BayiPuan.Entities.Concrete;

namespace BayiPuan.DataAccess.Concrete.EntityFramework.Mappings
{
  public class KnowledgeTestMap : EntityTypeConfiguration<KnowledgeTest>
  {
    public KnowledgeTestMap()
    {
      ToTable("KnowledgeTests","dbo");
HasKey(x => x.KnowledgeTestId);
Property(x => x.KnowledgeTestId).HasColumnName("KnowledgeTestId");
Property(x => x.Question).HasColumnName("Question");
Property(x => x.Answer1).HasColumnName("Answer1");
Property(x => x.Answer2).HasColumnName("Answer2");
Property(x => x.Answer3).HasColumnName("Answer3");
Property(x => x.Answer4).HasColumnName("Answer4");
Property(x => x.ValidAnswerType).HasColumnName("ValidAnswerType");
Property(x => x.Point).HasColumnName("Point");
Property(x => x.KnowledgeDate).HasColumnName("KnowledgeDate");
Property(x => x.IsActive).HasColumnName("IsActive");

    }
  }
}