using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaleaeApiUi.Common
{
    public abstract class ProcessUtils
    {
        static public System.Diagnostics.Process OpenFile(string path)
        {
            return System.Diagnostics.Process.Start(path);
        }

        static public System.Diagnostics.Process ExplorerTo(string path)
        {
            string param;

            if (File.Exists(path))
            {
                param = "/select," + '\"' + path + '\"';
            }
            else if (Directory.Exists(path))
            {
                param = '\"' + path + '\"';
            }
            else
            {
                return null;
            }

            return System.Diagnostics.Process.Start("explorer", param);
        }

        static public System.Diagnostics.Process RunSelfWithParameters(string param)
        {
            string executable = System.Reflection.Assembly.GetEntryAssembly().Location;
            return System.Diagnostics.Process.Start(executable, param);
        }

        static public System.Diagnostics.Process RunNeighborExe(string exeFile, string param = null)
        {
            string dir = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
            string executable = Path.Combine(dir, exeFile);

            if( string.IsNullOrWhiteSpace(param) )
            {
                return System.Diagnostics.Process.Start(executable);
            }
            else
            {
                return System.Diagnostics.Process.Start(executable, param);
            }
        }




    }
}
