using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Frame.Proxy.Controls;
using Frame.Utils;
using MahApps.Metro.Controls.Dialogs;

namespace Frame.SysWindows.Windows.Common
{
    /// <summary>
    /// NavBarGroupIcoRefer.xaml 的交互逻辑
    /// </summary>
    public partial class NavBarGroupIcoRefer : INotifyPropertyChanged
    {
        private readonly Action<FilePathSelTreeView> _callback;
        public NavBarGroupIcoRefer(Action<FilePathSelTreeView> callback)
        {
            InitializeComponent();
            _callback = callback;
            DataContext = this;
            InitTvIcoPath();
        }

        /// <summary>
        /// 初始化 TvIcoPath
        /// </summary>
        private void InitTvIcoPath()
        {
            IcoBasePath = Config.IcoBasePath;
            EnableBtnOk = false;

            Files.TreeViewAddItems(TvIcoPath, IcoBasePath, ".png", ".ico", ".jpg");
        }

        /// <summary>
        /// 打开 Ico 目录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnOpenIcoPath_OnClick(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", $"{Config.IcoBasePath}");
        }

        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnRefreshTreeView_OnClick(object sender, RoutedEventArgs e)
        {
            TvIcoPath.Items.Clear();
            InitTvIcoPath();
        }

        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnOk_OnClick(object sender, RoutedEventArgs e)
        {
            var selectedItem = (TvIcoPath.SelectedItem as TreeViewImgItem)?.DataContext as FilePathSelTreeView;
            if (selectedItem != null)
            {
                DialogResult = true;

                _callback(selectedItem);
            }
            else
            {
                EnableBtnOk = false;
                this.ShowMessageAsync("错误提示", "请先选择需要的图片文件……");
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
        /// tree 的节点选中后
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TvIcoPath_OnSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var selectedItem = (TvIcoPath.SelectedItem as TreeViewImgItem)?.DataContext as FilePathSelTreeView;
            if (selectedItem?.FileType == FileTypeEnum.File)
            {
                EnableBtnOk = true;
                SelectedIco = new BitmapImage(new Uri(selectedItem.FullPath));
            }
            else
                EnableBtnOk = false;
        }

        #region MVVM Models

        public event PropertyChangedEventHandler PropertyChanged;
        private string _icoBasePath;
        private bool _enableBtnOk;
        private ImageSource _selectedIco;

        public string IcoBasePath
        {
            get { return _icoBasePath; }
            set
            {
                _icoBasePath = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IcoBasePath)));
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

        public ImageSource SelectedIco
        {
            get { return _selectedIco; }
            set
            {
                _selectedIco = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedIco)));
            }
        }

        #endregion
    }
}
