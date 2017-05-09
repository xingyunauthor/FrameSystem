using System.ComponentModel;

namespace Frame.Login.MVModels
{
    public class Role : INotifyPropertyChanged
    {
        private int _roleId;
        private string _roleName;

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

        public override string ToString()
        {
            return RoleName;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
