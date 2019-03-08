using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BayiPuan.Entities.Concrete
{
  public enum BuyState
  {
    [Display(Name = "İnceleme Bekliyor")]
    IncelemeBekliyor=1,
    [Display(Name = "Onaylandı, Gönderim Bekleniyor.")]
    OnaylandiGonderimBekliyor =2,
    [Display(Name = "Gönderildi")]
    Gonderildi = 3,
    [Display(Name = "Hediye Karşılanamıyor")]
    HediyeKarsilanamiyor=4
  }
}