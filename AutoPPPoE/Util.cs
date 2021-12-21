using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace AutoPPPoE
{
    public static class Util
    {
        public static void forceUpdateNumericUpDownValue(NumericUpDown control, decimal value)
        {
            decimal pre = value + 1;
            if (pre > control.Maximum)
            {
                pre = value - 1;
            }
            control.Value = pre;
            control.Value = value;
        }

        public static void optionSelect(ComboBox cb, string option)
        {
            if (cb.Items.Count <= 0)
            {
                throw new EWException("選單不能為空");
            }

            for (int i = 0; i < cb.Items.Count; ++i)
            {
                if (cb.Items[i].ToString() == option)
                {
                    cb.SelectedIndex = i;
                    return;
                }
            }
            throw new EWException("找不到指定項目 : " + option);
        }

        public static void loadSettingNameUI(ComboBox cb)
        {
            cb.Items.Clear();
            Config config = Program.config;
            foreach (KeyValuePair<string, Setting> data in config.setting)
            {
                cb.Items.Add(data.Key);
            }
            if (cb.Items.Count > 0)
            {
                optionSelect(cb, config.select);
            }
        }

        public static Setting generateDefaultSetting()
        {
            const int DEFAULT_FAST_PING_WAIT_TIME = 750;
            const int DEFAULT_SLOW_PING_WAIT_TIME = 2500;
            const int DEFAULT_AUTOMATIC_START_WAIT_TIME = 5;

            AdapterManager adapter = Program.adapter;
            return new Setting(adapter.adapterName[0], adapter.rasName[0], string.Empty, string.Empty, DEFAULT_FAST_PING_WAIT_TIME, DEFAULT_SLOW_PING_WAIT_TIME, false, DEFAULT_AUTOMATIC_START_WAIT_TIME);
        }

        public static bool isValidModifySettingName(string name)
        {
            return !string.IsNullOrWhiteSpace(name) && !Program.config.setting.ContainsKey(name);
        }

        public static string removeWhiteSpace(string data)
        {
            return Regex.Replace(data, "\\s+", string.Empty);
        }

        public static bool isValidIP4Address(string data)
        {
            if (string.IsNullOrWhiteSpace(data))
            {
                return false;
            }

            string[] split = data.Split('.');
            if (split.Length != 4)
            {
                return false;
            }

            byte parse;
            return split.All(value => byte.TryParse(value, out parse));
        }
    }
}