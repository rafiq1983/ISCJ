using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.Cookies;
using MA.Common;
using MA.Common.Models.api;
using MA.Core;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

// https://docs.microsoft.com/en-us/aspnet/core/security/authorization/limitingidentitybyscheme?view=aspnetcore-3.1
//https://joonasw.net/view/creating-auth-scheme-in-aspnet-core-2

namespace ISCJ
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

           ConnectionString.Value= Configuration.GetConnectionString("Primary");

           string test = Configuration["SendGridApiKey"];
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "ICSJ API", Version = "v1" });
                //TODO: See what it does.  How to customize swagger.
                // c.DocumentFilter<MA.Core.Web.SwaggerSecurityRequirementsDocumentFilter>();
                c.OperationFilter<AuthorizationHeaderOperationFilter>(); //this will allow auth 
            });

            

            services.AddAuthentication(options =>
                {

                    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;

                }).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, "",
                    options => { options.LoginPath = "/Login";  })
                .AddCaptcha(options => { options.ClaimsIssuer = "Iftikhar"; })
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = false,
                        RequireSignedTokens =true,
                        IssuerSigningKey= new SymmetricSecurityKey(System.Text.ASCIIEncoding.ASCII.GetBytes("ISCJAppSigniningKey")),
                        ClockSkew = new TimeSpan(0L),
                        RequireExpirationTime = true,
                        ValidIssuer = "ISCJApp",
                        ValidAudience = "ISCJAppUser",
                    };
                });

            services.Configure<ApplicationPreferences>(Configuration.GetSection("ApplicationPrefs"));

            services.AddMvc().AddRazorPagesOptions(options =>
            {
                options.Conventions.AuthorizeFolder("/");
                options.Conventions.AllowAnonymousToPage("/Login");
                options.Conventions.AllowAnonymousToFolder("/signup");

            });
            
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
              
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddSingleton<TypeToNameService>();
            services.AddSingleton<ResourceService>();
            services.AddSingleton(typeof(List<IResourceProvider>), x =>
                {
                    return new List<IResourceProvider>() {new FieldLabelResourceProvider("ICSJ.Resource")};
                });

            services.AddTransient<StudentManager>();
            services.AddTransient<ProductManager>();
            services.AddTransient<SignupManager>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
           
            app.UseSwagger();
           
            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = "";
            });

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }
            
            
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseMvc();
          
        }

        
    }

    public class ApplicationPreferences
    {
        public string WelcomeMessage { get; set; }
    }

    //This modifies behavior of swagger to show authentication header.
    public class AuthorizationHeaderOperationFilter : IOperationFilter
    {
        /// <summary>
        /// Adds an authorization header to the given operation in Swagger.
        /// </summary>
        /// <param name="operation">The Swashbuckle operation.</param>
        /// <param name="context">The Swashbuckle operation filter context.</param>
        public void Apply(Operation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
            {
                operation.Parameters = new List<IParameter>();
            }


            var allowAnonymousAttributes = context.ApiDescription.ActionAttributes().OfType<AllowAnonymousAttribute>();

            if (allowAnonymousAttributes.Any())
            {
                return;
            }

            var authorizeAttributes = context.ApiDescription
                .ControllerAttributes()
                .Union(context.ApiDescription.ActionAttributes())
                .OfType<AuthorizeAttribute>();

            if (authorizeAttributes.SingleOrDefault(x=>x.AuthenticationSchemes == JwtBearerDefaults.AuthenticationScheme)!=null)
            {
                var parameter = new NonBodyParameter
                {
                    Name = "Authorization",
                    In = "header",
                    Description = "The bearer token",
                    Required = true,
                    Type = "string"
                };

                operation.Parameters.Add(parameter);
            }
        }
    }
}
