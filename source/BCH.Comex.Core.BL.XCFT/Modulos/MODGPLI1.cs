using BCH.Comex.Common.Tracing;
using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.DataTypes;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.T_Modulos;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.UI_Modulos;
using BCH.Comex.Data.DAL.Cext01;
using CodeArchitects.VB6Library;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BCH.Comex.Core.BL.XCFT.Modulos
{
    public class MODGPLI1
    {
        public static T_MODGPLI1 GetMODGPLI1()
        {
            return new T_MODGPLI1();
        }

        //CVD - Grabar Planilla Invisible.
        public static int SyPutn_Pli(InitializationObject initObject, UnitOfWorkCext01 unit, string CodAnu, short Estado)
        {
            using (var tracer = new Tracer("Grabar Planilla Invisible - SyPutn_Pli"))
            {
                T_MODGPLI1 MODGPLI1 = initObject.MODGPLI1;
                T_MODGCVD MODGCVD = initObject.MODGCVD;
                UI_Mdi_Principal Mdi_Principal = initObject.Mdi_Principal;

                int _retValue = 0;
                short i = 0;
                short n = 0;
                short ConError = 0;
                bool pasa = false;
                bool grabaAlguna = false;
                string motivo = String.Empty;
                try
                {

                    n = (short)VB6Helpers.UBound(MODGPLI1.Vplis);

                    for (i = 0; i <= (short)n; i++)
                    {
                        //--------------------------------------------------------------------------------------------------------------
                        //Accenture-Código Nuevo-Inicio
                        //Fecha Modificación 22022012
                        //Responsable: Angel Donoso Gonzalez.
                        //Versión: 1.0
                        //Descripción : se agrega condición para que solo grabe las planillas que sean distintas a Transferencia Interna
                        //--------------------------------------------------------------------------------------------------------------
                        pasa = false;
                        if (MODGCVD.VgPli.Length == 0)
                        {
                            pasa = true;
                            motivo += "VgPli.Length = 0\n";
                        }
                        else if (MODGCVD.VgPli[i].TipCVD != "TIN")
                        {
                            pasa = true;
                        }
                        else
                        {
                            motivo = "TipCVD = 'TIN'\n";
                        }

                        if (pasa == true)
                        {
                            //---------------------------------------------------------------------------------------------------------------
                            // Accenture - Código Nuevo - Termino
                            //---------------------------------------------------------------------------------------------------------------
                            if (MODGPLI1.Vplis[i].Status != T_MODGCVD.EstadoEli)
                            {
                                grabaAlguna = true;

                                List<string> parameters = new List<string>();
                                parameters.Add(MODGSYB.dbcharSy(MODGPLI1.Vplis[i].NumPli));
                                parameters.Add(MODGSYB.dbdatesy(MODGPLI1.Vplis[i].FecPli));
                                parameters.Add(MODGSYB.dbcharSy(MODGPLI1.Vplis[i].cencos));
                                parameters.Add(MODGSYB.dbcharSy(MODGPLI1.Vplis[i].codusr));
                                parameters.Add(MODGSYB.dbdatesy(MODGPLI1.Vplis[i].Fecing));
                                //-----------------------------------------------
                                parameters.Add(MODGSYB.dbcharSy(MODGPLI1.Vplis[i].codcct));
                                parameters.Add(MODGSYB.dbcharSy(MODGPLI1.Vplis[i].codpro));
                                parameters.Add(MODGSYB.dbcharSy(MODGPLI1.Vplis[i].codesp));
                                parameters.Add(MODGSYB.dbcharSy(MODGPLI1.Vplis[i].codofi));
                                parameters.Add(MODGSYB.dbcharSy(MODGPLI1.Vplis[i].codope));
                                parameters.Add(MODGSYB.dbcharSy(CodAnu));
                                //-----------------------------------------------
                                if (Estado == T_MODXPLN1.ExPlv_Anulada)
                                {
                                    parameters.Add(MODGSYB.dbnumesy(T_MODXPLN1.ExPlv_Anulada));
                                }
                                else
                                {
                                    parameters.Add(MODGSYB.dbnumesy(MODGPLI1.Vplis[i].Estado));
                                }

                                parameters.Add(MODGSYB.dbcharSy(MODGPLI1.Vplis[i].CodOper));
                                parameters.Add(MODGSYB.dbnumesy(MODGPLI1.Vplis[i].PlzBcc));
                                //-----------------------------------------------
                                parameters.Add(MODGSYB.dbcharSy(MODGPLI1.Vplis[i].rutcli));
                                parameters.Add(MODGSYB.dbcharSy(MODGPLI1.Vplis[i].PrtCli));
                                parameters.Add(MODGSYB.dbnumesy(MODGPLI1.Vplis[i].IndNom));
                                parameters.Add(MODGSYB.dbnumesy(MODGPLI1.Vplis[i].IndDir));
                                parameters.Add(MODGSYB.dbnumesy(MODGPLI1.Vplis[i].CodOci));
                                parameters.Add(MODGSYB.dbnumesy(MODGPLI1.Vplis[i].TipPln));
                                parameters.Add(MODGSYB.dbcharSy(MODGPLI1.Vplis[i].codcom));
                                parameters.Add(MODGSYB.dbcharSy(MODGPLI1.Vplis[i].Concep));
                                //-----------------------------------------------
                                parameters.Add(MODGSYB.dbcharSy(MODGPLI1.Vplis[i].AnuNum));
                                parameters.Add(MODGSYB.dbdatesy(MODGPLI1.Vplis[i].AnuFec));
                                parameters.Add(MODGSYB.dbnumesy(MODGPLI1.Vplis[i].AnuPbc));
                                parameters.Add(MODGSYB.dbcharSy(MODGPLI1.Vplis[i].ApcTip));
                                parameters.Add(MODGSYB.dbcharSy(MODGPLI1.Vplis[i].ApcNum));
                                parameters.Add(MODGSYB.dbdatesy(MODGPLI1.Vplis[i].ApcFec));
                                parameters.Add(MODGSYB.dbnumesy(MODGPLI1.Vplis[i].ApcPbc));
                                parameters.Add(MODGSYB.dbcharSy(MODGPLI1.Vplis[i].Motivo));
                                parameters.Add(MODGSYB.dbnumesy(MODGPLI1.Vplis[i].NumAcu));
                                parameters.Add(MODGSYB.dbcharSy(MODGCHQ.SoloNumeros(MODGPLI1.Vplis[i].Desacu, 5)));
                                //-----------------------------------------------
                                parameters.Add(MODGSYB.dbnumesy(MODGPLI1.Vplis[i].codpai));
                                parameters.Add(MODGSYB.dbnumesy(MODGPLI1.Vplis[i].CodMnd));
                                parameters.Add(MODGSYB.dbnumesy(MODGPLI1.Vplis[i].CodMndBC));
                                parameters.Add(MODGSYB.dbmontoSy(MODGPLI1.Vplis[i].MtoOpe));
                                parameters.Add(MODGSYB.dbTCamSy(MODGPLI1.Vplis[i].Mtopar));
                                parameters.Add(MODGSYB.dbmontoSy(MODGPLI1.Vplis[i].MtoDol));
                                parameters.Add(MODGSYB.dbTCamSy(MODGPLI1.Vplis[i].TipCam));
                                parameters.Add(MODGSYB.dbmontoSy(MODGPLI1.Vplis[i].MtoNac));
                                //-----------------------------------------------
                                parameters.Add(MODGSYB.dbcharSy(MODGPLI1.Vplis[i].DieNum));
                                parameters.Add(MODGSYB.dbdatesy(MODGPLI1.Vplis[i].DieFec));
                                parameters.Add(MODGSYB.dbnumesy(MODGPLI1.Vplis[i].DiePbc));
                                //-----------------------------------------------
                                parameters.Add(MODGSYB.dbcharSy(MODGPLI1.Vplis[i].numdec));
                                parameters.Add(MODGSYB.dbdatesy(MODGPLI1.Vplis[i].FecDec));
                                parameters.Add(MODGSYB.dbnumesy(MODGPLI1.Vplis[i].CodAdn));
                                //-----------------------------------------------
                                parameters.Add(MODGSYB.dbdatesy(MODGPLI1.Vplis[i].FecDeb));
                                parameters.Add(MODGSYB.dbcharSy(MODGPLI1.Vplis[i].DocNac));
                                parameters.Add(MODGSYB.dbcharSy(MODGPLI1.Vplis[i].DocExt));
                                parameters.Add(MODGSYB.dbnumesy(MODGPLI1.Vplis[i].BcoExt));
                                //-----------------------------------------------
                                parameters.Add(MODGSYB.dbnumesy(MODGPLI1.Vplis[i].NumCre));
                                parameters.Add(MODGSYB.dbdatesy(MODGPLI1.Vplis[i].FecCre));
                                parameters.Add(MODGSYB.dbnumesy(MODGPLI1.Vplis[i].MndCre));
                                parameters.Add(MODGSYB.dbmontoSy(MODGPLI1.Vplis[i].MtoCre));
                                //-----------------------------------------------
                                parameters.Add(MODGSYB.dbcharSy(MODGPLI1.Vplis[i].CodAcu));
                                parameters.Add(MODGSYB.dbcharSy(MODGPLI1.Vplis[i].RegAcu));
                                parameters.Add(MODGSYB.dbcharSy(MODGPLI1.Vplis[i].RutAcu));
                                parameters.Add(MODGSYB.dbcharSy(MODGPLI1.Vplis[i].ObsPli));
                                parameters.Add(MODGSYB.dbcharSy(MODGPLI1.Vplis[i].CodEOR));
                                parameters.Add(MODGSYB.dbnumesy(MODGPLI1.Vplis[i].DatImp));
                                parameters.Add(MODGSYB.dbdatesy(MODGPLI1.Vplis[i].fecins));
                                parameters.Add(MODGSYB.dbcharSy(MODGPLI1.Vplis[i].NomFin));
                                parameters.Add(MODGSYB.dbdatesy(MODGPLI1.Vplis[i].VenOfi));
                                parameters.Add(MODGSYB.dbnumesy(MODGPLI1.Vplis[i].NumCon));
                                parameters.Add(MODGSYB.dbdatesy(MODGPLI1.Vplis[i].fecsus));
                                parameters.Add(MODGSYB.dbdatesy(MODGPLI1.Vplis[i].VenOd));
                                parameters.Add(MODGSYB.dbnumesy(MODGPLI1.Vplis[i].insuti));
                                parameters.Add(MODGSYB.dbnumesy(MODGPLI1.Vplis[i].partip));
                                parameters.Add(MODGSYB.dbnumesy(MODGPLI1.Vplis[i].arecon));
                                parameters.Add(MODGSYB.dblogisy(MODGPLI1.Vplis[i].ZonFra));
                                parameters.Add(MODGSYB.dbnumesy(MODGPLI1.Vplis[i].SecBen));
                                parameters.Add(MODGSYB.dbnumesy(MODGPLI1.Vplis[i].SecInv));
                                parameters.Add(MODGSYB.dbnumesy(MODGPLI1.Vplis[i].PrcPar));

                                //-----------------------------------------------
                                //Se ejecuta el Procedimiento Almacenado.
                                //-----------------------------------------------

                                int res = unit.SceRepository.EjecutarSP<int>("sce_pli_w03_MS", parameters.ToArray()).First();
                                if (res == 9)
                                {
                                    Mdi_Principal.MESSAGES.Add(new UI_Message()
                                    {
                                        Text = "Se ha producido un error al tratar de grabar la Planilla Invisible (Sce_Pli).",
                                        Type = TipoMensaje.Error,
                                    });
                                    ConError = (short)(true ? -1 : 0);
                                }

                            }
                            else
                            {
                                motivo = "Vplis[i].Status = T_MODGCVD.EstadoEli";
                            }
                        }
                    }

                    if (!grabaAlguna && motivo != string.Empty)
                    {
                        tracer.TraceInformation("No se invoca sce_pli_w03_MS, motivo: " + motivo);
                    }
                }
                catch (Exception e)
                {
                    tracer.TraceException("Alerta", e);

                    Mdi_Principal.MESSAGES.Add(new UI_Message()
                    {
                        Text = "Se ha producido un error al tratar de grabar la Planilla Invisible (Sce_Pli).",
                        Type = TipoMensaje.Error,
                    });

                    ConError = (short)(true ? -1 : 0);
                }

                if (~ConError != 0)
                {
                    _retValue = -1;
                }

                return _retValue;

            }
        }
    }
}
