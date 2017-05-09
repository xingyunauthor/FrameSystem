namespace Frame.Models.SysModels.MainWindow
{
    public class DocumentPanelCaptionModel
    {
        public DocumentPanelCaptionModel(int leftMenuId, string title)
        {
            LeftMenuId = leftMenuId;
            Title = title;
        }

        public int LeftMenuId { get; set; }
        public string Title { get; set; }

        public override string ToString()
        {
            return Title;
        }
    }
}
