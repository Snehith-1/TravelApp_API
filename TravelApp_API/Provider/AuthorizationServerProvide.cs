using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Data; 

namespace TravelApp_API.Provider
{
    public class AuthorizationServerProvide : OAuthAuthorizationServerProvider
    {
         
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            string[] strPassword = null;
            //context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
            strPassword = context.Password.Split(new string[] { "||" }, StringSplitOptions.None);

            //string passwordencrypted = objcmnfunctions.passwordencryption(strPassword[1]);

            DataTable user = ValidateUserFromDatabase.IsValidUser(context.UserName, strPassword[0], strPassword[1]);
            if (user.Rows.Count > 0)
            {
                string email_id = user.Rows[0]["email_address"].ToString();
                string user_code = user.Rows[0]["user_code"].ToString();
                string name = user.Rows[0]["first_name"].ToString();

                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                identity.AddClaim(new Claim("sub", context.UserName));
                identity.AddClaim(new Claim(ClaimTypes.Role, "SuperAdmin"));

                var props = new AuthenticationProperties(new Dictionary<string, string>
                {
                    {"email_id", email_id ?? ""},
                    {"user_code", user_code ?? ""},
                    {"name", name ?? ""}

                });

                var ticket = new AuthenticationTicket(identity, props);
                context.Validated(ticket);
            }
        }
        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }
    }
}