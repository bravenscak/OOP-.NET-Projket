using ClassLibrary.Models;
using System;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            try
            {
                OpenMatchDetailsView();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load MatchDetailsView: {ex.Message}", "Navigation Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void OpenMatchDetailsView()
        {
            MatchDetailsView matchDetailsView = new MatchDetailsView();
            matchDetailsView.Show();
        }
    }
}
