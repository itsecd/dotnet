using System;
using System.Collections.ObjectModel;

using Avalonia.Controls;
using Avalonia.Interactivity;

namespace Avalonia1
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            IntegerListBox.Items = _list;
        }

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e) =>
            RemoveButton.IsEnabled = IntegerListBox.SelectedItem != null;

        private void OnAddClick(object sender, RoutedEventArgs e) =>
            _list.Add(_random.Next(5));

        private void OnRemoveClick(object sender, RoutedEventArgs e) =>
            _list.RemoveAt(IntegerListBox.SelectedIndex);

        private void OnExitClick(object sender, RoutedEventArgs e) => Close();

        private readonly ObservableCollection<int> _list = new();
        private readonly Random _random = new();
    }
}
