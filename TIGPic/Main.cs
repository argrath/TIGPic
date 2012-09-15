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
        private const int WM_NCHITTEST = 0x84;
        /// <summary>ウィンドウ境界の左端の線上にあります。</summary>
        private const int HTLEFT = 10;
        /// <summary>ウィンドウの右境界線にあります。</summary>
        private const int HTRIGHT = 11;
        /// <summary>ウィンドウ境界の上端の線上にあります。</summary>
        private const int HTTOP = 12;
        /// <summary>ウィンドウ境界線の左上隅にあります。</summary>
        private const int HTTOPLEFT = 13;
        /// <summary>ウィンドウ境界線の右上隅にあります。</summary>
        private const int HTTOPRIGHT = 14;
        /// <summary>ウィンドウの下端の境界線にあります。</summary>
        private const int HTBOTTOM = 15;
        /// <summary>ウィンドウ境界の左下隅にあります。</summary>
        private const int HTBOTTOMLEFT = 16;
        /// <summary>ウィンドウ境界の右下隅にあります。</summary>
        private const int HTBOTTOMRIGHT = 17;
        /// <summary>サイズ変更境界を保持しないウィンドウの境界内にあります。</summary>
        private const int HTBORDER = 18;

        private int paddingWidth;
        private int paddingHeight;

        protected override void WndProc(ref System.Windows.Forms.Message m) {
            if (m.Msg == WM_NCHITTEST) {
                // フォームの既定の処理を行い、マウスの位置を調査 (m.Result に値が入ります)
                base.WndProc(ref m);

                switch (m.Result.ToInt32()) {
                    case HTTOP:
                    case HTTOPRIGHT:
                    case HTBOTTOM:
                    case HTBOTTOMLEFT:
                    case HTLEFT:
                    case HTRIGHT:
                        m.Result = new IntPtr(HTBORDER);
                        return;
                }
            }

            base.WndProc(ref m);
        }

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
                paddingWidth = this.Width - this.ClientSize.Width;
                paddingHeight = this.Height - this.ClientSize.Height;
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
            this.SetStyle(ControlStyles.Opaque, true);
        }

        private void Main_Resize(object sender, EventArgs e) {
              ResizeSub();
        }

        private void ResizeSub() {
            if (NowImage.Size.Height > NowImage.Size.Width) {
                this.ClientSize = new Size(NowImage.Size.Width * this.ClientSize.Height / NowImage.Size.Height, this.ClientSize.Height);
            } else {
                this.ClientSize = new Size(this.ClientSize.Width, NowImage.Size.Height * this.ClientSize.Width / NowImage.Size.Width);
            }
            this.mainPic.Size = this.ClientSize;
        }
    }
}
