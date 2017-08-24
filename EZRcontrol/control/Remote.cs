using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Automation;


namespace EZRcontrol
{
    //http://ehpub.co.kr/%EC%9B%90%EA%B2%A9-%EC%A0%9C%EC%96%B4-%ED%94%84%EB%A1%9C%EA%B7%B8%EB%9E%A8-16-%EC%9B%90%EA%B2%A9-%EC%A0%9C%EC%96%B4-%ED%98%B8%EC%8A%A4%ED%8A%B8/

    public class Remote
    {
        static Remote _obj;

        public event RecvRCInfoEventHandler RecvedRCInfo = null;
        public event RecvKMEEventHandler RecvedKME = null;
        RecvEventServer res = null;

        public static Remote Singleton
        {
            get { return _obj; }
        }

        public Rectangle Rect
        {
            get;
            private set;
        }

        public string MyIP { get { return NetworkInfo.DefaultIP; } }

        static Remote()
        {
            _obj = new Remote();
        }
        private Remote()
        {
            AutomationElement ae = AutomationElement.RootElement;
            System.Windows.Rect rt = ae.Current.BoundingRectangle;//화면 사이즈
            Rect = new Rectangle((int)rt.Left, (int)rt.Right, (int)rt.Width, (int)rt.Height);

            SetupServer.RecvRcInfo += new RecvRCInfoEventHandler(SetupServer_RecvRcInfo);
            SetupServer.Start(MyIP,NetworkInfo.SetupPort);
        }

        //제어요청 핸들러
        void SetupServer_RecvRcInfo(object sender, RecvRCInfoEventArgs e)
        {
            if (RecvedRCInfo != null) //구독자가 있을때
            {
                RecvedRCInfo(this, e);
            }
        }

        //메시지 수신 서버 가동 
        public void RecvEventStart()
        {
            res = new RecvEventServer(MyIP, NetworkInfo.EventPort);
            res.RecvKME += new RecvKMEEventHandler(res_RecvKME);
        }

        //메시지 수신 처리 핸들러
        void res_RecvKME(object sender, RecvKMEEventArgs e)
        {
            if (RecvedKME != null)
            {
                RecvedKME(this, e);
            }
            switch (e.MT)
            {
                case MsgType.MT_KDOWN: win32API.Key_Down(e.Key); break;
                case MsgType.MT_KUP: win32API.Key_Up(e.Key); break;

                case MsgType.MT_M_LDOWN: win32API.Mouse_LDown(); break;
                case MsgType.MT_M_LUP: win32API.Mouse_LUp(); break;
                case MsgType.MT_M_RDOWN: win32API.Mouse_RDown(); break;
                case MsgType.MT_M_RUP: win32API.Mouse_RUp(); break;
                case MsgType.MT_M_MDOWN: win32API.Mouse_MDown(); break;
                case MsgType.MT_M_MUP: win32API.Mouse_MUp(); break;

                case MsgType.MT_M_MOVE: win32API.Mouse_Move(e.Now.X, e.Now.Y); break;

              
            }
        }

        public void Stop()
        {
            SetupServer.Close();
            if (res != null)
            {
                res.Close();
                res = null;
            }
        }
    }
}