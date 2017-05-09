using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using Frame.Business;
using MahApps.Metro.Controls;
using Frame.Business.interfaces;
using Frame.Models;
using Frame.Models.SysModels;
using Frame.Models.SysModels.LeftMenuPermissions;
using Frame.Models.SysModels.Permissions;
using Frame.Models.SysModels.Roles;
using Frame.Proxy;
using Frame.Proxy.Enums;
using Frame.SysWindows.MVModels;
using Frame.Utils;
using MahApps.Metro.Controls.Dialogs;

namespace Frame.SysWindows.Controls
{
    /// <summary>
    /// RolesManager.xaml 的交互逻辑
    /// </summary>
    public partial class RolesManager
    {
        private readonly IRolesManage _rolesManage = new RolesManage();
        private readonly ILeftMenuPermissionsManage _leftMenuPermissionsManage = new LeftMenuPermissionsManage();
        private readonly IPermissionsManage _permissionsManage = new PermissionsManage();
        private readonly MetroWindow _metroWindow;
        private readonly ClsLoginModel _clsLoginModel;
        private readonly string _menuId;

        private readonly RolesManagerViewModel _rolesManagerViewModel = new RolesManagerViewModel();
        public RolesManager(MetroWindow metroWindow, ClsLoginModel clsLogin, string menuId)
        {
            InitializeComponent();
            _metroWindow = metroWindow;
            _clsLoginModel = clsLogin;
            _menuId = menuId;
            DataContext = _rolesManagerViewModel;

            InitDgLeftMenuPermissionsColumns();
            InitDgRoles();
            InitDgPermissions();
        }

        /// <summary>
        /// 初始化权限列表
        /// </summary>
        private void InitDgLeftMenuPermissionsColumns()
        {
            DgLeftMenuPermissions.Columns.Clear();

            DgLeftMenuPermissions.Columns.Add(new DataGridTemplateColumn
            {
                Header = "编号",
                CellTemplate = CreateTextBlockTemplate("RowId", HorizontalAlignment.Center)
            });
            DgLeftMenuPermissions.Columns.Add(new DataGridTemplateColumn
            {
                Header = "菜单名称",
                CellTemplate = CreateTextBlockTemplate("DisplayName", HorizontalAlignment.Left)
            });

            var all = _permissionsManage.GetAll();
            all.ForEach(a =>
            {
                DgLeftMenuPermissions.Columns.Add(new DataGridTemplateColumn
                {
                    Header = a,
                    CellTemplate = CreateCheckBoxTemplate($"C{a.Id}")
                });
            });
        }

        /// <summary>
        /// 初始化角色列表
        /// </summary>
        private void InitDgRoles()
        {
            RolesSource = new ObservableCollection<RolesModel>();
            var rowId = 0;
            var list = (from a in _rolesManage.GetAll()
                       select new RolesModel
                       {
                           RowId = ++rowId,
                           Id = a.Id,
                           RoleName = a.RoleName,
                           Timestamp = a.Timestamp
                       }).ToList();
            list.ForEach(a => RolesSource.Add(a));
            DgRoles.ItemsSource = RolesSource;
        }

        private void UpdateDgLeftMenuPermissions()
        {
            var rolesModel = (RolesModel)DgRoles.SelectedItem;
            if (rolesModel == null)
                DgLeftMenuPermissions.ItemsSource = null;
            else
            {
                var roleId = rolesModel.Id;
                System.Threading.Tasks.Task.Factory.StartNew(() =>
                {
                    try
                    {
                        var dt = _leftMenuPermissionsManage.GetLeftMenuPermissions(roleId, _rolesManagerViewModel.LeftMenuNameSearchKey ?? "");
                        dt.Columns.Add("RowId", typeof(int));

                        for (var i = 0; i < dt.Rows.Count; i++)
                            dt.Rows[i]["RowId"] = i + 1;
                        Dispatcher.Invoke((Action) (() =>
                        {
                            DgLeftMenuPermissions.ItemsSource = dt.AsDataView();
                            var cv = CollectionViewSource.GetDefaultView(DgLeftMenuPermissions.ItemsSource);
                            cv.GroupDescriptions.Add(new PropertyGroupDescription("NavBarGroupName"));
                        }));
                    }
                    catch (Exception ex)
                    {
                        Dispatcher.Invoke((Action) (() => { throw ex; }));
                    }
                });
            }
        }

        /// <summary>
        /// 行选中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgRoles_OnSelected(object sender, RoutedEventArgs e)
        {
            _rolesManagerViewModel.LeftMenuNameSearchKey = "";
            UpdateDgLeftMenuPermissions();
        }

