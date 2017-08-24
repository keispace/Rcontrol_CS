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
    public partial class MainForm : Form
    {
        string sIp; //상대방 ip
        int sPort; //상대방 port
        RClinetForm rcf = null;//제어화면
        VcursorForm vcf = null; //가상 커서

        delegate void Recv_Dele(object sender, RecvKMEEventArgs e);

        public MainForm()
        {
            InitializeComponent();

            Fwall.AuthorizeProgram(Application.ProductName, Application.StartupPath);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
         

            Remote.Singleton.RecvedRCInfo += new RecvRCInfoEventHandler(Remote_RecvedRCInfo);
            Remote.Singleton.RecvedKME += new RecvKMEEventHandler(Remote_RecvedKME);
            this.Text = NetworkInfo.DefaultIP;
        }
        delegate void Remote_Dele(object sender, RecvRCInfoEventArgs e);

        void Remote_RecvedRCInfo(object sender, RecvRCInfoEventArgs e)
        {
            if (this.InvokeRequired)
            {
                object[] objs = new object[2] { sender, e };
                this.Invoke(new Remote_Dele(Remote_RecvedRCInfo), objs);
            }
            else
            {
                tbx_cont_ip.Text = e.IPAddressStr; //요청주소 표시
                sIp = e.IPAddressStr;
                sPort = e.Port;
                btn_ok.Enabled = true;
                //자동연결로 변경
                btn_ok_Click(sender, e);
            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Remote.Singleton.Stop();
            Controller.Singleton.Stop();
            noti.Dispose();
            Application.Exit();
        }

        private void btn_setting_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbx_ip.Text)) return;
            if (tbx_ip.Text == NetworkInfo.DefaultIP || tbx_ip.Text.Equals("127.0.0.1"))
            {
                MessageBox.Show("같은 호스트를 원격 제어할 수 없습니다.");
                tbx_ip.Text = string.Empty;
                return;
            }

            string host_ip = tbx_ip.Text;
            Rectangle rect = Remote.Singleton.Rect;
            if (Controller.Singleton.Start(host_ip))
            {
                rcf = new RClinetForm();
                rcf.ClientSize = new Size(rect.Width - 40, rect.Height - 80);
                rcf.Show();
            }
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            this.Hide();
            Remote.Singleton.RecvEventStart();
            tmr_send_img.Start();
            vcf = new VcursorForm();
            vcf.Show();
        }

        private void noti_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
        }

        private void tmr_send_img_Tick(object sender, EventArgs e)
        {
            Rectangle rect = Remote.Singleton.Rect;
            Bitmap bmp = new Bitmap(rect.Width, rect.Height);
            Graphics gp = Graphics.FromImage(bmp);
            Size size2 = new Size(rect.Width, rect.Height);

            try
            {
                gp.CopyFromScreen(new Point(0, 0), new Point(0, 0), size2); //화면 복사)
                gp.Dispose();
                ImageClient ic = new ImageClient(sIp, NetworkInfo.ImgPort);
                ic.SendImgAsync(bmp, null); //이미지 비동기로 전송

            }
            catch
            {
                tmr_send_img.Stop();
                //에러나면 뭐...
            }
        }



        private void tbx_ip_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
                btn_setting_Click(null, null);
        }


        void Remote_RecvedKME(object sender, RecvKMEEventArgs e)
        {

            if (this.InvokeRequired)
            {
                object[] objs = new object[2] { sender, e };
                this.Invoke(new Recv_Dele(Remote_RecvedKME), objs);
            }
            else
            {
                if (e.MT == MsgType.MT_CLOSE)
                {
                    this.Show();
                    vcf.Hide();
                }
            }
        }

    
    
    }

}
