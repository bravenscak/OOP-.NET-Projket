using ClassLibrary.Models;
using ClassLibrary.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for PlayerControl.xaml
    /// </summary>
    public partial class PlayerControlView : UserControl
    {
        private const string DEFAULT_ICON = "pack://application:,,,/WpfApp;component/Images/playerDefault.png";
        private PlayerDetailsWindow currentPlayerWindow;

        public static readonly DependencyProperty PlayerNameProperty =
            DependencyProperty.Register("PlayerName", typeof(string), typeof(PlayerControlView), new PropertyMetadata(""));

        public static readonly DependencyProperty ShirtNumberProperty =
            DependencyProperty.Register("ShirtNumber", typeof(long), typeof(PlayerControlView), new PropertyMetadata(0L));

        public static readonly DependencyProperty ImagePathProperty =
            DependencyProperty.Register("ImagePath", typeof(string), typeof(PlayerControlView), new PropertyMetadata(DEFAULT_ICON, OnImagePathChanged));

        public static readonly DependencyProperty CountryProperty =
            DependencyProperty.Register("Country", typeof(Result), typeof(PlayerControlView), new PropertyMetadata(null));

        public string PlayerName
        {
            get { return (string)GetValue(PlayerNameProperty); }
            set { SetValue(PlayerNameProperty, value); }
        }

        public long ShirtNumber
        {
            get { return (long)GetValue(ShirtNumberProperty); }
            set { SetValue(ShirtNumberProperty, value); }
        }

        public string ImagePath
        {
            get { return (string)GetValue(ImagePathProperty); }
            set { SetValue(ImagePathProperty, value); }
        }

        public Result Country
        {
            get { return (Result)GetValue(CountryProperty); }
            set { SetValue(CountryProperty, value); }
        }

        public PlayerControlView()
        {
            InitializeComponent();
            DataContext = this;
            MouseLeftButtonUp += OnPlayerSelected;
        }

        public PlayerControlView(string playerName, long shirtNumber, Result country)
            : this()
        {
            PlayerName = playerName;
            ShirtNumber = shirtNumber;
            Country = country;
        }

        private async void OnPlayerSelected(object sender, MouseButtonEventArgs e)
        {
            try
            {
                loadingProgressBar.Visibility = Visibility.Visible; 

                var parentWindow = Window.GetWindow(this);
                if (parentWindow != null)
                {
                    var playerData = await DataFactory.GetPlayerDataForSelectedCountryAsync(Country.Country);

                    if (playerData.TryGetValue(PlayerName, out var ranking))
                    {
                        if (currentPlayerWindow == null || currentPlayerWindow.tbPlayerName != tbPlayerName)
                        {
                            loadingProgressBar.Visibility = Visibility.Collapsed;
                            OpenPlayerDetailWindow(ranking);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error handling player selection: {ex.Message}");
            }
            finally
            {
                loadingProgressBar.Visibility = Visibility.Collapsed;
            }
        }

        private void OpenPlayerDetailWindow(PlayerRanking ranking)
        {
            currentPlayerWindow?.Close();

            currentPlayerWindow = new PlayerDetailsWindow(PlayerName, ShirtNumber, ImagePath, ranking.Goals, ranking.YellowCards);
            currentPlayerWindow.Closed += PlayerDetailsWindow_Closed;
            currentPlayerWindow.ShowDialog();
        }

        private void PlayerDetailsWindow_Closed(object sender, EventArgs e)
        {
            currentPlayerWindow.Closed -= PlayerDetailsWindow_Closed;
            currentPlayerWindow = null;
        }

        private static void OnImagePathChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is PlayerControlView playerControl)
            {
                try
                {
                    var imagePath = e.NewValue as string;
                    if (string.IsNullOrEmpty(imagePath))
                    {
                        imagePath = DEFAULT_ICON;
                    }

                    var uri = new Uri(imagePath, UriKind.RelativeOrAbsolute);
                    playerControl.imgPlayer.Source = new BitmapImage(uri);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error loading image: {ex.Message}");
                    playerControl.imgPlayer.Source = new BitmapImage(new Uri(DEFAULT_ICON, UriKind.RelativeOrAbsolute));
                }
            }
        }
    }
}
