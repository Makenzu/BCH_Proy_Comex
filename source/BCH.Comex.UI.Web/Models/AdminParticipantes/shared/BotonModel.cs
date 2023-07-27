
namespace BCH.Comex.UI.Web.Models.AdminParticipantes.shared
{
    public class BotonModel
    {
        public BotonModel(string img, string id, string url, bool hab)
        {
            this.imagen = img;
            this.id = id;
            this.url = url;
            this.habilitado = hab;
        }

        public string imagen { set; get; }
        public string id { set; get; }
        public string url { set; get; }
        public bool habilitado { set; get; }
    }
}