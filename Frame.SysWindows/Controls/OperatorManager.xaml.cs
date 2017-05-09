using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using Frame.Business;
using Frame.Business.interfaces;
using Frame.Models.SysModels;
using Frame.Models.SysModels.Operator;
using Frame.Proxy;
using Frame.Proxy.Enums;
using MahApps.Metro.Controls;
using Frame.SysWindows.Windows.Staff;
using MahApps.Metro.Controls.Dialogs;

namespace Frame.SysWindows.Controls
{
    /// <summary>
    /// OperatorManager.xaml 的交互逻辑
    /// </summary>
    public partial class OperatorManager : INotifyPropertyChanged
    {
        private readonly MetroWindow _metroWindow;
        private readonly ClsLoginModel _clsLoginModel;
        private readonly string _menuId;
        public event PropertyChangedEventHandler PropertyChanged;
        private readonly IOperatorManage _operatorManage = new OperatorManage();

        public OperatorManager(MetroWindow metroWindow, ClsLoginModel clsLogin, string menuId)
        {
            InitializeComponent();
            _metroWindow = metroWindow;
            _clsLoginModel = clsLogin;
            _menuId = menuId;
            DataContext = this;

            Init();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        private void Init()
        {
            OperatorList = _operatorManage.All(Keywords);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSearch_OnClick(object sender, RoutedEventArgs e)
        {
            Init();
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAdd_OnClick(object sender, RoutedEventArgs e)
        {
            if (!_clsLoginModel.PermissionsFunc(_menuId, (int) PermissionsEnum.添加))
            {
                _metroWindow.ShowMessageAsync("友情提示", "您没有添加操作员信息的权限");
                return;
            }

            var operatorAdd = new OperatorAdd() { Owner = _metroWindow };
            operatorAdd.AddSuccess += staff =>
            {
                Init();
            };
            operatorAdd.ShowDialog();
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnEdit_OnClick(object sender, RoutedEventArgs e)
        {
            var selectedItem = DgOperator.SelectedItem as OperatorAllResponseModel;
            if (!_clsLoginModel.PermissionsFunc(_menuId, (int) PermissionsEnum.修改))
                _metroWindow.ShowMessageAsync("友情提示", "您没有修改操作员信息的权限");
            else if (selectedItem == null)
                _metroWindow.ShowMessageAsync("友情提示", "请选择相应的操作员");
            else
            {
                var operatorEdit = new OperatorEdit(selectedItem.Id, Init) { Owner = _metroWindow };
                operatorEdit.ShowDialog();
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

            var selectedItem = DgOperator.SelectedItem as OperatorAllResponseModel;
            if (!_clsLoginModel.PermissionsFunc(_menuId, (int) PermissionsEnum.删除))
                _metroWindow.ShowMessageAsync("友情提示", "您没有删除操作员信息的权限");
            else if (selectedItem == null)
                _metroWindow.ShowMessageAsync("友情提示", "请选择相应的操作员");
            else
            {
                _metroWindow.ShowMessageAsync("安全提示", "您确定要删除该操作人吗？", MessageDialogStyle.AffirmativeAndNegative, setting).ContinueWith(
                    a =>
                    {
                        if (a.Result == MessageDialogResult.Affirmative)
                        {
                            Dispatcher.Invoke((Action)(() =>
                            {
                                var result = _operatorManage.Delete(selectedItem.Id);
                                _metroWindow.ShowMessageAsync(result.ResultStatus == ResultStatus.Success ? "成功提示" : "失败提示", result.Message);
                                if (result.ResultStatus == ResultStatus.Success)
                                    Init();
                            }));
                        }
                    });
            }
        }

        #region MVVM Models

        private string _keywords;
        private ObservableCollection<OperatorAllResponseModel> _operatorList;

        public string Keywords
        {
            get { return _keywords; }
            set
            {
                _keywords = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Keywords)));
            }
        }

        public ObservableCollection<OperatorAllResponseModel> OperatorList
        {
            get { return _operatorList; }
            set
            {
                _operatorList = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(OperatorList)));
            }
        }

        #endregion
    }
}
