using System.Collections.ObjectModel;
using System.Windows.Documents;
using Frame.Proxy.Interfaces;
using System.Windows;
using Frame.Models.SysModels.Staff;

namespace Frame.SysWindows.Prints
{
    public class UserManagerPrintRender : IDocumentRender
    {
        public void Render(FlowDocument doc, object data)
        {
            var group = doc.FindName("RowsDetails") as TableRowGroup;
            var styleCell = doc.Resources["BorderedCell"] as Style;
            foreach (var item in ((UserManagerPrintModel)data).StaffAll)
            {
                var row = new TableRow();

                var cell = new TableCell(new Paragraph(new Run(item.Code))) { Style = styleCell };
                row.Cells.Add(cell);

                cell = new TableCell(new Paragraph(new Run(item.StaffName))) { Style = styleCell };
                row.Cells.Add(cell);

                cell = new TableCell(new Paragraph(new Run(item.DeptName))) { Style = styleCell };
                row.Cells.Add(cell);

                cell = new TableCell(new Paragraph(new Run(item.Sex))) { Style = styleCell, TextAlignment = TextAlignment.Center };
                row.Cells.Add(cell);

                cell = new TableCell(new Paragraph(new Run(item.InTime))) { Style = styleCell };
                row.Cells.Add(cell);

                cell = new TableCell(new Paragraph(new Run(item.Telephone))) { Style = styleCell };
                row.Cells.Add(cell);

                cell = new TableCell(new Paragraph(new Run(item.Address))) { Style = styleCell };
                row.Cells.Add(cell);

                group?.Rows.Add(row);
            }
        }
    }
}
