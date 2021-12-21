using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace AutoPPPoE
{
    public class Config
    {
        private const string CONFIG_PATH = "config.sx";
        private string _select;

        public IDictionary<string, Setting> setting
        {
            get;
            private set;
        }

        public string select
        {
            get
            {
                return _select;
            }
            set
            {
                checkSelect(setting, value);
                _select = value;
            }
        }

        private static void checkSelect(IDictionary<string, Setting> setting, string select)
        {
            if (select == null)
            {
                if (setting.Count > 0)
                {
                    throw new EWException("設定名稱無法指定為空");
                }
            }
            else
            {
                if (!setting.ContainsKey(select))
                {
                    throw new EWException("無效的設定選項");
                }
            }
        }

        public Setting current
        {
            get
            {
                return setting[_select];
            }
            set
            {
                if (value == null)
                {
                    throw new EWException("無效的設定值");
                }
                if (!setting.ContainsKey(_select))
                {
                    throw new EWException("無效的設定名稱");
                }
                setting[_select] = value;
            }
        }

        public Config()
        {
            setting = new Dictionary<string, Setting>();
            try
            {
                loadSetting();
            }
            catch (Exception ex)
            {
                MessageBox.Show("讀取設定檔案時發生例外狀況 : " + ex.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void loadSetting()
        {
            if (!File.Exists(CONFIG_PATH))
            {
                return;
            }
            using (FileStream fs = new FileStream(CONFIG_PATH, FileMode.Open, FileAccess.Read))
            {
                using (BinaryReader br = new BinaryReader(fs))
                {
                    IDictionary<string, Setting> raw = new Dictionary<string, Setting>();
                    string selectSetting = br.ReadBoolean() ? br.ReadString() : null;
                    while (fs.Position != fs.Length)
                    {
                        string settingName = br.ReadString();
                        if (string.IsNullOrWhiteSpace(settingName) || raw.ContainsKey(settingName))
                        {
                            throw new EWException("設定名稱錯誤");
                        }

                        string adapter = br.ReadString();
                        string name = br.ReadString();
                        string account = Util.removeWhiteSpace(br.ReadString());
                        string password = Util.removeWhiteSpace(SimpleAES.AESDecryptBase64(br.ReadString(), Constant.AES_KEY));
                        int fastPing = br.ReadInt32();
                        int slowPing = br.ReadInt32();
                        bool automaticStart = br.ReadBoolean();
                        int automaticStartWaitTime = br.ReadInt32();
                        Setting readSetting = new Setting(adapter, name, account, password, fastPing, slowPing, automaticStart, automaticStartWaitTime);
                        raw.Add(settingName, readSetting);
                    }
                    checkSelect(raw, selectSetting);
                    setting = raw;
                    _select = selectSetting;
                }
            }
        }

        public void saveSetting()
        {
            try
            {
                using (FileStream fs = new FileStream(CONFIG_PATH, FileMode.Create, FileAccess.Write))
                {
                    using (BinaryWriter bw = new BinaryWriter(fs))
                    {
                        if (select == null)
                        {
                            bw.Write(false);
                        }
                        else
                        {
                            bw.Write(true);
                            bw.Write(select);
                        }
                        foreach (KeyValuePair<string, Setting> config in setting)
                        {
                            bw.Write(config.Key);

                            Setting value = config.Value;
                            bw.Write(value.adapter);
                            bw.Write(value.name);
                            bw.Write(value.account);
                            bw.Write(SimpleAES.AESEncryptBase64(value.password, Constant.AES_KEY));
                            bw.Write(value.fastPing);
                            bw.Write(value.slowPing);
                            bw.Write(value.automaticStart);
                            bw.Write(value.automaticStartWaitTime);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("儲存設定檔案時發生例外狀況 : " + ex.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public bool canStart()
        {
            return !string.IsNullOrWhiteSpace(current.account) && !string.IsNullOrWhiteSpace(current.password);
        }
    }
}