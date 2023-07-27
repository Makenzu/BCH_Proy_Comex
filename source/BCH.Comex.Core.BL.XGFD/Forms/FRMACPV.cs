
using BCH.Comex.Common;
using BCH.Comex.Core.Entities.Cext01.FinDia;

namespace BCH.Comex.Core.BL.XGFD.Forms
{
    public static class FRMACPV
    {
        public static string Imp_jAcp(T_CCIRLLVR CC)
        {
            int i = 0;
            int linea = 0;
            int num_pag = 0;

            Printer printer = new Printer();

            num_pag = 1;
            //MigrationSupport.Utils.Scale(this, 0.0, 0.0, 42.66666666667, 32.0);
            //MigrationSupport.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(MigrationSupport.Printer.DefInstance.Font, true);

            //MigrationSupport.Printer.DefInstance.Print();
            printer.Print("                    ** Aceptaciones de CCI Vencidas  **");
            //printer.Font = MigrationSupport.Utils.FontChangeBold(MigrationSupport.Printer.DefInstance.Font, false);
            //printer.Font = MigrationSupport.Utils.FontChangeSize(MigrationSupport.Printer.DefInstance.Font, 9);
            printer.Print("__________________________________________________________________________________________________________");
            printer.Print("       Referencia        Moneda Acp          Saldo Acp                 Venc Acp ");
            printer.Print("__________________________________________________________________________________________________________");


            //MigrationSupport.Printer.DefInstance.CurrentY = ((int)(MigrationSupport.Printer.DefInstance.CurrentY + 1));
            linea = 4;

            // printer.FontName = "Courier New"
            //MigrationSupport.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeName(MigrationSupport.Printer.DefInstance.Font, "Arial");

            foreach (T_jAcp jAcp in CC.VjAcp)
            {
                if (linea > 59)
                {
                    num_pag = num_pag + 1;
                    linea = 0;
                    printer.NewPage();
                }
                
                printer.PrintList(Printer.TAB(5),jAcp.refere.Trim(),Printer.TAB(37),jAcp.monacpnemonico,Printer.TAB(56),jAcp.salacp.ToString("#,###,###,###,##0.#0"),Printer.TAB(97),jAcp.venacp);

                linea = linea + 1;
            }
            printer.EndDoc();

            return printer.ToString();
            
            //for (i = 1; i <= Lista.Items.Count; i += 1)
            //{
            //    if (linea > 59)
            //    {
            //        num_pag = num_pag + 1;
            //        linea = 0;
            //        MigrationSupport.Printer.DefInstance.NewPage();
            //    }

            //    MigrationSupport.Printer.DefInstance.PrintList(new object[]{MigrationSupport.FileSystem.TAB(5),CCIRLLVR.VjAcp[i].refere.TrimB(),MigrationSupport.FileSystem.TAB(37),MODGTAB0.Get_NemMnd(CCIRLLVR.VjAcp[i].monacp),MigrationSupport.FileSystem.TAB(56),
            //   MODGPYF0.forma(CCIRLLVR.VjAcp[i].salacp,"#,###,###,###,##0.#0"),MigrationSupport.FileSystem.TAB(97),CCIRLLVR.VjAcp[i].venacp});

            //    linea = linea + 1;
            //}
            //MigrationSupport.Printer.DefInstance.EndDoc();
        }
   
    }
}
