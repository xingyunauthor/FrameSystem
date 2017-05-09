using System;
using System.Windows;
using System.Windows.Controls;
using Frame.Business;
using Frame.Business.interfaces;
using Frame.Models.SysModels;
using Frame.Models.SysModels.Dept;
using Frame.Models.SysModels.Staff;

namespace Frame.SysWindows.Windows.Staff
{
    /// <summary>
    /// StaffAdd.xaml 的交互逻辑
    /// </summary>
    public partial class StaffAdd
    {
        private readonly Action<Models.Staff> _callback;
        private MVModels.StaffAddEdit StaffModel { get; } = new MVModels.StaffAddEdit();
        private readonly IStaffManage _staffManage = new StaffManage();
        private readonly IDeptManage _deptManage = new DeptManage();
        public StaffAdd(Action<Models.Staff> callback)
        {
            InitializeComponent();
            _callback = callback;

            UserAddEdit.DataContext = StaffModel;
            StaffModel.Code = _staffManage.GetNewCode();
            StaffModel.DeptAll = _deptManage.All();
        }

        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserAddEdit_OnOk(object sender, RoutedEventArgs e)
        {
            if(!StaffModel.Verify()) return;

            var result = _staffManage.Add(new StaffAddRequestModel
            {
                Code = StaffModel.Code,
                Name = StaffModel.StaffName,
                Sex = StaffModel.SexMale ? 1 : 0,
                DeptId = StaffModel.Dept.DeptId,
                InTime = Convert.ToDateTime(StaffModel.InTime),
                Birth = Convert.ToDateTime(StaffModel.Birthday),
                Tel = StaffModel.Telephone,
                State = StaffModel.Enable,
                Add = StaffModel.Address,
                Oper = ""
            });
            MessageBox.Show(result.Message, result.ResultStatus == ResultStatus.Success ? "成功提示" : "失败提示", MessageBoxButton.OK,
                MessageBoxImage.Information);
            if (result.ResultStatus == ResultStatus.Success)
            {
                if (StaffModel.ContinueCheck)
                {
                    StaffModel.Code = "";
                    StaffModel.StaffName = "";
                    StaffModel.SexMale = true;
                    StaffModel.SexFemale = false;
                    StaffModel.Dept = default(DeptAllResponseModel);
                    StaffModel.InTime = "";
                    StaffModel.Birthday = "";
                    StaffModel.Telephone = "";
                    StaffModel.Enable = true;
                    StaffModel.UnEnable = false;
                    StaffModel.Address = "";
                    StaffModel.Remark = "";
                }
                else
                    DialogResult = true;
                _callback(result.Data);
            }
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserAddEdit_OnCancel(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void UserAddEdit_OnDeptSelectionChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var tvDept = (TreeView)sender;

            StaffModel.Dept = (DeptAllResponseModel)tvDept.SelectedItem;
            StaffModel.DeptName = StaffModel.Dept.DeptName;
        }
    }
}
