using System.Collections.Generic;
using System.Windows;
using System.Windows.Documents;
using Frame.Models.SysModels.Log;
using Frame.Proxy.Interfaces;

namespace Frame.SysWindows.Prints
{
    class LoginLogManagerPrintRender : IDocumentRender
    {
        public void Render(FlowDocument doc, object data)
        {
            var group = doc.FindName("RowsDetails") as TableRowGroup;
            var styleCell = doc.Resources["BorderedCell"] as Style;
            foreach (var item in ((List<LogAllResponseModel>)data))
            {
                var row = new TableRow();

                var cell = new TableCell(new Paragraph(new Run(item.RowId.ToString()))) { Style = styleCell, TextAlignment = TextAlignment.Center };
                row.Cells.Add(cell);

                cell = new TableCell(new Paragraph(new Run(item.LoginName))) { Style = styleCell };
                row.Cells.Add(cell);

                cell = new TableCell(new Paragraph(new Run(item.LoginTime))) { Style = styleCell };
                row.Cells.Add(cell);

                cell = new TableCell(new Paragraph(new Run(item.LoginRole))) { Style = styleCell };
                row.Cells.Add(cell);

                cell = new TableCell(new Paragraph(new Run(item.LoginMach))) { Style = styleCell };
                row.Cells.Add(cell);

                group?.Rows.Add(row);
            }
        }
    }
}
