using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing;

namespace EZRcontrol
{
//    http://ehpub.co.kr/%EC%9B%90%EA%B2%A9-%EC%A0%9C%EC%96%B4-%ED%94%84%EB%A1%9C%EA%B7%B8%EB%9E%A8-7-%EC%9B%90%EA%B2%A9-%EC%A0%9C%EC%96%B4-%ED%99%94%EB%A9%B4-%EC%A0%84%EC%86%A1/
    public class ImageClient
    {
        Socket sock;

        public delegate bool SendImgDele(Image img); //이미지 전달 델리게이트(비동기용)

        public ImageClient(string ip, int port)
        {
            sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            IPAddress ipaddr = IPAddress.Parse(ip);
            IPEndPoint ep = new IPEndPoint(ipaddr, port);
            sock.Connect(ep);
        }

        public bool sendImg(Image img)
        {
            if (sock == null) return false;

            MemoryStream ms = new MemoryStream();
            img.Save(ms, ImageFormat.Jpeg);

            byte[] data = ms.GetBuffer();
            try
            {
                int trans = 0;
                byte[] lbuf = BitConverter.GetBytes(data.Length);
                sock.Send(lbuf); // 버퍼 길이 전달 

                while (trans < data.Length)
                {
                    trans += sock.Send(data, trans, data.Length - trans, SocketFlags.None);
                }
                sock.Close();
                sock = null;
                return true;

            }
            catch
            {
                System.Windows.Forms.Application.Exit();
                return false;
            }
        }

        public void SendImgAsync(Image img, AsyncCallback callback)
        {
            SendImgDele dele = new SendImgDele(sendImg);
            dele.BeginInvoke(img, callback, this);
        }

        public void Close()
        {
            if (sock != null)
            {
                sock.Close();
                sock = null;
            }
        }
    }
}
