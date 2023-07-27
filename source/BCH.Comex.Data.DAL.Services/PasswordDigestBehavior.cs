//INICIO MODIFICACION CNC - ACCENTURE

namespace consultarFichaClienteFull
{
    using System;
    using System.ServiceModel.Description;

    /// <summary>
    /// Custom Endpoint Behavior to provide a client client behavior.
    /// </summary>
    public class PasswordDigestBehavior : IEndpointBehavior
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PasswordDigestBehavior"/> class.
        /// </summary>
        /// <param name="username">
        /// The username.
        /// </param>
        /// <param name="password">
        /// The password.
        /// </param>
        public PasswordDigestBehavior(string username, string password)
        {
            this.Username = username;
            this.Password = password;
        }

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        public string Password { get; set; }

        #region IEndpointBehavior Members

        /// <summary>
        /// The add binding parameters.
        /// </summary>
        /// <param name="endpoint">
        /// The endpoint.
        /// </param>
        /// <param name="bindingParameters">
        /// The binding parameters.
        /// </param>
        /// <exception cref="NotImplementedException">
        /// This method is not implemented.
        /// </exception>
        public void AddBindingParameters(ServiceEndpoint endpoint, System.ServiceModel.Channels.BindingParameterCollection bindingParameters)
        {
            return;
        }

        /// <summary>
        /// The apply client behavior.
        /// </summary>
        /// <param name="endpoint">
        /// The endpoint.
        /// </param>
        /// <param name="clientRuntime">
        /// The client runtime.
        /// </param>
        public void ApplyClientBehavior(ServiceEndpoint endpoint, System.ServiceModel.Dispatcher.ClientRuntime clientRuntime)
        {
            clientRuntime.MessageInspectors.Add(new PasswordDigestMessageInspector(this.Username, this.Password));
        }

        /// <summary>
        /// The apply dispatch behavior.
        /// </summary>
        /// <param name="endpoint">
        /// The endpoint.
        /// </param>
        /// <param name="endpointDispatcher">
        /// The endpoint dispatcher.
        /// </param>
        /// <exception cref="NotImplementedException">
        /// This method is not implemented.
        /// </exception>
        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, System.ServiceModel.Dispatcher.EndpointDispatcher endpointDispatcher)
        {
            return;
        }

        /// <summary>
        /// The validate.
        /// </summary>
        /// <param name="endpoint">
        /// The endpoint.
        /// </param>
        /// <exception cref="NotImplementedException">
        /// This method is not implemented.
        /// </exception>
        public void Validate(ServiceEndpoint endpoint)
        {
            return;
        }

        #endregion
    }
}
//FIN MODIFICACION CNC - ACCENTURE
