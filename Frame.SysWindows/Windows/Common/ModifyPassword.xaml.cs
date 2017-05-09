using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using Frame.Business;
using Frame.Business.interfaces;
using Frame.Models.SysModels;
using Frame.Proxy;

namespace Frame.SysWindows.Windows.Common
{
    /// <summary>
    /// ModifyPassword.xaml 的交互逻辑
    /// </summary>
    public partial class ModifyPassword : INotifyPropertyChanged, IDataErrorInfo
    {
        private readonly IOperatorManage _operatorManage = new OperatorManage();
        private readonly ClsLoginModel _loginModel;
        
        public ModifyPassword(ClsLoginModel loginModel)
        {
            InitializeComponent();
            DataContext = this;
            _loginModel = loginModel;
        }

        /// <summary>
        /// 确认修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnConfirmModify_OnClick(object sender, RoutedEventArgs e)
        {
            if (Verify())
            {
                if (MessageBox.Show("您确定要修改密码吗？", "安全提示", MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.OK)
                {
                    var result = _operatorManage.EditPwd(_loginModel.UserId, OldPwd, NewPwd);
                    MessageBox.Show(result.Message, "友情提示", MessageBoxButton.OK, result.ResultStatus == ResultStatus.Error ? MessageBoxImage.Error : MessageBoxImage.Information);
                    if (result.ResultStatus == ResultStatus.Success)
                        DialogResult = true;
                }
            }
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCancel_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        #region MVVM Models
        public event PropertyChangedEventHandler PropertyChanged;
        private string _oldPwd;
        private string _newPwd;
        private string _confirmNewPwd;

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
                    case nameof(OldPwd):
                        if (OldPwd == null || OldPwd.Trim().Length == 0)
                            errMsg = "请输入原密码";
                        break;
                    case nameof(NewPwd):
                        if (NewPwd == null || NewPwd.Trim().Length == 0)
                            errMsg = "请输入新密码";
                        else if (NewPwd.Trim().Length < 6)
                            errMsg = "密码的长度至少为6位";
                        break;
                    case nameof(ConfirmNewPwd):
                        if (ConfirmNewPwd == null || ConfirmNewPwd.Trim().Length == 0)
                            errMsg = "请确认密码";
                        else if (ConfirmNewPwd != NewPwd)
                            errMsg = "两次密码输入不一致";
                        break;
                }
                _verifyDictionary[columnName] = errMsg != null;
                return errMsg;
            }
        }

        public string Error => null;

        public string OldPwd
        {
            get { return _oldPwd; }
            set
            {
                _oldPwd = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(OldPwd)));
            }
        }

        public string NewPwd
        {
            get { return _newPwd; }
            set
            {
                ConfirmNewPwd = "";
                _newPwd = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NewPwd)));
            }
        }

        public string ConfirmNewPwd
        {
            get { return _confirmNewPwd; }
            set
            {
                _confirmNewPwd = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(ConfirmNewPwd));
            }
        }
        #endregion
    }
}
