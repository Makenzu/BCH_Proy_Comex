using System;
using System.Collections.Generic;
using System.Linq;

namespace BCH.Comex.Core.Entities.Portal
{
    public partial class DatosUsuario : IDatosUsuario
    {
        public PrintFormat ConfigImpres_PrintFormat
        {
            get {
                PrintFormat value;
                if (!Enum.TryParse<PrintFormat>(this.ConfigImpres_Formato, out value))
                    value = PrintFormat.TIFF;

                return value;
            }
            set {
                this.ConfigImpres_Formato = value.ToString();
            }
        }

        public string Identificacion_CentroDeCostosImpersonado
        {
            get
            {
                if (this.Identificacion_CCtUsr.Length == 5)
                {
                    return this.Identificacion_CCtUsr.Substring(0, 3);
                }
                else return String.Empty;
            }
        }

        public string Identificacion_IdEspecialistaImpersonado
        {
            get
            {
                if (this.Identificacion_CCtUsr.Length == 5)
                {
                    return this.Identificacion_CCtUsr.Substring(3, 2);
                }
                else return String.Empty;
            }
        }

        public string Identificacion_CentroDeCostosOriginal
        {
            get
            {
                if (this.Identificacion_CCtUsro.Length == 5)
                {
                    return this.Identificacion_CCtUsro.Substring(0, 3);
                }
                else return String.Empty;
            }
        }

        public string Identificacion_IdEspecialistaOriginal
        {
            get
            {
                if (this.Identificacion_CCtUsro.Length == 5)
                {
                    return this.Identificacion_CCtUsro.Substring(3, 2);
                }
                else return String.Empty;
            }
        }

        public int RutEnFormatoBDSwift
        {
            get
            {
                if (!String.IsNullOrEmpty(this.Identificacion_Rut))
                {
                    if (this.Identificacion_Rut.Length > 8)
                    {
                        return int.Parse(this.Identificacion_Rut.Substring(0, this.Identificacion_Rut.Length - 1)); //dejo afuera el digito verificador
                    }
                    else
                    {
                        return int.Parse(this.Identificacion_Rut);
                    }
                }
                else return 0;
            }
        }

        public string CodBCCH
        {
            get
            {
                var aux = this.tbl_datos_usuario_codigos_sucursal.Where(c => c.CentroCosto == this.Identificacion_CentroDeCostosImpersonado).SingleOrDefault();
                if (aux == null)
                    return "";
                else
                    return aux.CodBCCH;

            }
        }

        public string CodPBC
        {
            get
            {
                var aux = this.tbl_datos_usuario_codigos_sucursal.Where(c => c.CentroCosto == this.Identificacion_CentroDeCostosImpersonado).SingleOrDefault();
                if (aux == null)
                    return "";
                else
                    return aux.CodPBC;
            }
        }

        public string CodBCH
        {
            get
            {
                var aux = this.tbl_datos_usuario_codigos_sucursal.Where(c => c.CentroCosto == this.Identificacion_CentroDeCostosImpersonado).SingleOrDefault();
                if (aux == null)
                    return "";
                else
                    return aux.CodBCH;
            }
        }

        public string SucBCH
        {
            get
            {
                var aux = this.tbl_datos_usuario_codigos_sucursal.Where(c => c.CentroCosto == this.Identificacion_CentroDeCostosImpersonado).SingleOrDefault();
                if (aux == null)
                    return "";
                else
                    return aux.SucBCH;
            }
        }

        public IEnumerable<ICodigosSucursal> codigos_sucursal
        {
            get
            {
                return this.tbl_datos_usuario_codigos_sucursal;
            }
        }
    }
}
