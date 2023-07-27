using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.Entities.Cext01;
using BCH.Comex.Core.Entities.Cext01.MT300Common;
using BCH.Comex.Data.DAL.Cext01;
using BCH.Comex.Data.DAL.Swift;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace BCH.Comex.UI.Web.Areas.MT300Planilla.Models
{
    public class DetalleMensajeViewModel
    {
        public bool EsModificacion { get; set; }
        public bool EsNuevo { get; set; }
        public MensajeViewModel Mensaje { get; set; }
        public IList<UI_Message> ListaMensajes { get; set; }
        public DetalleMensajeViewModel()
        {
            ListaMensajes = new List<UI_Message>();
            EsNuevo = false;
            EsModificacion = false;
        }
    }

    public class MensajeViewModel
    {
        public decimal idArchivo { get; set; }
        public decimal idDetalle { get; set; }
        [Required(ErrorMessage = "Debe ingresar campo Safekeeping")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Campo safekeeping debe ser númerico")]
        [ValidaCustodia(ErrorMessage = "Cliente no pertenece a registros válidos")]
        public decimal safekeeping { get; set; }
        [Required(ErrorMessage = "Debe ingresar campo reference")]
        [ValidaNackReference(ErrorMessage = "Campo reference contiene caracteres inválidos")]
        [ValidaMT300Previo(ErrorMessage = "Registro ya tiene MT300 previo. Favor validar reference")]
        public string reference { get; set; }
        [Required(ErrorMessage = "Debe ingresar campo Booked by")]
        [ValidaFechayyyymmdd(ErrorMessage = "Formato fecha campo bookedBy no es válido")]
        public string bookedBy { get; set; }
        [Required(ErrorMessage = "Debe ingresar campo Value date")]
        [ValidaFechayyyymmdd(ErrorMessage = "Formato fecha campo Value Date no es válido")]
        public string valueDate { get; set; }
        [Required(ErrorMessage = "Debe ingresar campo rate")]
        [ValidaDecimal(ErrorMessage = "Debe ingresar un monto válido distinto de cero")]
        public decimal rate { get; set; }
        [Required(ErrorMessage = "Debe ingresar campo Monto Moneda Nacional")]
        [ValidaDecimal(ErrorMessage = "Debe ingresar un monto válido distinto de cero")]
        public string campo32B { get; set; }
        [Required(ErrorMessage = "Debe ingresar campo Monto Moneda Extranjera")]
        [ValidaDecimal(ErrorMessage = "Debe ingresar un monto válido")]
        public string campo33B { get; set; }
        [Required(ErrorMessage = "Debe ingresar campo Codigo Moneda Nacional")]
        [ValidaCodMonedaNacional(ErrorMessage = "Codigo de Moneda Nacional debe ser 'CLP'")]
        public string codMonedamn { get; set; }
        [Required(ErrorMessage = "Debe ingresar campo Codigo Moneda Extranjera")]
        [ValidaCodMoneda(ErrorMessage = "Codigo de Moneda no reconocido (formato ISO 4217​)")]
        public string codMonedame { get; set; }
        public IList<string> mensajes { get; set; }

        public string estado { get; set; }
        public string executionTimehhmmss { get; set; }
    }

    public class ValidaNackReference : ValidationAttribute
    {
        public override bool IsValid(object value)
        {


            Regex regex = new Regex("[^A-Za-z0-9/\\-?:().,'+\n\r ]");
            MatchCollection invalidCharacters = regex.Matches(value.ToString());

            if (invalidCharacters.Count > 0)
            {
                string[] arr;
                arr = invalidCharacters.OfType<Match>()
                .Select(m => m.Groups[0].Value)
                .Distinct()
                .ToArray();

                string invalidCharsString = "'" + string.Join("', '", arr) + "'";

                return false;
            }

            return true;
        }
    }

    public class ValidaCustodia : ValidationAttribute
    {
        private readonly UnitOfWorkCext01 uow;

        public ValidaCustodia()
            {
                uow = new UnitOfWorkCext01();
            }
        public override bool IsValid(object value)
        {
            IList<Mt300Custodia> custodia = this.uow.Mt300CustodiaRepository.GetCustodia(value.ToString()).ToList();
            if (custodia.Count == 0 || custodia.First().ind_mt300 != "S" || custodia.First().tipo_mt300 != "B")
            {
                return false;
            }
            else
            {
                return true;
            }

        }
    }

    public class ValidaFechayyyymmdd : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime output;
            bool esValido = DateTime.TryParseExact(value.ToString(), "yyyyMMdd", null, DateTimeStyles.None, out output);

            if (value.ToString() == "19000101")
            {
                return false;
            }

            return esValido;
        }
    }

    public class ValidaMT300Previo : ValidationAttribute
    {
        private readonly UnitOfWorkCext01 uow;

        public ValidaMT300Previo()
        {
            uow = new UnitOfWorkCext01();
        }
        public override bool IsValid(object value)
        {

            bool Existe = this.uow.Mt300ArchivosProcesadosRepository.ExistsArchivoProcesado(value.ToString());

            return !Existe;
        }
    }


    public class ValidaDecimal : ValidationAttribute
{
    public override bool IsValid(object value)
    {

            if (!String.IsNullOrWhiteSpace(value.ToString()))
            {
                try
                {
                    decimal monto = Decimal.Parse(value.ToString(), CultureInfo.InvariantCulture);
                    if (monto <= 0)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                catch
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

            return true;

        }
    }

    public class ValidaCodMoneda : ValidationAttribute
    {
        private readonly UnitOfWorkSwift uow;

        public ValidaCodMoneda()
        {
            uow = new UnitOfWorkSwift();
        }
        public override bool IsValid(object value)
        {

            var registro = this.uow.MonedaRepository.Get(m => m.cod_moneda_sw == value.ToString() && m.uso_moneda_banco == "S").FirstOrDefault();
            if (registro == null)
            {
                return false;
            }
            else
            {
                return true;
            }

        }
    }

    public class ValidaCodMonedaNacional : ValidationAttribute
    {

        public ValidaCodMonedaNacional()
        {
        }
        public override bool IsValid(object value)
        {
            return value.ToString().Equals("CLP");
        }
    }

}