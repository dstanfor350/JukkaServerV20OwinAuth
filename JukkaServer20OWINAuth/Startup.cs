using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using JukkaServer20OWINAuth.Models;
using System.Threading.Tasks;

[assembly: OwinStartup(typeof(JukkaServer20OWINAuth.Startup))]
namespace JukkaServer20OWINAuth
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureOAuth(app);
        }

        private void ConfigureOAuth(IAppBuilder app)
        {
            app.CreatePerOwinContext<JukkaAuthDbContext>(() => new JukkaAuthDbContext());
            app.CreatePerOwinContext<UserManager<IdentityUser>>(CreateManager);

            app.UseOAuthAuthorizationServer(new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/oauth/token"),
                Provider = new AuthorizationServerProvider(),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(30),
                AllowInsecureHttp = true,

            });
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }

        public class AuthorizationServerProvider : OAuthAuthorizationServerProvider
        {
            public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
            {
                context.Validated();

                //string clientId;
                //string clientSecret;

                ////if (context.TryGetBasicCredentials(out clientId, out clientSecret))
                //if (context.TryGetFormCredentials(out clientId, out clientSecret))
                //{
                //    // validate the client Id and secret against database or from configuration file.  
                //    context.Validated();
                //}
                //else
                //{
                //    context.SetError("invalid_client", "Client credentials could not be retrieved from the Authorization header");
                //    context.Rejected();
                //}
            }

            public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
            {
                UserManager<IdentityUser> userManager = context.OwinContext.GetUserManager<UserManager<IdentityUser>>();
                IdentityUser user;
                try
                {
                    user = await userManager.FindAsync(context.UserName, context.Password);
                }
                catch
                {
                    // Could not retrieve the user due to error.
                    context.SetError("server_error");
                    context.Rejected();
                    return;
                }
                if (user != null)
                {
                    ClaimsIdentity identity = await userManager.CreateIdentityAsync(
                                                            user,
                                                            DefaultAuthenticationTypes.ExternalBearer);
                    context.Validated(identity);
                }
                else
                {
                    context.SetError("invalid_grant", "Invalid UserId or password'");
                    context.Rejected();
                }
            }

        }
        private static UserManager<IdentityUser> CreateManager(IdentityFactoryOptions<UserManager<IdentityUser>> options, IOwinContext context)
        {
            //var creds = new StreamReader(context.Request.InputStream).ReadToEnd();
            var ctx = context.Get<JukkaAuthDbContext>();
            var userStore = new UserStore<IdentityUser>(ctx);
            var owinManager = new UserManager<IdentityUser>(userStore);
            return owinManager;
        }

    }
}