// Copyright © John Gietzen. All Rights Reserved. This source is subject to the LICENSE license. Please see license.md for more information.

namespace Farkle
{
    using Microsoft.CodeAnalysis.Diagnostics;

    internal static class GeneratorExtensions
    {
        public static string? GetStringOption(this AnalyzerConfigOptions options, string key) =>
            options.TryGetValue(key, out var value)
                ? value
                : null;

        public static bool? GetBooleanOption(this AnalyzerConfigOptions options, string key) =>
            options.TryGetValue(key, out var value) && bool.TryParse(value, out var result)
                ? result
                : null;
    }
}
