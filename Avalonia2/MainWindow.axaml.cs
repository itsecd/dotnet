using System.Collections.ObjectModel;
using System.IO;

using Avalonia.Controls;
using Avalonia.Interactivity;

namespace Avalonia2
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ShowCredits(object sender, RoutedEventArgs e)
        {
            if (double.IsPositiveInfinity(Credits.MaxHeight))
                Credits.MaxHeight = Credits.Bounds.Height;

            try
            {
                using var stream = ResourceLoader.Open("Assets/Credits.txt");
                if (stream is null)
                    return;

                using var reader = new StreamReader(stream);
                Credits.Text = reader.ReadToEnd();
            }
            catch
            {
                // Log io error
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
