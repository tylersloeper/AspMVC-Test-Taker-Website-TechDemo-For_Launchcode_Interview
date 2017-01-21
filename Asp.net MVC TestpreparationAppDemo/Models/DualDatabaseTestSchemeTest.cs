using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Asp.net_MVC_TestpreparationAppDemo.Models
{
    public class DualDatabaseTestSchemeTest
    {
        //link to question database will be via ID. using scheme "test ID" + "question ID" equals question id.
        //and the going up linearly on the question side.
            public int ID { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }

    }


    public class DualDatabaseTestSchemeTestsDbContext : DbContext
    {
        public DbSet<DualDatabaseTestSchemeTest> DualDatabaseTestSchemeTestDataBase { get; set; }
    }

}