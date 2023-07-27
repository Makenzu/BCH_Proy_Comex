using BCH.Comex.Common.ExceptionHandling.Configuration;
using BCH.Comex.Common.Exceptions;
using BCH.Comex.Common.Tracing;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;

namespace BCH.Comex.Common.ExceptionHandling
{
    /// <summary>
    /// Clase encargada del manejo de excepciones segun políticas
    /// Autor: Microsoft Consulting Services
    /// Fecha de creación: 03/05/2015
    /// Fecha de modificación: 03/05/2015
    /// </summary>
    public static class ExceptionPolicy
    {
        #region fileds
        /// <summary>
        /// nombre de la sección de manejo de excepciones en el archivo de configuración
        /// </summary>
        private const string exceptionHandlingSectionName = "exceptionHandlingSection";
        private static List<PolicyConfiguration> policyConfigurationList;
        private static TraceSource traceSource;
        #endregion 

        #region ctor

        /// <summary>
        /// Type initializer
        /// </summary>
        static ExceptionPolicy()
        {
            LoadConfiguration();
        }

        #endregion ctor

        #region methods
         /// <summary>
        /// Punto de entrada principal del módulo de manejo de excepciones. 
        /// </summary>
        /// <param name="exception">El objeto de <see cref="Exception"/> a manejar</param>
        /// <param name="policyName">El nombre de la política con que se quiere manejar la excepción</param>
        /// <returns>Si se recomienda relanzar o no</returns>
        /// <example>
        /// El siguiente código muestra un ejemplo del uso del componente
        /// <code>
        /// try
        ///	{
        ///		Foo();
        ///	}
        ///	catch (Exception e)
        ///	{
        ///		if (ExceptionPolicy.HandleException(e)) throw;
        ///	}
        /// </code>
        /// </example>
        public static bool HandleException(Exception exception)
        {
            string policyName = GlobalConstants.ExceptionPolicyDefault;
            PolicyConfiguration policyConf = GetPolicyConfiguration(policyName);
            ExceptionPolicyConfiguration exceptionPolicy = null;
            if (policyConf != null)
            {
                exceptionPolicy = GetExceptionPolicyConfiguration(exception, policyConf);
                if (exceptionPolicy == null)
                    throw new ExceptionHandlingException(exception, string.Format(Messages.PolicyWithoutAssociatedType,
                        policyName, ExceptionTypeElement.GetTypeName(exception.GetType())));
            }
            else
            {
                exceptionPolicy = new ExceptionPolicyConfiguration()
                {
                    Action = HandlingAction.LogAndRethrow,
                    ExceptionType = typeof(System.Exception)
                };
            }
            return HandleException(exception, exceptionPolicy);
        }

        /// <summary>
        /// Punto de entrada principal del módulo de manejo de excepciones. 
        /// </summary>
        /// <param name="exception">El objeto de <see cref="Exception"/> a manejar</param>
        /// <param name="policyName">El nombre de la política con que se quiere manejar la excepción</param>
        /// <returns>Si se recomienda relanzar o no</returns>
        /// <example>
        /// El siguiente código muestra un ejemplo del uso del componente
        /// <code>
        /// try
        ///	{
        ///		Foo();
        ///	}
        ///	catch (Exception e)
        ///	{
        ///		if (ExceptionPolicy.HandleException(e, name)) throw;
        ///	}
        /// </code>
        /// </example>
        public static bool HandleException(Exception exception, string policyName)
        {
            PolicyConfiguration policyConf = GetPolicyConfiguration(policyName);
            if (policyConf == null)
                throw new ExceptionHandlingException(exception, Messages.InexistentPolicyName + policyName);

            ExceptionPolicyConfiguration exceptionPolicy = GetExceptionPolicyConfiguration(exception, policyConf);
            if (exceptionPolicy == null)
                throw new ExceptionHandlingException(exception, string.Format(Messages.PolicyWithoutAssociatedType,
                    policyName, ExceptionTypeElement.GetTypeName(exception.GetType())));

            return HandleException(exception, exceptionPolicy);
        }

