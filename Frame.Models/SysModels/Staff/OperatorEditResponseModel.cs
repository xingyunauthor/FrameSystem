using System.Collections.Generic;

namespace Frame.Models.SysModels.Staff
{
    public class OperatorEditResponseModel
    {
        /// <summary>
        /// 员工姓名
        /// </summary>
        public string LogonName { get; set; }

        /// <summary>
        /// 员工密码
        /// </summary>
        public string LogonPwd { get; set; }

        /// <summary>
        /// 权限
        /// </summary>
        public List<int> RoleIdes { get; set; }
    }
}
