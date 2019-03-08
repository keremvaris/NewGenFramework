using System.Data.Entity.ModelConfiguration;
using NewGenFramework.Entities.Concrete;

namespace NewGenFramework.DataAccess.Concrete.EntityFramework.Mappings
{
  public class GN_ReportMap : EntityTypeConfiguration<GN_Report>
  {
    public GN_ReportMap()
    {
      ToTable("GN_Reports","dbo");
HasKey(x => x.ReportId);
Property(x => x.ReportId).HasColumnName("ReportId");
Property(x => x.ReportTitle).HasColumnName("ReportTitle");
Property(x => x.ReportSql).HasColumnName("ReportSql");
Property(x => x.ReportFilter).HasColumnName("ReportFilter");
Property(x => x.ReportAuthorization).HasColumnName("ReportAuthorization");

    }
  }
}