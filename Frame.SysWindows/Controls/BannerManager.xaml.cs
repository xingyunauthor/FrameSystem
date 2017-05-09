using System;
using System.Linq;
using System.Reflection;
using System.Windows;
using DevExpress.Mvvm.Native;
using Frame.Business;
using Frame.Business.interfaces;
using Frame.Models.SettingModels;
using Frame.Models.SysModels;
using Frame.Proxy;
using Frame.Proxy.Enums;
using Frame.SysWindows.MVModels;
using Frame.SysWindows.Windows.Common;
using Frame.Utils;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace Frame.SysWindows.Controls
{
    /// <summary>
    /// BannerManager.xaml 的交互逻辑
    /// </summary>
    public partial class BannerManager
    {
        private readonly IBannerManage _bannerManage = new BannerManage();
        private readonly BannerManagerViewModel _bannerManagerViewModel = new BannerManagerViewModel();
        private readonly MetroWindow _metroWindow;
        private readonly ClsLoginModel _clsLoginModel;
        private readonly string _menuId;

        public BannerManager(MetroWindow metroWindow, ClsLoginModel clsLoginModel, string menuId)
        {
            InitializeComponent();
            _metroWindow = metroWindow;
            _clsLoginModel = clsLoginModel;
            _menuId = menuId;
            DataContext = _bannerManagerViewModel;

            InitData();
        }

        private void InitData()
        {
            var model = _bannerManage.GetSettingModel() ?? new Banner();
            _bannerManagerViewModel.MenuId = model.MenuId;
            _bannerManagerViewModel.DllPath = model.DllPath;
            _bannerManagerViewModel.EntryFunction = model.EntryFunction;
            _bannerManagerViewModel.EnabledYes = model.Enabled;
        }

        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnReset_OnClick(object sender, RoutedEventArgs e)
        {
            InitData();
        }

        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnOk_OnClick(object sender, RoutedEventArgs e)
        {
            var setting = new MetroDialogSettings()
            {
                AnimateShow = true,
                AnimateHide = true,
                AffirmativeButtonText = "确定",
                NegativeButtonText = "取消",
                DefaultButtonFocus = MessageDialogResult.Negative
            };

            if (!_clsLoginModel.PermissionsFunc(_menuId, (int) PermissionsEnum.修改))
                _metroWindow.ShowMessageAsync("友情提示", "您没有权限修改 Banner 配置信息");
            else if (_bannerManagerViewModel.Verify())
            {
                _metroWindow.ShowMessageAsync("安全提示", "您确定要修改 Banner 的配置信息吗？", MessageDialogStyle.AffirmativeAndNegative, setting).ContinueWith(a =>
                {
                    if (a.Result == MessageDialogResult.Affirmative)
                    {
                        try
                        {
                            var result = _bannerManage.AddOrUpdate(new Banner
                            {
                                MenuId = _bannerManagerViewModel.MenuId,
                                DllPath = _bannerManagerViewModel.DllPath,
                                EntryFunction = _bannerManagerViewModel.EntryFunction,
                                Enabled = _bannerManagerViewModel.EnabledYes
                            });
                            _metroWindow.Dispatcher.Invoke((Action) (() => {
                                _metroWindow.ShowMessageAsync("友情提示", result.ResultStatus == ResultStatus.Success ? "Banner 配置信息修改成功" : result.Message);
                            }));
                        }
                        catch (Exception ex)
                        {
                            _metroWindow.Dispatcher.Invoke((Action) (() => { throw ex; }));
                        }
                    }
                });
            }
        }

        private void BtnSelectDll_OnClick(object sender, RoutedEventArgs e)
        {
            var dllPathRefer = new DllPathRefer(dllPathSelTreeView =>
            {
                _bannerManagerViewModel.DllPath = dllPathSelTreeView.FullPath.Remove(0, Config.DevPlatformPath.Length);
                var types = Assembly.LoadFile(dllPathSelTreeView.FullPath).GetTypes();

                var id = 1;
                types.ForEach(a =>
                {
                    var dllEntry = new DgDllEntryClass { Id = id++, FullName = a.FullName };
                    dllEntry.CheckChanged += DllEntry_CheckChanged;
                    _bannerManagerViewModel.DgDllEntries.Add(dllEntry);
                });
            })
            {
                Owner = _metroWindow
            };
            dllPathRefer.ShowDialog();
        }

        /// <summary>
        /// 选择项发生变化后
        /// </summary>
        /// <param name="sender"></param>
        private void DllEntry_CheckChanged(DgDllEntryClass sender)
        {
            var dgList = _bannerManagerViewModel.DgDllEntries;
            if (sender.IsChecked)
            {
                dgList.Where(a => a.Id != sender.Id).ToList().ForEach(a => a.IsChecked = false);
                _bannerManagerViewModel.EntryFunction = dgList.Single(a => a.Id == sender.Id).FullName;
            }
            else
                _bannerManagerViewModel.EntryFunction = "";
        }
    }
}
