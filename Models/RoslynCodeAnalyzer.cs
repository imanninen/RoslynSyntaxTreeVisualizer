using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using RoslynSyntaxTreeVisualizer.Models.SyntaxTreeData;

namespace RoslynSyntaxTreeVisualizer.Models;

public sealed class RoslynCodeAnalyzer
{
    private SyntaxNode GetRoot(string code)
    {
        var syntaxTree = CSharpSyntaxTree.ParseText(code);
        return syntaxTree.GetCompilationUnitRoot();
    }
    
    public ISyntaxTreeUnit Analyze(string code)
    {
        var root = GetRoot(code);
        var newRoot = ConstructValidSyntaxTreeStructureForMv(root);
        return newRoot;
    }

    private SyntaxTreeNode ConstructValidSyntaxTreeStructureForMv(SyntaxNode root)
    {
        var newChildren = new List<ISyntaxTreeUnit>();
        foreach (var child in root.ChildNodesAndTokens())
        {
            if (child.IsNode)
            {
                var newChild = ConstructValidSyntaxTreeStructureForMv(child.AsNode() !);
                newChildren.Add(newChild);
            }
            else
            {
                newChildren.Add(new SyntaxTreeToken(child.AsToken()));
            }
        }

        return new SyntaxTreeNode(root, new ReadOnlyCollection<ISyntaxTreeUnit>(newChildren));
    }
    
    private static void TraverseSyntaxTree(SyntaxTreeNode node, string indent = "")
    {
        Console.WriteLine($"{indent}{node.Kind}");
            
        foreach (var child in node.Children)
        {
            if (child.IsNode)
            {
                TraverseSyntaxTree((SyntaxTreeNode)child, indent + "  ");
            }
            else
            {
                Console.WriteLine($"{indent}{child.GetType()} - {child.ToString()}");
            }
        }
    }
    
}