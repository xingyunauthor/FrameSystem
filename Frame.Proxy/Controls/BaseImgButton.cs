using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Frame.Proxy.Controls
{
    public class BaseImgButton : Button
    {
        // ReSharper disable once UnassignedReadonlyField
        public static readonly DependencyProperty ImageSourceProperty= DependencyProperty.Register(nameof(ImageSource), typeof(ImageSource), typeof(BaseImgButton), new PropertyMetadata(default(ImageSource)));

        public static readonly DependencyProperty ImageWidthProperty = DependencyProperty.Register(nameof(ImageWidth), typeof(double), typeof(BaseImgButton), new PropertyMetadata(10.00));

        public static readonly DependencyProperty ImageHeightProperty = DependencyProperty.Register(nameof(ImageHeight), typeof(double), typeof(BaseImgButton), new PropertyMetadata(10.00));

        public ImageSource ImageSource
        {
            get { return (ImageSource)GetValue(ImageSourceProperty); }
            set { SetValue(ImageSourceProperty, value); }
        }

        public double ImageWidth
        {
            get { return (double)GetValue(ImageWidthProperty); }
            set { SetValue(ImageWidthProperty, value); }
        }

        public double ImageHeight
        {
            get { return (double)GetValue(ImageHeightProperty); }
            set { SetValue(ImageHeightProperty, value); }
        }
    }
}
