using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Frame.Models.SysModels.Roles
{
    public class RoleEditRequestModel
    {
        public int RoleId { get; set; }
        public long Timestamp { get; set; }
    }

    public class RoleEditRequestNewModel
    {
        public string RoleName { get; set; }
        public long Timestamp { get; set; }
    }
}
