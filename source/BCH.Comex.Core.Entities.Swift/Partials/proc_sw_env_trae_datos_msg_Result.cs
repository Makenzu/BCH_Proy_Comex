
namespace BCH.Comex.Core.Entities.Swift
{
    public partial class proc_sw_env_trae_datos_msg_MS_Result
    {
        public string TipoIngresoDesc
        {
            get
            {
                if (this.tipo_ingreso == "A"){
                    return "Automático";
                }
                else if(this.tipo_ingreso == "M"){
                    return "Manual";
                }
                else return this.tipo_ingreso;
            }
        }
    }
}
