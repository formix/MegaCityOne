using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MegaCityOne.Mvc
{
    [Serializable]
    public class UserData
    {
        public string Name { get; set; }
        public string[] Roles { get; set; }
        public IDictionary<string, object> Data { get; set; }

        public UserData()
        {
            this.Name = "";
            this.Roles = new string[0];
            this.Data = new Dictionary<string, object>();
        }
    }
}
