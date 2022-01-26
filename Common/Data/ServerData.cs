using System.Collections.Generic;

namespace GTA5OnlineTools.Common.Data
{
    public class ServerData
    {
        public string Version { get; set; }
        public Latest0 Latest { get; set; }
        public Address0 Address { get; set; }
        public List<Download0> Download { get; set; }

        public class Latest0
        {
            public string Date { get; set; }
            public string Change { get; set; }
        }

        public class Address0
        {
            public string Notice { get; set; }
            public string Change { get; set; }
        }

        public class Download0
        {
            public string Name { get; set; }
            public string Url { get; set; }
        }
    }
}
