using System;

namespace BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica.T_Modulos
{
    public class T_MT_R
    {
        public int codmt;   //Código MT
        public string Titulo = String.Empty;   //Título MT
        public string ValAnt = String.Empty;   //Valor anterior de MT
        public string ValAct = String.Empty;   //Valor actual de MT
    }
    public class T_MT_H
    {
        public string Sender = String.Empty;
        public string Type = String.Empty;
        public string Reciver = String.Empty;
        public string DateIssue = String.Empty;
    }
    public class T_MT_D
    {
        public string campo = String.Empty;
        public string Titulo = String.Empty;
        public string Valor = String.Empty;
        public int Manual;
    }
    public class T_MT_G
    {
        public string CamMan = String.Empty;
    }
    public class T_MODMMT
    {
        public T_MT_R[] VMT_R = new T_MT_R[0];
        // Defino arreglo donde se gurda linia por line el swift
        public string[] VMT_L = new string[0];
        public T_MT_H VMT_H = new T_MT_H();
        public T_MT_D[] VMT_D = new T_MT_D[0];
        public T_MT_D[] VMT_DR = new T_MT_D[0];
        public T_MT_G VMT_G = new T_MT_G();
        public const string MsgMmt = "Modificación de MT";
        public string recive = "";
    }
}
