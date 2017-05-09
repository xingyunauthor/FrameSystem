using System.Windows;
using Frame.SysWindows.MVModels;

namespace Frame.SysWindows.Windows.Common
{
    /// <summary>
    /// DebugFeedback.xaml 的交互逻辑
    /// </summary>
    public partial class BugFeedback
    {
        private readonly BugFeedbackViewModel _bugFeedbackViewModel;
        public BugFeedback()
        {
            InitializeComponent();
            _bugFeedbackViewModel = new BugFeedbackViewModel();
            DataContext = _bugFeedbackViewModel;
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
        /// 提交反馈
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnConfirm_OnClick(object sender, RoutedEventArgs e)
        {
            if (_bugFeedbackViewModel.Verify())
            {
                MessageBox.Show(_bugFeedbackViewModel.Content);
            }
        }
    }
}
