using System;
using System.Net;

namespace HttpListenerProject
{
    class DNSUtility
    {
        [Obsolete]
        public static IPAddress getIPMachine()
        {
            String host = Dns.GetHostName();
            IPAddress ip = Dns.GetHostByName(host).AddressList[0];

            return ip;
        }

    }
}
