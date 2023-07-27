using BCH.Comex.Core.BL.XGPY.Modulos;
//using BCH.Comex.Common.XGPY.Datatypes;
//using BCH.Comex.Common.XGPY.T_Modulos;
//using BCH.Comex.Common.XGPY.UI_Modulos;
using BCH.Comex.Core.Entities.Cext01.AdminParticipantes.Datatypes;
using BCH.Comex.Core.Entities.Cext01.AdminParticipantes.T_Modulos;
using BCH.Comex.Data.DAL.Cext01;
using CodeArchitects.VB6Library;
using System;
using System.Linq;

namespace BCH.Comex.Core.BL.XGPY.Forms
{
    public static class PrtEnt04
    {
        static string[] ResMemo = null;
        static string[] Nombres = null;
        // constantes de nombre para Combo
        private const string ConMemo = "»» ";
        private const string SinMemo = "     ";
        public static void Aceptar_Click(InitializationObject initObj)
        {
            int i = 0;
            // si dio un aceptar sin realizar un click en la combo, forzarlo
            CmbMemo_Click(initObj);

            initObj.PRTGLOB.Party.PrtGlob.FlagParty = 1;
            if (initObj.PRTGLOB.Party.estado == T_PRTGLOB.leido)
                initObj.PRTGLOB.Party.estado = T_PRTGLOB.modificado;

            // calculamos los flag de partys
            initObj.PRTGLOB.Party.flagins = 0;
            for (i = 0; i <= 6; i += 1)
            {
                if (ResMemo[i] != "")
                {
                    initObj.PRTGLOB.Party.flagins = (initObj.PRTGLOB.Party.flagins + Math.Pow(2, i)).ToInt();
                }
            }

            // recalculamos el flag de partys
            if (initObj.PRTGLOB.Party.flagins.ToBool() && !(initObj.PRTGLOB.Party.Flag & T_PRTGLOB.GPRT_FlagInstrucciones).ToBool())
            {
                initObj.PRTGLOB.Party.Flag = initObj.PRTGLOB.Party.Flag + T_PRTGLOB.GPRT_FlagInstrucciones;
            }
            else
            {
                if (!initObj.PRTGLOB.Party.flagins.ToBool() && (initObj.PRTGLOB.Party.Flag & T_PRTGLOB.GPRT_FlagInstrucciones).ToBool())
                    initObj.PRTGLOB.Party.Flag = initObj.PRTGLOB.Party.Flag - T_PRTGLOB.GPRT_FlagInstrucciones;
            }

            initObj.PRTGLOB.Party.estado = 5;
        }
        public static void Cancelar_Click(InitializationObject initObj)
        {
            return;
        }
        public static void CmbMemo_Click(InitializationObject initObj)
        {
            int valor = initObj.PrtEnt04.CmbMemo.Tag.ToInt();
            if (valor == -1)
                initObj.PrtEnt04.prtinstruc.Text = string.Empty;
            else
                initObj.PrtEnt04.prtinstruc.Text = initObj.PRTGLOB.instruccion[initObj.PrtEnt04.CmbMemo.ListIndex] == null ? string.Empty : initObj.PRTGLOB.instruccion[initObj.PrtEnt04.CmbMemo.ListIndex].Memo.ToString().Trim(); //initObj.PRTGLOB.instruccion[initObj.PrtEnt04.CmbMemo.ListIndex].Memo.ToString().Trim();

            initObj.PrtEnt04.CmbMemo.Tag = VB6Helpers.Format(initObj.PrtEnt04.CmbMemo.ListIndex, String.Empty);
        }
        public static void Form_Load(InitializationObject initObj, UnitOfWorkCext01 uow)
        {
            int i = 0;
            T_PRTGLOB.FlagInstruccion = 1;
            ResMemo = new string[7];
            Nombres = new string[7];
            initObj.PRTGLOB.instruccion = new prtyinst[7];

            // traspaso los datos actuales al vector de respaldo
            ResMemo[0] = initObj.PRTGLOB.Party.insgen_imp.ToStr();
            ResMemo[1] = initObj.PRTGLOB.Party.insgen_exp.ToStr();
            ResMemo[2] = initObj.PRTGLOB.Party.insgen_ser.ToStr();
            ResMemo[3] = initObj.PRTGLOB.Party.inscob_imp.ToStr();
            ResMemo[4] = initObj.PRTGLOB.Party.inscob_exp.ToStr();
            ResMemo[5] = initObj.PRTGLOB.Party.inscre_imp.ToStr();
            ResMemo[6] = initObj.PRTGLOB.Party.inscre_exp.ToStr();

            // cargamos los nombres de glosa
            Nombres[0] = "Generales de Importación";
            Nombres[1] = "Generales de Exportación";
            Nombres[2] = "Generales de Servicios";
            Nombres[3] = "Cobranzas de Importación";
            Nombres[4] = "Cobranzas de Exportación";
            Nombres[5] = "Carta de Crédito de Importación";
            Nombres[6] = "Carta de Crédito de Exportación";

            initObj.PrtEnt04.CmbMemo.Clear();

            // seteamos los valores...            
            initObj.PRTGLOB.instruccion = new prtyinst[Nombres.Count()];
            for (i = 0; i <= Nombres.Count() - 1; i += 1)
            {
                //initObj.PRTGLOB.instruccion[i] = new prtyinst();
                if (ResMemo[i].ToStr() != "0")
                {
                    // Aquí se carga la estructura de memos
                    //initObj.PRTGLOB.instruccion[i] = new prtyinst();
                    //initObj.PRTGLOB.instruccion[i].codigo = ResMemo[i].ToInt() == 0 ? 0 : ResMemo[i].ToInt();//i;
                    //initObj.PRTGLOB.instruccion[i].Memo = MODGMEM.SyGetn_Mem(initObj, uow, "p", ResMemo[i].ToInt()).ToStr();
                    initObj.PRTGLOB.instruccion[i] = new prtyinst()
                    {
                        codigo = ResMemo[i].ToInt() == 0 ? 0 : ResMemo[i].ToInt(),
                        Memo = MODGMEM.SyGetn_Mem(initObj, uow, "p", ResMemo[i].ToInt()).ToStr()
                    };

                    initObj.PrtEnt04.CmbMemo.Items.Add(new BCH.Comex.Common.UI_Modulos.UI_ComboItem
                  {
                      ID = i.ToString(),
                      Data = i,
                      Value = ConMemo + Nombres[i]
                  });
                }
                else
                {
                    initObj.PrtEnt04.CmbMemo.Items.Add(new BCH.Comex.Common.UI_Modulos.UI_ComboItem
                      {
                          ID = i.ToString(),
                          Data = i,
                          Value = SinMemo + Nombres[i]
                      });
                }
            }

            // marcar y desplegar el correspondiente
            initObj.PrtEnt04.CmbMemo.Tag = "-1";
            initObj.PrtEnt04.CmbMemo.ListIndex = -1;
            initObj.PrtEnt04.prtinstruc.Text = string.Empty;
            initObj.PrtEnt04.prtinstruc.Enabled = false;
            initObj.PrtEnt04.aceptar.Enabled = false;
        }

