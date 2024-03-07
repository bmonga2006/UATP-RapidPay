using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace RapidPay.Authentication
{
    public class XAuthTokenAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
      
        public XAuthTokenAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock)
            : base(options, logger, encoder, clock)
        {
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.TryGetValue("X-Auth-Token", out var token))
            {
                return AuthenticateResult.Fail("Missing X-Auth-Token header");
            }

            if ( IsTokenValid(token, out var roles))
            {
                var  claims = new List<Claim> { new Claim(ClaimTypes.Name, "user") };
                claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

                var identity = new ClaimsIdentity(claims, Scheme.Name);
                var principal = new ClaimsPrincipal(identity);
                var ticket = new AuthenticationTicket(principal, Scheme.Name);

                return AuthenticateResult.Success(ticket);
            }

            return AuthenticateResult.Fail("Invalid X-Auth-Token");
        }

        private bool IsTokenValid(string token, out List<string> roles)
        {
            // This code is just for demonstration purposes, In ideal Scenario this will be done using user management,
            // where we can get a user from the token and then check for permissions/roles assigned to the user

            roles = new List<string>();          

            if(string.IsNullOrEmpty(token))
                return false;

            var tokenAttribute = token.Split(':');
            if (tokenAttribute.Length == 2 && !string.IsNullOrEmpty(tokenAttribute[0]) && !string.IsNullOrEmpty(tokenAttribute[1]))
            {
                roles.Add(tokenAttribute[1]);
                return (string.Equals("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9", tokenAttribute[0]));
            }
            return false;

            
        }
    }
}
