using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica.T_Modulos
{

    public class T_Module1
    {
        public T_Module1()
        {
            Codop = new CdOper();
            Codop_FT = new CdOper();
            Codop_CVD = new CdOper();
            PartysOpe = new PartyKey[0];
            PrtControl = new PartyParametros();
            Partys = new PartyKey[0];
            PopeOpe = new PartysPope[0];
            Pope = new PartysPope[0];
            PrtTbl = new string[0];
        }

        public CdOper Codop_FT;
        public CdOper Codop_CVD;
        public CdOper Codop;
        public PartyKey[] PartysOpe;
        public PartyParametros PrtControl;
        public PartyKey[] Partys;
        public PartysPope[] PopeOpe;
        public PartysPope[] Pope;
        public string[] PrtTbl;
        public string KeyPrt;

        //Declaracion de Constantes party
        public const string GPrt_GetParty = "Captura de Participantes";
        public const string GPrt_NoPath = "Error del path a Bases de Datos";
        //Declaracion de Valores de Retorno de GetParty
        public const short GPrt_RetExiste = 0;
        public const short GPrt_RetCancelo = 1;
        //Declaracion de ubicaciones
        public const short GPrt_EnParty = 0;
        public const short GPrt_EnOperacion = 1;

        //declaraciones de status normales
        public const short GPrt_StatVacio = 0;
        public const short GPrt_StatLleno = 1;
        public const short GPrt_StatDatos = 2;
        public const short GPrt_StatDatosLleno = 3;

        //declaracion status Pope
        public const short GPrt_StatNuevo = -1;
        public const short GPrt_StatCambio = -2;
        public const short GPrt_StatBorro = -3;
        public const short GPrt_StatIntacto = -4;
        public const short GPrt_StatVacion = -5;

        public const short KEY_PRIOR = 0x21;
        public const short KEY_NEXT = 0x22;
        public const short KEY_UP = 0x26;
        public const short KEY_DOWN = 0x28;

        public const short GPrt_TipoBanco = 1;
        public const short GPrt_TipoEnOperacion = 3;
        public const short GPrt_TipoBcoOperacion = 4;

        //tipos de Flag
        public const short GPrt_FlagInst = 1;

        //marcas de lista para operaciones especiales
        public const string GPrt_MarcaRequerido = "&";
        public const string GPrt_MarcaOtros = "#";
        public const string GPrt_MarcaBanco = "@";

        public const string GPrt_TxtOtros = "(Otros)";
        public const string GPrt_InputCambio = "La descripción para el participante es requerida";
        public const string GPrt_Caption = "Identificar Participantes";
        public const string GPrt_ErrGetDbf = "Se ha producido un error en el acceso a las bases de datos de Participantes.";
        public const string GPrt_ErrRequerido = ", esta información es requerida para la operación.";
        public const string GPrt_ErrRut = "Rut Invalido";
        public const string GPrt_NoPuedeBanco = "La operación requiere que este participante sea un banco, por lo tanto este participante no puede estar asociado a la operación.";
        public const string GPrt_ErrEliminar = "El participante posee datos asociados en la operacion, para eliminarlo deberá primero liberar dicha información.";
        public const string GPrt_ErrModificar = "El participante tiene asociada información que impide su modificación, para modificarlo deberá primero liberar dicha información";
    }

}
