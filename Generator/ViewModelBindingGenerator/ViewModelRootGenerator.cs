using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AlexMalyutinDev.ViewModelBinding.Generator;

[Generator]
public class ViewModelRootGenerator : ISourceGenerator
{
    public void Initialize(GeneratorInitializationContext context)
    {
        context.RegisterForSyntaxNotifications(() => new ViewModelRootSyntaxReciever());
    }

    public void Execute(GeneratorExecutionContext context)
    {
        if (context.SyntaxReceiver is ViewModelRootSyntaxReciever receiver)
        {
            Console.WriteLine(
                "[Generating] " + String.Join(
                    "\n",
                    receiver.ClassesWithAttributes.Select(info => $"{info.Namespace}::{info.Name}")
                )
            );

            foreach (var classInfo in receiver.ClassesWithAttributes)
            {
                var sb = new SourceBuilder();
                using (sb.Namespace(classInfo.Namespace))
                {
                    using (sb.Class("public partial", classInfo.Name))
                    {
                        using (sb.Method("protected override void InitPropertiesCache()"))
                        {
                            foreach (var property in classInfo.Properties)
                            {
                                sb.AppendLine(
                                    $"_propertiesCache[nameof({property})] = {property} = new();"
                                );
                            }
                        }

                        using (sb.Method("public string GetInfo()"))
                        {
                            sb.BeginLine("return \"").Append(classInfo.Name).Append('\"').EndLine();
                        }
                    }
                }

                context.AddSource($"{classInfo.Name}.g", sb.ToString());
            }
        }
    }

    private class ViewModelRootSyntaxReciever : ISyntaxReceiver
    {
        public List<ClassInfo> ClassesWithAttributes = new();

        public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
        {
            if (syntaxNode is ClassDeclarationSyntax classNode)
            {
                foreach (var attribute in classNode.AttributeLists)
                {
                    foreach (var attr in attribute.Attributes)
                    {
                        if (attr.Name is IdentifierNameSyntax { Identifier.Text: "ViewModelRootAttribute" }
                            or IdentifierNameSyntax { Identifier.Text: "ViewModelRoot" })
                        {
                            // TODO: Take argument!
                            // var targetTypeName = attr.ArgumentList?.Arguments.Count > 0
                            //     ? attr.ArgumentList?.Arguments[0].ToString().Replace(' ', '\t')
                            //     : "<null>";

                            // TODO: Use target class namespace to support partial
                            var namespaceName = "";
                            if (classNode.Parent is NamespaceDeclarationSyntax namespaceNode)
                            {
                                namespaceName = namespaceNode.Name.ToString();
                            }

                            var properties = new List<string>();
                            foreach (var childNode in classNode.ChildNodes())
                            {
                                if (childNode is not FieldDeclarationSyntax fieldNode)
                                {
                                    continue;
                                }

                                foreach (var variable in fieldNode.Declaration.Variables)
                                {
                                    var type = fieldNode.Declaration.Type;
                                    if (type.GetFirstToken().Text != "PropertyView")
                                    {
                                        continue;
                                    }

                                    var fieldName = variable.Identifier.Text;
                                    properties.Add(fieldName);
                                }
                            }

                            ClassesWithAttributes.Add(
                                new ClassInfo
                                {
                                    ClassNode = classNode,
                                    Name = classNode.Identifier.Text,
                                    Properties = properties,
                                    Namespace = namespaceName
                                }
                            );
                            Console.WriteLine($"[COLLECT] {namespaceName}::{classNode.Identifier.Text}");
                            return;
                        }
                    }
                }
            }
        }

        public struct ClassInfo
        {
            public ClassDeclarationSyntax ClassNode;
            public string Name;
            public string Namespace;
            public List<string> Properties;
        }
    }
}
