using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using DevExpress.Mvvm.Native;
using DevExpress.Xpf.NavBar;
using Frame.Business;
using Frame.Business.interfaces;
using Frame.Models;
using Frame.Models.interfaces;
using Frame.Models.SysModels;
using Frame.Models.SysModels.LeftMenus;
using Frame.Proxy;
using Frame.Proxy.Controls;
using Frame.Proxy.Enums;
using Frame.SysWindows.MVModels;
using Frame.Utils;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Frame.SysWindows.Windows.Common;

namespace Frame.SysWindows.Controls
{
    /// <summary>
    /// LeftMenuManager.xaml 的交互逻辑
    /// </summary>
    public partial class LeftMenuManager : INotifyPropertyChanged, IDataErrorInfo
    {
        private readonly ILeftMenusManage _leftMenusManage = new LeftMenusManage();
        private readonly INavBarGroupsManage _navBarGroupsManage = new NavBarGroupsManage();

        public event PropertyChangedEventHandler PropertyChanged;

        private readonly MetroWindow _metroWindow;
        private readonly ClsLoginModel _clsLoginModel;
        private readonly string _currentMenuId;
        private LeftMenu _leftMenu;

        public LeftMenuManager(MetroWindow metroWindow, ClsLoginModel clsLoginModel, string menuId)
        {
            InitializeComponent();
            _metroWindow = metroWindow;
            _clsLoginModel = clsLoginModel;
            _currentMenuId = menuId;

            DataContext = this;
            InitLeftMenu();
            InitViewModel();
        }

        /// <summary>
        /// 初始化 LeftMenu
        /// </summary>
        private void InitLeftMenu()
        {
            var allLeftMenus = _leftMenusManage.GetAll();
            _leftMenu = new LeftMenu(allLeftMenus);
            // 给每个组的根目录添加“根节点”选择
            _leftMenu.NavBarControlMain.Groups.ForEach(a =>
            {
                var leftMenusModel = new LeftMenus
                {
                    DisplayName = Config.RootDisplayName,
                    NavBarGroupId = ((NavBarGroups)a.DataContext).Id
                };
                var treeViewItem = new TreeViewImgItem
                {
                    DataContext = leftMenusModel,
                    HeaderText = leftMenusModel.DisplayName,
                };
                ((CTreeView)a.Items[0]).Items.Insert(0, treeViewItem);
                treeViewItem.Selected += LeftMenu_RootItemSelected;
            });

            _leftMenu.ParentItemSelected += LeftMenu_ParentItemSelected;
            _leftMenu.ChildItemSelected += LeftMenu_ChildItemSelected;
            GridLeftMenu.Children.Add(_leftMenu);
        }

        private void InitLeftMenuData(int leftMenuId)
        {
            var model = _leftMenusManage.GetLeftMenuById(leftMenuId);

            LeftMenuId = model.Id;
            DisplayName = model.DisplayName;
            MenuId = model.MenuId;
            NavBarGroupId = model.NavBarGroupId;
            DllPath = model.DllPath;
            EntryFunction = model.EntryFunction;
            ParentId = model.ParentId;
            StartWithSysYes = model.StartWithSys;
            Timestamp = model.Timestamp;
            ParentDisplayName = GetSelectedPath(_leftMenu.LeftMenusList, model.NavBarGroupId, ParentId, "");
        }

        private void InitViewModel()
        {
            DisableModifyButton();
            ShowDockPanelNew();
            NavBarGroupId = GetSelectedNavBarGroups().Id;
            ParentDisplayName = $"{GetSelectedNavBarGroups()?.Name}\\{Config.RootDisplayName}";
        }

        /// <summary>
        /// 根目录被选中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LeftMenu_RootItemSelected(object sender, RoutedEventArgs e)
        {
            var item = (TreeViewItem)sender;
            var leftMenuItem = (LeftMenus)item.DataContext;
            DisplayName = leftMenuItem.DisplayName;
            DllPath = "";
            EntryFunction = "";
            MenuId = "";
            NavBarGroupId = leftMenuItem.NavBarGroupId;
            ParentId = leftMenuItem.ParentId;
            ParentDisplayName = GetSelectedRootPath(leftMenuItem.NavBarGroupId);

            DisableModifyButton();
            ShowDockPanelModify();

            e.Handled = true;
        }

