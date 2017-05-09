using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using DevExpress.Data.Helpers;
using Frame.Proxy;

namespace Frame.SysWindows.MVModels
{
    public class BannerManagerViewModel : ViewModel, IDataErrorInfo
    {
        private string _menuId;
        private string _dllPath;
        private string _entryFunction;
        private bool _enabledYes = true;
        private bool _enabledNo;
        private ObservableCollection<DgDllEntryClass> _dgDllEntries = new ObservableCollection<DgDllEntryClass>();

        public string MenuId
        {
            get { return _menuId; }
            set
            {
                if(_menuId == value)
                    return;
                _menuId = value;
                OnPropertyChanged(nameof(MenuId));
            }
        }

        public string DllPath
        {
            get { return _dllPath; }
            set
            {
                if(_dllPath == value)
                    return;
                _dllPath = value;
                OnPropertyChanged(nameof(DllPath));
            }
        }

        public string EntryFunction
        {
            get { return _entryFunction; }
            set
            {
                if(_entryFunction == value) return;
                _entryFunction = value;
                OnPropertyChanged(nameof(EntryFunction));
            }
        }

        public bool EnabledYes
        {
            get { return _enabledYes; }
            set
            {
                if(_enabledYes == value) return;
                _enabledYes = value;
                OnPropertyChanged(nameof(EnabledYes));

                EnabledNo = !value;
            }
        }

        public bool EnabledNo
        {
            get { return _enabledNo; }
            set
            {
                if(_enabledNo == value) return;
                _enabledNo = value;
                OnPropertyChanged(nameof(EnabledNo));
            }
        }

        public ObservableCollection<DgDllEntryClass> DgDllEntries
        {
            get { return _dgDllEntries; }
            set
            {
                if(_dgDllEntries == value) return;
                _dgDllEntries = value;
                OnPropertyChanged(nameof(DgDllEntries));
            }
        }

        #region 数据完整性验证

        private readonly Dictionary<string, bool> _verifyDictionary = new Dictionary<string, bool>();

        public bool Verify()
        {
            return _verifyDictionary.All(a => !a.Value);
        }

        public string Error => null;

        public string this[string columnName]
        {
            get
            {
                string errMsg = null;
                switch (columnName)
                {
                    case nameof(MenuId):
                        if (MenuId == null || MenuId.Trim().Length == 0)
                            errMsg = "请输入 Banner 唯一标识";
                        break;
                    case nameof(DllPath):
                        if (DllPath == null || DllPath.Trim().Length == 0)
                            errMsg = "请输入 dll 路径";
                        break;
                    case nameof(EntryFunction):
                        if (EntryFunction == null || EntryFunction.Trim().Length == 0)
                            errMsg = "请输入入口函数名称";
                        break;
                }
                _verifyDictionary[columnName] = errMsg != null;
                return errMsg;
            }
        }

        #endregion
    }
}
