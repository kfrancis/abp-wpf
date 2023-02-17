using System.Windows;
using Wpf.Ui.Appearance;

namespace Acme.BookStore.WpfApp
{
    /// <summary>
    /// Interaction logic for SplashWindow.xaml
    /// </summary>
    public partial class SplashWindow : Window
    {
        public SplashWindow()
        {
            Theme.Apply(
             ThemeType.Dark,     // Theme type
              BackgroundType.Mica, // Background type
              true                                  // Whether to change accents automatically
            );

            InitializeComponent();
        }
    }
}
