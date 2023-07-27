using BCH.Comex.Core.Entities.Cext01.FundTransfer.DataTypes;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace BCH.Comex.UI.Web.Models.FundTransfer
{
    public class ParticipantesCrearViewModel : FundTransferViewModel
    {
        public int NuestroPais { get; set; }
        public string Caption { get; set; }
        public bool CargaAutomatica { get; set; }
        public string ClaveOperacion { get; set; }
        public string Rut { get; set; }
        public bool EsBanco { get; set; }
        public string RazonSocial { get; set; }
        public string Direccion { get; set; }
        public string CodPostal { get; set; }
        public string Localidad { get; set; }
        public string Region { get; set; }
        public string Ciudad { get; set; }
        public string Fax { get; set; }
        public int Pais { get; set; }
        public List<SelectListItem> ListaPais { get; set; }
        public string OtroPais { get; set; }
        public string CasillaBanco { get; set; }
        public string CasillaPostal { get; set; }
        public string Telefono { get; set; }
        public string Telex { get; set; }
        public int EnvioCorrespondencia { get; set; }

        public ParticipantesCrearViewModel()
        {
        }

        public ParticipantesCrearViewModel(InitializationObject initObj)
        {
            MensajesDeError = initObj.Mdi_Principal.MESSAGES;

            ListaPais = initObj.Frm_Crea_Participante.Pais.Items.Select(x => new SelectListItem {
                Text = x.Value, 
                Value = x.Data.ToString() }).ToList();

            if (initObj.Frm_Crea_Participante.Pais.ListIndex > 0)
                ListaPais[initObj.Frm_Crea_Participante.Pais.ListIndex].Selected = true;
            //Pais = initObj.Frm_Crea_Participante.Pais.ListIndex;
            //OtroPais = ;

            CargaAutomatica = (initObj.Mdl_Funciones_Varias.CARGA_AUTOMATICA == 1);

            NuestroPais = initObj.Frm_Crea_Participante.nuestro_pais;
            Caption = initObj.Frm_Crea_Participante.Caption;
            Rut = initObj.Frm_Crea_Participante.rut.Text;
            RazonSocial = initObj.Frm_Crea_Participante.Nombre.Text;
            Direccion = initObj.Frm_Crea_Participante.Direccion.Text;
            CodPostal = initObj.Frm_Crea_Participante.cas_postal.Text;
            Localidad = initObj.Frm_Crea_Participante.comuna.Text;
            Region = initObj.Frm_Crea_Participante.Estado.Text;
            Ciudad = initObj.Frm_Crea_Participante.Ciudad.Text;
            Fax = initObj.Frm_Crea_Participante.Fax.Text;
            CasillaBanco = initObj.Frm_Crea_Participante.cas_bco.Text;
            CasillaPostal = initObj.Frm_Crea_Participante.cas_postal.Text;
            Telefono = initObj.Frm_Crea_Participante.Telefono.Text;
            Telex = initObj.Frm_Crea_Participante.Telex.Text;
            EnvioCorrespondencia = initObj.Frm_Crea_Participante.envio.FindIndex(x => x.Selected);
        }

        public void Update(InitializationObject initObj)
        {
            initObj.Frm_Crea_Participante.Pais.ListIndex = 
                initObj.Frm_Crea_Participante.Pais.Items.FindIndex(x => x.Data == Pais);

            initObj.Frm_Crea_Participante.Caption = Caption;
            initObj.Frm_Crea_Participante.rut.Text = Rut == null ? string.Empty : Rut
                .Replace("-", "")
                .Replace("_", "")
                .Replace(".", "");
            initObj.Frm_Crea_Participante.Nombre.Text = RazonSocial;
            initObj.Frm_Crea_Participante.Direccion.Text = Direccion;
            initObj.Frm_Crea_Participante.cas_postal.Text = CodPostal;
            initObj.Frm_Crea_Participante.comuna.Text = Localidad;
            initObj.Frm_Crea_Participante.Estado.Text = Region;
            initObj.Frm_Crea_Participante.Ciudad.Text = Ciudad;
            initObj.Frm_Crea_Participante.cas_bco.Text = CasillaBanco;
            initObj.Frm_Crea_Participante.cas_postal.Text = CasillaPostal;
            initObj.Frm_Crea_Participante.Fax.Text = Fax == null ? string.Empty : Fax
                .Replace("(", "")
                .Replace(")", "")
                .Replace("_", "")
                .Replace("-", "");
            initObj.Frm_Crea_Participante.Telefono.Text = Telefono == null ? string.Empty : Telefono
                .Replace("(", "")
                .Replace(")", "")
                .Replace("_", "")
                .Replace("-", "");
            initObj.Frm_Crea_Participante.Telex.Text = Telex;
            int i = 0;
            initObj.Frm_Crea_Participante.envio.ForEach(x => x.Selected = i++ == EnvioCorrespondencia);
        }
    }

}