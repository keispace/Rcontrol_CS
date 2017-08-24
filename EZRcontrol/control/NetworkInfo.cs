using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;

namespace EZRcontrol
{
    public class NetworkInfo
    {
        //http://ehpub.co.kr/%EC%9B%90%EA%B2%A9-%EC%A0%9C%EC%96%B4-%ED%94%84%EB%A1%9C%EA%B7%B8%EB%9E%A8-14-%EC%9B%90%EA%B2%A9-%EC%A0%9C%EC%96%B4-%EC%BB%A8%ED%8A%B8%EB%A1%A4%EB%9F%AC/
        //https://stackoverflow.com/questions/1069103/how-to-get-the-ip-address-of-the-server-on-which-my-c-sharp-application-is-runni/10774826#10774826
        public static short ImgPort { get { return 20004; } }
        public static short SetupPort { get { return 20002; } }
        public static short EventPort { get { return 20010; } }

        public static string DefaultIP
        {
            get
            {
                //string host_name = Dns.GetHostName();
                //IPHostEntry host_entry = Dns.GetHostEntry(host_name);
                //foreach (IPAddress ipaddr in host_entry.AddressList)
                //    if (ipaddr.AddressFamily == AddressFamily.InterNetwork)
                //        return ipaddr.ToString(); //ip 주소 문자열 반환 
                //return string.Empty;


                var ipAddress = GetRealIpAddress();
                if (ipAddress != null)
                    return ipAddress.ToString();
                else return string.Empty;
            }
        }

        #region GetRealIpAddress
        private static IPAddress GetRealIpAddress()
        {
            IPAddress gateway = FindGetGatewayAddress();

            if (gateway == null)
                return null;

            IPAddress[] pIPAddress = Dns.GetHostAddresses(Dns.GetHostName());

            foreach (IPAddress address in pIPAddress)
                if (IsAddressOfGateway(address, gateway))
                    return address;

            return null;
        }

        private static bool IsAddressOfGateway(IPAddress address, IPAddress gateway)
        {
            if (address != null && gateway != null)
                return IsAddressOfGateway(address.GetAddressBytes(), gateway.GetAddressBytes());

            return false;
        }

        private static bool IsAddressOfGateway(byte[] address, byte[] gateway)
        {
            if (address != null && gateway != null)
            {
                int gwLen = gateway.Length;

                if (gwLen > 0)
                {
                    if (address.Length == gateway.Length)
                    {
                        --gwLen;
                        int counter = 0;

                        for (int i = 0; i < gwLen; i++)
                        {
                            if (address[i] == gateway[i])
                                ++counter;
                        }

                        return (counter == gwLen);
                    }
                }
            }
            return false;
        }

        private static IPAddress FindGetGatewayAddress()
        {
            IPGlobalProperties ipGlobProps = IPGlobalProperties.GetIPGlobalProperties();

            foreach (NetworkInterface ni in NetworkInterface.GetAllNetworkInterfaces())
            {
                IPInterfaceProperties ipInfProps = ni.GetIPProperties();

                foreach (GatewayIPAddressInformation gi in ipInfProps.GatewayAddresses)
                    if (!gi.Address.Equals(IPAddress.None) && !gi.Address.Equals(IPAddress.Loopback) && !gi.Address.Equals(IPAddress.Any)) return gi.Address;
            }

            return null;
        }
        #endregion
    }
}
