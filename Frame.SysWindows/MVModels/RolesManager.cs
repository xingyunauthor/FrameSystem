using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Frame.SysWindows.MVModels
{
    public class RolesModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private int _rowId;
        private int _id;
        private string _roleName;
        private long _timestamp;

        /// <summary>
        /// 行号
        /// </summary>
        public int RowId {
            get { return _rowId; }
            set
            {
                _rowId = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(RowId)));
            }
        }
        /// <summary>
        /// 角色编号
        /// </summary>
        public int Id {
            get { return _id; }
            set
            {
                _id = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Id)));
            }
        }

        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName {
            get { return _roleName; }
            set
            {
                _roleName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(RoleName)));
            }
        }

        /// <summary>
        /// 时间戳
        /// </summary>
        public long Timestamp {
            get { return _timestamp; }
            set
            {
                _timestamp = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Timestamp)));
            }
        }
    }


    public class PermissionsModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private int _rowId;
        private int _permissionsId;
        private string _permissionsName;
        private int _sort;

        /// <summary>
        /// 行号
        /// </summary>
        public int RowId {
            get { return _rowId; }
            set
            {
                _rowId = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(RowId)));
            }
        }

        /// <summary>
        /// 权限编号
        /// </summary>
        public int PermissionsId {
            get { return _permissionsId; }
            set
            {
                _permissionsId = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PermissionsId)));
            }
        }

        /// <summary>
        /// 权限名称
        /// </summary>
        public string PermissionsName { get { return _permissionsName; }
            set
            {
                _permissionsName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PermissionsName)));
            }
        }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort {
            get { return _sort; }
            set
            {
                _sort = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Sort)));
            }
        }

    }
}
