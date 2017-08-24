using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Drawing;
using System.IO;
namespace EZRcontrol
{
    //http://ehpub.co.kr/%EC%9B%90%EA%B2%A9-%EC%A0%9C%EC%96%B4-%ED%94%84%EB%A1%9C%EA%B7%B8%EB%9E%A8-9-%EC%9B%90%EA%B2%A9-%EC%A0%9C%EC%96%B4-%ED%99%94%EB%A9%B4-%EC%88%98%EC%8B%A0-%EC%84%9C%EB%B2%84/
    public class ImageServer
    {
        static Socket listener;
        static Thread accepter_T = null;

        public event RecvImgEventHandler RecvImg = null;

        public ImageServer(string ip, int port)
        {
            IPAddress ipaddr = IPAddress.Parse(ip);
            IPEndPoint ep = new IPEndPoint(ipaddr, port);
            listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            listener.Bind(ep);  // 소켓-ip 결합
            listener.Listen(5); // back 로그 큐 크기설정

            //연결요청 허용 쓰레드 진입점 생성 및 시작
            ThreadStart ts = new ThreadStart(AcceptLoop);
            accepter_T = new Thread(ts);
            accepter_T.Start();
        }

        //연결요청 대기
        void AcceptLoop()
        {
            try
            {
                while (listener != null)
                {
                    Socket do_sock = listener.Accept(); //리스너 연결 수락
                    Receive(do_sock);
                }
            }
            catch
            {
                Close();
            }
        }

        void Receive(Socket dosock)
        {
            byte[] lbuf = new byte[4]; //이미지 길이수신 버퍼
            dosock.Receive(lbuf);

            int len = BitConverter.ToInt32(lbuf, 0);
            byte[] buff = new byte[len];

            int trans = 0;
            while (trans < len)
            {
                trans += dosock.Receive(buff, trans, len - trans, SocketFlags.None);//이미지수신
            }

            if (RecvImg != null)
            {
                //이미지 수신이벤트
                IPEndPoint iep = dosock.RemoteEndPoint as IPEndPoint;
                RecvImgEventArgs e = new RecvImgEventArgs(iep, ConvertBitmap(buff));
                RecvImg(this, e);
            }
            dosock.Close();
        }

        public Bitmap ConvertBitmap(byte[] data)
        {
            MemoryStream ms = new MemoryStream();
            ms.Write(data, 0, (int)data.Length);
            Bitmap bmp = new Bitmap(ms);
            return bmp;
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
