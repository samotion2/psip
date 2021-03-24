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

namespace psip {

    public partial class Form1 : Form {
        
        public List<CAMTable> Data { get; set; }//list CAM zaznamov
        public int time = 60;//defaultny timer

        //aktualizuje statistiky
        public void updateStats(int[,] arr, int i) {
            TextBox[] textboxarr = new TextBox[4] {textBox1, textBox2, textBox3, textBox4};

            textboxarr[i].Invoke(new MethodInvoker(() => {
                textboxarr[i].Text = "Ethernet: " + arr[i, 0] + "\r\nARP: " + arr[i, 1] + "\r\nIP: " + arr[i, 2] + "\r\nTCP: " + arr[i, 3] + "\r\nUDP: " + arr[i, 4] + "\r\nICMP: " + arr[i, 5] + "\r\nHTTP: " + arr[i, 6];
            }));
        }

        //inicializacia
        public Form1() {
            Data = Controller.InitTable();
            InitializeComponent();
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
    }
}
