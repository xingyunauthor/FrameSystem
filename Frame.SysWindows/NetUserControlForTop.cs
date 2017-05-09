using System.Windows.Controls;
using Frame.Proxy;
using Frame.SysWindows.Windows.Common;
using MahApps.Metro.Controls;
using System.Diagnostics;

namespace Frame.SysWindows
{
    public class NetUserControlForTop : INetUserControl
    {
        public Control CreateControl(MetroWindow metroWindow, ClsLoginModel clsLogin, string menuId)
        {
            switch (menuId)
            {
                case "退出":
                    metroWindow.Close();
                    return null;
                case "Bug 反馈":
                    Process.Start("http://www.devapplication.com");
                    return null;
                case "关于我们":
                    return new About { Owner = metroWindow };
                default:
                    return null;
            }
        }
    }
}
