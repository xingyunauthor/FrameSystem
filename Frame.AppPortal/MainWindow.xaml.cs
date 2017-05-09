using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Docking;
using Frame.AppPortal.MVModels;
using Frame.Business;
using Frame.Business.interfaces;
using Frame.Login.MVModels;
using Frame.Models;
using Frame.Models.SettingModels;
using Frame.Models.SysModels;
using Frame.Models.SysModels.MainWindow;
using Frame.Models.SysModels.Operator;
using Frame.Models.SysModels.TopMenus;
using Frame.Proxy;
using Frame.SysWindows.Controls;
using Frame.Utils;
using MahApps.Metro.Controls.Dialogs;

namespace Frame.AppPortal
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow
    {
        private readonly ILeftMenuPermissionsManage _leftMenuPermissionsManage = new LeftMenuPermissionsManage();
        private readonly IOperatorManage _operatorManage = new OperatorManage();
        private readonly ILeftMenusManage _leftMenusManage = new LeftMenusManage();
        private readonly ITopMenusManage _topMenusManage = new TopMenusManage();
        private readonly IBannerManage _bannerManage = new BannerManage();

        private bool _closeReally;
        private LeftMenu _leftMenu;
        private readonly ICompanyManage _companyManage = new CompanyManage();
        public static readonly ClsLoginModel ClsLoginModel = new ClsLoginModel();
        private readonly MainWindowModel _mainWindowModel = new MainWindowModel();

        public MainWindow()
        {
            var userLogon = new Login.Login();
            userLogon.LogonSuccessEvent += (userId, logonName, role) =>
            {
                InitClsLoginModel(userId, logonName, role);
                InitializeComponent();
                DataContext = _mainWindowModel;

                var roleLeftMenus = _leftMenusManage.GetLeftMenusesByRoleId(ClsLoginModel.RoleId);
                _leftMenu = new LeftMenu(roleLeftMenus);
                InitDataContext();
                InitLeftMenu();
                InitTopMenu();
                InitBanner();
                InitDocumentGroupMainItems();
            };
            userLogon.CancelLogonEvent += () =>
            {
                _closeReally = true;
                Close();
            };
            userLogon.ShowDialog();
        }

        /// <summary>
        /// 初始化 DataContext
        /// </summary>
        private void InitDataContext()
        {
            var company = _companyManage.GetSettingModel() ?? new Company();
            _mainWindowModel.CompanyName = company.Name;
            _mainWindowModel.Copyright = company.Copyright;
            _mainWindowModel.LogonName = ClsLoginModel.LoginName;
        }

        /// <summary>
        /// 初始化 ClsLoginModel
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="logonName"></param>
        /// <param name="role"></param>
        private void InitClsLoginModel(int userId, string logonName, Role role)
        {
            var conn = _leftMenuPermissionsManage.GetConnection();
            ClsLoginModel.UserId = userId;
            ClsLoginModel.LoginName = logonName;
            ClsLoginModel.LoginTime = DateTime.Now;
            ClsLoginModel.RoleId = role.RoleId;
            ClsLoginModel.ConnectionString = conn.ConnectionString;
            ClsLoginModel.DataSource = conn.DataSource;
            ClsLoginModel.Database = conn.Database;
            ClsLoginModel.PermissionsFunc =
                (menuId, permissionsId) =>
                        _leftMenuPermissionsManage.PermissionHave(menuId, permissionsId, role.RoleId);
        }

        /// <summary>
        /// 初始化 LeftMenu
        /// </summary>
        private void InitLeftMenu()
        {
            _leftMenu.ChildItemSelected += LeftMenu_ChildItemSelected;
            DocumentPanelLeftMenu.Content = _leftMenu;
        }

        /// <summary>
        /// 初始化顶部菜单
        /// </summary>
        private void InitTopMenu()
        {
            var all = _topMenusManage.GetAllMenusHierarchicalData();
            foreach (var model in all)
            {
                var barSubItem = new BarSubItem { Content = model.DisplayName, DataContext = model };
                TopMainMenuControl.Items.Add(barSubItem);
                GenerateTopMenu(barSubItem, model.Nodes);
            }
        }

        /// <summary>
        /// 递归生成 Menu
        /// </summary>
        /// <param name="barSubItem"></param>
        /// <param name="nodes"></param>
        private void GenerateTopMenu(BarSubItem barSubItem, ObservableCollection<AllTopMenusHierarchicalDataModel> nodes)
        {
            foreach (var node in nodes)
            {
                if (node.Nodes.Any())
                {
                    var newBarSubItem = new BarSubItem {Content = node.DisplayName, DataContext = node};
                    barSubItem.Items.Add(newBarSubItem);
                    GenerateTopMenu(newBarSubItem, node.Nodes);
                }
                else
                {
                    var barButtonItem = new BarButtonItem {Content = node.DisplayName, DataContext = node};
                    barSubItem.Items.Add(barButtonItem);
                    barButtonItem.ItemClick += TopMenu_ItemClick;
                }
            }
        }

        /// <summary>
        /// 初始化加载 Banner
        /// </summary>
        private void InitBanner()
        {
            var model = _bannerManage.GetSettingModel();
            if (model != null && model.Enabled)
            {
                var control = GetControl(model.DllPath, model.EntryFunction, model.MenuId);
                GridBanner.Children.Add(control);
            }
        }

        /// <summary>
        /// 初始化加载随系统启动的菜单
        /// </summary>
        private void InitDocumentGroupMainItems()
        {
            var list = _leftMenusManage.GetStartWithSysLeftMenuses();
            System.Threading.Tasks.Task.Factory.StartNew(() =>
            {
                try
                {
                    list.ForEach(a =>
                    {
                        Dispatcher.Invoke((Action) (() => ControlShowOrAddToDocumentGroup(new DocumentPanelCaptionModel(a.Id, a.DisplayName), a.DllPath, a.EntryFunction, a.MenuId)));
                    });
                }
                catch (Exception ex)
                {
                    Dispatcher.Invoke((Action)(() => { throw ex; }));
                }
            });
        }

        /// <summary>
        /// 顶部菜单选中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TopMenu_ItemClick(object sender, ItemClickEventArgs e)
        {
            var caption = (AllTopMenusHierarchicalDataModel) ((BarButtonItem) sender).DataContext;
            ControlShowOrAddToDocumentGroup(new DocumentPanelCaptionModel(caption.Id, caption.DisplayName), caption.DllPath, caption.EntryFunction, caption.MenuId);
        }

        /// <summary>
        /// 点击左侧菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LeftMenu_ChildItemSelected(TreeViewItem sender, RoutedEventArgs e)
        {
            var caption = (LeftMenus) sender.DataContext;
            ControlShowOrAddToDocumentGroup(new DocumentPanelCaptionModel(caption.Id, caption.DisplayName), caption.DllPath, caption.EntryFunction, caption.MenuId);
        }

        /// <summary>
        /// 处理 Control 控件的显示方式
        /// </summary>
        /// <param name="caption"></param>
        /// <param name="dllPath"></param>
        /// <param name="entryFunction"></param>
        /// <param name="menuId"></param>
        private void ControlShowOrAddToDocumentGroup(DocumentPanelCaptionModel caption, string dllPath, string entryFunction, string menuId)
        {
            var content = GetControl(dllPath, entryFunction, menuId);
            if (content == null) return;
            if ((content as Window) != null)
                ((Window) content).ShowDialog();
            else
                DocumentGroupItemAdd(caption, content);
        }

        private void DocumentGroupItemAdd(DocumentPanelCaptionModel caption, Control content)
        {
            content.Margin = new Thickness(5);
            
            var items = DocumentGroupMain.Items;
            var singleOrDefaultPanel = items.SingleOrDefault(a => ((DocumentPanelCaptionModel) a.Caption).LeftMenuId == caption.LeftMenuId);
            if (singleOrDefaultPanel == null)
            {
                var documentPanel = new DocumentPanel
                {
                    Caption = caption,
                    CustomizationCaption = caption.ToString(),
                    AllowFloat = false,
                    AllowHide = false,
                    Content = content,
                    IsActive = true
                };
                items.Add(documentPanel);
            }
            else
                singleOrDefaultPanel.IsActive = true;
        }

        /// <summary>
        /// 根据 DllPath, MenuId 返回控件
        /// </summary>
        /// <param name="dllRelativePath"></param>
        /// <param name="entryFunction"></param>
        /// <param name="menuId">唯一标识</param>
        /// <returns></returns>
        private Control GetControl(string dllRelativePath, string entryFunction, string menuId)
        {
            var dllPath = $"{Config.DevPlatformPath}{dllRelativePath}";
            if (System.IO.File.Exists(dllPath))
            {
                var ass = Assembly.LoadFile(dllPath);
                var instance = ass.CreateInstance(entryFunction);
                var loginModel = new ClsLoginModel
                {
                    UserId = ClsLoginModel.UserId,
                    LoginName = ClsLoginModel.LoginName,
                    LoginTime = ClsLoginModel.LoginTime,
                    RoleId = ClsLoginModel.RoleId,
                    ConnectionString = ClsLoginModel.ConnectionString,
                    DataSource = ClsLoginModel.DataSource,
                    Database = ClsLoginModel.Database,
                    PermissionsFunc = ClsLoginModel.PermissionsFunc
                };
                var control = ((INetUserControl)instance)?.CreateControl(this, loginModel, menuId);
                if (control != null)
                    return control;
            }
            else
                this.ShowMessageAsync($"文件路径“{dllPath}”不存在，打开窗口失败！", "");
            return null;
        }

        /// <summary>
        /// 主窗体关闭时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindow_OnClosing(object sender, CancelEventArgs e)
        {
            if (!_closeReally)
            {
                e.Cancel = true;
                var settings = new MetroDialogSettings
                {
                    AnimateShow = true,
                    AffirmativeButtonText = "确定",
                    NegativeButtonText = "取消"
                };
                this.ShowMessageAsync("退出提示", "您确定要推出当前系统吗？", MessageDialogStyle.AffirmativeAndNegative, settings)
                    .ContinueWith(
                        task =>
                        {
                            if (task.Result == MessageDialogResult.Affirmative)
                            {
                                _closeReally = true;
                                Dispatcher.Invoke((Action)Close);
                            }
                        });
            }
        }

        private void MainWindow_OnClosed(object sender, EventArgs e)
        {
            Application.Current?.Shutdown();
        }

        /// <summary>
        /// 锁屏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonClockScreen_OnClick(object sender, RoutedEventArgs e)
        {
            this.ShowLoginAsync("屏幕锁定", "请输入正确的密码解锁", new LoginDialogSettings
            {
                AffirmativeButtonText = "解锁",
                ShouldHideUsername = true,
                NegativeButtonVisibility = Visibility.Collapsed,
                PasswordWatermark = "密码……",
                EnablePasswordPreview = true
            }).ContinueWith(data =>
            {
                var pwd = data.Result.Password;
                var logonResult = _operatorManage.Login(new OperatorLogonRequestModel
                {
                    LogonName = ClsLoginModel.LoginName,
                    LogonPwd = pwd,
                    RoleId = ClsLoginModel.RoleId
                });
                if (logonResult.ResultStatus == ResultStatus.Error)
                {
                    Dispatcher.Invoke((Action)(() =>
                    {
                        this.ShowMessageAsync("错误提示", logonResult.Message).ContinueWith(task =>
                        {
                            Dispatcher.Invoke((Action)(() =>
                            {
                                ButtonClockScreen_OnClick(sender, e);
                            }));
                        });
                    }));
                }
            });
        }

        /// <summary>
        /// 用户注销
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonLogout_OnClick(object sender, RoutedEventArgs e)
        {
            var setting = new MetroDialogSettings
            {
                AnimateShow = true,
                AnimateHide = true,
                AffirmativeButtonText = "确定",
                NegativeButtonText = "取消",
                DefaultButtonFocus = MessageDialogResult.Negative
            };
            this.ShowMessageAsync("友情提示", "您确定要切换用户吗？", MessageDialogStyle.AffirmativeAndNegative, setting).ContinueWith(a =>
            {
                if (a.Result == MessageDialogResult.Affirmative)
                {
                    Dispatcher.Invoke((Action) (() =>
                    {
                        var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                        _closeReally = true;
                        Close();

                        Process.Start($"{baseDirectory}Frame.AppPortal.exe");
                    }));
                }
            });
        }

        #region 工具栏
        #endregion
    }
}
