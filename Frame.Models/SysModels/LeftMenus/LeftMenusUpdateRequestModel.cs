namespace Frame.Models.SysModels.LeftMenus
{
    public class LeftMenusUpdateRequestModel
    {
        public string DisplayName { get; set; }
        public string DllPath { get; set; }
        public string EntryFunction { get; set; }
        public string Ico { get; set; }
        public string MenuId { get; set; }
        public int NavBarGroupId { get; set; }
        public int ParentId { get; set; }
        public bool StartWithSys { get; set; }
        public long Timestamp { get; set; }
    }

    public class LeftMenusUpdateSortRequestModel
    {
        public string DisplayName { get; set; }
        public int Sort { get; set; }
        public long Timestamp { get; set; }
    }
}
