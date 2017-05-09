using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Frame.Proxy.Controls
{
    public class TreeViewImgItem : TreeViewItem
    {
        private readonly Image _image = new Image { Stretch = Stretch.Uniform, VerticalAlignment = VerticalAlignment.Center };
        private readonly TextBlock _textBlock = new TextBlock { Margin = new Thickness(3, 0, 0, 0), VerticalAlignment = VerticalAlignment.Center };
        public TreeViewImgItem()
        {
            var dockPanel = new DockPanel();
            dockPanel.Children.Add(_image);
            dockPanel.Children.Add(_textBlock);
            
            Header = dockPanel;

            Expanded += OnExpanded;
            Collapsed += OnCollapsed;
        }

        public ImageSource ExpandedImageSource { get; set; }

        private ImageSource _defaultImageSource;
        public ImageSource DefaultImageSource
        {
            get { return _defaultImageSource; }
            set
            {
                _defaultImageSource = value;
                _image.Source = value;
            }
        }

        public new object Header
        {
            get { return base.Header; }
            private set { base.Header = value; }
        }

        /// <summary>
        /// 显示内容
        /// </summary>
        public string HeaderText
        {
            get { return _textBlock.Text; }
            set { _textBlock.Text = value; }
        }

        /// <summary>
        /// 合并
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="routedEventArgs"></param>
        private void OnCollapsed(object sender, RoutedEventArgs routedEventArgs)
        {
            if (!IsExpanded)
                _image.Source = DefaultImageSource;
        }

        /// <summary>
        /// 展开
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="routedEventArgs"></param>
        private void OnExpanded(object sender, RoutedEventArgs routedEventArgs)
        {
            if (IsExpanded)
                _image.Source = ExpandedImageSource;
        }
    }
}
