using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Frame.Business;
using Frame.Login.MVModels;
using Frame.Utils;
using Frame.Business.interfaces;
using System.Windows.Markup;
using Frame.Models.SysModels;
using Frame.Models.SysModels.Log;
using Frame.Models.SysModels.Operator;

namespace Frame.Login
{
    /// <summary>
    /// Login.xaml 的交互逻辑
    /// </summary>
    public partial class Login : INotifyPropertyChanged, IDataErrorInfo
    {
        private readonly IRolesManage _rolesManage = new RolesManage();
        private readonly IOperatorManage _operatorManage = new OperatorManage();
        private readonly ILogManage _logManage = new LogManage();

        public delegate void LogonSuccessEventHandler(int userId, string logonName, Role role);

        public event LogonSuccessEventHandler LogonSuccessEvent;
        public event Action CancelLogonEvent;

        public Login()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void Login_OnLoaded(object sender, RoutedEventArgs e)
        {
            try
            {
                //CloseSplashScreen();
                LoadTheme();

                if(TestDatabaseConnect())
                {
                    LoadData();
                    Activate();
                }
            }
            catch (Exception ex)
            {
                ErrorMessageBoxShow(ex);
            }
        }

        private bool TestDatabaseConnect()
        {
            try
            {
                var serverTime = _rolesManage.ServerTime;
            }
            catch
            {
                MessageBox.Show("数据库连接失败，请检查相关配置是否正确！", "友情提示", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            return true;
        }

        /// <summary>
        /// 加载主题
        /// </summary>
        private void LoadTheme()
        {
            var reader =
                new StreamReader($"{AppDomain.CurrentDomain.BaseDirectory}LogonComponents\\default\\default.xaml",
                    Encoding.UTF8);
            var val = reader.ReadToEnd()
                .Replace(" Source=\"", $" Source=\"{AppDomain.CurrentDomain.BaseDirectory}LogonComponents\\default\\");
            reader.Dispose();
            var bytes = Encoding.UTF8.GetBytes(val);
            var stream = new MemoryStream(bytes);

            var grid = XamlReader.Load(stream) as Grid;
            Content = grid;
            stream.Dispose();
        }

        private void LoadData()
        {
            try
            {
                Roles = _rolesManage.GetAll().Select(a => new Role
                {
                    RoleId = a.Id,
                    RoleName = a.RoleName
                }).ToList();
            }
            catch (Exception ex)
            {
                ErrorMessageBoxShow(ex);
            }
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        private void UserLogon()
        {
            if (Verify())
            {
                var result = _operatorManage.Login(new OperatorLogonRequestModel
                {
                    LogonName = LogonName,
                    LogonPwd = LogonPwd,
                    RoleId = RoleModel.RoleId});
                if (result.ResultStatus == ResultStatus.Error)
                    MessageBox.Show(result.Message, "友情提示", MessageBoxButton.OK, MessageBoxImage.Information);
                else
                {
                    UserId = result.Data.Id;
                    //登陆日志记录
                    _logManage.Add(new LogAddRequestModel
                    {
                        LoginName = LogonName,
                        LoginTime = DateTime.Now,
                        LoginRole = RoleModel.RoleName,
                        LoginMach = Dns.GetHostName(),
                        LoginCpu = SystemInfo.GetCpuId()
                    });
                    DialogResult = true;
                }
            }
        }

        /// <summary>
        /// 取消登录
        /// </summary>
        private void CancelLogon()
        {
            DialogResult = false;
        }
        
        /// <summary>
        /// 移动窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Login_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Login_OnClosing(object sender, CancelEventArgs e)
        {
            if (DialogResult == true)
                LogonSuccessEvent?.Invoke(UserId, LogonName, RoleModel);
            else
                CancelLogonEvent?.Invoke();
        }

        private void Login_OnClosed(object sender, EventArgs e)
        {
            if(DialogResult != true)
                Application.Current?.Shutdown();
        }

        /// <summary>
        /// 关闭 SplashScreen
        /// </summary>
        private void CloseSplashScreen()
        {
            try
            {
                SplashScreenHelper.Close();
            }
            catch (Exception ex)
            {
                ExceptionHelper.ErrFunc(ex);
            }
        }
        
        public void ErrorMessageBoxShow(Exception ex)
        {
            var e = Functions.GetLastChildException(ex);
            Dispatcher.Invoke((Action)(() =>
            {
                MessageBox.Show(e.Message, "友情提示", MessageBoxButton.OK, MessageBoxImage.Error);
            }));
            ELogger.Debug(e);
        }

        #region MVVM Models

        public event PropertyChangedEventHandler PropertyChanged;

        private int _userId;
        private string _logonName;
        private string _logonPwd;
        private Role _roleModel;
        private List<Role> _roles;
        private RelayCommand _logonCmd;
        private RelayCommand _cancelLogonCmd;

        public int UserId
        {
            get { return _userId; }
            set
            {
                _userId = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(UserId)));
            }
        }

        public string LogonName
        {
            get { return _logonName; }
            set
            {
                _logonName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(LogonName)));
                LogonPwd = "";
            }
        }

        public string LogonPwd
        {
            get { return _logonPwd; }
            set
            {
                _logonPwd = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(LogonPwd)));
            }
        }

        public List<Role> Roles
        {
            get { return _roles; }
            set
            {
                _roles = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Roles)));
            }
        }

        public Role RoleModel
        {
            get { return _roleModel; }
            set
            {
                _roleModel = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(RoleModel)));
            }
        }

        public ICommand LogonCmd
        {
            get { return _logonCmd ?? (_logonCmd = new RelayCommand(a => UserLogon())); }
        }

        public ICommand CancelLogonCmd
        {
            get { return _cancelLogonCmd ?? (_cancelLogonCmd = new RelayCommand(a => CancelLogon())); }
        }

        #region 空验证
        private readonly Dictionary<string, bool> _verifyDictionary = new Dictionary<string, bool>();

        public bool Verify()
        {
            return _verifyDictionary.All(a => !a.Value);
        }

        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case nameof(LogonName):
                        return VerifyColumnName(columnName, LogonName, "请输入登陆名称");
                    case nameof(LogonPwd):
                        return VerifyColumnName(columnName, LogonPwd, "请输入登陆密码");
                    case nameof(RoleModel):
                        if (RoleModel == null)
                        {
                            _verifyDictionary[columnName] = true;
                            return "请选择登陆角色";
                        }
                        _verifyDictionary[columnName] = false;
                        return null;
                }
                return null;
            }
        }

        public string Error => null;
        
        private string VerifyColumnName(string columnName, string value, string errMsg)
        {
            var error = !(value != null && value.Trim().Length != 0);

            _verifyDictionary[columnName] = error;
            return error ? errMsg : null;
        }
        #endregion

        #endregion
    }
}
