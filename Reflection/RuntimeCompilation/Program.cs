using System;
using System.IO;
using System.Diagnostics;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Text;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Text.Json;

namespace RuntimeCompilation
{
    // https://www.damirscorner.com/blog/posts/20190802-CompilingAndExecutingCodeInACsApp.html
    class Program
    {
        static void Main(string[] args)
        {
            string src = File.ReadAllText("Source.txt");

            var syntaxTree = SyntaxFactory.ParseSyntaxTree(SourceText.From(src));

            var assemblyPath = "Program.exe";
            var dotNetCoreDir = Path.GetDirectoryName(typeof(object).Assembly.Location);

            var compilation = CSharpCompilation.Create(assemblyPath)
                .WithOptions(new CSharpCompilationOptions(OutputKind.ConsoleApplication))
                .AddReferences(
                    MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                    MetadataReference.CreateFromFile(typeof(Console).Assembly.Location),
                    MetadataReference.CreateFromFile(typeof(SyntaxTree).Assembly.Location),
                    MetadataReference.CreateFromFile(typeof(CSharpSyntaxTree).Assembly.Location),
                    MetadataReference.CreateFromFile(Path.Combine(dotNetCoreDir, "System.Runtime.dll"))
                )
                .AddSyntaxTrees(syntaxTree);

            var result = compilation.Emit(assemblyPath);

            if (result.Success)
            {
                File.WriteAllText(
                    Path.ChangeExtension(assemblyPath, "runtimeconfig.json"),
                    GenerateRuntimeConfig()
                );
                Process.Start("dotnet", assemblyPath);
            }
            else
            {
                Console.WriteLine(string.Join(
                    Environment.NewLine,
                    result.Diagnostics.Select(diagnostic => diagnostic.ToString())
                ));
            }
        }

        private static string GenerateRuntimeConfig()
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new Utf8JsonWriter(
                    stream,
                    new JsonWriterOptions() { Indented = true }
                ))
                {
                    writer.WriteStartObject();
                    writer.WriteStartObject("runtimeOptions");
                    writer.WriteStartObject("framework");
                    writer.WriteString("name", "Microsoft.NETCore.App");
                    writer.WriteString(
                        "version",
                        RuntimeInformation.FrameworkDescription.Replace(".NET ", "")
                    );
                    writer.WriteEndObject();
                    writer.WriteEndObject();
                    writer.WriteEndObject();
                }

                return Encoding.UTF8.GetString(stream.ToArray());
            }
        }
    }
}
