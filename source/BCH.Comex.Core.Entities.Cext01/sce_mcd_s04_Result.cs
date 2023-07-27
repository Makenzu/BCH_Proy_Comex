using System;

namespace BCH.Comex.Core.Entities.Cext01
{
    public class sce_mcd_s04_MS_Result
    {
      public string codcct { get; set; }
      public string codpro { get; set; }
      public string codesp { get; set; }
      public string codofi { get; set; }
      public string codope { get; set; }
      public decimal nrofac    { get; set; }
      public decimal nrorpt { get; set; }
      public DateTime fecfac   { get; set; }
      public decimal netofac { get; set; }
      public decimal ivafac { get; set; }
      public decimal montofac { get; set; }
      public decimal monedafac { get; set; }
      public string tipofac { get; set; }
    }
}
