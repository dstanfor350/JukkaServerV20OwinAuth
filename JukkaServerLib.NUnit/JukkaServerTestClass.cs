using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using System.Threading.Tasks;
using NUnit.Framework;
using SUT = JukkaServerLib;
using System.Web;

namespace JukkaServerLib.NUnit
{
    [TestFixture]
    public class JukkaServerTestClass
    {
        [TestCase]
        public void LoginSuccess_WhenLogin_ReturnsTrue()
        {
            Console.WriteLine("=========== LoginSuccess_WhenLogin_ReturnsTrue ===========");
            // arrange
            NameValueCollection LoginCollection = HttpUtility.ParseQueryString(
                "userName = machine % 40jukka.com & password = Machine143 & grant_type = password");

            Console.WriteLine("Login Payload collection:");
            foreach (string s in LoginCollection.AllKeys)
            {
                Console.WriteLine($"{s} = {LoginCollection[s]}");
            }

            // act
            // Todo: Process login here.

            // Create the Login object
            FakeLoginExtensionManager mgr = new FakeLoginExtensionManager();
            mgr.WillBeValid = true;
            Login login = new SUT.Login(LoginCollection, mgr);

            // assert
            Assert.IsTrue(login.AuthenticateUser());
        }

        [TestCase]
        public void LoginFails_WhenLogin_ReturnsFalse()
        {
            Console.WriteLine("=========== LoginFails_WhenLogin_ReturnsFalse ===========");
            // arrange
            NameValueCollection LoginCollection = HttpUtility.ParseQueryString(
                "userName = machine % 40jukka.com & password = Machine143 & grant_type = password");

            Console.WriteLine("Login Payload collection:");
            foreach (string s in LoginCollection.AllKeys)
            {
                Console.WriteLine($"{s} = {LoginCollection[s]}");
            }

            // act
            // Todo: Process login here.

            // Create the Login object
            FakeLoginExtensionManager mgr = new FakeLoginExtensionManager();
            mgr.WillBeValid = false;
            Login login = new SUT.Login(LoginCollection, mgr);

            // assert
            Assert.IsFalse(login.AuthenticateUser());
        }
    }

    public class FakeLoginExtensionManager : IExtensionLoginManager
    {
        public bool WillBeValid { get; set; }

        //public bool IsLoginValid(NameValueCollection loginCollection)
        public bool IsLoginValid(NameValueCollection loginCollection)
        {
            return WillBeValid;
        }
    }
}
