using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaleaeApiUi.Common
{
    // Usage Example:
    // Periodic = new PeriodActionDelegated
    //     {
    //         DelegatedAction = PeriodicUpdate,
    //         Period = TimeSpan.FromSeconds(0.125),
    //     };
    // Periodic.Start();
    //
    public class PeriodActionDelegated : PeriodicAction
    {
        public Action<object> DelegatedAction { get; set; }

        bool IsBusy = false;
        object IsBusyLock = new object();

        public override void PeriodicFunction(object state)
        {
            bool busy;

            lock (IsBusyLock)
            {
                busy = IsBusy;
                IsBusy = true;
            }

            if (!busy)
            {
                DelegatedAction?.Invoke(state);
                IsBusy = false;
            }
        }


    }
}
