using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;

namespace SaleaeApiUi.Common
{
    public class BasicCommand : DispatcherObject, ICommand
    {
        public Func<object, bool> CanExecuteFunc { get; set; }
        public Action<object> ExecuteAction { get; set; }

        public void TriggerCanExecuteChanged(object s, EventArgs e)
        {
            Dispatcher.BeginInvoke(((Action)(() => { CanExecuteChangedEvent?.Invoke(s, e); })), null);
        }

        //==============================================================

        private event EventHandler CanExecuteChangedEvent;
        private object EventLock = new object();

        event EventHandler ICommand.CanExecuteChanged
        {
            add
            {
                lock(EventLock)
                {
                    CanExecuteChangedEvent += value;
                }
            }

            remove
            {
                lock (EventLock)
                {
                    CanExecuteChangedEvent -= value;
                }
            }
        }

        bool ICommand.CanExecute(object parameter)
        {
            bool result = (CanExecuteFunc == null)
                ? false
                : CanExecuteFunc(parameter);

            return result;
        }

        void ICommand.Execute(object parameter)
        {
            ExecuteAction?.Invoke(parameter);
        }
    }
}
