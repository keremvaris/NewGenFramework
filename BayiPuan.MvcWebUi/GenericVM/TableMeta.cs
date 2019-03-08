using System.Collections.Generic;
using System.Reflection;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace BayiPuan.MvcWebUi.GenericVM
{
	public class TableMeta
	{
		public List<ColumnMeta> Columns { get; set; }
		public PropertyInfo PrimaryColumn { get; set; }
		public PropertyInfo IdentityColumn { get; set; }
		public string ControllerName { get; set; }

		public string ActualTableName { get; set; }

		public DisplayColumnAttribute DisplayColumn { get; internal set; }
		public DisplayNameAttribute Caption { get; internal set; }

        public ColumnMeta MasterFk { get; set; }

		public TableMeta()
		{
			Columns = new List<ColumnMeta>();
		}
		public void Add(ColumnMeta column)
		{
			Columns.Add(column);
		}

	}
}