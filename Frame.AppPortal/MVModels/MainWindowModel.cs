using System.ComponentModel;

namespace Frame.AppPortal.MVModels
{
    public class MainWindowModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string _companyName;
        private string _copyright;
        private string _logonName;

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

        public string LogonName
        {
            get { return _logonName; }
            set
            {
                _logonName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(LogonName)));
            }
        }
    }
}
