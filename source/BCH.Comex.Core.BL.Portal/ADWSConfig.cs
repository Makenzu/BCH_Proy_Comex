using System.Configuration;

namespace BCH.Comex.Core.BL.Portal
{
    public class ADWSConfig
    {

        public string Url { get; set; }
        public string PrincipalDNFormat { get; set; }
        public string Server { get; set; }
        public string PartitionDN { get; set; }
        public string ResourseContextPartition { get; set; }
        public string ResourseContextServer { get; set; }
        public string Protocol { get; set; }
        public string Endpoint { get; set; }

        private ADWSConfig() { }

        private static ADWSConfig instance; 
        private static object lockObject = new object(); 

        public static ADWSConfig Get()
        {
            if (instance != null)
                return instance;

            lock (lockObject) {
                if (instance != null)
                    return instance;

                PortalService servicio = new PortalService();

                instance = new ADWSConfig()
                {
                    Url = servicio.BuscarParametro(ConfigNameUrl),
                    Server = servicio.BuscarParametro(ConfigNameServer),
                    PartitionDN = servicio.BuscarParametro(ConfigNamePartitionDN),
                    ResourseContextPartition = servicio.BuscarParametro(ConfigNameResourseContextPartition),
                    ResourseContextServer = servicio.BuscarParametro(ConfigNameResourseContextServer),
                    Protocol = servicio.BuscarParametro(ConfigNameProtocol),
                    Endpoint = servicio.BuscarParametro(ConfigNameEndpoint)
                };
                return instance;
            }

            
        }
        const string ConfigNameProtocol = "ADWS.Protocol";
        const string ConfigNameEndpoint = "ADWS.WSEndpoint";
        const string ConfigNameUrl = "ADWS.Url";
        const string ConfigNameServer = "ADWS.Server";
        const string ConfigNamePartitionDN = "ADWS.PartitionDN";
        const string ConfigNameResourseContextPartition = "ADWS.ResourseContextPartition";
        const string ConfigNameResourseContextServer = "ADWS.ResourseContextServer";

    }
}
