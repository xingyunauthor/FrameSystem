using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Frame.Models
{
    [Table(nameof(Staff))]
    public class Staff
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// 用户标识
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 所属部门
        /// </summary>
        public int DeptId { get; set; }
        /// <summary>
        /// 真实姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public int Sex { get; set; }
        /// <summary>
        /// 生日
        /// </summary>
        public DateTime Birth { get; set; }
        /// <summary>
        /// 加入时间
        /// </summary>
        public DateTime InTime { get; set; }
        /// <summary>
        /// 联系方式
        /// </summary>
        public string Tel { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Add { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool State { get; set; }
        /// <summary>
        /// 操作人
        /// </summary>
        public string Oper { get; set; }
        /// <summary>
        /// 登陆编号
        /// </summary>
        public string LogonId { get; set; }
        /// <summary>
        /// 登陆名称
        /// </summary>
        public string LogonName { get; set; }
        /// <summary>
        /// 登陆密码
        /// </summary>
        public string LogonPwd { get; set; }
        /// <summary>
        /// 是否是超级管理员
        /// </summary>
        public bool Supper { get; set; }

        [ForeignKey(nameof(DeptId))]
        public Dept DeptForeign { get; set; }
    }
}