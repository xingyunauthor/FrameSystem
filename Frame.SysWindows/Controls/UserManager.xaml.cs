using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows;
using Frame.Business;
using Frame.Business.interfaces;
using Frame.Models;
using Frame.Models.SysModels;
using Frame.Models.SysModels.Staff;
using Frame.Proxy;
using Frame.Proxy.Enums;
using Frame.Proxy.Windows;
using Frame.SysWindows.Prints;
using Frame.SysWindows.Windows.Staff;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Win32;

namespace Frame.SysWindows.Controls
{
    /// <summary>
    /// UserManager.xaml 的交互逻辑
    /// </summary>
    public partial class UserManager : INotifyPropertyChanged
    {
        private readonly MetroWindow _metroWindow;
        private readonly ClsLoginModel _clsLoginModel;
        private readonly string _menuId;
        private readonly IStaffManage _staffManage = new StaffManage();
        private readonly IDeptManage _deptManage = new DeptManage();
        private readonly ICompanyManage _companyManage = new CompanyManage();

        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<StaffAllResponseModel> StaffAll { get; }

        public UserManager(MetroWindow metroWindow, ClsLoginModel clsLoginModel, string menuId)
        {
            InitializeComponent();
            _metroWindow = metroWindow;
            _clsLoginModel = clsLoginModel;
            _menuId = menuId;
            DataContext = this;

            StaffAll = _staffManage.All();
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSearch_OnClick(object sender, RoutedEventArgs e)
        {
            _staffManage.StaffSearch(StaffAll, Keywords);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAdd_OnClick(object sender, RoutedEventArgs e)
        {
            if (!_clsLoginModel.PermissionsFunc(_menuId, (int)PermissionsEnum.添加))
            {
                _metroWindow.ShowMessageAsync("友情提示", "您没有添加用户的权限");
                return;
            }

            Action<Staff> callback = (staff) =>
            {
                StaffAll.Clear();
                var all = _staffManage.All();
                foreach (var model in all)
                {
                    StaffAll.Add(model);
                }
            };
            var staffAdd = new StaffAdd(callback) { Owner = _metroWindow };
            staffAdd.ShowDialog();
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnUpdate_OnClick(object sender, RoutedEventArgs e)
        {
            var model = (StaffAllResponseModel)DgStaffs.SelectedItem;
            if (!_clsLoginModel.PermissionsFunc(_menuId, (int) PermissionsEnum.修改))
                _metroWindow.ShowMessageAsync("友情提示", "您没有修改用户的权限");
            else if (model == null)
                _metroWindow.ShowMessageAsync("友情提示", "请选择需要修改信息的员工");
            else
            {
                Action<Staff> callback = (staff) =>
                {
                    var result = _deptManage.GetModel(staff.DeptId);
                    if (result.ResultStatus == ResultStatus.Error)
                        _metroWindow.ShowMessageAsync("友情提示", result.Message);
                    else
                    {
                        model.StaffName = staff.Name;
                        model.DeptName = result.Data.Name;
                        model.Sex = staff.Sex == 1 ? "男" : "女";
                        model.Birthday = staff.Birth.ToString("yyyy-MM-dd");
                        model.InTime = staff.InTime.ToString("yyyy-MM-dd");
                        model.Telephone = staff.Tel;
                        model.Address = staff.Add;
                        model.StateName = staff.State ? "启用" : "未启用";
                        model.Remark = staff.Remark;
                    }
                };
                var staffEdit = new StaffEdit(model.StaffId, callback) { Owner = _metroWindow };
                staffEdit.ShowDialog();
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
            var model = (StaffAllResponseModel)DgStaffs.SelectedItem;
            if (!_clsLoginModel.PermissionsFunc(_menuId, (int) PermissionsEnum.删除))
                _metroWindow.ShowMessageAsync("友情提示", "您没有删除用户的权限");
            else if (model == null)
                _metroWindow.ShowMessageAsync("友情提示", "请选择需要删除的员工");
            else
                _metroWindow.ShowMessageAsync("安全提示", "您确定要出删除该员工信息吗？", MessageDialogStyle.AffirmativeAndNegative, setting).ContinueWith(
                    t =>
                    {
                        Dispatcher.Invoke((Action)(() =>
                        {
                            if (t.Result == MessageDialogResult.Affirmative)
                            {
                                var result = _staffManage.Delete(model.StaffId);
                                if (result.ResultStatus == ResultStatus.Success)
                                    StaffAll.Remove(model);
                                _metroWindow.ShowMessageAsync(result.ResultStatus == ResultStatus.Success ? "删除成功" : "删除失败", result.Message);
                            }
                        }));
                    });
        }

        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnPrint_OnClick(object sender, RoutedEventArgs e)
        {
            if (!_clsLoginModel.PermissionsFunc(_menuId, (int) PermissionsEnum.打印))
            {
                _metroWindow.ShowMessageAsync("友情提示", "您没有打印用户信息的权限");
                return;
            }

            var companyName = _companyManage.GetSettingModel()?.Name;
            var previewWnd = new PrintPreviewWindow(
                companyName,
                "pack://application:,,,/Frame.SysWindows;component/Prints/UserManagerPrint.xaml", 
                new UserManagerPrintModel { TotalCount = StaffAll.Count, StaffAll = StaffAll }, 
                new UserManagerPrintRender())
            {
                Owner = _metroWindow,
                ShowInTaskbar = false
            };
            previewWnd.ShowDialog();
        }

        /// <summary>
        /// 导出数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnExport_OnClick(object sender, RoutedEventArgs e)
        {
            var saveFile = new SaveFileDialog
            {
                AddExtension = true,
                CheckPathExists = true,
                OverwritePrompt = true,
                DefaultExt = ".csv",
                Filter = "csv 表格文件|*.csv"
            };
            if (!_clsLoginModel.PermissionsFunc(_menuId, (int) PermissionsEnum.导出))
                _metroWindow.ShowMessageAsync("友情提示", "您没有导出用户信息的权限");
            else if (saveFile.ShowDialog() == true)
            {
                var fileName = saveFile.FileName;
                using (var writer = new StreamWriter(fileName, false, Encoding.UTF8))
                {
                    writer.WriteLine("序号,员工编号,员工姓名,所属部门,性别,出生日期,入职日期,电话号码,联系地址,状态,备注");
                    foreach (var model in StaffAll)
                    {
                        writer.WriteLine($"{model.RowId},{model.Code},\t{model.StaffName},{model.DeptName},{model.Sex},\t{model.Birthday},\t{model.InTime},\t{model.Telephone},{model.Address},{model.StateName},{model.Remark}");
                    }
                }

                _metroWindow.ShowMessageAsync("友情提示", "员工信息已成功导出");
            }
        }

        #region MVVM Models

        private string _keywords;

        /// <summary>
        /// 关键词搜索
        /// </summary>
        public string Keywords
        {
            get { return _keywords; }
            set
            {
                _keywords = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Keywords)));
            }
        }
        #endregion
    }
}
