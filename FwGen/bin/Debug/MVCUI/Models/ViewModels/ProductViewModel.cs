using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewGenFramework.MvcWebUi.Models.ViewModels
{               
[Table("Products")]
[DisplayColumn("ProductName")]
[DisplayName("Product")]
public class ProductViewModel
{
[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
[Display(Name = "ProductId Id", AutoGenerateField = false)]
public virtual int ProductId{ get; set; }
[Display(Name = "ProductCode"), Required()]
public virtual string ProductCode{ get; set; }
[Display(Name = "ProductShortCode"), Required()]
public virtual string ProductShortCode{ get; set; }
[Display(Name = "ProductName"), Required()]
public virtual string ProductName{ get; set; }
[Display(Name = "UnitType"), Required()]
public virtual unittype UnitType{ get; set; }
[Display(Name = "UnitTypeId"), Required()]
public virtual int UnitTypeId{ get; set; }
[ForeignKey("UnitTypeId")]
public virtual UnitTypeViewModel UnitType{ get; set; }
[Display(Name = "StockAmount"), Required()]
public virtual int StockAmount{ get; set; }
[Display(Name = "RemainingStockAmount"), Required()]
public virtual int RemainingStockAmount{ get; set; }
[Display(Name = "CriticalStockAmount"), Required()]
public virtual int CriticalStockAmount{ get; set; }
[Display(Name = "Point"), Required()]
public virtual decimal Point{ get; set; }
[Display(Name = "PointToMoney"), Required()]
public virtual decimal PointToMoney{ get; set; }

     }
}