using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using Frame.Business;
using Frame.Business.interfaces;
using Frame.Models.SysModels.Dept;

namespace Frame.SysWindows.MVModels
{
    public class StaffAddEdit : INotifyPropertyChanged, IDataErrorInfo
    {
        private readonly IStaffManage _staffManage = new StaffManage();
        public event PropertyChangedEventHandler PropertyChanged;

        private bool _codeReadonly;
        private bool _autoCode;
        private string _code;
        private string _staffName;
        private bool _sexMale;
        private bool _sexFemale;
        private string _deptName;
        private DeptAllResponseModel _dept;
        private ObservableCollection<DeptAllResponseModel> _deptAll;
        private string _inTime;
        private string _birthday;
        private string _telephone;
        private bool _enable;
        private bool _unEnable;
        private string _address;
        private string _remark;
        private bool _continueCheck;

        public StaffAddEdit()
        {
            AutoCode = true;
            InTime = DateTime.Now.ToString(CultureInfo.InvariantCulture);
            SexMale = true;
            Enable = true;
        }

        public bool CodeReadonly
        {
            get { return _codeReadonly; }
            set
            {
                _codeReadonly = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CodeReadonly)));
            }
        }

        public bool AutoCode
        {
            get { return _autoCode; }
            set
            {
                _autoCode = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AutoCode)));

                if (value)
                    Code = _staffManage.GetNewCode();
                CodeReadonly = value;
            }
        }

        public string Code
        {
            get { return _code; }
            set
            {
                _code = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Code)));
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

        public bool SexMale
        {
            get { return _sexMale; }
            set
            {
                _sexMale = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SexMale)));
            }
        }

        public bool SexFemale
        {
            get { return _sexFemale; }
            set
            {
                _sexFemale = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SexFemale)));
            }
        }

        public string DeptName
        {
            get { return _deptName; }
            set
            {
                _deptName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DeptName)));
            }
        }

        public DeptAllResponseModel Dept
        {
            get { return _dept; }
            set
            {
                _dept = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Dept)));
            }
        }

        public ObservableCollection<DeptAllResponseModel> DeptAll
        {
            get { return _deptAll; }
            set
            {
                _deptAll = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DeptAll)));
            }
        }

        public string InTime
        {
            get { return _inTime; }
            set
            {
                _inTime = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(InTime)));
            }
        }

        public string Birthday
        {
            get { return _birthday; }
            set
            {
                _birthday = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Birthday)));
            }
        }

        public string Telephone
        {
            get { return _telephone; }
            set
            {
                _telephone = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Telephone)));
            }
        }

        public bool Enable
        {
            get { return _enable; }
            set
            {
                _enable = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Enable)));
            }
        }

        public bool UnEnable
        {
            get { return _unEnable; }
            set
            {
                _unEnable = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(UnEnable)));
            }
        }

        public string Address
        {
            get { return _address; }
            set
            {
                _address = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Address)));
            }
        }

        public string Remark
        {
            get { return _remark; }
            set
            {
                _remark = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Remark)));
            }
        }

        public bool ContinueCheck
        {
            get { return _continueCheck; }
            set
            {
                _continueCheck = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ContinueCheck)));
            }
        }

        #region 验证
        private readonly Dictionary<string, bool> _verifyDictionary = new Dictionary<string, bool>();

        /// <summary>
        /// 验证
        /// </summary>
        /// <returns></returns>
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
                    case nameof(Code):
                        return VerifyColumnName(Code, columnName, "请输入员工编号");
                    case nameof(StaffName):
                        return VerifyColumnName(StaffName, columnName, "请输入员工姓名");
                    case nameof(DeptName):
                        return VerifyColumnName(DeptName, columnName, "请选择部门");
                    case nameof(InTime):
                        return VerifyColumnName(InTime, columnName, "请选择入职日期");
                    case nameof(Birthday):
                        return VerifyColumnName(Birthday, columnName, "请选择出生日期");
                    case nameof(Telephone):
                        return VerifyColumnName(Telephone, columnName, "请输入联系方式");
                }
                return null;
            }
        }

        private string VerifyColumnName(string value, string columnName, string errMsg)
        {
            var error = value == null || value.Trim().Length == 0;
            _verifyDictionary[columnName] = error;
            return error ? errMsg : null;
        }

        #endregion
    }
}
