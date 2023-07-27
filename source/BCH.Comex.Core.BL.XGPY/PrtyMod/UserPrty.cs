using System;

namespace BCH.Comex.Core.BL.XGPY.PrtyMod
{
    public class UserPrty
    {
        public String rut { get; set; }
        public Int32 jerarquia { get; set; }
        public string centroCosto { get; set; }   //cent_costo_esp
        public string especialista { get; set; }   //id_especialista
        public string centroCostoOrigal { get; set; }   //Centro Costo Original.-
        public string especialistaOriginal { get; set; }   //Especialista Original.-
        public Boolean delegada { get; set; }   //es del mismo nivel que super
        public string costoSuperior { get; set; }   //jefe superior
        public string idSuperior { get; set; }   //jefe superior
        public string nombre { get; set; }   //nombre_esp
        public string direccion { get; set; }   //direcc_esp
        public string comuna { get; set; }   //comuna especialista
        public string ciudad { get; set; }   //ciudad_esp
        public string seccion { get; set; }   //seccion_esp
        public int oficina { get; set; }   //codigo de su oficina
        public string telefono { get; set; }   //Telefono_esp
        public string swift { get; set; }   //swift_esp
        public string fax { get; set; }   //fax_esp
        public string tipoEjecutivo { get; set; }   //Tipo Ejecutivo Operativo Negocios XOtro
        public string reemplazos { get; set; }   //Reemplazos.-
        public string remplazoOrig { get; set; }   //Reemplazos Usuario Original.-
        public string ofixUser { get; set; }   //Oficinas que puede atender el usuario

        private XgpyService m_XgpyService = new XgpyService();

        public void LoadRegUser(String centroCosto, String codigoUsuario)
        {
            var resUser = m_XgpyService.Sce_Usr_S05_MS(centroCosto, codigoUsuario);
            if(resUser != null)
            {
                this.rut = resUser.rut;
                this.jerarquia = (int)resUser.jerarquia;
                this.centroCosto = resUser.cent_costo;
                this.especialista = resUser.id_especia;
                this.delegada = resUser.delegada;
                this.costoSuperior = resUser.cent_super;
                this.idSuperior = resUser.id_super;
                this.nombre = resUser.nombre;
                this.direccion = resUser.direccion;
                this.comuna = resUser.comuna;
                this.ciudad = resUser.ciudad;
                this.seccion = resUser.seccion;
                this.oficina = (int)resUser.ofic_orige;
                this.telefono = resUser.telefono;
                this.swift = resUser.swift;
                this.fax = resUser.fax;
                this.tipoEjecutivo = resUser.tipeje;

                var resReemplasos = m_XgpyService.Sce_Usr_S06_MS(centroCosto, this.especialista);
                if(resReemplasos != null)
                {
                    this.reemplazos = "";
                    for(int i = 0; i < resReemplasos.Count; i++)
                    {
                        this.reemplazos = this.reemplazos + resReemplasos[i] + ";";
                    }
                }
            }
            resUser = null;
        }

        public Int32 Acceso()
        {
            if(this.jerarquia == 1 || this.tipoEjecutivo == "N")
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }
}
