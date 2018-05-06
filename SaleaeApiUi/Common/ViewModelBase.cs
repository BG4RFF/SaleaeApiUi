using System;
using System.Windows.Media;

namespace SaleaeApiUi.Common
{
    public abstract class ViewModelBase : PropertyChangedNotifier
    {

        //================================================================================
        // null  => Transparent
        // false => Pink
        // true  => Green
        protected Brush TristateColorBrush(bool? b, Brush def= null)
        {
            def = def ?? Brushes.Transparent;
            return (!b.HasValue) ? (def) : ((b.Value) ? (Brushes.LightGreen) : (Brushes.LightPink));
        }


        public static string GetVersionString()
        {
            Version v = System.Reflection.Assembly.GetEntryAssembly().GetName().Version;
            string verStr = v.Major.ToString() + "." + v.Minor.ToString("D2");
            if (v.Build != 0)
            {
                verStr += " Beta" + v.Build.ToString("D4");// + ((v.Revision == 0) ? "" : "-" + v.Revision.ToString("D2"));
            }
            return verStr;
        }
    }
}
