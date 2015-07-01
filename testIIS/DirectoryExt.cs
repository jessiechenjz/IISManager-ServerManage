using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Security.AccessControl;

namespace testIIS
{
    public static class DirectoryExt
    {
        public static bool CreateIISDirectory(this DirectoryInfo dirInfo)
        {
            try
            {
                DirectorySecurity security = new DirectorySecurity();
                security.AddAccessRule(new FileSystemAccessRule("Everyone", FileSystemRights.FullControl, InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit, PropagationFlags.None, AccessControlType.Allow));

                if (!dirInfo.Exists)
                {
                    dirInfo.Create();
                }

                // 设置Everyone权限
                dirInfo.SetAccessControl(security);
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public static void EmptyDirectory(this DirectoryInfo dir, bool deleteSelf)
        {
            FileInfo[] fs = dir.GetFiles();
            foreach (FileInfo f in fs)
            {
                f.Attributes = FileAttributes.Normal;
                f.Delete();
            }

            DirectoryInfo[] subDirs = dir.GetDirectories();
            foreach (DirectoryInfo d in subDirs)
            {
                d.EmptyDirectory(true);
            }

            if (deleteSelf)
            {
                dir.Attributes = FileAttributes.Normal;
                dir.Delete();
            }
        }
    }
}
