using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Frame.SysWindows.Controls
{
    /// <summary>
    /// LoginThemeManager.xaml 的交互逻辑
    /// </summary>
    public partial class LoginThemeManager : UserControl
    {
        public LoginThemeManager()
        {
            InitializeComponent();

            for (int i = 0; i < 10; i++)
            {
                DockPanelMain.Children.Add(LoadTheme());
            }
        }

        private UIElement LoadTheme()
        {
            var reader =
                new StreamReader($"{AppDomain.CurrentDomain.BaseDirectory}LogonComponents\\default\\default.xaml",
                    Encoding.UTF8);
            var val = reader.ReadToEnd()
                .Replace(" Source=\"", $" Source=\"{AppDomain.CurrentDomain.BaseDirectory}LogonComponents\\default\\");
            reader.Dispose();
            var bytes = Encoding.UTF8.GetBytes(val);
            var stream = new MemoryStream(bytes);

            var grid = XamlReader.Load(stream) as UIElement;
            stream.Dispose();

            return grid;
        }
    }
}
