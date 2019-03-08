
using System.Collections.Generic;
using System.Linq;
using BayiPuan.DataAccess.Concrete.Context;


namespace BayiPuan.MvcWebUi.GenericVM
{
	public static class Tools
	{
		public static List<T> Select<T>(string sql, params object[] args)
		{
			//try
			//{
				using (var db = new BayiPuanContext())
				{
					return db.Database.SqlQuery<T>(sql, args).ToList();
				}

			//}
			//catch (Exception e)
			//{
			//	// throw new ApplicationException(sql);
			//	return null;
			//}
		}
	    
    }
}
