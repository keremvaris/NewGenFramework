using System.Data.Entity.ModelConfiguration;
using NewGenFramework.Entities.Concrete;

namespace NewGenFramework.DataAccess.Concrete.EntityFramework.Mappings
{
  public class UserMap : EntityTypeConfiguration<User>
  {
    public UserMap()
    {
      ToTable("Users","dbo");
HasKey(x => x.UserId);
Property(x => x.UserId).HasColumnName("UserId");
Property(x => x.UserType).HasColumnName("UserType");
Property(x => x.UserTypeId).HasColumnName("UserTypeId");
Property(x => x.UserName).HasColumnName("UserName");
Property(x => x.Password).HasColumnName("Password");
Property(x => x.FirstName).HasColumnName("FirstName");
Property(x => x.LastName).HasColumnName("LastName");
Property(x => x.Email).HasColumnName("Email");
Property(x => x.MobilePhone).HasColumnName("MobilePhone");
Property(x => x.UserImage).HasColumnName("UserImage");
Property(x => x.BirthDate).HasColumnName("BirthDate");
Property(x => x.State).HasColumnName("State");
Property(x => x.Agreement).HasColumnName("Agreement");
Property(x => x.Seller).HasColumnName("Seller");
Property(x => x.SellerId).HasColumnName("SellerId");
Property(x => x.Contact).HasColumnName("Contact");

    }
  }
}