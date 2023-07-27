//INICIO MODIFICACION CNC - ACCENTURE

namespace consultarFichaClienteFull
{
    using System;
    using System.ServiceModel.Channels;
    using System.ServiceModel.Dispatcher;
    using System.Xml;

    using Microsoft.Web.Services3.Security.Tokens;

    /// <summary>
    /// Custom ClientMessageInspector to modify the message before it is sent
    /// and include a Hashed Password Digest UsernameToken in accordance to 
    /// the WS-I Basic Profile Security profile. The item shall be added to the
    /// SOAP request headers.
    /// </summary>
    public class PasswordDigestMessageInspector : IClientMessageInspector
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PasswordDigestMessageInspector" /> class.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        public PasswordDigestMessageInspector(string username, string password)
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

        #region IClientMessageInspector Members

        /// <summary>
        /// The after receive reply.
        /// </summary>
        /// <param name="reply">The reply.</param>
        /// <param name="correlationState">The correlation state.</param>
        /// <exception cref="NotImplementedException">This method is not implemented.</exception>
        public void AfterReceiveReply(ref Message reply, object correlationState)
        {
            return;
        }

        /// <summary>
        /// The before send request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="channel">The channel.</param>
        /// <returns>
        /// The <see cref="object" />.
        /// </returns>
        public object BeforeSendRequest(ref Message request, System.ServiceModel.IClientChannel channel)
        {

            // Use the WSE 3.0 security token class
            UsernameToken token = new UsernameToken(this.Username, this.Password, PasswordOption.SendHashed);

            // Serialize the token to XML
            XmlDocument XD = new XmlDocument();
            XD.XmlResolver = null;
            XmlElement securityToken = token.GetXml(XD);

            // build the security header 
            MessageHeader securityHeader = MessageHeader.CreateHeader("Security", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd", securityToken, false);
            request.Headers.Add(securityHeader);

            // complete
            return Convert.DBNull;  
        }

        #endregion
    }
}
//FIN MODIFICACION CNC - ACCENTURE