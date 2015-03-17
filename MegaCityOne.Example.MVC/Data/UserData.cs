using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MegaCityOne.Example.Mvc.Data
{
    public class UserData
    {
        public string Name { get; set; }
        public string Roles { get; set; }

        public UserData()
        {
            this.Name = "";
            this.Roles = "";
        }

        public string[] RolesToArray()
        {
            return (from r in this.Roles.Split(';')
                    select r.Trim()).ToArray();
        }
    }
}