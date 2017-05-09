using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Frame.Models
{
    [Table(nameof(StaffRoleRelationships))]
    public class StaffRoleRelationships
    {
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// 员工编号
        /// </summary>
        public int StaffId { get; set; }
        /// <summary>
        /// 角色编号
        /// </summary>
        public int RoleId { get; set; }

        [ForeignKey(nameof(StaffId))]
        public Staff StaffForeign { get; set; }

        [ForeignKey(nameof(RoleId))]
        public Roles RolesForeign { get; set; }
    }
}
