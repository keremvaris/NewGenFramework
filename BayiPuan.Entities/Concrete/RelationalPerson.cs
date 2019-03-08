using NewGenFramework.Core.Entities;

namespace BayiPuan.Entities.Concrete
{
    public class RelationalPerson : IEntity
    {
        public virtual int RelationalPersonId { get; set; }
        public virtual string RelationalPersonName { get; set; }
        public virtual string RelationalPersonSurname { get; set; }
        public virtual string MobilePhone { get; set; }

    }
}