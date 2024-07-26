using Avalonia.Controls;
using Avalonia.Interactivity;
using RoslynSyntaxTreeVisualizer.ViewModels;

namespace RoslynSyntaxTreeVisualizer.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    // private void OnOpenSecondWindowClick(object? sender, RoutedEventArgs e)
    // {
    //     if (SubmitCodeButton.IsEnabled)
    //     {
    //         SubmitCodeButton.IsEnabled = false;
    //         
    //         var ownerWindow = this;
    //         var window = new TreeViewWindow();
    //         window.DataContext = ownerWindow.DataContext;
    //         window.Closing += (o, args) => { SubmitCodeButton.IsEnabled = true; };
    //         window.ShowDialog(ownerWindow);
    //     }
    // }
}