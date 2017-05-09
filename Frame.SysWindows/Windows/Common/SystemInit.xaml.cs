using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Frame.Proxy;

namespace Frame.SysWindows.Windows.Common
{
    /// <summary>
    /// SystemInit.xaml 的交互逻辑
    /// </summary>
    public partial class SystemInit
    {
        private readonly ClsLoginModel _clsLogin;
        public SystemInit(ClsLoginModel clsLogin)
        {
            InitializeComponent();
            _clsLogin = clsLogin;
        }

        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnConfirm_OnClick(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCancel_OnClick(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
