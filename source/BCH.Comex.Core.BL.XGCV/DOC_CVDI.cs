using System;
using System.Windows.Forms;

namespace BCH.Comex.Core.BL.XGCV
{
   public static class DOC_CVDI
   {
      // ============================================================================
      // VARIABLES Y CODIGO EN EL SERVIDOR DE IMPRESION.-
      // ============================================================================
      // Datos Generales para Carta de Compra Venta.-
      // ----------------------------------------------------------------------------
      public struct STipo_DocCV
      {
         public string NroRef;   //Número de la Cobranza.-    NomPrt      As String       'Nombre del Importador.-
         public string Oprel;   //número de operación relacionada
         public string Nomprt;
         public string DirPrt;   //Dirección del Importador.-
         public string CiuPrt;   //Ciudad del Importador.-
         public string EstPrt;   //Estado del Importador.-
         public string PosPrt;   //Postal del Importador.-
         public string NomEsp;   //Nombre del Especialista.-
         public string DirEsp;   //Direccion        "
         public string TelEsp;   //Telefono         "
         public string FaxEsp;   //Fax              "
         public int tipocarta;   //1:Compra; 2:Venta; 3:Ambos.-
         public string MonSeg;   //Moneda del Seguro.-
         public string MtoSeg;   //Monto del Seguro.-
         public string MonFle;   //Moneda del Flete.-
         public string MtoFle;   //Monto del Flete.-
         public string MonGas;   //Moneda del Gasto del Cedente.-
         public string MtoGas;   //Monto del Gasto del Cedente.-
         public string Totpes;   //Total en Pesos, en Detalle.-
         public string Concepto;
      }
      // ----------------------------------------------------------------------------
      // Montos, Monedas y Tipo de Cambio de las compras y/o ventas
      // ----------------------------------------------------------------------------
      public struct Scompra_venta
      {
         public string tipo;   //1 si es compra, 2 si es venta
         public string Moneda;   //Moneda del Principal.-
         public double Monto;   //Monto de lo Cubierto.-
         public double Cambio;   //Tipo de Cambio.-
         public double total_peso;   //total en pesos de la compra o venta
         public int NumPla;   //numero planilla
         public string NumDec;   //numero declaracion
      }
      // ----------------------------------------------------------------------------
      //  Monedas y montos de los arbitrajes
      // ----------------------------------------------------------------------------
      public struct sarbitraje
      {
         public string moneda_1;
         public string monto_1;
         public string moneda_2;
         public string monto_2;
         public string Paridad;
      }
      // ----------------------------------------------------------------------------
      // Beneficiarios del Pago.-
      // ----------------------------------------------------------------------------
      public struct STipo_BenCV
      {
         public string Benef;   //Nombre del Beneficiario.-
         public string Via;   //Vía Ej. Cta. Cte. + #Cta.-
         public string Moneda;
         public string Monto;
      }
      // ----------------------------------------------------------------------------
      // Débitos por efecto de Pago.-
      // ----------------------------------------------------------------------------
      public struct STipo_DebCV
      {
         public string deb_hab;
         public string Debito;   //Tipo de Débito, Ej. Cuenta Contable.-
         public string Moneda;
         public string Monto;
      }
      // ----------------------------------------------------------------------------
      // Detalle de Impuestos en $.-
      // ----------------------------------------------------------------------------
      public struct STipo_DetCV
      {
         public string Detalle;   //Glosa de Impuestos + Varios ,en $.-
         public string Moneda;
         public string Monto;
      }
      // ----------------------------------------------------------------------------
      public static STipo_DocCV SDoccv = new STipo_DocCV();
      public static Scompra_venta[] smtocv = null;     // Montos Pagados.-
      public static Scompra_venta[] smtotr = null;     // Montos Trasfer.-
      public static STipo_BenCV[] sbencv = null;     // Beneficiarios del Pago.-
      public static STipo_DebCV[] Sdebcv = null;     // Débitos por Pago.-
      public static STipo_DetCV[] sdetcv = null;     // Detalle de Impuestos.-
      public static sarbitraje[] smonarb = null;
      public static string Ciudad = "";
      public static int chequea_linea(ref int Lineas)
      {
         int chequea_linea = 0;


         Lineas = Lineas + 1;
         if (Lineas > 48)
         {
            chequea_linea = 5;
         }
         else
         {
            chequea_linea = Lineas;
         }

         return chequea_linea;
      }
      public static int HayOperacion(string tipo)
      {
         int HayOperacion = 0;

         int i = 0;

         for(i = 0; i <= smtocv.GetUpperBound(0); i += 1)
         {
            if (smtocv[i].tipo == tipo)
            {
               HayOperacion = true.ToInt();
               return HayOperacion;
            }
         }
         return false.ToInt();
      }
      // Header para Carta de Compra Venta.-
      public static void Header06()
      {
         string Hoy = "";

         XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeItalic(XGCV.Printer.DefInstance.Font, false);
         XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(XGCV.Printer.DefInstance.Font, false);
         XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeUnderline(XGCV.Printer.DefInstance.Font, false);

         XGCV.Printer.DefInstance.Print( );
         XGCV.Printer.DefInstance.Print( );
         XGCV.Printer.DefInstance.Print( );
         XGCV.Printer.DefInstance.Print( );

         XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeSize(XGCV.Printer.DefInstance.Font, 12);
         XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(XGCV.Printer.DefInstance.Font, true);
         if (MODXDOC.VOpe.NumOpe.Mid(1, 3) == "753")
         {
            XGCV.Printer.DefInstance.PrintWithoutCrlf("Servicios M/E");
         }
         else
         {
            XGCV.Printer.DefInstance.PrintWithoutCrlf("Comercio Exterior");
         }
         XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeSize(XGCV.Printer.DefInstance.Font, 10);
         XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(XGCV.Printer.DefInstance.Font, false);
         Hoy = MigrationSupport.Utils.Format(DateTime.Now,"dd/mm/yyyy");
         XGCV.Printer.DefInstance.PrintList(new object[]{XGCV.Printer.TAB(70),Ciudad + ", " + MODXDOC.Glosa_Fecha_Hoy_Espanol(Hoy)});

         // 
         // Referencia.-
         XGCV.Printer.DefInstance.PrintWithoutCrlf(XGCV.Printer.TAB(70),"Compra-Venta  Divisas Nº :",XGCV.Printer.TAB(99));
         XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(XGCV.Printer.DefInstance.Font, true);
         XGCV.Printer.DefInstance.Print(MODXDOC.raya_Cobranza(MODXDOC.VOpe.NumOpe));
         XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(XGCV.Printer.DefInstance.Font, false);
         if (SDoccv.Oprel != "")
         {
            XGCV.Printer.DefInstance.PrintWithoutCrlf(XGCV.Printer.TAB(70),"Operación Relacionada Nº :",XGCV.Printer.TAB(99));
            XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(XGCV.Printer.DefInstance.Font, true);
            XGCV.Printer.DefInstance.Print(MODXDOC.raya_Cobranza(SDoccv.Oprel));
            XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(XGCV.Printer.DefInstance.Font, false);
         }

         XGCV.Printer.DefInstance.Print( );
         XGCV.Printer.DefInstance.Print( );
         MODXDOC.linea = 8;

      }
      // Recibe los datos de la Carta de Compra Venta.-
      public static void Pr_Load_Doc402(string Dato)
      {
         double Sum = 0.0;
         int eltop = 0;
         int i = 0;

         smonarb = new sarbitraje[1];
         smtocv = new Scompra_venta[1];
         sbencv = null;
         Sdebcv = null;
         sdetcv = null;

         MODXDOC.VOpe.NumOpe = MODGDOC.CopiarDeString(Dato,9.Char(),MODGSYB.NumIni()).TrimB();
         SDoccv.Oprel = MODGDOC.CopiarDeString(Dato,9.Char(),MODGSYB.NumSig()).TrimB();
         // ------------------------------------------------------------------------
         // Datos del Cliente.-
         // ------------------------------------------------------------------------
         MODXDOC.PartysOpe = new MODXDOC.PartyKey[2];
         MODXDOC.PartysOpe[1].NombreUsado = MODGDOC.CopiarDeString(Dato,9.Char(),MODGSYB.NumSig()).TrimB();
         MODXDOC.PartysOpe[1].DireccionUsado = MODGDOC.CopiarDeString(Dato,9.Char(),MODGSYB.NumSig()).TrimB();
         MODXDOC.PartysOpe[1].CiudadUsado = MODGDOC.CopiarDeString(Dato,9.Char(),MODGSYB.NumSig()).TrimB();
         MODXDOC.PartysOpe[1].EstadoUsado = MODGDOC.CopiarDeString(Dato,9.Char(),MODGSYB.NumSig()).TrimB();
         MODXDOC.PartysOpe[1].PostalUsado = MODGDOC.CopiarDeString(Dato,9.Char(),MODGSYB.NumSig()).TrimB();
         MODXDOC.PartysOpe[1].Fax = MODGDOC.CopiarDeString(Dato,9.Char(),MODGSYB.NumSig()).TrimB();
         MODXDOC.PartysOpe[1].CasBanco = MODGDOC.CopiarDeString(Dato,9.Char(),MODGSYB.NumSig()).TrimB();
         MODXDOC.PartysOpe[1].CasPostal = MODGDOC.CopiarDeString(Dato,9.Char(),MODGSYB.NumSig()).TrimB();
         MODXDOC.PartysOpe[1].Enviara = MODGDOC.CopiarDeString(Dato,9.Char(),MODGSYB.NumSig()).ToInt();
         // ------------------------------------------------------------------------
         // Compra, Venta o Ambos.-
         // ------------------------------------------------------------------------
         SDoccv.tipocarta = MODGDOC.CopiarDeString(Dato,9.Char(),MODGSYB.NumSig()).ToInt();
         // ------------------------------------------------------------------------
         // Otros Pagos de : Seguro, Flete, Gastos Cedente.-
         // ------------------------------------------------------------------------
         SDoccv.NomEsp = MODGDOC.CopiarDeString(Dato,9.Char(),MODGSYB.NumSig()).TrimB();
         SDoccv.DirEsp = MODGDOC.CopiarDeString(Dato,9.Char(),MODGSYB.NumSig()).TrimB();
         SDoccv.TelEsp = MODGDOC.CopiarDeString(Dato,9.Char(),MODGSYB.NumSig()).TrimB();
         SDoccv.FaxEsp = MODGDOC.CopiarDeString(Dato,9.Char(),MODGSYB.NumSig()).TrimB();

         // ------------------------------------------------------------------------
         // Concepto de la CVD.-
         // ------------------------------------------------------------------------
         SDoccv.Concepto = MODGDOC.CopiarDeString(Dato,9.Char(),MODGSYB.NumSig()).TrimB();
         // ------------------------------------------------------------------------
         // Compras y ventas realizadas.-
         // ------------------------------------------------------------------------
         i = MODGDOC.CopiarDeString(Dato,9.Char(),MODGSYB.NumSig()).ToInt();

         smtocv = new Scompra_venta[i + 1];

         for(i = 1; i <= smtocv.GetUpperBound(0); i += 1)
         {
            smtocv[i].tipo = MODGDOC.CopiarDeString(Dato,9.Char(),MODGSYB.NumSig()).TrimB();
            smtocv[i].Moneda = MODGDOC.CopiarDeString(Dato,9.Char(),MODGSYB.NumSig()).TrimB();
            smtocv[i].Monto = MODGDOC.CopiarDeString(Dato,9.Char(),MODGSYB.NumSig()).ToVal();
            smtocv[i].Cambio = MODGDOC.CopiarDeString(Dato,9.Char(),MODGSYB.NumSig()).ToVal();
            smtocv[i].total_peso = MODGDOC.CopiarDeString(Dato,9.Char(),MODGSYB.NumSig()).ToVal();
            smtocv[i].NumPla = MODGDOC.CopiarDeString(Dato,9.Char(),MODGSYB.NumSig()).ToInt();
            smtocv[i].NumDec = MODGDOC.CopiarDeString(Dato,9.Char(),MODGSYB.NumSig()).TrimB();
         }

         // ------------------------------------------------------------------------
         // Transferencias realizadas.-
         // ------------------------------------------------------------------------
         i = MODGDOC.CopiarDeString(Dato,9.Char(),MODGSYB.NumSig()).ToInt();

         smtotr = new Scompra_venta[i + 1];

         for(i = 1; i <= smtotr.GetUpperBound(0); i += 1)
         {
            smtotr[i].Moneda = MODGDOC.CopiarDeString(Dato,9.Char(),MODGSYB.NumSig()).TrimB();
            smtotr[i].Monto = MODGDOC.CopiarDeString(Dato,9.Char(),MODGSYB.NumSig()).ToVal();
            smtotr[i].NumPla = MODGDOC.CopiarDeString(Dato,9.Char(),MODGSYB.NumSig()).ToInt();
            smtotr[i].NumDec = MODGDOC.CopiarDeString(Dato,9.Char(),MODGSYB.NumSig()).TrimB();
         }

         // ------------------------------------------------------------------------
         // Número de Vias.-
         // ------------------------------------------------------------------------
         i = MODGDOC.CopiarDeString(Dato,9.Char(),MODGSYB.NumSig()).ToInt();
         if (i != -1)
         {
            sbencv = new STipo_BenCV[i + 1];
            for(i = 1; i <= sbencv.GetUpperBound(0); i += 1)
            {
               sbencv[i].Benef = MODGDOC.CopiarDeString(Dato,9.Char(),MODGSYB.NumSig()).TrimB();
               sbencv[i].Via = MODGDOC.CopiarDeString(Dato,9.Char(),MODGSYB.NumSig()).TrimB();
               sbencv[i].Moneda = MODGDOC.CopiarDeString(Dato,9.Char(),MODGSYB.NumSig()).TrimB();
               sbencv[i].Monto = MODGDOC.forma(MODGDOC.CopiarDeString(Dato,9.Char(),MODGSYB.NumSig()).TrimB(),MODXDOC.Formato);
            }
         }
         // ------------------------------------------------------------------------
         // Número de Débitos.-
         // ------------------------------------------------------------------------
         i = MODGDOC.CopiarDeString(Dato,9.Char(),MODGSYB.NumSig()).ToInt();
         if (i > 0)
         {
            Sdebcv = new STipo_DebCV[i + 1];
            for(i = 1; i <= Sdebcv.GetUpperBound(0); i += 1)
            {
               Sdebcv[i].deb_hab = MODGDOC.CopiarDeString(Dato,9.Char(),MODGSYB.NumSig()).TrimB();
               Sdebcv[i].Debito = MODGDOC.CopiarDeString(Dato,9.Char(),MODGSYB.NumSig()).TrimB();
               Sdebcv[i].Moneda = MODGDOC.CopiarDeString(Dato,9.Char(),MODGSYB.NumSig()).TrimB();
               Sdebcv[i].Monto = MODGDOC.forma(MODGDOC.CopiarDeString(Dato,9.Char(),MODGSYB.NumSig()).TrimB(),MODXDOC.Formato);
            }
         }
         // ------------------------------------------------------------------------
         // Número de Detalles.-
         // ------------------------------------------------------------------------
         i = (MODGDOC.CopiarDeString(Dato,9.Char(),MODGSYB.NumSig()).ToVal() - 1).ToInt();
         if (i > 0)
         {
            sdetcv = new STipo_DetCV[i + 1];
            for(i = 0; i <= sdetcv.GetUpperBound(0); i += 1)
            {
               sdetcv[i].Detalle = MODGDOC.CopiarDeString(Dato,9.Char(),MODGSYB.NumSig()).TrimB();
               sdetcv[i].Moneda = MODGDOC.CopiarDeString(Dato,9.Char(),MODGSYB.NumSig()).TrimB();
               sdetcv[i].Monto = MODGDOC.forma(MODGDOC.CopiarDeString(Dato,9.Char(),MODGSYB.NumSig()).TrimB(),MODXDOC.Formato);
            }
         }
         // ------------------------------------------------------------------------
         // Se calcula por mientras la suma del detalle.-
         // ------------------------------------------------------------------------
         eltop = -1;

            eltop = sdetcv.GetUpperBound(0);
            if (eltop > -1)
            {
               Sum = 0;
               for(i = 0; i <= sdetcv.GetUpperBound(0); i += 1)
               {
                  Sum = Sum + MigrationSupport.Utils.Format(sdetcv[i].Monto,String.Empty).ToDbl();
               }
               SDoccv.Totpes = MODGDOC.forma((Sum.ToInt()).Str().TrimB(),MODXDOC.Formato);
            }
      }
      // Carta de Compra Venta.-
      public static void Pr_Principal_13()
      {
         int intCaseArg = 0;
         double TotDetalle = 0.0;
         bool EsOrigenPesos = false;
         int eltop = 0;
         int i = 0;
         int sw = 0;
         string s = "";
         int A_CPo = 0;
         int A_CBa = 0;
         int A_Fax = 0;
         int A_Dir = 0;
         string s2 = "";
         int NLinea = 0;
         NLinea = 5;
         // ----------------------------------------------------------
         // Titulo para Señores.-
         // ----------------------------------------------------------
         XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(XGCV.Printer.DefInstance.Font, true);
         XGCV.Printer.DefInstance.Print("Señores:");
         XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(XGCV.Printer.DefInstance.Font, false);
         // ------------------------------------------------------------------------
         // Datos del Importador.-
         // ------------------------------------------------------------------------
         if (MODXDOC.PartysOpe[1].EstadoUsado.TrimB().UCase() != "CHILE")
         {
            s2 = MODXDOC.Concatena(MODXDOC.PartysOpe[1].CiudadUsado,MODXDOC.PartysOpe[1].EstadoUsado,MODXDOC.PartysOpe[1].PostalUsado);
         }
         else
         {
            if (MODXDOC.PartysOpe[1].PostalUsado != "")
            {
               s2 = MODXDOC.PartysOpe[1].CiudadUsado + "," + MODXDOC.PartysOpe[1].PostalUsado;
            }
            else
            {
               s2 = MODXDOC.PartysOpe[1].CiudadUsado;
            }
         }

         XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeItalic(XGCV.Printer.DefInstance.Font, true);
         XGCV.Printer.DefInstance.Print(MODXDOC.PartysOpe[1].NombreUsado);
         intCaseArg = MODXDOC.PartysOpe[1].Enviara;
         if (intCaseArg == A_Dir || intCaseArg == A_Fax)
         {
            XGCV.Printer.DefInstance.Print(MODXDOC.PartysOpe[1].DireccionUsado);
            XGCV.Printer.DefInstance.Print(s2);
         }
         else if (intCaseArg == A_CBa)
         {
            XGCV.Printer.DefInstance.Print("Casilla Interna Banco: " + MODXDOC.PartysOpe[1].CasBanco);
            XGCV.Printer.DefInstance.Print("Presente");
         }
         else if (intCaseArg == A_CPo)
         {
            XGCV.Printer.DefInstance.Print("Casilla Postal: " + MODXDOC.PartysOpe[1].CasPostal);
            XGCV.Printer.DefInstance.Print(s2);
         }
         XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeItalic(XGCV.Printer.DefInstance.Font, false);

         XGCV.Printer.DefInstance.Print( );
         XGCV.Printer.DefInstance.Print( );
         NLinea = NLinea + 6;
         // ------------------------------------------------------------------------
         // Encabezado.-
         // ------------------------------------------------------------------------
         s = "De acuerdo a vuestras instrucciones hemos emitido ";
         switch(SDoccv.tipocarta)
         {
         case 1:
            s = s + "planillas, según el siguiente detalle :";
            break;
         case 2:
            s = s + "planillas, ";
            if (SDoccv.Concepto != "")
            {
               s = s + "por Concepto de " + SDoccv.Concepto;
               XGCV.Printer.DefInstance.Print(s);
               NLinea = NLinea + 1;
               s = "";
            }
            s = s + " según el siguiente detalle :";
            break;
         case 3:
            s = s + "Compras y Ventas de Divisas, según el siguiente detalle :";
            break;
         case 4:
            s = "Hemos efectuado Arbitraje de Divisas, según el siguiente detalle/distribución: ";
            break;
         }
         XGCV.Printer.DefInstance.Print(s);
         XGCV.Printer.DefInstance.Print( );
         NLinea = NLinea + 2;
         // ------------------------------------------------------------------------
         // Monto de las compras y/o ventas.-
         // ------------------------------------------------------------------------
         XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(XGCV.Printer.DefInstance.Font, true);
         XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeUnderline(XGCV.Printer.DefInstance.Font, true);

         switch(SDoccv.tipocarta)
         {
         case 1:
         case 2:
         case 3:
            if (HayOperacion("1") != 0)
            {
                    // Compras
               sw = 0;
               for(i = 1; i <= smtocv.GetUpperBound(0); i += 1)
               {
                  if (smtocv[i].Monto != 0)
                  {
                     sw = 1;
                     break;
                  }
               }
               if (sw == 1)
               {
                  XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeUnderline(XGCV.Printer.DefInstance.Font, true);
                  XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(XGCV.Printer.DefInstance.Font, true);
                  XGCV.Printer.DefInstance.PrintList(new object[]{XGCV.Printer.TAB(40),"Ventas de Divisas: "});
                  XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeUnderline(XGCV.Printer.DefInstance.Font, false);
                  XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(XGCV.Printer.DefInstance.Font, false);
                  XGCV.Printer.DefInstance.Print( );
                  XGCV.Printer.DefInstance.PrintList(new object[]{XGCV.Printer.TAB(24),"Monto",XGCV.Printer.TAB(54),"Tipo Cambio",XGCV.Printer.TAB(78),"Nº Planilla",XGCV.Printer.TAB(100),
                     "Declaración de Import."});
                  XGCV.Printer.DefInstance.Print( );
                  for(i = 0; i <= smtocv.GetUpperBound(0); i += 1)
                  {
                     if (smtocv[i].tipo == "1")
                     {
                        // If smtocv(i%).Monto <> "" Then 'PACP
                        if (smtocv[i].Monto != 0)
                        {
                           XGCV.Printer.DefInstance.PrintList(new object[]{smtocv[i].Moneda.UCase(),XGCV.Printer.TAB(10),MODGDOC.forma(smtocv[i].Monto,MODXDOC.Formato),XGCV.Printer.TAB(40),MODGDOC.forma(smtocv[i].Cambio,
                              "#,###,###,###,##0.0000"),XGCV.Printer.TAB(80),smtocv[i].NumPla,XGCV.Printer.TAB(110),smtocv[i].NumDec});
                           // ''Printer.Print UCase$(smtocv(i%).Moneda); Tab(10); forma(smtocv(i%).Monto, Formato); Tab(40); "Tipo de Cambio $"; Tab(60); forma(smtocv(i%).Cambio, "#,###,###,###,##0.0000"); Tab(90); "Total $"; Tab(100); forma(smtocv(i%).total_peso, FormatoSinDec)
                           NLinea = chequea_linea(ref NLinea);
                        }
                     }
                  }
               }
               XGCV.Printer.DefInstance.Print( );
               XGCV.Printer.DefInstance.Print( );
               NLinea = NLinea + 2;
            }
            if (HayOperacion("2") != 0)
            {
                    //  ventas
               sw = 0;
               for(i = 1; i <= smtocv.GetUpperBound(0); i += 1)
               {
                  if (smtotr[i].Monto != 0)
                  {
                     sw = 1;
                     break;
                  }
               }
               if (sw == 1)
               {
                  NLinea = NLinea - 1;
                  NLinea = chequea_linea(ref NLinea);
                  XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeUnderline(XGCV.Printer.DefInstance.Font, true);
                  XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(XGCV.Printer.DefInstance.Font, true);
                  XGCV.Printer.DefInstance.PrintList(new object[]{XGCV.Printer.TAB(5),"Ventas de Divisas: "});
                  XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeUnderline(XGCV.Printer.DefInstance.Font, false);
                  XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(XGCV.Printer.DefInstance.Font, false);
                  XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeUnderline(XGCV.Printer.DefInstance.Font, false);
                  XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(XGCV.Printer.DefInstance.Font, false);
                  XGCV.Printer.DefInstance.Print( );
                  XGCV.Printer.DefInstance.PrintList(new object[]{XGCV.Printer.TAB(24),"Monto",XGCV.Printer.TAB(54),"Tipo Cambio",XGCV.Printer.TAB(78),"Nº Planilla",XGCV.Printer.TAB(100),
                     "Declaración de Import."});
                  XGCV.Printer.DefInstance.Print( );
                  NLinea = NLinea + 2;
                  for(i = 0; i <= smtocv.GetUpperBound(0); i += 1)
                  {
                     if (smtocv[i].tipo == "2")
                     {
                        // If smtocv(i%).Monto <> "" Then
                        if (smtocv[i].Monto != 0)
                        {
                           XGCV.Printer.DefInstance.PrintList(new object[]{smtocv[i].Moneda.UCase(),XGCV.Printer.TAB(10),MODGDOC.forma(smtocv[i].Monto,MODXDOC.Formato),XGCV.Printer.TAB(40),MODGDOC.forma(smtocv[i].Cambio,
                              "#,###,###,###,##0.0000"),XGCV.Printer.TAB(80),smtocv[i].NumPla,XGCV.Printer.TAB(110),smtocv[i].NumDec});
                           // ''Printer.Print UCase$(smtocv(i%).Moneda); Tab(5); forma(smtocv(i%).Monto, Formato); Tab(20); "Tipo de Cambio $"; Tab(35); forma(smtocv(i%).Cambio, "#,###,###,###,##0.0000"); Tab(50); "Total $"; Tab(65); forma(smtocv(i%).total_peso, FormatoSinDec); Tab(80); smtocv(i%).NumPla; Tab(95); smtocv(i%).NumDec
                           NLinea = chequea_linea(ref NLinea);
                        }
                     }
                  }
               }
               XGCV.Printer.DefInstance.Print( );
               XGCV.Printer.DefInstance.Print( );
               NLinea = NLinea + 2;
            }
            // DDJT
            sw = 0;
            for(i = 1; i <= smtotr.GetUpperBound(0); i += 1)
            {
               if (smtotr[i].Monto != 0)
               {
                  sw = 1;
                  break;
               }
            }
            if (sw == 1)
            {
               XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeUnderline(XGCV.Printer.DefInstance.Font, true);
               XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(XGCV.Printer.DefInstance.Font, true);
               XGCV.Printer.DefInstance.PrintList(new object[]{XGCV.Printer.TAB(38),"Transferencias: "});
               XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeUnderline(XGCV.Printer.DefInstance.Font, false);
               XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(XGCV.Printer.DefInstance.Font, false);
               XGCV.Printer.DefInstance.Print( );
               XGCV.Printer.DefInstance.PrintList(new object[]{XGCV.Printer.TAB(24),"Monto",XGCV.Printer.TAB(54),"Nº Planilla",XGCV.Printer.TAB(78),"Declaración de Importación"});
               XGCV.Printer.DefInstance.Print( );
               for(i = 1; i <= smtotr.GetUpperBound(0); i += 1)
               {
                  // ''If smtotr(i%).tipo = "1" Then
                  // If smtocv(i%).Monto <> "" Then 'PACP
                  if (smtotr[i].Monto != 0)
                  {
                     XGCV.Printer.DefInstance.PrintList(new object[]{smtotr[i].Moneda.UCase(),XGCV.Printer.TAB(10),MODGDOC.forma(smtotr[i].Monto,MODXDOC.Formato),XGCV.Printer.TAB(56),smtotr[i].NumPla,XGCV.Printer.TAB(80
                        ),smtotr[i].NumDec});
                     NLinea = chequea_linea(ref NLinea);
                  }
                  // ''End If
               }
            }
            // DDJT
            break;
         case 4:
            sw = 0;
            for(i = 1; i <= smonarb.GetUpperBound(0); i += 1)
            {
               if (smonarb[i].monto_1.ToInt() != 0)
               {
                  sw = 1;
                  break;
               }
            }
            if (sw == 1)
            {
               NLinea = NLinea - 1;
               NLinea = chequea_linea(ref NLinea);
               XGCV.Printer.DefInstance.PrintList(new object[]{XGCV.Printer.TAB(6),"Compra de Divisas: ",XGCV.Printer.TAB(56),"Ventas de Divisas: ",XGCV.Printer.TAB(100),"Paridades "});
               XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeUnderline(XGCV.Printer.DefInstance.Font, false);
               XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(XGCV.Printer.DefInstance.Font, false);
               XGCV.Printer.DefInstance.Print( );
               NLinea = NLinea + 2;
               for(i = 0; i <= smonarb.GetUpperBound(0); i += 1)
               {
                  if (smonarb[i].monto_1 != "")
                  {
                     XGCV.Printer.DefInstance.PrintList(new object[]{XGCV.Printer.TAB(6),smonarb[i].moneda_1.UCase(),XGCV.Printer.TAB(15),smonarb[i].monto_1,XGCV.Printer.TAB(58),smonarb[i].moneda_2.UCase(),
                        XGCV.Printer.TAB(67),smonarb[i].monto_2,XGCV.Printer.TAB(108),smonarb[i].Paridad});
                     NLinea = chequea_linea(ref NLinea);
                  }
               }
            }
            XGCV.Printer.DefInstance.Print( );
            NLinea = NLinea + 1;
            break;
         }
         XGCV.Printer.DefInstance.Print( );
         NLinea = NLinea + 1;
         // ------------------------------------------------------------------------
         // Beneficiarios.-
         // ------------------------------------------------------------------------
         eltop = -1;
            eltop = sbencv.GetUpperBound(0);
            if (eltop >= 0)
            {
               NLinea = NLinea - 1;
               NLinea = chequea_linea(ref NLinea);
               s = "La moneda extranjera vendida fue distribuida de la siguiente forma :";
               XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeItalic(XGCV.Printer.DefInstance.Font, true);
               XGCV.Printer.DefInstance.Print(s);
               XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeItalic(XGCV.Printer.DefInstance.Font, false);
               XGCV.Printer.DefInstance.Print( );
               // --------------------------------------------------------------------
               // Título de Beneficiarios.-
               // --------------------------------------------------------------------
               sw = 0;
               for(i = 0; i <= sbencv.GetUpperBound(0); i += 1)
               {
                  if (sbencv[i].Monto != "")
                  {
                     sw = 1;
                     break;
                  }
               }

               if (sw == 1)
               {
                  NLinea = chequea_linea(ref NLinea);
                  XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(XGCV.Printer.DefInstance.Font, true);
                  XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeUnderline(XGCV.Printer.DefInstance.Font, true);
                  XGCV.Printer.DefInstance.PrintList(new object[]{"Beneficiario",XGCV.Printer.TAB(38),"Vía de la Remesa",XGCV.Printer.TAB(95),"Monto"});
                  XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(XGCV.Printer.DefInstance.Font, false);
                  XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeUnderline(XGCV.Printer.DefInstance.Font, false);
                  XGCV.Printer.DefInstance.Print("");
                  // --------------------------------------------------------------------
                  // Detalle de Beneficiarios.-
                  // --------------------------------------------------------------------
                  NLinea = NLinea + 3;
                  for(i = 0; i <= sbencv.GetUpperBound(0); i += 1)
                  {
                     if (sbencv[i].Monto != "")
                     {
                        XGCV.Printer.DefInstance.PrintList(new object[]{sbencv[i].Benef,XGCV.Printer.TAB(40),sbencv[i].Via,XGCV.Printer.TAB(93),sbencv[i].Moneda.UCase(),XGCV.Printer.TAB(100),sbencv[i].Monto});
                        NLinea = chequea_linea(ref NLinea);
                     }
                  }
               }
            }
            XGCV.Printer.DefInstance.Print( );
            NLinea = NLinea + 1;
            // ------------------------------------------------------------------------
            // Débitos.-
            // ------------------------------------------------------------------------
            eltop = -1;
            // OnErrorResumeNext();
            eltop = Sdebcv.GetUpperBound(0);
            if (eltop > 0)
            {
               NLinea = NLinea - 1;
               NLinea = chequea_linea(ref NLinea);
               XGCV.Printer.DefInstance.Print( );
               XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(XGCV.Printer.DefInstance.Font, true);
               XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeUnderline(XGCV.Printer.DefInstance.Font, true);
               XGCV.Printer.DefInstance.PrintList(new object[]{XGCV.Printer.TAB(38),"Cargos y/o Abonos"});
               XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeUnderline(XGCV.Printer.DefInstance.Font, false);
               XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(XGCV.Printer.DefInstance.Font, false);
               XGCV.Printer.DefInstance.Print( );
               s = "En consecuencia efectuamos los débitos y/o abonos que se detallan: ";
               XGCV.Printer.DefInstance.Print(s);
               XGCV.Printer.DefInstance.Print( );
               NLinea = NLinea + 3;
               // --------------------------------------------------------------------
               // Detalle de Débitos.-
               // --------------------------------------------------------------------
               for(i = 0; i <= Sdebcv.GetUpperBound(0); i += 1)
               {
                  if (Sdebcv[i].Monto != "")
                  {
                     XGCV.Printer.DefInstance.PrintList(new object[]{Sdebcv[i].deb_hab,XGCV.Printer.TAB(25),Sdebcv[i].Debito,XGCV.Printer.TAB(93),Sdebcv[i].Moneda.UCase(),XGCV.Printer.TAB(100),Sdebcv[i].Monto});
                     if (Sdebcv[i].Debito.Mid(1, 23) == "Cuenta Corriente M/N  $")
                     {
                        EsOrigenPesos = true;
                     }
                     NLinea = chequea_linea(ref NLinea);
                  }
               }
            }
            EsOrigenPesos = true;
            if (SDoccv.tipocarta == 4)
            {
               EsOrigenPesos = false;
            }
            if (EsOrigenPesos)
            {
               // --------------------------------------------------------------------
               // Impuestos en $.-
               // --------------------------------------------------------------------
               XGCV.Printer.DefInstance.Print( );
               XGCV.Printer.DefInstance.Print( );
               XGCV.Printer.DefInstance.Print( );
               NLinea = NLinea + 2;
               NLinea = chequea_linea(ref NLinea);
               eltop = -1;
               // OnErrorResumeNext();
               eltop = sdetcv.GetUpperBound(0);
               if (eltop >= 0)
               {
                  XGCV.Printer.DefInstance.Print( );
                  XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(XGCV.Printer.DefInstance.Font, true);
                  XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeUnderline(XGCV.Printer.DefInstance.Font, true);
                  XGCV.Printer.DefInstance.Print("Detalle :");
                  XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(XGCV.Printer.DefInstance.Font, false);
                  XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeUnderline(XGCV.Printer.DefInstance.Font, false);
                  // ----------------------------------------------------------------
                  // Detalle de Impuestos.-
                  // ----------------------------------------------------------------
                  XGCV.Printer.DefInstance.Print( );
                  NLinea = NLinea + 3;
                  TotDetalle = 0;
                  for(i = 0; i <= sdetcv.GetUpperBound(0); i += 1)
                  {
                     if (sdetcv[i].Detalle.Mid(1, 5) == "Monto" && SDoccv.tipocarta == 4)
                     {

                     }
                     else
                     {
                        if (i == sdetcv.GetUpperBound(0) && sdetcv.GetUpperBound(0) > 0)
                        {
                           if (sdetcv[i].Monto != "")
                           {
                              if (sdetcv[i].Detalle == "Monto en pesos de la moneda extranjera vendida")
                              {
                                 if (sdetcv[i].Monto.ToInt() > 0)
                                 {
                                    XGCV.Printer.DefInstance.PrintList(new object[]{XGCV.Printer.TAB(10),sdetcv[i].Detalle,XGCV.Printer.TAB(93),"$",XGCV.Printer.TAB(100),sdetcv[i].Monto});
                                    TotDetalle = TotDetalle + MigrationSupport.Utils.Format(sdetcv[i].Monto,String.Empty).ToDbl();
                                    NLinea = chequea_linea(ref NLinea);
                                 }
                              }
                              else
                              {
                                 XGCV.Printer.DefInstance.PrintList(new object[]{XGCV.Printer.TAB(10),sdetcv[i].Detalle,XGCV.Printer.TAB(93),"$",XGCV.Printer.TAB(100),sdetcv[i].Monto});
                                 TotDetalle = TotDetalle + MigrationSupport.Utils.Format(sdetcv[i].Monto,String.Empty).ToDbl();
                                 NLinea = chequea_linea(ref NLinea);
                              }
                           }
                        }
                        else
                        {
                           if (sdetcv[i].Monto != "")
                           {
                              if (sdetcv[i].Detalle == "Monto en pesos de la moneda extranjera vendida")
                              {
                                 if (sdetcv[i].Monto.ToInt() > 0)
                                 {
                                    XGCV.Printer.DefInstance.PrintList(new object[]{XGCV.Printer.TAB(10),sdetcv[i].Detalle,XGCV.Printer.TAB(93),"$",XGCV.Printer.TAB(100),sdetcv[i].Monto});
                                    TotDetalle = TotDetalle + MigrationSupport.Utils.Format(sdetcv[i].Monto,String.Empty).ToDbl();
                                    NLinea = chequea_linea(ref NLinea);
                                 }
                              }
                              else
                              {
                                 XGCV.Printer.DefInstance.PrintList(new object[]{XGCV.Printer.TAB(10),sdetcv[i].Detalle,XGCV.Printer.TAB(93),"$",XGCV.Printer.TAB(100),sdetcv[i].Monto});
                                 TotDetalle = TotDetalle + MigrationSupport.Utils.Format(sdetcv[i].Monto,String.Empty).ToDbl();
                                 NLinea = chequea_linea(ref NLinea);
                              }
                           }
                        }
                     }
                  }
               }
               if (TotDetalle > 0)
               {
                  XGCV.Printer.DefInstance.PrintList(new object[]{XGCV.Printer.TAB(80),"________________________________"});
                  XGCV.Printer.DefInstance.PrintList(new object[]{XGCV.Printer.TAB(85),"Total : ",XGCV.Printer.TAB(93),"$",XGCV.Printer.TAB(100),SDoccv.Totpes});
                  // printer.Print Tab(85); "Total : "; Tab(93); "$"; Tab(96); Format$(TotDetalle#, Formato)
               }
            }
            while (NLinea < 45)
            {
               NLinea = NLinea + 1;
               XGCV.Printer.DefInstance.Print( );
            }
            // ***************************************** MPN REALSYSTEMS MAV 19/08/2010 SE COMENTA*******************************************************************
            //       If Hab_SGTCliEje Then
            //            Cliente_SGT = Es_Cliente(Mid(rutiparty, 1, 9))
            //            If Cliente_SGT Then
            //                bien% = Lee_SgtCliEsp(Mid(rutiparty, 1, 9))
            //                If bien% Then
            //                  For i% = 1 To UBound(VSGTCliEsp)
            //                     If VSGTCliEsp(i%).tipo = SGT_tipopimp Then
            //                        nom_eje = Obtiene_NomEsp(VSGTCliEsp(i%).ofieje, VSGTCliEsp(i%).codeje)
            //                     ElseIf VSGTCliEsp(i%).tipo = SGT_tipopexp Then
            //                        nom_eje = Obtiene_NomEsp(VSGTCliEsp(i%).ofieje, VSGTCliEsp(i%).codeje)
            //                     ElseIf VSGTCliEsp(i%).tipo = SGT_tipnegoc Then
            //                        nom_eje = Obtiene_NomEsp(VSGTCliEsp(i%).ofieje, VSGTCliEsp(i%).codeje)
            //                     End If
            //                  Next i%
            //                   'End If
            //                  For i% = 1 To UBound(VEjc)
            //                     If nom_eje = Minuscula2(VEjc(i%).nombre) Then
            //                        dir_eje = sacar_direccion(VEjc(i%).rut)
            //                        tel_eje = VEjc(i%).telefono
            //                        fax_eje = VEjc(i%).Fax
            //                        If fax_eje = "" Then
            //                           fax_eje = " "
            //                        End If
            //                        If tel_eje = "" Then
            //                           tel_eje = " "
            //                        End If
            //                        Exit For
            //                     End If
            //                  Next i%
            //                End If
            //            End If
            //      End If
            //   If nom_eje <> "" Then
            //        SDoccv.NomEsp = nom_eje
            //        SDoccv.DirEsp = dir_eje
            //        SDoccv.TelEsp = tel_eje
            //        SDoccv.FaxEsp = fax_eje
            //   End If
            // **********************************************************************************
            //     cad$ = raya_Cobranza(VOpe.NumOpe) + "~" + SDoccv.NomEsp + " " + SDoccv.DirEsp + "~" + SDoccv.TelEsp + "~" + SDoccv.FaxEsp
            // 
            //     s$ = SyGet_Fra(9002, "E", cad$)'
            // 
            //     Printer.FontBold = False
            //     x% = GetLines(s$, ServxDoc.CajaMultilinea, Lineas())
            //     For i% = 1 To UBound(Lineas)
            //         If Trim$(Lineas(i%)) <> "" Then Printer.Print Lineas(i%)
            //     Next i%
            //     Printer.FontUnderline = False
            //     Printer.FontBold = False
      }
      public static string sacar_direccion(string rut_eje)
      {
         string sacar_direccion = "";

         string TitEjc = "";
         string R = "";
         string Que = "";
         // *************************************** MPN *****************************************************
         Que = "";
         Que = "Exec " + MODGSRM.ParamSrm8k.base_migname + "." + MODGSRM.ParamSrm8k.usuario + "." + "sce_usr_s25";
         Que = Que.LCase();
         Que = Que + MODGSYB.dbcharSy(rut_eje);

         // 
         //     Que$ = ""
         //     Que$ = Que$ + "select direccion from sce_usr"
         //     Que$ = Que$ + " where rut = " + dbcharSy(rut_eje)
         R = MODGSRM.RespuestaQuery(ref Que);

         if (R == "-1")
         {
            MigrationSupport.Utils.MsgBox("Se ha producido un error al tratar de leer la Tabla Sce_usr",MODGDOC.Pito(48).Cast<MigrationSupport.MsgBoxStyle>(),TitEjc);
            //   GoTo sacar_direccionErr
            return sacar_direccion;
         }

         // Resultado nulo de la Consulta.-
         if (R == "")
         {
            // MsgBox "No se han encontrado datos en la Tabla Sce_Usr", Pito(48), ""
            return sacar_direccion;
            // GoTo sacar_direccionEnd
         }

         sacar_direccion = MODGSYB.GetPosSy(MODGSYB.NumIni(),"C",R).ToStr();
         // ******************************************* MPN *********************************+
         return sacar_direccion;
      }
      public static int Lee_SgtCliEsp(string rutcli)
      {
         int Lee_SgtCliEsp = 0;
         string R = "";
         string rut = "";
         int fin = 0;
         int i = 0;
         int sig = 0;
         int h = 0;

         try
         {
            // ParamSgt
            rut = "12345678";
            R = "";
            string argTemp1 = "";
            //R = RespuestaSgt2(Module1.ParamSgt.Nodo,rut,Module1.ParamSgt.SerLee,Module1.ParamSgt.VisLee,ref argTemp1,rutcli,"V0009");

            if (R == "-1")
            {
               MigrationSupport.Utils.MsgBox("Hay problemas de comunicación con el SRM o bien con la vista L023. No se podrá rescatar el nombre del ejecutivo.",MODGDOC.Pito(48).Cast<MigrationSupport.MsgBoxStyle>(),"Atención");
               return Lee_SgtCliEsp;
            }


            Module1.RSGTCliEsp = new Module1.CliEsp[1];
            Module1.VSGTCliEsp = new Module1.CliEsp[1];

            if (R != "")
            {
               fin = (R.Mid(2, 4)).ToInt();

               R = R.Mid(6, R.Len() - 5);
            }

            h = 0;

            sig = 0;

            for(i = 1; i <= fin; i += 1)
            {

               if (R.Mid((14 + sig), 2) == Module1.SGT_tipopimp || R.Mid((14 + sig), 2) == Module1.SGT_tipopexp || R.Mid((14 + sig), 2) == Module1.SGT_tipnegoc)
               {
                  h = h + 1;
                  Array.Resize(ref Module1.RSGTCliEsp, h + 1);
                  Array.Resize(ref Module1.VSGTCliEsp, h + 1);
                  Module1.RSGTCliEsp[h].nrut = R.Mid((5 + sig), 9);
                  Module1.RSGTCliEsp[h].tipo = R.Mid((14 + sig), 2);
                  Module1.RSGTCliEsp[h].ofieje = R.Mid((16 + sig), 3);
                  Module1.RSGTCliEsp[h].codeje = R.Mid((19 + sig), 3);
                  Module1.RSGTCliEsp[h].feccre = R.Mid((22 + sig), 8);
                  Module1.RSGTCliEsp[h].rutope = R.Mid((30 + sig), 9);
                  Module1.RSGTCliEsp[h].drutope = R.Mid((39 + sig), 1);
                  Module1.RSGTCliEsp[h].filler = R.Mid((40 + sig), 35);
                  Module1.RSGTCliEsp[h].borrar = false.ToInt();
                  Module1.VSGTCliEsp[h] = Module1.RSGTCliEsp[h];

               }
               sig = sig + 74;

            }

            Lee_SgtCliEsp = true.ToInt();

            return Lee_SgtCliEsp;

         }
         catch(Exception exc)
         {
            MigrationSupport.GlobalException.Initialize(exc);
            System.Windows.Forms.MessageBox.Show("Error al Actualizar en tablas SGT : [" + MigrationSupport.Utils.Format(MigrationSupport.GlobalException.Instance.Number,String.Empty) + "] " + MigrationSupport.Utils.GetErrorDescription(
               MigrationSupport.GlobalException.Instance.Number), "", MessageBoxButtons.OK);
            Lee_SgtCliEsp = false.ToInt();


         }
         return Lee_SgtCliEsp;
      }
      // Deja un string de varias palabras separadas por un blanco como la primera
      // letra en Mayúscula y el resto de la palabra en minúscula.-
      public static string Minuscula2(string PDato)
      {
         string Minuscula2 = "";

         string strCaseArg = "";
         int j = 0;
         string s = "";
         int i = 0;
         string[] Palabras = null;
         //  *********************************  MPN  *************************************************************
         // D.S.B.
         Palabras = new string[1];
         string Dato = "";

         // Deja cada palabra separado con un solo blanco
         Dato = MODGDOC.Componer(PDato,"  "," ");
         i = 1;
         s = MODGDOC.CopiarDeString(Dato," ",i);
         while (s != "")
         {
            // Verifica si la palabra siguiente contiene la cantidad de caracteres necesarios.
            j = Palabras.GetUpperBound(0) + 1;
            Array.Resize(ref Palabras, j + 1);
            strCaseArg = s.UCase().TrimB();
            if (strCaseArg == "S.A." || strCaseArg == "M/E" || strCaseArg == "M/N")
            {
               Palabras[j] = s.UCase();
            }
            else if (strCaseArg == "A" || strCaseArg == "DE" || strCaseArg == "Y" || strCaseArg == "O" || strCaseArg == "Y/O" || strCaseArg == "U" || strCaseArg == "AL" || strCaseArg == "DE" || strCaseArg == "LO" || strCaseArg == "LA" || strCaseArg == "EL" ||
               strCaseArg == "SI" || strCaseArg == "NO" || strCaseArg == "E" || strCaseArg == "POR" || strCaseArg == "OF")
            {
               Palabras[j] = s.LCase();
            }
            else
            {
               Palabras[j] = s.Mid(1, 1).UCase() + s.Mid(2, s.Len() - 1).LCase();
            }
            i = i + 1;
            s = MODGDOC.CopiarDeString(Dato," ",i);
         }
         s = "";
         for(i = 1; i <= Palabras.GetUpperBound(0); i += 1)
         {
            s = s + Palabras[i] + " ";
         }
         Minuscula2 = s.TrimB();

         return Minuscula2;
      }
      public static string Obtiene_NomEsp(object codofi,string CodEsp)
      {
         string Obtiene_NomEsp = "";


         int i = 0;
         int fin = 0;


         Obtiene_NomEsp = "";

         fin = 0;
         fin = Module1.VEjc.GetUpperBound(0);

         for(i = 1; i <= fin; i += 1)
         {
            if (Module1.VEjc[i].codofi == codofi.ToStr() && Module1.VEjc[i].codejc == CodEsp)
            {
               Obtiene_NomEsp = Minuscula2(Module1.VEjc[i].nombre);
               break;
            }
         }
         return Obtiene_NomEsp;
      }
      public static int Es_Cliente(string rutcli)
      {
         int Es_Cliente = 0;

         string R = "";
         //  colocado para poder buscar a los ejecutivos
         string rut = "";
         int fin = 0;
         int i = 0;
         int sig = 0;
         int h = 0;

         try
         {

            rut = "12345678";
            R = "";
            string argTemp1 = "";
            R = RespuestaSgt2(Module1.ParamSgt.Nodo,rut,Module1.ParamSgt.SerLee,Module1.ParamSgt.VisClt,ref argTemp1,rutcli,"V0009");

            if (R == "-1")
            {
               MigrationSupport.Utils.MsgBox("Hay problemas de comunicación con el SRM o bien con la vista L001. No se podrá rescatar el nombre del ejecutivo.",MODGDOC.Pito(48).Cast<MigrationSupport.MsgBoxStyle>(),"Atención");
               return Es_Cliente;
            }

            if (R == "")
            {
               Es_Cliente = false.ToInt();
            }
            else
            {
               //  Es Cliente
               Es_Cliente = true.ToInt();
            }

            // 

            return Es_Cliente;

         }
         catch(Exception exc)
         {
            MigrationSupport.GlobalException.Initialize(exc);
            System.Windows.Forms.MessageBox.Show("Error al Actualizar en tablas SGT : [" + MigrationSupport.Utils.Format(MigrationSupport.GlobalException.Instance.Number,String.Empty) + "] " + MigrationSupport.Utils.GetErrorDescription(
               MigrationSupport.GlobalException.Instance.Number), "", MessageBoxButtons.OK);
            Es_Cliente = false.ToInt();

            // 

         }
         return Es_Cliente;
      }
      public static string RespuestaSgt2(string Nodo,string RutCons,string Servidor,string Vista,ref string Tabla,string llave,string Oper)
      {
         string RespuestaSgt2 = "";

         string s = "";
         string R = "";
         int x = 0;
         string m = "";


         int Intentos = 0;
         MODGSRM.RowCount = 0;
         Intentos = 0;

      Conssgt2:
         Intentos = Intentos + 1;
         Tabla = Tabla.UCase();
         m = RutCons + Vista.TrimB() + Oper.TrimB() + Tabla.TrimB() + llave;
         Module1.ParamSgt.Mensaje = m;
         Module1.ParamSgt.largo = m.Len();
         Module1.ParamSgt.Status = "00";
         Module1.ParamSgt.Funcion = "08";
         Module1.ParamSgt.Contexto = "00";
         object argTemp1 = Module1.ParamSgt.largo;
         x = MODGSRM.srmw8(Nodo,Servidor,Module1.ParamSgt.Mensaje,ref argTemp1,Module1.ParamSgt.Status,Module1.ParamSgt.Funcion,Module1.ParamSgt.Contexto,Module1.ParamSgt.Control);
         if (!(x == 0 && Module1.ParamSgt.Mensaje.Left(2) == "00"))
         {
            if (Module1.ParamSgt.Mensaje.Left(2) == "96")
            {
               RespuestaSgt2 = "";
               return RespuestaSgt2;
            }

            if (Intentos <= 1)
            {
               goto Conssgt2;
            }
            RespuestaSgt2 = "-1";
            return RespuestaSgt2;
         }
         R = Module1.ParamSgt.Mensaje.TrimB();

         s = R.Mid(3, R.Len() - 2);

         RespuestaSgt2 = s;


         return RespuestaSgt2;
      }
   }
}
