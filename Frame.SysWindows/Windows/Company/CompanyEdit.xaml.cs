using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using Frame.Business;
using Frame.Business.interfaces;
using Frame.Models.SysModels;
using Frame.Proxy;
using Frame.Proxy.Enums;
using Frame.Utils;
using Frame.SysWindows.MVModels;

namespace Frame.SysWindows.Windows.Company
{
    /// <summary>
    /// CompanyEdit.xaml 的交互逻辑
    /// </summary>
    public partial class CompanyEdit : INotifyPropertyChanged, IDataErrorInfo
    {
        private readonly ICompanyManage _companyManage = new CompanyManage();
        private readonly ClsLoginModel _clsLoginModel;
        private readonly string _menuId;
        public CompanyEdit(ClsLoginModel loginModel, string menuId)
        {
            InitializeComponent();
            _clsLoginModel = loginModel;
            _menuId = menuId;
            DataContext = this;

            InitCompanyInfo();
        }

        private void InitCompanyInfo()
        {
            var company = _companyManage.GetSettingModel() ?? new Models.SettingModels.Company();
            CompanyName = company.Name;
            RegCode = company.RegCode;
            Telephone = company.Tel;
            Fax = company.Fax;
            Bank = company.Bank;
            BankCode = company.BankCode;
            TaxCode = company.TaxCode;
            Mail = company.Mail;
            Copyright = company.Copyright;
            Address = company.Add;
            Remark = company.Remark;
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCancel_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnOk_OnClick(object sender, RoutedEventArgs e)
        {
            if (!_clsLoginModel.PermissionsFunc(_menuId, (int) PermissionsEnum.修改))
                MessageBox.Show("您没有修改公司信息的权限", "友情提示", MessageBoxButton.OK, MessageBoxImage.Information);
            else if (Verify())
            {
                var result = _companyManage.AddOrUpdate(new Models.SettingModels.Company
                {
                    Name = CompanyName,
                    RegCode = RegCode,
                    Tel = Telephone,
                    Fax = Fax,
                    Bank = Bank,
                    BankCode = BankCode,
                    TaxCode = TaxCode,
                    Mail = Mail,
                    Copyright = Copyright,
                    Add = Address,
                    Remark = Remark,
                    OperMan = _clsLoginModel.LoginName
                });

                if (result.ResultStatus == ResultStatus.Success)
                {
                    var configDirectory = $"{AppDomain.CurrentDomain.BaseDirectory}{Config.ConfigDirectory}";
                    var path = $"{configDirectory}{Config.SplashScreenDataFile}";
                    if (!Directory.Exists(configDirectory))
                        Directory.CreateDirectory(configDirectory);

                    var data = new SplashScreenViewModel
                    {
                        CompanyName = result.Data.Name,
                        Copyright = result.Data.Copyright
                    };
                    File.WriteAllText(path, data.ToJson(), Encoding.UTF8);
                }
                MessageBox.Show(result.ResultStatus == ResultStatus.Success ? "公司信息修改成功" : result.Message, "友情提示", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        #region MVVM Models
        public event PropertyChangedEventHandler PropertyChanged;
        private string _companyName;
        private string _regCode;
        private string _telephone;
        private string _fax;
        private string _bank;
        private string _bankCode;
        private string _taxCode;
        private string _mail;
        private string _copyright;
        private string _address;
        private string _remark;

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
                    case nameof(CompanyName):
                        return VerifyColumnName(columnName, CompanyName, "请输入公司名称");
                    case nameof(Copyright):
                        return VerifyColumnName(columnName, Copyright, "请输入版权信息");
                    default:
                        return null;
                }
            }
        }

        public string Error => null;

        private string VerifyColumnName(string columnName, string value, string errMsg)
        {
            var error = !(value != null && value.Trim().Length != 0);

            _verifyDictionary[columnName] = error;
            return error ? errMsg : null;
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

        public string RegCode
        {
            get { return _regCode; }
            set
            {
                _regCode = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(RegCode)));
            }
        }

        public string Telephone
        {
            get { return _telephone; }
            set
            {
                _telephone = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Telephone)));
            }
        }

        public string Fax
        {
            get { return _fax; }
            set
            {
                _fax = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Fax)));
            }
        }

        public string Bank
        {
            get { return _bank; }
            set
            {
                _bank = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Bank)));
            }
        }

        public string BankCode
        {
            get { return _bankCode; }
            set
            {
                _bankCode = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(BankCode)));
            }
        }

        public string TaxCode
        {
            get { return _taxCode; }
            set
            {
                _taxCode = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TaxCode)));
            }
        }

        public string Mail
        {
            get { return _mail; }
            set
            {
                _mail = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Mail)));
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

        public string Address
        {
            get { return _address; }
            set
            {
                _address = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Address)));
            }
        }

        public string Remark
        {
            get { return _remark; }
            set
            {
                _remark = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Remark)));
            }
        }
        #endregion
    }
}
