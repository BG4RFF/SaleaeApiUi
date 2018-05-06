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
            if (View == null)
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
                if (StatTxtLines.Count > 1024)
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


        //========================================================================================== GetConnDevCmd
        private BasicCommand _GetConnDevCmd;
        public BasicCommand GetConnDevCmd
        {
            get
            {
                if (_GetConnDevCmd == null)
                {
                    _GetConnDevCmd = new BasicCommand { ExecuteAction = (x) => GetConnDev(), CanExecuteFunc = ((obj) => { return true; }) };
                }
                return _GetConnDevCmd;
            }
        }

        private void GetConnDev()
        {
            try
            {
                var devices = Client.GetConnectedDevices();

                var types = new List<string>();
                var names = new List<string>();
                var ids = new List<string>();
                var indx = new List<string>();
                var active = new List<string>();

                types.Add("Device Type");
                names.Add("Name");
                ids.Add("Device Id");
                indx.Add("Index");
                active.Add("Is Active");

                foreach( var x in devices )
                {
                    types.Add(x.DeviceType.ToString());
                    names.Add(x.Name);
                    ids.Add(x.DeviceId.ToString());
                    indx.Add(x.Index.ToString());
                    active.Add(x.IsActive.ToString());
                }

                var widTypes = (from x in types select x.Length).Max() + 2;
                var widNames = (from x in names select x.Length).Max() + 2;
                var widIds = (from x in ids select x.Length).Max() + 2;
                var widIndex = (from x in indx select x.Length).Max() + 2;
                var widActive = (from x in active select x.Length).Max() + 2;

                var newTxt = new List<string>();
                var sb = new StringBuilder();
                for( int i=0; i < types.Count; i++ )
                {
                    sb.Append(types[i].PadRight(widTypes));
                    sb.Append(names[i].PadRight(widNames));
                    sb.Append(ids[i].PadRight(widIds));
                    sb.Append(indx[i].PadRight(widIndex));
                    sb.Append(active[i].PadRight(widActive));
                    newTxt.Add(sb.ToString());
                    sb.Clear();
                }

                AddStatTxtLine(newTxt);
            }
            catch (Exception ex)
            {
                AddStatTxtLine("Get Devices FAILED: " + ex.Message);
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
    }
}
