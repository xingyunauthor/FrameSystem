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
    /// OperatorEdit.xaml 的交互逻辑
    /// </summary>
    public partial class OperatorEdit
    {
        private readonly int _staffId;
        private readonly Action _callback;
        private readonly IOperatorManage _operatorManage = new OperatorManage();
        private readonly IRolesManage _rolesManage = new RolesManage();
        private readonly IStaffRoleRelationshipsManage _staffRoleRelationshipsManage = new StaffRoleRelationshipsManage();
        private readonly OperatorAddEditModel _operatorEditModel = new OperatorAddEditModel();
        public OperatorEdit(int staffId, Action callback)
        {
            InitializeComponent();
            OperatorAddEdit.DataContext = _operatorEditModel;
            _staffId = staffId;
            _callback = callback;

            Init();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        private void Init()
        {
            var result = _operatorManage.GetModel(_staffId);
            if (result.ResultStatus == ResultStatus.Error)
            {
                MessageBox.Show(result.Message, "友情提示", MessageBoxButton.OK, MessageBoxImage.Information);
                DialogResult = false;
            }
            else
            {
                var relationships = _staffRoleRelationshipsManage.GetRelationships(_staffId);

                _operatorEditModel.StaffId = result.Data.Id;
                _operatorEditModel.StaffName = result.Data.Name;
                _operatorEditModel.LogonName = result.Data.LogonName;
                _operatorEditModel.LogonNameEnable = !(result.Data.LogonName == "admin" || result.Data.Supper);
                _operatorEditModel.LogonPwd = result.Data.LogonPwd;
                _operatorEditModel.ConfirmLogonPwd = result.Data.LogonPwd;
                _rolesManage.GetAll().ForEach(a =>
                {
                    _operatorEditModel.Roles.Add(new OperatorAddEditModel.RoleModel
                    {
                        Checked = relationships.Any(z => z.RoleId == a.Id),
                        RoleId = a.Id,
                        RoleName = a.RoleName
                    });
                });
            }
        }

        /// <summary>
        /// 确定
        /// </summary>
        private void OperatorAddEdit_OnOk()
        {
            if (_operatorEditModel.Verify())
            {
                if (MessageBox.Show("您确定要修改该操作员信息吗？", "安全提示", MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.OK)
                {
                    var result = _operatorManage.Edit(_staffId, new OperatorEditResponseModel
                    {
                        LogonName = _operatorEditModel.LogonName,
                        LogonPwd = _operatorEditModel.LogonPwd,
                        RoleIdes = _operatorEditModel.Roles.Where(a => a.Checked).Select(a => a.RoleId).ToList()
                    });
                    MessageBox.Show(result.Message, result.ResultStatus == ResultStatus.Success ? "成功提示" : "失败提示", MessageBoxButton.OK, MessageBoxImage.Information);
                    if (result.ResultStatus == ResultStatus.Success)
                    {
                        _callback?.Invoke();
                        DialogResult = true;
                    }
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

        private void OperatorAddEdit_OnSelectStaff(int staffid, string staffname)
        {
            MessageBox.Show("员工姓名不能修改", "友情提示", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
