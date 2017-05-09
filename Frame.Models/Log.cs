using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Frame.Models
{
    [Table(nameof(Log))]
    public class Log
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// 登录名
        /// </summary>
        public string LoginName { get; set; }

        /// <summary>
        /// 登陆时间
        /// </summary>
        public DateTime LoginTime { get; set; }

        /// <summary>
        /// 登陆角色
        /// </summary>
        public string LoginRole { get; set; }

        /// <summary>
        /// 登陆计算机名
        /// </summary>
        public string LoginMach { get; set; }

        /// <summary>
        /// 登陆cpu
        /// </summary>
        public string LoginCpu { get; set; }

    }
}