        public static void prtinstruc_Change(InitializationObject initObj)
        {
            T_PRTGLOB.FlagInstruccion = 1;
        }
        public static void prtinstruc_KeyPress(InitializationObject initObj)
        {
            initObj.PrtEnt04.aceptar.Enabled = initObj.PrtEnt04.CmbMemo.ListIndex == -1 ? false : true;
        }

        public static void prtinstruc_LostFocus(InitializationObject initObj)
        {
            int ant = 0;
            ant = initObj.PrtEnt04.CmbMemo.Tag.ToInt();
            if (ant != -1)
            {
                string prtinstruc = initObj.PrtEnt04.prtinstruc.Text == null ? string.Empty : initObj.PrtEnt04.prtinstruc.Text;

                if (!string.IsNullOrEmpty(prtinstruc))
                {
                    initObj.PrtEnt04.CmbMemo.ListIndex = ant;
                    initObj.PrtEnt04.CmbMemo.Items[ant].Value = ConMemo + Nombres[ant];
                }
                else
                {
                    initObj.PrtEnt04.CmbMemo.ListIndex = ant;
                    initObj.PrtEnt04.CmbMemo.Items[ant].Value = SinMemo + Nombres[ant];
                }

                //initObj.PRTGLOB.instruccion[ant].Memo = initObj.PrtEnt04.prtinstruc.Text.TrimB();
                initObj.PRTGLOB.instruccion[ant] = new prtyinst()
                {
                    Memo = initObj.PrtEnt04.prtinstruc.Text.TrimB()
                };
            }

        }

    }
}
