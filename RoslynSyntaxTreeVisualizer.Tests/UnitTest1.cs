using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using RoslynSyntaxTreeVisualizer.Models;
using RoslynSyntaxTreeVisualizer.Models.SyntaxTreeData;

namespace RoslynSyntaxTreeVisualizer.Tests;

public class Tests
{
    private RoslynCodeAnalyzer _analyzer;

    [SetUp]
    public void Setup()
    {
        _analyzer = new RoslynCodeAnalyzer();
    }

    [Test]
    public void Test1()
    {
        const string code = @"using System.Collections.ObjectModel;
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
}";
        DoSimpleStructureTest(code);
    }

    [Test]
    public void Test2()
    {
        const string code = @"using System.Collections.ObjectModel;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace RoslynSyntaxTreeVisualizer.Models.SyntaxTreeData;

public interface ISyntaxTreeUnit
{
    SyntaxNodeOrToken WrappedNode { get; }
    bool IsNode { get; }
    string Kind { get; }

    ReadOnlyCollection<ISyntaxTreeUnit> Children { get; }
}";
        DoSimpleStructureTest(code);
    }

    private void DoSimpleStructureTest(string code)
    {
        ISyntaxTreeUnit myRoot = _analyzer.Analyze(code);
        SyntaxNode actualRoot = GetActualRoot(code);
        TraverseSyntaxTree(actualRoot, (SyntaxTreeNode)myRoot);
    }

    static SyntaxNode GetActualRoot(string code)
    {
        SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(code);
        return syntaxTree.GetCompilationUnitRoot();
    }

    static void TraverseSyntaxTree(SyntaxNode node, SyntaxTreeNode myNode)
    {
        Assert.That(string.Equals(node.Kind().ToString(), myNode.Kind),
            $"Kinds are different: (actual){node.Kind()} != {myNode.Kind}");
        Assert.That(string.Equals(node.ToString(), myNode.ToString()),
            $"ToStringRepresentation is not the same: (actual){node} != {myNode}");
        var dict = node.ChildNodesAndTokens()
            .Zip(myNode.Children, (k, v) =>
                new KeyValuePair<SyntaxNodeOrToken, ISyntaxTreeUnit>(k, v))
            .ToDictionary();
        foreach (var pair in dict)
        {
            if (pair.Key.IsNode)
            {
                TraverseSyntaxTree(pair.Key.AsNode() !, (SyntaxTreeNode)pair.Value);
            }
            else
            {
                var actualToken = pair.Key;
                var myToken = (SyntaxTreeToken)pair.Value;

                Assert.That(string.Equals(actualToken.Kind().ToString(), myToken.Kind),
                    $"Kinds are different: (actual){actualToken.Kind()} != {myToken.Kind}");
                Assert.That(string.Equals(actualToken.ToString(), myToken.StringRepresentation),
                    $"ToStringRepresentation is not the same: (actual){actualToken} != {myToken.StringRepresentation}");
            }
        }
    }
}