using System;
using System.Windows;
using Frame.Models.SysModels.Staff;
using Frame.SysWindows.Windows.Staff;

namespace Frame.SysWindows.Controls
{
    /// <summary>
    /// OperatorAddEdit.xaml 的交互逻辑
    /// </summary>
    public partial class OperatorAddEdit
    {
        public delegate void SelectStaffDelegate(int staffId, string staffName);

        public event SelectStaffDelegate SelectStaff;
        public event Action Ok;
        public event Action Cancel;

        public OperatorAddEdit()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 选择员工信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSelectStaff_OnClick(object sender, RoutedEventArgs e)
        {
            Action<StaffAllResponseModel> callback = model =>
            {
                SelectStaff?.Invoke(model.StaffId, model.StaffName);
            };
            var staffRefer = new StaffRefer(callback);
            staffRefer.ShowDialog();
        }

        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnOk_OnClick(object sender, RoutedEventArgs e)
        {
            Ok?.Invoke();
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCancel_OnClick(object sender, RoutedEventArgs e)
        {
            Cancel?.Invoke();
        }
    }
}
