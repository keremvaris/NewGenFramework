
using NewGenFramework.Core.Entities;

namespace BayiPuan.Entities.Concrete
{
    public class LanguageWord : IEntity
    {
        public virtual int Id { get; set; }
        public virtual int LanguageId { get; set; }
        public virtual string Code { get; set; }
        public virtual string Value { get; set; }
    }
}
