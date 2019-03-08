using NewGenFramework.Core.Entities;

namespace BayiPuan.Entities.ComplexTypes
{
    public class ProductDetail : IEntity
    {
        public virtual int ProductId { get; set; }
        public virtual string ProductName { get; set; }
        public virtual string CategoryName { get; set; }

    }
}
