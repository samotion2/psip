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
        public static void doWork(PacketCommunicator communicator, PacketCommunicator communicator1, Controller con, int i) {
            Packet packet;
            int ind = 0;
            if (i == 0) ind = 2;
            do {
                PacketCommunicatorReceiveResult result = communicator.ReceivePacket(out packet);
                switch (result) {
                    case PacketCommunicatorReceiveResult.Timeout:
                        continue;
                    case PacketCommunicatorReceiveResult.Ok: //ak je packet v poriadku, zapocitaju sa jeho statistiky a ak sa ma predoslat tak sa preposle
                        if (Controller.isAllowedFilter(packet, "In", i.ToString())) { //skontroluje filtre v smere in
                            con.checkStats(packet, i);
                            con.updateTable(i, packet.Ethernet.Source);
                        }

                        if (con.isInTable(i, packet.Ethernet.Destination)) {//zisti ci sa ma packet preposlat
                            if (Controller.isAllowedFilter(packet, "Out", ind.ToString())) { //skontroluje filtre v smere out <----------------------tu doriesit porty
                                communicator1.SendPacket(packet); //odoslanie packet + jeho statistiky
                                con.checkStats(packet, i + 1);
                            }
                            break;
                        }

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
        //zisti sattistiky o packete ak sa nejdena o packet vygenerovany nasim interfacom
        public void checkStats(Packet packet, int i) {
            if (packet.Ethernet.Source.Equals(new MacAddress("02:00:4C:4F:4F:50"))) //packet interfacu
                return;
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

        //aktualizuje tabulku, packety patriace nasim interfacom ignoruje
        public void updateTable(int i, MacAddress mac) {
            if (mac.Equals(new MacAddress("02:00:4C:4F:4F:50"))) //packet interfacu
                return;
            addToTable(form.getData(), i, mac);
            form.refreshTable();
        }

        //zisti ci sa ma packet posielat alebo nie za pomoci CAMTable
        public bool isInTable(int i, MacAddress mac) {
            var data = form.getData();
            int count = 0;

            //ak sa dst_Mac nenachadza v tabulke vrati true
            foreach (var item in data) {
                if (item.Mac.Equals(mac))
                    count++;
            }
            if (count == 0) {
                return true;
            }
          
            //ak sa dst_Mac nachadza v tabulke ale na inom porte(mozna vymena kablov), vymeni sa port
            foreach (var item in data) {
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
                    if (item.Mac == mac && item.Port == i) {// src_mac sa nachadza na rovnakom porte - aktualizacia timeru
                        item.Timer = form.getTime();
                        return list1;
                    }
                    if (item.Mac == mac && item.Port != i) { //src_mac sa nachadza na inom porte - aktualizacia portu
                        item.Port = i;
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
        
        //inicializacia tabulky filtrov
        public static List<Filter> InitFilter() {
            var list = new List<Filter>();
            return list;
        }

        //prida zaznam do filtra
        public static List<Filter> addToFilter(List<Filter> list1, string inout, string port, string srcip, string dstip, string srcmac, string dstmac, string protocol, string sport, string dport) {
            Filter list = new Filter() { InOut = inout, SrcIp = srcip, Port = port, DstIp = dstip, SrcMac = srcmac, DstMac = dstmac, Protocol = protocol, SPort = sport, DPort = dport};
            list1.Add(list);
            return list1;
        }

        //zisti ci je dany packet povoleny alebo nie
        public static bool isAllowedFilter(Packet packet, string inout, string port) { 
            var list = form.getFilterData();

            foreach (var item in list) {
                if (item.InOut == inout) {
                    if (item.Port == port) {
                        if (item.SrcIp == "any" || new IpV4Address(item.SrcIp).Equals(packet.Ethernet.IpV4.Source)) {
                            if (item.DstIp == "any" || new IpV4Address(item.DstIp).Equals(packet.Ethernet.IpV4.Destination)) {
                                if (item.SrcMac == "any" || new MacAddress(item.SrcMac).Equals(packet.Ethernet.Source)) {
                                    if (item.DstMac == "any" || new MacAddress(item.DstMac).Equals(packet.Ethernet.Destination)) {
                                        if (item.Protocol == "any" || (item.Protocol == "Tcp" && IpV4Protocol.Tcp == packet.Ethernet.IpV4.Protocol) || (item.Protocol == "Udp" && IpV4Protocol.Tcp == packet.Ethernet.IpV4.Protocol) || (item.Protocol == "Icmp" && IpV4Protocol.InternetControlMessageProtocol == packet.Ethernet.IpV4.Protocol)) { //pridat nech moze byt aj protokol filter<<<<<<<<<<<<<<<<<<<<----------------------------------------------------------------------------------
                                            //Console.WriteLine(packet.Ethernet.IpV4.Icmp.Code);
                                            if (item.Protocol == "Tcp") {
                                                if ((item.SPort != "any" && short.Parse(item.SPort) == packet.Ethernet.IpV4.Tcp.SourcePort) || (item.DPort != "any" && short.Parse(item.DPort) == packet.Ethernet.IpV4.Tcp.DestinationPort)) {
                                                    return false;
                                                }
                                            } else if (item.Protocol == "Udp") {
                                                if ((item.SPort != "any" && short.Parse(item.SPort) == packet.Ethernet.IpV4.Udp.SourcePort) || (item.DPort != "any" && short.Parse(item.DPort) == packet.Ethernet.IpV4.Udp.DestinationPort)) {
                                                    return false;
                                                }
                                            } else if (item.Protocol == "Icmp") {
                                                if ((item.SPort != "any" && Byte.Parse(item.SPort) == packet.Ethernet.IpV4.Icmp.Code) || (item.DPort != "any" && Byte.Parse(item.DPort) == packet.Ethernet.IpV4.Icmp.Code)) {
                                                    return false;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return true;
        }
    }

    public class CAMTable {
        public string Id { get; set; }
        public int Port { get; set; }
        public MacAddress Mac { get; set; }
        public int Timer { get; set; }
    }

    public class Filter {
        public string Id { get; set; }
        public string InOut { get; set; }
        public string Port { get; set; }
        public string SrcIp { get; set; }
        public string DstIp { get; set; }
        public string SrcMac { get; set; }
        public string DstMac { get; set; }
        public string Protocol { get; set; }
        public string SPort { get; set; }
        public string DPort { get; set; }
    }
    //sluzi na aktualizaciu casu a nasledne vymazanie zaznamu po uplynuti casu
    public class CAMTimer {
        public static void timer(Form1 form) {
            var data = form.getData();
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
            Thread thread = new Thread(() => ThreadWork.doWork(communicator1, communicator, con, 0));
            thread.Start();
            Thread thread2 = new Thread(() => ThreadWork.doWork(communicator, communicator1, con, 2));
            thread2.Start();
            //timer thread
            Thread thread3 = new Thread(() => CAMTimer.timer(form));
            thread3.Start();

            Application.Run(form);
        }
    }
}
