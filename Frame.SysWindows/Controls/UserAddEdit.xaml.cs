using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Frame.Models.SysModels.Dept;

namespace Frame.SysWindows.Controls
{
    /// <summary>
    /// UserAddEdit.xaml 的交互逻辑
    /// </summary>
    public partial class UserAddEdit : INotifyPropertyChanged
    {
        public event Action<object, RoutedEventArgs> Cancel;
        public event Action<object, RoutedEventArgs> Ok;
        public event PropertyChangedEventHandler PropertyChanged;
        public event Action<object, RoutedPropertyChangedEventArgs<object>> DeptSelectionChanged ;

        public UserAddEdit()
        {
            InitializeComponent();
            DpOperation.DataContext = this;
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCancel_OnClick(object sender, RoutedEventArgs e)
        {
            Cancel?.Invoke(sender, e);
        }

        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnOk_OnClick(object sender, RoutedEventArgs e)
        {
            Ok?.Invoke(sender, e);
        }

        private void BtnDept_OnClick(object sender, RoutedEventArgs e)
        {
            PopupDept.IsOpen = true;
        }

        private void TvDept_OnSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var item = ((TreeView)sender).SelectedItem as DeptAllResponseModel;
            if (item?.Nodes?.Count == 0)
            {
                PopupDept.IsOpen = false;
                DeptSelectionChanged?.Invoke(sender, e);
            }
        }

        #region MVVM Models

        private Visibility _showContinueCheck;

        public Visibility ShowContinueCheck
        {
            get { return _showContinueCheck; }
            set
            {
                _showContinueCheck = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ShowContinueCheck)));
            }
        }
        #endregion
    }
}
