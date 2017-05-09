using System.Windows.Documents;

namespace Frame.Proxy.Interfaces
{
    public interface IDocumentRender
    {
        void Render(FlowDocument doc, object data);
    }
}
