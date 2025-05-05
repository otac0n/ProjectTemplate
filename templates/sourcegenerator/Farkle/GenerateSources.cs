// Copyright © John Gietzen. All Rights Reserved. This source is subject to the LICENSE license. Please see license.md for more information.

namespace Farkle
{
    using System.Linq;
    using Farkle.Model;
    using Microsoft.CodeAnalysis;

    [Generator]
    internal class GenerateSources : IIncrementalGenerator
    {
        /// <summary>
        /// Used to determine which MSBuild item group included a specific item in the AdditionalFiles collection.
        /// </summary>
        /// <remarks>
        /// <see href="https://platform.uno/blog/using-msbuild-items-and-properties-in-c-9-source-generators"/>.
        /// </remarks>
        public const string SourceItemGroup = "build_metadata.AdditionalFiles.SourceItemGroup";

        // Updates to these constants require corresponding changes in 'Farkle.targets' and 'Model\FarkleItem.cs'.
        public const string MyBoolOption = $"build_metadata.{FarkleItem.ItemName}.{nameof(FarkleItem.MyBoolOption)}";
        public const string MyStringOption = $"build_metadata.{FarkleItem.ItemName}.{nameof(FarkleItem.MyStringOption)}";
        public const string Link = $"build_metadata.{FarkleItem.ItemName}.Link";

        private const string ProjectDir = "build_property.ProjectDir";
        private const string RootNamespace = "build_property.RootNamespace";

        /// <inheritdoc/>
        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
            var inputFiles = context.AdditionalTextsProvider
                .Combine(context.AnalyzerConfigOptionsProvider)
                .Select((pair, cancel) =>
                {
                    var text = pair.Left;
                    var options = pair.Right.GetOptions(text);
                    var source = options.GetStringOption(SourceItemGroup);
                    return source != FarkleItem.ItemName
                        ? null
                        : new FarkleItem(
                            identity: text.Path,
                            link: options.GetStringOption(Link),
                            text: text.GetText(cancel),
                            myBoolOption: options.GetBooleanOption(MyBoolOption),
                            myStringOption: options.GetStringOption(MyStringOption));
                })
                .Where(file => file != null);

            var projectProperties = context.AnalyzerConfigOptionsProvider
                .Select((p, ct) => new ProjectProperties(
                    projectDir: p.GlobalOptions.GetStringOption(ProjectDir),
                    rootNamespace: p.GlobalOptions.GetStringOption(RootNamespace)));

            var filesWithConfigs = inputFiles
                .Combine(projectProperties)
                .Select((p, ct) => new
                {
                    File = p.Left!,
                    ProjectProperties = p.Right,
                });

            context.RegisterSourceOutput(
                filesWithConfigs,
                (context, pair) => CompileItem(context, pair.File, pair.ProjectProperties));
        }

        private static void CompileItem(SourceProductionContext context, FarkleItem file, ProjectProperties project)
        {
            if (file.Text != null)
            {
                var relativePath = PathUtils.GetRelativePath(project.ProjectDir ?? string.Empty, file.Link ?? file.Identity);
                var parsedInput = Compiler.ParseFile(file, project);

                var hadFatal = parsedInput.ReportDiagnostics(context.ReportDiagnostic);
                if (hadFatal)
                {
                    return;
                }

                var compiledInput = Compiler.Compile(parsedInput.Result, project);

                hadFatal = compiledInput.ReportDiagnostics(context.ReportDiagnostic);
                if (hadFatal || compiledInput.Result == null)
                {
                    return;
                }

                context.AddSource(PathUtils.EncodePathAsFilename(relativePath) + ".g.cs", compiledInput.Result);
            }
        }
    }
}
