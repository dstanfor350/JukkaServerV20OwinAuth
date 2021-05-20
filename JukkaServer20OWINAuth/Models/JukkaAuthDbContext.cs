using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JukkaServer20OWINAuth.Models
{
    public class JukkaAuthDbContext : IdentityDbContext
    {
        public JukkaAuthDbContext()
            : base("JukkaAuthDbContext")
        {
        }
    }
}