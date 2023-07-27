using System;

namespace BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica.T_Modulos
{
    public class Tipo_Contabilidad
    {
        public string Monto;
        public string CodMoneda;
        public string NomMoneda;
        public string NemMoneda;
        public string CodMonBC;
        public string SwfMoneda;
        public string Nemonico;
        public int Ind_Benef;
        public string Nom_Benef;
        public string CtaCte;
        public string Num_Partida;
        public int Tipo_Mov;
        public string Of_Origen;
        public string Nombre_Of;
        public string Swift;
        public int CodBco;
        public string Num_Ref;
        public string Num_Cheque;
        public string Num_Reembolso;
        public string Glosa;
        public int Ind_Cuenta;
        public string Moneda_Arb;
        public int modulo;
        public int Ind_ChqSwf;   //Indice Cheque_Swift.-
        public string num_op;
        public PartyKey Party;
        public int debe;
        public int Borrado;
        public string fecVen;

        public Tipo_Contabilidad()
        {
            Monto= String.Empty;
            CodMoneda= String.Empty;
            NomMoneda= String.Empty;
            NemMoneda= String.Empty;
            CodMonBC= String.Empty;
            SwfMoneda= String.Empty;
            Nemonico= String.Empty;
            
            Nom_Benef= String.Empty;
            CtaCte= String.Empty;
            Num_Partida= String.Empty;
            
            Of_Origen= String.Empty;
            Nombre_Of= String.Empty;
            Swift= String.Empty;
            
            Num_Ref= String.Empty;
            Num_Cheque= String.Empty;
            Num_Reembolso= String.Empty;
            Glosa= String.Empty;
            
            Moneda_Arb= String.Empty;
           
            num_op= String.Empty;
            Party= new PartyKey();
            
            fecVen = String.Empty;
        }
    }

    public class Tip_Cuentas
    {
        public string Nem;
        public int Mon;
        public string num;
        public string Nom;
        public int gl;
        public int NroTO;
        public int IndTO;
        public int CIT;
        public int CVT;
        public int CAP;
        public int CTD;
        public int pos;
        public int CDR;
        public int Vig;   // Vigenteable

        public Tip_Cuentas()
        {
            Nem = String.Empty;
            num = String.Empty;
            Nom = String.Empty;
        }
    }

    public class control_cobro
    {
        public string sistema;
        public string Producto;
        public string etapa;

        public control_cobro()
        {
            sistema = String.Empty;
            Producto = String.Empty;
            etapa = String.Empty;
        }
    }

    public class T_CONTAB01
    {
        public Tipo_Contabilidad[] Est_Contab = new Tipo_Contabilidad[0];
        public Tip_Cuentas Cuenta = new Tip_Cuentas();
        // definición de constantes para los índices de las cuentas que requieren datos
        // adicionales. Guardan correspondencia con los índices de las cuentas dentro del sistema
        // definición de constantes para los índices de los orígenes
        public const int CTACTEMN = 3;     // Cuenta Corriente M/N
        public const int VVBCH = 4;     // Vale Vista Bco. Chile
        public const int VVOB = 5;     // Vale Vista Otro Banco
        public const int CHMNBCH = 6;     // Cheque M/N Bco. Chile
        public const int CHMNOB = 7;     // Cheque M/N Otro Banco
        public const int CTAPTEMN = 8;     // Cuenta Puente M/N
        public const int SCSMN = 9;     // Saldos Con Sucursales M/N
        public const int CTACTEME = 10;     // Cuenta Corriente M/E
        public const int CHMEBCH = 11;     // Cheque M/E Emitido por Bco. Chile
        public const int CHMEONY = 12;     // Cheque M/E cliente of. N.Y.
        public const int CHMEOBC = 13;     // Cheque M/E Otro Banco (Chile)
        public const int CHMEOBE = 14;     // Cheque M/E Otro Banco (Extranjero)
        public const int CTAPTEME = 15;     // Cheque Puente M/E
        public const int SCSME = 16;     // Saldos Con Sucursales M/E
        public const int OPC = 17;     // Orden de Pago Convenio
        public const int OPOP = 18;     // Orden de Pago Otros Países
        public const int VAM = 19;     // Varios Acreedores Importaciones
        public const int VAX = 20;     // Varios Acreedores Exportaciones
        public const int VAMC = 21;     // Varios Acreedores Mcdo. Corredores
        public const int CTACTEBC = 22;     // Cta. Cte. Banco Central
        public const int CTAORD = 23;     // Corresponsal cuenta ordinaria
        public const int DIVENPEN = 24;
        public const int OPEPEND = 25;     // Cuenta Divisas Pendientes.-
        public const string NEMOPEPEND = "OPEPEND";     // Nemónico Cuenta Divisas Pendientes.-
        public const int ONMN = 50;     // Otro Nemónico M/N
        public const int ONME = 60;     // Otro Nemónico M/E
        public string NEMO_PAR = "";     // Nemónico puente arbitraje
        public string PUENTEARB = "";     // glosa puente arbitraje
        public string REEM_CHEQUE = "";     // reemplaza nemónico cheque, cuando corresponsal tiene cuenta cheque
                                                   // constantes para saber cómo llamar al servidor de vías/vueltos
        public const int COMO_VIAS = 0;
        public const int COMO_VUELTOS = 1;
        // definición de constantes para procedencia de los datos contables
        public const int MODULO_COBRO = 1;
        public const int MODULO_ORIGEN = 2;
        public const int MODULO_VIAS = 3;
        public const int MODULO_VUELTO = 4;
        public const int MODULO_ARBITRAJE = 5;
        public control_cobro ctrol_cobro = new control_cobro();
    }
}
