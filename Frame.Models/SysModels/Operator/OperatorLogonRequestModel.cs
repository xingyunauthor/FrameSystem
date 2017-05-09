namespace Frame.Models.SysModels.Operator
{
    public class OperatorLogonRequestModel
    {
        /// <summary>
        /// 登陆名称
        /// </summary>
        public string LogonName { get; set; }

        /// <summary>
        /// 登陆密码
        /// </summary>
        public string LogonPwd { get; set; }

        /// <summary>
        /// 角色编号
        /// </summary>
        public int RoleId { get; set; }
    }
}
