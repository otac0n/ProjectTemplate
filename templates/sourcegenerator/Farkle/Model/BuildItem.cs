// Copyright © John Gietzen. All Rights Reserved. This source is subject to the LICENSE license. Please see license.md for more information.

namespace Farkle.Model
{
    using Microsoft.CodeAnalysis.Text;

    /// <summary>
    /// Encapsulates an MSBuild file item.
    /// </summary>
    /// <param name="identity">The path to the item.</param>
    /// <param name="link">An optional project-relative path where the file should be considered to have been placed.</param>
    /// <param name="text">The source of the file.</param>
    public class BuildItem(string identity, string? link, SourceText? text)
    {
        /// <summary>
        /// Gets the path to the item.
        /// </summary>
        public string Identity { get; } = identity;

        /// <summary>
        /// Gets an optional project-relative path where the file should be considered to have been placed.
        /// </summary>
        public string? Link { get; } = string.IsNullOrEmpty(link) ? null : link;

        /// <summary>
        /// Gets the source of the file.
        /// </summary>
        public SourceText? Text { get; } = text;
    }
}
