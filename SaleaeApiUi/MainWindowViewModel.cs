using Saleae.SocketApi;
using SaleaeApiUi.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SaleaeApiUi
{
    class MainWindowViewModel : ViewModelBase
    {
        MainWindow View;


        internal void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
            if( View == null )
            {
                View = sender as MainWindow;
            }
        }

        //========================================================================================== StatTxt
        private List<string> StatTxtLines = new List<string>();
        public string StatTxt
        {
            get
            {
                if( StatTxtLines.Count > 1024 )
                {
                    var removeCount = StatTxtLines.Count - 1024;
                    StatTxtLines.RemoveRange(0, removeCount);
                }
                return string.Join($"{Environment.NewLine}", StatTxtLines.ToArray());
            }
        }

        private void AddStatTxtLine(IEnumerable<string> lines, bool prefixDivider = true)
        {
            if (prefixDivider) { StatTxtLines.Add("========================================"); }
            StatTxtLines.AddRange(lines);
            OnPropertyChanged(nameof(StatTxt));
        }

        private void AddStatTxtLine(string line, bool prefixDivider = true)
        {
            if (prefixDivider) { StatTxtLines.Add("========================================"); }
            StatTxtLines.Add(line);
            OnPropertyChanged(nameof(StatTxt));
        }

        //========================================================================================== ConnectCmd

        SaleaeClient Client;

        private string _Host = "127.0.0.1";
        public string Host
        {
            get { return _Host; }
            set { _Host = value; }
        }

        private int _Port = 10429;
        public int Port
        {
            get { return _Port; }
            set { _Port = value; }
        }

        //ConnectCmd
        private BasicCommand _ConnectCmd;
        public BasicCommand ConnectCmd
        {
            get
            {
                if (_ConnectCmd == null)
                {
                    _ConnectCmd = new BasicCommand { ExecuteAction = (x) => Connect(), CanExecuteFunc = ((obj) => { return true; }) };
                }
                return _ConnectCmd;
            }
        }

        private void Connect()
        {
            try
            {
                var newClient = new SaleaeClient(Host, Port);
                Client = newClient;
                AddStatTxtLine("Connection Established");
            }
            catch (Exception ex)
            {
                AddStatTxtLine("Connection FAILED: " + ex.Message);
            }
        }


        //========================================================================================== 
        //========================================================================================== 
        //========================================================================================== 
        //========================================================================================== 
        //========================================================================================== 
        //========================================================================================== 
        //========================================================================================== 
        //========================================================================================== 
        //========================================================================================== 
        //========================================================================================== 
    }
}
