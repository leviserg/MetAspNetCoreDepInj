using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetAspNetCoreDepInj.Services
{
    public class EmailSender : IMessageSender
    {
        public string Send()
        {
            return "Send by Email";
        }
    }
}
