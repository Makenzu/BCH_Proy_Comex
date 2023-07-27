using System;

namespace BCH.Comex.Core.BL.XGPL
{
    public class FrmgPlv
    {
        private const string PL_VIS_EXP = "Planillas Visibles de Exportación";
        private const string PL_INV_EXP = "Planillas Invisibles";
        private const string PL_ANU_EXP = "Planillas Visibles Anuladas de Exportación";
        private const string PL_VIS_END = "Planillas Visibles de Importaciones Endosadas";
        private const string PL_VIS_IMP = "Planillas Visibles de Importación";
        public enum TipoListado { tplVisibleExportacion, tplInvisibleExportacion, tplAnuladaExportacion, tplVisibleImportacionEndosadas, tplVisibleImportacion }

        public static string TipoListadoString(TipoListado tipo)
        {
            switch (tipo)
            {
                case TipoListado.tplVisibleExportacion:
                    return PL_VIS_EXP;
                case TipoListado.tplInvisibleExportacion:
                    return PL_INV_EXP;
                case TipoListado.tplAnuladaExportacion:
                    return PL_ANU_EXP;
                case TipoListado.tplVisibleImportacionEndosadas:
                    return PL_VIS_END;
                case TipoListado.tplVisibleImportacion:
                    return PL_VIS_IMP;
                default:
                    return "(desconocido)";
            }
        }

        public static string FormatoOrden(string OrdenSinFormato)
        {
            var str = OrdenSinFormato;
            if (str.Length != 15)
            {
                return str;
            }
            // MODXPLV2.VPlv[i].codope.Mid(1, 3) + "-" + MODXPLV2.VPlv[i].codope.Mid(4, 2) + "-" + MODXPLV2.VPlv[i].codope.Mid(6, 2) + "-" + MODXPLV2.VPlv[i].codope.Mid(8, 3) + "-" + MODXPLV2.VPlv[i].codope.Mid(11, 5) + 9.Char();
            return str.Mid(1, 3) + "-" + str.Mid(4, 2) + "-" + str.Mid(6, 2) + "-" + str.Mid(8, 3) + "-" + str.Mid(11, 5); // no necesito tab
        }

        public static string NombreModelo(TipoListado Tipo, string NombreUsuario, DateTime? FechaIngreso)
        {
            string strFecha = Tipo != TipoListado.tplVisibleImportacionEndosadas && FechaIngreso.HasValue
                ? string.Format(" ingresadas el {0}", FechaIngreso.Value.ToString("dd/MM/yyyy"))
                : "";
            return TipoListadoString(Tipo)
                    + strFecha
                    + (string.IsNullOrEmpty(NombreUsuario) ? " (sin información de usuario)" : string.Format(" - Especialista: {0}", NombreUsuario));
        }
    }
}
