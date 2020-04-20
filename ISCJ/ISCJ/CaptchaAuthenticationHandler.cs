using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;


namespace ISCJ
{
    public static class ISCJAuthenticationCaptchExtensions
    {
        public static AuthenticationBuilder AddCaptcha(this AuthenticationBuilder builder, Action<CaptchaAuthenticationOptions> configureOptions)
        {
            //This method is to ensure that if caller has not assigned options, we use default options.
            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<IPostConfigureOptions<CaptchaAuthenticationOptions>, PostConfigureCaptchaAuthenticationOptions>());

            return builder.AddScheme<CaptchaAuthenticationOptions, CaptchaAuthenticationHandler>("Captcha", null, configureOptions);

        }
    }

    public class CaptchaAuthenticationHandler : AuthenticationHandler<CaptchaAuthenticationOptions>
    {
        private static Dictionary<string, bool> AuthenticatedSessions = new Dictionary<string, bool>();

        public CaptchaAuthenticationHandler(IOptionsMonitor<CaptchaAuthenticationOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock)
            : base(options, logger, encoder, clock)
        {

        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
           // return Task.FromResult(AuthenticateResult.Fail("Invalid captcha"));
           List<Claim> claims = new List<Claim>();
           claims.Add(new Claim("AppUserName", "Syed"));
           claims.Add(new Claim("AppRoles", "admin1"));
           claims.Add(new Claim("AppRoles", "admin2"));
           claims.Add(new Claim("AppAuthType", "Custom"));

            ClaimsIdentity identity = new ClaimsIdentity(claims, "AppAuthType", "AppUserName","AppRoles");

           return Task.FromResult<AuthenticateResult>(AuthenticateResult.Success(new AuthenticationTicket(

               new ClaimsPrincipal(identity), new AuthenticationProperties(), "Captcha")));
        }

        protected override Task HandleChallengeAsync(AuthenticationProperties properties)
        {
            Response.StatusCode = 401;
            Response.Redirect("/login");
            return Task.CompletedTask;
        }

        /*
        protected override Task HandleSignInAsync(ClaimsPrincipal user, AuthenticationProperties properties)
        {
            var sessionIdClaim = user.Claims.FirstOrDefault(x => x.Type.Equals("SessionIdClaim"));

            if (sessionIdClaim == null)
                throw new Exception("Session Id is required for Captcha");

            AuthenticatedSessions.Add(sessionIdClaim.Value,true);
            return Task.CompletedTask;
        }

        protected override Task HandleSignOutAsync(AuthenticationProperties properties)
        {
            throw new NotImplementedException();
        }*/
    }

    public class CaptchaAuthenticationOptions : AuthenticationSchemeOptions
    {
        public CaptchaAuthenticationOptions()
        {
        }

    }

    //Use to configure defaults. If no options provided.
    public class PostConfigureCaptchaAuthenticationOptions : IPostConfigureOptions<CaptchaAuthenticationOptions>
    {
        private readonly IDataProtectionProvider _dp;

        public PostConfigureCaptchaAuthenticationOptions(IDataProtectionProvider dataProtection)
        {
            _dp = dataProtection;
        }

        /// <summary>
        /// Invoked to post configure a TOptions instance.
        /// </summary>
        /// <param name="name">The name of the options instance being configured.</param>
        /// <param name="options">The options instance to configure.</param>
        public void PostConfigure(string name, CaptchaAuthenticationOptions options)
        {
            /*
            options.DataProtectionProvider = options.DataProtectionProvider ?? _dp;

            if (string.IsNullOrEmpty(options.Cookie.Name))
            {
                options.Cookie.Name = CookieAuthenticationDefaults.CookiePrefix + name;
            }
            if (options.TicketDataFormat == null)
            {
                // Note: the purpose for the data protector must remain fixed for interop to work.
                var dataProtector = options.DataProtectionProvider.CreateProtector("Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationMiddleware", name, "v2");
                options.TicketDataFormat = new TicketDataFormat(dataProtector);
            }
            if (options.CookieManager == null)
            {
                options.CookieManager = new ChunkingCookieManager();
            }
            if (!options.LoginPath.HasValue)
            {
                options.LoginPath = CookieAuthenticationDefaults.LoginPath;
            }
            if (!options.LogoutPath.HasValue)
            {
                options.LogoutPath = CookieAuthenticationDefaults.LogoutPath;
            }
            if (!options.AccessDeniedPath.HasValue)
            {
                options.AccessDeniedPath = CookieAuthenticationDefaults.AccessDeniedPath;
            }*/
        }
    }

}
