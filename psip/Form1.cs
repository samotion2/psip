using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using PcapDotNet.Core;
using PcapDotNet.Packets;
using PcapDotNet.Packets.Ethernet;
using PcapDotNet.Packets.Icmp;
using PcapDotNet.Packets.IpV4;
using System.Text.RegularExpressions;

namespace psip {

    public partial class Form1 : Form {
        
        public List<CAMTable> Data { get; set; }//list CAM zaznamov
        public int time = 60;//defaultny timer

        public List<Filter> Filter_Data { get; set; } //list s filtrami

        //inicializacia
        public Form1() {
            Data = Controller.InitTable();
            Filter_Data = Controller.InitFilter();
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            protocolCombo.SelectedIndex = 0;
            showInterfaces();
        }

        //vypise list interfacov
        private void showInterfaces() {
            IList<LivePacketDevice> allDevices = LivePacketDevice.AllLocalMachine;

            for (int i = 0; i != allDevices.Count; ++i) {
                LivePacketDevice device = allDevices[i];
                if (device.Description != null)
                    listBox1.Items.Add( i + ". " + device.Description + Environment.NewLine);
                else
                    listBox1.Items.Add(" (No description available)" + Environment.NewLine);
            }
        }

        //aktualizuje statistiky
        public void updateStats(int[,] arr, int i) {
            TextBox[] textboxarr = new TextBox[4] { textBox1, textBox2, textBox3, textBox4 };

            textboxarr[i].Invoke(new MethodInvoker(() => {
                textboxarr[i].Text = "Ethernet: " + arr[i, 0] + "\r\nARP: " + arr[i, 1] + "\r\nIP: " + arr[i, 2] + "\r\nTCP: " + arr[i, 3] + "\r\nUDP: " + arr[i, 4] + "\r\nICMP: " + arr[i, 5] + "\r\nHTTP: " + arr[i, 6];
            }));
        }

        public List<Filter> getFilterData() {
            return this.Filter_Data;
        }
        public List<CAMTable> getData() {
            return this.Data;
        }

        public int getTime() {
            return this.time;
        }

        //obnovi tabulku
        public void refreshTable() {
            var data = this.Data;
            
            dataGridView1.Invoke(new MethodInvoker(() => {
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = data;

                //ocisluje riadky
                foreach (DataGridViewRow row in dataGridView1.Rows)
                    row.Cells["Id"].Value = (row.Index + 1).ToString();
            }));
        }

        //obnovi tabulku filtrov
        public void refreshFilter() {
            var filter_data = this.Filter_Data;

            dataGridView2.Invoke(new MethodInvoker(() => {
                dataGridView2.DataSource = null;
                dataGridView2.DataSource = filter_data;

                //ocisluje riadky
                foreach (DataGridViewRow row in dataGridView2.Rows)
                    row.Cells["Id"].Value = (row.Index + 1).ToString();
            }));
        }

        //resetuju sa statisktiky pre textbox[i]
        private void resetButton_Click(object sender, EventArgs e) {
            for(int i = 0; i < 4; i ++) {
                Controller.reset(i);
            }
        }

        //vyprazdni sa CAM tabulka
        private void clearButton_Click(object sender, EventArgs e) {
            var data = this.Data;

            foreach (var item in data.ToList()) {
                data.Remove(item);
            }

            //aktualizuje data
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = data;
        }

        //nastavi dlzku casovacu ak je zadana ako int a je vacsi ako 0
        private void timerButton_Click(object sender, EventArgs e) {
            int tim = this.time;
            try {
                tim = Int32.Parse(textBox5.Text);
                if (tim > 0)
                    this.time = tim;
            } catch {
                Console.WriteLine("Zle zadane cislo, platny iba format int");
            }
        }

        private void textBox5_MouseDown(object sender, MouseEventArgs e) {
            textBox5.Text = "";
            textBox5.ForeColor = Color.Black;
        }

        //skontroluje ci je zadany input do filtra spravne
        private bool isInputCorrect(string src_ip, string dst_ip, string src_mac, string dst_mac, string src_port, string dst_port) {
            IpV4Address ip;
            short port;
            Regex mac = new Regex(@"^[0-9A-F]{2}:[0-9A-F]{2}:[0-9A-F]{2}:[0-9A-F]{2}:[0-9A-F]{2}:[0-9A-F]{2}$");
            if (src_ip != "any" && !IpV4Address.TryParse(src_ip, out ip)) return false;
            if (dst_ip != "any" && !IpV4Address.TryParse(dst_ip, out ip)) return false;
            if (src_mac != "any" && !mac.IsMatch(src_mac)) return false;
            if (dst_mac != "any" && !mac.IsMatch(dst_mac)) return false;
            if (src_port != "any" && !short.TryParse(src_port, out port)) return false;
            if (dst_port != "any" && !short.TryParse(dst_port, out port)) return false;
            return true;
        }

        private void filterButton_Click(object sender, EventArgs e) {
            //Console.WriteLine(comboBox1.SelectedItem.ToString() + comboBox2.SelectedItem.ToString() + src_ip_text.Text + dst_ip_text.Text + src_mac_text.Text + dst_mac_text.Text);
            wrongInputLabel.Visible = false;
            if (!isInputCorrect(src_ip_text.Text, dst_ip_text.Text, src_mac_text.Text, dst_mac_text.Text, src_port_text.Text, dst_port_text.Text)) {
                wrongInputLabel.Visible = true;
                return;
            }
            Controller.addToFilter(this.Filter_Data, comboBox1.SelectedItem.ToString(), comboBox2.SelectedItem.ToString(), src_ip_text.Text, dst_ip_text.Text, src_mac_text.Text, dst_mac_text.Text, protocolCombo.SelectedItem.ToString(), src_port_text.Text, dst_port_text.Text);
            refreshFilter();
        }

        //vymaze oznaceny filter
        private void delSelectedButton_Click(object sender, EventArgs e) {
            if (this.Filter_Data.Count != 0) {
                var index = dataGridView2.CurrentCell.RowIndex;
                Console.WriteLine(index);
                this.Filter_Data.RemoveAt(index);
                refreshFilter();
            }

        }

        //vymaze vsetky filtre
        private void clearFilterButton_Click(object sender, EventArgs e) {
            var data = this.Filter_Data;

            foreach (var item in data.ToList()) {
                data.Remove(item);
            }
            refreshFilter();
        }

        private void protocolCombo_SelectedIndexChanged(object sender, EventArgs e) {
            if (protocolCombo.SelectedItem == "Arp" || protocolCombo.SelectedItem == "any") {
                src_port_text.Enabled = false;
                src_port_text.Text = "any";
                dst_port_text.Enabled = false;
                dst_port_text.Text = "any";
            } else {
                src_port_text.Enabled = true;
                dst_port_text.Enabled = true;
            }
        }
    }
}
