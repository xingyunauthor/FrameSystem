using System.ComponentModel;

namespace Frame.Models.SysModels.Staff
{
    public class StaffAllResponseModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private int _rowId;
        private int _staffId;
        private string _code;
        private int _deptId;
        private string _deptName;
        private string _staffName;
        private string _sex;
        private string _birthday;
        private string _inTime;
        private string _telephone;
        private string _address;
        private bool _state;
        private string _stateName;
        private string _remark;

        /// <summary>
        /// 行号
        /// </summary>
        public int RowId
        {
            get { return _rowId; }
            set
            {
                _rowId = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(RowId)));
            }
        }

        /// <summary>
        /// 员工编号
        /// </summary>
        public int StaffId
        {
            get { return _staffId; }
            set
            {
                _staffId = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(StaffId)));
            }
        }

        /// <summary>
        /// 员工编号
        /// </summary>
        public string Code
        {
            get { return _code; }
            set
            {
                _code = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Code)));
            }
        }

        /// <summary>
        /// 部门编号
        /// </summary>
        public int DeptId
        {
            get { return _deptId; }
            set
            {
                _deptId = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DeptId)));
            }
        }

        /// <summary>
        /// 部门名称
        /// </summary>
        public string DeptName
        {
            get { return _deptName; }
            set
            {
                _deptName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DeptName)));
            }
        }

        /// <summary>
        /// 员工姓名
        /// </summary>
        public string StaffName
        {
            get { return _staffName; }
            set
            {
                _staffName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(StaffName)));
            }
        }

        /// <summary>
        /// 性别
        /// </summary>
        public string Sex
        {
            get { return _sex; }
            set
            {
                _sex = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Sex)));
            }
        }

        /// <summary>
        /// 生日
        /// </summary>
        public string Birthday
        {
            get { return _birthday; }
            set
            {
                _birthday = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Birthday)));
            }
        }

        /// <summary>
        /// 入职日期
        /// </summary>
        public string InTime
        {
            get { return _inTime; }
            set
            {
                _inTime = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(InTime)));
            }
        }

        /// <summary>
        /// 联系方式
        /// </summary>
        public string Telephone
        {
            get { return _telephone; }
            set
            {
                _telephone = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Telephone)));
            }
        }

        /// <summary>
        /// 联系地址
        /// </summary>
        public string Address
        {
            get { return _address; }
            set
            {
                _address = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Address)));
            }
        }

        /// <summary>
        /// 启用状态
        /// </summary>
        public bool State
        {
            get { return _state; }
            set
            {
                _state = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(State)));
            }
        }

        /// <summary>
        /// 启用状态
        /// </summary>
        public string StateName
        {
            get { return _stateName; }
            set
            {
                _stateName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(StateName)));
            }
        }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            get { return _remark; }
            set
            {
                _remark = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Remark)));
            }
        }
    }
}
