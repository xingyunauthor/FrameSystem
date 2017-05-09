using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Frame.SysWindows.MVModels
{
    public class OperatorAddEditModel : INotifyPropertyChanged, IDataErrorInfo
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private int _staffId;
        private string _staffName;
        private string _logonName;
        private bool _logonNameEnable = true;
        private string _logonPwd;
        private string _confirmLogonPwd;
        private List<RoleModel> _roles;

        public OperatorAddEditModel()
        {
            Roles = new List<RoleModel>();
        }

        public int StaffId
        {
            get { return _staffId; }
            set
            {
                _staffId = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(StaffId)));
            }
        }

        public string StaffName
        {
            get { return _staffName; }
            set
            {
                _staffName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(StaffName)));
            }
        }

        public string LogonName
        {
            get { return _logonName; }
            set
            {
                _logonName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(LogonName)));
            }
        }

        public bool LogonNameEnable
        {
            get { return _logonNameEnable; }
            set
            {
                if(_logonNameEnable == value) return;
                _logonNameEnable = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(LogonNameEnable)));
            }
        }

        public string LogonPwd
        {
            get { return _logonPwd; }
            set
            {
                _logonPwd = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(LogonPwd)));

                if (ConfirmLogonPwd != null && ConfirmLogonPwd.Trim().Length != 0)
                    ConfirmLogonPwd = "";
            }
        }

        public string ConfirmLogonPwd
        {
            get { return _confirmLogonPwd; }
            set
            {
                _confirmLogonPwd = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ConfirmLogonPwd)));
            }
        }

        public List<RoleModel> Roles
        {
            get { return _roles; }
            set
            {
                _roles = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Roles)));
            }
        }
        
        private readonly Dictionary<string, bool> _verifyDictionary = new Dictionary<string, bool>();

        public bool Verify()
        {
            return _verifyDictionary.All(a => !a.Value);
        }

        public string Error => null;

        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case nameof(StaffName):
                        return VerifyColumnName(columnName, StaffName, "请选择员工信息");
                    case nameof(LogonName):
                        return VerifyColumnName(columnName, LogonName, "请输入登录名");
                    case nameof(LogonPwd):
                        return VerifyColumnName(columnName, LogonPwd, "请输入登录密码");
                    case nameof(ConfirmLogonPwd):
                        return ConfirmPwd() ?? VerifyColumnName(columnName, ConfirmLogonPwd, "请确认密码");
                }
                return null;
            }
        }

        private string ConfirmPwd()
        {
            if (ConfirmLogonPwd != null && ConfirmLogonPwd.Trim().Length != 0 &&
                LogonPwd != null && LogonPwd.Trim().Length != 0 &&
                LogonPwd != ConfirmLogonPwd)
            {
                _verifyDictionary[nameof(ConfirmLogonPwd)] = true;
                return "两次密码输入不一致";
            }
            _verifyDictionary[nameof(ConfirmLogonPwd)] = false;
            return null;
        }

        private string VerifyColumnName(string columnName, string value, string errMsg)
        {
            var error = !(value != null && value.Trim().Length != 0);

            _verifyDictionary[columnName] = error;
            return error ? errMsg : null;
        }

        public class RoleModel : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler PropertyChanged;

            private bool _checked;
            private int _roleId;
            private string _roleName;

            public bool Checked
            {
                get { return _checked; }
                set
                {
                    _checked = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Checked)));
                }
            }

            public int RoleId
            {
                get { return _roleId; }
                set
                {
                    _roleId = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(RoleId)));
                }
            }

            public string RoleName
            {
                get { return _roleName; }
                set
                {
                    _roleName = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(RoleName)));
                }
            }
        }
    }
}
