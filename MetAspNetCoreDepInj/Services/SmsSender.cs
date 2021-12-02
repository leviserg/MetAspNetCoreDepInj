namespace MetAspNetCoreDepInj.Services
{
    public class SmsSender : IMessageSender
    {
        public string Send()
        {
            return "Send by Sms";
        }
    }
}
