using System;
using System.Collections.Generic;
using System.Text;
using System.Net.NetworkInformation;

namespace tcp_client
{
    class Mac_addr_grab
    {
        public static string mac_addr_find()
        {
            string addr = "";
            foreach (NetworkInterface n in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (n.OperationalStatus == OperationalStatus.Up)
                {
                    addr += n.GetPhysicalAddress().ToString();
                    break;
                }
            }
            return addr;
        }
    }
}