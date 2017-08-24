using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EZRcontrol
{
    public class Controller
    {
        //http://ehpub.co.kr/%EC%9B%90%EA%B2%A9-%EC%A0%9C%EC%96%B4-%ED%94%84%EB%A1%9C%EA%B7%B8%EB%9E%A8-14-%EC%9B%90%EA%B2%A9-%EC%A0%9C%EC%96%B4-%EC%BB%A8%ED%8A%B8%EB%A1%A4%EB%9F%AC/

        static Controller _obj;

        public static Controller Singleton
        {
            get { return _obj; }
        }
        static Controller()
        {
            _obj = new Controller();
        }
        private Controller()
        {
        }

        ImageServer img_sever = null; //이미지 수신 서버
        SendEventClient sce = null; //키보드, 마우스 제어 이벤트 전송 클라이언트
        public event RecvImgEventHandler RecvedImage = null;//이미지 수신 이벤트
        string host_ip; //원격 제어 호스트 IP 문자열

        public SendEventClient SendEventClient //이벤트 전송 클라이언트 접근자
        {
            get
            {
                return sce;
            }
        }

        public string MyIP//로컬 IP문자열 접근자
        {
            get
            {
                return NetworkInfo.DefaultIP;
            }
        }

        public bool Start(string host_ip)
        {
            this.host_ip = host_ip;
            img_sever = new ImageServer(MyIP, NetworkInfo.ImgPort);

            //구독요청
            img_sever.RecvImg += new RecvImgEventHandler(RecvImgHandler);

            if (SetupClient.Setup(host_ip, NetworkInfo.SetupPort))
                return true;
            else
            {
                Stop();
                return false; 
            }

        }
        //이미지 수신 이벤트 핸들러
        void RecvImgHandler(object sender, RecvImgEventArgs e)
        {
            if (RecvedImage != null)
            {
                RecvedImage(this, e);
            }
        }

        public void StartEventClient()
        {
            sce = new SendEventClient(host_ip, NetworkInfo.EventPort);
        }

        public void Stop()
        {
            if (img_sever != null)
            {
                img_sever.Close();
                img_sever = null;
            }
        }

    }
}
