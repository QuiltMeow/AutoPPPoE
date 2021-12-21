using DotRas;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoPPPoE
{
    public partial class MainForm : Form
    {
        private static readonly IDictionary<Status, string> TIP_MESSAGE = new Dictionary<Status, string>() {
            { Status.SHOW_WELCOME, "自動撥號程式將常駐於右下角" },
            { Status.SHOW_START, "自動撥號程式執行中 ..." },
            { Status.SHOW_AUTOMATIC_START, "自動撥號程式已自動執行" },
            { Status.SHOW_DISCONNECT, "偵測到網路斷線 正在進行重撥 ..." },
            { Status.SHOW_ADAPTER, "目標網路卡停用 正在進行啟用 ..." },
            { Status.SHOW_CONNECT, "正在進行 PPPoE 撥號 ..." }
        };

        private static readonly Config config = Program.config;
        private Thread checkThread;

        public MainForm()
        {
            InitializeComponent();
            loadNetworkInterface();
            Util.loadSettingNameUI(cbSetting);
            updateUIStatus();
            txtAccount.SelectionStart = txtAccount.Text.Length;
        }

        private void loadNetworkInterface()
        {
            AdapterManager adapter = Program.adapter;
            foreach (string adapterName in adapter.adapterName)
            {
                cbAdapter.Items.Add(adapterName);
            }
            cbAdapter.SelectedIndex = 0;

            foreach (string rasName in adapter.rasName)
            {
                cbName.Items.Add(rasName);
            }
            cbName.SelectedIndex = 0;
        }

        private void enableControlUI(bool enable)
        {
            cbAdapter.Enabled = cbName.Enabled = txtAccount.Enabled
                = txtPassword.Enabled = chkShowPassword.Enabled = numFastPing.Enabled
                = numSlowPing.Enabled = chkAutomaticStart.Enabled = numAutomaticStartWait.Enabled
                = btnSave.Enabled = enable;
            if (enable)
            {
                updateAutomaticStartWaitUI();
            }
        }

        private void restoreDefault()
        {
            cbAdapter.SelectedIndex = cbName.SelectedIndex = 0;
            txtAccount.Text = txtPassword.Text = string.Empty;
            chkShowPassword.Checked = chkAutomaticStart.Checked = false;
            Util.forceUpdateNumericUpDownValue(numFastPing, 750);
            Util.forceUpdateNumericUpDownValue(numSlowPing, 2500);
            Util.forceUpdateNumericUpDownValue(numAutomaticStartWait, 5);
        }

        private void updateUISetting()
        {
            Setting current = config.current;
            Util.optionSelect(cbAdapter, current.adapter);
            Util.optionSelect(cbName, current.name);

            txtAccount.Text = current.account;
            txtPassword.Text = current.password;
            Util.forceUpdateNumericUpDownValue(numFastPing, current.fastPing);
            Util.forceUpdateNumericUpDownValue(numSlowPing, current.slowPing);
            chkAutomaticStart.Checked = current.automaticStart;
            Util.forceUpdateNumericUpDownValue(numAutomaticStartWait, current.automaticStartWaitTime);
        }

        private void updateUIStatus()
        {
            if (checkThread != null)
            {
                cbSetting.Enabled = btnManageSetting.Enabled = false;
                enableControlUI(false);
                updateUISetting();
                btnStart.Text = "停止";
            }
            else
            {
                cbSetting.Enabled = btnManageSetting.Enabled = true;
                bool hasSelect = config.select != null;
                enableControlUI(hasSelect);
                btnStart.Enabled = hasSelect;
                if (!hasSelect)
                {
                    restoreDefault();
                }
                else
                {
                    updateUISetting();
                }
                btnStart.Text = "開始";
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            welcome();
            if (config.select != null && config.current.automaticStart)
            {
                if (applyUIStart(true))
                {
                    startPPPoEThread(Status.MODE_START_UP);
                    updateUIStatus();
                }
            }
        }

        private static bool isPPPoEActive()
        {
            foreach (RasConnection connection in RasConnection.GetActiveConnections())
            {
                if (connection.EntryName == config.current.name)
                {
                    return true;
                }
            }
            return false;
        }

        private Setting createSetting()
        {
            return new Setting(cbAdapter.SelectedItem.ToString(),
                cbName.SelectedItem.ToString(),
                txtAccount.Text,
                txtPassword.Text,
                Convert.ToInt32(numFastPing.Value),
                Convert.ToInt32(numSlowPing.Value),
                chkAutomaticStart.Checked,
                Convert.ToInt32(numAutomaticStartWait.Value));
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            txtAccount.Text = Util.removeWhiteSpace(txtAccount.Text);
            txtPassword.Text = Util.removeWhiteSpace(txtPassword.Text);
            try
            {
                Setting current = createSetting();
                config.current = current;
                config.saveSetting();
                updateUISetting();
            }
            catch (Exception ex)
            {
                MessageBox.Show("儲存設定時發生例外狀況 : " + ex.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static bool isAdapterAlive()
        {
            foreach (string adapter in NetworkInterface.GetAllNetworkInterfaces().Select(target => target.Name))
            {
                if (adapter == config.current.adapter)
                {
                    return true;
                }
            }
            return false;
        }

        private void startPPPoEThread(Status mode)
        {
            checkThread = new Thread(() => checkConnect(mode));
            checkThread.Priority = ThreadPriority.AboveNormal;
            checkThread.Start();
        }

        private static void startPPPoE()
        {
            Setting current = config.current;
            CommandHelper.runCommand("rasdial \"" + current.name + "\" " + current.account + " " + current.password);
        }

        private static void stopPPPoE()
        {
            CommandHelper.runCommand("rasdial \"" + config.current.name + "\" /disconnect");
        }

        private static void enableAdapter()
        {
            CommandHelper.runCommand("netsh interface set interface \"" + config.current.adapter + "\" enable");
        }

        private void welcome()
        {
            niPermanent.Visible = true;
            showBalloonTip(Status.SHOW_WELCOME);
        }

        private void showBalloonTip(Status mode, bool log = false)
        {
            string message = TIP_MESSAGE[mode];
            niPermanent.BalloonTipText = message;
            niPermanent.ShowBalloonTip(Constant.TOOL_TIP_SHOW_DURATION);
            if (log)
            {
                appendDebugLog(message);
            }
        }

        private void checkConnect(Status mode)
        {
            if (mode == Status.MODE_START_UP)
            {
                Thread.Sleep(config.current.automaticStartWaitTime * 1000);
            }
            appendDebugLog("網路連線檢查已開始運作");
            while (true)
            {
                try
                {
                    if (!tsmiPause.Checked)
                    {
                        if (!isAdapterAlive())
                        {
                            showBalloonTip(Status.SHOW_ADAPTER, true);
                            enableAdapter();
                            Constant.wait(Status.WAIT_ADAPTER); // 等待網路卡就緒
                        }

                        if (!isPPPoEActive())
                        {
                            showBalloonTip(Status.SHOW_CONNECT, true);
                            startPPPoE();
                        }
                        else
                        {
                            int fastCheckFailCount = 0;
                            int slowCheckFailCount = 0;

                            Setting current = config.current;
                            for (int i = 1; i <= Constant.FAST_PING_CHECK_TIME; ++i)
                            {
                                fastCheckFailCount += !PingHelper.pingHost(Constant.FAST_CHECK_HOST, current.fastPing) ? 1 : 0;
                            }
                            for (int i = 1; i <= Constant.SLOW_PING_CHECK_TIME; ++i)
                            {
                                slowCheckFailCount += !PingHelper.pingHost(Constant.SLOW_CHECK_HOST, current.slowPing) ? 1 : 0;
                            }

                            if (fastCheckFailCount >= Constant.FAST_PING_CHECK_TIME && slowCheckFailCount >= Constant.SLOW_PING_CHECK_TIME)
                            {
                                showBalloonTip(Status.SHOW_DISCONNECT, true);
                                stopPPPoE();
                                Constant.wait(Status.WAIT_RASDIAL); // PPPoE 重撥延遲 確保 IP 更換
                                startPPPoE();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    appendDebugLog("發生例外狀況 : " + ex.Message);
                }
                Constant.wait(Status.WAIT_NEXT_TIME);
            }
        }

        private bool applyUIStart(bool automaticStart)
        {
            if (!config.canStart())
            {
                MessageBox.Show("設定不完整 無法啟動", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            WindowState = FormWindowState.Minimized;
            ShowInTaskbar = false;

            showBalloonTip(automaticStart ? Status.SHOW_AUTOMATIC_START : Status.SHOW_START);
            return true;
        }

        private async void btnStart_Click(object sender, EventArgs e)
        {
            if (checkThread != null)
            {
                btnStart.Enabled = false;
                await Task.Run(() =>
                {
                    checkThread.Abort();
                    while (checkThread.ThreadState != ThreadState.Aborted)
                    {
                        Thread.Sleep(1);
                    }
                    checkThread = null;
                });
                appendDebugLog("使用者已停止操作");
                updateUIStatus();
                btnStart.Enabled = true;
            }
            else
            {
                if (applyUIStart(false))
                {
                    startPPPoEThread(Status.MODE_WATCH_DOG);
                    updateUIStatus();
                }
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Hide();
            e.Cancel = true;
        }

        private void updateAutomaticStartWaitUI()
        {
            numAutomaticStartWait.Enabled = chkAutomaticStart.Checked;
        }

        private void chkAutomaticStart_CheckedChanged(object sender, EventArgs e)
        {
            updateAutomaticStartWaitUI();
        }

        private void tsmiSetting_Click(object sender, EventArgs e)
        {
            showForm();
        }

        private void tsmiPause_Click(object sender, EventArgs e)
        {
            tsmiPause.Checked = !tsmiPause.Checked;
        }

        private void tsmiExit_Click(object sender, EventArgs e)
        {
            exit();
        }

        private void exit()
        {
            niPermanent.Visible = false;
            Environment.Exit(Environment.ExitCode);
        }

        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                Hide();
            }
        }

        private void tsmiIPTool_Click(object sender, EventArgs e)
        {
            Program.ipTool.Show();
        }

        private void niPermanent_DoubleClick(object sender, EventArgs e)
        {
            showForm();
        }

        private void showForm()
        {
            Show();
            ShowInTaskbar = true;
            WindowState = FormWindowState.Normal;
        }

        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.PasswordChar = chkShowPassword.Checked ? (char)0 : '*';
        }

        private void cbSetting_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbSetting.SelectedIndex == -1)
            {
                return;
            }
            config.select = cbSetting.SelectedItem.ToString();
            updateUIStatus();
        }

        private void btnManageSetting_Click(object sender, EventArgs e)
        {
            using (SettingManager manager = new SettingManager())
            {
                manager.ShowDialog();
            }
            updateUIStatus();
        }

        private void chkShowDebugLog_CheckedChanged(object sender, EventArgs e)
        {
            Size originSize = new Size(340, 410);
            Size extendSize = new Size(610, 410);
            Size = chkShowDebugLog.Checked ? extendSize : originSize;
            labelDebugLog.Visible = txtDebugLog.Visible = chkShowDebugLog.Checked;
        }

        private void appendDebugLog(string data)
        {
            string date = DateTime.Now.ToString("yyyy - MM - dd tt hh : mm : ss");
            txtDebugLog.Invoke(new MethodInvoker(delegate ()
            {
                txtDebugLog.AppendText("[" + date + "] " + data + Environment.NewLine);
            }));
        }
    }
}