using System.Data.Entity.ModelConfiguration;
using BayiPuan.Entities.ComplexTypes;


namespace BayiPuan.DataAccess.Concrete.Context
{
    internal class vwRP_StockCountMap : EntityTypeConfiguration<vwRP_StockCount>
    {
        public vwRP_StockCountMap()
        {
            ToTable("vwRP_StockCount", "dbo");
            HasKey(x => x.TableName);
            Property(x => x.TableName).HasColumnName("TableName");
            Property(x => x.TableRows).HasColumnName("TableRows");
           
        }
    }
}