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
        
        public List<CAMTable> Data { get; set; }
        public int time = 10;

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

        private void button1_Click(object sender, EventArgs e) {
            // Retrieve the device list from the local machine
            IList<LivePacketDevice> allDevices = LivePacketDevice.AllLocalMachine;
           /* textBox1.Clear();
            if (allDevices.Count == 0)
            {
                textBox1.AppendText("No interfaces found! Make sure WinPcap is installed." + Environment.NewLine);
                return;
            }*/

            // Print the list
           /* for (int i = 0; i != allDevices.Count; ++i)
            {
                LivePacketDevice device = allDevices[i];
                textBox1.AppendText((i + 1) + ". " + device.Name);
                if (device.Description != null)
                    textBox1.AppendText(" (" + device.Description + ")" + Environment.NewLine);
                else
                    textBox1.AppendText(" (No description available)" + Environment.NewLine);
            }*/


            PacketDevice selectedDevice = allDevices[0];

            using (PacketCommunicator communicator = selectedDevice.Open(100, PacketDeviceOpenAttributes.Promiscuous, 1000)) {
                communicator.SendPacket(BuildDummyPacket());
            }

            Packet BuildDummyPacket() {
                EthernetLayer ethernetLayer =
                    new EthernetLayer {
                        Source = new MacAddress("01:01:01:01:01:01"),
                        Destination = new MacAddress("02:02:02:02:02:02"),
                        EtherType = EthernetType.None, // Will be filled automatically.
                    };

                IpV4Layer ipV4Layer =
                    new IpV4Layer {
                        Source = new IpV4Address("1.2.3.4"),
                        CurrentDestination = new IpV4Address("11.22.33.44"),
                        Fragmentation = IpV4Fragmentation.None,
                        HeaderChecksum = null, // Will be filled automatically.
                        Identification = 123,
                        Options = IpV4Options.None,
                        Protocol = null, // Will be filled automatically.
                        Ttl = 100,
                        TypeOfService = 0,
                    };

                IcmpEchoLayer icmpLayer =
                    new IcmpEchoLayer {
                        Checksum = null, // Will be filled automatically.
                        Identifier = 456,
                        SequenceNumber = 800,
                    };

                PacketBuilder builder = new PacketBuilder(ethernetLayer, ipV4Layer, icmpLayer);

                return builder.Build(DateTime.Now);
            }
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

        //nastavi dlzku casovacu ak je zadana ako int
        private void timerButton_Click(object sender, EventArgs e) {
            try {
                this.time = Int32.Parse(textBox5.Text);
                foreach (var item in this.Data.ToList()) {
                   item.Timer = this.time;
                }
                refreshTable();
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
