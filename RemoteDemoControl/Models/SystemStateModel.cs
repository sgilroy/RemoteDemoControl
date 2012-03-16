using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RemoteDemoControlLib;

namespace RemoteDemoControl.Models
{
    public class SystemStateModel
    {
        internal static dynamic GetSystemState()
        {
            if (WorkstationLockedUtil.IsWorkstationLocked())
            {
                return "Workstation is locked. Please unlock workstation in order to control the system.";
            }
            else
            {
                return "";
            }
        }
    }
}