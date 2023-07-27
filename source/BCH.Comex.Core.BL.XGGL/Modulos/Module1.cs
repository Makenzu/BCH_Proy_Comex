using BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica.T_Modulos;
using CodeArchitects.VB6Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCH.Comex.Core.BL.XGGL.Modulos
{
    public class Module1
    {
        public static T_Module1 Getmodule1()
        {
            T_Module1 Module1 = new T_Module1();
            return Module1;
        }


        public static short ResetParty(T_Module1 Module1, string[] Arreglo)
        {
            if (Module1.PrtControl.Leidos == 0)
            {
                bool posRes = LeeParametrosParty(Module1);
                if (posRes)
                {
                    return 0;
                }
            }
            Module1.PrtControl.Leidos = (short)(true ? -1 : 0);
            Module1.PrtControl.Modifico = (short)(false ? -1 : 0);
            Module1.PrtControl.LimInf = (short)VB6Helpers.LBound(Arreglo);
            Module1.PrtControl.LimSup = (short)VB6Helpers.UBound(Arreglo);

            Module1.PartysOpe = new PartyKey[Module1.PrtControl.LimSup + 1];
            for (int i = 0; i <= Module1.PrtControl.LimSup; i++)
            {
                if (Module1.PartysOpe[i] == null)
                {
                    Module1.PartysOpe[i] = new PartyKey();
                }
            }


            //Module1.PrtTbl = new string[Module1.PrtControl.LimSup + 1];
            //inicio el array con valor "" 
            Module1.PrtTbl = Enumerable.Repeat(string.Empty, Module1.PrtControl.LimSup + 1).ToArray();

            Module1.PrtControl.Insertado = 0;
            Module1.PrtControl.Otros = 0;

            for (int i = Module1.PrtControl.LimInf; i <= Module1.PrtControl.LimSup; i++)
            {
                if (String.IsNullOrEmpty(Arreglo[i]))
                {
                    Module1.PrtControl.Otros += 1;
                }
            }

            short Lim = (short)VB6Helpers.UBound(Module1.PopeOpe);
            if (Lim < 0)
            {
                return 0;
            }

            Module1.PopeOpe = new PartysPope[1] { new PartysPope() };

            return 0;
        }

        public static bool LeeParametrosParty(T_Module1 Module1)
        {
            string path = MODGPYF0.GetUbicacion("PathPartys");
            if (path.Length == 0)
            {
                Module1.PrtControl.Leidos = 0;
                return true;
            }
            Module1.PrtControl.PartyPath = path;

            string dato = Mdl_Acceso.GetConfigValue("ContabilidadGenerica.Participantes.PartyEnRed");
            Module1.PrtControl.Red = (short)(false ? -1 : 0);
            if (dato.Length > 0 && int.Parse(dato) > 0)
            {
                Module1.PrtControl.Red = (short)(true ? -1 : 0);
            }

            dato = Mdl_Acceso.GetConfigValue("ContabilidadGenerica.Participantes.PartyNodo");
            Module1.PrtControl.Nodo = "";
            if (dato.Length > 0)
            {
                Module1.PrtControl.Nodo = dato;
            }

            dato = Mdl_Acceso.GetConfigValue("ContabilidadGenerica.Participantes.PartyServidor");
            Module1.PrtControl.Servidor = "";
            if (dato.Length > 0)
            {
                Module1.PrtControl.Servidor = dato;
            }

            if (String.IsNullOrEmpty(Module1.PrtControl.Nodo) || String.IsNullOrEmpty(Module1.PrtControl.Servidor))
            {
                Module1.PrtControl.Red = -1;
            }
            Module1.PrtControl.Leidos = -1;

            return false;
        }



    }
}
