using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using DevExpress.Mvvm.Native;
using Frame.Business;
using Frame.Business.interfaces;
using Frame.Models;
using Frame.Models.SysModels;
using Frame.Models.SysModels.TopMenus;
using Frame.Proxy;
using Frame.Proxy.Enums;
using Frame.SysWindows.MVModels;
using Frame.SysWindows.Windows.Common;
using Frame.Utils;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace Frame.SysWindows.Controls
{
    public partial class TopMenuManager : INotifyPropertyChanged, IDataErrorInfo
    {
        private readonly ITopMenusManage _topMenusManage = new TopMenusManage();

        public event PropertyChangedEventHandler PropertyChanged;

        private readonly MetroWindow _metroWindow;
        private readonly ClsLoginModel _clsLoginModel;
        private readonly string _currentMenuId;
        private List<TopMenus> _topMenusList;
        public TopMenuManager(MetroWindow metroWindow, ClsLoginModel clsLoginModel, string menuId)
        {
            InitializeComponent();
            _metroWindow = metroWindow;
            _clsLoginModel = clsLoginModel;
            _currentMenuId = menuId;

            DataContext = this;
            InitTopMenuForLeftAndRefer();
            InitViewModel();
        }

        private void InitTopMenuForLeftAndRefer()
        {
            _topMenusList = _topMenusManage.GetAll();

            TreeViewMain.Items.Clear();
            TreeViewMain.Items.Add(GetRootTreeViewItem(TvLeftTopMenusRootItemt_Selected));

            TvTopMenuRefer.Items.Clear();
            TvTopMenuRefer.Items.Add(GetRootTreeViewItem(TvTopMenusReferItemt_Selected));

            _topMenusList.Where(a => a.ParentId == 0).ToList().ForEach(a =>
            {
                // left 左侧树形
                var leftItem = new TreeViewItem { Header = a.DisplayName, DataContext = a};
                ForeachTreeViewItem(_topMenusList, leftItem, a.Id, TvLeftTopMenusItem_Selected);
                TreeViewMain.Items.Add(leftItem);

                // refer 参照之树形
                var topItem = new TreeViewItem { Header = a.DisplayName, DataContext = a };
                ForeachTreeViewItem(_topMenusList, topItem, a.Id, TvTopMenusReferItemt_Selected);
                TvTopMenuRefer.Items.Add(topItem);
            });

        }

        private TreeViewItem GetRootTreeViewItem(RoutedEventHandler selected)
        {
            var rootItem = new TreeViewItem
            {
                Header = Config.RootDisplayName,
                DataContext = new TopMenus
                {
                    DisplayName = Config.RootDisplayName
                }
            };
            rootItem.Selected += selected;
            return rootItem;
        }

        /// <summary>
        /// 初始化右侧显示的数据
        /// </summary>
        /// <param name="topMenuId"></param>
        private void InitRightShowData(int topMenuId)
        {
            var model = _topMenusManage.GetTopMenuById(topMenuId);

            TopMenuId = model.Id;
            DisplayName = model.DisplayName;
            MenuId = model.MenuId;
            DllPath = model.DllPath;
            EntryFunction = model.EntryFunction;
            ParentId = model.ParentId;
            Timestamp = model.Timestamp;
            ParentDisplayName = LeftMenuRefer.GetSelectedPath(_topMenusList, ParentId, "");
        }

        private void InitViewModel()
        {
            DisableModifyButton();
            ShowDockPanelNew();
            ParentDisplayName = Config.RootDisplayName;
        }

        /// <summary>
        /// 子节点递归显示
        /// </summary>
        /// <param name="menuList"></param>
        /// <param name="item"></param>
        /// <param name="parentId"></param>
        /// <param name="selected"></param>
        private void ForeachTreeViewItem(List<TopMenus> menuList, TreeViewItem item, int parentId, RoutedEventHandler selected)
        {
            var list = menuList.Where(a => a.ParentId == parentId).OrderBy(a => a.Sort).ToList();
            item.Selected += selected;

            list.ForEach(a => {
                var newItem = new TreeViewItem { Header = a.DisplayName, DataContext = a };
                item.Items.Add(newItem);
                ForeachTreeViewItem(menuList, newItem, a.Id, selected);
            });
        }

        /// <summary>
        /// 选择 dll 路径
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
        /// 参数选择父级菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnShowLeftMenuWindow_OnClick(object sender, RoutedEventArgs e)
        {
            // 显示之前取消之前的选择
            Functions.CancelTreeViewItemsSelected(TvTopMenuRefer);

            PopupTopMenus.IsOpen = true;
        }

        /// <summary>
        /// TreeView 节点选中触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TvLeftTopMenusItem_Selected(object sender, RoutedEventArgs e)
        {
            TopMenuId = ((TopMenus)((TreeViewItem)sender).DataContext).Id;
            InitRightShowData(TopMenuId);

            EnableModifyButton();
            ShowDockPanelModify();

            e.Handled = true;
        }

        /// <summary>
        /// 左侧 TopMenus 的 TreeViewItem 选中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TvLeftTopMenusRootItemt_Selected(object sender, RoutedEventArgs e)
        {
            ShowDockPanelModify();
            DisableModifyButton();

            DisplayName = Config.RootDisplayName;
            DllPath = "";
            MenuId = "";
            EntryFunction = "";
            ParentDisplayName = "\\";

            e.Handled = true;
        }

        /// <summary>
        /// 父级参照 TreeViewItemSelected 事件触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TvTopMenusReferItemt_Selected(object sender, RoutedEventArgs e)
        {
            var item = (TopMenus)((TreeViewItem)sender).DataContext;

            if (item != null)
            {
                PopupTopMenus.IsOpen = false;
                if (item.DisplayName == Config.RootDisplayName)
                {
                    ParentId = 0;
                    ParentDisplayName = Config.RootDisplayName;
                }
                else
                {
                    var topMenuModel = _topMenusManage.GetTopMenuById(item.Id);
                    ParentId = topMenuModel.Id;
                    ParentDisplayName = LeftMenuRefer.GetSelectedPath(_topMenusList, ParentId, "");
                }
            }

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
            if (!LeftMenuManager.JudgeDisplayNameNotRootName(_metroWindow, DisplayName)) return;

            Action updateAction = () =>
            {
                var requestModel = new TopMenusUpdateRequestModel
                {
                    DisplayName = DisplayName,
                    DllPath = DllPath,
                    EntryFunction = EntryFunction,
                    Ico = null,
                    MenuId = MenuId,
                    ParentId = ParentId,
                    Timestamp = _topMenusManage.ServerTime.ToUnixTimestamp()
                };
                var result = _topMenusManage.Update(TopMenuId, Timestamp, requestModel);
                if (result.ResultStatus == ResultStatus.Success)
                {
                    Timestamp = result.Data.Timestamp;
                    ((TopMenus)((TreeViewItem)(TreeViewMain.SelectedItem)).DataContext).Timestamp = result.Data.Timestamp;

                    UpdateSelectedItemPosition(TopMenuId);
                    UpdateSort(TreeViewMain.Items);
                    UpdateSelectedItem(TreeViewMain.Items, TopMenuId);

                    _topMenusList = _topMenusManage.GetAll();
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
                            if (_topMenusManage.Exist(a => a.Id != TopMenuId && a.DisplayName == DisplayName))
                                _metroWindow.ShowMessageAsync("信息重复提示", $"菜单名称“{DisplayName}”,在系统中已存在，是否仍使用该名称？", MessageDialogStyle.AffirmativeAndNegative, setting)
                                    .ContinueWith(z =>
                                    {
                                        if (z.Result == MessageDialogResult.Affirmative)
                                            _metroWindow.Dispatcher.Invoke((Action) (() =>
                                            {
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
            InitRightShowData(TopMenuId);
        }

        /// <summary>
        /// 清除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnClear_OnClick(object sender, RoutedEventArgs e)
        {
            ClearMenuData();
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
            if (!LeftMenuManager.JudgeDisplayNameNotRootName(_metroWindow, DisplayName)) return;

            Action addAction = () =>
            {
                var requestModel = new TopMenusAddRequestModel
                {
                    DisplayName = DisplayName,
                    DllPath = DllPath,
                    EntryFunction = EntryFunction,
                    MenuId = MenuId,
                    ParentId = ParentId,
                    Sort = int.MaxValue,
                    Timestamp = _topMenusManage.ServerTime.ToUnixTimestamp()
                };
                var result = _topMenusManage.Add(requestModel);
                if (result.ResultStatus == ResultStatus.Success)
                {
                    ClearMenuData();

                    var newItem = GenerateTreeViewItem(result.Data);
                    if (TreeViewMain.SelectedItem == null || ((TreeViewItem)TreeViewMain.SelectedItem).Header.ToString() == Config.RootDisplayName)
                        TreeViewMain.Items.Add(newItem);
                    else
                        ((TreeViewItem)TreeViewMain.SelectedItem).Items.Add(newItem);

                    _topMenusList = _topMenusManage.GetAll();
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
            if (_topMenusManage.Exist(a => a.DisplayName == DisplayName.Trim()))
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
        /// 上移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnUp_OnClick(object sender, RoutedEventArgs e)
        {
            LeftMenuManager.TreeViewItemUp(TreeViewMain);
        }

        /// <summary>
        /// 下移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDown_OnClick(object sender, RoutedEventArgs e)
        {
            LeftMenuManager.TreeViewItemDown(TreeViewMain);
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
                var model = (TopMenus)item.DataContext;
                if(model.DisplayName == Config.RootDisplayName) continue;

                var requestModel = new TopMenusUpdateSortRequestModel
                {
                    DisplayName = model.DisplayName,
                    Sort = model.Sort,
                    Timestamp = _topMenusManage.ServerTime.ToUnixTimestamp()
                };
                var result = _topMenusManage.UpdateSort(model.Id, model.Timestamp, requestModel);
                if (result.ResultStatus == ResultStatus.Success)
                    model.Timestamp = result.Data.Timestamp;
                else
                    _metroWindow.ShowMessageAsync("更新节点排序失败", result.Message);

                if (item.HasItems)
                    UpdateSort(item.Items);
            }
        }

        /// <summary>
        /// 更新节点的信息
        /// </summary>
        private void UpdateSelectedItem(ItemCollection items, int topMenuId)
        {
            foreach (var i in items)
            {
                var item = (TreeViewItem)i;
                var model = (TopMenus)item.DataContext;
                if (model.Id == topMenuId)
                {
                    if (_topMenusManage.Exist(a => a.Id == topMenuId))
                    {
                        var entity = _topMenusManage.GetTopMenuById(topMenuId);
                        item.Header = entity.DisplayName;
                        item.DataContext = entity;
                        break;
                    }
                }
                if (item.HasItems)
                    UpdateSelectedItem(item.Items, topMenuId);
            }
        }

        /// <summary>
        /// 更新节点的位置（通过选择父级的形式修改了 TreeViewItem 所在的 Items）
        /// </summary>
        private void UpdateSelectedItemPosition(int leftMenuId)
        {
            var model = _topMenusManage.GetTopMenuById(leftMenuId);
            var selectedItem = (TreeViewItem)TreeViewMain.SelectedItem;

            var parentView = selectedItem.Parent as TreeView;
            var parentItem = selectedItem.Parent as TreeViewItem;
            if (parentView != null && model.ParentId != 0)
            {
                parentView.Items.Remove(selectedItem);
                AddTreeViewItem(TreeViewMain, TreeViewMain.Items, selectedItem, model);
            }

            else if (parentItem != null && ((TopMenus)parentItem.DataContext).Id != model.ParentId)
            {
                parentItem.Items.Remove(selectedItem);
                AddTreeViewItem(TreeViewMain, TreeViewMain.Items, selectedItem, model);
            }
        }

        private void AddTreeViewItem(TreeView treeView, ItemCollection items, TreeViewItem selectedItem, TopMenus topMenus)
        {
            if (topMenus.ParentId == 0)
            {
                treeView.Items.Add(GenerateTreeViewItem(selectedItem));
                selectedItem.IsSelected = true;
                return;
            }

            foreach (var i in items)
            {
                var item = (TreeViewItem)i;
                if (((TopMenus)item.DataContext).Id == topMenus.ParentId)
                {
                    item.Items.Add(GenerateTreeViewItem(selectedItem));
                    selectedItem.IsSelected = true;
                    item.IsExpanded = true;

                    var requestModel = new TopMenusUpdateSortRequestModel
                    {
                        DisplayName = topMenus.DisplayName,
                        Sort = int.MaxValue,
                        Timestamp = _topMenusManage.ServerTime.ToUnixTimestamp()
                    };
                    var result = _topMenusManage.UpdateSort(topMenus.Id, Timestamp, requestModel);
                    if (result.ResultStatus == ResultStatus.Success)
                    {
                        topMenus.DisplayName = result.Data.DisplayName;
                        topMenus.Sort = result.Data.Sort;
                        topMenus.Timestamp = result.Data.Timestamp;
                    }
                    else
                        _metroWindow.ShowMessageAsync("更新节点排序失败", result.Message);
                    break;
                }

                if (item.HasItems)
                    AddTreeViewItem(treeView, item.Items, selectedItem, topMenus);
            }
        }

        /// <summary>
        /// 根据原有的 TreeViewItem 生成一个新的 TreeViewItem，其中也包括了事件
        /// </summary>
        /// <param name="oldItem"></param>
        /// <returns></returns>
        public TreeViewItem GenerateTreeViewItem(TreeViewItem oldItem)
        {
            var leftMenuModel = (TopMenus)oldItem.DataContext;
            var item = new TreeViewItem
            {
                Header = leftMenuModel.DisplayName,
                DataContext = leftMenuModel
            };
            for (var i = 0; i < oldItem.Items.Count; i++)
            {
                var old = oldItem.Items[i];
                oldItem.Items.Remove(old);
                item.Items.Add(old);

                --i;
            }
            item.IsExpanded = oldItem.IsExpanded;
            item.Selected += TvLeftTopMenusItem_Selected;

            return item;
        }

        public TreeViewItem GenerateTreeViewItem(TopMenus topMenusModel)
        {
            var treeViewItem = new TreeViewItem
            {
                Header = topMenusModel.DisplayName,
                DataContext = topMenusModel
            };
            treeViewItem.Selected += TvLeftTopMenusItem_Selected;

            return treeViewItem;
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
            var selectedItem = TreeViewMain.SelectedItem as TreeViewItem;
            if (selectedItem == null || (string)selectedItem.Header == Config.RootDisplayName)
            {
                ParentId = 0;
                ParentDisplayName = Config.RootDisplayName;
            }
            else
            {
                var topMenusModel = (TopMenus)selectedItem.DataContext;
                ParentId = topMenusModel.Id;
                ParentDisplayName = LeftMenuRefer.GetSelectedPath(_topMenusList, topMenusModel.Id, "");
            }

            DisplayName = "";
            DllPath = "";
            MenuId = "";
            EntryFunction = "";
        }

        #region TooBar 操作事件

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
            var item = TreeViewMain.SelectedItem as TreeViewItem;
            if (!_clsLoginModel.PermissionsFunc(_currentMenuId, (int) PermissionsEnum.删除))
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

                _metroWindow.ShowMessageAsync("安全提示", $"您确定要删除名称为“{item.Header}”的节点吗？", MessageDialogStyle.AffirmativeAndNegative, setting).ContinueWith(
                    t =>
                    {
                        _metroWindow.Dispatcher.Invoke((Action)(() =>
                        {
                            if (t.Result == MessageDialogResult.Affirmative)
                            {
                                if (_topMenusManage.Exist(a => a.Id == ((TopMenus)item.DataContext).Id))
                                {
                                    var result = _topMenusManage.Deletes(a => a.Id == ((TopMenus)item.DataContext).Id);
                                    if (result)
                                    {
                                        // 第一种情况
                                        (item.Parent as TreeView)?.Items.Remove(item);
                                        // 第二种情况
                                        (item.Parent as TreeViewItem)?.Items.Remove(item);

                                        _topMenusList = _topMenusManage.GetAll().ToList();
                                        _metroWindow.ShowMessageAsync("删除成功提示", $"名称为“{item.Header}”的节点删除成功！");
                                    }
                                    else
                                        _metroWindow.ShowMessageAsync("删除失败提示", $"名称为“{item.Header}”的节点删除失败！");
                                }
                                else
                                    _metroWindow.ShowMessageAsync("删除失败提示", $"该“{item.Header}”节点在系统中已不存在！");
                            }
                        }));
                    });
            }
        }

        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnRefreshLeftMenu_OnClick(object sender, RoutedEventArgs e)
        {
            InitTopMenuForLeftAndRefer();
        }
        #endregion

        #region MVVM Models
        private int _topMenuId;
        private string _displayName;
        private string _menuId;
        private string _dllPath;
        private string _entryFunction;
        private int _parentId;
        private string _parentDisplayName;
        private long _timestamp;

        private bool _enableBtnReset;
        private bool _enableBtnSave;
        private Visibility _visibilityDockPanelNew;
        private Visibility _visibilityDockPanelModify;
        public int TopMenuId
        {
            get { return _topMenuId; }
            set
            {
                _topMenuId = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TopMenuId)));
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
