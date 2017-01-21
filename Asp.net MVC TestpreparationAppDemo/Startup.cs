using Microsoft.Owin;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Asp.net_MVC_TestpreparationAppDemo.Models;

[assembly: OwinStartupAttribute(typeof(Asp.net_MVC_TestpreparationAppDemo.Startup))]
namespace Asp.net_MVC_TestpreparationAppDemo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            Globals.count = 1;
            Globals.answerscorrect = 0;
            Globals.answerswrong = 0;

        }
    }
}
