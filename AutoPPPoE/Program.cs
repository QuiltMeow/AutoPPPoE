using SkinSharp;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace AutoPPPoE
{
    public static class Program
    {
        private const int EXIT_FAILURE = 1;

        private static SkinH_Net skin;

        public static IPTool ipTool
        {
            get;
            private set;
        }

        public static AdapterManager adapter
        {
            get;
            private set;
        }

        public static Config config
        {
            get;
            private set;
        }

        public static MainForm main
        {
            get;
            private set;
        }

        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        [STAThread]
        public static void Main(string[] args)
        {
            Application.SetCompatibleTextRenderingDefault(false);
            try
            {
                skin = new SkinH_Net();
                skin.AttachEx("Skin.she", "");
            }
            catch
            {
                Application.EnableVisualStyles();
            }

            string name = Process.GetCurrentProcess().ProcessName;
            if (Process.GetProcessesByName(name).Length > 1)
            {
                MessageBox.Show("已經有其他程式實例正在執行", "資訊", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            adapter = new AdapterManager();
            if (adapter.adapterName.Count <= 0 || adapter.rasName.Count <= 0)
            {
                MessageBox.Show("找不到網路卡 請檢查網路設定是否正確", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(EXIT_FAILURE);
            }
            ipTool = new IPTool();

            config = new Config();
            main = new MainForm();
            Application.Run(main);
        }
    }
}