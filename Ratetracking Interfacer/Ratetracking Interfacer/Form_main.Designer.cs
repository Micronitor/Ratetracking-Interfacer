namespace Ratetracking_Interfacer
{
    partial class Form_main
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
            this.comList = new System.Windows.Forms.ListBox();
            this.Baudrate = new System.Windows.Forms.TextBox();
            this.lb_Baudrate = new System.Windows.Forms.Label();
            this.ComStatus = new System.Windows.Forms.RichTextBox();
            this.bt_serialConnect = new System.Windows.Forms.Button();
            this.SERCOM_statusbar = new System.Windows.Forms.ProgressBar();
            this.ConsoleText = new System.Windows.Forms.RichTextBox();
            this.sendMessage = new System.Windows.Forms.TextBox();
            this.bt_send = new System.Windows.Forms.Button();
            this.Sent = new System.Windows.Forms.TextBox();
            this.bt_Clear_ConsoleText = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // comList
            // 
            this.comList.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.comList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.comList.FormattingEnabled = true;
            this.comList.Location = new System.Drawing.Point(12, 26);
            this.comList.Name = "comList";
            this.comList.Size = new System.Drawing.Size(75, 93);
            this.comList.TabIndex = 13;
            this.comList.SelectedIndexChanged += new System.EventHandler(this.comList_SelectedIndexChanged);
            // 
            // Baudrate
            // 
            this.Baudrate.BackColor = System.Drawing.SystemColors.ControlLight;
            this.Baudrate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Baudrate.Location = new System.Drawing.Point(12, 144);
            this.Baudrate.Name = "Baudrate";
            this.Baudrate.Size = new System.Drawing.Size(75, 20);
            this.Baudrate.TabIndex = 14;
            this.Baudrate.Text = "227790";
            this.Baudrate.TextChanged += new System.EventHandler(this.Baudrate_TextChanged);
            // 
            // lb_Baudrate
            // 
            this.lb_Baudrate.AutoSize = true;
            this.lb_Baudrate.Location = new System.Drawing.Point(12, 125);
            this.lb_Baudrate.Name = "lb_Baudrate";
            this.lb_Baudrate.Size = new System.Drawing.Size(50, 13);
            this.lb_Baudrate.TabIndex = 15;
            this.lb_Baudrate.Text = "Baudrate";
            // 
            // ComStatus
            // 
            this.ComStatus.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.ComStatus.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ComStatus.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.ComStatus.Location = new System.Drawing.Point(93, 26);
            this.ComStatus.Name = "ComStatus";
            this.ComStatus.ReadOnly = true;
            this.ComStatus.Size = new System.Drawing.Size(163, 93);
            this.ComStatus.TabIndex = 16;
            this.ComStatus.Text = "";
            // 
            // bt_serialConnect
            // 
            this.bt_serialConnect.BackColor = System.Drawing.SystemColors.Control;
            this.bt_serialConnect.Enabled = false;
            this.bt_serialConnect.Location = new System.Drawing.Point(93, 144);
            this.bt_serialConnect.Name = "bt_serialConnect";
            this.bt_serialConnect.Size = new System.Drawing.Size(163, 23);
            this.bt_serialConnect.TabIndex = 17;
            this.bt_serialConnect.Text = "Connect";
            this.bt_serialConnect.UseVisualStyleBackColor = false;
            this.bt_serialConnect.Click += new System.EventHandler(this.bt_serialConnect_Click);
            // 
            // SERCOM_statusbar
            // 
            this.SERCOM_statusbar.Location = new System.Drawing.Point(12, 3);
            this.SERCOM_statusbar.Name = "SERCOM_statusbar";
            this.SERCOM_statusbar.Size = new System.Drawing.Size(529, 20);
            this.SERCOM_statusbar.TabIndex = 30;
            // 
            // ConsoleText
            // 
            this.ConsoleText.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.ConsoleText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ConsoleText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ConsoleText.ForeColor = System.Drawing.SystemColors.WindowText;
            this.ConsoleText.Location = new System.Drawing.Point(12, 173);
            this.ConsoleText.MaxLength = 2000000;
            this.ConsoleText.Name = "ConsoleText";
            this.ConsoleText.ReadOnly = true;
            this.ConsoleText.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ConsoleText.Size = new System.Drawing.Size(530, 300);
            this.ConsoleText.TabIndex = 31;
            this.ConsoleText.Text = "";
            this.ConsoleText.TextChanged += new System.EventHandler(this.ConsoleText_TextChanged);
            // 
            // sendMessage
            // 
            this.sendMessage.BackColor = System.Drawing.SystemColors.ControlLight;
            this.sendMessage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.sendMessage.Enabled = false;
            this.sendMessage.Location = new System.Drawing.Point(12, 481);
            this.sendMessage.Name = "sendMessage";
            this.sendMessage.Size = new System.Drawing.Size(360, 20);
            this.sendMessage.TabIndex = 32;
            this.sendMessage.KeyDown += new System.Windows.Forms.KeyEventHandler(this.sendMessage_KeyDown);
            // 
            // bt_send
            // 
            this.bt_send.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.bt_send.Enabled = false;
            this.bt_send.Location = new System.Drawing.Point(385, 480);
            this.bt_send.Name = "bt_send";
            this.bt_send.Size = new System.Drawing.Size(75, 23);
            this.bt_send.TabIndex = 33;
            this.bt_send.Text = "Send";
            this.bt_send.UseVisualStyleBackColor = false;
            this.bt_send.Click += new System.EventHandler(this.bt_send_Click);
            // 
            // Sent
            // 
            this.Sent.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.Sent.Location = new System.Drawing.Point(11, 508);
            this.Sent.Name = "Sent";
            this.Sent.ReadOnly = true;
            this.Sent.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Sent.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.Sent.Size = new System.Drawing.Size(533, 20);
            this.Sent.TabIndex = 34;
            // 
            // bt_Clear_ConsoleText
            // 
            this.bt_Clear_ConsoleText.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.bt_Clear_ConsoleText.Enabled = false;
            this.bt_Clear_ConsoleText.Location = new System.Drawing.Point(470, 480);
            this.bt_Clear_ConsoleText.Name = "bt_Clear_ConsoleText";
            this.bt_Clear_ConsoleText.Size = new System.Drawing.Size(75, 23);
            this.bt_Clear_ConsoleText.TabIndex = 35;
            this.bt_Clear_ConsoleText.Text = "Clear";
            this.bt_Clear_ConsoleText.UseVisualStyleBackColor = false;
            this.bt_Clear_ConsoleText.Click += new System.EventHandler(this.bt_Clear_ConsoleText_Click);
            // 
            // Form_main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.ClientSize = new System.Drawing.Size(884, 561);
            this.Controls.Add(this.bt_Clear_ConsoleText);
            this.Controls.Add(this.Sent);
            this.Controls.Add(this.bt_send);
            this.Controls.Add(this.sendMessage);
            this.Controls.Add(this.ConsoleText);
            this.Controls.Add(this.SERCOM_statusbar);
            this.Controls.Add(this.bt_serialConnect);
            this.Controls.Add(this.ComStatus);
            this.Controls.Add(this.lb_Baudrate);
            this.Controls.Add(this.Baudrate);
            this.Controls.Add(this.comList);
            this.Name = "Form_main";
            this.Text = "Ratetracking Interfacer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_main_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox comList;
        private System.Windows.Forms.TextBox Baudrate;
        private System.Windows.Forms.Label lb_Baudrate;
        private System.Windows.Forms.RichTextBox ComStatus;
        private System.Windows.Forms.Button bt_serialConnect;
        private System.Windows.Forms.ProgressBar SERCOM_statusbar;
        private System.Windows.Forms.RichTextBox ConsoleText;
        private System.Windows.Forms.TextBox sendMessage;
        private System.Windows.Forms.Button bt_send;
        private System.Windows.Forms.TextBox Sent;
        private System.Windows.Forms.Button bt_Clear_ConsoleText;
    }
}

