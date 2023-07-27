using BCH.Comex.Core.Entities.Portal;
using System;
using System.DirectoryServices.AccountManagement;
using System.Security.Principal;

namespace BCH.Comex.Core.BL.Portal.Users
{
    public class ComexUser
    {
        public string Name { get; private set; }
        
        public static ComexUser Create(IPrincipal user) {
            return new ComexUser((WindowsIdentity)user.Identity); 
        }

        internal UserIdentification User(string Name)
        {
            try
            {
                var domainName = System.Net.NetworkInformation.IPGlobalProperties.GetIPGlobalProperties().DomainName;
                if (string.IsNullOrEmpty(domainName))
                    return new UserIdentification()
                    {
                        FullName = Name,
                    };
                PrincipalContext domain = new PrincipalContext(ContextType.Domain, domainName);
                UserPrincipal userPrincipal = UserPrincipal.FindByIdentity(domain, IdentityType.SamAccountName, Name.Split('\\')[1]);
                string email = GetUserEMail(userPrincipal.DistinguishedName);

                return new UserIdentification()
                {
                    FullName = userPrincipal.Name,
                    DistinguishedName = userPrincipal.DistinguishedName,
                    EMail = email,
                };
            }
            catch (Exception)
            {
                return new UserIdentification()
                {
                    FullName = Name,
                };
            }
        }

        private ComexUser(WindowsIdentity windowsIdentity)
        {
            this.Name = windowsIdentity.Name;
            if(LazyIndetification == null || string.IsNullOrEmpty(LazyIndetification.Value.FullName))
                this.LazyIndetification = new Lazy<UserIdentification>(() =>
                {
                    return User(Name);
                });
        }

        /// <summary>
        /// Obtiene el correo del usuario desde AD
        /// </summary>
        /// <param name="distinguishedName"></param>
        /// <returns></returns>
        private string GetUserEMail(string distinguishedName)
        {
            string email = null;

            if (!string.IsNullOrEmpty(distinguishedName))
            {
                string adProperty = "mail";
                string adPath = "LDAP://" + distinguishedName;

                using (System.DirectoryServices.DirectoryEntry de = new System.DirectoryServices.DirectoryEntry(adPath))
                {
                    if (de.Properties[adProperty].Value != null)
                    {
                        email = de.Properties[adProperty].Value.ToString();
                    }
                }
            }

            return email;
        }

        Lazy<UserIdentification> LazyIndetification;

        public IDatosUsuario GetDatosUsuario() {
            var service = new PortalService();
            return service.GetDatosUsuario(this);
        }

        public string FullName {
            get {
                return LazyIndetification.Value.FullName;
            }
        }
        public string DistinguishedName
        {
            get
            {
                return LazyIndetification.Value.DistinguishedName;
            }
        }
        public string EMail
        {
            get
            {
                return LazyIndetification.Value.EMail;
            }
        }

    }
}