        /// <summary>
        /// 父节点选中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LeftMenu_ParentItemSelected(TreeViewItem sender, RoutedEventArgs e)
        {
            LeftMenuId = ((LeftMenus)sender.DataContext).Id;
            InitLeftMenuData(LeftMenuId);

            EnableModifyButton();
            ShowDockPanelModify();

            e.Handled = true;
        }

        /// <summary>
        /// LeftMenu 子项被选中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LeftMenu_ChildItemSelected(TreeViewItem sender, RoutedEventArgs e)
        {
            LeftMenuId = ((LeftMenus)sender.DataContext).Id;
            InitLeftMenuData(LeftMenuId);

            EnableModifyButton();
            ShowDockPanelModify();

            e.Handled = true;
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSave_OnClick(object sender, RoutedEventArgs e)
        {
            if (!_clsLoginModel.PermissionsFunc(_currentMenuId, (int) PermissionsEnum.修改))
            {
                _metroWindow.ShowMessageAsync("友情提示", "您没有修改菜单信息的权限");
                return;
            }
            if (!Verify()) return;
            if (!JudgeDisplayNameNotRootName(_metroWindow, DisplayName)) return;

            Action updateAction = () =>
            {
                var requestModel = new LeftMenusUpdateRequestModel
                {
                    DisplayName = DisplayName,
                    DllPath = DllPath,
                    EntryFunction = EntryFunction,
                    Ico = null,
                    MenuId = MenuId,
                    NavBarGroupId = NavBarGroupId,
                    ParentId = ParentId,
                    StartWithSys = StartWithSysYes,
                    Timestamp = _leftMenusManage.ServerTime.ToUnixTimestamp()
                };
                var result = _leftMenusManage.Update(LeftMenuId, Timestamp, requestModel);
                if (result.ResultStatus == ResultStatus.Success)
                {
                    var selectedTreeView = GetSelectedTreeView();
                    Timestamp = result.Data.Timestamp;
                    ((LeftMenus)((TreeViewItem)selectedTreeView.SelectedItem).DataContext).Timestamp = result.Data.Timestamp;

                    var currentTreeView = GetSelectedTreeView();
                    UpdateSelectedItemPosition(LeftMenuId);
                    UpdateSort(currentTreeView.Items);
                    UpdateSelectedItem(currentTreeView.Items, LeftMenuId);

                    _leftMenu.LeftMenusList = _leftMenusManage.GetAll();
                }
                _metroWindow.ShowMessageAsync(result.ResultStatus == ResultStatus.Success ? "更新成功" : "更新失败", result.Message);
            };

            var setting = new MetroDialogSettings
            {
                AnimateShow = true,
                AnimateHide = true,
                AffirmativeButtonText = "确定",
                NegativeButtonText = "取消",
                DefaultButtonFocus = MessageDialogResult.Negative
            };
            _metroWindow.ShowMessageAsync("更新节点提示", "您确定要更新此节点信息吗？", MessageDialogStyle.AffirmativeAndNegative, setting).ContinueWith(
                t =>
                {
                    _metroWindow.Dispatcher.Invoke((Action)(() =>
                    {
                        if (t.Result == MessageDialogResult.Affirmative)
                        {
                            if (_leftMenusManage.Exist(a => a.Id != LeftMenuId && a.DisplayName == DisplayName.Trim()))
                                _metroWindow.ShowMessageAsync("信息重复提示", $"菜单名称“{DisplayName}”,在系统中已存在，是否仍使用该名称？", MessageDialogStyle.AffirmativeAndNegative, setting)
                                    .ContinueWith(z =>
                                    {
                                        if (z.Result == MessageDialogResult.Affirmative)
                                            _metroWindow.Dispatcher.Invoke((Action)(() => {
                                                updateAction();
                                            }));
                                    });
                            else
                                updateAction();
                        }
                    }));
                });
        }

        /// <summary>
        /// 重置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnReset_OnClick(object sender, RoutedEventArgs e)
        {
            InitLeftMenuData(LeftMenuId);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAdd_OnClick(object sender, RoutedEventArgs e)
        {
            if (!_clsLoginModel.PermissionsFunc(_currentMenuId, (int) PermissionsEnum.添加))
            {
                _metroWindow.ShowMessageAsync("友情提示", "您没有添加菜单信息的权限");
                return;
            }
            if (!Verify()) return;
            if (!JudgeDisplayNameNotRootName(_metroWindow, DisplayName)) return;

            Action addAction = () =>
            {
                var requestModel = new LeftMenusAddRequestModel
                {
                    DisplayName = DisplayName,
                    DllPath = DllPath,
                    EntryFunction = EntryFunction,
                    MenuId = MenuId,
                    NavBarGroupId = NavBarGroupId,
                    ParentId = ParentId,
                    Sort = int.MaxValue,
                    StartWithSys = StartWithSysYes,
                    Timestamp = _leftMenusManage.ServerTime.ToUnixTimestamp()
                };
                var result = _leftMenusManage.Add(requestModel);
                if (result.ResultStatus == ResultStatus.Success)
                {
                    ClearMenuData();
                    AddTreeViewItem(result.Data);
                    _leftMenu.LeftMenusList = _leftMenusManage.GetAll().ToList();
                }
                _metroWindow.ShowMessageAsync(result.ResultStatus == ResultStatus.Success ? "添加成功提示" : "添加失败提示", result.Message);
            };


            var setting = new MetroDialogSettings
            {
                AnimateShow = true,
                AnimateHide = true,
                AffirmativeButtonText = "是",
                NegativeButtonText = "否",
                DefaultButtonFocus = MessageDialogResult.Negative
            };
            if (_leftMenusManage.Exist(a => a.DisplayName == DisplayName.Trim()))
            {
                _metroWindow.ShowMessageAsync("信息重复提示", $"菜单名称“{DisplayName}”,在系统中已存在，是否仍使用该名称？", MessageDialogStyle.AffirmativeAndNegative, setting).ContinueWith(
                    t =>
                    {
                        _metroWindow.Dispatcher.Invoke((Action)(() =>
                        {
                            if (t.Result == MessageDialogResult.Affirmative)
                                addAction();
                        }));
                    });
            }
            else
                addAction();
        }

        /// <summary>
        /// 清空
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnClear_OnClick(object sender, RoutedEventArgs e)
        {
            ClearMenuData();
        }

        /// <summary>
        /// 位置上移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnUp_OnClick(object sender, RoutedEventArgs e)
        {
            var treeView = GetSelectedTreeView();
            TreeViewItemUp(treeView);
        }

        /// <summary>
        /// 位置下移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDown_OnClick(object sender, RoutedEventArgs e)
        {
            var treeView = GetSelectedTreeView();
            TreeViewItemDown(treeView);
        }

        /// <summary>
        /// 显示 LeftMenu 参照界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnShowLeftMenuWindow_OnClick(object sender, RoutedEventArgs e)
        {
            Action<LeftMenus> callback = model =>
            {
                NavBarGroupId = model.NavBarGroupId;
                ParentId = model.Id;
                ParentDisplayName = model.DisplayName == Config.RootDisplayName 
                    ? GetSelectedRootPath(model.NavBarGroupId) 
                    : GetSelectedPath(_leftMenu.LeftMenusList, model.NavBarGroupId, ParentId, "");
            };
            var leftMenuRefer = new LeftMenuRefer(ParentId, LeftMenuId, callback) { Owner = _metroWindow };
            leftMenuRefer.ShowDialog();
        }

        /// <summary>
        /// 选择 Dll 路径
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSelectDll_OnClick(object sender, RoutedEventArgs e)
        {
            var dllPathRefer = new DllPathRefer(dllPathSelTreeView =>
            {
                DllPath = dllPathSelTreeView.FullPath.Remove(0, Config.DevPlatformPath.Length);
                var types = Assembly.LoadFile(dllPathSelTreeView.FullPath).GetTypes();

                var id = 1;
                var list = types.Select(a =>
                {
                    var dllEntry = new DgDllEntryClass { Id = id++, FullName = a.FullName };
                    dllEntry.CheckChanged += DllEntry_CheckChanged;
                    return dllEntry;
                }).ToList();

                DataGridMain.ItemsSource = list;
            })
            {
                Owner = _metroWindow
            };
            dllPathRefer.ShowDialog();
        }

        /// <summary>
        /// 选择项发生变化后
        /// </summary>
        /// <param name="sender"></param>
        private void DllEntry_CheckChanged(DgDllEntryClass sender)
        {
            var dgList = (List<DgDllEntryClass>)DataGridMain.ItemsSource;
            if (sender.IsChecked)
            {
                dgList.Where(a => a.Id != sender.Id).ForEach(a => a.IsChecked = false);
                EntryFunction = dgList.Single(a => a.Id == sender.Id).FullName;
            }
            else
            {
                EntryFunction = "";
            }
        }

        /// <summary>
        /// 更新排序
        /// </summary>
        /// <param name="items"></param>
        private void UpdateSort(ItemCollection items)
        {
            foreach (var i in items)
            {
                var item = (TreeViewItem)i;
                var model = (LeftMenus)item.DataContext;
                if (model.DisplayName == Config.RootDisplayName) continue;

                var requestModel = new LeftMenusUpdateSortRequestModel
                {
                    DisplayName = model.DisplayName,
                    Sort = model.Sort,
                    Timestamp = _leftMenusManage.ServerTime.ToUnixTimestamp()
                };
                var result = _leftMenusManage.UpdateSort(model.Id, model.Timestamp, requestModel);
                if (result.ResultStatus == ResultStatus.Success)
                    model.Timestamp = result.Data.Timestamp;
                else
                    _metroWindow.ShowMessageAsync("更新节点排序失败", result.Message);

                if(item.HasItems)
                    UpdateSort(item.Items);
            }
        }

        /// <summary>
        /// 更新节点的信息
        /// </summary>
        private void UpdateSelectedItem(ItemCollection items, int leftMenuId)
        {
            foreach (var i in items)
            {
                var item = (TreeViewImgItem)i;
                var model = (LeftMenus)item.DataContext;
                if (model.Id == leftMenuId)
                {
                    if (_leftMenusManage.Exist(a => a.Id == leftMenuId))
                    {
                        var entity = _leftMenusManage.GetLeftMenuById(leftMenuId);
                        item.HeaderText = entity.DisplayName;
                        item.DataContext = entity;
                        break;
                    }
                }
                if (item.HasItems)
                    UpdateSelectedItem(item.Items, leftMenuId);
            }
        }

        /// <summary>
        /// 更新节点的位置
        /// </summary>
        private void UpdateSelectedItemPosition(int leftMenuId)
        {
            var model = _leftMenusManage.GetLeftMenuById(leftMenuId);
            var nav = _leftMenu.NavBarControlMain.Groups.SingleOrDefault(a => ((NavBarGroups)a.DataContext).Id == model.NavBarGroupId);
            if (nav != null)
            {
                var tree = GetSelectedTreeView();
                var selectedItem = (TreeViewImgItem)tree.SelectedItem;

                if (Equals(nav, (NavBarGroup) _leftMenu.NavBarControlMain.SelectedGroup))
                {
                    var parentView = selectedItem.Parent as CTreeView;
                    var parentItem = selectedItem.Parent as TreeViewImgItem;
                    if (parentView != null && model.ParentId != 0)
                    {
                        parentView.Items.Remove(selectedItem);
                        AddTreeViewItem(tree, tree.Items, selectedItem, model);
                    }

                    else if (parentItem != null && ((LeftMenus)parentItem.DataContext).Id != model.ParentId)
                    {
                        parentItem.Items.Remove(selectedItem);
                        AddTreeViewItem(tree, tree.Items, selectedItem, model);
                    }
                }
                else
                {
                    var parentTreeView = selectedItem.Parent as CTreeView;
                    var parentTreeViewItem = selectedItem.Parent as TreeViewItem;
                    var first = (CTreeView)nav.Items.First();
                    if (parentTreeView != null)
                    {
                        parentTreeView.Items.Remove(selectedItem);
                        AddTreeViewItem(first, first.Items, selectedItem, model);
                    }

                    else if (parentTreeViewItem != null)
                    {
                        parentTreeViewItem.Items.Remove(selectedItem);
                        AddTreeViewItem(first, first.Items, selectedItem, model);
                    }
                }
            }
        }


        private void AddTreeViewItem(CTreeView treeView, ItemCollection items, TreeViewImgItem selectedItem, LeftMenus leftMenus)
        {
            if (leftMenus.ParentId == 0)
            {
                treeView.Items.Add(GetSelectedTreeView().GenerateTreeViewItem(selectedItem));
                selectedItem.IsSelected = true;
                return;
            }

            foreach (var i in items)
            {
                var item = (TreeViewImgItem)i;
                if (((LeftMenus)item.DataContext).Id == leftMenus.ParentId)
                {
                    var treeViewMain = GetSelectedTreeView();
                    item.Items.Add(treeViewMain.GenerateTreeViewItem(selectedItem));
                    selectedItem.IsSelected = true;
                    item.IsExpanded = true;

                    var requestModel = new LeftMenusUpdateSortRequestModel
                    {
                        DisplayName = leftMenus.DisplayName,
                        Sort = int.MaxValue,
                        Timestamp = _leftMenusManage.ServerTime.ToUnixTimestamp()
                    };
                    var result = _leftMenusManage.UpdateSort(leftMenus.Id, Timestamp, requestModel);
                    if (result.ResultStatus == ResultStatus.Success)
                    {
                        leftMenus.DisplayName = result.Data.DisplayName;
                        leftMenus.Sort = result.Data.Sort;
                        leftMenus.Timestamp = result.Data.Timestamp;
                    }
                    else
                        _metroWindow.ShowMessageAsync("更新节点排序失败", result.Message);
                    break;
                }

                if (item.HasItems)
                    AddTreeViewItem(treeView, item.Items, selectedItem, leftMenus);
            }
        }


        /// <summary>
        /// 获取当前显示的 TreeView
        /// </summary>
        /// <returns></returns>
        private CTreeView GetSelectedTreeView()
        {
            var treeView = GetSelectedGroup()?.Items?.First() as CTreeView;

            return treeView;
        }

        private NavBarGroups GetSelectedNavBarGroups()
        {
            return GetSelectedGroup()?.DataContext as NavBarGroups;
        }

        private NavBarGroup GetSelectedGroup()
        {
            return (NavBarGroup)_leftMenu.NavBarControlMain.SelectedGroup;
        }

        /// <summary>
        /// 显示新增区域按钮
        /// </summary>
        private void ShowDockPanelNew()
        {
            VisibilityDockPanelNew = Visibility.Visible;
            VisibilityDockPanelModify = Visibility.Collapsed;
        }

        /// <summary>
        /// 显示修改区域按钮
        /// </summary>
        private void ShowDockPanelModify()
        {
            VisibilityDockPanelNew = Visibility.Collapsed;
            VisibilityDockPanelModify = Visibility.Visible;
        }

        /// <summary>
        /// 启用修改区域的 Button
        /// </summary>
        private void EnableModifyButton()
        {
            EnableBtnSave = true;
            EnableBtnReset = true;
        }

        /// <summary>
        /// 不启用修改区域的 Button
        /// </summary>
        private void DisableModifyButton()
        {
            EnableBtnSave = false;
            EnableBtnReset = false;
        }

        /// <summary>
        /// 清空数据
        /// </summary>
        private void ClearMenuData()
        {
            var selectedItem = GetSelectedTreeView().SelectedItem as TreeViewItem;
            if (selectedItem == null)
            {
                ParentId = 0;
                ParentDisplayName = GetSelectedPath(_leftMenu.LeftMenusList, GetSelectedNavBarGroups().Id, ParentId, "");
            }
            else
            {
                var leftMenusModel = (LeftMenus)selectedItem.DataContext;
                ParentId = leftMenusModel.Id;
                ParentDisplayName = GetSelectedPath(_leftMenu.LeftMenusList, leftMenusModel.NavBarGroupId, ParentId, "");
            }

            DisplayName = "";
            DllPath = "";
            MenuId = "";
            EntryFunction = "";

            var nav = (NavBarGroup)_leftMenu.NavBarControlMain.SelectedGroup;
            var navBarGroup = (NavBarGroups)nav.DataContext;
            NavBarGroupId = navBarGroup.Id;
        }

        /// <summary>
        /// 获取当前选择的字符串路径
        /// </summary>
        /// <param name="leftMenus"></param>
        /// <param name="navBarGroupId"></param>
        /// <param name="currentLeftMenuId"></param>
        /// <param name="selectedPath"></param>
        /// <returns></returns>
        private string GetSelectedPath(List<LeftMenus> leftMenus, int navBarGroupId, int currentLeftMenuId, string selectedPath)
        {
            var navBarGroupName = _navBarGroupsManage.GetNavBarGroupById(navBarGroupId)?.Name;
            var path = LeftMenuRefer.GetSelectedPath(leftMenus, currentLeftMenuId, selectedPath);
            return $"{navBarGroupName}\\{path}";
        }

        /// <summary>
        /// 获取当前 root TreeViewItem 的字符串路径
        /// </summary>
        /// <param name="navBarGroupId"></param>
        /// <returns></returns>
        private string GetSelectedRootPath(int navBarGroupId)
        {
            var navBarGroupName = _navBarGroupsManage.GetNavBarGroupById(navBarGroupId)?.Name;
            return $"{navBarGroupName}\\";
        }

        /// <summary>
        /// 节点添加成功后，然后将其添加到界面上
        /// </summary>
        /// <param name="leftMenusModel"></param>
        private void AddTreeViewItem(LeftMenus leftMenusModel)
        {
            var selectedTreeView = GetSelectedTreeView();

            var newItem = selectedTreeView?.GenerateTreeViewItem(leftMenusModel);
            var selectedItem = selectedTreeView?.SelectedItem as TreeViewImgItem;
            if (selectedItem == null || ((LeftMenus) selectedItem.DataContext)?.DisplayName == Config.RootDisplayName)
                selectedTreeView?.Items.Add(newItem);
            else
            {
                selectedItem.Items.Add(newItem);
                selectedTreeView.InitSelectedAndIco(selectedItem, selectedItem.Items.Count);
            }
        }

        #region TooBar 操作事件
        /// <summary>
        /// 刷新左侧树形菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnRefreshLeftMenu_OnClick(object sender, RoutedEventArgs e)
        {
            GridLeftMenu.Children.Clear();
            InitLeftMenu();
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TbEdit_OnAddClick(object sender, RoutedEventArgs e)
        {
            if (!_clsLoginModel.PermissionsFunc(_currentMenuId, (int) PermissionsEnum.添加))
                _metroWindow.ShowMessageAsync("友情提示", "您没有添加菜单信息的权限");
            else
            {
                ShowDockPanelNew();
                DisableModifyButton();

                ClearMenuData();
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TbEdit_OnDeleteClick(object sender, RoutedEventArgs e)
        {
            var treeView = GetSelectedTreeView();
            var item = treeView.SelectedItem as TreeViewImgItem;
            if(!_clsLoginModel.PermissionsFunc(_currentMenuId, (int)PermissionsEnum.删除))
                _metroWindow.ShowMessageAsync("友情提示", "您没有删除菜单信息的权限");
            else if (item == null)
                _metroWindow.ShowMessageAsync("删除失败提示", "请先选择需要删除的节点……");
            else
            {
                var setting = new MetroDialogSettings
                {
                    AffirmativeButtonText = "确定",
                    NegativeButtonText = "取消",
                    DefaultButtonFocus = MessageDialogResult.Negative,
                    AnimateShow = true,
                    AnimateHide = true
                };

                _metroWindow.ShowMessageAsync("安全提示", $"您确定要删除名称为“{item.HeaderText}”的节点吗？", MessageDialogStyle.AffirmativeAndNegative, setting).ContinueWith(
                    t =>
                    {
                        _metroWindow.Dispatcher.Invoke((Action)(() =>
                        {
                            if (t.Result == MessageDialogResult.Affirmative)
                            {
                                if (_leftMenusManage.Exist(a => a.Id == ((LeftMenus)item.DataContext).Id))
                                {
                                    var result = _leftMenusManage.Deletes(a => a.Id == ((LeftMenus)item.DataContext).Id);
                                    if (result.ResultStatus == ResultStatus.Success)
                                    {
                                        // 第一种情况
                                        (item.Parent as CTreeView)?.Items.Remove(item);
                                        // 第二种情况
                                        if ((item.Parent as TreeViewImgItem) != null)
                                        {
                                            var itemParent = (TreeViewImgItem)item.Parent;
                                            itemParent.Items.Remove(item);
                                            treeView.InitSelectedAndIco(itemParent, itemParent.Items.Count);
                                        }

                                        _leftMenu.LeftMenusList = _leftMenusManage.GetAll().ToList();
                                        _metroWindow.ShowMessageAsync("删除成功提示", $"名称为“{item.HeaderText}”的节点删除成功！");
                                    }
                                    else
                                        _metroWindow.ShowMessageAsync("删除失败提示", result.Message);
                                }
                                else
                                    _metroWindow.ShowMessageAsync("删除失败提示", $"该“{item.HeaderText}”节点在系统中已不存在！");
                            }
                        }));
                    });
            }
        }

        #endregion

        #region Public Static Method
        /// <summary>
        /// TreeView 的节点向上移动
        /// </summary>
        /// <param name="treeView"></param>
        public static void TreeViewItemUp(TreeView treeView)
        {
            var selectedItem = (TreeViewItem)treeView.SelectedItem;
            if (selectedItem == null) return;

            // 第一种情况
            var parentTreeView = selectedItem.Parent as TreeView;
            if (parentTreeView != null)
            {
                var index = parentTreeView.Items.IndexOf(selectedItem);
                if (index > 1)
                {
                    parentTreeView.Items.Remove(selectedItem);
                    parentTreeView.Items.Insert(index - 1, selectedItem);

                    for (var i = 0; i < parentTreeView.Items.Count; i++)
                        ((IMenus)((TreeViewItem)parentTreeView.Items[i]).DataContext).Sort = i;
                }
            }

            // 第二种情况
            var parentTreeViewItem = selectedItem.Parent as TreeViewItem;
            if (parentTreeViewItem != null)
            {
                var index = parentTreeViewItem.Items.IndexOf(selectedItem);
                if (index > 0)
                {
                    parentTreeViewItem.Items.Remove(selectedItem);
                    parentTreeViewItem.Items.Insert(index - 1, selectedItem);

                    for (var i = 0; i < parentTreeViewItem.Items.Count; i++)
                        ((IMenus)((TreeViewItem)parentTreeViewItem.Items[i]).DataContext).Sort = i;
                }
            }
            selectedItem.IsSelected = true;
        }

        /// <summary>
        /// TreeView 的节点向下移动
        /// </summary>
        /// <param name="treeView"></param>
        public static void TreeViewItemDown(TreeView treeView)
        {
            var selectedItem = (TreeViewItem)treeView.SelectedItem;
            if(selectedItem == null) return;

            // 第一种情况
            var parentTreeView = selectedItem.Parent as TreeView;
            if (parentTreeView != null)
            {
                if (((IMenus)selectedItem.DataContext).DisplayName == Config.RootDisplayName) return;
                var index = parentTreeView.Items.IndexOf(selectedItem);
                if (index < parentTreeView.Items.Count - 1)
                {
                    parentTreeView.Items.Remove(selectedItem);
                    parentTreeView.Items.Insert(index + 1, selectedItem);

                    for (var i = 0; i < parentTreeView.Items.Count; i++)
                        ((IMenus)((TreeViewItem)parentTreeView.Items[i]).DataContext).Sort = i;
                }
            }

            // 第二种情况
            var parentTreeViewItem = selectedItem.Parent as TreeViewItem;
            if (parentTreeViewItem != null)
            {
                var index = parentTreeViewItem.Items.IndexOf(selectedItem);
                if (index < parentTreeViewItem.Items.Count - 1)
                {
                    parentTreeViewItem.Items.Remove(selectedItem);
                    parentTreeViewItem.Items.Insert(index + 1, selectedItem);

                    for (var i = 0; i < parentTreeViewItem.Items.Count; i++)
                        ((IMenus)((TreeViewItem)parentTreeViewItem.Items[i]).DataContext).Sort = i;
                }
            }
            selectedItem.IsSelected = true;
        }

        /// <summary>
        /// 判断节点名称是否为系统保留字段
        /// </summary>
        /// <param name="metroWindow"></param>
        /// <param name="displayName"></param>
        /// <returns></returns>
        public static bool JudgeDisplayNameNotRootName(MetroWindow metroWindow, string displayName)
        {
            if (displayName != null && displayName.Trim() == Config.RootDisplayName)
            {
                metroWindow.ShowMessageAsync("友情提示", $"该名称“{displayName}”为系统保留字段，请重新输入");
                return false;
            }
            return true;
        }
        #endregion

        #region MVVM Models
        private int _leftMenuId;
        private string _displayName;
        private string _menuId;
        private int _navBarGroupId;
        private string _dllPath;
        private string _entryFunction;
        private int _parentId;
        private string _parentDisplayName;
        private bool _startWithSysYes;
        private bool _startWithSysNo = true;
        private long _timestamp;

        private bool _enableBtnReset;
        private bool _enableBtnSave;
        private Visibility _visibilityDockPanelNew;
        private Visibility _visibilityDockPanelModify;
        public int LeftMenuId
        {
            get { return _leftMenuId; }
            set
            {
                _leftMenuId = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(LeftMenuId)));
            }
        }
        public string DisplayName
        {
            get { return _displayName; }
            set
            {
                _displayName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DisplayName)));
            }
        }

