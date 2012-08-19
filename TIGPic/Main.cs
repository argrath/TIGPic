using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Net;
using System.IO;


namespace WindowsFormsApplication1 {
    public partial class Main : Form {
        public string URL {
            get;
            set;
        }

        private Image NowImage;

        public Main() {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e) {
            {
                WebClient wc = new WebClient();
                using (Stream st = wc.OpenRead(URL)) {
                    NowImage = Image.FromStream(st);
                }
            }
            mainPic.Image = NowImage;

            Size s = NowImage.Size;

            {
                Size maxSize = SystemInformation.MaxWindowTrackSize;
                int paddingWidth = this.Width - this.ClientSize.Width;
                int paddingHeight = this.Height - this.ClientSize.Height;
                maxSize.Width -= paddingWidth;
                maxSize.Height -= paddingHeight;

                if(s.Width > maxSize.Width){
                    s.Height = s.Height * maxSize.Width / s.Width;
                    s.Width = maxSize.Width;
                }
                if (s.Height > maxSize.Height) {
                    s.Width = s.Width * maxSize.Height / s.Height;
                    s.Height = maxSize.Height;
                }
            }

            this.ClientSize = new Size(s.Width, s.Height);
            this.Text = URL;
            ResizeSub();
        }

        private void Main_Resize(object sender, EventArgs e) {
//            ResizeSub();
        }

        private void ResizeSub() {
            mainPic.Size = this.ClientSize;
        }

        private void Main_SizeChanged(object sender, EventArgs e) {
            ResizeSub();
        }
    }
}
