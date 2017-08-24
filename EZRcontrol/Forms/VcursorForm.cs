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
    public partial class VcursorForm : Form
    {
        public VcursorForm()
        {
            InitializeComponent();
        }

        private void VcursorForm_Load(object sender, EventArgs e)
        {
            Remote.Singleton.RecvedKME += new RecvKMEEventHandler(GetInstance_RecvedKME);
        }

        void GetInstance_RecvedKME(object sender, RecvKMEEventArgs e)
        {

            if (this.InvokeRequired)
            {
                object[] objs = new object[2] { sender, e };
                this.Invoke(new Recv_Dele(GetInstance_RecvedKME), objs);
            }
            else
            {
                if (this.Visible)
                    if (e.MT == MsgType.MT_M_MOVE)
                    {
                        this.Location = new Point(e.Now.X + 3, e.Now.Y + 3);
                    }

            }
        }

        delegate void Recv_Dele(object sender, RecvKMEEventArgs e);



    }
}