        private void DgRoles_OnSourceUpdated(object sender, DataTransferEventArgs e)
        {
            MessageBox.Show("DgRoles_OnSourceUpdated");
        }

        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TEdit_OnAddClick(object sender, RoutedEventArgs e)
        {
            if (!_clsLoginModel.PermissionsFunc(_menuId, (int) PermissionsEnum.添加))
            {
                _metroWindow.ShowMessageAsync("友情提示", "您没有添加角色信息的权限");
                return;
            }

            var setting = new MetroDialogSettings
            {
                AnimateShow = true,
                AnimateHide = true,
                AffirmativeButtonText = "确定",
                NegativeButtonText = "取消",
                DefaultButtonFocus = MessageDialogResult.Negative
            };
            _metroWindow.ShowInputAsync("角色添加", "角色名称", setting).ContinueWith(t =>
            {
                Dispatcher.Invoke((Action)(() =>
                {
                    var roleName = t.Result;
                    if (roleName != null)
                    {
                        if (roleName.Trim().Length == 0)
                            _metroWindow.ShowMessageAsync("友情提示", "角色名称不能为空").ContinueWith(z => {
                                Dispatcher.Invoke((Action) (() => TEdit_OnAddClick(sender, e)));
                            });
                        else
                        {
                            var result = _rolesManage.Add(roleName, _rolesManage.ServerTime.ToUnixTimestamp());
                            if (result.ResultStatus == ResultStatus.Success)
                                RolesSource.Add(new RolesModel
                                {
                                    RowId = RolesSource.Count + 1,
                                    Id = result.Data.Id,
                                    RoleName = result.Data.RoleName,
                                    Timestamp = result.Data.Timestamp
                                });
                            _metroWindow.ShowMessageAsync(
                                result.ResultStatus == ResultStatus.Success ? "添加成功" : "添加失败", result.Message);
                        }
                    }
                }));
            });
        }

