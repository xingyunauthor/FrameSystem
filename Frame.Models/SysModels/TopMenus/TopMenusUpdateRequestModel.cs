namespace Frame.Models.SysModels.TopMenus
{
    public class TopMenusUpdateRequestModel
    {
        public string DisplayName { get; set; }
        public string DllPath { get; set; }
        public string EntryFunction { get; set; }
        public string Ico { get; set; }
        public string MenuId { get; set; }
        public int ParentId { get; set; }
        public long Timestamp { get; set; }
    }

    public class TopMenusUpdateSortRequestModel
    {
        public string DisplayName { get; set; }
        public int Sort { get; set; }
        public long Timestamp { get; set; }
    }
}
