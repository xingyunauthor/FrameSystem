using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace Frame.Proxy.Windows
{
    public class PaginatorHeaderFooter : DocumentPaginator
    {
        readonly DocumentPaginator _paginator;
        private readonly string _companyName;

        public PaginatorHeaderFooter(DocumentPaginator paginator, string companyName)
        {
            _paginator = paginator;
            _companyName = companyName;
        }

        public override DocumentPage GetPage(int pageNumber)
        {
            DocumentPage page = _paginator.GetPage(pageNumber);
            ContainerVisual newpage = new ContainerVisual();

            //页眉:公司名称
            DrawingVisual header = new DrawingVisual();
            using (DrawingContext ctx = header.RenderOpen())
            {
                FormattedText text = new FormattedText(_companyName,
                    System.Globalization.CultureInfo.CurrentCulture, FlowDirection.LeftToRight,
                    new Typeface("Courier New"), 14, Brushes.Black);
                ctx.DrawText(text, new Point(page.ContentBox.Left, page.ContentBox.Top));
                ctx.DrawLine(new Pen(Brushes.Black, 0.5), new Point(page.ContentBox.Left, page.ContentBox.Top+16), new Point(page.ContentBox.Right, page.ContentBox.Top+16));
            }

            //页脚:第几页
            DrawingVisual footer = new DrawingVisual();
            using (DrawingContext ctx = footer.RenderOpen())
            {
                FormattedText text = new FormattedText("第" + (pageNumber+1) + "页",
                    System.Globalization.CultureInfo.CurrentCulture, FlowDirection.RightToLeft, 
                    new Typeface("Courier New"), 14, Brushes.Black);
                ctx.DrawText(text, new Point(page.ContentBox.Right, page.ContentBox.Bottom-20));
            }

            //将原页面微略压缩(使用矩阵变换)
            ContainerVisual mainPage = new ContainerVisual();
            mainPage.Children.Add(page.Visual);
            mainPage.Transform = new MatrixTransform(1, 0, 0, 0.95, 0, 0.025 * page.ContentBox.Height);

            //在现页面中加入原页面，页眉和页脚
            newpage.Children.Add(mainPage);
            newpage.Children.Add(header);
            newpage.Children.Add(footer);

            return new DocumentPage(newpage, page.Size, page.BleedBox, page.ContentBox);
        }

        public override bool IsPageCountValid
        {
            get
            {
                return _paginator.IsPageCountValid;
            }
        }

        public override int PageCount
        {
            get
            {
                return _paginator.PageCount;
            }
        }

        public override Size PageSize
        {
            get
            {
                return _paginator.PageSize;
            }

            set
            {
                _paginator.PageSize = value;
            }
        }

        public override IDocumentPaginatorSource Source
        {
            get
            {
                return _paginator.Source;
            }
        }
    }
}
