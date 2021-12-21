namespace AutoPPPoE
{
    partial class IPTool
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IPTool));
            this.txtAdapterDesc = new System.Windows.Forms.TextBox();
            this.btnUpdateAdapterList = new System.Windows.Forms.Button();
            this.labelAdapterIP = new System.Windows.Forms.Label();
            this.cbAdapter = new System.Windows.Forms.ComboBox();
            this.labelIPTool = new System.Windows.Forms.Label();
            this.labelPublicIP = new System.Windows.Forms.Label();
            this.txtPublicIP = new System.Windows.Forms.TextBox();
            this.txtAdapterIP = new System.Windows.Forms.TextBox();
            this.labelAdapterDesc = new System.Windows.Forms.Label();
            this.labelAdapterName = new System.Windows.Forms.Label();
            this.btnUpdatePublicIP = new System.Windows.Forms.Button();
            this.labelProxy = new System.Windows.Forms.Label();
            this.txtProxy = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtAdapterDesc
            // 
            this.txtAdapterDesc.Location = new System.Drawing.Point(112, 136);
            this.txtAdapterDesc.Name = "txtAdapterDesc";
            this.txtAdapterDesc.ReadOnly = true;
            this.txtAdapterDesc.Size = new System.Drawing.Size(184, 22);
            this.txtAdapterDesc.TabIndex = 7;
            // 
            // btnUpdateAdapterList
            // 
            this.btnUpdateAdapterList.Location = new System.Drawing.Point(27, 220);
            this.btnUpdateAdapterList.Name = "btnUpdateAdapterList";
            this.btnUpdateAdapterList.Size = new System.Drawing.Size(269, 23);
            this.btnUpdateAdapterList.TabIndex = 12;
            this.btnUpdateAdapterList.Text = "更新列表";
            this.btnUpdateAdapterList.UseVisualStyleBackColor = true;
            this.btnUpdateAdapterList.Click += new System.EventHandler(this.btnUpdateAdapterList_Click);
            // 
            // labelAdapterIP
            // 
            this.labelAdapterIP.AutoSize = true;
            this.labelAdapterIP.Location = new System.Drawing.Point(25, 167);
            this.labelAdapterIP.Name = "labelAdapterIP";
            this.labelAdapterIP.Size = new System.Drawing.Size(81, 12);
            this.labelAdapterIP.TabIndex = 8;
            this.labelAdapterIP.Text = "網路卡 IP 位址";
            // 
            // cbAdapter
            // 
            this.cbAdapter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAdapter.FormattingEnabled = true;
            this.cbAdapter.Items.AddRange(new object[] {
            "空"});
            this.cbAdapter.Location = new System.Drawing.Point(112, 110);
            this.cbAdapter.Name = "cbAdapter";
            this.cbAdapter.Size = new System.Drawing.Size(184, 20);
            this.cbAdapter.TabIndex = 5;
            this.cbAdapter.SelectedIndexChanged += new System.EventHandler(this.cbAdapter_SelectedIndexChanged);
            // 
            // labelIPTool
            // 
            this.labelIPTool.AutoSize = true;
            this.labelIPTool.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.labelIPTool.Location = new System.Drawing.Point(12, 9);
            this.labelIPTool.Name = "labelIPTool";
            this.labelIPTool.Size = new System.Drawing.Size(60, 20);
            this.labelIPTool.TabIndex = 0;
            this.labelIPTool.Text = "IP 工具";
            // 
            // labelPublicIP
            // 
            this.labelPublicIP.AutoSize = true;
            this.labelPublicIP.Location = new System.Drawing.Point(25, 43);
            this.labelPublicIP.Name = "labelPublicIP";
            this.labelPublicIP.Size = new System.Drawing.Size(69, 12);
            this.labelPublicIP.TabIndex = 1;
            this.labelPublicIP.Text = "外部 IP 位址";
            // 
            // txtPublicIP
            // 
            this.txtPublicIP.Location = new System.Drawing.Point(100, 40);
            this.txtPublicIP.Name = "txtPublicIP";
            this.txtPublicIP.ReadOnly = true;
            this.txtPublicIP.Size = new System.Drawing.Size(196, 22);
            this.txtPublicIP.TabIndex = 2;
            this.txtPublicIP.Text = "等待中 ...";
            // 
            // txtAdapterIP
            // 
            this.txtAdapterIP.Location = new System.Drawing.Point(112, 164);
            this.txtAdapterIP.Name = "txtAdapterIP";
            this.txtAdapterIP.ReadOnly = true;
            this.txtAdapterIP.Size = new System.Drawing.Size(184, 22);
            this.txtAdapterIP.TabIndex = 9;
            // 
            // labelAdapterDesc
            // 
            this.labelAdapterDesc.AutoSize = true;
            this.labelAdapterDesc.Location = new System.Drawing.Point(25, 139);
            this.labelAdapterDesc.Name = "labelAdapterDesc";
            this.labelAdapterDesc.Size = new System.Drawing.Size(65, 12);
            this.labelAdapterDesc.TabIndex = 6;
            this.labelAdapterDesc.Text = "網路卡描述";
            // 
            // labelAdapterName
            // 
            this.labelAdapterName.AutoSize = true;
            this.labelAdapterName.Location = new System.Drawing.Point(25, 113);
            this.labelAdapterName.Name = "labelAdapterName";
            this.labelAdapterName.Size = new System.Drawing.Size(65, 12);
            this.labelAdapterName.TabIndex = 4;
            this.labelAdapterName.Text = "網路卡名稱";
            // 
            // btnUpdatePublicIP
            // 
            this.btnUpdatePublicIP.Enabled = false;
            this.btnUpdatePublicIP.Location = new System.Drawing.Point(27, 68);
            this.btnUpdatePublicIP.Name = "btnUpdatePublicIP";
            this.btnUpdatePublicIP.Size = new System.Drawing.Size(269, 23);
            this.btnUpdatePublicIP.TabIndex = 3;
            this.btnUpdatePublicIP.Text = "更新外部 IP";
            this.btnUpdatePublicIP.UseVisualStyleBackColor = true;
            this.btnUpdatePublicIP.Click += new System.EventHandler(this.btnUpdatePublicIP_Click);
            // 
            // labelProxy
            // 
            this.labelProxy.AutoSize = true;
            this.labelProxy.Location = new System.Drawing.Point(25, 195);
            this.labelProxy.Name = "labelProxy";
            this.labelProxy.Size = new System.Drawing.Size(33, 12);
            this.labelProxy.TabIndex = 10;
            this.labelProxy.Text = "Proxy";
            // 
            // txtProxy
            // 
            this.txtProxy.Location = new System.Drawing.Point(112, 192);
            this.txtProxy.Name = "txtProxy";
            this.txtProxy.ReadOnly = true;
            this.txtProxy.Size = new System.Drawing.Size(184, 22);
            this.txtProxy.TabIndex = 11;
            // 
            // IPTool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(324, 256);
            this.Controls.Add(this.txtProxy);
            this.Controls.Add(this.labelProxy);
            this.Controls.Add(this.btnUpdatePublicIP);
            this.Controls.Add(this.labelAdapterName);
            this.Controls.Add(this.labelAdapterDesc);
            this.Controls.Add(this.txtAdapterIP);
            this.Controls.Add(this.txtPublicIP);
            this.Controls.Add(this.labelPublicIP);
            this.Controls.Add(this.labelIPTool);
            this.Controls.Add(this.txtAdapterDesc);
            this.Controls.Add(this.btnUpdateAdapterList);
            this.Controls.Add(this.labelAdapterIP);
            this.Controls.Add(this.cbAdapter);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.MaximizeBox = false;
            this.Name = "IPTool";
            this.Text = "IP 工具";
            this.Load += new System.EventHandler(this.IPTool_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtAdapterDesc;
        private System.Windows.Forms.Button btnUpdateAdapterList;
        private System.Windows.Forms.Label labelAdapterIP;
        private System.Windows.Forms.ComboBox cbAdapter;
        private System.Windows.Forms.Label labelIPTool;
        private System.Windows.Forms.Label labelPublicIP;
        private System.Windows.Forms.TextBox txtPublicIP;
        private System.Windows.Forms.TextBox txtAdapterIP;
        private System.Windows.Forms.Label labelAdapterDesc;
        private System.Windows.Forms.Label labelAdapterName;
        private System.Windows.Forms.Button btnUpdatePublicIP;
        private System.Windows.Forms.Label labelProxy;
        private System.Windows.Forms.TextBox txtProxy;
    }
}