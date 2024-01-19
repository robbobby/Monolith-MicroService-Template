using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace XamlKeyGenerator;

[Generator]
public class XamlSourceGenerator : IIncrementalGenerator {
    public void Initialize(IncrementalGeneratorInitializationContext context) {
        var xamlFiles = context.AdditionalTextsProvider.Where(f => f.Path.EndsWith("ResourceBase.axaml"));

        context.RegisterImplementationSourceOutput(xamlFiles, (context, xamlFile) => {
            try {
                var xaml =
                    XDocument.Parse(xamlFile.GetText(context.CancellationToken)?.ToString() ?? string.Empty);

                var keys = GetResourceKeys(xaml);

                if(!keys.Any())
                    return;
                var source = new StringBuilder();
                source.AppendLine("// <auto-generated/>");
                source.AppendLine("namespace ComponentLibrary;");
                source.AppendLine("public abstract partial class ResourceKeys");
                source.AppendLine("{");

                foreach (var group in keys.GroupBy(k => string.Join(".", k.Parts.Take(k.Parts.Count - 1))))
                    GenerateKeyClass(group.Key, group.Select(k => k.Parts.Last()), source);

                source.AppendLine("}");

                var fileName = Path.GetFileNameWithoutExtension(xamlFile.Path);
                context.AddSource($"{fileName}.ResourceKeys.g.cs", SourceText.From(source.ToString(), Encoding.UTF8));
            }
            catch (Exception e) {
                // existing code...
            }
        });
    }

    private static void GenerateKeyClass(string classPath, IEnumerable<string> properties, StringBuilder source) {
        var parts = classPath.Split('.').ToList();
        GenerateNestedClasses(parts, properties, source, 1);
    }

    private static void GenerateNestedClasses(List<string> parts, IEnumerable<string> properties, StringBuilder source,
                                              int depth) {
        var indent = new string(' ', depth * 4);
        if(parts.Count == 1) {
            source.AppendLine($"{indent}public partial class {parts[0]}");
            source.AppendLine($"{indent}{{");
            foreach (var property in properties)
                source.AppendLine(
                    $"{indent}    public static string {property} = \"{string.Join(".", parts)}.{property}\";");
            source.AppendLine($"{indent}}}");
        } else {
            source.AppendLine($"{indent}public partial class {parts[0]}");
            source.AppendLine($"{indent}{{");
            GenerateNestedClasses(parts.Skip(1).ToList(), properties, source, depth + 1);
            source.AppendLine($"{indent}}}");
        }
    }

    private static List<(string Key, List<string> Parts)> GetResourceKeys(XDocument xaml) {
        var keys = xaml.Descendants()
            .SelectMany(e => e.Attributes())
            .Where(a => a.Name == XName.Get("Key", "http://schemas.microsoft.com/winfx/2006/xaml"))
            .Select(a => a.Value)
            .Select(k => (Key: k, Parts: k.Split('.').ToList()))
            .ToList();
        return keys;
    }
}
