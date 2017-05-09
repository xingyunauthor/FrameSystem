
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Frame.Models
{
    [Table(nameof(Dept))]
    public class Dept
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// 父级编号
        /// </summary>
        public int PId { get; set; }
        /// <summary>
        /// 部门名称
        /// </summary>
        public string Name { get; set; }
        
        public override string ToString()
        {
            return Name;}
    }
}