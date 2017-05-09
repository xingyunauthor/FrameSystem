using System;
using System.Linq;
using System.Windows;
using Frame.Business;
using Frame.Business.interfaces;
using Frame.Models.SysModels;
using Frame.Models.SysModels.Staff;
using Frame.SysWindows.MVModels;

namespace Frame.SysWindows.Windows.Staff
{
    /// <summary>
    /// OperatorAdd.xaml 的交互逻辑
    /// </summary>
    public partial class OperatorAdd
    {
        public event Action<Models.Staff> AddSuccess;

        private readonly IOperatorManage _operatorManage = new OperatorManage();
        private readonly IRolesManage _rolesManage = new RolesManage();
        private readonly OperatorAddEditModel _addModel = new OperatorAddEditModel();
        public OperatorAdd()
        {
            InitializeComponent();
            OperatorAddEdit.DataContext = _addModel;

            _rolesManage.GetAll().ForEach(a =>
            {
                _addModel.Roles.Add(new OperatorAddEditModel.RoleModel
                {
                    Checked = false,
                    RoleId = a.Id,
                    RoleName = a.RoleName
                });
            });
        }

        /// <summary>
        /// 确定
        /// </summary>
        private void OperatorAddEdit_OnOk()
        {
            if (_addModel.Verify())
            {
                var result = _operatorManage.Add(new OperatorAddResponseModel
                {
                    StaffId = _addModel.StaffId,
                    LogonName = _addModel.LogonName,
                    LogonPwd = _addModel.LogonPwd,
                    RoleIdes = _addModel.Roles.Where(a => a.Checked).Select(a => a.RoleId).ToList()
                });
                MessageBox.Show(result.Message, result.ResultStatus == ResultStatus.Success ? "添加成功" : "添加失败", MessageBoxButton.OK, MessageBoxImage.Information);
                if (result.ResultStatus == ResultStatus.Success)
                {
                    AddSuccess?.Invoke(result.Data);
                    DialogResult = true;
                }
            }
        }

        /// <summary>
        /// 取消
        /// </summary>
        private void OperatorAddEdit_OnCancel()
        {
            DialogResult = false;
        }

        /// <summary>
        /// 选择员工信息
        /// </summary>
        /// <param name="staffid">员工编号</param>
        /// <param name="staffname">员工姓名</param>
        private void OperatorAddEdit_OnSelectStaff(int staffid, string staffname)
        {
            _addModel.StaffId = staffid;
            _addModel.StaffName = staffname;
        }
    }
}
