using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetAspNetCoreDepInj.Services
{
    public class DateService
    {
        public string GetDate { 
            get {
                return DateTime.Now.ToString("yyyy-MM-dd");
            }
        }
    }
}
