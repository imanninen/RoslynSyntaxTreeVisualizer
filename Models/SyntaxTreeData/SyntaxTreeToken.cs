using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;


namespace RoslynSyntaxTreeVisualizer.Models.SyntaxTreeData;

public class SyntaxTreeToken(
    SyntaxToken token) : ISyntaxTreeUnit
{
    public SyntaxNodeOrToken WrappedNode => token;
    public bool IsNode { get; } = false;

    public string Kind { get; } = token.Kind().ToString();

    public string StringRepresentation { get; } = token.ToString();
    public ReadOnlyCollection<ISyntaxTreeUnit> Children => new(new List<ISyntaxTreeUnit>());

    public override string ToString()
    {
        return WrappedNode.ToString();
    }
}