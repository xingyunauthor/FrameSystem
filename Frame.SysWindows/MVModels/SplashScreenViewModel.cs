using System.ComponentModel;

namespace Frame.SysWindows.MVModels
{
    public class SplashScreenViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _loadingValue;
        private string _companyName;
        private string _copyright;

        public string LoadingValue
        {
            get { return _loadingValue; }
            set
            {
                _loadingValue = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(LoadingValue)));
            }
        }

        public string CompanyName
        {
            get { return _companyName; }
            set
            {
                _companyName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CompanyName)));
            }
        }

        public string Copyright
        {
            get { return _copyright; }
            set
            {
                _copyright = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Copyright)));
            }
        }
    }
}
