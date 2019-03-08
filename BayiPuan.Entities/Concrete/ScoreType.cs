using System.ComponentModel.DataAnnotations;

namespace BayiPuan.Entities.Concrete
{
  public enum ScoreType
  {
    [Display(Name = "Ürün")]
    UrunModulu=1,
    [Display(Name = "Puan Artır")]
    YoneticiPuanArtır=2,
    [Display(Name="Bilgini Sına")]
    BilgiModulu=3
  }
}