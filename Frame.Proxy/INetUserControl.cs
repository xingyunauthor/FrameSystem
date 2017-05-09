using System.Windows.Controls;
using MahApps.Metro.Controls;

namespace Frame.Proxy
{
    public delegate bool PermissionsFuncEventHandler(string menuId, int permissionsId);

    public interface INetUserControl
    {
        Control CreateControl(MetroWindow metroWindow, ClsLoginModel clsLogin, string menuId);
    }
}
