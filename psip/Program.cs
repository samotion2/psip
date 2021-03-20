using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using PcapDotNet.Core;
using PcapDotNet.Packets;
using PcapDotNet.Packets.IpV4;
using PcapDotNet.Packets.Transport;
using PcapDotNet.Packets.Ethernet;
using System.Diagnostics;

namespace psip {
    public class ThreadWork {
        public static void DoWork(PacketCommunicator communicator, PacketCommunicator communicator1, Controller con, int i) {
            Packet packet;
            do {
                PacketCommunicatorReceiveResult result = communicator.ReceivePacket(out packet);
                switch (result) {
                    case PacketCommunicatorReceiveResult.Timeout:
                        // Timeout elapsed
                        continue;
                    case PacketCommunicatorReceiveResult.Ok:
                        con.updateStats(packet, i);
                        con.updateTable(i, packet.Ethernet.Source);
                        //Console.WriteLine(packet.Timestamp.ToString("yyyy-MM-dd hh:mm:ss.fff") + " length:" + packet.Length);
                        //Stopwatch sw = new Stopwatch();
                        //->>>>>>>pomaly retos fix trba tu->>>>>>>>>>>>>
                        //sw.Start();
                        if (con.isInTable(i, packet.Ethernet.Destination)) {
                            //statistiky pre druhy kidos
                            communicator1.SendPacket(packet);
                            con.updateStats(packet, i + 1);
                            break;
                        }
                        //sw.Stop();
                        //Console.WriteLine("Elapsed={0}", sw.Elapsed);

                        //Console.WriteLine("SRC_MAC: " + packet.Ethernet.Source);
                        break;
                    default:
                        throw new InvalidOperationException("The result " + result + " shoudl never be reached here");
                }
            } while (true);   
        }
    }

    public class Controller {
        static Form1 form;
        static int[,] arr = new int[4,7];
        public Controller(Form1 form1) {
            form = form1;
        }
        public void updateStats(Packet packet, int i) {
            if (packet.IsValid) {
                if (packet.DataLink.Kind == DataLinkKind.Ethernet)
                    arr[i, 0] += 1;
                if (packet.Ethernet.EtherType == EthernetType.Arp)
                    arr[i, 1] += 1;
                if (packet.Ethernet.EtherType == EthernetType.IpV4)
                    arr[i, 2] += 1;
                if (packet.Ethernet.IpV4.Protocol == IpV4Protocol.Tcp)
                    arr[i, 3] += 1;
                if (packet.Ethernet.IpV4.Protocol == IpV4Protocol.Udp)
                    arr[i, 4] += 1;
                if (packet.Ethernet.IpV4.Protocol == IpV4Protocol.InternetControlMessageProtocol)
                    arr[i, 5] += 1;
                if (packet.Ethernet.IpV4.Protocol == IpV4Protocol.Tcp && (packet.Ethernet.IpV4.Tcp.SourcePort == 80 || packet.Ethernet.IpV4.Tcp.DestinationPort == 80))
                    arr[i, 6] += 1;

                form.updateUi(arr, i);
            }
        }
        public static void reset(int x) {
            for(int i = 0; i < 7; i++) {
                arr[x, i] = 0;
            }
            form.updateUi(arr, x);
        }
        public void updateTable(int i, MacAddress mac) {
            //Console.WriteLine(mac);
            CAMTable.addToTable(form.getData(), i, mac);
            form.updateTable();
        }
        public bool isInTable(int i, MacAddress mac) {
            var temp = form.getData();
            int count = 0;

            //Console.WriteLine(temp + ",");
            //ak sa mac nenachadza v tabulke vrati true
            foreach (var item in temp) {
                if (item.Mac.Equals(mac))
                    count++;
            }
            if (count == 0) {
                return true;
            }

            
            //ak sa max nachadza v tabulke a zaroven sa nenachadza na rovnakom porte na ktory dosiel packet, vrati true
            foreach (var item in temp) {
                if (item.Mac.Equals(mac) && (item.Port != i)) {
                    return true;
                }
            }
            return false;
        }
    }

    public class CAMTable {
        public string Id { get; set; }
        public int Port { get; set; }
        public MacAddress Mac { get; set; }
        public string Timer { get; set; }

        public static List<CAMTable> InitTable() {
            var list = new List<CAMTable>();
            list.Add(new CAMTable() { Id = null, Port = 100, Mac = new MacAddress("aa:aa:aa:aa:aa:aa"), Timer = "1" });
            return list;

        }

        public static List<CAMTable> addToTable(List<CAMTable> list1, int i, MacAddress mac) {
            CAMTable list = new CAMTable() { Id = "", Port = i, Mac = mac, Timer = "1" };


            //TU PUSTIT KIDKOSA
            //ak sa src_mac uz nachadza v liste neprida sa
            try {
                foreach (var item in list1) {
                    if (item.Mac == mac)
                        return list1;
                }
            } catch {}
            
            list1.Add(list);
            return list1;
        }
    }

    static class Program {
        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            IList<LivePacketDevice> allDevices = LivePacketDevice.AllLocalMachine;

            PacketCommunicator communicator = allDevices[0].Open(65536, PacketDeviceOpenAttributes.Promiscuous | PacketDeviceOpenAttributes.NoCaptureLocal, 1);
            PacketCommunicator communicator1 = allDevices[1].Open(65536, PacketDeviceOpenAttributes.Promiscuous | PacketDeviceOpenAttributes.NoCaptureLocal, 1);

            Form1 form = new Form1();
            Controller con = new Controller(form);

            //z 1 na 2
            Thread thread = new Thread(() => ThreadWork.DoWork(communicator1, communicator, con, 0));
            thread.Start();
            Thread thread2 = new Thread(() => ThreadWork.DoWork(communicator, communicator1, con, 2));
            thread2.Start();

            Application.Run(form);
        }
    }
}
