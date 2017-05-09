using Frame.Proxy;

namespace Frame.SysWindows.MVModels
{
    public class RolesManagerViewModel : ViewModel
    {
        private string _leftMenuNameSearchKey;
        public string LeftMenuNameSearchKey
        {
            get { return _leftMenuNameSearchKey; }
            set
            {
                if(_leftMenuNameSearchKey == value)
                    return;
                value = value?.Replace("'", "").Replace("delete", "");
                _leftMenuNameSearchKey = value;
                OnPropertyChanged(nameof(LeftMenuNameSearchKey));
            }
        }
    }
}
