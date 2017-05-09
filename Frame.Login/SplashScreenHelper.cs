using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using Frame.Models.SysModels.Common;
using Frame.SysWindows.MVModels;
using Frame.Utils;

namespace Frame.Login
{
    public class SplashScreenHelper
    {
        private static readonly List<string> LoadingValueList = new List<string>();
        private static readonly CancellationTokenSource Cancellation = new CancellationTokenSource();
        private static Window SplashScreenWindow { get; set; }
        public static void Show<T>() where T : Window, new()
        {
            var thread = new Thread(() =>
            {
                try
                {
                    var screenViewModel = new SplashScreenViewModel();
                    Task.Factory.StartNew(() =>
                    {
                        var data = GetSplashScreenData();
                        screenViewModel.CompanyName = data.CompanyName;
                        screenViewModel.Copyright = data.Copyright;
                    });

                    Task.Factory.StartNew(() =>
                    {
                        try
                        {
                            while (true)
                            {
                                if (Cancellation.IsCancellationRequested)
                                    break;
                                if (LoadingValueList.Count > 6)
                                    LoadingValueList.Clear();
                                screenViewModel.LoadingValue = string.Join("", LoadingValueList);
                                LoadingValueList.Add(".");

                                Thread.Sleep(350);
                            }
                        }
                        catch (Exception ex)
                        {
                            SplashScreenWindow.Dispatcher.Invoke((Action) (() => { throw ex; }));
                        }
                    });

                    SplashScreenWindow = new T
                    {
                        WindowStyle = WindowStyle.None,
                        ResizeMode = ResizeMode.NoResize,
                        AllowsTransparency = true,
                        Background = new SolidColorBrush(Colors.Transparent),
                        ShowInTaskbar = false,
                        Topmost = true,
                        SizeToContent = SizeToContent.WidthAndHeight,
                        WindowStartupLocation = WindowStartupLocation.CenterScreen,
                        DataContext = screenViewModel
                    };
                    SplashScreenWindow.ShowDialog();
                }
                catch (Exception ex)
                {
                    SplashScreenWindow.Dispatcher.Invoke((Action) (() => { throw ex; }));
                }
            }) {IsBackground = true};
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }


        public static void Close()
        {
            SplashScreenWindow?.Dispatcher?.Invoke((Action) (() =>
            {
                Cancellation.Cancel();
                SplashScreenWindow?.Close();
            }));
        }

        private static SplashScreenDataModel GetSplashScreenData()
        {
            var data = new SplashScreenDataModel();
            var path = $"{AppDomain.CurrentDomain.BaseDirectory}{Config.ConfigDirectory}{Config.SplashScreenDataFile}";
            if (File.Exists(path))
            {
                using (var reader = new StreamReader(path, Encoding.UTF8))
                {
                    data = reader.ReadToEnd().ToObject<SplashScreenDataModel>();
                }
            }
            return data;
        }
    }
}
