using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace EZRcontrol
{
    //http://ehpub.co.kr/%EC%9B%90%EA%B2%A9-%EC%A0%9C%EC%96%B4-%ED%94%84%EB%A1%9C%EA%B7%B8%EB%9E%A8-11-%EC%9B%90%EA%B2%A9-%EC%A0%9C%EC%96%B4-%EC%9D%B4%EB%B2%A4%ED%8A%B8-%EC%88%98%EC%8B%A0-%EB%B2%84%ED%8D%BC-%EB%B6%84/
    public class Meta
    {
        public MsgType Mt
        {
            get;
            private set;
        }

        public int Key
        {
            get;
            private set;
        }

        public Point Now
        {
            get;
            private set;
        }

        public Meta(byte[] data)
        {
            Mt = (MsgType)data[0];
            switch (Mt)
            {
                case MsgType.MT_KDOWN:
                case MsgType.MT_KUP: MarkingKey(data); break;
                case MsgType.MT_M_MOVE: MarkingPoint(data); break;

            }
        }

        private void MarkingPoint(byte[] data)
        {
            Point now = new Point(0, 0);
            now.X = (data[4] << 24) + (data[3] << 16) + (data[2] << 8) + (data[1]);
            now.Y = (data[8] << 24) + (data[7] << 16) + (data[6] << 8) + (data[5]);
            Now = now;
        }

        private void MarkingKey(byte[] data)
        {
            Key = (data[4] << 24) + (data[3] << 16) + (data[2] << 8) + (data[1]);
        }
    }
}
