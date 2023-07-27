﻿using System.Collections.Generic;

namespace BCH.Comex.Core.Entities.Cext01.FinDia
{
    public class T_MODGUSR
    {
        public EstrucUsuarios UsrEsp;
        public List<EstrucUsuarios> UsrLidEsp = null;

        public T_MODGUSR()
        {
            UsrEsp = new EstrucUsuarios();
            UsrLidEsp = new List<EstrucUsuarios>();
        }
    }

    public class EstrucUsuarios
    {
        public string Rut;   //rut_usuario
        public int Jerarquia;   //nivel de jerarquia
        public string CentroCosto;   //cent_costo_esp
        public string Especialista;   //id_especialista
        public string CCtOrig;   //Centro Costo Original.-
        public string EspOrig;   //Especialista Original.-
        public int Delegada;   //es del mismo nivel que super
        public string CostoSuper;   //jefe superior
        public string Id_Super;   //jefe superior
        public string nombre;   //nombre_esp
        public string Direccion;   //direcc_esp
        public string comuna;   //comuna especialista
        public string Ciudad;   //ciudad_esp
        public string Seccion;   //seccion_esp
        public int Oficina;   //codigo de su oficina
        public string Telefono;   //Telefono_esp
        public string swift;   //swift_esp
        public string Fax;   //fax_esp
        public string Tipeje;   //Tipo Ejecutivo Operativo Negocios XOtro
        public string reemplazos;   //Reemplazos.-
        public string RempOrig;   //Reemplazos Usuario Original.-
        public string OfixUser;   //Oficinas que puede atender el usuario
    }
}
