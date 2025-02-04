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

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for PlayerDetails.xaml
    /// </summary>
    public partial class PlayerDetailsWindow : Window
    {
        private const string DEFAULT_ICON = "/Images/playerIcon.png";

        public PlayerDetailsWindow(string playerName, long shirtNumber, string imagePath, int goalsScored, int yellowCards)
        {
            InitializeComponent();

            if (string.IsNullOrEmpty(imagePath))
            {
                imagePath = DEFAULT_ICON;
            }
            imgPlayer.Source = new BitmapImage(new Uri(imagePath, UriKind.RelativeOrAbsolute));
            tbPlayerName.Text = $"{playerName}";
            tbShirtNumber.Text = $"Shirt Number: {shirtNumber}";
            tbGoalsScored.Text = $"Goals Scored: {goalsScored}";
            tbYellowCards.Text = $"Yellow Cards: {yellowCards}";
        }
    }
}
