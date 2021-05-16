using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using WebAPI.Data;
using WebAPI.Models;
using WebAPI.Repository;

namespace WebAPI.Controllers
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {

        private readonly PigeonContext context;
        private UserRepository userRepository => new UserRepository(context);
        public BasicAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock, PigeonContext _context) : base(options, logger, encoder, clock)
        {
            context = _context;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            try
            {
                if (!Request.Headers.ContainsKey("Authorization"))
                    return AuthenticateResult.Fail("Authorization header hash was not found");


                var authenticationHeaderValue = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
                var bytes = Convert.FromBase64String(authenticationHeaderValue.Parameter);
                string[] credentials = Encoding.UTF8.GetString(bytes).Split(":");
                string email = credentials[0];
                string password = credentials[1];

                string hash = userRepository.GetUserPasswordHash(email);

                var result = SecurePasswordHasher.Verify(password, hash);


                if (result == false)
                    return AuthenticateResult.Fail("Invalid username or password");
                else
                {
                    if (result == true)
                    {
                        var claims = new[] { new Claim(ClaimTypes.Name, email) };
                        var identity = new ClaimsIdentity(claims, Scheme.Name);
                        var principal = new ClaimsPrincipal(identity);
                        var ticket = new AuthenticationTicket(principal, Scheme.Name);

                        return AuthenticateResult.Success(ticket);
                    }

                }

            }
            catch (Exception)
            {
                return AuthenticateResult.Fail("Error has occured");
            }
            return AuthenticateResult.Fail("Fail");
        }
    }
}
