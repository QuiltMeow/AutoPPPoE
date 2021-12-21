using DotRas;
using System.Collections.Generic;
using System.Management;

namespace AutoPPPoE
{
    public class AdapterManager
    {
        public IList<string> adapterName
        {
            get;
            private set;
        }

        public IList<string> rasName
        {
            get;
            private set;
        }

        public AdapterManager()
        {
            adapterName = new List<string>();
            rasName = new List<string>();
            loadAdapter();
            loadRAS(RasPhoneBookType.AllUsers);
            loadRAS(RasPhoneBookType.User);
        }

        private void loadAdapter()
        {
            ManagementScope scope = new ManagementScope();
            ObjectQuery objectQuery = new ObjectQuery("SELECT * FROM Win32_NetworkAdapter");
            ManagementObjectSearcher objectSearcher = new ManagementObjectSearcher(scope, objectQuery);
            ManagementObjectCollection objectCollect = objectSearcher.Get();
            foreach (ManagementObject result in objectCollect)
            {
                object name = result.Properties["NetConnectionID"].Value;
                if (name != null)
                {
                    adapterName.Add(name.ToString());
                }
            }
        }

        private void loadRAS(RasPhoneBookType type)
        {
            string phoneBookPath = RasPhoneBook.GetPhoneBookPath(type);
            RasPhoneBook phoneBook = new RasPhoneBook();
            phoneBook.Open(phoneBookPath);
            foreach (RasEntry entry in phoneBook.Entries)
            {
                rasName.Add(entry.Name);
            }
        }
    }
}