        /// <summary>
        /// Metodo para el manejo de excepciones. 
        /// </summary>
        /// <param name="exception">El objeto de <see cref="Exception"/> a manejar</param>
        /// <param name="exceptionPolicy">Configuración de la política con que se quiere manejar la excepción</param>
        /// <returns>Si se recomienda relanzar o no</returns>
        private static bool HandleException(Exception exception, ExceptionPolicyConfiguration exceptionPolicy)
        {
            switch (exceptionPolicy.Action)
            {
                case HandlingAction.None:
                    break;
                case HandlingAction.LogAndRethrow:
                    LogException(exception);
                    break;
                case HandlingAction.Rethrow:
                    break;
                case HandlingAction.LogAndWrap:
                    LogException(exception);
                    WrapException(exception, exceptionPolicy);
                    break;
                case HandlingAction.Wrap:
                    WrapException(exception, exceptionPolicy);
                    break;
                case HandlingAction.Replace:
                    ReplaceException(exception, exceptionPolicy);
                    break;
                default:
                    throw new ExceptionHandlingException(exception, Messages.InvalidAction);
            }

            return (exceptionPolicy.Action == HandlingAction.Rethrow || exceptionPolicy.Action == HandlingAction.LogAndRethrow);
        }

        /// <summary>
        /// Realiza el log de la excepción
        /// </summary>
        /// <param name="exception">Excepción a loguear</param>
        private static void LogException(Exception exception)
        {
            traceSource.TraceEvent(TraceEventType.Warning, (int)EventType.TraceWrite, exception.ToString());
        }

        /// <summary>
        /// Carga los valores de configuración de la política
        /// </summary>
        private static void LoadConfiguration()
        {
            policyConfigurationList = new List<PolicyConfiguration>();
            ExceptionHandlingSection section = null;
            try
            {
                section = ConfigurationManager.GetSection(exceptionHandlingSectionName) as ExceptionHandlingSection;
            }
            catch (ConfigurationErrorsException ceex)
            {
                throw new ExceptionHandlingException(Messages.ExceptionHandlingLoadError, ceex);
            }

            if (section == null)
                LoadConfigurationFromDefaultValues();
            else
                LoadConfigurationFromConfigurationSection(section);

            traceSource = new TraceSource(GlobalConstants.ExceptionTraceSourceDefault, SourceLevels.All);
        }

        /// <summary>
        /// Carga la configuración a partir de los valores por defecto
        /// </summary>
        private static void LoadConfigurationFromDefaultValues()
        {
            string defaultPolicyName = "BCHComex";
            string defaultExceptionTypeName = "Exception";

            PolicyConfiguration policyConf = new PolicyConfiguration()
            {
                Name = defaultPolicyName,
                ExceptionPolicyList = new List<ExceptionPolicyConfiguration>()
            };

            policyConf.ExceptionPolicyList.Add(new ExceptionPolicyConfiguration()
            {
                Name = defaultExceptionTypeName,
                Action = HandlingAction.Rethrow,
                ExceptionType = typeof(Exception)
            });

            ExceptionPolicy.policyConfigurationList.Add(policyConf);
        }

        /// <summary>
        /// Carga la configuración a partir de <paramref name="section"/>
        /// </summary>
        /// <param name="section">Sección de configuración del archivo de configuración</param>
        private static void LoadConfigurationFromConfigurationSection(
            ExceptionHandlingSection section)
        {
            foreach (ExceptionPolicyElement policyElement in section.PolicyCollection)
            {
                PolicyConfiguration policyConf = new PolicyConfiguration()
                {
                    Name = policyElement.Name,
                    ExceptionPolicyList = new List<ExceptionPolicyConfiguration>()
                };

                foreach (ExceptionTypeElement exTypeElement in policyElement.ExceptionTypeCollection)
                {
                    ExceptionPolicyConfiguration politicaExcepcion = new ExceptionPolicyConfiguration()
                    {
                        Name = exTypeElement.Name,
                        Action = exTypeElement.HandlingAction,
                        NewExceptionMessage = exTypeElement.NewExceptionMessage,
                        ExceptionType = exTypeElement.ExceptionType,
                        NewExceptionType = exTypeElement.NewExceptionType
                    };

                    policyConf.ExceptionPolicyList.Add(politicaExcepcion);
                }

                ExceptionPolicy.policyConfigurationList.Add(policyConf);
            }
        }

