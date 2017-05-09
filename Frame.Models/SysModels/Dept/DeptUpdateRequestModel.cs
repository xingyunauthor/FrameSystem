namespace Frame.Models.SysModels.Dept
{
    public class DeptUpdateRequestModel
    {
        /// <summary>
        /// 部门名称
        /// </summary>
        public string DeptName { get; set; }

        /// <summary>
        /// 父级编号
        /// </summary>
        public int ParentId { get; set; }
    }
}