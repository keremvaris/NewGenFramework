using System.Data.Entity.ModelConfiguration;
using BayiPuan.Entities.Concrete;

namespace BayiPuan.DataAccess.Concrete.EntityFramework.Mappings
{
  public class AnswersMap : EntityTypeConfiguration<Answer>
  {
    public AnswersMap()
    {
      ToTable("Answers", "dbo");
      HasKey(x => x.AnswerId);
      Property(x => x.AnswerId).HasColumnName("AnswerId");
      Property(x => x.KnowledgeTestId).HasColumnName("KnowledgeTestId");
      Property(x => x.UserId).HasColumnName("UserId");
      Property(x => x.AnswerDate).HasColumnName("AnswerDate");
      Property(x => x.ValidAnswer).HasColumnName("ValidAnswer");

    }
  }
}