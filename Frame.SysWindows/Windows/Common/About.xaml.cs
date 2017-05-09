using System.Diagnostics;
using System.Windows;

namespace Frame.SysWindows.Windows.Common
{
    public partial class About
    {
        public About()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 打开官网
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnOpenWebsite_OnClick(object sender, RoutedEventArgs e)
        {
            Process.Start("http://www.ithome.com");
        }
    }
}
