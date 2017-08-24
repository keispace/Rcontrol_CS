using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace EZRcontrol
{
    //http://ehpub.co.kr/%EC%9B%90%EA%B2%A9-%EC%A0%9C%EC%96%B4-%ED%94%84%EB%A1%9C%EA%B7%B8%EB%9E%A8-4-%EC%9B%90%EA%B2%A9-%EC%A0%9C%EC%96%B4-%EC%9A%94%EC%B2%AD-%ED%81%B4%EB%9D%BC%EC%9D%B4%EC%96%B8%ED%8A%B8/
    public static class SetupClient
    {
        public static bool Setup(string ip, int port)
        {
            IPAddress ipaddr = IPAddress.Parse(ip);
            IPEndPoint ep = new IPEndPoint(ipaddr, port);

            //tcp로 원격제어 수용여부 판단. 
            Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                sock.Connect(ep);
                return true;
            }
            catch (Exception e)
            {

                System.Windows.Forms.MessageBox.Show(e.Message);
                return false;
            }
            finally
            {
                sock.Close();
            }
        }

    }
}
