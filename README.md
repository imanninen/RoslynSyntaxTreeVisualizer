# Roslyn syntax tree visualizer

This is a simple application, that accepts users C# code and show syntax tree of this code.

As main idea I used `Microsoft.CodeAnalysis.CSharp` to get Syntax tree from raw code. Then I created a wrapper around
`SyntaxNodeOrToken` which is a base class of elements of syntax tree.

I wore [test](RoslynSyntaxTreeVisualizer.Tests/UnitTest1.cs) for ensuring, that my tree construction
function works properly.

Also, I installed some additional packages for simplicity. You may see all my external
dependencies [here](RoslynSyntaxTreeVisualizer/RoslynSyntaxTreeVisualizer.csproj).