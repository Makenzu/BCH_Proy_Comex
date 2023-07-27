using System;

namespace BCH.Comex.Core.BL.XGPY.PrtyMod
{
    public class NivelAcceso
    {
        public Boolean nuevoParticipante { get; set; }
        public Boolean salvarParticipante { get; set; }
        public Boolean tasasEspeciales { get; set; }
        public Boolean activarRazonSocial { get; set; }
        public Boolean recuperarParticipante { get; set; }
        public Boolean eliminarParticipante { get; set; }
        public Boolean menuOpcionesParticipante { get; set; }
        public Boolean instruccionesEspeciales { get; set; }
        public Boolean caracteristicas { get; set; }
        public Boolean cuentas { get; set; }

        public NivelAcceso()
        {
            this.SoloLectura(true);
        }

        public void Habilitar(Boolean habilitar)
        {
            this.caracteristicas = habilitar;
            this.instruccionesEspeciales = habilitar;
            this.cuentas = habilitar;
            this.tasasEspeciales = habilitar;
            this.salvarParticipante = habilitar;
            this.menuOpcionesParticipante = habilitar;
        }

        public void SoloLectura(Boolean esSoloLectura)
        {
            this.nuevoParticipante = !esSoloLectura;
            this.salvarParticipante = !esSoloLectura;
            this.tasasEspeciales = !esSoloLectura;
            this.activarRazonSocial = !esSoloLectura;
            this.recuperarParticipante = !esSoloLectura;
            this.eliminarParticipante = !esSoloLectura;
            this.instruccionesEspeciales = !esSoloLectura;
            this.caracteristicas = !esSoloLectura;
            this.cuentas = !esSoloLectura;
            this.menuOpcionesParticipante = !esSoloLectura;
        }

        public void Eliminado(Boolean participanteEliminado)
        {
            this.salvarParticipante = !participanteEliminado;
        }

        public void Pertenece(Boolean perteneParticipante)
        {
            this.salvarParticipante = perteneParticipante;
            this.eliminarParticipante = perteneParticipante;
            this.activarRazonSocial = perteneParticipante;
        }
    }
}
