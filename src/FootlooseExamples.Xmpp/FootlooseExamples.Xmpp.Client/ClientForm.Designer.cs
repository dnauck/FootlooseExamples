namespace FootlooseExamples.Xmpp.Client
{
    partial class ClientForm
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
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.SendPresenceButton = new System.Windows.Forms.Button();
            this.UnavailableStatusRadioButton = new System.Windows.Forms.RadioButton();
            this.TemporarilyUnavailableStatusRadioButton = new System.Windows.Forms.RadioButton();
            this.BusyStatusRadioButton = new System.Windows.Forms.RadioButton();
            this.OnlineStatusRadioButton = new System.Windows.Forms.RadioButton();
            this.label6 = new System.Windows.Forms.Label();
            this.StatusInfoTextBox = new System.Windows.Forms.TextBox();
            this.PriorityNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.EndpointIdentityView = new Footloose.WinForms.EndpointIdentityView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.AutoServerResolveCheckBox = new System.Windows.Forms.CheckBox();
            this.ServerAddressTextBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.ConnectButton = new System.Windows.Forms.Button();
            this.EndpointIdTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.DisconnectButton = new System.Windows.Forms.Button();
            this.PasswortTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.ServerTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.UserNameTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.CustomersDataGridView = new System.Windows.Forms.DataGridView();
            this.UseServiceDiscoCheckBox = new System.Windows.Forms.CheckBox();
            this.GetCustomersButton = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.ServiceUriTextBox = new System.Windows.Forms.TextBox();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PriorityNumericUpDown)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CustomersDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.SendPresenceButton);
            this.groupBox3.Controls.Add(this.UnavailableStatusRadioButton);
            this.groupBox3.Controls.Add(this.TemporarilyUnavailableStatusRadioButton);
            this.groupBox3.Controls.Add(this.BusyStatusRadioButton);
            this.groupBox3.Controls.Add(this.OnlineStatusRadioButton);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.StatusInfoTextBox);
            this.groupBox3.Controls.Add(this.PriorityNumericUpDown);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Location = new System.Drawing.Point(13, 159);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(599, 119);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "My presence information";
            // 
            // SendPresenceButton
            // 
            this.SendPresenceButton.Location = new System.Drawing.Point(437, 88);
            this.SendPresenceButton.Name = "SendPresenceButton";
            this.SendPresenceButton.Size = new System.Drawing.Size(155, 23);
            this.SendPresenceButton.TabIndex = 20;
            this.SendPresenceButton.Text = "Send Presence";
            this.SendPresenceButton.UseVisualStyleBackColor = true;
            this.SendPresenceButton.Click += new System.EventHandler(this.SendPresenceButton_Click);
            // 
            // UnavailableStatusRadioButton
            // 
            this.UnavailableStatusRadioButton.AutoSize = true;
            this.UnavailableStatusRadioButton.Location = new System.Drawing.Point(454, 55);
            this.UnavailableStatusRadioButton.Name = "UnavailableStatusRadioButton";
            this.UnavailableStatusRadioButton.Size = new System.Drawing.Size(81, 17);
            this.UnavailableStatusRadioButton.TabIndex = 19;
            this.UnavailableStatusRadioButton.Text = "Unavailable";
            this.UnavailableStatusRadioButton.UseVisualStyleBackColor = true;
            // 
            // TemporarilyUnavailableStatusRadioButton
            // 
            this.TemporarilyUnavailableStatusRadioButton.AutoSize = true;
            this.TemporarilyUnavailableStatusRadioButton.Location = new System.Drawing.Point(454, 33);
            this.TemporarilyUnavailableStatusRadioButton.Name = "TemporarilyUnavailableStatusRadioButton";
            this.TemporarilyUnavailableStatusRadioButton.Size = new System.Drawing.Size(138, 17);
            this.TemporarilyUnavailableStatusRadioButton.TabIndex = 18;
            this.TemporarilyUnavailableStatusRadioButton.Text = "Temporarily Unavailable";
            this.TemporarilyUnavailableStatusRadioButton.UseVisualStyleBackColor = true;
            // 
            // BusyStatusRadioButton
            // 
            this.BusyStatusRadioButton.AutoSize = true;
            this.BusyStatusRadioButton.Location = new System.Drawing.Point(363, 57);
            this.BusyStatusRadioButton.Name = "BusyStatusRadioButton";
            this.BusyStatusRadioButton.Size = new System.Drawing.Size(48, 17);
            this.BusyStatusRadioButton.TabIndex = 17;
            this.BusyStatusRadioButton.Text = "Busy";
            this.BusyStatusRadioButton.UseVisualStyleBackColor = true;
            // 
            // OnlineStatusRadioButton
            // 
            this.OnlineStatusRadioButton.AutoSize = true;
            this.OnlineStatusRadioButton.Checked = true;
            this.OnlineStatusRadioButton.Location = new System.Drawing.Point(363, 33);
            this.OnlineStatusRadioButton.Name = "OnlineStatusRadioButton";
            this.OnlineStatusRadioButton.Size = new System.Drawing.Size(55, 17);
            this.OnlineStatusRadioButton.TabIndex = 16;
            this.OnlineStatusRadioButton.TabStop = true;
            this.OnlineStatusRadioButton.Text = "Online";
            this.OnlineStatusRadioButton.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(14, 59);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(61, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "Status Info:";
            // 
            // StatusInfoTextBox
            // 
            this.StatusInfoTextBox.Location = new System.Drawing.Point(83, 56);
            this.StatusInfoTextBox.Name = "StatusInfoTextBox";
            this.StatusInfoTextBox.Size = new System.Drawing.Size(252, 20);
            this.StatusInfoTextBox.TabIndex = 14;
            // 
            // PriorityNumericUpDown
            // 
            this.PriorityNumericUpDown.Location = new System.Drawing.Point(83, 29);
            this.PriorityNumericUpDown.Maximum = new decimal(new int[] {
            128,
            0,
            0,
            0});
            this.PriorityNumericUpDown.Minimum = new decimal(new int[] {
            128,
            0,
            0,
            -2147483648});
            this.PriorityNumericUpDown.Name = "PriorityNumericUpDown";
            this.PriorityNumericUpDown.Size = new System.Drawing.Size(100, 20);
            this.PriorityNumericUpDown.TabIndex = 13;
            this.PriorityNumericUpDown.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(34, 31);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Priority:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.EndpointIdentityView);
            this.groupBox2.Location = new System.Drawing.Point(618, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(245, 637);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Endpoints";
            // 
            // EndpointIdentityView
            // 
            this.EndpointIdentityView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.EndpointIdentityView.Location = new System.Drawing.Point(3, 16);
            this.EndpointIdentityView.Name = "EndpointIdentityView";
            this.EndpointIdentityView.Size = new System.Drawing.Size(239, 618);
            this.EndpointIdentityView.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.AutoServerResolveCheckBox);
            this.groupBox1.Controls.Add(this.ServerAddressTextBox);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.ConnectButton);
            this.groupBox1.Controls.Add(this.EndpointIdTextBox);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.DisconnectButton);
            this.groupBox1.Controls.Add(this.PasswortTextBox);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.ServerTextBox);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.UserNameTextBox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(13, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(599, 131);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Settings";
            // 
            // AutoServerResolveCheckBox
            // 
            this.AutoServerResolveCheckBox.AutoSize = true;
            this.AutoServerResolveCheckBox.Checked = true;
            this.AutoServerResolveCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.AutoServerResolveCheckBox.Location = new System.Drawing.Point(202, 99);
            this.AutoServerResolveCheckBox.Name = "AutoServerResolveCheckBox";
            this.AutoServerResolveCheckBox.Size = new System.Drawing.Size(219, 17);
            this.AutoServerResolveCheckBox.TabIndex = 15;
            this.AutoServerResolveCheckBox.Text = "Enable automatic server adress resolving";
            this.AutoServerResolveCheckBox.UseVisualStyleBackColor = true;
            this.AutoServerResolveCheckBox.CheckedChanged += new System.EventHandler(this.AutoServerResolveCheckBox_CheckedChanged);
            // 
            // ServerAddressTextBox
            // 
            this.ServerAddressTextBox.Location = new System.Drawing.Point(93, 97);
            this.ServerAddressTextBox.Name = "ServerAddressTextBox";
            this.ServerAddressTextBox.Size = new System.Drawing.Size(100, 20);
            this.ServerAddressTextBox.TabIndex = 14;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(5, 100);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(82, 13);
            this.label8.TabIndex = 13;
            this.label8.Text = "Server Address:";
            // 
            // ConnectButton
            // 
            this.ConnectButton.Location = new System.Drawing.Point(437, 95);
            this.ConnectButton.Name = "ConnectButton";
            this.ConnectButton.Size = new System.Drawing.Size(75, 23);
            this.ConnectButton.TabIndex = 12;
            this.ConnectButton.Text = "Connect";
            this.ConnectButton.UseVisualStyleBackColor = true;
            this.ConnectButton.Click += new System.EventHandler(this.ConnectButton_Click);
            // 
            // EndpointIdTextBox
            // 
            this.EndpointIdTextBox.Location = new System.Drawing.Point(461, 32);
            this.EndpointIdTextBox.Name = "EndpointIdTextBox";
            this.EndpointIdTextBox.Size = new System.Drawing.Size(131, 20);
            this.EndpointIdTextBox.TabIndex = 8;
            this.EndpointIdTextBox.Text = "Footloose-Client-1";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(360, 35);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(95, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Endpoint Identifier:";
            // 
            // DisconnectButton
            // 
            this.DisconnectButton.Enabled = false;
            this.DisconnectButton.Location = new System.Drawing.Point(518, 95);
            this.DisconnectButton.Name = "DisconnectButton";
            this.DisconnectButton.Size = new System.Drawing.Size(75, 23);
            this.DisconnectButton.TabIndex = 6;
            this.DisconnectButton.Text = "Disconnect";
            this.DisconnectButton.UseVisualStyleBackColor = true;
            this.DisconnectButton.Click += new System.EventHandler(this.DisconnectButton_Click);
            // 
            // PasswortTextBox
            // 
            this.PasswortTextBox.Location = new System.Drawing.Point(93, 64);
            this.PasswortTextBox.Name = "PasswortTextBox";
            this.PasswortTextBox.PasswordChar = '*';
            this.PasswortTextBox.Size = new System.Drawing.Size(100, 20);
            this.PasswortTextBox.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(31, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Password:";
            // 
            // ServerTextBox
            // 
            this.ServerTextBox.Location = new System.Drawing.Point(223, 32);
            this.ServerTextBox.Name = "ServerTextBox";
            this.ServerTextBox.Size = new System.Drawing.Size(122, 20);
            this.ServerTextBox.TabIndex = 3;
            this.ServerTextBox.Text = "jabber.org";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(199, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(18, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "@";
            // 
            // UserNameTextBox
            // 
            this.UserNameTextBox.Location = new System.Drawing.Point(93, 32);
            this.UserNameTextBox.Name = "UserNameTextBox";
            this.UserNameTextBox.Size = new System.Drawing.Size(100, 20);
            this.UserNameTextBox.TabIndex = 1;
            this.UserNameTextBox.Text = "username";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Jabber Id:";
            // 
            // CustomersDataGridView
            // 
            this.CustomersDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.CustomersDataGridView.Location = new System.Drawing.Point(13, 284);
            this.CustomersDataGridView.Name = "CustomersDataGridView";
            this.CustomersDataGridView.Size = new System.Drawing.Size(592, 309);
            this.CustomersDataGridView.TabIndex = 7;
            // 
            // UseServiceDiscoCheckBox
            // 
            this.UseServiceDiscoCheckBox.AutoSize = true;
            this.UseServiceDiscoCheckBox.Checked = true;
            this.UseServiceDiscoCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.UseServiceDiscoCheckBox.Location = new System.Drawing.Point(413, 599);
            this.UseServiceDiscoCheckBox.Name = "UseServiceDiscoCheckBox";
            this.UseServiceDiscoCheckBox.Size = new System.Drawing.Size(193, 17);
            this.UseServiceDiscoCheckBox.TabIndex = 8;
            this.UseServiceDiscoCheckBox.Text = "Enable automatic service discovery";
            this.UseServiceDiscoCheckBox.UseVisualStyleBackColor = true;
            this.UseServiceDiscoCheckBox.CheckedChanged += new System.EventHandler(this.UseServiceDiscoCheckBox_CheckedChanged);
            // 
            // GetCustomersButton
            // 
            this.GetCustomersButton.Location = new System.Drawing.Point(413, 623);
            this.GetCustomersButton.Name = "GetCustomersButton";
            this.GetCustomersButton.Size = new System.Drawing.Size(192, 23);
            this.GetCustomersButton.TabIndex = 9;
            this.GetCustomersButton.Text = "Get Customers";
            this.GetCustomersButton.UseVisualStyleBackColor = true;
            this.GetCustomersButton.Click += new System.EventHandler(this.GetCustomersButton_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(34, 602);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(62, 13);
            this.label7.TabIndex = 10;
            this.label7.Text = "Service Uri:";
            // 
            // ServiceUriTextBox
            // 
            this.ServiceUriTextBox.Enabled = false;
            this.ServiceUriTextBox.Location = new System.Drawing.Point(96, 599);
            this.ServiceUriTextBox.Name = "ServiceUriTextBox";
            this.ServiceUriTextBox.Size = new System.Drawing.Size(252, 20);
            this.ServiceUriTextBox.TabIndex = 11;
            this.ServiceUriTextBox.Text = "xmpp:username@jabber.org/Footloose-Service-1";
            // 
            // ClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(871, 661);
            this.Controls.Add(this.ServiceUriTextBox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.GetCustomersButton);
            this.Controls.Add(this.UseServiceDiscoCheckBox);
            this.Controls.Add(this.CustomersDataGridView);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "ClientForm";
            this.Text = "Footloose Client";
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PriorityNumericUpDown)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CustomersDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button SendPresenceButton;
        private System.Windows.Forms.RadioButton UnavailableStatusRadioButton;
        private System.Windows.Forms.RadioButton TemporarilyUnavailableStatusRadioButton;
        private System.Windows.Forms.RadioButton BusyStatusRadioButton;
        private System.Windows.Forms.RadioButton OnlineStatusRadioButton;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox StatusInfoTextBox;
        private System.Windows.Forms.NumericUpDown PriorityNumericUpDown;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox2;
        private Footloose.WinForms.EndpointIdentityView EndpointIdentityView;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button ConnectButton;
        private System.Windows.Forms.TextBox EndpointIdTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button DisconnectButton;
        private System.Windows.Forms.TextBox PasswortTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox ServerTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox UserNameTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView CustomersDataGridView;
        private System.Windows.Forms.CheckBox UseServiceDiscoCheckBox;
        private System.Windows.Forms.Button GetCustomersButton;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox ServiceUriTextBox;
        private System.Windows.Forms.TextBox ServerAddressTextBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox AutoServerResolveCheckBox;
    }
}

