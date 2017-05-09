using System;
using System.ComponentModel;
using System.Windows;
using Frame.Proxy.Controls;
using Frame.Utils;
using MahApps.Metro.Controls.Dialogs;

namespace Frame.SysWindows.Windows.Common
{
    /// <summary>
    /// DllPathRefer.xaml 的交互逻辑
    /// </summary>
    public partial class DllPathRefer : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public Action<FilePathSelTreeView> Callback;
        public DllPathRefer(Action<FilePathSelTreeView> callback)
        {
            InitializeComponent();
            Callback = callback;

            DataContext = this;

            InitTvDllPath();
        }

        /// <summary>
        /// 初始化 TvDllPath 控件
        /// </summary>
        private void InitTvDllPath()
        {
            DevPlatformPath = Config.DevPlatformPath;
            EnableBtnOk = false;

            Files.TreeViewAddItems(TvDllPath, DevPlatformPath, ".dll");
        }

        /// <summary>
        /// 打开开发目录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnOpenDevPlatformPath_OnClick(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", Config.DevPlatformPath);
        }

        /// <summary>
        /// 刷新树形
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnRefreshTreeView_OnClick(object sender, RoutedEventArgs e)
        {
            TvDllPath.Items.Clear();
            InitTvDllPath();
        }

        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnOk_OnClick(object sender, RoutedEventArgs e)
        {
            var selectedItem = TvDllPath.SelectedItem as TreeViewImgItem;
            if (selectedItem == null)
            {
                EnableBtnOk = false;
                this.ShowMessageAsync("信息提示", "请选择需要选择的节点……");
            }
            else
            {
                Callback((FilePathSelTreeView)selectedItem.DataContext);
                DialogResult = true;
            }
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCancel_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        /// <summary>
        /// TvDllPath 下的节点选择发生变化后执行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TvDllPath_OnSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var selectedItem = TvDllPath.SelectedItem as TreeViewImgItem;
            EnableBtnOk = ((FilePathSelTreeView)selectedItem?.DataContext)?.FileType == FileTypeEnum.File;
        }

        #region MVVM Models

        private string _devPlatformPath;
        private bool _enableBtnOk;

        /// <summary>
        /// 开发目录
        /// </summary>
        public string DevPlatformPath
        {
            get { return _devPlatformPath; }
            set
            {
                _devPlatformPath = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DevPlatformPath)));
            }
        }

        public bool EnableBtnOk
        {
            get { return _enableBtnOk; }
            set
            {
                _enableBtnOk = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(EnableBtnOk)));
            }
        }

        #endregion
    }
}
