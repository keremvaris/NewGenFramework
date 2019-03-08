using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewGenFramework.Core.Entities;

namespace BayiPuan.Entities.Concrete
{
    public class GN_Report:IEntity
    {
        public int ReportId { get; set; }
        public string ReportTitle { get; set; }
        public string ReportSql { get; set; }
        public string ReportFilter { get; set; }
        public string ReportAuthorization { get; set; }
    }
}
