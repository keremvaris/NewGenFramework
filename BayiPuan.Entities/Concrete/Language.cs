using NewGenFramework.Core.Entities;
namespace BayiPuan.Entities.Concrete
{
    public class Language : IEntity
    {
        public virtual int LanguageId { get; set; }
        public virtual string Name { get; set; }
        public virtual string Code { get; set; }
    }
}
