namespace Frame.Models.SysModels.TopMenus
{
    public class TopMenusAddRequestModel
    {
        public string DisplayName { get; set; }
        public string DllPath { get; set; }
        public string EntryFunction { get; set; }
        public string MenuId { get; set; }
        public int ParentId { get; set; }
        public int Sort { get; set; }
        public long Timestamp { get; set; }
    }
}
