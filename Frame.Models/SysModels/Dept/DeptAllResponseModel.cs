using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Frame.Models.SysModels.Dept
{
    public class DeptAllResponseModel : INotifyPropertyChanged, IDataErrorInfo
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private int _deptId;
        private string _deptName;
        private int _parentId;
        private ObservableCollection<DeptAllResponseModel> _nodes;

        public int DeptId
        {
            get
            {
                return _deptId;
            }
            set
            {
                _deptId = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DeptId)));
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

        public int ParentId
        {
            get
            {
                return _parentId;
            }
            set
            {
                _parentId = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ParentId)));
            }
        }

        public ObservableCollection<DeptAllResponseModel> Nodes
        {
            get { return _nodes; }
            set
            {
                _nodes = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Nodes)));
            }
        }
        public string Error => null;

        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case nameof(DeptName):
                        return DeptName != null && DeptName.Trim().Length > 0 ? null : "请输入或选择部门";
                }
                return null;
            }
        }

        public override string ToString()
        {
            return DeptName;}
    }
}
