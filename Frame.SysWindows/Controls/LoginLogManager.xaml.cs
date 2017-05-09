using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows;
using Frame.Business;
using Frame.Business.interfaces;
using Frame.Models.SysModels.Log;
using Frame.Proxy;
using Frame.Proxy.Enums;
using Frame.Proxy.Windows;
using Frame.SysWindows.Prints;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Win32;

namespace Frame.SysWindows.Controls
{
    /// <summary>
    /// LoginLogManager.xaml 的交互逻辑
    /// </summary>
    public partial class LoginLogManager : INotifyPropertyChanged
    {
        private readonly ILogManage _logManage = new LogManage();
        private readonly ICompanyManage _companyManage = new CompanyManage();
        private readonly MetroWindow _metroWindow;
        private readonly ClsLoginModel _clsLoginModel;
        private readonly string _menuId;

        public event PropertyChangedEventHandler PropertyChanged;

        public LoginLogManager(MetroWindow metroWindow, ClsLoginModel clsLoginModel, string menuId)
        {
            InitializeComponent();
            _metroWindow = metroWindow;
            _clsLoginModel = clsLoginModel;
            _menuId = menuId;

            DataContext = this;
            InitLog();
        }

        private void InitLog()
        {
            LogAll = _logManage.LogManageSearch(Keywords);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSearch_OnClick(object sender, RoutedEventArgs e)
        {
            InitLog();
        }

        /// <summary>
        /// 清除全部
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnClearAll_OnClick(object sender, RoutedEventArgs e)
        {
            if (!_clsLoginModel.PermissionsFunc(_menuId, (int) PermissionsEnum.删除))
            {
                _metroWindow.ShowMessageAsync("友情提示", "您没有删除这些登陆日志的权限");
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
            _metroWindow.ShowMessageAsync("安全提示", "您确定要清空所有登陆日志吗？", MessageDialogStyle.AffirmativeAndNegative, setting).ContinueWith(
                a =>
                {
                    if (a.Result == MessageDialogResult.Affirmative)
                    {
                        Dispatcher.Invoke((Action) (() =>
                        {
                            _logManage.DeleteAll();
                            InitLog();
                            _metroWindow.ShowMessageAsync("友情提示", "所有登陆日志已全部清空");
                        }));
                    }
                });
        }

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnExport_OnClick(object sender, RoutedEventArgs e)
        {
            var saveFileDialog = new SaveFileDialog
            {
                AddExtension = true,
                CheckPathExists = true,
                OverwritePrompt = true,
                DefaultExt = ".csv",
                Filter = "csv 表格文件|*.csv"
            };
            if (!_clsLoginModel.PermissionsFunc(_menuId, (int) PermissionsEnum.导出))
                _metroWindow.ShowMessageAsync("友情提示", "您没有导出这些登陆日志的权限");
            else if (saveFileDialog.ShowDialog() == true)
            {
                var fileName = saveFileDialog.FileName;
                using (var writer = new StreamWriter(fileName, false, Encoding.UTF8))
                {
                    writer.WriteLine("序号,登录名,登陆日期,登陆角色,计算机名");
                    LogAll.ForEach(a =>
                    {
                        writer.WriteLine($"{a.RowId},\t{a.LoginName},\t{a.LoginTime},{a.LoginRole},\t{a.LoginMach}");
                    });
                }
                _metroWindow.ShowMessageAsync("友情提示", "登陆日志已成功导出");
            }
        }

        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnPrint_OnClick(object sender, RoutedEventArgs e)
        {
            if(!_clsLoginModel.PermissionsFunc(_menuId, (int)PermissionsEnum.打印))
            {
                _metroWindow.ShowMessageAsync("友情提示", "您没有打印这些登陆日志的权限");
                return;
            }

            var companyName = _companyManage.GetSettingModel()?.Name;
            var previewWnd = new PrintPreviewWindow(
                companyName,
                "pack://application:,,,/Frame.SysWindows;component/Prints/LoginLogManagerPrint.xaml",
                LogAll,
                new LoginLogManagerPrintRender())
            {
                Owner = _metroWindow,
                ShowInTaskbar = false
            };
            previewWnd.ShowDialog();
        }

        #region MVVM Models

        private string _keywords;
        private List<LogAllResponseModel> _logAll;

        public string Keywords
        {
            get { return _keywords; }
            set
            {
                _keywords = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Keywords)));
            }
        }

        public List<LogAllResponseModel> LogAll
        {
            get { return _logAll; }
            set
            {
                _logAll = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(LogAll)));
            }
        }

        #endregion
    }
}
