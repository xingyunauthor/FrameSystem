using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Frame.Proxy;

namespace Frame.SysWindows.MVModels
{
    public class BugFeedbackViewModel : ViewModel, IDataErrorInfo
    {
        private string _title;
        private string _content;

        public string Title
        {
            get { return _title; }
            set
            {
                if(_title == value)
                    return;
                _title = value;
                OnPropertyChanged(nameof(Title));
            }
        }

        public string Content
        {
            get { return _content; }
            set
            {
                if(_content == value)
                    return;
                _content = value;
                OnPropertyChanged(nameof(Content));
            }
        }

        #region 验证
        private readonly Dictionary<string, bool> _verifyDictionary = new Dictionary<string, bool>();

        public bool Verify()
        {
            return _verifyDictionary.All(a => !a.Value);
        }

        public string this[string columnName]
        {
            get
            {
                string errMsg = null;
                switch (columnName)
                {
                    case nameof(Title):
                        if (Title == null || Title.Trim().Length == 0)
                            errMsg = "请输入 BUG 的标题";
                        break;
                    case nameof(Content):
                        if (Content == null || Content.Trim().Length == 0)
                            errMsg = "请输入 BUG 的具体内容";
                        break;
                }
                _verifyDictionary[columnName] = errMsg != null;
                return errMsg;
            }
        }

        public string Error => null;
        #endregion
    }
}
