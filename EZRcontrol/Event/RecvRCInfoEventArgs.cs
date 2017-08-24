using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace EZRcontrol
{
    //http://ehpub.co.kr/%EC%9B%90%EA%B2%A9-%EC%A0%9C%EC%96%B4-%ED%94%84%EB%A1%9C%EA%B7%B8%EB%9E%A8-5-%EC%9B%90%EA%B2%A9-%EC%A0%9C%EC%96%B4-%EC%9A%94%EC%B2%AD-%EC%88%98%EC%8B%A0-%EC%9D%B4%EB%B2%A4%ED%8A%B8/
    public delegate void RecvRCInfoEventHandler(object sender, RecvRCInfoEventArgs e);
    public class RecvRCInfoEventArgs : EventArgs
    {

        public IPEndPoint IPePoint
        {
            get;
            private set;
        }

        public string IPAddressStr
        {
            get
            {
                return IPePoint.Address.ToString();
            }
        }

        public int Port
        {
            get
            {
                return IPePoint.Port;
            }
        }

        internal RecvRCInfoEventArgs(EndPoint rEndPoint)
        {
            IPePoint = rEndPoint as IPEndPoint;
        }
    }
}
