using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetAspNetCoreDepInj.Services
{
    public class MessageService
    {
        IMessageSender _sender;
        public MessageService(IMessageSender sender) // dependency injection as service class constructor parameter
        {
            _sender = sender;
        }
        public string Send()
        {
            return _sender.Send();
        }
    }
}
