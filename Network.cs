using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.DirectoryServices;
namespace MyTotalCommander
{
    public class Network
    {
        private string _domain;
        private string _name;
        private IPAddress[] _addresses = null;
        public string Domain { get { return _domain; } }
        public string Name { get { return _name; } }
        public IPAddress[] Addresses { get { return _addresses; } }
        private Network(string domain, string name)
        {
            IPAddress a;
            _domain = domain;
            _name = name;
            try { _addresses = Dns.GetHostAddresses(name); } catch { }
        }

        public static Network[] GetLocalNetwork()
        {
            var list = new List<Network>();
            using (var root = new DirectoryEntry("WinNT:"))
            {
                foreach (var _ in root.Children.OfType<DirectoryEntry>())
                {
                    switch (_.SchemaClassName)
                    {
                        case "Computer":
                            list.Add(new Network("", _.Name));
                            break;
                        case "Domain":
                            list.AddRange(_.Children.OfType<DirectoryEntry>().Where(__ => (__.SchemaClassName == "Computer")).Select(__ => new Network(_.Name, __.Name)));
                            break;
                    }
                }
            }
            return list.OrderBy(_ => _.Domain).ThenBy(_ => _.Name).ToArray();
        }
    }
}
