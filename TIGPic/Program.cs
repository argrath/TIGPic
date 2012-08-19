using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace WindowsFormsApplication1 {
    static class Program {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            string args = System.Environment.CommandLine;
            Main m = new Main();
            Parse p = new Parse(args);
            m.URL = p.ImageURL;
            if (m.URL != null) {
                Application.Run(m);
            }
        }
    }
}
