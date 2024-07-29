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

    [ObservableProperty] private ReadOnlyCollection<ISyntaxTreeUnit> _root =
        new (new List<ISyntaxTreeUnit>{new SyntaxTreeNode(null, ReadOnlyCollection<ISyntaxTreeUnit>.Empty)});

    [ObservableProperty] private string _stringRepresentation = "";

    [RelayCommand]
    private void AnalyzeUserCode()
    {
        Root = _analyzer.Analyze(Code).Children;
    }

    [RelayCommand]
    private void ShowStringRepresentation(string stringRepresentation)
    {
        StringRepresentation = stringRepresentation;
    }
    
}