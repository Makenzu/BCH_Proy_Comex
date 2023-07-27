using System;

namespace BCH.Comex.Core.Entities.Swift
{
    public class ConfiguracionAlerta
    {
        public enum AplicacionEmiteAlertas: byte
        {
            EnvioSwift,
            AutorizacionSwift,
            AdminEnvioSwift,
            RecepcionSwift
        }


        public short? MinsIntervaloConsultar { get; set; }
        public AplicacionEmiteAlertas Aplicacion { get; set; }
        public DateTime? UltimaVezLeidas { get; set; }
    }
}
