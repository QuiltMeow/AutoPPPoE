using System;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoPPPoE
{
    public partial class IPTool : Form
    {
        private readonly HttpClient httpClient;

        public IPTool()
        {
            InitializeComponent();
            httpClient = new HttpClient();
        }

        private async void btnUpdatePublicIP_Click(object sender, EventArgs e)
        {
            btnUpdatePublicIP.Enabled = false;
            txtPublicIP.Text = "等待中 ...";
            await fetchPublicIP();
        }

        private async Task fetchPublicIP()
        {
            string ip = await Task.Run(() => getPublicIPAddress());
            if (string.IsNullOrWhiteSpace(ip))
            {
                ip = "取得外部 IP 時發生例外狀況";
            }
            txtPublicIP.Text = ip;
            btnUpdatePublicIP.Enabled = true;
        }

        private void btnUpdateAdapterList_Click(object sender, EventArgs e)
        {
            fetchAdapter();
            fetchProxy();
        }

        private void fetchProxy()
        {
            WinhttpCurrentUserIeProxyConfig config = new WinhttpCurrentUserIeProxyConfig();
            WinHttpGetIEProxyConfigForCurrentUser(ref config);
            string proxy = config.Proxy;
            txtProxy.Text = proxy != null ? proxy : "尚未設定";
        }

        private void fetchAdapter()
        {
            try
            {
                updateAdapterList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("讀取網路卡資訊時發生例外狀況 : " + ex.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void updateAdapterList()
        {
            clearAdapterInformationUI();
            cbAdapter.Items.Clear();
            foreach (NetworkInterface adapter in NetworkInterface.GetAllNetworkInterfaces())
            {
                cbAdapter.Items.Add(adapter.Name);
            }
            if (cbAdapter.Items.Count <= 0)
            {
                throw new EWException("找不到網路卡");
            }
            else
            {
                cbAdapter.SelectedIndex = 0;
            }
        }

        public string getPublicIPAddress()
        {
            string ret = null;
            byte attempt = 0;

            while (ret == null && attempt < Constant.MAX_FETCH_ATTEMPT)
            {
                try
                {
                    ++attempt;
                    string response = httpClient.GetStringAsync(Constant.IP_FETCH_URL).Result;
                    string candidatePublicIPAddress = response.Replace("\n", string.Empty);

                    if (!Util.isValidIP4Address(candidatePublicIPAddress))
                    {
                        throw new EWException("IP 位址回應格式錯誤 : " + response);
                    }
                    ret = candidatePublicIPAddress;
                }
                catch (Exception ex)
                {
                    if (attempt >= Constant.MAX_FETCH_ATTEMPT)
                    {
                        MessageBox.Show("取得外部 IP 位址時發生例外狀況 : " + ex.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            return ret;
        }

        private void clearAdapterInformationUI()
        {
            txtAdapterIP.Text = txtAdapterDesc.Text = string.Empty;
        }

        private void cbAdapter_SelectedIndexChanged(object sender, EventArgs e)
        {
            clearAdapterInformationUI();
            if (cbAdapter.SelectedIndex == -1)
            {
                return;
            }

            foreach (NetworkInterface adapter in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (adapter.Name == cbAdapter.SelectedItem.ToString())
                {
                    foreach (UnicastIPAddressInformation ip in adapter.GetIPProperties().UnicastAddresses)
                    {
                        if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
                        {
                            txtAdapterIP.Text = ip.Address.ToString();
                            break;
                        }
                    }
                    txtAdapterDesc.Text = adapter.Description;
                    break;
                }
            }
        }

        private async void IPTool_Load(object sender, EventArgs e)
        {
            fetchAdapter();
            fetchProxy();
            await fetchPublicIP();
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct WinhttpCurrentUserIeProxyConfig
        {
            [MarshalAs(UnmanagedType.Bool)]
            public bool AutoDetect;

            [MarshalAs(UnmanagedType.LPWStr)]
            public string AutoConfigUrl;

            [MarshalAs(UnmanagedType.LPWStr)]
            public string Proxy;

            [MarshalAs(UnmanagedType.LPWStr)]
            public string ProxyBypass;
        }

        [DllImport("winhttp.dll", SetLastError = true)]
        private static extern bool WinHttpGetIEProxyConfigForCurrentUser(ref WinhttpCurrentUserIeProxyConfig pointerProxyConfig);
    }
}