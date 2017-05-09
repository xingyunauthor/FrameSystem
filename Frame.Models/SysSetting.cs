using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Frame.Models
{
    [Table(nameof(SysSetting))]
    public class SysSetting
    {
        [Key]
        public int Id { get; set; }
        public string ColumnName { get; set; }
        public string Value { get; set; }
        public int GroupId { get; set; }
    }
}
