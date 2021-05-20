// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Specialized;
using Newtonsoft.Json;

namespace JukkaServerLib
{

    [JsonObject(MemberSerialization.OptIn)]
    public class Login
    {
        IExtensionLoginManager LoginMgr;

        // Might use UserCollection or might use the three values from UserCollection
        public NameValueCollection UserCollection { get; set; }

        public string UserName { get; set; }
        public string Password { get; set; }
        public string GrantType { get; set; }
        public bool UserAuthenticated { get; set; }

        [JsonProperty]
        public string Access_token { get; set; }

        [JsonProperty]
        public string Token_Type { get; set; }

        [JsonProperty]
        public int Expires_in { get; set; }

        [JsonProperty]
        public string FirstName { get; set; }

        [JsonProperty]
        public string LastName { get; set; }

        [JsonProperty]
        public string Email { get; set; }

        [JsonProperty]
        public string[] Roles { get; set; }

        [JsonProperty]
        public string UserId { get; set; }

        [JsonProperty]
        public DateTime Issued { get; set; }

        [JsonProperty]
        public DateTime Expires { get; set; }

        public Login()
        {
        }

        public Login(NameValueCollection loginCollection, IExtensionLoginManager mgr)
        //, string access_Token, string token_Type, int expires_in, string firstName,
        //            string lastName, string emailAddress, string[] roles,
        //            string UID, DateTime issuedDate, DateTime expires
        {
            // The login manager is a property which can be assigned any login manager inheriting from IExtensionLoginManager
            LoginMgr = mgr;

            // Might use UserCollection or might use the three values from UserCollection
            UserCollection = loginCollection;

            UserName = loginCollection[0];
            Password = loginCollection[1];
            GrantType = loginCollection[2];

            UserAuthenticated = false;

            DateTime dt = DateTime.Now;
            TimeSpan duration = new TimeSpan(30, 0, 0, 0);

            // Hard coded for now.  Will be set later if User is Authenticated.
            Access_token = "R6DIvk7";
            Token_Type = "bearer";
            Expires_in = 1209599;
            FirstName = "Joe";
            LastName = "C";
            Email = "machine@jukka.com";
            Roles = new string[] { "admin", "service" };
            UserId = "91cdb71d";
            Issued = dt;
            Expires = dt + duration;
        }

        public bool AuthenticateUser()
        {
            return LoginMgr.IsLoginValid(UserCollection);
        }

        private bool IsLoginValid(NameValueCollection userCollection)
        {
            return true;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
}
