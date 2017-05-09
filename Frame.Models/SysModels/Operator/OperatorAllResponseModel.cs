using System.ComponentModel;

namespace Frame.Models.SysModels.Operator
{
    public class OperatorAllResponseModel : INotifyPropertyChanged
    {
        private int _id;
        private int _rowId;
        private string _staffName;
        private string _logonName;
        private string _roleName;

        public event PropertyChangedEventHandler PropertyChanged;

        public int Id
        {
            get { return _id; }
            set
            {
                _id = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Id)));
            }
        }

        public int RowId
        {
            get { return _rowId; }
            set
            {
                _rowId = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(RowId)));
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
