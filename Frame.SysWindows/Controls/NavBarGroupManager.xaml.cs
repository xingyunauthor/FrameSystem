using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using DevExpress.Xpf.NavBar;
using Frame.Business;
using Frame.Business.interfaces;
using Frame.Models;
using Frame.Models.SysModels;
using Frame.Models.SysModels.NavBarGroups;
using Frame.Proxy;
using Frame.Proxy.Enums;
using Frame.SysWindows.Windows.Common;
using Frame.Utils;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace Frame.SysWindows.Controls
{
    /// <summary>
    /// NavBarGroupManager.xaml 的交互逻辑
    /// </summary>
    public partial class NavBarGroupManager : INotifyPropertyChanged, IDataErrorInfo
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private readonly MetroWindow _metroWindow;
        private readonly ClsLoginModel _clsLoginModel;
        private readonly string _menuId;
        private LeftMenu _leftMenu;

        private readonly INavBarGroupsManage _navBarGroupsManage = new NavBarGroupsManage();
        private readonly ILeftMenusManage _leftMenusManage = new LeftMenusManage();

        public NavBarGroupManager(MetroWindow metroWindow, ClsLoginModel clsLoginModel, string menuId)
        {
            InitializeComponent();
            _metroWindow = metroWindow;
            _clsLoginModel = clsLoginModel;
            _menuId = menuId;
            DataContext = this;

            InitLeftMenu();
            ShowDockPanelNew();
        }

        /// <summary>
        /// 初始化 NavBarGroups
        /// </summary>
        private void InitLeftMenu()
        {
            var allLeftMenus = _leftMenusManage.GetAll();
            _leftMenu = new LeftMenu(allLeftMenus);
            GridNavBarGroup.Children.Add(_leftMenu);
            _leftMenu.NavBarGroupActivate += LeftMenu_OnNavBarGroupActivate;

            var nav = _leftMenu.NavBarControlMain.ActiveGroup?.DataContext as NavBarGroups;
            if (nav != null)
                InitActiveGroupRightData(nav);
        }

        /// <summary>
        /// 激活功能组菜单后
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LeftMenu_OnNavBarGroupActivate(object sender, EventArgs e)
        {
            var navBarGroupModel = (NavBarGroups)((NavBarGroup)sender).DataContext;
            InitActiveGroupRightData(navBarGroupModel);

            ShowDockPanelModify();
        }

        /// <summary>
        /// 图片选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnIcoSelect_OnClick(object sender, RoutedEventArgs e)
        {
            Action<FilePathSelTreeView> callback = m =>
            {
                IcoPath = m.FullPath.Remove(0, AppDomain.CurrentDomain.BaseDirectory.Length);
            };
            var navBarGroupIcoRefer = new NavBarGroupIcoRefer(callback) { Owner = _metroWindow };
            navBarGroupIcoRefer.ShowDialog();
        }

        /// <summary>
        /// 清空
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnClear_OnClick(object sender, RoutedEventArgs e)
        {
            ClearData();
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAdd_OnClick(object sender, RoutedEventArgs e)
        {
            if (!_clsLoginModel.PermissionsFunc(_menuId, (int) PermissionsEnum.添加))
            {
                _metroWindow.ShowMessageAsync("友情提示", "您没有添加功能分组的权限");
                return;
            }
            if (!Verify()) return;
            if (IcoSource == null)
            {
                _metroWindow.ShowMessageAsync("友情提示", "请选择图标文件");
                return;
            }

            var requestModel = new NavBarGroupsAddRequestModel
            {
                NavBarGroupName = NavBarGroupName,
                IcoPath = IcoPath,
                Sort = Convert.ToInt32(Sort),
                Timestamp = _navBarGroupsManage.ServerTime.ToUnixTimestamp()
            };
            var result = _navBarGroupsManage.Add(requestModel);
            if (result.ResultStatus == ResultStatus.Success)
            {
                InitLeftMenu();
                ClearData();
            }
            _metroWindow.ShowMessageAsync(result.ResultStatus == ResultStatus.Success ? "新增成功提示" : "新增失败提示", result.Message);
        }

        /// <summary>
        /// 重置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnReset_OnClick(object sender, RoutedEventArgs e)
        {
            if (!_navBarGroupsManage.Exist(a => a.Id == NavBarGroupId))
                _metroWindow.ShowMessageAsync("重置失败", $"该“{NavBarGroupName}”功能组信息在系统中已不存在！");
            else
            {
                var entity = _navBarGroupsManage.GetNavBarGroupById(NavBarGroupId);
                InitActiveGroupRightData(entity);

                _leftMenu.ResetActiveGroup(entity);
            }
        }

        /// <summary>
        /// 保存修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnEdit_OnClick(object sender, RoutedEventArgs e)
        {
            if (!_clsLoginModel.PermissionsFunc(_menuId, (int) PermissionsEnum.修改))
            {
                _metroWindow.ShowMessageAsync("友情提示", "您没有权限修改功能分组信息");
                return;
            }
            if (!Verify()) return;
            if (IcoSource == null)
            {
                _metroWindow.ShowMessageAsync("友情提示", "请选择图标文件");
                return;
            }

            var old = (NavBarGroups)_leftMenu.NavBarControlMain.ActiveGroup.DataContext;
            var setting = new MetroDialogSettings
            {
                AnimateShow = true,
                AnimateHide = true,
                FirstAuxiliaryButtonText = "是",
                NegativeButtonText = "否",
                DefaultButtonFocus = MessageDialogResult.Negative
            };
            _metroWindow.ShowMessageAsync("安全提示", "修改后将无法恢复成原有数据，您是否继续修改此信息？", MessageDialogStyle.AffirmativeAndNegative, setting).ContinueWith(
                t =>
                {
                    if (t.Result == MessageDialogResult.Affirmative)
                    {
                        _metroWindow.Dispatcher.Invoke((Action)(() =>
                        {
                            var requestModel = new NavBarGroupsUpdateRequestModel
                            {
                                NavBarGroupName = NavBarGroupName,
                                IcoPath = IcoPath,
                                Sort = Convert.ToInt32(Sort),
                                Timestamp = _navBarGroupsManage.ServerTime.ToUnixTimestamp()
                            };
                            var result = _navBarGroupsManage.Update(NavBarGroupId, old.Timestamp, requestModel);
                            if(result.ResultStatus == ResultStatus.Success)
                                _leftMenu.ResetActiveGroup(result.Data);
                            _metroWindow.ShowMessageAsync(result.ResultStatus == ResultStatus.Success ? "保存成功提示" : "保存失败提示", result.Message);
                        }));
                    }
                });
        }

        /// <summary>
        /// 刷新功能组菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnRefreshNavBarGroup_OnClick(object sender, RoutedEventArgs e)
        {
            _leftMenu.NavBarControlMain.Groups.Clear();
            InitLeftMenu();
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="sneder"></param>
        /// <param name="e"></param>
        private void TbEdit_OnAddClick(object sneder, RoutedEventArgs e)
        {
            if (!_clsLoginModel.PermissionsFunc(_menuId, (int) PermissionsEnum.添加))
                _metroWindow.ShowMessageAsync("友情提示", "您没有权限添加功能分组信息");
            else
            {
                ClearData();
                ShowDockPanelNew();
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sneder"></param>
        /// <param name="e"></param>
        private void TbEdit_OnDeleteClick(object sneder, RoutedEventArgs e)
        {
            var navBarGroupModel = _leftMenu.NavBarControlMain.ActiveGroup?.DataContext as NavBarGroups;
            if (!_clsLoginModel.PermissionsFunc(_menuId, (int) PermissionsEnum.删除))
                _metroWindow.ShowMessageAsync("友情提示", "您没有权限删除功能分组信息");
            else if (navBarGroupModel == null)
                _metroWindow.ShowMessageAsync("删除失败提示", "请选择需要删除的功能组……");
            else
            {
                var setting = new MetroDialogSettings
                {
                    AnimateShow = true,
                    AnimateHide = true,
                    AffirmativeButtonText = "是",
                    NegativeButtonText = "否",
                    DefaultButtonFocus = MessageDialogResult.Negative
                };
                _metroWindow.ShowMessageAsync("安全提示", "您是否继续要删除该功能组信息吗？", MessageDialogStyle.AffirmativeAndNegative, setting).ContinueWith(
                    t =>
                    {
                        if (t.Result == MessageDialogResult.Affirmative)
                        {
                            _metroWindow.Dispatcher.Invoke((Action)(() =>
                            {
                                var result = _navBarGroupsManage.Delete(navBarGroupModel.Id);
                                if (result.ResultStatus == ResultStatus.Success)
                                {
                                    InitLeftMenu();
                                    ShowDockPanelModify();
                                }

                                _metroWindow.ShowMessageAsync(
                                    result.ResultStatus == ResultStatus.Success ? "删除成功提示" : "删除失败提示", result.Message);
                            }));
                        }
                    });
            }
        }

        /// <summary>
        /// 清除数据
        /// </summary>
        private void ClearData()
        {
            NavBarGroupId = 0;
            NavBarGroupName = "";
            IcoPath = "";
            Sort = "0";
        }

        /// <summary>
        /// 显示新增按钮区域
        /// </summary>
        private void ShowDockPanelNew()
        {
            VisibilityDockPanelModify = Visibility.Collapsed;
            VisibilityDockPanelNew = Visibility.Visible;
        }

        /// <summary>
        /// 选择功能菜单后初始化数据
        /// </summary>
        /// <param name="navBarGroupModel"></param>
        private void InitActiveGroupRightData(NavBarGroups navBarGroupModel)
        {
            NavBarGroupId = navBarGroupModel.Id;
            NavBarGroupName = navBarGroupModel.Name;
            IcoPath = navBarGroupModel.Ico;
            Sort = navBarGroupModel.Sort.ToString();
        }

        /// <summary>
        /// 显示修改按钮区域
        /// </summary>
        private void ShowDockPanelModify()
        {
            VisibilityDockPanelModify = Visibility.Visible;
            VisibilityDockPanelNew = Visibility.Collapsed;
        }
        #region MVVM Models

        private int _navBarGroupId;
        private string _navBarGroupName;
        private string _icoPath;
        private ImageSource _icoSource;
        private string _sort;

        private Visibility _visibilityDockPanelNew;
        private Visibility _visibilityDockPanelModify;

        public int NavBarGroupId
        {
            get { return _navBarGroupId; }
            set
            {
                _navBarGroupId = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NavBarGroupId)));
            }
        }

        public string NavBarGroupName
        {
            get { return _navBarGroupName; }
            set
            {
                _navBarGroupName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NavBarGroupName)));
            }
        }

        public string IcoPath
        {
            get { return _icoPath; }
            set
            {
                _icoPath = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IcoPath)));
                
                if (value == null || value.Trim().Length == 0)
                    IcoSource = null;
                else
                {
                    var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, value);
                    IcoSource = !System.IO.File.Exists(path) ? null : new BitmapImage(new Uri(path));
                }
            }
        }

        public ImageSource IcoSource
        {
            get { return _icoSource; }
            set
            {
                _icoSource = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IcoSource)));
            }
        }

        public string Sort
        {
            get { return _sort; }
            set
            {
                _sort = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Sort)));
            }
        }

        /// <summary>
        /// 显示\隐藏新增按钮区域
        /// </summary>
        public Visibility VisibilityDockPanelNew
        {
            get { return _visibilityDockPanelNew; }
            set
            {
                _visibilityDockPanelNew = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(VisibilityDockPanelNew)));
            }
        }

        /// <summary>
        /// 显示\隐藏修改按钮区域
        /// </summary>
        public Visibility VisibilityDockPanelModify
        {
            get { return _visibilityDockPanelModify; }
            set
            {
                _visibilityDockPanelModify = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(VisibilityDockPanelModify)));
            }
        }

        #region 必输验证

        private readonly Dictionary<string, bool> _verifyDictionary = new Dictionary<string, bool>();

        public bool Verify()
        {
            return _verifyDictionary.All(a => !a.Value);
        }

        public string Error => null;

        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case nameof(NavBarGroupName):
                        return VerifyColumnName(columnName, NavBarGroupName, "请输入功能组名称");
                    case nameof(Sort):
                        return VerifyColumnName(columnName, Sort, "请输入排序");
                }
                return null;
            }
        }

        private string VerifyColumnName(string columnName, string value, string errMsg)
        {
            var error = !(value != null && value.Trim().Length != 0);

            _verifyDictionary[columnName] = error;
            return error ? errMsg : null;
        }
        #endregion
        #endregion
    }
}
