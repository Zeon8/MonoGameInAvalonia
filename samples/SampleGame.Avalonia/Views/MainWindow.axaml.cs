using Avalonia.Controls;
using Avalonia.Interactivity;
using SampleGame.Avalonia.ViewModels;

namespace SampleGame.Avalonia.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object? sender, RoutedEventArgs e)
        {
            if (Design.IsDesignMode)
                return;

            var viewModel = (MainWindowViewModel)DataContext!;
            viewModel.RunGame();
        }
    }
}