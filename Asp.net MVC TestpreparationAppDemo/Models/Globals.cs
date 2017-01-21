using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Asp.net_MVC_TestpreparationAppDemo.Models
{
    public static class Globals
    {
            public static double answerscorrect = 0;
            public static double answerswrong = 0;
            public static double delayedanswerscorrect = 0;
            public static double delayedanswerswrong = 0;
            public static int count = 1;
            public static List<DualDatabaseTestSchemeQuestion> GlobalQuestionList = new List<DualDatabaseTestSchemeQuestion>();
            public static List<DualDatabaseTestSchemeTest> GlobalTestList = new List<DualDatabaseTestSchemeTest>();


    }
}