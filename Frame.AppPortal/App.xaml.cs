using System;
using System.Windows;
using System.Windows.Threading;
using Frame.Utils;
using MahApps.Metro;

namespace Frame.AppPortal
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App
    {
        public App()
        {
            /**
             * 全局异常捕捉
             **/
            AppDomain.CurrentDomain.UnhandledException += (sender, args) =>
            {
                try
                {
                    var exception = sender as Exception;
                    if (exception != null)
                        ExceptionHelper.ErrFunc(exception);
                }
                catch (Exception ex)
                {
                    ExceptionHelper.ErrFunc(ex);
                }
            };
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            var theme = ThemeManager.DetectAppStyle(Current);
            var accent = ThemeManager.GetAccent("Steel");
            ThemeManager.ChangeAppStyle(Current, accent, theme.Item1);

            try
            {
                //SplashScreenHelper.Show<FrameSplashScreen.SplashScreenView>();
            }
            catch (Exception ex)
            {
                ExceptionHelper.ErrFunc(ex);
            }

            base.OnStartup(e);
        }

        /// <summary>
        /// 主题设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnAppStartup_UpdateThemeName(object sender, StartupEventArgs e)
        {
            DevExpress.Xpf.Core.ApplicationThemeHelper.UpdateApplicationThemeName();
        }

        /// <summary>
        /// 全局捕获异常
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void App_OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            try
            {
                ExceptionHelper.ErrFunc(e.Exception);
                e.Handled = true;
            }
            catch (Exception ex)
            {
                ExceptionHelper.ErrFunc(ex);
            }
        }
    }
}
