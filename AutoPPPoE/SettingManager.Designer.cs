namespace AutoPPPoE
{
    partial class SettingManager
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingManager));
            this.btnNew = new System.Windows.Forms.Button();
            this.labelSelectSetting = new System.Windows.Forms.Label();
            this.labelSettingName = new System.Windows.Forms.Label();
            this.cbSetting = new System.Windows.Forms.ComboBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnNew
            // 
            this.btnNew.Location = new System.Drawing.Point(17, 75);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(65, 23);
            this.btnNew.TabIndex = 4;
            this.btnNew.Text = "新增";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // labelSelectSetting
            // 
            this.labelSelectSetting.AutoSize = true;
            this.labelSelectSetting.Location = new System.Drawing.Point(15, 18);
            this.labelSelectSetting.Name = "labelSelectSetting";
            this.labelSelectSetting.Size = new System.Drawing.Size(77, 12);
            this.labelSelectSetting.TabIndex = 0;
            this.labelSelectSetting.Text = "選擇設定檔案";
            // 
            // labelSettingName
            // 
            this.labelSettingName.AutoSize = true;
            this.labelSettingName.Location = new System.Drawing.Point(15, 44);
            this.labelSettingName.Name = "labelSettingName";
            this.labelSettingName.Size = new System.Drawing.Size(77, 12);
            this.labelSettingName.TabIndex = 2;
            this.labelSettingName.Text = "設定檔案名稱";
            // 
            // cbSetting
            // 
            this.cbSetting.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSetting.FormattingEnabled = true;
            this.cbSetting.Location = new System.Drawing.Point(105, 15);
            this.cbSetting.Name = "cbSetting";
            this.cbSetting.Size = new System.Drawing.Size(150, 20);
            this.cbSetting.TabIndex = 1;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(105, 41);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(150, 22);
            this.txtName.TabIndex = 3;
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(105, 75);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(65, 23);
            this.btnEdit.TabIndex = 5;
            this.btnEdit.Text = "更名";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(190, 75);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(65, 23);
            this.btnRemove.TabIndex = 6;
            this.btnRemove.Text = "移除";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // SettingManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(274, 111);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.cbSetting);
            this.Controls.Add(this.labelSettingName);
            this.Controls.Add(this.labelSelectSetting);
            this.Controls.Add(this.btnNew);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.MaximizeBox = false;
            this.Name = "SettingManager";
            this.Text = "設定檔案管理";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Label labelSelectSetting;
        private System.Windows.Forms.Label labelSettingName;
        private System.Windows.Forms.ComboBox cbSetting;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnRemove;
    }
}