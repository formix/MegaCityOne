using MegaCityOne.Example.Mvc.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MegaCityOne.Example.Mvc.Models
{
    public class HomeModel
    {
        public string User { get; set; }
        public List<RoleModel> Roles { get; set; }


        public HomeModel()
        {
            this.Roles = new List<RoleModel>();
            this.Roles.Add(new RoleModel("Administrator"));
            this.Roles.Add(new RoleModel("ProjectManager"));
        }


        public void SetSelected(string roleName, bool value)
        {
            foreach (var role in this.Roles)
            {
                if (role.Name == roleName)
                {
                    role.Selected = value;
                }
            }
        }
    }
}