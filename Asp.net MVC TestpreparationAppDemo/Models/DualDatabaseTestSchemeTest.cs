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

        //new

        public string Status { get; set; } //private or public
        public string Genre { get; set; } //test genre
        public string DateModified { get; set; } //sort by date
        public int GroupingId { get; set; } // to identify who the test belongs to
        public int TimesTaken { get; set; } // sort by popularity

        //second round
        public string Owner { get; set; } //User ID to more easily identify an owner
        public string Upvotes { get; set; } //sort by upvotes

        //advanced options

        public bool IsAdvanced { get; set; } //to divide tests in function. So that advanced are processed differently from simple
        public string TestFinishedDefaultMessage { get; set; } //you completed the test, etc.


        //I need to add 2 arrays. one of conditions, (how many right)
        //one array of string. present message if got x right.
        //use a single string, and use split to divide it into an array when needed. So get the info originally as an array. then when its being saved
        //fix it up into a single string with a default seperator.
        //then split the string when it is needed.

        public string MessagesArrayAsSingleString { get; set; }
        public string AmountCorrectArrayAsSingleString { get; set; }
        public string AdditionalConditionsArrayAsSingleString { get; set; }




    }


    public class DualDatabaseTestSchemeTestsDbContext : DbContext
    {
        public DbSet<DualDatabaseTestSchemeTest> DualDatabaseTestSchemeTestDataBase { get; set; }
    }

}