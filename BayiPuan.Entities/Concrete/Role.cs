using NewGenFramework.Core.Entities;

namespace BayiPuan.Entities.Concrete
{
    public class Role:IEntity
    {
        public virtual int RoleId { get; set; }
        public virtual string RoleName { get; set; }

    }
}
