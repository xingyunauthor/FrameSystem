using System.Windows;
using System.Windows.Media;
using MahApps.Metro.Controls;

namespace Frame.Proxy
{
    public class BaseWindow : MetroWindow
    {
        public BaseWindow()
        {
            var brush = new BrushConverter().ConvertFromString("#0072C6") as Brush;
            BorderBrush = brush;
            GlowBrush = brush;

            ResizeMode = ResizeMode.NoResize;
            WindowStartupLocation = WindowStartupLocation.CenterOwner;
        }
    }
}
