using BCH.Comex.Common.Caching;
using BCH.Comex.Core.BL.Portal.ActiveDirectory.Service;
using BCH.Comex.Core.BL.Portal.Users;
using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.ServiceModel;

namespace BCH.Comex.Core.BL.Portal
{
    public class UserMembershipProvider
    {
        private static Lazy<ICacheManager> Cache { get; set; }
        static UserMembershipProvider()
        {
            Cache = new Lazy<ICacheManager>(() =>
            {
                return CacheFactory.GetCache("PolicyUserMembershipProvider");
            });
        }

        public List<string> GetGroups(ComexUser user, bool forceGet)
        {
            var newValue = new Lazy<List<string>>(() => GetGroupsInternal(user));

            if (forceGet)
            {
                Cache.Value.Set(user.Name, newValue);
                return newValue.Value;
            }
            else
            {
                // the line belows returns existing item or adds the new value if it doesn't exist
                var value = Cache.Value.GetOrAdd(user.Name, () => newValue);
                return (value ?? newValue).Value; // Lazy<T> handles the locking itself
            }
        }

        private List<string> GetGroupsInternalDefault(ComexUser user)
        {
            var config = ADWSConfig.Get();
            if (string.IsNullOrEmpty(config.Server))
                return null;

            NetTcpBinding tcpBind = new NetTcpBinding();
            AccountManagementClient acctMgmt = new AccountManagementClient(tcpBind, new EndpointAddress(config.Url));
            acctMgmt.ClientCredentials.Windows.AllowedImpersonationLevel = System.Security.Principal.TokenImpersonationLevel.Impersonation;
            //use process credentials
            string principalDN = user.DistinguishedName;
            var groups = acctMgmt.GetADPrincipalGroupMembership(config.Server, config.PartitionDN, principalDN, config.ResourseContextPartition, config.ResourseContextServer);
            return groups.Select(i => i.SamAccountName).ToList();
        }

        private List<string> GetGroupsInternal(ComexUser user)
        {
            try
            {
                var config = ADWSConfig.Get();
                if (string.IsNullOrEmpty(config.Server))
                    return null;
                NetTcpBinding tcpBind = new NetTcpBinding();
                using (var currentdomain = Domain.GetComputerDomain())
                {
                    DirectoryContext ctx = new DirectoryContext(DirectoryContextType.Domain, currentdomain.Name);
                    var domain = DomainController.FindOne(ctx);
                    //Se sobreescribe el valor de Url por el procotol + nombre de dominio más cercano encontrado + firma de WS
                    config.Url = config.Protocol + domain.Name + config.Endpoint;
                    AccountManagementClient acctMgmt = new AccountManagementClient(tcpBind, new EndpointAddress(config.Url));
                    acctMgmt.ClientCredentials.Windows.AllowedImpersonationLevel = System.Security.Principal.TokenImpersonationLevel.Impersonation;
                    //use process credentials
                    string principalDN = user.DistinguishedName;
                    //var groups = acctMgmt.GetADPrincipalGroupMembership(config.Server, config.PartitionDN, principalDN, config.ResourseContextPartition, config.ResourseContextServer);
                    var groups = acctMgmt.GetADPrincipalGroupMembership(config.Server, config.PartitionDN, principalDN, config.ResourseContextPartition, domain.Name);
                    return groups.Select(i => i.SamAccountName).ToList();
                }
            }
            catch
            {
                return GetGroupsInternalDefault(user);
            }
        }
    }
}