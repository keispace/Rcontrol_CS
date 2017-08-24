using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EZRcontrol
{
    //http://ehpub.co.kr/%EC%9B%90%EA%B2%A9-%EC%A0%9C%EC%96%B4-%ED%94%84%EB%A1%9C%EA%B7%B8%EB%9E%A8-18-%EC%9B%90%EA%B2%A9-%ED%81%B4%EB%9D%BC%EC%9D%B4%EC%96%B8%ED%8A%B8-%ED%8F%BC/
    public partial class RClinetForm : Form
    {
        public RClinetForm()
        {
            InitializeComponent();
        }

        bool chk; //이미지 수신여부
        Size csize; // 클라이언트 폼 크기 

        SendEventClient EventSC
        {
            get { return Controller.Singleton.SendEventClient; }
        }

        private void RClinetForm_Load(object sender, EventArgs e)
        {
            Controller.Singleton.RecvedImage += new RecvImgEventHandler(GetInstacne_RecvedImage);
        }

        void GetInstacne_RecvedImage(object sender, RecvImgEventArgs e)
        {
            if (!chk)
            {
                Controller.Singleton.StartEventClient();
                chk = true;
                csize = e.Image.Size;
            }
            pbx_remote.Image = e.Image;
        }

        private void RClinetForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (chk)
            {
                EventSC.SendKeyDown(e.KeyValue);
            }
        }

        private void RClinetForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (chk)
            {
                EventSC.SendKeyUp(e.KeyValue);
            }
        }


        private void RClinetForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (chk)
            {
                Point pt = ConvertPoint(e.X, e.Y);
                EventSC.SendMouseMove(pt.X, pt.Y);
            }
        }
        private void pbx_remote_MouseMove(object sender, MouseEventArgs e)
        {
            RClinetForm_MouseMove(sender, e);
        }
        private Point ConvertPoint(int x, int y)
        {
            int nx = csize.Width * x / pbx_remote.Width;
            int ny = csize.Height * y / pbx_remote.Height;
            return new Point(nx, ny);
        }


        private void RClinetForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (chk)
            {
                Text = e.Location.ToString();
                EventSC.SendMouseDown(e.Button);
            }
        }
        private void pbx_remote_MouseDown(object sender, MouseEventArgs e)
        {
            RClinetForm_MouseDown(sender, e);
        }


        private void RClinetForm_MouseUp(object sender, MouseEventArgs e)
        {
            if (chk)
            {
                EventSC.SendMouseUp(e.Button);
            }
        }
        private void pbx_remote_MouseUp(object sender, MouseEventArgs e)
        {
            RClinetForm_MouseUp(sender, e);
        }

        private void RClinetForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (EventSC != null) EventSC.SendClose(); 
            Controller.Singleton.Stop();
           
        }

      
       


       


    }
}
