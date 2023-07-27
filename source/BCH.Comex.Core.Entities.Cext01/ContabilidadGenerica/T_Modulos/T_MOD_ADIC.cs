using System;

namespace BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica.T_Modulos
{
    public class tipo_partidas
    {
        public int Borrado;
        public int envio;
        public int debe;
        public string Monto;
        public string CodMoneda;
        public string NomMoneda;
        public string NemMoneda;
        public string CodMonBC;
        public string SwfMoneda;
        public string Nemonico;
        public string Glosa;
        public int Ind_Benef;
        public string Nom_Benef;
        public PartyKey Party;
        public int Ind_Cuenta;
        public string CtaCte;
        public string NroRef;   // Numero de Referencia
        public string NumPar;

        public tipo_partidas()
        {
            
            Monto= String.Empty;
            CodMoneda= String.Empty;
            NomMoneda= String.Empty;
            NemMoneda= String.Empty;
            CodMonBC= String.Empty;
            SwfMoneda= String.Empty;
            Nemonico= String.Empty;
            Glosa= String.Empty;
            Nom_Benef= String.Empty;
            Party = new PartyKey();
            CtaCte= String.Empty;
            NroRef= String.Empty;   // Numero de Referencia
            NumPar= String.Empty;
        }
    }

    public class tipo_numeros
    {
        public int Borrado;
        public int Indice;
        public string num_op;
        public int origen;
        public int via;
        public int Vuelto;
        public int listo;

        public tipo_numeros()
        {
            num_op = String.Empty;
        }
    }

    public class Tipo_DebCre
    {
        public string Mto;   //Monto del Débito/Crédito.-
        public string DH;   //Monto del Débito/Crédito.-
        public string Cta;   //Monto del Débito/Crédito.-
        public string Mon;   //Monto del Débito/Crédito.-
        public string Ori;   //Origen del Débito/Crédito.-
        public PartyKey Prt;   //Participante al cual se le debitó o acreditó
        public string Con;   //Concepto de débito/crédito

        public Tipo_DebCre()
        {
            Mto= String.Empty;   //Monto del Débito/Crédito.-
            DH= String.Empty;   //Monto del Débito/Crédito.-
            Cta= String.Empty;   //Monto del Débito/Crédito.-
            Mon= String.Empty;   //Monto del Débito/Crédito.-
            Ori= String.Empty;   //Origen del Débito/Crédito.-
            Prt = new PartyKey();   //Participante al cual se le debitó o acreditó
            Con= String.Empty;   //Concepto de débito/crédito
        }
    }


    public class T_MOD_ADIC
    {
        public tipo_partidas[] partidas = new tipo_partidas[0];
        public tipo_numeros[] Numeros = new tipo_numeros[0];
        public int ind_partida = 0;
        public Tipo_DebCre[] DebCre = new Tipo_DebCre[0];     // Arreglo de Débito/Crédito.-
    }
}