        /// <summary>
        /// 修改角色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TEdit_OnEditClick(object sender, RoutedEventArgs e)
        {
            if (!_clsLoginModel.PermissionsFunc(_menuId, (int) PermissionsEnum.修改))
            {
                _metroWindow.ShowMessageAsync("友情提示", "您没有修改角色信息的权限");
                return;
            }
            if (DgRoles.SelectedItems.Count < 1)
            {
                _metroWindow.ShowMessageAsync("友情提示", "请选择需要修改的角色信息");
                return;
            }
            var selectedItem = (RolesModel)DgRoles.SelectedItem;
            var setting = new MetroDialogSettings
            {
                AnimateShow = true,
                AnimateHide = true,
                AffirmativeButtonText = "保存",
                NegativeButtonText = "取消",
                DefaultButtonFocus = MessageDialogResult.Negative,
                DefaultText = selectedItem.RoleName
            };
            _metroWindow.ShowInputAsync("角色信息修改", "角色名称", setting).ContinueWith(t =>
            {
                Dispatcher.Invoke((Action)(() =>
                {
                    var roleName = t.Result;
                    if (roleName != null)
                    {
                        if (roleName.Trim().Length == 0)
                            _metroWindow.ShowMessageAsync("友情提示", "角色名称不能为空").ContinueWith(z => {
                                Dispatcher.Invoke((Action)(() => TEdit_OnEditClick(sender, e)));
                            });
                        else
                        {
                            _metroWindow.ShowMessageAsync("安全提示", "您确定要修改此角色信息吗？", MessageDialogStyle.AffirmativeAndNegative, new MetroDialogSettings
                            {
                                AnimateShow = true,
                                AnimateHide = true,
                                AffirmativeButtonText = "确定",
                                NegativeButtonText = "取消",
                                DefaultButtonFocus = MessageDialogResult.Negative
                            }).ContinueWith(a =>
                            {
                                Dispatcher.Invoke((Action)(() =>
                                {
                                    if (a.Result == MessageDialogResult.Affirmative)
                                    {
                                        var result =
                                            _rolesManage.Update(
                                                new RoleEditRequestModel
                                                {
                                                    RoleId = selectedItem.Id,
                                                    Timestamp = selectedItem.Timestamp
                                                },
                                                new RoleEditRequestNewModel
                                                {
                                                    RoleName = roleName,
                                                    Timestamp = _rolesManage.ServerTime.ToUnixTimestamp()
                                                });
                                        if (result.ResultStatus == ResultStatus.Success)
                                        {
                                            selectedItem.RoleName = result.Data.RoleName;
                                            selectedItem.Timestamp = result.Data.Timestamp;
                                        }
                                        Dispatcher.Invoke((Action)(() => _metroWindow.ShowMessageAsync(result.ResultStatus == ResultStatus.Success ? "修改成功" : "修改失败", result.Message)));
                                    }
                                }));
                            });
                        }
                    }
                }));
            });
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TEdit_OnDeleteClick(object sender, RoutedEventArgs e)
        {
            var setting = new MetroDialogSettings
            {
                AnimateShow = true,
                AnimateHide = true,
                AffirmativeButtonText = "确定",
                NegativeButtonText = "取消",
                DefaultButtonFocus = MessageDialogResult.Negative
            };
            if (!_clsLoginModel.PermissionsFunc(_menuId, (int) PermissionsEnum.删除))
            {
                _metroWindow.ShowMessageAsync("友情提示", "您没有删除角色信息的权限");
                return;
            }
            if (DgRoles.SelectedItems.Count < 1)
            {
                _metroWindow.ShowMessageAsync("友情提示", "请选择需要删除的角色信息");
                return;
            }

            _metroWindow.ShowMessageAsync("安全提示", "角色删除后将不能恢复，确定要删除吗？", MessageDialogStyle.AffirmativeAndNegative, setting).ContinueWith(
                t =>
                {
                    Dispatcher.Invoke((Action)(() =>
                    {
                        if (t.Result == MessageDialogResult.Affirmative)
                        {
                            var selectedItem = (RolesModel)DgRoles.SelectedItem;
                            var result = _rolesManage.Delete(selectedItem.Id);
                            if (result.ResultStatus == ResultStatus.Success)
                                RolesSource.Remove(selectedItem);
                            _metroWindow.ShowMessageAsync(result.ResultStatus == ResultStatus.Success ? "删除成功" : "删除失败", result.Message);
                        }
                    }));
                });
        }

        /// <summary>
        /// 角色刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TEdit_OnRefreshClick(object sender, RoutedEventArgs e)
        {
            InitDgLeftMenuPermissionsColumns();
            InitDgRoles();
        }

        /// <summary>
        /// 搜索“菜单名称”
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TEdit_OnSearchLeftMenuClick(object sender, RoutedEventArgs e)
        {
            UpdateDgLeftMenuPermissions();
        }

        /// <summary>
        /// 权限被点击后
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChkPermissions_OnClick(object sender, RoutedEventArgs e)
        {
            var isChecked = ((CheckBox)sender).IsChecked ?? false;
            var selectedItem = (DataRowView)DgLeftMenuPermissions.SelectedItem;
            var permissions = (Permissions)DgLeftMenuPermissions.CurrentCell.Column.Header;
            var requestModel = new ModifyPermissionsRequestModel
            {
                RoleId = Convert.ToInt32(selectedItem["RoleId"]),
                LeftMenuId = Convert.ToInt32(selectedItem["LeftMenuId"]),
                PermissionsId = permissions.Id,
                Have = isChecked
            };

            var result = _leftMenuPermissionsManage.ModifyPermissions(requestModel);
            if (result.ResultStatus == ResultStatus.Error)
            {
                selectedItem[$"C{permissions.Id}"] = !isChecked;
                _metroWindow.ShowMessageAsync("友情提示", result.Message);
            }
            else
                selectedItem[$"C{permissions.Id}"] = isChecked;
        }

        /// <summary>
        /// 创建 CheckBox 的模板列
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        private DataTemplate CreateCheckBoxTemplate(string column)
        {
            var template = new DataTemplate();

            var factoryText = new FrameworkElementFactory(typeof(CheckBox));
            factoryText.SetValue(ToggleButton.IsCheckedProperty, new Binding(column));
            factoryText.SetValue(ToggleButton.HorizontalAlignmentProperty, HorizontalAlignment.Center);
            factoryText.AddHandler(ButtonBase.ClickEvent, new RoutedEventHandler(ChkPermissions_OnClick));
            template.VisualTree = factoryText;

            return template;
        }

        /// <summary>
        /// 创建 TextBlock 模板列
        /// </summary>
        /// <param name="column"></param>
        /// <param name="alignment"></param>
        /// <returns></returns>
        private DataTemplate CreateTextBlockTemplate(string column, HorizontalAlignment alignment)
        {
            var template = new DataTemplate();

            var factoryText = new FrameworkElementFactory(typeof(TextBlock));
            factoryText.SetValue(TextBlock.TextProperty, new Binding(column));
            factoryText.SetValue(TextBlock.HorizontalAlignmentProperty, alignment);
            template.VisualTree = factoryText;

            return template;
        }

        private ObservableCollection<RolesModel> RolesSource { get; set; }

        #region Permissions 权限操作
        /// <summary>
        /// 初始化 DgPermissions 数据
        /// </summary>
        private void InitDgPermissions()
        {
            PermissionsSource = new ObservableCollection<PermissionsModel>();
            var rowId = 0;
            var list = _permissionsManage.GetAll().ToList();
            list.ForEach(a =>
            {
                PermissionsSource.Add(new PermissionsModel
                {
                    RowId = ++rowId,
                    PermissionsId = a.Id,
                    PermissionsName = a.PermissionsName,
                    Sort = a.Sort
                });
            });
            DgPermissions.ItemsSource = PermissionsSource;
        }

        /// <summary>
        /// 权限新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TbtnPermissionsAdd_OnClick(object sender, RoutedEventArgs e)
        {
            if (!_clsLoginModel.PermissionsFunc(_menuId, (int) PermissionsEnum.添加))
            {
                _metroWindow.ShowMessageAsync("友情提示", "您没有添加权限信息的权限");
                return;
            }
            var setting = new MetroDialogSettings
            {
                AnimateShow = true,
                AnimateHide = true,
                AffirmativeButtonText = "确定",
                NegativeButtonText = "取消",
                DefaultButtonFocus = MessageDialogResult.Negative
            };
            _metroWindow.ShowInputAsync("权限新增", "权限名称", setting).ContinueWith(
                a =>
                {
                    Dispatcher.Invoke((Action)(() =>
                    {
                        var permissionsName = a.Result;
                        if (permissionsName != null)
                        {
                            if (permissionsName.Trim().Length == 0)
                                _metroWindow.ShowMessageAsync("友情提示", "权限名称不能空").ContinueWith(z => {
                                    Dispatcher.Invoke((Action)(() => TbtnPermissionsAdd_OnClick(sender, e)));
                                });
                            else
                            {
                                var response = _permissionsManage.Add(new PermissionsAddRequestModel
                                {
                                    PermissionsName = permissionsName,
                                    Sort = int.MaxValue
                                });
                                InitDgPermissions();
                                _metroWindow.ShowMessageAsync(response.ResultStatus == ResultStatus.Success ? "新增成功" : "新增失败", response.Message);
                            }
                        }
                    }));
                });
        }

        /// <summary>
        /// 权限修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TbtnPermissionsEdit_OnClick(object sender, RoutedEventArgs e)
        {
            var entity = (PermissionsModel)DgPermissions.SelectedItem;
            if (!_clsLoginModel.PermissionsFunc(_menuId, (int) PermissionsEnum.修改))
                _metroWindow.ShowMessageAsync("友情提示", "您没有修改权限信息的权限");
            else if (entity == null)
                _metroWindow.ShowMessageAsync("友情提示", "请选择需要修改的权限信息");
            else
            {
                var setting = new MetroDialogSettings
                {
                    AnimateShow = true,
                    AnimateHide = true,
                    AffirmativeButtonText = "保存",
                    NegativeButtonText = "取消",
                    DefaultButtonFocus = MessageDialogResult.Negative,
                    DefaultText = entity.PermissionsName
                };
                _metroWindow.ShowInputAsync("权限信息修改", "权限名称", setting).ContinueWith(a =>
                {
                    Dispatcher.Invoke((Action)(() =>
                    {
                        if (a.Result != null)
                        {
                            if (a.Result.Trim().Length == 0)
                                _metroWindow.ShowMessageAsync("友情提示", "权限名称不能为空").ContinueWith(z => {
                                    Dispatcher.Invoke((Action)(() => TbtnPermissionsEdit_OnClick(sender, e)));
                                });
                            else
                            {
                                var updateRequest = new PermissionsUpdateRequestModel
                                {
                                    PermissionsName = a.Result
                                };
                                var result = _permissionsManage.Update(entity.PermissionsId, updateRequest);
                                _metroWindow.ShowMessageAsync(
                                    result.ResultStatus == ResultStatus.Success ? "修改成功提示" : "修改失败提示",
                                    result.Message);
                                if (result.ResultStatus == ResultStatus.Success)
                                {
                                    entity.PermissionsName = result.Data.PermissionsName;
                                    entity.Sort = result.Data.Sort;
                                }
                            }
                        }
                    }));
                });
            }
        }

        /// <summary>
        /// 权限删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TbtnPermissionsDelete_OnClick(object sender, RoutedEventArgs e)
        {
            var setting = new MetroDialogSettings
            {
                AnimateShow = true,
                AnimateHide = true,
                AffirmativeButtonText = "确定",
                NegativeButtonText = "取消",
                DefaultButtonFocus = MessageDialogResult.Negative
            };
            var entity = (PermissionsModel)DgPermissions.SelectedItem;
            if (!_clsLoginModel.PermissionsFunc(_menuId, (int) PermissionsEnum.删除))
                _metroWindow.ShowMessageAsync("友情提示", "您没有删除权限信息的权限");
            else if (entity == null)
                _metroWindow.ShowMessageAsync("友情提示", "请选择需要删除的权限信息");
            else
                _metroWindow.ShowMessageAsync("安全提示", $"您确定要删除“{entity.PermissionsName}”权限吗？", MessageDialogStyle.AffirmativeAndNegative, setting).ContinueWith(
                    a =>
                    {
                        Dispatcher.Invoke((Action)(() =>
                        {
                            if (a.Result == MessageDialogResult.Affirmative)
                            {
                                var result = _permissionsManage.Delete(entity.PermissionsId);
                                _metroWindow.ShowMessageAsync(
                                    result.ResultStatus == ResultStatus.Success ? "删除成功提示" : "删除失败提示", result.Message);
                                if (result.ResultStatus == ResultStatus.Success)
                                    InitDgPermissions();
                            }
                        }));
                    });
        }

        /// <summary>
        /// 权限位置上移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TbtnPermissionsUp_OnClick(object sender, RoutedEventArgs e)
        {
            var selectedIndex = DgPermissions.SelectedIndex;
            if (selectedIndex == -1)
                _metroWindow.ShowMessageAsync("友情提示", "请选择需要移动的权限");
            else
            {
                var selectedModel = PermissionsSource[selectedIndex];
                var upIndex = selectedIndex - 1;
                if (upIndex > -1)
                {
                    PermissionsSource.Remove(selectedModel);
                    PermissionsSource.Insert(upIndex, selectedModel);
                    DgPermissions.SelectedItem = selectedModel;
                }
            }
        }

        /// <summary>
        /// 权限位置下移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TbtnPermissionsDown_OnClick(object sender, RoutedEventArgs e)
        {
            var selectedIndex = DgPermissions.SelectedIndex;
            if (selectedIndex == -1)
                _metroWindow.ShowMessageAsync("友情提示", "请选择需要移动的权限");
            else
            {
                var selectedModel = PermissionsSource[selectedIndex];
                var downIndex = selectedIndex + 1;
                if (downIndex < PermissionsSource.Count)
                {
                    PermissionsSource.Remove(selectedModel);
                    PermissionsSource.Insert(downIndex, selectedModel);
                    DgPermissions.SelectedItem = selectedModel;
                }
            }
        }

        /// <summary>
        /// 权限保存排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TbtnPermissionsSave_OnClick(object sender, RoutedEventArgs e)
        {
            if (!_clsLoginModel.PermissionsFunc(_menuId, (int) PermissionsEnum.修改))
            {
                _metroWindow.ShowMessageAsync("安全提示", "您没有修改权限排序的权限");
                return;
            }
            var setting = new MetroDialogSettings
            {
                AnimateShow = true,
                AnimateHide = true,
                AffirmativeButtonText = "确定",
                NegativeButtonText = "取消",
                DefaultButtonFocus = MessageDialogResult.Negative
            };
            _metroWindow.ShowMessageAsync("安全提示", "您确定要更改当前排序吗？", MessageDialogStyle.AffirmativeAndNegative, setting).ContinueWith(
                a =>
                {
                    Dispatcher.Invoke((Action) (() =>
                    {
                        if (a.Result == MessageDialogResult.Affirmative)
                        {
                            var list = new List<PermissionsUpdateSortRequestModel>();
                            for (var i = 0; i < PermissionsSource.Count; i++)
                            {
                                PermissionsSource[i].Sort = i;
                                list.Add(new PermissionsUpdateSortRequestModel
                                {
                                    PermissionsId = PermissionsSource[i].PermissionsId,
                                    Sort = i
                                });
                            }
                            var result = _permissionsManage.UpdateSort(list);
                            _metroWindow.ShowMessageAsync(result.ResultStatus == ResultStatus.Success ? "保存成功提示" : "保存失败提示", result.Message);
                            if(result.ResultStatus == ResultStatus.Success)
                                InitDgPermissions();
                        }
                    }));
                });
        }

        /// <summary>
        /// 刷新权限列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TbtnPermissionsRefresh_OnClick(object sender, RoutedEventArgs e)
        {
            InitDgPermissions();
        }

        private ObservableCollection<PermissionsModel> PermissionsSource { get; set; }
        #endregion
    }
}
