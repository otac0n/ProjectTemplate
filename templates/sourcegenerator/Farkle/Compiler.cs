// Copyright © John Gietzen. All Rights Reserved. This source is subject to the LICENSE license. Please see license.md for more information.

namespace Farkle
{
    using System.IO;
    using Farkle.Model;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;
    using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

    /// <summary>
    /// Provides parsing and compilation services for Farkle files.
    /// </summary>
    public static class Compiler
    {
        /// <summary>
        /// Parses a Farkle file into its input representation.
        /// </summary>
        /// <param name="inputFile">The input file.</param>
        /// <param name="project">The project properties.</param>
        /// <returns>A <see cref="CompileResult{T}"/> containing the errors, warnings, and results of parsing.</returns>
        public static CompileResult<Farkle> ParseFile(FarkleItem inputFile, ProjectProperties project)
        {
            // TODO: Add parsing logic here.
            var parsed = new Farkle
            {
                Name = Path.GetFileNameWithoutExtension(inputFile.Identity),
                Text = inputFile.Text?.ToString() ?? string.Empty,
                Info = inputFile.MyStringOption,
            };

            return new CompileResult<Farkle>
            {
                Result = parsed,
            };
        }

        /// <summary>
        /// Compiles a parsed Farkle into its output representation.
        /// </summary>
        /// <param name="parsed">The parse result to compile.</param>
        /// <param name="project">The project properties.</param>
        /// <returns>A <see cref="CompileResult{T}"/> containing the errors, warnings, and results of compilation.</returns>
        public static CompileResult<string> Compile(Farkle? parsed, ProjectProperties project)
        {
            // TODO: Add compilation logic here. You can use https://github.com/KirillOsenkov/RoslynQuoter to create templates, or simply return a string.
            var literal =
                LiteralExpression(SyntaxKind.StringLiteralExpression, Literal(parsed.Text));

            var variable =
                VariableDeclaration(PredefinedType(Token(SyntaxKind.StringKeyword)))
                    .WithVariables(
                        SingletonSeparatedList(
                            VariableDeclarator(Identifier(parsed.Name))
                                .WithInitializer(
                                    EqualsValueClause(
                                        literal))));

            var field =
                FieldDeclaration(variable)
                    .WithModifiers(
                        TokenList([
                            Token(SyntaxKind.PublicKeyword),
                            Token(SyntaxKind.StaticKeyword),
                            Token(SyntaxKind.ReadOnlyKeyword)
                        ]));

            var @class =
                ClassDeclaration("FarkleFiles")
                    .WithModifiers(
                        TokenList([
                            Token(SyntaxKind.InternalKeyword),
                            Token(SyntaxKind.PartialKeyword)
                        ]))
                    .WithMembers(SingletonList<MemberDeclarationSyntax>(field));

            var @namespace =
                NamespaceDeclaration(
                    ParseName(project.RootNamespace))
                    .WithMembers(SingletonList<MemberDeclarationSyntax>(@class));

            var compiled =
                CompilationUnit()
                    .WithMembers(SingletonList<MemberDeclarationSyntax>(@namespace));

            return new CompileResult<string>
            {
                Result = compiled.NormalizeWhitespace().ToFullString(),
            };
        }
    }
}
