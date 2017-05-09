using System;
using System.IO;
using System.IO.Packaging;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Markup;
using System.Windows.Threading;
using System.Windows.Xps;
using System.Windows.Xps.Packaging;
using Frame.Proxy.Interfaces;

namespace Frame.Proxy.Windows
{
    partial class PrintPreviewWindow
    {
        private delegate void LoadXpsMethod();
        private readonly FlowDocument _doc;
        private static string _companyName;

        public static FlowDocument LoadDocumentAndRender(string xamlPath, object data, IDocumentRender renderer=null)
        {
            var stream = Application.GetResourceStream(new Uri(xamlPath, UriKind.RelativeOrAbsolute))?.Stream;
            if (stream != null)
            {
                var doc = XamlReader.Load(stream) as FlowDocument;
                stream.Dispose();
                if (doc != null)
                {
                    doc.PagePadding = new Thickness(50);
                    doc.DataContext = data;
                    if (renderer != null)
                    {
                        renderer.Render(doc, data);
                    }
                    return doc;
                }
            }
            return new FlowDocument();
        }

        public static DocumentPaginator GetPaginator(FlowDocument doc)
        {
            bool? bPrintHeaderAndFooter = doc.Resources["PrintHeaderAndFooter"] as bool?;
            if (bPrintHeaderAndFooter==true)
            {
                return new PaginatorHeaderFooter(((IDocumentPaginatorSource)doc).DocumentPaginator, _companyName);
            }
            else
            {
                return ((IDocumentPaginatorSource) doc).DocumentPaginator;
            }
        }

        public PrintPreviewWindow(string companyName, string xamlPath, object data, IDocumentRender renderer=null)
        {
            InitializeComponent();
            _companyName = companyName;
            _doc = LoadDocumentAndRender(xamlPath, data, renderer);
            Dispatcher.BeginInvoke(new LoadXpsMethod(LoadXps), DispatcherPriority.ApplicationIdle);
        }

        public void LoadXps()
        {
            //构造一个基于内存的xps document
            MemoryStream ms = new MemoryStream();
            Package package = Package.Open(ms, FileMode.Create, FileAccess.ReadWrite);
            Uri documentUri = new Uri("pack://InMemoryDocument.xps");
            PackageStore.RemovePackage(documentUri);
            PackageStore.AddPackage(documentUri, package);
            XpsDocument xpsDocument = new XpsDocument(package, CompressionOption.Fast, documentUri.AbsoluteUri);

            //将flow document写入基于内存的xps document中去
            XpsDocumentWriter writer = XpsDocument.CreateXpsDocumentWriter(xpsDocument);
            writer.Write(GetPaginator(_doc));

            //获取这个基于内存的xps document的fixed document
            DocViewer.Document = xpsDocument.GetFixedDocumentSequence();

            //关闭基于内存的xps document
            xpsDocument.Close();
        }
    }
}
