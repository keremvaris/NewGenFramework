using System.Data.Entity.ModelConfiguration;
using NewGenFramework.Entities.Concrete;

namespace NewGenFramework.DataAccess.Concrete.EntityFramework.Mappings
{
  public class AnswerMap : EntityTypeConfiguration<Answer>
  {
    public AnswerMap()
    {
      ToTable("Answers","dbo");
HasKey(x => x.AnswerId);
Property(x => x.AnswerId).HasColumnName("AnswerId");
Property(x => x.KnowledgeTest).HasColumnName("KnowledgeTest");
Property(x => x.KnowledgeTestId).HasColumnName("KnowledgeTestId");
Property(x => x.User).HasColumnName("User");
Property(x => x.UserId).HasColumnName("UserId");
Property(x => x.AnswerDate).HasColumnName("AnswerDate");
Property(x => x.ValidAnswer).HasColumnName("ValidAnswer");

    }
  }
}