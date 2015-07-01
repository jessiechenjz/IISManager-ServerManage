using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;

namespace testIIS
{
    public class ImpersonateUser
    {
        public const int LOGON32_LOGON_INTERACTIVE = 2;
        public const int LOGON32_PROVIDER_DEFAULT = 0;

        WindowsImpersonationContext impersonationContext;

        [DllImport("advapi32.dll", CharSet = CharSet.Auto)]
        public static extern int LogonUser(String lpszUserName,
                                          String lpszDomain,
                                          String lpszPassword,
                                          int dwLogonType,
                                          int dwLogonProvider,
                                          ref IntPtr phToken);
        [DllImport("advapi32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto, SetLastError = true)]
        public extern static int DuplicateToken(IntPtr hToken,
                                          int impersonationLevel,
                                          ref IntPtr hNewToken);

        public bool ImpersonateValidUser(String userName, String domain, String password)
        {
            IntPtr token = IntPtr.Zero;
            IntPtr tokenDuplicate = IntPtr.Zero;

            if (LogonUser(userName, domain, password, LOGON32_LOGON_INTERACTIVE,
                LOGON32_PROVIDER_DEFAULT, ref token) == 0) return false;

            if (DuplicateToken(token, 2, ref tokenDuplicate) == 0) return false;

            var tempWindowsIdentity = new WindowsIdentity(tokenDuplicate);
            impersonationContext = tempWindowsIdentity.Impersonate();

            return impersonationContext != null;
        }

        public void UndoImpersonation()
        {
            if (impersonationContext != null)
            {
                impersonationContext.Undo();
            }
        }

        public string GetDisplayName(string userName, string password)
        {
            try
            {
                string adPath = "IIS://localhost/W3SVC";
                DirectoryEntry entry = new DirectoryEntry(adPath, userName, password);
                DirectorySearcher search = new DirectorySearcher(entry);
                search.Filter = "(sAMAccountName=" + userName + ")";
                search.PropertiesToLoad.Add("displayName");
                SearchResult adUser = null;
                adUser = search.FindOne();
                string displayName = adUser.Properties["displayName"][0].ToString();
                return displayName;
            }
            catch
            {
                return userName;
            }
        }
    }
}
