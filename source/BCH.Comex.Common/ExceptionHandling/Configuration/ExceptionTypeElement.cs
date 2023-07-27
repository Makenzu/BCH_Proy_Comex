using BCH.Comex.Common.Exceptions;
using System;
using System.Configuration;

namespace BCH.Comex.Common.ExceptionHandling.Configuration
{
    /// <summary>
    /// Configuración para un tipo de excepción.
    /// Autor: Microsoft Consulting Services
    /// Fecha de creación: 03/05/2015
    /// Fecha de modificación: 03/05/2015
    /// </summary>
    public class ExceptionTypeElement : ConfigurationElement
    {
        #region fields
        private const string NAME_PROPERTY = "name";
        private const string HANDLING_ACTION_NAME_PROPERTY = "action";
        private const string EXCEPTION_TYPE_NAME_PROPERTY = "type";
        private const string NEW_EXCEPTION_TYPE_NAME_PROPERTY = "newExceptionType";
        private const string NEW_EXCEPTION_MESSAGE_PROPERTY = "newExceptionMessage";
        #endregion 

        #region ctor
        /// <summary>
        /// Constructor
        /// </summary>
        public ExceptionTypeElement()
        {

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Nombre del elemento</param>
        public ExceptionTypeElement(string name)
        {
            Name = name;
        }
        #endregion

        #region properties
        /// <summary>
        /// Nombre del tipo 
        /// </summary>
        [ConfigurationProperty(NAME_PROPERTY, IsKey=true)]
        public string Name
        {
            get
            {
                return (string)this[NAME_PROPERTY];
            }
            set
            {
                this[NAME_PROPERTY] = value;
            }
        }

        /// <summary>
        /// Nombre de la acción
        /// </summary>
        [ConfigurationProperty(HANDLING_ACTION_NAME_PROPERTY, IsRequired = true)]
        public string HandlingActionName
        {
            get
            {
                return (string)this[HANDLING_ACTION_NAME_PROPERTY];
            }
            set
            {
                this[HANDLING_ACTION_NAME_PROPERTY] = value;
            }
        }

        /// <summary>
        /// Mensaje en la nueva excepción
        /// </summary>
        [ConfigurationProperty(NEW_EXCEPTION_MESSAGE_PROPERTY, DefaultValue=null)]
        public string NewExceptionMessage
        {
            get
            {
                return (string)this[NEW_EXCEPTION_MESSAGE_PROPERTY];
            }
            set
            {
                this[NEW_EXCEPTION_MESSAGE_PROPERTY] = value;
            }
        }

        /// <summary>
        /// Tipo de la nueva excepción
        /// </summary>
        [ConfigurationProperty(EXCEPTION_TYPE_NAME_PROPERTY, IsRequired=true)]
        public string ExceptionTypeName
        {
            get
            {
                return (string)this[EXCEPTION_TYPE_NAME_PROPERTY];
            }
            set
            {
                this[EXCEPTION_TYPE_NAME_PROPERTY] = value;
            }
        }

        /// <summary>
        /// Tipo de la nueva excepción
        /// </summary>
        [ConfigurationProperty(NEW_EXCEPTION_TYPE_NAME_PROPERTY)]
        public string NewExceptionTypeName
        {
            get
            {
                return (string)this[NEW_EXCEPTION_TYPE_NAME_PROPERTY];
            }
            set
            {
                this[NEW_EXCEPTION_TYPE_NAME_PROPERTY] = value;
            }
        }

        /// <summary>
        /// Acción asociada a la política
        /// </summary>
        public HandlingAction HandlingAction
        {
            get
            {
                return ToHandlingAction(HandlingActionName);
            }
            set
            {
                HandlingActionName = value.ToString();
            }
        }

        /// <summary>
        /// Tipo de excepción a capturar
        /// </summary>
        public Type ExceptionType
        {
            get
            {
                return ToType(ExceptionTypeName);
            }
            set
            {
                ExceptionTypeName = GetTypeName(value);
            }
        }

        /// <summary>
        /// Tipo de la nueva Excepción
        /// </summary>
        public Type NewExceptionType
        {
            get
            {
                Type t = null;
                if (!string.IsNullOrEmpty(NewExceptionTypeName))
                    t = ExceptionTypeElement.ToType(NewExceptionTypeName);

                return t;
            }
            set
            {
                string typeName = null;
                if (value != null)
                    typeName = ExceptionTypeElement.GetTypeName(value);

                NewExceptionTypeName = typeName;
            }
        }
        #endregion

        #region methods
        /// <summary>
        /// Obtiene el HandlingAction a partir de su nombre
        /// </summary>
        /// <param name="handlingActionName">Nombre de la acción de manejo de excepciones</param>
        /// <returns>
        /// Valor del enumerado <see cref="HandlingAction"/> de la acción 
        /// de manejo de excepciones
        /// </returns>
        internal static HandlingAction ToHandlingAction(string handlingActionName)
        {
            if (string.IsNullOrEmpty(handlingActionName))
                throw new ExceptionHandlingException(Messages.UndefinedKeyOrInexistentValue);

            try
            {
                return (HandlingAction)Enum.Parse(typeof(HandlingAction), handlingActionName);
            }
            catch (ArgumentException ae)
            {
                throw new ExceptionHandlingException(string.Format(
                    Messages.UndefinedKeyOrInexistentValue,
                    handlingActionName), ae);
            }
        }


        /// <summary>
        /// Obtiene un Type a partir de su nombre
        /// </summary>
        /// <param name="typeName">Nombre del tipo</param>
        /// <returns>Tipo</returns>
        internal static Type ToType(string typeName)
        {
            Type t = Type.GetType(typeName, false);
            if (t == null)
                throw new ExceptionHandlingException(string.Format(Messages.InvalidType, typeName));

            return t;
        }

        /// <summary>
        /// Obtiene el nombre de un Type
        /// </summary>
        /// <param name="type">Tipo a obtener el nombre</param>
        /// <returns>Nombre del tipo</returns>
        internal static string GetTypeName(Type type)
        {
            if (type == null)
                throw new ExceptionHandlingException(Messages.EmptyType);

            return type.AssemblyQualifiedName;
        }
        #endregion
    }
}
