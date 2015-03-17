using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MegaCityOne.Example.Mvc.Models
{
    public class RoleModel
    {
        public bool Selected { get; set; }
        public string Name { get; set; }

        public RoleModel() : this("") { }

        public RoleModel(string name)
        {
            this.Name = name;
            this.Selected = false;
        }
    }
}