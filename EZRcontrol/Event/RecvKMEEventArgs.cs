using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace EZRcontrol
{
    //http://ehpub.co.kr/%EC%9B%90%EA%B2%A9-%EC%A0%9C%EC%96%B4-%ED%94%84%EB%A1%9C%EA%B7%B8%EB%9E%A8-12-%EC%9B%90%EA%B2%A9-%EC%A0%9C%EC%96%B4-%EC%9D%B4%EB%B2%A4%ED%8A%B8-%EC%88%98%EC%8B%A0-%EC%9D%B4%EB%B2%A4%ED%8A%B8/
    public delegate void RecvKMEEventHandler(object sender, RecvKMEEventArgs e);
    /// <summary>
    /// 원격제어 이벤트 수신용 이벤트
    /// </summary>
    public class RecvKMEEventArgs : EventArgs
    {

     
        public Meta Meta
        {
            get;
            private set;
        }
        public int Key
        {
            get { return Meta.Key; }
        }
        public Point Now
        {
            get { return Meta.Now; }
        }
        public MsgType MT
        {
            get { return Meta.Mt; }
        }

        internal RecvKMEEventArgs(Meta meta)
        {
            Meta = meta;
        }



    }
}
