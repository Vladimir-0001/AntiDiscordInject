using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;



namespace AntiInject
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Hide();
            watcher w = new watcher();
            {
                
                
                Thread watcher = new Thread(w.Watch);
                watcher.Start();
            }
            this.Hide();
            this.ShowInTaskbar = false;
        }
    }
}
