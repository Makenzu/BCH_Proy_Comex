using BCH.Comex.Common.Tracing;
using BCH.Comex.Core.Entities.Sbcor;
using BCH.Comex.Core.Entities.Swift;
using BCH.Comex.Data.DAL.Sbcor;
using BCH.Comex.Data.DAL.Swift;
using DocumentFormat.OpenXml.Spreadsheet;
using SpreadsheetLight;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BCH.Comex.Core.BL.BancaMundial
{
    public class BancaMundialService
    {
        public const string LIMIT_EXCEEDED = "LIMIT_EXCEEDED";
        public const string MSG_ERROR = "Ha ocurrido un error, contacte al administrador: ";

        private UnitOfWorkSbcor unitOfWork;
        private UnitOfWorkSwift unitOfWorkSwift;
        public BancaMundialService()
        {
            this.unitOfWork = new UnitOfWorkSbcor();
            this.unitOfWorkSwift = new UnitOfWorkSwift();
        }

        public List<sce_bic> BuscarBancaMundial(string swift, string pais, string ciudad, string banco, string direccion, string postal)
        {
            return ListBancos(swift, pais, ciudad, banco, direccion, postal).
                            Select(i => new sce_bic
                            {
                                bic_swf = i.bic_swf + i.bic_sec,
                                bic_nom = i.bic_nom,
                                bic_ciu = i.bic_ciu,
                                bic_pai = i.bic_pai,
                                bic_dir = i.bic_dir,
                                bic_pos = i.bic_pos,
                                bic_des = i.bic_des,
                                bic_ala = i.bic_ala
                            }).ToList();
                   
        }

        public int CountBancos(string swift, string pais, string ciudad, string banco, string direccion, string postal)
        {
            return unitOfWork.BancoRepository.Count(swift, pais, ciudad, banco, direccion, postal);
        }

        public IEnumerable<sw_bancos> BuscarBancaMundialImprimir(IEnumerable<string> swifts)
        {
            var res = this.unitOfWorkSwift.BancoRepository.Get(x=>swifts.Contains(x.cod_banco.Trim()+x.branch.Trim()));
            //var res = this.unitOfWorkSwift.BancoRepository.Get(x => swifts.Contains(x.cod_banco+x.branch));
            return res;
        }

        public List<sce_bic> ListBancos(string swift, string pais, string ciudad, string banco, string direccion, string postal)
        {
            return unitOfWork.BancoRepository.List(swift, pais, ciudad, banco, direccion, postal);
        }

        public List<sbc_cpai> ListPaises() 
        {
            return unitOfWork.PaisRepository.GetAll().OrderBy(p => p.cpai_nompai).ToList();
        }

        public MemoryStream ExcelBancos(string swift, string pais, string ciudad, string banco, string direccion, string postal)
        {
            using (Tracer tracer = new Tracer("ExcelBancos"))
            {
                MemoryStream stream = new MemoryStream();
                try
                {
                    using (SLDocument doc = new SLDocument())
                    {
                        doc.SelectWorksheet(SLDocument.DefaultFirstSheetName);

                        List<sce_bic> bancos = BuscarBancaMundial(swift, pais, ciudad, banco, direccion, postal);

                        //titulo
                        SLStyle styleTitulo = doc.CreateStyle();
                        styleTitulo.Font.Bold = true;
                        styleTitulo.Font.FontSize = 15;
                        styleTitulo.SetHorizontalAlignment(HorizontalAlignmentValues.Center);

                        doc.SetCellValue(1, 1, "Consulta de Banca Mundial");
                        doc.MergeWorksheetCells("A1", "G1");
                        doc.SetCellStyle("A1", styleTitulo);
                        
                        //filtros
                        doc.SetCellValue(3, 1, "Swift o Flia.");
                        doc.SetCellValue(4, 1, "País");
                        doc.SetCellValue(5, 1, "Ciudad");
                        doc.SetCellValue(3, 4, "Banco");
                        doc.SetCellValue(4, 4, "Dirección");
                        doc.SetCellValue(5, 4, "Postal");

                        doc.SetCellValue(3, 2, (String.IsNullOrEmpty(swift) ? "-- Todos --" : swift));
                        doc.SetCellValue(4, 2, (String.IsNullOrEmpty(pais) ? "-- Todos --" : pais));
                        doc.SetCellValue(5, 2, (String.IsNullOrEmpty(ciudad) ? "-- Todos --" : ciudad));
                        doc.SetCellValue(3, 5, (String.IsNullOrEmpty(banco) ? "-- Todos --" : banco));
                        doc.SetCellValue(4, 5, (String.IsNullOrEmpty(direccion) ? "-- Todos --" : direccion));
                        doc.SetCellValue(5, 5, (String.IsNullOrEmpty(postal) ? "-- Todos --" : postal));

                        //Style encabezados o filtros
                        SLStyle styleEncabezados = doc.CreateStyle();
                        styleEncabezados.Font.Bold = true;
                        styleEncabezados.Font.FontSize = 13;
                        doc.SetCellStyle("A3", "A5", styleEncabezados);
                        doc.SetCellStyle("D3", "D5", styleEncabezados);
                        doc.SetRowStyle(7, styleEncabezados);
                                                
                        int colIndex = 1;
                        int rowIndex = 7;
                        doc.SetCellValue(rowIndex, colIndex++, "Swift");
                        doc.SetCellValue(rowIndex, colIndex++, "Nombre");
                        doc.SetCellValue(rowIndex, colIndex++, "Ciudad");
                        doc.SetCellValue(rowIndex, colIndex++, "País");
                        doc.SetCellValue(rowIndex, colIndex++, "Dirección");
                        doc.SetCellValue(rowIndex, colIndex++, "Cod. Postal");
                        doc.SetCellValue(rowIndex, colIndex++, "Aladi");
                        
                        foreach(sce_bic b in bancos)
                        {
                            colIndex = 1;
                            rowIndex++;

                            doc.SetCellValue(rowIndex, colIndex++, (String.IsNullOrEmpty(b.bic_swf) ? String.Empty : b.bic_swf.Trim()));
                            doc.SetCellValue(rowIndex, colIndex++, (String.IsNullOrEmpty(b.bic_nom) ? String.Empty : b.bic_nom.Trim()));
                            doc.SetCellValue(rowIndex, colIndex++, (String.IsNullOrEmpty(b.bic_ciu) ? String.Empty : b.bic_ciu.Trim()));
                            doc.SetCellValue(rowIndex, colIndex++, (String.IsNullOrEmpty(b.bic_pai) ? String.Empty : b.bic_pai.Trim()));
                            doc.SetCellValue(rowIndex, colIndex++, (String.IsNullOrEmpty(b.bic_dir) ? String.Empty : b.bic_dir.Trim()));
                            doc.SetCellValue(rowIndex, colIndex++, (String.IsNullOrEmpty(b.bic_cod) ? String.Empty : b.bic_cod.Trim()));
                            doc.SetCellValue(rowIndex, colIndex++, (String.IsNullOrEmpty(b.bic_ala) ? String.Empty : b.bic_ala.Trim()));
                        }

                        doc.AutoFitColumn(1, 7);

                        doc.SaveAs(stream);
                    }

                    stream.Position = 0; //importante, dejar el stream pronto para leer;
                }
                catch (Exception ex)
                {
                    tracer.TraceException("Alerta, no se ha podido generar archivo excel de consulta devengamiento reajuste", ex);
                }
                return stream;
            }
        }

    }
}
