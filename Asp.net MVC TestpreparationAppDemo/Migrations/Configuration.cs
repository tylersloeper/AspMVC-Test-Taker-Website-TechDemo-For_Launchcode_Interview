namespace Asp.net_MVC_TestpreparationAppDemo.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Asp.net_MVC_TestpreparationAppDemo.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    internal sealed class Configuration : DbMigrationsConfiguration<Asp.net_MVC_TestpreparationAppDemo.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        bool AddUserAndRole(Asp.net_MVC_TestpreparationAppDemo.Models.ApplicationDbContext context)
        {
            //adds can edit role, and admin user using application dbcontext
            IdentityResult ir;
            var rm = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            ir = rm.Create(new IdentityRole("canEdit"));
            var um = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var user = new ApplicationUser(){UserName = "MyAdmin@codingisfun.com",};

            //create new user with username and password using usermanager and add to app dbcontext
            ir = um.Create(user, "Mypassword1!");
            if (ir.Succeeded == false)
                return ir.Succeeded;

            ir = um.AddToRole(user.Id, "canEdit");
            return ir.Succeeded;
        }

        protected override void Seed(Asp.net_MVC_TestpreparationAppDemo.Models.ApplicationDbContext context)
        {
            //save of updated db context is automatically performed by migration
            AddUserAndRole(context);


           // context.Contacts.AddOrUpdate(p => p.Name);

            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
