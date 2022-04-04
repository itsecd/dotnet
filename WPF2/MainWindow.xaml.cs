using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;

namespace WPF2
{
    public partial class MainWindow : Window
    {
        public MainWindow() => InitializeComponent();

        private void OnCreditsLoaded(object sender, RoutedEventArgs e)
        {
            Credits.MaxHeight = Credits.ActualHeight;
        }

        private void ShowCredits(object sender, RoutedEventArgs e)
        {
            try
            {
                var uri = new Uri("pack://application:,,,/Assets/Credits.txt", UriKind.Absolute);

                using var stream = Application.GetResourceStream(uri).Stream;
                if (stream is null)
                    return;

                using var reader = new StreamReader(stream);
                Credits.Text = reader.ReadToEnd();
            }
            catch
            {
                // Ignore
            }
        }

        private void ClearSelection(object sender, RoutedEventArgs e)
        {
            IntegerList.SelectedIndex = -1;
        }

        private void RemoveEven(object sender, RoutedEventArgs e)
        {
            if (DataContext is not ObservableCollection<int> model)
                return;

            model.RemoveAt(IntegerList.SelectedIndex);
        }
    }
}
