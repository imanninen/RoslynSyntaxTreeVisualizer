using System.Collections.ObjectModel;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace RoslynSyntaxTreeVisualizer.Models.SyntaxTreeData;

public interface ISyntaxTreeUnit
{
    SyntaxNodeOrToken WrappedNode { get; }
    bool IsNode { get; }
    string Kind { get; }

    ReadOnlyCollection<ISyntaxTreeUnit> Children { get; }
}