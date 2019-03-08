
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using NewGenFramework.Business.Abstract;
using NewGenFramework.Core.Aspects.Postsharp.AuthorizationAspects;
using NewGenFramework.Core.Aspects.Postsharp.CacheAspects;
using NewGenFramework.Core.Aspects.Postsharp.TransactionAspects;
using NewGenFramework.Core.CrossCuttingConcerns.Caching.Microsoft;
using NewGenFramework.DataAccess.Abstract;
using NewGenFramework.Entities.Concrete;
using NewGenFramework.Core.CrossCuttingConcerns.Transaction;

namespace NewGenFramework.Business.Concrete.Managers
{
    public class GN_ReportManager : ManagerBase, IGN_ReportService
    {
        private IGN_ReportDal _gN_ReportDal;

        public GN_ReportManager(IGN_ReportDal gN_ReportDal)
        {
            _gN_ReportDal = gN_ReportDal;
        }
        
         // [LogAspect(typeof(DatabaseLogger))]
        [CacheAspect(typeof(MemoryCacheManager))]
        // [PerformanceCounterAspect(1)]      
        public List<GN_Report> GetAll()
        {
            return _gN_ReportDal.GetList();
        }

        public GN_Report GetById(int gN_ReportId)
        {
            return _gN_ReportDal.Get(u => u.GN_ReportId == gN_ReportId);
        }      

        //[FluentValidationAspect(typeof(GN_ReportValidator))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public GN_Report Add(GN_Report gN_Report)
        {
            return _gN_ReportDal.Add(gN_Report);
        }
        //[FluentValidationAspect(typeof(GN_ReportValidator))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Update(GN_Report gN_Report)
        {
              _gN_ReportDal.Update(gN_Report);
        }

        public void Delete(GN_Report gN_Report)
        {
            _gN_ReportDal.Delete(gN_Report);
        }    

        public List<GN_Report> GetByGN_Report(int gN_ReportId)
        {
            return _gN_ReportDal.GetList(filter: t => t.GN_ReportId == gN_ReportId).ToList();
        }
    }
}
