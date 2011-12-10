namespace e4dnd2obsidianGUI
{
    partial class Form1
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.comboBoxCampaign = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.btn_login = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.btn_selFile = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.comboBoxCharacter = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_update_chr = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.button_create_chr = new System.Windows.Forms.Button();
            this.txt_json_input = new System.Windows.Forms.TextBox();
            this.txt_msg_output = new System.Windows.Forms.TextBox();
            this.btn_send_testdata = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.comboBoxCampaign);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(12, 113);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(281, 43);
            this.panel1.TabIndex = 1;
            // 
            // comboBoxCampaign
            // 
            this.comboBoxCampaign.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCampaign.FormattingEnabled = true;
            this.comboBoxCampaign.Location = new System.Drawing.Point(107, 11);
            this.comboBoxCampaign.Name = "comboBoxCampaign";
            this.comboBoxCampaign.Size = new System.Drawing.Size(163, 21);
            this.comboBoxCampaign.TabIndex = 0;
            this.comboBoxCampaign.SelectedIndexChanged += new System.EventHandler(this.comboBoxCampaign_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Kampagne";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 532);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(745, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(118, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // btn_login
            // 
            this.btn_login.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btn_login.Location = new System.Drawing.Point(12, 11);
            this.btn_login.Name = "btn_login";
            this.btn_login.Size = new System.Drawing.Size(90, 23);
            this.btn_login.TabIndex = 4;
            this.btn_login.Text = "Login Obsidian";
            this.btn_login.UseVisualStyleBackColor = true;
            this.btn_login.Click += new System.EventHandler(this.btn_login_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.comboBox2);
            this.panel2.Controls.Add(this.btn_selFile);
            this.panel2.Location = new System.Drawing.Point(12, 40);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(281, 67);
            this.panel2.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Datei";
            // 
            // comboBox2
            // 
            this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(47, 39);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(223, 21);
            this.comboBox2.TabIndex = 5;
            this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // btn_selFile
            // 
            this.btn_selFile.Location = new System.Drawing.Point(9, 9);
            this.btn_selFile.Name = "btn_selFile";
            this.btn_selFile.Size = new System.Drawing.Size(127, 23);
            this.btn_selFile.TabIndex = 4;
            this.btn_selFile.Text = "DnD4e Datei laden";
            this.btn_selFile.UseVisualStyleBackColor = true;
            this.btn_selFile.Click += new System.EventHandler(this.btn_selFile_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.comboBoxCharacter);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Location = new System.Drawing.Point(12, 162);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(281, 43);
            this.panel3.TabIndex = 2;
            // 
            // comboBoxCharacter
            // 
            this.comboBoxCharacter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCharacter.FormattingEnabled = true;
            this.comboBoxCharacter.Location = new System.Drawing.Point(107, 11);
            this.comboBoxCharacter.Name = "comboBoxCharacter";
            this.comboBoxCharacter.Size = new System.Drawing.Size(163, 21);
            this.comboBoxCharacter.TabIndex = 0;
            this.comboBoxCharacter.SelectedIndexChanged += new System.EventHandler(this.comboBoxCharacter_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Charakter";
            // 
            // btn_update_chr
            // 
            this.btn_update_chr.Location = new System.Drawing.Point(13, 212);
            this.btn_update_chr.Name = "btn_update_chr";
            this.btn_update_chr.Size = new System.Drawing.Size(107, 45);
            this.btn_update_chr.TabIndex = 7;
            this.btn_update_chr.Text = "Charakter aktualisieren";
            this.btn_update_chr.UseVisualStyleBackColor = true;
            this.btn_update_chr.Click += new System.EventHandler(this.btn_update_chr_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.InitialDirectory = global::e4dnd2obsidianGUI.Properties.Settings.Default.lastPath;
            // 
            // button_create_chr
            // 
            this.button_create_chr.Location = new System.Drawing.Point(126, 211);
            this.button_create_chr.Name = "button_create_chr";
            this.button_create_chr.Size = new System.Drawing.Size(107, 45);
            this.button_create_chr.TabIndex = 8;
            this.button_create_chr.Text = "Charakter erstellen";
            this.button_create_chr.UseVisualStyleBackColor = true;
            this.button_create_chr.Click += new System.EventHandler(this.button_create_chr_Click);
            // 
            // txt_json_input
            // 
            this.txt_json_input.Location = new System.Drawing.Point(299, 41);
            this.txt_json_input.Multiline = true;
            this.txt_json_input.Name = "txt_json_input";
            this.txt_json_input.Size = new System.Drawing.Size(434, 267);
            this.txt_json_input.TabIndex = 9;
            // 
            // txt_msg_output
            // 
            this.txt_msg_output.Location = new System.Drawing.Point(12, 314);
            this.txt_msg_output.Multiline = true;
            this.txt_msg_output.Name = "txt_msg_output";
            this.txt_msg_output.Size = new System.Drawing.Size(721, 216);
            this.txt_msg_output.TabIndex = 10;
            // 
            // btn_send_testdata
            // 
            this.btn_send_testdata.Location = new System.Drawing.Point(12, 263);
            this.btn_send_testdata.Name = "btn_send_testdata";
            this.btn_send_testdata.Size = new System.Drawing.Size(107, 45);
            this.btn_send_testdata.TabIndex = 11;
            this.btn_send_testdata.Text = "Testdaten senden";
            this.btn_send_testdata.UseVisualStyleBackColor = true;
            this.btn_send_testdata.Click += new System.EventHandler(this.btn_send_testdata_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(745, 554);
            this.Controls.Add(this.btn_send_testdata);
            this.Controls.Add(this.txt_msg_output);
            this.Controls.Add(this.txt_json_input);
            this.Controls.Add(this.button_create_chr);
            this.Controls.Add(this.btn_update_chr);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.btn_login);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btn_login;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ComboBox comboBoxCampaign;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_selFile;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ComboBox comboBoxCharacter;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_update_chr;
        private System.Windows.Forms.Button button_create_chr;
        private System.Windows.Forms.TextBox txt_json_input;
        private System.Windows.Forms.TextBox txt_msg_output;
        private System.Windows.Forms.Button btn_send_testdata;
    }
}

