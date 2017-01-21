using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Asp.net_MVC_TestpreparationAppDemo.Models
{
    public class Contact
    {
        public int ContactId { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

    }
}