        /// <summary>
        /// Obtiene el <see cref="PolicyConfiguration"/> de la lista
        /// </summary>
        /// <param name="policyName">Nombre de la política a obtener</param>
        /// <returns>Política obtenida</returns>
        private static PolicyConfiguration GetPolicyConfiguration(string policyName)
        {
            PolicyConfiguration policyConf = policyConfigurationList.Find(
                cp => cp.Name == policyName);

            return policyConf;
        }

        /// <summary>
        /// Obtiene la configuración de la política para una excepción del tipo de 
        /// <paramref name="exception"/>
        /// </summary>
        /// <param name="exception">Excepción para la que se quieren los valore de configuración
        /// </param>
        /// <param name="policyConf">Valores de configuración de una política</param>
        /// <returns>Valores de configuración para <paramref name="exception"/></returns>
        private static ExceptionPolicyConfiguration GetExceptionPolicyConfiguration(
            Exception exception, PolicyConfiguration policyConf)
        {
            if (policyConf == null)
                throw new ExceptionHandlingException(exception, string.Format(Messages.ParameterCanBeNull, "policyConf"));
            if (exception == null)
                throw new ExceptionHandlingException(exception, string.Format(Messages.ParameterCanBeNull, "exception"));

            ExceptionPolicyConfiguration exPolicyConf = null;

            Type exceptionType = exception.GetType();
            while (exceptionType != typeof(object))
            {
                exPolicyConf = policyConf.ExceptionPolicyList.Find(
                    pe => pe.ExceptionType == exceptionType);

                if (exPolicyConf == null)
                {
                    exceptionType = exceptionType.BaseType;
                }
                else
                {
                    break;
                    // encontré la exceptionPolicyConfiguration asociada al parámetro exception
                }
            }

            return exPolicyConf;
        }

        /// <summary>
        /// Lanza la nueva excepción que remplaza a <paramref name="exception"/>
        /// </summary>
        /// <param name="exception">Excepción a remplazar</param>
        /// <param name="exceptionPolicyConf">Incormación de configuración que contiene el tipo 
        /// de la nueva excepción</param>
        internal static void ReplaceException(Exception exception, 
            ExceptionPolicyConfiguration exceptionPolicyConf)
        {
            Exception ex = Activator.CreateInstance(exceptionPolicyConf.NewExceptionType, 
                new object[] { exceptionPolicyConf.NewExceptionMessage }) as Exception;

            if (ex == null)
                throw new ExceptionHandlingException(Messages.TypeDoesNotInheritsFromException +
                    ExceptionTypeElement.GetTypeName(exceptionPolicyConf.NewExceptionType));

            throw ex;
        }

        /// <summary>
        /// Envuelve a <paramref name="originalEx"/> en una nueva excepción definida en 
        /// <paramref name="exceptionPolicyConf"/>
        /// </summary>
        /// <param name="originalEx">Excepción original</param>
        /// <param name="exceptionPolicyConf">Información de configuración</param>
        internal static void WrapException(Exception originalEx,
            ExceptionPolicyConfiguration exceptionPolicyConf)
        {
            Exception ex = Activator.CreateInstance(exceptionPolicyConf.NewExceptionType,
                new object[] { exceptionPolicyConf.NewExceptionMessage, originalEx }) as Exception;

            if (ex == null)
                throw new ExceptionHandlingException(Messages.TypeDoesNotInheritsFromException +
                    ExceptionTypeElement.GetTypeName(exceptionPolicyConf.NewExceptionType));

            // Lanzo la nueva excepción
            throw ex;
        }

        #endregion
    }
}
