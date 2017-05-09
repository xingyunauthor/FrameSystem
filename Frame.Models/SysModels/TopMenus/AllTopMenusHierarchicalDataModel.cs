using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Frame.Models.SysModels.TopMenus
{
    public class AllTopMenusHierarchicalDataModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private int _id;
        private string _displayName;
        private string _dllPath;
        private string _entryFunction;
        private string _menuId;
        private ObservableCollection<AllTopMenusHierarchicalDataModel> _nodes;

        public int Id
        {
            get { return _id; }
            set
            {
                _id = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Id)));
            }
        }

        public string DisplayName
        {
            get { return _displayName; }
            set
            {
                _displayName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DisplayName)));
            }
        }

        public string DllPath
        {
            get { return _dllPath; }
            set
            {
                _dllPath = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DllPath)));
            }
        }

        public string EntryFunction
        {
            get { return _entryFunction; }
            set
            {
                _entryFunction = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(EntryFunction)));
            }
        }

        public string MenuId
        {
            get { return _menuId; }
            set
            {
                _menuId = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(MenuId)));
            }
        }

        public ObservableCollection<AllTopMenusHierarchicalDataModel> Nodes
        {
            get { return _nodes; }
            set
            {
                _nodes = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Nodes)));
            }
        }
    }
}
