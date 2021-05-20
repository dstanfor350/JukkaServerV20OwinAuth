using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;

namespace JukkaServerLib
{
    public interface IExtensionLoginManager
    {
        //bool IsLoginValid(NameValueCollection loginCollection);
        bool IsLoginValid(NameValueCollection loginCollection);
    };
}
