using System.Net.NetworkInformation;

namespace AutoPPPoE
{
    public static class PingHelper
    {
        public static bool pingHost(string host, int timeout)
        {
            bool pingable = false;
            Ping ping = new Ping();
            try
            {
                PingReply reply = ping.Send(host, timeout, new byte[Constant.PING_BYTE]);
                pingable = reply.Status == IPStatus.Success;
            }
            catch
            {
            }
            return pingable;
        }
    }
}