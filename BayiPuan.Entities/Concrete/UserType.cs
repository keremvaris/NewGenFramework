using NewGenFramework.Core.Entities;

namespace BayiPuan.Entities.Concrete
{
    public class UserType:IEntity
    {
        public virtual int UserTypeId { get; set; }
        public virtual string UserTypeName { get; set; }

    }
}
