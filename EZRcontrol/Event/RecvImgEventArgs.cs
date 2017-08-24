using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Drawing;

namespace EZRcontrol
{
    //http://ehpub.co.kr/%EC%9B%90%EA%B2%A9-%EC%A0%9C%EC%96%B4-%ED%94%84%EB%A1%9C%EA%B7%B8%EB%9E%A8-8-%EC%9B%90%EA%B2%A9-%EC%A0%9C%EC%96%B4-%ED%99%94%EB%A9%B4-%EC%88%98%EC%8B%A0-%EC%9D%B4%EB%B2%A4%ED%8A%B8/
    public delegate void RecvImgEventHandler(object sender, RecvImgEventArgs e);
    public class RecvImgEventArgs : EventArgs
    {

        public IPEndPoint IPEndPoint
        {
            get;
            private set;
        }

        public IPAddress IPAddress
        {
            get
            {
                return IPEndPoint.Address;
            }
        }
        public string IPAddressStr
        {
            get
            {
                return IPEndPoint.Address.ToString();
            }
        }

        public int Port
        {
            get
            {
                return IPEndPoint.Port;
            }
        }

        public Image Image
        {
            get;
            private set;
        }
        public Size Size
        {
            get
            {
                return Image.Size;
            }
        }

        public int Width
        {
            get
            {
                return Image.Width;
            }
        }

        public int Height
        {
            get
            {
                return Image.Height;
            }
        }




        internal RecvImgEventArgs(IPEndPoint r_iep, Image img)
        {
            IPEndPoint = r_iep;
            Image = img;

        }
        //상대측 IP 정보와 이미지의 폭과 높이를 문자열로 형성하여 제공
        public override string ToString()
        {
            return string.Format("IP:{0} width:{1} height:{1}", IPAddressStr, Width, Height);
        }
    }
}
