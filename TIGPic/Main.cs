using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1 {
    public partial class Main : Form {
        public string URL {
            get;
            set;
        }

        public Main() {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e) {
            mainPic.WaitOnLoad = true;
            mainPic.ImageLocation = URL;
            Size s = mainPic.Image.Size;
            mainPic.Height = s.Height;
            mainPic.Width = s.Width;
            this.Height = s.Height + 24;
            this.Width = s.Width + 16;
            this.Text = URL;
        }
    }
}
