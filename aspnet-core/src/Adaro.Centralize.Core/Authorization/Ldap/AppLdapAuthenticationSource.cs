using Abp.Zero.Ldap.Authentication;
using Abp.Zero.Ldap.Configuration;
using Adaro.Centralize.Authorization.Users;
using Adaro.Centralize.MultiTenancy;

namespace Adaro.Centralize.Authorization.Ldap
{
    public class AppLdapAuthenticationSource : LdapAuthenticationSource<Tenant, User>
    {
        public AppLdapAuthenticationSource(ILdapSettings settings, IAbpZeroLdapModuleConfig ldapModuleConfig)
            : base(settings, ldapModuleConfig)
        {
        }
    }
}