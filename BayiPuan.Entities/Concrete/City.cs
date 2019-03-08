using NewGenFramework.Core.Entities;

namespace BayiPuan.Entities.Concrete
{
   public class City:IEntity
    {
        public virtual int CityId { get; set; }
        public virtual string CityName { get; set; }

    }
}
