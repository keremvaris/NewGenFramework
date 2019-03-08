using NewGenFramework.Core.Entities;

namespace BayiPuan.Entities.Concrete
{
    public class UserRole:IEntity
    {
        public virtual int UserRoleId { get; set; }
        public virtual int RoleId { get; set; }
        public Role Role { get; set; }
        public User User { get; set; }
        public virtual int UserId { get; set; }  
    } 
}