        public string MenuId
        {
            get { return _menuId; }
            set
            {
                _menuId = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(MenuId)));
            }
        }

        public int NavBarGroupId
        {
            get { return _navBarGroupId; }
            set
            {
                _navBarGroupId = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NavBarGroupId)));
            }
        }

        public string DllPath
        {
            get { return _dllPath; }
            set
            {
                _dllPath = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DllPath)));
            }
        }

        public string EntryFunction
        {
            get { return _entryFunction; }
            set
            {
                _entryFunction = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(EntryFunction)));
            }
        }

        public int ParentId
        {
            get { return _parentId; }
            set
            {
                _parentId = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ParentId)));
            }
        }

        public string ParentDisplayName
        {
            get { return _parentDisplayName; }
            set
            {
                _parentDisplayName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ParentDisplayName)));
            }
        }

        public bool StartWithSysYes
        {
            get { return _startWithSysYes; }
            set
            {
                if(_startWithSysYes == value) return;
                _startWithSysYes = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(StartWithSysYes)));

                StartWithSysNo = !value;
            }
        }

        public bool StartWithSysNo
        {
            get { return _startWithSysNo; }
            set
            {
                if(_startWithSysNo == value) return;
                _startWithSysNo = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(StartWithSysNo)));
            }
        }

        public long Timestamp
        {
            get { return _timestamp; }
            set
            {
                _timestamp = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Timestamp)));
            }
        }

        /// <summary>
        /// 是否启用 BtnReset 重置按钮
        /// </summary>
        public bool EnableBtnReset
        {
            get { return _enableBtnReset; }
            set
            {
                _enableBtnReset = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(EnableBtnReset)));
            }
        }

        /// <summary>
        /// 是否启用 BtnSave 重置按钮
        /// </summary>
        public bool EnableBtnSave
        {
            get { return _enableBtnSave; }
            set
            {
                _enableBtnSave = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(EnableBtnSave)));
            }
        }

        /// <summary>
        /// 显示、隐藏新增区域按钮
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
        /// 显示、隐藏修改区域按钮
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
                    case nameof(DisplayName):
                        return VerifyColumnName(columnName, DisplayName, "请输入菜单名称");
                    case nameof(ParentDisplayName):
                        return VerifyColumnName(columnName, ParentDisplayName, "请选择父级菜单");
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
