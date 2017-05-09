using System;

namespace Frame.Proxy
{
    public class ClsLoginModel
    {
        /// <summary>
        /// 用户编号
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 登陆用户
        /// </summary>
        public string LoginName { get; set; }
        /// <summary>
        /// 登陆时间
        /// </summary>
        public DateTime LoginTime { get; set; }
        /// <summary>
        /// 当前所属角色
        /// </summary>
        public int RoleId { get; set; }
        /// <summary>
        /// 连接字符串
        /// </summary>
        public string ConnectionString { get; set; }
        /// <summary>
        /// 服务器名称
        /// </summary>
        public string DataSource { get; set; }
        /// <summary>
        /// 数据库名称
        /// </summary>
        public string Database { get; set; }

        /// <summary>
        /// 根据菜单编号，权限编号查看是否有权限
        /// 第一个参数 string：menuId（菜单编号）
        /// 第二个参数 int：permissionsId（权限编号）
        /// </summary>
        public PermissionsFuncEventHandler PermissionsFunc { get; set; }
    }
}
