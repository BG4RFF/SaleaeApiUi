using System.Collections.Generic;
using System.ComponentModel;

namespace SaleaeApiUi.Common
{
    public class PropertyChangedNotifier : INotifyPropertyChanged
    {
        //================================================================================
        #region Stuff for INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }
        public void OnPropertyChanged(string propName)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propName));
        }
        public void OnPropertyChanged(IEnumerable<string> propName)
        {
            foreach( string p in propName )
            {
                OnPropertyChanged(p);
            }
        }
        public void OnPropertyChanged()
        {
            OnPropertyChanged(new PropertyChangedEventArgs(null));
        }
        #endregion
    }
}
