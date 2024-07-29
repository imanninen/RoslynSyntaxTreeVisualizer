using System.Collections.Generic;
using System.Collections.ObjectModel;
using Avalonia.Controls;
using Avalonia.Interactivity;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.CodeAnalysis;
using RoslynSyntaxTreeVisualizer.Models;
using RoslynSyntaxTreeVisualizer.Models.SyntaxTreeData;

namespace RoslynSyntaxTreeVisualizer.ViewModels;

public partial class MainWindowViewModel : ObservableObject
{
    private readonly RoslynCodeAnalyzer _analyzer = new RoslynCodeAnalyzer();

    [ObservableProperty] private string _code = "";

    [ObservableProperty] private ObservableCollection<ISyntaxTreeUnit> _root =
        new ObservableCollection<ISyntaxTreeUnit>{new SyntaxTreeNode(null, ReadOnlyCollection<ISyntaxTreeUnit>.Empty)};

    [ObservableProperty] private string _stringRepresentation = "";

    [RelayCommand]
    private void AnalyzeUserCode()
    {
        Root = new ObservableCollection<ISyntaxTreeUnit> { _analyzer.Analyze(Code) };
    }

    [RelayCommand]
    private void ShowStringRepresentation(string stringRepresentation)
    {
        StringRepresentation = stringRepresentation;
    }
    
}