using BCH.Comex.Core.BL.XGSV.Modulos;
using BCH.Comex.Core.Entities.Cext01.Supervisor;
using BCH.Comex.Data.DAL.Cext01;
using System.Collections.Generic;

namespace BCH.Comex.Core.BL.XGSV.Forms
{
    public static class FrmTrasp
    {
        public static void Form_Load(DatosGlobales globales, UnitOfWorkCext01 uow)
        {
            MODUSRS.SyGetv_Usr(string.Empty, uow);
            globales.FrmTrasp.UsuariosActivos = new List<string>();
            globales.FrmTrasp.UsuariosNuevos = new List<string>();

            foreach (var usr in MODUSRS.VUsrs)
            {
                string Paso = "";
                Paso = Paso + usr.CCtUsr.Trim() + "-" + usr.codusr.Trim() + new string(' ', 3);
                Paso = Paso + usr.NomUsr.Trim();
                
                if (usr.CCtUsr == globales.UsrEsp.cent_costo)
                    globales.FrmTrasp.UsuariosActivos.Add(Paso);

                globales.FrmTrasp.UsuariosNuevos.Add(Paso);
            }

            /*
            globales.FrmUsr.X_Mxg = string.Empty;

            if (!MODGUSR1.SyGetn_Usr1(globales.cent_costo, globales, uow))
            {
                return;
            }

            globales.FrmUsr.Supervisor = globales.UsrEsp.cent_costo + "-" + globales.UsrEsp.id_especia + "  :  " + globales.UsrEsp.nombre;

            globales.FrmUsr.Opcion = new Dictionary<string, string>();
            globales.FrmUsr.Opcion.Add("0", "Sus Especialistas");
            globales.FrmUsr.Opcion.Add("1", "Todos los Especialistas");


            if (globales.VUsr[1].FecOut == "")
            {
                globales.FrmUsr.X_Mxg = "El Cierre diario de Comercio Exterior aún no se ha realizado";
                globales.FrmUsr.UnCierre = false;
                globales.FrmUsr.Cierre = true;
            }
            if (globales.VUsr[1].FecOut != "")
            {
                globales.FrmUsr.X_Mxg = "El Cierre diario de Comercio Exterior se realizó con fecha " + globales.VUsr[1].FecOut;
                globales.FrmUsr.UnCierre = true;
            }
            */
        }
    }
}
