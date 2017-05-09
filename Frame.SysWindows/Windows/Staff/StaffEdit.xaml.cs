using System;
using System.Globalization;
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
    /// StaffEdit.xaml 的交互逻辑
    /// </summary>
    public partial class StaffEdit
    {
        private readonly int _staffId;
        private readonly Action<Models.Staff> _callback;

        public MVModels.StaffAddEdit StaffModel { get; } = new MVModels.StaffAddEdit();
        
        private readonly IStaffManage _staffManage = new StaffManage();
        private readonly IDeptManage _deptManage = new DeptManage();

        public StaffEdit(int staffId, Action<Models.Staff> callback)
        {
            InitializeComponent();
            _staffId = staffId;
            _callback = callback;

            UserAddEdit.DataContext = StaffModel;
            Init();
        }

        private void Init()
        {
            var result = _staffManage.GetModel(_staffId);if (result.ResultStatus == ResultStatus.Error)
                MessageBox.Show(result.Message, "友情提示", MessageBoxButton.OK, MessageBoxImage.Information);
            else
            {
                StaffModel.CodeReadonly = true;
                StaffModel.Code = result.Data.Code;
                StaffModel.StaffName = result.Data.Name;
                StaffModel.SexMale = result.Data.Sex == 1;
                StaffModel.SexFemale = result.Data.Sex == 0;
                StaffModel.Dept = new DeptAllResponseModel { DeptId = result.Data.DeptForeign.Id, DeptName = result.Data.DeptForeign.Name, ParentId = result.Data.DeptForeign.PId };
                StaffModel.DeptName = StaffModel.Dept.DeptName;
                StaffModel.DeptAll = _deptManage.All();
                StaffModel.InTime = result.Data.InTime.ToString(CultureInfo.InvariantCulture);
                StaffModel.Birthday = result.Data.Birth.ToString(CultureInfo.InvariantCulture);
                StaffModel.Telephone = result.Data.Tel;
                StaffModel.Enable = result.Data.State;
                StaffModel.UnEnable = !result.Data.State;
                StaffModel.Address = result.Data.Add;
                StaffModel.Remark = result.Data.Remark;
            }
        }

        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserAddEdit_OnOk(object sender, RoutedEventArgs e)
        {
            if (StaffModel.Verify())
            {
                if (MessageBox.Show("您确定要修改此员工信息吗？", "安全提示", MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.OK)
                {
                    var result = _staffManage.Update(_staffId, new StaffUpdateRequestModel
                    {
                        Name = StaffModel.StaffName,
                        Sex = StaffModel.SexMale ? 1 : 0,
                        Birth = Convert.ToDateTime(StaffModel.Birthday),
                        Add = StaffModel.Address,
                        Tel = StaffModel.Telephone,
                        DeptId = StaffModel.Dept.DeptId,
                        InTime = Convert.ToDateTime(StaffModel.InTime),
                        State = StaffModel.Enable,
                        Remark = StaffModel.Remark,
                        Oper = ""
                    });
                    MessageBox.Show(result.Message, result.ResultStatus == ResultStatus.Success ? "成功提示" : "失败提示", MessageBoxButton.OK, MessageBoxImage.Information);
                    if (result.ResultStatus == ResultStatus.Success)
                    {
                        DialogResult = true;
                        _callback(result.Data);
                    }
                }
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
