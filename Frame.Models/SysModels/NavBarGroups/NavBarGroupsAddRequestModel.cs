using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Frame.Models.SysModels.NavBarGroups
{
    public class NavBarGroupsAddRequestModel
    {
        public string NavBarGroupName { get; set; }
        public string IcoPath { get; set; }
        public int Sort { get; set; }
        public long Timestamp { get; set; }
    }
}
