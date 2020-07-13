using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAN_XLV_MilosPeric.EventLogger
{
    internal class ActionEvent
    {
        public delegate void ActionPerformedEventHandler(object source, ActionEventArgs args);
        public event ActionPerformedEventHandler ActionPerformed;

        public void OnActionPerformed(string logMessage)
        {
            ActionPerformed?.Invoke(this, new ActionEventArgs() { LogMessage = logMessage });
        }
    }
}
