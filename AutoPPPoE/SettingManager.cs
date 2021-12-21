using System;
using System.Windows.Forms;

namespace AutoPPPoE
{
    public partial class SettingManager : Form
    {
        private static readonly Config config = Program.config;

        public SettingManager()
        {
            InitializeComponent();
            loadSettingNameUI();
        }

        private void loadSettingNameUI()
        {
            Util.loadSettingNameUI(cbSetting);
            Util.loadSettingNameUI(Program.main.cbSetting);
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            string name = txtName.Text;
            if (!Util.isValidModifySettingName(name))
            {
                MessageBox.Show("請輸入有效的設定名稱", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            config.setting.Add(name, Util.generateDefaultSetting());
            if (config.setting.Count == 1)
            {
                config.select = name;
            }
            config.saveSetting();
            loadSettingNameUI();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            string newName = txtName.Text;
            if (cbSetting.SelectedIndex == -1)
            {
                MessageBox.Show("請指定設定名稱", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (!Util.isValidModifySettingName(newName))
            {
                MessageBox.Show("請輸入有效的設定名稱", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string select = cbSetting.SelectedItem.ToString();
            Setting setting = config.setting[select];
            config.setting.Remove(select);
            config.setting.Add(newName, setting);
            if (select == config.select)
            {
                config.select = newName;
            }
            config.saveSetting();
            loadSettingNameUI();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (cbSetting.SelectedIndex == -1)
            {
                MessageBox.Show("請指定設定名稱", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            config.setting.Remove(cbSetting.SelectedItem.ToString());
            if (config.setting.Count <= 0)
            {
                config.select = null;
            }
            config.saveSetting();
            loadSettingNameUI();
        }
    }
}