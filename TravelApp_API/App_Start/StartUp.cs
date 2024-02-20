using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Microsoft.AspNet.SignalR; 
using System.Configuration;
using TravelApp_API.Provider;
using TravelApp_API.App_Start;

[assembly: OwinStartup(typeof(TravelApp_API.App_Start.StartUp))]
namespace TravelApp_API.App_Start
{
    public class StartUp
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            ConfigureOAuth(app);

            WebApiConfig.Register(config);
            app.UseWebApi(config); 
        }

        public void ConfigureOAuth(IAppBuilder app)
        {
            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),


                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(480),
                Provider = new AuthorizationServerProvide()
            };
            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }

      
    }
}