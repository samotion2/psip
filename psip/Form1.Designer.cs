
namespace psip
{
    partial class Form1
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.resetButton = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.clearButton = new System.Windows.Forms.Button();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.timerButton = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.filterButton = new System.Windows.Forms.Button();
            this.src_ip_text = new System.Windows.Forms.TextBox();
            this.dst_ip_text = new System.Windows.Forms.TextBox();
            this.dst_mac_text = new System.Windows.Forms.TextBox();
            this.src_mac_text = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.delSelectedButton = new System.Windows.Forms.Button();
            this.clearFilterButton = new System.Windows.Forms.Button();
            this.dst_port_text = new System.Windows.Forms.TextBox();
            this.src_port_text = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.protocolCombo = new System.Windows.Forms.ComboBox();
            this.wrongInputLabel = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.device_list_text = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(163, 51);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(118, 147);
            this.textBox1.TabIndex = 1;
            // 
            // resetButton
            // 
            this.resetButton.Location = new System.Drawing.Point(27, 116);
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(75, 23);
            this.resetButton.TabIndex = 3;
            this.resetButton.Text = "Reset";
            this.resetButton.UseVisualStyleBackColor = true;
            this.resetButton.Click += new System.EventHandler(this.resetButton_Click);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(654, 51);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(118, 147);
            this.textBox2.TabIndex = 4;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(530, 51);
            this.textBox3.Multiline = true;
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(118, 147);
            this.textBox3.TabIndex = 5;
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(287, 51);
            this.textBox4.Multiline = true;
            this.textBox4.Name = "textBox4";
            this.textBox4.ReadOnly = true;
            this.textBox4.Size = new System.Drawing.Size(118, 147);
            this.textBox4.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(262, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 18);
            this.label1.TabIndex = 7;
            this.label1.Text = "Port0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(626, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 18);
            this.label2.TabIndex = 8;
            this.label2.Text = "Port2";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(213, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(19, 17);
            this.label3.TabIndex = 9;
            this.label3.Text = "In";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(333, 31);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 17);
            this.label4.TabIndex = 10;
            this.label4.Text = "Out";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(579, 31);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(19, 17);
            this.label5.TabIndex = 11;
            this.label5.Text = "In";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(697, 31);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(31, 17);
            this.label6.TabIndex = 12;
            this.label6.Text = "Out";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(163, 261);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(609, 197);
            this.dataGridView1.TabIndex = 13;
            // 
            // clearButton
            // 
            this.clearButton.Location = new System.Drawing.Point(27, 418);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(75, 23);
            this.clearButton.TabIndex = 14;
            this.clearButton.Text = "Clear";
            this.clearButton.UseVisualStyleBackColor = true;
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // textBox5
            // 
            this.textBox5.AccessibleDescription = "";
            this.textBox5.AccessibleName = "";
            this.textBox5.ForeColor = System.Drawing.Color.Gray;
            this.textBox5.Location = new System.Drawing.Point(27, 301);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(75, 22);
            this.textBox5.TabIndex = 15;
            this.textBox5.Text = "Duration";
            this.textBox5.MouseDown += new System.Windows.Forms.MouseEventHandler(this.textBox5_MouseDown);
            // 
            // timerButton
            // 
            this.timerButton.Location = new System.Drawing.Point(27, 272);
            this.timerButton.Name = "timerButton";
            this.timerButton.Size = new System.Drawing.Size(75, 23);
            this.timerButton.TabIndex = 16;
            this.timerButton.Text = "Set timer";
            this.timerButton.UseVisualStyleBackColor = true;
            this.timerButton.Click += new System.EventHandler(this.timerButton_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(424, 232);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(90, 18);
            this.label7.TabIndex = 17;
            this.label7.Text = "CAM Table";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "In",
            "Out"});
            this.comboBox1.Location = new System.Drawing.Point(163, 519);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 24);
            this.comboBox1.TabIndex = 18;
            // 
            // filterButton
            // 
            this.filterButton.Location = new System.Drawing.Point(27, 519);
            this.filterButton.Name = "filterButton";
            this.filterButton.Size = new System.Drawing.Size(75, 23);
            this.filterButton.TabIndex = 20;
            this.filterButton.Text = "Add filter";
            this.filterButton.UseVisualStyleBackColor = true;
            this.filterButton.Click += new System.EventHandler(this.filterButton_Click);
            // 
            // src_ip_text
            // 
            this.src_ip_text.Location = new System.Drawing.Point(417, 521);
            this.src_ip_text.Name = "src_ip_text";
            this.src_ip_text.Size = new System.Drawing.Size(121, 22);
            this.src_ip_text.TabIndex = 21;
            this.src_ip_text.Text = "any";
            // 
            // dst_ip_text
            // 
            this.dst_ip_text.Location = new System.Drawing.Point(544, 521);
            this.dst_ip_text.Name = "dst_ip_text";
            this.dst_ip_text.Size = new System.Drawing.Size(121, 22);
            this.dst_ip_text.TabIndex = 22;
            this.dst_ip_text.Text = "any";
            // 
            // dst_mac_text
            // 
            this.dst_mac_text.Location = new System.Drawing.Point(800, 521);
            this.dst_mac_text.Name = "dst_mac_text";
            this.dst_mac_text.Size = new System.Drawing.Size(121, 22);
            this.dst_mac_text.TabIndex = 24;
            this.dst_mac_text.Text = "any";
            // 
            // src_mac_text
            // 
            this.src_mac_text.Location = new System.Drawing.Point(673, 521);
            this.src_mac_text.Name = "src_mac_text";
            this.src_mac_text.Size = new System.Drawing.Size(121, 22);
            this.src_mac_text.TabIndex = 23;
            this.src_mac_text.Text = "any";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(453, 500);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(49, 17);
            this.label8.TabIndex = 25;
            this.label8.Text = "Src_IP";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(581, 500);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(49, 17);
            this.label9.TabIndex = 26;
            this.label9.Text = "Dst_IP";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(703, 501);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(66, 17);
            this.label10.TabIndex = 27;
            this.label10.Text = "Src_MAC";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(824, 501);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(66, 17);
            this.label11.TabIndex = 28;
            this.label11.Text = "Dst_MAC";
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(163, 566);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowHeadersWidth = 51;
            this.dataGridView2.RowTemplate.Height = 24;
            this.dataGridView2.Size = new System.Drawing.Size(1337, 150);
            this.dataGridView2.TabIndex = 29;
            // 
            // comboBox2
            // 
            this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "0",
            "2"});
            this.comboBox2.Location = new System.Drawing.Point(290, 520);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(121, 24);
            this.comboBox2.TabIndex = 30;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(309, 500);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(86, 17);
            this.label12.TabIndex = 31;
            this.label12.Text = "Port number";
            // 
            // delSelectedButton
            // 
            this.delSelectedButton.Location = new System.Drawing.Point(15, 620);
            this.delSelectedButton.Name = "delSelectedButton";
            this.delSelectedButton.Size = new System.Drawing.Size(107, 23);
            this.delSelectedButton.TabIndex = 32;
            this.delSelectedButton.Text = "Del Selected";
            this.delSelectedButton.UseVisualStyleBackColor = true;
            this.delSelectedButton.Click += new System.EventHandler(this.delSelectedButton_Click);
            // 
            // clearFilterButton
            // 
            this.clearFilterButton.Location = new System.Drawing.Point(27, 693);
            this.clearFilterButton.Name = "clearFilterButton";
            this.clearFilterButton.Size = new System.Drawing.Size(75, 23);
            this.clearFilterButton.TabIndex = 33;
            this.clearFilterButton.Text = "Clear";
            this.clearFilterButton.UseVisualStyleBackColor = true;
            this.clearFilterButton.Click += new System.EventHandler(this.clearFilterButton_Click);
            // 
            // dst_port_text
            // 
            this.dst_port_text.Location = new System.Drawing.Point(1182, 521);
            this.dst_port_text.Name = "dst_port_text";
            this.dst_port_text.Size = new System.Drawing.Size(121, 22);
            this.dst_port_text.TabIndex = 36;
            this.dst_port_text.Text = "any";
            // 
            // src_port_text
            // 
            this.src_port_text.Location = new System.Drawing.Point(1055, 521);
            this.src_port_text.Name = "src_port_text";
            this.src_port_text.Size = new System.Drawing.Size(121, 22);
            this.src_port_text.TabIndex = 35;
            this.src_port_text.Text = "any";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(956, 500);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(60, 17);
            this.label13.TabIndex = 37;
            this.label13.Text = "Protocol";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(1084, 500);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(62, 17);
            this.label14.TabIndex = 38;
            this.label14.Text = "Src_port";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(1211, 500);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(62, 17);
            this.label15.TabIndex = 39;
            this.label15.Text = "Dst_port";
            // 
            // protocolCombo
            // 
            this.protocolCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.protocolCombo.FormattingEnabled = true;
            this.protocolCombo.Items.AddRange(new object[] {
            "Icmp",
            "Tcp",
            "Udp"});
            this.protocolCombo.Location = new System.Drawing.Point(927, 520);
            this.protocolCombo.Name = "protocolCombo";
            this.protocolCombo.Size = new System.Drawing.Size(121, 24);
            this.protocolCombo.TabIndex = 41;
            // 
            // wrongInputLabel
            // 
            this.wrongInputLabel.AutoSize = true;
            this.wrongInputLabel.ForeColor = System.Drawing.Color.Crimson;
            this.wrongInputLabel.Location = new System.Drawing.Point(21, 568);
            this.wrongInputLabel.Name = "wrongInputLabel";
            this.wrongInputLabel.Size = new System.Drawing.Size(88, 17);
            this.wrongInputLabel.TabIndex = 42;
            this.wrongInputLabel.Text = "Wrong Input!";
            this.wrongInputLabel.Visible = false;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(766, 478);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(72, 18);
            this.label16.TabIndex = 43;
            this.label16.Text = "Blacklist";
            // 
            // device_list_text
            // 
            this.device_list_text.Location = new System.Drawing.Point(944, 51);
            this.device_list_text.Multiline = true;
            this.device_list_text.Name = "device_list_text";
            this.device_list_text.ReadOnly = true;
            this.device_list_text.Size = new System.Drawing.Size(498, 407);
            this.device_list_text.TabIndex = 44;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(1152, 24);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(86, 18);
            this.label17.TabIndex = 45;
            this.label17.Text = "Device list";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1512, 728);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.device_list_text);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.wrongInputLabel);
            this.Controls.Add(this.protocolCombo);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.dst_port_text);
            this.Controls.Add(this.src_port_text);
            this.Controls.Add(this.clearFilterButton);
            this.Controls.Add(this.delSelectedButton);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.dst_mac_text);
            this.Controls.Add(this.src_mac_text);
            this.Controls.Add(this.dst_ip_text);
            this.Controls.Add(this.src_ip_text);
            this.Controls.Add(this.filterButton);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.timerButton);
            this.Controls.Add(this.textBox5);
            this.Controls.Add(this.clearButton);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.resetButton);
            this.Controls.Add(this.textBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button resetButton;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.Button timerButton;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button filterButton;
        private System.Windows.Forms.TextBox src_ip_text;
        private System.Windows.Forms.TextBox dst_ip_text;
        private System.Windows.Forms.TextBox dst_mac_text;
        private System.Windows.Forms.TextBox src_mac_text;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button delSelectedButton;
        private System.Windows.Forms.Button clearFilterButton;
        private System.Windows.Forms.TextBox dst_port_text;
        private System.Windows.Forms.TextBox src_port_text;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ComboBox protocolCombo;
        private System.Windows.Forms.Label wrongInputLabel;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox device_list_text;
        private System.Windows.Forms.Label label17;
    }
}

