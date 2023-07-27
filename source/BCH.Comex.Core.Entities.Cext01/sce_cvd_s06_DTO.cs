using System;

namespace BCH.Comex.Core.Entities.Cext01
{
    public partial class sce_cvd_s06_MS_DTO
    {
        public sce_cvd_s06_MS_DTO_03 codpro_03 { get; set; }
        public sce_cvd_s06_MS_DTO_05 codpro_05 { get; set; }
        public sce_cvd_s06_MS_DTO_06 codpro_06 { get; set; }
        public sce_cvd_s06_MS_DTO_07 codpro_07 { get; set; }
        public sce_cvd_s06_MS_DTO_08 codpro_08 { get; set; }
        public sce_cvd_s06_MS_DTO_09 codpro_09 { get; set; }
        public sce_cvd_s06_MS_DTO_17 codpro_17 { get; set; }
        public sce_cvd_s06_MS_DTO_18 codpro_18 { get; set; }
        public sce_cvd_s06_MS_DTO_20 codpro_20 { get; set; }
        public sce_cvd_s06_MS_DTO_30 codpro_30 { get; set; }
    }

    #region Clases de Resultados
    public class sce_cvd_s06_MS_DTO_05
    {
        public string codcct { get; set; }
        public string codpro { get; set; }
        public string codesp { get; set; }
        public string codofi { get; set; }
        public string codope { get; set; }
        public Nullable <DateTime> fecing { get; set; }
        public Nullable<int> campo6 { get; set; }
        public string prtcli { get; set; }
        public Nullable <decimal> indnomc { get; set; }
        public Nullable <decimal> inddirc { get; set; }
        public int TipCVD { get; set; } 
    }
    public class sce_cvd_s06_MS_DTO_03 : sce_cvd_s06_MS_DTO_05
    {
        
    }
    public class sce_cvd_s06_MS_DTO_07 : sce_cvd_s06_MS_DTO_05
    {
    }
    public class sce_cvd_s06_MS_DTO_08 : sce_cvd_s06_MS_DTO_05
    {
    }
    public class sce_cvd_s06_MS_DTO_20
    {
        public string codcct { get; set; }
        public string codpro { get; set; }
        public string codesp { get; set; }
        public string codofi { get; set; }
        public string codope { get; set; }
        public DateTime fecing { get; set; }
        public decimal tipcvd { get; set; }
        public string prtcli { get; set; }
        public decimal indnomc { get; set; }
        public decimal inddirc { get; set; }
    }
    public class sce_cvd_s06_MS_DTO_06 : sce_cvd_s06_MS_DTO_05
    {
    }
    public class sce_cvd_s06_MS_DTO_09
    {
           public string codcct { get; set; }
           public string codpro { get; set; }
           public string codesp { get; set; }
           public string codofi { get; set; }
           public string codope { get; set; }
           public DateTime fecing { get; set; }
           public int campo6 { get; set; } 
           public string prtexp { get; set; }
           public decimal indexpn { get; set; }
           public decimal indexpd { get; set; }
    }
    public class sce_cvd_s06_MS_DTO_18 : sce_cvd_s06_MS_DTO_09
    {
    }
    public class sce_cvd_s06_MS_DTO_17
    {
        public string codcct { get; set; }
        public string codpro { get; set; }
        public string codesp { get; set; }
        public string codofi { get; set; }
        public string codope { get; set; }
        public DateTime fecing { get; set; }
        public int campo6 { get; set; }
        public string prtexp { get; set; }
        public decimal indnome { get; set; }
        public decimal inddire { get; set; }
    }
    public class sce_cvd_s06_MS_DTO_30
    {
        public string codcct { get; set; }
        public string codpro { get; set; }
        public string codesp { get; set; }
        public string codofi { get; set; }
        public string codope { get; set; }
        public DateTime fecing { get; set; }
        public decimal tipcvd { get; set; }
        public string prtcli { get; set; }
        public decimal  indnomc { get; set; }
        public decimal inddirc { get; set; }
    }

#endregion

}
