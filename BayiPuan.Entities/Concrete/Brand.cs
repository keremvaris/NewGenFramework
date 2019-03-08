using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewGenFramework.Core.Entities;

namespace BayiPuan.Entities.Concrete
{
    public class Brand:IEntity
    {
        public virtual int BrandId { get; set; }
        public virtual string BrandName { get; set; }
        public virtual byte[] BrandImage { get; set; }
        public virtual string BrandImageExt { get; set; }
    }
}
