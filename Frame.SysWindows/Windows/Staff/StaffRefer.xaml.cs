using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using Frame.Business;
using Frame.Business.interfaces;
using Frame.Models.SysModels.Staff;

namespace Frame.SysWindows.Windows.Staff
{
    /// <summary>
    /// StaffRefer.xaml 的交互逻辑
    /// </summary>
    public partial class StaffRefer : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private readonly IStaffManage _staffManage = new StaffManage();
        private readonly Action<StaffAllResponseModel> _callback;
        public StaffRefer(Action<StaffAllResponseModel> callback)
        {
            InitializeComponent();
            StaffAll = new ObservableCollection<StaffAllResponseModel>();

            _callback = callback;
            DataContext = this;
            _staffManage.StaffReferSearch(StaffAll, Keywords);
        }

        private void BtnSearch_OnClick(object sender, RoutedEventArgs e)
        {
            _staffManage.StaffReferSearch(StaffAll, Keywords);
        }

        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnConfirm_OnClick(object sender, RoutedEventArgs e)
        {
            SelectStaff();
        }

        /// <summary>
        /// 双击确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgStaff_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            SelectStaff();
        }

        private void SelectStaff()
        {
            var selected = DgStaff.SelectedItem as StaffAllResponseModel;
            if (selected == null)
                MessageBox.Show("请选择员工信息", "友情提示", MessageBoxButton.OK, MessageBoxImage.Information);
            else
            {
                _callback?.Invoke(selected);
                DialogResult = true;
            }
        }

        #region MVVM Models

        private string _keywords;
        private ObservableCollection<StaffAllResponseModel> _staffAll;

        public string Keywords
        {
            get { return _keywords; }
            set
            {
                _keywords = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Keywords)));
            }
        }

        public ObservableCollection<StaffAllResponseModel> StaffAll
        {
            get { return _staffAll; }
            set
            {
                _staffAll = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(StaffAll)));
            }
        }
        #endregion
    }
}
