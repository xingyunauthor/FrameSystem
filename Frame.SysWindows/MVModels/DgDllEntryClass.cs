using System;
using System.ComponentModel;

namespace Frame.SysWindows.MVModels
{
    public class DgDllEntryClass : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// 选择发生变化后发生
        /// </summary>
        public event Action<DgDllEntryClass> CheckChanged;

        private bool _isChecked;
        private string _fullName;

        public int Id { get; set; }

        /// <summary>
        /// 是否选择
        /// </summary>
        public bool IsChecked
        {
            get { return _isChecked; }
            set
            {
                _isChecked = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsChecked)));
                CheckChanged?.Invoke(this);
            }
        }

        /// <summary>
        /// Dll 类名完整路径
        /// </summary>
        public string FullName
        {
            get { return _fullName; }
            set
            {
                _fullName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FullName)));
            }
        }
    }
}
