using BCH.Comex.Common.Tracing;
using BCH.Comex.UI.Web.Common;
using System;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Linq;
using log4net.Config;
using System.IO;
using System.Configuration;
using System.Web.Http;

namespace BCH.Comex.UI.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ClientDataTypeModelValidatorProvider.ResourceClassKey = "BCH";
            DefaultModelBinder.ResourceClassKey = "BCH";
            ModelBinders.Binders.Add(typeof(decimal), new DecimalModelBinder());
            ModelBinders.Binders.Add(typeof(decimal?), new DecimalModelBinder());

            Application[ApplicationKeys.Comex.ComexVersionKey] = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();

            XmlConfigurator.Configure(new FileInfo(Server.MapPath("~/Web.config")));

            MvcHandler.DisableMvcResponseHeader = true;

            CleanUpLogFiles();
        }

        private string NOT_FIRST_REQUEST = "NotFirstRequest";
        protected void Session_Start(object sender, EventArgs e)
        {
            
            var cookies = HttpContext.Current.Request.Cookies;
            

            if (cookies.AllKeys.Contains(NOT_FIRST_REQUEST))
            {
                if (cookies.AllKeys.Contains("ASP.NET_SessionId"))
                {
                    HttpContext.Current.Response.Cookies.Clear();
                    cookies.Remove(NOT_FIRST_REQUEST);

                    HttpContext.Current.Response.StatusCode = 412;
                    HttpContext.Current.Response.StatusDescription = "Session Expired";
                    HttpContext.Current.Response.Redirect("~/Static/SessionExpired.html");
                }
            }
            else
            {
                HttpCookie cookie = new HttpCookie(NOT_FIRST_REQUEST);
                HttpContext.Current.Response.AppendCookie(cookie);
            }
            
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            using(var tracer = new Tracer("Global.asax - Application Error"))
	        {
                Exception ex = Server.GetLastError();
                if (ex != null)
                {
                    var builder = new StringBuilder();
                    builder.AppendLine(ex.ToString());
                    
                    while (ex.InnerException != null)
                    {
                        builder.AppendLine();
                        builder.AppendFormat("Inner exception: {0}", ex.InnerException.ToString());
                        builder.AppendLine();

                        ex = ex.InnerException;
                    }

                    tracer.TraceError("Alerta: {0}", builder.ToString());
                }
	        }
        }

        private void CleanUpLogFiles()
        {
            using (var tracer = new Tracer("Global.asax - CleanUp LogFiles"))
            {
                try
                {
                    bool enabled = false;
                    bool.TryParse(ConfigurationManager.AppSettings["LogCleaning.Enabled"], out enabled);

                    if (!enabled)
                    {
                        tracer.TraceInformation("Información: La limpieza del log está desactivada.");
                        return;
                    }

                    // obtenemos la configuración para poder limpiar los logs de WCF 
                    int eliminationRangeDays = Convert.ToInt32(ConfigurationManager.AppSettings["LogCleaning.EliminationRangeDays"]);
                    string LogPath = ConfigurationManager.AppSettings["LogCleaning.LogPath"] ?? string.Empty;
                    int maxFilesToDelete = Convert.ToInt32(ConfigurationManager.AppSettings["LogCleaning.maxFilesToDelete"]);
                    // en caso de que eliminationRangeDays sea 0, no se borra ningun log
                    if (eliminationRangeDays == 0)
                    {
                        tracer.TraceInformation("Información: No se llevó a cabo la limpieza de log debido a que no está correctamente configurada.");
                        return;
                    }

                    DateTime deleteFilesOlderThan = DateTime.Now.Date.AddDays(-1 * eliminationRangeDays);
                    LogFileCleanupTask logFileCleanUpTask = new LogFileCleanupTask();
                    logFileCleanUpTask.CleanUp(LogPath, deleteFilesOlderThan, maxFilesToDelete);
                }
                catch (Exception e)
                {
                    tracer.TraceException("Alerta: No se ha podido llevar a cabo la limpieza de los log", e);
                }
            }
        }
    }

}
