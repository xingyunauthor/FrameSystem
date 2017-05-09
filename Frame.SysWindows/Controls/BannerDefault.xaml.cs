using System.Diagnostics;
using System.Windows;
using MahApps.Metro.Controls;

namespace Frame.SysWindows.Controls
{
    /// <summary>
    /// BannerDefault.xaml 的交互逻辑
    /// </summary>
    public partial class BannerDefault
    {
        private readonly MetroWindow _metroWindow;
        public BannerDefault(MetroWindow metroWindow)
        {
            InitializeComponent();
            _metroWindow = metroWindow;
        }

        /// <summary>
        /// 退出系统
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnExit_OnClick(object sender, RoutedEventArgs e)
        {
            _metroWindow.Close();
        }

        /// <summary>
        /// 打开计算器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnOpenCalculator_OnClick(object sender, RoutedEventArgs e)
        {
            var startInfo = new ProcessStartInfo {FileName = @"C:\WINDOWS\system32\calc.exe"};
            Process.Start(startInfo);
        }
    }
}
