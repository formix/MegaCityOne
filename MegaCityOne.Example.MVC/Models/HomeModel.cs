using MegaCityOne.Example.Mvc.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MegaCityOne.Example.Mvc.Models
{
    public class HomeModel
    {
        public UserData User { get; set; }
        public string Identity { get; set; }
        public string[] Roles { get; set; }
    }
}