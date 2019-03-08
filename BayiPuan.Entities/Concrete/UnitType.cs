using NewGenFramework.Core.Entities;

namespace BayiPuan.Entities.Concrete
{
    public class UnitType:IEntity
    {
        public virtual int UnitTypeId { get; set; }
        public virtual string UnitTypeName { get; set; }
    }
}
