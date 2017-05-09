using System;
using System.Windows;
using NLog;

namespace Frame.Utils
{
    public class ExceptionHelper
    {
        /// <summary>
        /// 显示和记录异常
        /// </summary>
        /// <param name="ex"></param>
        public static void ErrFunc(Exception ex)
        {
            var e = Functions.GetLastChildException(ex);
            Application.Current.Dispatcher.Invoke((Action)(() =>
            {
#if DEBUG
                MessageBox.Show(e.Message, "系统异常", MessageBoxButton.OK, MessageBoxImage.Error);
#else
                MessageBox.Show("我们很抱歉，当前应用程序遇到一些问题，该操作已经终止，请进行重试，如果问题继续存在，请联系管理员.", "意外的操作", MessageBoxButton.OK, MessageBoxImage.Information);
#endif
            }));
            ELogger.Debug(e);
        }
    }
    public class ELogger
    {
        private static Logger _logger;

        public static void Debug(Exception ex)
        {
            Logger.Debug(ex);
        }

        public static Logger Logger => _logger ?? LogManager.GetCurrentClassLogger();
    }
}
