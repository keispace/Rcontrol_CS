using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Net;

namespace EZRcontrol
{
    //http://ehpub.co.kr/%EC%9B%90%EA%B2%A9-%EC%A0%9C%EC%96%B4-%ED%94%84%EB%A1%9C%EA%B7%B8%EB%9E%A8-13-%EC%9B%90%EA%B2%A9-%EC%A0%9C%EC%96%B4-%EC%9D%B4%EB%B2%A4%ED%8A%B8-%EC%88%98%EC%8B%A0-%EC%84%9C%EB%B2%84/
    public class RecvEventServer
    {
        static Socket listener;
        static Thread th = null;

        public event RecvKMEEventHandler RecvKME = null;
        public delegate void RecvDele(Socket dosock);

        public RecvEventServer(string ip, int port)
        {
            IPAddress ipaddr = IPAddress.Parse(ip);
            IPEndPoint ep = new IPEndPoint(ipaddr, port);
            listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            listener.Bind(ep);  // 소켓-ip 결합
            listener.Listen(5); // back 로그 큐 크기설정

            //연결요청 허용 쓰레드 진입점 생성 및 시작
            ThreadStart ts = new ThreadStart(AcceptLoop);
            th = new Thread(ts);
            th.Start();
        }


        //연결요청 대기
        void AcceptLoop()
        {
            Socket do_sock;
            RecvDele rld = new RecvDele(Receive);
            try
            {
                while (true)
                {
                    do_sock = listener.Accept(); //리스너 연결 수락
                    rld.BeginInvoke(do_sock, null, null);
                }
            }
            catch
            {
                Close();
            }
        }

        void Receive(Socket dosock)
        {
            byte[] buff = new byte[9];
            int n = dosock.Receive(buff);
            if (RecvKME != null)
            {
                RecvKMEEventArgs e = new RecvKMEEventArgs(new Meta(buff));
                RecvKME(this, e);
            }
            dosock.Close();
        }


        public void Close()
        {
            if (listener != null)
            {
                listener.Close();
                listener = null;
            }
        }

    }
}
