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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Cryptex
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // Beaufort Page Button
        private void BeaufortBtn_Click(object sender, RoutedEventArgs e)
        {
            displayWindowFrame.Source = new Uri("BeaufortPage.xaml", UriKind.Relative);
        }

        // Bifid Page Button
        private void BifidBtn_Click(object sender, RoutedEventArgs e)
        {
            displayWindowFrame.Source = new Uri("BifidPage.xaml", UriKind.Relative);
        }

        // Home Button
        private void HomeBtn_Click(object sender, RoutedEventArgs e)
        {
            displayWindowFrame.Source = new Uri("Home.xaml", UriKind.Relative);
        }

        // Homophonic Page Button
        private void HomophonicBtn_Click(object sender, RoutedEventArgs e)
        {
            displayWindowFrame.Source = new Uri("HomophonicPage.xaml", UriKind.Relative);
        }

        // Porta Page Button
        private void PortaBtn_Click(object sender, RoutedEventArgs e)
        {
            displayWindowFrame.Source = new Uri("PortaPage.xaml", UriKind.Relative);
        }

        // Straddle Checkerboard Page Button
        private void StaddleBtn_Click(object sender, RoutedEventArgs e)
        {
            displayWindowFrame.Source = new Uri("StraddleCheckerBoardPage.xaml", UriKind.Relative);
        }
    }
}
