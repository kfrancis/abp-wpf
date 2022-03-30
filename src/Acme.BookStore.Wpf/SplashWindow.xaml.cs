using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Acme.BookStore.Wpf
{
    /// <summary>
    /// Interaction logic for SplashWindow.xaml
    /// </summary>
    public partial class SplashWindow : Window
    {
        public SplashWindow()
        {
            WPFUI.Appearance.Theme.Set(
              WPFUI.Appearance.ThemeType.Dark,     // Theme type
              WPFUI.Appearance.BackgroundType.Mica, // Background type
              true                                  // Whether to change accents automatically
            );

            InitializeComponent();
        }
    }
}
