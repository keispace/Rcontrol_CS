using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

namespace EZRcontrol
{
    //http://ehpub.co.kr/%EC%9B%90%EA%B2%A9-%EC%A0%9C%EC%96%B4-%ED%94%84%EB%A1%9C%EA%B7%B8%EB%9E%A8-10-%EC%9B%90%EA%B2%A9-%EC%A0%9C%EC%96%B4-%EC%9D%B4%EB%B2%A4%ED%8A%B8-%EC%A0%84%EC%86%A1-%ED%81%B4%EB%9D%BC%EC%9D%B4%EC%96%B8/

    public enum MsgType //원격제어 이벤트 종류
    {
        MT_KDOWN = 1,
        MT_KUP,
        MT_M_LDOWN,
        MT_M_LUP,
        MT_M_RDOWN,
        MT_M_RUP,
        MT_M_MDOWN,
        MT_M_MUP,
        MT_M_MOVE,
        MT_CLOSE
    }

    public class SendEventClient
    {
        IPEndPoint ep;

        public SendEventClient(string ip, int port)
        {
            ep = new IPEndPoint(IPAddress.Parse(ip), port);
        }


        private void SendData(byte[] data)
        {
            Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                sock.Connect(ep);
                sock.Send(data);
            }
            catch { }
            finally
            {
                sock.Close();
            }
        }

        public void SendKeyDown(int key)
        {
            byte[] data = new byte[9];//최대 종류(1), x좌표(4), y좌표(4) 로 9바이트 고정 

            data[0] = (byte)MsgType.MT_KDOWN;
            Array.Copy(BitConverter.GetBytes(key), 0, data, 1, 4);
            SendData(data);//버퍼 전송
        }

        public void SendKeyUp(int key)
        {
            byte[] data = new byte[9];//최대 종류(1), x좌표(4), y좌표(4) 로 9바이트 고정 

            data[0] = (byte)MsgType.MT_KUP;
            Array.Copy(BitConverter.GetBytes(key), 0, data, 1, 4);
            SendData(data);//버퍼 전송
        }

        public void SendMouseDown(MouseButtons mBtn)
        {
            byte[] data = new byte[9];//최대 종류(1), x좌표(4), y좌표(4) 로 9바이트 고정 

            switch (mBtn)
            {
                case MouseButtons.Left: data[0] = (byte)MsgType.MT_M_LDOWN; break;
                case MouseButtons.Middle: data[0] = (byte)MsgType.MT_M_MDOWN; break;
                case MouseButtons.Right: data[0] = (byte)MsgType.MT_M_RDOWN; break;
            }
            SendData(data);//버퍼 전송
        }

        public void SendMouseUp(MouseButtons mBtn)
        {
            byte[] data = new byte[9];//최대 종류(1), x좌표(4), y좌표(4) 로 9바이트 고정 

            switch (mBtn)
            {
                case MouseButtons.Left: data[0] = (byte)MsgType.MT_M_LUP; break;
                case MouseButtons.Middle: data[0] = (byte)MsgType.MT_M_MUP; break;
                case MouseButtons.Right: data[0] = (byte)MsgType.MT_M_RUP; break;
            }
            SendData(data);//버퍼 전송
        }

        public void SendMouseMove(int x, int y)
        {
            byte[] data = new byte[9];//최대 종류(1), x좌표(4), y좌표(4) 로 9바이트 고정 
            data[0] = (byte)MsgType.MT_M_MOVE;
            Array.Copy(BitConverter.GetBytes(x), 0, data, 1, 4);
            Array.Copy(BitConverter.GetBytes(y), 0, data, 5, 4);
            SendData(data);
        }


        internal void SendClose()
        {
            byte[] data = new byte[9];//최대 종류(1), x좌표(4), y좌표(4) 로 9바이트 고정 
            data[0] = (byte)MsgType.MT_CLOSE;

            SendData(data);
        }
    }
}
