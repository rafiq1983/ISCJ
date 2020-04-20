﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Resources;
using System.Security.Claims;
using System.Threading.Tasks;
using BusinessLogic;
using ICSJ.Resource;
using MA.Common.Entities.User;
using MA.Core;
using MA.Core.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ISCJ.Pages.Admin
{
    public class LoginModel : BasePageModel
    {
        private readonly SignupManager _signupManager;
        private ILogger<LoginModel> _logger;

        public LoginModel(SignupManager mgr, ILogger<LoginModel> logger, ResourceService svc, IOptions<ApplicationPreferences> prefs, IOptions<AuthenticationOptions> authOptions)
        {
            _signupManager = mgr;
            svc.GetLabelName("logoName");
            _logger = logger;
            
            IOptions<ApplicationPreferences> ioptions = prefs;
            var _authOptions = authOptions;

        }

        [BindProperty]
        public LoginData loginData { get; set; }
        public void OnGet()
        {
            
            ResourceService svc = new ResourceService(new List<IResourceProvider>(){new FieldLabelResourceProvider("ICSJ.Resource", "ICSJ.Resource")});
            string s1= svc.GetString("logoName");

            ResourceService svc2 = new ResourceService(new List<IResourceProvider>() { new SmartAssemblyResourceProvider("ICSJ.Resource") });
            s1=svc2.GetString("ICSJ.Resource.FieldLabels.logoName");

            // System.Threading.Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("fr-FR");
            //ICSJ.Resource.FieldLabels.Culture = CultureInfo.GetCultureInfo("fr-FR");
            string s = ICSJ.Resource.FieldLabels.logoName;
        }

        public async Task OnPostAsync()
        {
            User user;
            var isValid = false;
            if (ModelState.IsValid)
            {
                UserManager mgr = new UserManager();

                user = mgr.VerifyLogin(new MA.Common.VerifyLoginInput()
                {
                    UserName = loginData.Username,
                    Password = loginData.Password
                });

                if (user != null)
                {
                    isValid = true;
                }

                if (!isValid)
                {
                    ModelState.AddModelError<LoginModel>((x) => x.loginData.Username,
                        "username or password is invalid");
                    return;

                }
                
                var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name,
                    ClaimTypes.Role);
                
                identity.AddClaim(new Claim(AppClaimTypes.UserId, user.UserId.ToString()));
                identity.AddClaim(new Claim(AppClaimTypes.LoginName, loginData.Username));
                identity.AddClaim(new Claim(ClaimTypes.Name, user.Contact.FirstName + " " + user.Contact.LastName));

                //this is just for captch.  We want to store session id claim in session store.
                identity.AddClaim(new Claim("SessionIdClaim", Guid.NewGuid().ToString()));

                if (user.UserTenants.Count >= 1)
                {
                    //TOOD: For Some reason I'm not able to get Tenant part of User.UserTenats.Tenanct call.  Check later.
                    var tenant = mgr.GetUserTenants(user.UserId).First();
                    identity.AddClaim(new Claim(AppClaimTypes.TenantId, tenant.TenantId.ToString()));
                    identity.AddClaim(new Claim(AppClaimTypes.TenantName, tenant.Tenant.OrganizationName));

                    mgr.AddUserLoginAudit(new CallContext(user.UserId, user.UserName, Request.HttpContext.Connection.RemoteIpAddress.ToString(), tenant.TenantId.ToString(),
                        tenant.TenantId));


                    var principal = new ClaimsPrincipal(identity);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal,
                       new AuthenticationProperties { IsPersistent = loginData.RememberMe });

                    //Can only call SignIn if CaptchaAuthenticationHandler inherits from SignInAuthenticationHandler.
                    //await HttpContext.SignInAsync("Captcha", principal,
                     //   new AuthenticationProperties { IsPersistent = loginData.RememberMe });

                    Response.Redirect("/main");
                }
                else if (user.UserTenants.Count == 0)
                {
                    var principal = new ClaimsPrincipal(identity);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal,
                        new AuthenticationProperties { IsPersistent = loginData.RememberMe });


                    Response.Redirect("/Me/selecttenant");

                    mgr.AddUserLoginAudit(new CallContext(user.UserId, user.UserName, "TBD", "", null));
                }
               

                
            }
        }

        //This method generates the model key from lamba expression.
        void AddModelError<TModel>(Expression<Func<TModel, object>> expression, string errorMessage)
        {
            ModelState.AddModelError(ExpressionHelper.GetExpressionText(expression), errorMessage);
        }




    }

    //A custom method which takes property names as lambda expressions and generates the model key name.  
    //however, this is not needed now as .net has this method built in.
    public static class ModelStateDictionaryHelper2
    {
        public static void AddModelErrorTesting<TViewModel>(
            this ModelStateDictionary me,
            Expression<Func<TViewModel, object>> lambdaExpression, string error)
        {
            me.AddModelError(GetPropertyName(lambdaExpression), error);
        }

        private static string GetPropertyName(Expression lambdaExpression)
        {
            IList<string> list = new List<string>();
            var e = lambdaExpression;

            while (true)
            {
                switch (e.NodeType)
                {
                    case ExpressionType.Lambda:
                        e = ((LambdaExpression)e).Body;
                        break;
                    case ExpressionType.MemberAccess:
                        var propertyInfo = ((MemberExpression)e).Member as PropertyInfo;
                        var prop = propertyInfo != null
                                          ? propertyInfo.Name
                                          : null;
                        list.Add(prop);

                        var memberExpression = (MemberExpression)e;
                        if (memberExpression.Expression.NodeType != ExpressionType.Parameter)
                        {
                            var parameter = GetParameterExpression(memberExpression.Expression);
                            if (parameter != null)
                            {
                                e = Expression.Lambda(memberExpression.Expression, parameter);
                                break;
                            }
                        }
                        return string.Join(".", list.Reverse());
                    default:
                        return null;
                }
            }
        }

        private static ParameterExpression GetParameterExpression(Expression expression)
        {
            while (expression.NodeType == ExpressionType.MemberAccess)
            {
                expression = ((MemberExpression)expression).Expression;
            }
            return expression.NodeType == ExpressionType.Parameter ? (ParameterExpression)expression : null;
        }
    }


    public class LoginData
    {
        [Required]
        public string Username { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }


}
