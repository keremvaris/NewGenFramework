using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewGenFramework.Core.Entities;

namespace BayiPuan.Entities.ComplexTypes
{
    public class vwRP_StockCount:IEntity
    {
        public string TableName { get; set; }
        public long TableRows { get; set; }
       
    }
}
