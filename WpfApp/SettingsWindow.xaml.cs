using ClassLibrary.Interfaces;
using ClassLibrary.Repositories;
using ClassLibrary.Settings;
using ClassLibrary.Language;
using System;
using System.Windows;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public static ISettingsRepo settingsRepo = RepoFactory.GetSettingsRepo();
        private AppSettings currentSettings;
        private Window matchDetailsWindow;
        private readonly ILanguage languageManager = new LanguageManager();

        public event EventHandler SettingsApplied;

        public SettingsWindow(Window matchDetailsWindow)
        {
            InitializeComponent();
            this.matchDetailsWindow = matchDetailsWindow;
            LoadSettingsAsync();
        }

        private async void LoadSettingsAsync()
        {
            try
            {
                currentSettings = await settingsRepo.GetSettingsAsync();
                if (currentSettings.Language == "Hrvatski")
                {
                    SetLanguage("hr");
                }
                else
                {
                    SetLanguage("en");
                }

                cbTournament.SelectedIndex = currentSettings.GenderCategory == "Men" ? 0 : 1;
                cbLanguage.SelectedIndex = currentSettings.Language == "English" ? 0 : 1;
                cbResolution.SelectedValue = currentSettings.Resolution;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading settings: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SetLanguage(string languageCode)
        {
            languageManager.SetLanguage(languageCode);
            UpdateUIStrings();
        }

        private void UpdateUIStrings()
        {
            tbSettings.Text = languageManager.GetLanguageString("tbSettings");
            tbTournament.Text = languageManager.GetLanguageString("tbTournament");
            tbLanguage.Text = languageManager.GetLanguageString("tbLanguage");
            tbResolution.Text = languageManager.GetLanguageString("tbResolution");
            btnApply.Content = languageManager.GetLanguageString("btnApply");
        }

        private async void OnApplyButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                currentSettings.GenderCategory = cbTournament.SelectedIndex == 0 ? "Men" : "Women";
                currentSettings.Language = cbLanguage.SelectedIndex == 0 ? "English" : "Hrvatski";
                currentSettings.Resolution = cbResolution.SelectedValue.ToString();
                string resolution = cbResolution.SelectedValue.ToString();

                if (cbResolution.SelectedValue != null)
                {
                    switch (resolution)
                    {
                        case "Fullscreen":
                            SetFullScreen(true);
                            break;
                        case "r1280x720":
                            SetWindowSize(1280, 720);
                            break;
                        case "r1024x768":
                            SetWindowSize(1024, 768);
                            break;
                        default:
                            SetFullScreen(false);
                            break;
                    }

                    currentSettings.Resolution = resolution;
                }
                await settingsRepo.UpdateSettingsAsync(currentSettings);

                MessageBox.Show("Settings applied successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                SettingsApplied?.Invoke(this, EventArgs.Empty);

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SetWindowSize(int width, int height)
        {
            matchDetailsWindow.WindowState = WindowState.Normal;
            matchDetailsWindow.WindowStyle = WindowStyle.SingleBorderWindow;
            matchDetailsWindow.ResizeMode = ResizeMode.CanResize;
            matchDetailsWindow.Width = width;
            matchDetailsWindow.Height = height;
        }

        private void SetFullScreen(bool isFullScreen)
        {
            if (isFullScreen)
            {
                matchDetailsWindow.WindowState = WindowState.Maximized;
                matchDetailsWindow.WindowStyle = WindowStyle.None;
                matchDetailsWindow.ResizeMode = ResizeMode.NoResize;
            }
            else
            {
                matchDetailsWindow.WindowState = WindowState.Normal;
                matchDetailsWindow.WindowStyle = WindowStyle.SingleBorderWindow;
                matchDetailsWindow.ResizeMode = ResizeMode.CanResize;
            }
        }
    }
}
