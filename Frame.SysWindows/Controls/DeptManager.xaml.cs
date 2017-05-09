using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Frame.Business;
using Frame.Business.interfaces;
using Frame.Models.SysModels;
using Frame.Models.SysModels.Dept;
using Frame.Proxy;
using Frame.Proxy.Enums;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace Frame.SysWindows.Controls
{
    /// <summary>
    /// DeptManager.xaml 的交互逻辑
    /// </summary>
    public partial class DeptManager
    {
        private ObservableCollection<DeptAllResponseModel> _tvDeptItemsSource;

        private readonly MetroWindow _metroWindow;
        private readonly ClsLoginModel _clsLoginModel;
        private readonly string _menuId;
        private readonly IDeptManage _deptManage = new DeptManage();
        public DeptManager(MetroWindow metroWindow, ClsLoginModel clsLoginModel, string menuId)
        {
            InitializeComponent();
            _metroWindow = metroWindow;
            _clsLoginModel = clsLoginModel;
            _menuId = menuId;
            InitTvDept();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        private void InitTvDept()
        {
            _tvDeptItemsSource = _deptManage.All();
            _tvDeptItemsSource.Insert(0, new DeptAllResponseModel { DeptId = 0, DeptName = "root/", ParentId = 0});
            TvDept.ItemsSource = _tvDeptItemsSource;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAdd_OnClick(object sender, RoutedEventArgs e)
        {
            var setting = new MetroDialogSettings
            {
                AnimateShow = true,
                AnimateHide = true,
                AffirmativeButtonText = "确定",
                NegativeButtonText = "取消",
                DefaultButtonFocus = MessageDialogResult.Negative
            };
            var selectedItem = (DeptAllResponseModel)TvDept.SelectedItem;
            if (!_clsLoginModel.PermissionsFunc(_menuId, (int) PermissionsEnum.添加))
                _metroWindow.ShowMessageAsync("友情提示", "您没有添加部门信息的权限");
            else if (selectedItem == null)
                _metroWindow.ShowMessageAsync("友情提示", "请选择相应的节点作为父级节点");
            else
            {
                _metroWindow.ShowInputAsync("部门新增", "部门名称", setting).ContinueWith(t =>
                {
                    Dispatcher.Invoke((Action) (() =>
                    {
                        var deptName = t.Result;
                        if (deptName != null)
                        {
                            var result = _deptManage.Add(new DeptAddRequestModel { DeptName = deptName, ParentId = selectedItem.DeptId });
                            if (result.ResultStatus == ResultStatus.Success)
                            {
                                var item = new DeptAllResponseModel
                                {
                                    DeptId = result.Data.Id,
                                    DeptName = result.Data.Name,
                                    ParentId = result.Data.PId,
                                    Nodes = new ObservableCollection<DeptAllResponseModel>()
                                };
                                if (selectedItem.DeptId == 0)
                                    _tvDeptItemsSource.Add(item);
                                else
                                    selectedItem.Nodes.Add(item);
                            }
                            _metroWindow.ShowMessageAsync(result.ResultStatus == ResultStatus.Success ? "成功提示" : "失败提示", result.Message);
                        }
                    }));
                });
            }
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnUpdate_OnClick(object sender, RoutedEventArgs e)
        {
            var selectedItem = (DeptAllResponseModel)TvDept.SelectedItem;
            if (!_clsLoginModel.PermissionsFunc(_menuId, (int) PermissionsEnum.修改))
                _metroWindow.ShowMessageAsync("友情提示", "您没有修改部门信息的权限");
            else if (selectedItem == null)
                _metroWindow.ShowMessageAsync("友情提示", "请选择相应的节点");
            else if (selectedItem.DeptId == 0)
                _metroWindow.ShowMessageAsync("友情提示", "该节点不能被修改，请重新选择");
            else
            {
                var setting = new MetroDialogSettings
                {
                    AnimateShow = true,
                    AnimateHide = true,
                    AffirmativeButtonText = "确定",
                    NegativeButtonText = "取消",
                    DefaultButtonFocus = MessageDialogResult.Negative,
                    DefaultText = selectedItem.DeptName
                };
                _metroWindow.ShowInputAsync("部门信息修改", "部门名称", setting).ContinueWith(t =>
                {
                    Dispatcher.Invoke((Action)(() =>
                    {
                        var deptName = t.Result;
                        if (deptName != null)
                        {
                            var requestModel = new DeptUpdateRequestModel
                            {
                                DeptName = deptName,
                                ParentId = selectedItem.ParentId
                            };
                            var result = _deptManage.Update(selectedItem.DeptId, requestModel);
                            if (result.ResultStatus == ResultStatus.Success)
                            {
                                selectedItem.DeptName = result.Data.Name;
                                selectedItem.ParentId = result.Data.PId;
                            }
                            _metroWindow.ShowMessageAsync(result.ResultStatus == ResultStatus.Success ? "成功提示" : "失败提示", result.Message);
                        }
                    }));
                });
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDelete_OnClick(object sender, RoutedEventArgs e)
        {
            var setting = new MetroDialogSettings
            {
                AnimateShow = true,
                AnimateHide = true,
                AffirmativeButtonText = "确定",
                NegativeButtonText = "取消",
                DefaultButtonFocus = MessageDialogResult.Negative
            };
            var selectedItem = (DeptAllResponseModel)TvDept.SelectedItem;
            if (!_clsLoginModel.PermissionsFunc(_menuId, (int) PermissionsEnum.删除))
                _metroWindow.ShowMessageAsync("友情提示", "您没有删除部门信息的权限");
            else if (selectedItem == null)
                _metroWindow.ShowMessageAsync("友情提示", "请选择相应的节点");
            else if (selectedItem.DeptId == 0)
                _metroWindow.ShowMessageAsync("友情提示", "该节点不能被删除，请重新选择");
            else
            {
                _metroWindow.ShowMessageAsync("安全提示", $"您确定要删除“{selectedItem.DeptName}”部门吗？", MessageDialogStyle.AffirmativeAndNegative, setting).ContinueWith(
                    t =>
                    {
                        Dispatcher.Invoke((Action)(() =>
                        {
                            if (t.Result == MessageDialogResult.Affirmative)
                            {
                                var result = _deptManage.Delete(selectedItem.DeptId);
                                if (result.ResultStatus == ResultStatus.Success)
                                    RemoveDeptAllResponseModel(_tvDeptItemsSource, selectedItem);
                                _metroWindow.ShowMessageAsync(result.ResultStatus == ResultStatus.Success ? "成功提示" : "失败提示",
                                    result.Message);
                            }
                        }));
                    });
            }
        }

        /// <summary>
        /// 删除选中的节点
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="model"></param>
        private void RemoveDeptAllResponseModel(ObservableCollection<DeptAllResponseModel> collection, DeptAllResponseModel model)
        {
            if (collection.Count(a => a.DeptId == model.DeptId) > 0)
                _tvDeptItemsSource.Remove(model);
            else
                RemoveDeptAllResponseModel(collection, model);
        }
    }
}
