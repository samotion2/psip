using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using PcapDotNet.Core;
using PcapDotNet.Packets;
using PcapDotNet.Packets.IpV4;
using PcapDotNet.Packets.Ethernet;

namespace psip {
    public class ThreadWork {
        public static void DoWork(PacketCommunicator communicator, PacketCommunicator communicator1, Controller con, int i) {
            Packet packet;
            do {
                PacketCommunicatorReceiveResult result = communicator.ReceivePacket(out packet);
                switch (result) {
                    case PacketCommunicatorReceiveResult.Timeout:
                        continue;
                    case PacketCommunicatorReceiveResult.Ok: //ak je packet v poriadku, zapocitaju sa jeho statistiky a ak sa ma predoslat tak sa preposle
                        con.checkStats(packet, i);
                        con.updateTable(i, packet.Ethernet.Source);
                        //Console.WriteLine(packet.Timestamp.ToString("yyyy-MM-dd hh:mm:ss.fff") + " length:" + packet.Length);
                        if (con.isInTable(i, packet.Ethernet.Destination)) {//zisti ci sa ma packet preposlat
                            //odoslanie packet + jeho statistiky
                            communicator1.SendPacket(packet);
                            con.checkStats(packet, i + 1);
                            break;
                        }
                        //Console.WriteLine("SRC_MAC: " + packet.Ethernet.Source);
                        break;
                    default:
                        throw new InvalidOperationException("Chyba: " + result);
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
        //zisti sattistiky o packete
        public void checkStats(Packet packet, int i) {
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
                //aktualizacia statistik
                form.updateStats(arr, i);
            }
        }

        //reset statistik
        public static void reset(int x) {
            for(int i = 0; i < 7; i++) {
                arr[x, i] = 0;
            }
            form.updateStats(arr, x);
        }

        public void updateTable(int i, MacAddress mac) {
            addToTable(form.getData(), i, mac);
            form.refreshTable();
        }

        //zisti ci sa ma packet posielat alebo nie za pomoci CAMTable
        public bool isInTable(int i, MacAddress mac) {
            var temp = form.getData();
            int count = 0;

            //ak sa mac nenachadza v tabulke vrati true
            foreach (var item in temp) {
                if (item.Mac.Equals(mac))
                    count++;
            }
            if (count == 0) {
                return true;
            }
          
            //ak sa MAC nachadza v tabulke a zaroven sa nenachadza na rovnakom porte na ktory dosiel packet, vrati true
            foreach (var item in temp) {
                if (item.Mac.Equals(mac) && (item.Port != i)) {
                    return true;
                }
            }
            return false;
        }

        //prida src_mac do tabulky ak splna poziadavky
        public static List<CAMTable> addToTable(List<CAMTable> list1, int i, MacAddress mac) {
            CAMTable list = new CAMTable() { Id = "", Port = i, Mac = mac, Timer = form.getTime() };

            //ak sa src_mac uz nachadza v liste neprida sa ale obnovi sa timer
            try {
                foreach (var item in list1) {
                    if (item.Mac == mac) {
                        item.Timer = form.getTime();
                        return list1;
                    }
                }
            } catch { }

            //ak sa src_mac nenachadza v liste prida sa
            list1.Add(list);
            return list1;
        }
        //inicializacia CAMTable
        public static List<CAMTable> InitTable() {
            var list = new List<CAMTable>();
            return list;
        }
    }

    public class CAMTable {
        public string Id { get; set; }
        public int Port { get; set; }
        public MacAddress Mac { get; set; }
        public int Timer { get; set; }
    }

    //sluzi na aktualizaciu casu a nasledne vymazanie zaznamu po uplynuti casu
    public class CAMTimer {
        public static void timer(Form1 form) {
            var data = form.getData();
            Console.WriteLine(data.Count + "---------");
            while (true) {
                if (data.Count > 0) {
                    foreach (var item in data.ToList()) {
                        if (item.Timer == 0) {
                            data.Remove(item);
                        }
                        else 
                            item.Timer -= 1;
                    }
                    form.refreshTable();
                    Thread.Sleep(1000);
                }
            }
        }
    }

    static class Program {
        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            IList<LivePacketDevice> allDevices = LivePacketDevice.AllLocalMachine;

            //konfiguracia communicatorov
            PacketCommunicator communicator = allDevices[0].Open(65536, PacketDeviceOpenAttributes.Promiscuous | PacketDeviceOpenAttributes.NoCaptureLocal, 1);
            PacketCommunicator communicator1 = allDevices[1].Open(65536, PacketDeviceOpenAttributes.Promiscuous | PacketDeviceOpenAttributes.NoCaptureLocal, 1);

            Form1 form = new Form1();
            Controller con = new Controller(form);

            //spustenie snifferov na nasich 2 interfacoch
            Thread thread = new Thread(() => ThreadWork.DoWork(communicator1, communicator, con, 0));
            thread.Start();
            Thread thread2 = new Thread(() => ThreadWork.DoWork(communicator, communicator1, con, 2));
            thread2.Start();
            //timer thread
            Thread thread3 = new Thread(() => CAMTimer.timer(form));
            thread3.Start();

            Application.Run(form);
        }
    }
}
