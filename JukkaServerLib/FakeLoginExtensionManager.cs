using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JukkaServer
{
    public class FakeLoginExtensionManager : IExtensionLoginManager
    {
        public bool WillBeValid { get; set; }

        public bool IsLoginValid(NameValueCollection loginCollection)
        {
            return WillBeValid;
        }
    }
}
