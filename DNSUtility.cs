using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

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
