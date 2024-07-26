using System.Collections.ObjectModel;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.VisualBasic;

namespace RoslynSyntaxTreeVisualizer.Models.SyntaxTreeData;

public class SyntaxTreeNode(SyntaxNode node, ReadOnlyCollection<ISyntaxTreeUnit> children) : ISyntaxTreeUnit
{
    public SyntaxNodeOrToken WrappedNode => node;
    public bool IsNode { get; } = true;


    public string Kind { get; } = node != null ? node.Kind().ToString() : "Default";

    public string StringRepresentation { get; } = node != null ? node.ToString() : "Default";

    public ReadOnlyCollection<ISyntaxTreeUnit> Children => children;

    public override bool Equals(object? obj)
    {
        return WrappedNode.Equals(obj);
    }

    public override string ToString()
    {
        return WrappedNode.ToString();
    }

    public override int GetHashCode()
    {
        return WrappedNode.GetHashCode();
    }
}