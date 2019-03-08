using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Dynamic;    
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

//using GenericFW.Core;

namespace BayiPuan.MvcWebUi.GenericVM
{
	public static class CrudExtensions
	{
		private static ConcurrentDictionary<Type, TableMeta> _metaCache =
				new ConcurrentDictionary<Type, TableMeta>();
		private static Dictionary<string, TableMeta> _metaCacheByName =
			new Dictionary<string, TableMeta>();
		public static IDictionary<string, object> ToDynamic(this object value)
		{
			IDictionary<string, object> expando = new ExpandoObject();

			foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(value.GetType()))
				expando.Add(property.Name, property.GetValue(value));
			expando.Add("_type", value.GetType());

			return expando;
		}

		public static void ResolveFkAttributes(Type foreignType, ColumnMeta fkColumn)
		{
			var foreignMeta = _metaCache.GetOrAdd(foreignType, AddMeta);
			fkColumn.PrimaryTable = foreignMeta;
			fkColumn.Template = "Lookup";
		}

		private static Dictionary<Type, DataType> _type2dt = new Dictionary<Type, DataType>
		{
			{ typeof(string), DataType.Text },
			{ typeof(DateTime), DataType.DateTime }
		};

		class OrderComparer : IComparer<ColumnMeta>
		{
			public int Compare(ColumnMeta x, ColumnMeta y)
			{
				var xo = x?.Display.GetOrder() ?? 0;
				var yo = y?.Display.GetOrder() ?? 0;
				if (xo < yo) return -1;
				if (xo > yo) return 1;
				return 0;
			}
		}

		private static OrderComparer defaultComparer = new OrderComparer();

		public static TableMeta Register<T>()
		{
			var meta = _metaCache.GetOrAdd(typeof(T), AddMeta);
			return meta;
		}
		private static TableMeta AddMeta(Type _type)
		{
			var tm = new TableMeta();

			tm.Caption = _type.GetCustomAttribute<DisplayNameAttribute>();
			tm.DisplayColumn = _type.GetCustomAttribute<DisplayColumnAttribute>();
			tm.ActualTableName = _type.GetCustomAttribute<TableAttribute>()?.Name ?? _type.Name;

			List<PropertyInfo> ShowColumns = new List<PropertyInfo>();
			PropertyInfo[] columns = _type.GetProperties();
		    ColumnMeta actualMaster = null;

			foreach (var col in columns)
			{
				var colMeta = new ColumnMeta(col);
				tm.Add(colMeta);
			}

			var removeList = new List<ColumnMeta>();

			foreach (var colMeta in tm.Columns)
			{
				var col = colMeta.Property;
				var skipColumn = false;
				foreach (var attr in col.GetCustomAttributes())
				{
					if (attr is KeyAttribute)
					{
						tm.PrimaryColumn = col;
					}
					else if (attr is DisplayAttribute)
					{
						colMeta.Display = attr as DisplayAttribute;
					}
					else if (attr is RequiredAttribute)
					{
						colMeta.Required = true;
					}
					else if (attr is NotMappedAttribute)
					{
					    colMeta.NotMapped = true;
					}
					else if (attr is ReadOnlyAttribute)
					{
					    colMeta.IsReadonly = true;
					}
					else if (attr is ForeignKeyAttribute)
					{
						var fkAttr = attr as ForeignKeyAttribute;
						if (fkAttr != null)
						{
							// Ilgili kolonu bul
							var fkCol = tm.Columns.First(c => c.Property.Name == (fkAttr as ForeignKeyAttribute).Name);

							ResolveFkAttributes(col.PropertyType, fkCol);
							// bu kolonu atla
							skipColumn = true;
						}
					}
					else if (attr is DataTypeAttribute)
					{
						colMeta.DataType = attr as DataTypeAttribute;
					}
					else if (attr is ScaffoldColumnAttribute)
					{
						colMeta.IsVisible = (attr as ScaffoldColumnAttribute).Scaffold;
					}
					else if (attr is UIHintAttribute)
					{
						colMeta.UIHint = (attr as UIHintAttribute);
					}
					else if (attr is UIAttribute)
					{
						var cpcn = (attr as UIAttribute).CascadeParentColumnName;

						colMeta.ParentColumn = tm.Columns.First(c => c.Property.Name == cpcn);
					    if ((attr as UIAttribute).IsMaster) actualMaster = colMeta;
					}
				}
				if (skipColumn)
				{
					removeList.Add(colMeta);
					continue;
				}
				colMeta.InnerType = Nullable.GetUnderlyingType(col.PropertyType);
				if (colMeta.InnerType != null)
				{
					colMeta.Required = false;
				}
				else
				{
					colMeta.InnerType = col.PropertyType;
				}
				// Eğer enum varsa
				if(colMeta.InnerType.IsEnum)
				{
					var names = Enum.GetNames(colMeta.InnerType).AsQueryable();
					var values = Enum.GetValues(colMeta.InnerType).Cast<int>().AsQueryable();

					colMeta.SetManualLookup(
						names.Zip(values, (n, v) => new ListItem { Id = Convert.ToInt32(v), Text = n }).ToList()
						);
				}

				// Datatype verilmemişse, anlamaya çalış.
				if(colMeta.DataType == null)
				{
					DataType dt;
					if(_type2dt.TryGetValue(colMeta.InnerType, out dt)) {
						colMeta.DataType = new DataTypeAttribute(dt);
					}
				}

			}
			removeList.ForEach(c => tm.Columns.Remove(c));

			// Order'a göre sırala.
			tm.Columns.Sort(defaultComparer); 
			// Nesnenin bariz master ı varsa bul ve set et.
			var fks = tm.Columns.Where(c => c.PrimaryTable != null).ToArray();
			switch (fks.Length)
			{
                case 0:
                    break;
				case 1:
					tm.MasterFk = fks[0];
					break;
                default:
                    tm.MasterFk = actualMaster;
				    break;
			}
			if(!_metaCacheByName.ContainsKey(tm.ActualTableName))
				_metaCacheByName.Add(tm.ActualTableName, tm);

			return tm;

		}

		public static GenericListVM ToListVM<T>(this List<T> values) where T : class
		{
			var _type = typeof(T);

			TableMeta meta = _metaCache.GetOrAdd(_type, AddMeta);


			var result = new GenericListVM(meta);


			result.Items = new List<IDictionary<string, object>>();

			foreach (var v in values)
				result.Items.Add(v.ToDynamic());

			return result;

		}

		public static GenericVM ToVM(this object value)
		{
			var _type = value.GetType();

			TableMeta meta = _metaCache.GetOrAdd(_type, AddMeta);


			var result = new GenericVM(meta);
			result.Data = value.ToDynamic();


			return result;
		}

		public static GenericVM ToVM(this Type type, object data)
		{
			TableMeta meta = _metaCache.GetOrAdd(type, AddMeta);


			var result = new GenericVM(meta);
			result.Data = data.ToDynamic();


			return result;
		}

		public static TableMeta Get(Type type)
		{
		  try
		  {
		    return _metaCache[type];
      }
		  catch (Exception e)
		  {
		    Console.WriteLine(e);
		    throw;
		  }
			
		}

		public static TableMeta Get(string typeName)
		{
			return _metaCacheByName[typeName];
		}
	}
}