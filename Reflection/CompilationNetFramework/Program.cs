using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace CompilationNetFramework
{
    class Program
    {
        public static void Main()
        {
            string src = File.ReadAllText("Source.txt");
            var syntaxTree = SyntaxFactory.ParseSyntaxTree(SourceText.From(src));

            var assemblyPath = Path.ChangeExtension(Path.GetTempFileName(), "exe");

            var compilation = CSharpCompilation.Create(Path.GetFileName(assemblyPath))
                .WithOptions(new CSharpCompilationOptions(OutputKind.ConsoleApplication))
                .AddReferences(
                    MetadataReference.CreateFromFile(typeof(object).Assembly.Location)
                )
                .AddSyntaxTrees(syntaxTree);

            var result = compilation.Emit(assemblyPath);

            if (result.Success)
            {
                Process.Start(assemblyPath);
            }
            else
            {
                Console.WriteLine(string.Join(
                    Environment.NewLine,
                    result.Diagnostics.Select(diagnostic => diagnostic.ToString())
                ));
            }
        }
    }
}
