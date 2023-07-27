
namespace BCH.Comex.Core.Entities.Cext01.FundTransfer.Domain
{
    public class EstructUsuarios
    {
        public string rut{ set; get; }  //rut_usuario
        public short Jerarquia{ set; get; }  //nivel de jerarquia
        public string CentroCosto{ set; get; }  //cent_costo_esp
        public string Especialista{ set; get; }  //id_especialista
        public string CCtOrig{ set; get; }  //Centro Costo Original.-
        public string EspOrig{ set; get; }  //Especialista Original.-
        public short Delegada{ set; get; }  //es del mismo nivel que super
        public string CostoSuper{ set; get; }  //jefe superior
        public string Id_Super{ set; get; }  //jefe superior
        public string Nombre{ set; get; }  //nombre_esp
        public string Direccion{ set; get; }  //direcc_esp
        public string comuna{ set; get; }  //comuna especialista
        public string Ciudad{ set; get; }  //ciudad_esp
        public string Seccion{ set; get; }  //seccion_esp
        public short Oficina{ set; get; }  //codigo de su oficina
        public string Telefono{ set; get; }  //Telefono_esp
        public string Swift{ set; get; }  //swift_esp
        public string Fax{ set; get; }  //fax_esp
        public string Tipeje{ set; get; }  //Tipo Ejecutivo Operativo Negocios XOtro
        public string reemplazos{ set; get; }  //Reemplazos.-
        public string RempOrig{ set; get; }  //Reemplazos Usuario Original.-
        public string OfixUser{ set; get; }  //Oficinas que puede atender el usuario
    }
}
