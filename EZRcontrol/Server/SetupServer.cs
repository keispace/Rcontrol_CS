using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace EZRcontrol
{
    //http://ehpub.co.kr/%EC%9B%90%EA%B2%A9-%EC%A0%9C%EC%96%B4-%ED%94%84%EB%A1%9C%EA%B7%B8%EB%9E%A8-6-%EC%9B%90%EA%B2%A9-%EC%A0%9C%EC%96%B4-%EC%9A%94%EC%B2%AD-%EC%88%98%EC%8B%A0-%EC%84%9C%EB%B2%84/
   public static class SetupServer
    {
       static Socket listener;
       static Thread accepter_T = null;

       public static event RecvRCInfoEventHandler RecvRcInfo = null;

       public static void Start(string ip, int port)
       {
           IPAddress ipaddr = IPAddress.Parse(ip);
           IPEndPoint ep = new IPEndPoint(ipaddr, port);
           listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
           listener.Bind(ep);  // 소켓-ip 결합
           listener.Listen(1); // back 로그 큐 크기설정

           //연결요청 허용 쓰레드 진입점 생성 및 시작
           ThreadStart ts = new ThreadStart(AcceptLoop);
           accepter_T = new Thread(ts);
           accepter_T.Start();
       }

       //연결요청 대기
       static void AcceptLoop()
       {
           try
           {
               while (true)
               {
                   Socket do_sock = listener.Accept(); //리스너 연결 수락
                   if (RecvRcInfo != null) //수신 이벤트 핸들러가 있으면 == 받은게 있으면
                   {
                       RecvRCInfoEventArgs e = new RecvRCInfoEventArgs(do_sock.RemoteEndPoint);
                       RecvRcInfo(null, e);
                   }
               }
           }
           catch
           {
               Close();
           }
       }

       public static void Close()
       {
           if (listener != null)
           {
               listener.Close();
               listener = null;
           }
       }
   }

}
