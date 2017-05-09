using System.Windows.Controls;
using Frame.Proxy;
using Frame.SysWindows.Controls;
using Frame.SysWindows.Windows.Common;
using Frame.SysWindows.Windows.Company;
using MahApps.Metro.Controls;

namespace Frame.SysWindows
{
    public class NetUserControl : INetUserControl
    {
        public Control CreateControl(MetroWindow metroWindow, ClsLoginModel clsLogin, string menuId)
        {
            var control = new Control();
            switch (menuId)
            {
                case "顶部菜单管理":
                    control = new TopMenuManager(metroWindow, clsLogin, menuId);
                    break;
                case "左侧菜单管理":
                    control = new LeftMenuManager(metroWindow, clsLogin, menuId);
                    break;
                case "功能组管理":
                    control = new NavBarGroupManager(metroWindow, clsLogin, menuId);
                    break;
                case "登陆主题设置":
                    control = new LoginThemeManager();
                    break;
                case "权限管理":
                    control = new RolesManager(metroWindow, clsLogin, menuId);
                    break;
                case "用户管理":
                    control = new UserManager(metroWindow, clsLogin, menuId);
                    break;
                case "部门管理":
                    control = new DeptManager(metroWindow, clsLogin, menuId);
                    break;
                case "登陆日志管理":
                    control = new LoginLogManager(metroWindow, clsLogin, menuId);
                    break;
                case "操作员管理":
                    control = new OperatorManager(metroWindow, clsLogin, menuId);
                    break;
                case "公司信息设置":
                    control = new CompanyEdit(clsLogin, menuId) { Owner = metroWindow };
                    break;
                case "修改密码":
                    control = new ModifyPassword(clsLogin) { Owner = metroWindow };
                    break;
                case "系统初始化":
                    control = new SystemInit(clsLogin) { Owner = metroWindow };
                    break;
                case "Banner 设置":
                    control = new BannerManager(metroWindow, clsLogin, menuId);
                    break;
                case "Banner 默认":
                    control = new BannerDefault(metroWindow);
                    break;
            }
            return control;
        }
    }
}
