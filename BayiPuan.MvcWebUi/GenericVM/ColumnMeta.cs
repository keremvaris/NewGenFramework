using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace BayiPuan.MvcWebUi.GenericVM
{
	public class ColumnMeta
	{
		public PropertyInfo Property { get; private set; }
		public DisplayAttribute Display { get; set; }
		public bool Required { get; internal set; }
		public Type InnerType { get; internal set; }
		public TableMeta PrimaryTable { get; internal set; }
		public DataTypeAttribute DataType { get; internal set; }
		public bool IsVisible { get; internal set; } = true;
				public bool IsReadonly { get; internal set; }
		public UIHintAttribute UIHint { get; set; }
		public string Template { get; set; }	

		public List<ListItem> ManualLookup { get; private set; }

				public bool NotMapped { get; set; }
				/// <summary>
				/// Bağlı master lookup varsa o kolonun tanımıdır.
				/// </summary>
				public ColumnMeta ParentColumn { get; set; }

		public ColumnMeta(PropertyInfo property)
		{
			Property = property;
		}

		public void SetManualLookup(List<ListItem> items)
		{
			ManualLookup = items;
			Template = "Enum";
		}

			public override string ToString()
			{
					return Property.Name;
			}
	}
}