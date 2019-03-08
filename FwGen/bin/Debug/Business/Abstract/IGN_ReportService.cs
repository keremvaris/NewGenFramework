
using System.Collections.Generic;
using NewGenFramework.Entities.ComplexTypes;
using NewGenFramework.Entities.Concrete;

namespace NewGenFramework.Business.Abstract
{
		public interface IGN_ReportService
		{
				List<GN_Report> GetAll();
				GN_Report GetById(int gN_ReportId);
				List<GN_Report> GetByGN_Report(int gN_ReportId);
				
				GN_Report Add(GN_Report gN_Report);
				void Update(GN_Report gN_Report);
				void Delete(GN_Report gN_Report);

		}
}