using Microsoft.Owin;
using Owin;
using UnicomWeb.Models;
using Microsoft.Owin.Security.Cookies;
using Microsoft.AspNet.Identity;
using System;

[assembly: OwinStartup(typeof(UnicomWeb.App_Start.Startup))]

namespace UnicomWeb.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // настраиваем контекст и менеджер
            app.CreatePerOwinContext<ApplicationContext>(ApplicationContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationRoleManager>(ApplicationRoleManager.Create);

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                ExpireTimeSpan = TimeSpan.FromMinutes(20),
            });
        }
    }
}