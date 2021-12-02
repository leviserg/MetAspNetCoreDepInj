using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetAspNetCoreDepInj.Services
{
    public class TimeService
    {
        public string GetTime { 
            get {
                return DateTime.Now.ToString("HH:mm:ss");
            }
        }
    }
}
